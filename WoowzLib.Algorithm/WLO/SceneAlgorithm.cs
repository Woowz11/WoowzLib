using System.Text;
using WLO.Attribute;

namespace WLO;

/// <summary>
/// Режимы кеширования для SceneAlgorithm
/// </summary>
public enum SceneCacheMode{
    /// <summary>
    /// Не кеширует Childrens, каждый вызов Childrens высчитывает его с нуля
    /// </summary>
    None,
    /// <summary>
    /// Кеширует Childrens только у SceneAlgorithm
    /// </summary>
    SceneOnly,
    /// <summary>
    /// Кеширует Childrens везде, у SceneAlgorithm и у SceneNode, но потребляет больше памяти и возможны фризы при изменении детей/родителей
    /// </summary>
    Full
}

[WoowzLibHint(Information.New)]
public class SceneAlgorithm<T> where T : SceneObject<T>{
    public SceneAlgorithm(object? Data = null, SceneCacheMode Mode = SceneCacheMode.SceneOnly){ ID = __ID++; this.Data = Data; CacheMode = Mode; }

    /// <summary>
    /// Произвольные данные сцены, возможно объект к которому привязана сцена
    /// </summary>
    public readonly object? Data;
    
    /// <summary>
    /// Режим кеширования
    /// </summary>
    public readonly SceneCacheMode CacheMode;

    /// <summary>
    /// Используется кеширование Childrens у сцены?
    /// </summary>
    public bool UseSceneCache => CacheMode != SceneCacheMode.None;
    /// <summary>
    /// Используется кеширование Childrens у детей сцены?
    /// </summary>
    public bool UseNodeCache  => CacheMode == SceneCacheMode.Full;
    
    /// <summary>
    /// Корневые дети (первый слой)
    /// </summary>
    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    /// <summary>
    /// Все дети сцены, и корневые (CacheMode влияет)
    /// </summary>
    public IReadOnlyCollection<SceneNode<T>> Childrens => UseSceneCache ? __Descendants : __CalculateDescendants();
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Кол-во корневых детей
    /// </summary>
    public int Count => __Level0.Count;
    
    /// <summary>
    /// Есть объект в корневых детях?
    /// </summary>
    public bool Contains(SceneNode<T> Node) => __Level0.Contains(Node);

    /// <summary>
    /// Есть объект на сцене? (CacheMode влияет)
    /// </summary>
    public bool ContainsDescendant(SceneNode<T> node){
        if(!UseSceneCache){ Logger.Warn("Вызван \"ContainsDescendant\" у [" + this + "], лучше установить другой CacheMode, потому-что каждый раз при вызове происходит пересборка Childrens!"); }
        return Childrens.Contains(node);
    }

    /// <summary>
    /// Добавить новый объект на сцену
    /// </summary>
    /// <returns>Новый объект</returns>
    public SceneNode<T>? Add(T Object) => Add(Object.Node);

    /// <summary>
    /// Добавить объект на сцену
    /// </summary>
    /// <returns>Добавленный объект</returns>
    public SceneNode<T>? Add(SceneNode<T> Node){
        try{
            if(Node == null){ throw new Exception("Добавляемый объект не может быть null!"); }

            if(Node.Scene == this){ return Node; }

            if(Node.Scene != null){ Node.Scene.Remove(Node); }

            Node.Parent = null;

            try{
                if(OnBeforeAdd != null){
                    foreach(Delegate Delegate in OnBeforeAdd.GetInvocationList()){
                        Func<SceneAlgorithm<T>, SceneNode<T>, SceneNode<T>?> Func = (Func<SceneAlgorithm<T>, SceneNode<T>, SceneNode<T>?>)Delegate;
                        Node = Func(this, Node)!;
                        if(Node == null){ return null; }
                    }
                }
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnBeforeAdd у SceneAlgorithm [" + this + "]!\nОбъект: " + Node, e);
            }

            __Level0.Add(Node);
            Node.__SetScene(this);

            if(UseSceneCache){ __AddTree(Node); }

            try{
                if(OnAfterAdd != null){
                    foreach(Delegate Delegate in OnAfterAdd.GetInvocationList()){
                        Action<SceneAlgorithm<T>, SceneNode<T>> Action = (Action<SceneAlgorithm<T>, SceneNode<T>>)Delegate;
                        Action(this, Node);
                    }
                }
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnAfterAdd у SceneAlgorithm [" + this + "]!\nОбъект: " + Node, e);
            }

            return Node;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении объекта сцене [" + this + "]!\nОбъект: " + Node, e);
        }
    }

    /// <summary>
    /// Удалить объект со сцены
    /// </summary>
    public void Remove(SceneNode<T> Node){
        try{
            if(Node == null){ throw new Exception("Удаляемый объект не может быть null!"); }
            
            if(!__Level0.Contains(Node)){ throw new Exception("Сцена указанного объекта, не является указанной сценой");  }

            try{
                if(OnBeforeRemove != null && OnBeforeRemove.GetInvocationList().Cast<Func<SceneAlgorithm<T>, SceneNode<T>, bool>>().Any(Func => !Func(this, Node))){ return; }
            }catch(Exception e){
                Logger.Error("Ошибка OnBeforeRemove у SceneAlgorithm [" + this + "]!\nОбъект: " + Node, e);
            }
            
            __Level0.Remove(Node);

            if(UseSceneCache){ __RemoveTree(Node); }

            Node.__SetScene(null);
            
            try{
                OnAfterRemove?.GetInvocationList().Cast<Action<SceneAlgorithm<T>, SceneNode<T>>>().ToList().ForEach(A => A(this, Node));
            }catch(Exception e){
                Logger.Error("Ошибка OnAfterRemove у SceneAlgorithm [" + this + "]!\nОбъект: " + Node, e);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при удалении объекта со сцены [" + this + "]!\nОбъект: " + Node, e);
        }
    }

    /// <summary>
    /// Удалить все объекты на сцене
    /// </summary>
    public void Clear(){ foreach(SceneNode<T> Node in __Level0.ToList()){ Remove(Node); } }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Вызывается перед добавлением элемента на сцену (Сцена, Элемент) => (Какой элемент добавить? (если null, ничего не добавляет))
    /// </summary>
    public event Func<SceneAlgorithm<T>, SceneNode<T>, SceneNode<T>?>? OnBeforeAdd;
    
    /// <summary>
    /// Вызывается после добавления элемента на сцену (Сцена, Элемент)
    /// </summary>
    public event Action<SceneAlgorithm<T>, SceneNode<T>>? OnAfterAdd;

    /// <summary>
    /// Вызывается перед удалением элемента со сцены (Сцена, Элемент) => (Удалить элемент?)
    /// </summary>
    public event Func<SceneAlgorithm<T>, SceneNode<T>, bool>? OnBeforeRemove;
    
    /// <summary>
    /// Вызывается после удаления элемента со сцены (Сцена, Элемент)
    /// </summary>
    public event Action<SceneAlgorithm<T>, SceneNode<T>>? OnAfterRemove;
    
    // ----------------------------------------------------------------------
    
    private static  long __ID;
    public readonly long ID;
    
    internal readonly HashSet<SceneNode<T>> __Level0      = [];
    internal readonly HashSet<SceneNode<T>> __Descendants = [];
    
    internal void __AddTree(SceneNode<T> Node){
        if(!__Descendants.Add(Node)){ return; }

        foreach(SceneNode<T> c in Node.Level0){ __AddTree(c); }
    }

    internal void __RemoveTree(SceneNode<T> Node){
        if(!__Descendants.Remove(Node)){ return; }

        foreach(SceneNode<T> Node__ in Node.Level0){ __RemoveTree(Node__); }
    }

    private HashSet<SceneNode<T>> __CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];

        void Recurse(SceneNode<T> Node){

            if(!Result.Add(Node)){ return; }

            foreach(SceneNode<T> Node__ in Node.Level0){ Recurse(Node__); }
        }

        foreach(SceneNode<T> Node__ in __Level0){ Recurse(Node__); }

        return Result;
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"SceneAlg.({Count}{(UseSceneCache ? $" ({__Descendants.Count})" : "")})";

    public string ToHierarchyString(){
        StringBuilder SB = new StringBuilder();
        SB.AppendLine(ToString());

        List<SceneNode<T>> Level0__ = __Level0.ToList();

        for(int i = 0; i < Level0__.Count; i++){ SB.Append(Level0__[i].ToHierarchyString("", i == Level0__.Count - 1)); }

        return SB.ToString();
    }

    public override bool Equals(object? Object) => Object is SceneAlgorithm<T> Other && ID == Other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.New)]
public class SceneNode<T> where T : SceneObject<T>{
    public SceneNode(T Self){ ID = __ID++; this.Self = Self; }
    
    /// <summary>
    /// Сам объект
    /// </summary>
    public readonly T Self;

    /// <summary>
    /// Используется кеширование Childrens у сцены?
    /// </summary>
    public bool UseSceneCache => __Scene?.CacheMode != SceneCacheMode.None;
    /// <summary>
    /// Используется кеширование Childrens у детей сцены?
    /// </summary>
    public bool UseNodeCache  => __Scene?.CacheMode == SceneCacheMode.Full;

    /// <summary>
    /// Нельзя изменять?
    /// </summary>
    public bool IsReadOnly{ get; private set; }

    /// <summary>
    /// На какой сцене находится объект?
    /// </summary>
    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            if(__Scene == value){ return; }
            if(IsReadOnly){ throw new Exception("Нельзя изменять!"); }

            if(value != null){ Parent = null; }

            try{
                if(OnSceneChangeBefore != null && OnSceneChangeBefore.GetInvocationList().Cast<Func<SceneNode<T>, SceneAlgorithm<T>?, SceneAlgorithm<T>?, bool>>().Any(Func => !Func(this, __Scene, value))){ return; }
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnSceneChangeBefore у Node [" + this + "]!\nНовая сцена: " + WL.__Base.Other.ToString(value), e);
            }

            SceneAlgorithm<T>? OldScene = __Scene;
            
            if(__Scene != null){
                __Scene.__Level0.Remove(this);

                if(__Scene.UseSceneCache){ __Scene.__RemoveTree(this); }
            }

            __Scene = value;

            if(__Scene != null){
                __Scene.__Level0.Add(this);

                if(__Scene.UseSceneCache){
                    __Scene.__AddTree(this);
                }
            }

            foreach(SceneNode<T> Node in __Level0){ Node.__SetScene(__Scene); }
            
            try{
                OnSceneChangeAfter?.GetInvocationList().Cast<Action<SceneNode<T>, SceneAlgorithm<T>?, SceneAlgorithm<T>?>>().ToList().ForEach(A => A(this, OldScene, value));
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnSceneChangeAfter у Node [" + this + "]!\nНовая сцена: " + WL.__Base.Other.ToString(value), e);
            }
        }
    }

    /// <summary>
    /// В памяти? (есть сцена?)
    /// </summary>
    public bool InMemory => __Scene == null;
    
    /// <summary>
    /// Родитель объекта
    /// </summary>
    public SceneNode<T>? Parent{
        get => __Parent;
        set{
            if(__Parent == value){ return; }
            if(IsReadOnly){ throw new Exception("Нельзя изменять!"); }

            try{
                if(OnParentChangeBefore != null && OnParentChangeBefore.GetInvocationList().Cast<Func<SceneNode<T>, SceneNode<T>?, SceneNode<T>?, bool>>().Any(Func => !Func(this, __Parent, value))){ return; }
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnParentChangeBefore у Node [" + this + "]!\nНовый родитель: " + WL.__Base.Other.ToString(value), e);
            }

            SceneNode<T>? OldParent = __Parent;
            
            if(value != null){
                SceneNode<T>? Current = value;
                while(Current != null){
                    if(Current == this){
                        throw new Exception("Нельзя сделать родителем своего потомка! (цикл)");
                    }
                    Current = Current.Parent;
                }
            }

            if(__Parent != null){
                __Parent.__Level0.Remove(this);
                __Parent.__PropagateRemove(this);

                if(__Parent.__Scene?.UseSceneCache == true){
                    __Parent.__Scene.__RemoveTree(this);
                }
            }

            __Parent = value;

            if(__Parent != null){
                __Parent.__Level0.Add(this);
                __Parent.__PropagateAdd(this);

                if(__Parent.__Scene?.UseSceneCache == true){
                    __Parent.__Scene.__AddTree(this);
                }

                __SetScene(__Parent.__Scene);
            }
            
            try{
                OnParentChangeAfter?.GetInvocationList().Cast<Action<SceneNode<T>, SceneNode<T>?, SceneNode<T>?>>().ToList().ForEach(A => A(this, OldParent, value));
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивента OnParentChangeAfter у Node [" + this + "]!\nНовый родитель: " + WL.__Base.Other.ToString(value), e);
            }
        }
    }

    /// <summary>
    /// Есть родитель?
    /// </summary>
    public bool HasParent => Parent != null;
    
    /// /// <summary>
    /// Корневые дети (первый слой)
    /// </summary>
    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    /// <summary>
    /// Все дети объекта, и корневые (CacheMode влияет)
    /// </summary>
    public IReadOnlyCollection<SceneNode<T>> Childrens => UseNodeCache ? __Descendants : __CalculateDescendants();
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Кол-во корневых детей
    /// </summary>
    public int Count => __Level0.Count;

    /// <summary>
    /// Есть объект в корневых детях?
    /// </summary>
    public bool Contains(SceneNode<T> Node) => __Level0.Contains(Node);

    /// <summary>
    /// Есть объект на сцене? (CacheMode влияет)
    /// </summary>
    public bool ContainsDescendant(SceneNode<T> node){
        if(!UseSceneCache){ Logger.Warn("Вызван \"ContainsDescendant\" у [" + this + "], лучше установить другой CacheMode, потому-что каждый раз при вызове происходит пересборка Childrens!"); }
        return Childrens.Contains(node);
    }
    
    /// <summary>
    /// Добавить новый объект в объект
    /// </summary>
    /// <returns>Новый объект</returns>
    public SceneNode<T> Add(T Object) => Add(Object.Node);

    /// <summary>
    /// Добавить объект в объект
    /// </summary>
    /// <returns>Добавленный объект</returns>
    public SceneNode<T> Add(SceneNode<T> Node){
        try{
            if(IsReadOnly){ throw new Exception("Нельзя изменять!"); }
            Node.Parent = this;
            return Node;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении объекта объекту [" + this + "]!\nОбъект: " + Node, e);
        }
    }

    /// <summary>
    /// Удаляет объект
    /// </summary>
    public void Remove(SceneNode<T> Node){
        try{
            if(Node.Parent != this){ throw new Exception("Родитель указанного объекта, не является указанным родителем"); }
            if(IsReadOnly){ throw new Exception("Нельзя изменять!"); }
            
            Node.Parent = null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при удалении объекта с объекта [" + this + "]!\nОбъект: " + Node, e);
        }
    }

    /// <summary>
    /// Удаляет все объекты
    /// </summary>
    public void Clear(){
        try{
            if(IsReadOnly){ throw new Exception("Нельзя изменять!"); }
            foreach(SceneNode<T> Node in __Level0.ToList()){ Remove(Node); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при удалении всех объектов у объекта [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Возвращает всех родителей этого объекта, заканчивая сценой (null)
    /// </summary>
    public IEnumerable<SceneNode<T>> Parents(){
        SceneNode<T>? Current = Parent;
        while(Current != null){
            yield return Current;
            Current = Current.Parent;
        }
    }

    /// <summary>
    /// Запретить изменять
    /// </summary>
    public void Freeze() => IsReadOnly = true;
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Вызывается перед изменением сцены у объекта (Объект, Старая сцена, Новая сцена) => (Отменить изменение?)
    /// </summary>
    public event Func<SceneNode<T>, SceneAlgorithm<T>?, SceneAlgorithm<T>?, bool>? OnSceneChangeBefore;
    
    /// <summary>
    /// Вызывается после изменением сцены у объекта (Объект, Старая сцена, Новая сцена)
    /// </summary>
    public event Action<SceneNode<T>, SceneAlgorithm<T>?, SceneAlgorithm<T>?>? OnSceneChangeAfter;
    
    /// <summary>
    /// Вызывается перед изменением родителя у объекта (Объект, Старый родитель, Новый родитель) => (Отменить изменение?)
    /// </summary>
    public event Func<SceneNode<T>, SceneNode<T>?, SceneNode<T>?, bool>? OnParentChangeBefore;
    
    /// <summary>
    /// Вызывается после изменением родителя у объекта (Объект, Старый родитель, Новый родитель)
    /// </summary>
    public event Action<SceneNode<T>, SceneNode<T>?, SceneNode<T>?>? OnParentChangeAfter;
    
    [WoowzLibHint(Information.WorkInProgress)]
    public event Func<SceneNode<T>, SceneNode<T>, bool>? OnChildAddedBefore;
    
    [WoowzLibHint(Information.WorkInProgress)]
    public event Action<SceneNode<T>, SceneNode<T>>? OnChildAddedAfter;

    [WoowzLibHint(Information.WorkInProgress)]
    public event Func<SceneNode<T>, SceneNode<T>, bool>? OnChildRemovedBefore;
    
    [WoowzLibHint(Information.WorkInProgress)]
    public event Action<SceneNode<T>, SceneNode<T>>? OnChildRemovedAfter;
    
    // ----------------------------------------------------------------------
    
    private static  long __ID;
    public readonly long ID;

    private SceneAlgorithm<T>? __Scene;
    private SceneNode<T>?      __Parent;

    private readonly HashSet<SceneNode<T>> __Level0      = [];
    private readonly HashSet<SceneNode<T>> __Descendants = [];
    
    internal void __SetScene(SceneAlgorithm<T>? Scene){
        __Scene = Scene;

        foreach(SceneNode<T> Node in __Level0){ Node.__SetScene(Scene); }
    }

    internal void __AddTree(SceneNode<T> Node){
        if(!__Descendants.Add(Node)){ return; }

        foreach(SceneNode<T> Node__ in Node.Level0){ __AddTree(Node__); }
    }

    internal void __RemoveTree(SceneNode<T> Node){
        if(!__Descendants.Remove(Node)){ return; }

        foreach(SceneNode<T> Node__ in Node.Level0){ __RemoveTree(Node__); }
    }

    private void __PropagateAdd(SceneNode<T> Node){
        SceneNode<T>? Current = this;

        while(Current != null){
            if(Current.UseNodeCache){ Current.__AddTree(Node); }

            Current = Current.Parent;
        }
    }

    private void __PropagateRemove(SceneNode<T> Node){

        SceneNode<T>? Current = this;

        while(Current != null){
            if(Current.UseNodeCache){ Current.__RemoveTree(Node); }

            Current = Current.Parent;
        }
    }

    private HashSet<SceneNode<T>> __CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];

        void Recurse(SceneNode<T> Node){
            if(!Result.Add(Node)){ return; }

            foreach(SceneNode<T> Node__ in Node.Level0){ Recurse(Node__); }
        }

        foreach(SceneNode<T> Node__ in __Level0){ Recurse(Node__); }

        return Result;
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Count})";
    
    public string ToHierarchyString(string Indent = "", bool Last = true){

        StringBuilder SB = new StringBuilder();

        string Pointer = Last ? "└─ " : "├─ ";
        SB.AppendLine($"{Indent}{Pointer}{Self}");

        string ChildIndent = Indent + (Last ? "   " : "│  ");
        List<SceneNode<T>> List = __Level0.ToList();

        for(int i = 0; i < List.Count; i++){ SB.Append(List[i].ToHierarchyString(ChildIndent, i == List.Count - 1)); }

        return SB.ToString();
    }

    public override bool Equals(object? Object) => Object is SceneNode<T> Other && ID == Other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.New)]
public abstract class SceneObject<T> where T : SceneObject<T>{
    private SceneNode<T>? __Node;

    /// <summary>
    /// Нода объекта
    /// </summary>
    public SceneNode<T> Node => __Node ??= new SceneNode<T>((T)this);
}