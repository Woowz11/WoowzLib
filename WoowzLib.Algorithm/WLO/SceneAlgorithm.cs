using System.Text;
using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.Testing)]
public class SceneAlgorithm<T> where T : SceneObject<T>{
    // Переменная, указывающая на сколько возможен по глубине иерархия


    public SceneAlgorithm(object? Data = null, bool CacheChildrens = true){ ID = TotalID++; this.Data = Data; this.CacheChildrens = CacheChildrens; }
    private readonly long ID;
    private static   long TotalID;

    /// <summary>
    /// Дополнительная информация, возможно привязанный компонент
    /// </summary>
    public object? Data{ get; private set; }

    /// <summary>
    /// Кешировать детей? Если да, то будет получать список Childrens со скоростью O(1), но будет больше потреблять памяти, иначе каждый раз будет вычислять список со скоростью O(N)
    /// </summary>
    public readonly bool CacheChildrens;

    // ----------------------------------------------------------------------

    public IReadOnlyCollection<SceneNode<T>> Layer0 => __Layer0;

    public IReadOnlyCollection<SceneNode<T>> Childrens{
        get{
            if(CacheChildrens){
                return __Childrens;
            }else{
                return __CalculateChildrens();
            }    
        }  
    }

    public int Count => __Layer0.Count;

    public bool Contains(SceneNode<T> Node) => __Layer0.Contains(Node);
    
    public bool ContainsDescendant(SceneNode<T> Node){
        if(!CacheChildrens){ Logger.Warn("бебебе бабабаба"); }
        return Childrens.Contains(Node);
    }
    
    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Scene = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Scene != this){ throw new Exception("Сцена Node не равна указанной сцене!"); }
        Node.Scene = null;
    }

    public void Clear(){
        List<SceneNode<T>> Layer0__ = __Layer0.ToList();
        foreach(SceneNode<T> VARIABLE in Layer0__){
            Remove(VARIABLE);
        }
    }
    
    // ----------------------------------------------------------------------

    internal readonly HashSet<SceneNode<T>> __Layer0    = [];
    internal readonly HashSet<SceneNode<T>> __Childrens = [];

    private HashSet<SceneNode<T>> __CalculateChildrens(){
        HashSet<SceneNode<T>> Result = [];

        void __Add(SceneNode<T> Node){
            if(!Result.Add(Node)){ return; }
            foreach(SceneNode<T> Node__ in Node.__Layer0){
                __Add(Node__);
            }
        }
        
        foreach(SceneNode<T> Node in __Layer0){
            __Add(Node);
        }
        
        return Result;
    }

    internal void __AddTree(SceneNode<T> Node){
        if(!__Childrens.Add(Node)){ return; }
        foreach(SceneNode<T> Node__ in Node.__Layer0){
            __AddTree(Node__);
        }
    }
    
    internal void __RemoveTree(SceneNode<T> Node){
        if(!__Childrens.Remove(Node)){ return; }
        foreach(SceneNode<T> Node__ in Node.__Layer0){
            __RemoveTree(Node__);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SceneAlg.(" + Count + (CacheChildrens ? "(" + __Childrens.Count + ")" : "") + ")";
    
    public string ToHierarchyString(){
        StringBuilder SB = new StringBuilder();

        SB.Append(ToString() + "\n");
        
        var roots = __Layer0.ToList();
        for(int i = 0; i < roots.Count; i++){
            bool isLast = i == roots.Count - 1;
            SB.Append(roots[i].ToHierarchyString("", isLast));
        }
        return SB.ToString();
    }
    
    public override bool Equals(object? obj){
        if(obj is SceneAlgorithm<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode(){
        return ID.GetHashCode();
    }
}

[WoowzLibHint(Information.Testing)]
public class SceneNode<T> where T : SceneObject<T>{
    public SceneNode(T Self, bool CacheChildrens = true){
        ID = TotalID++;
        this.Self = Self;
    }
    private readonly long ID;
    private static   long TotalID;

    public readonly T Self;

    public bool CacheChildrens => Scene is{ CacheChildrens: true };

    public bool InMemory => Scene == null;
    
    // ----------------------------------------------------------------------

    private void __SetScene(SceneAlgorithm<T>? value, bool changeparent){
        if(__Scene == value){ return; }

        if(changeparent){ Parent = null; }
            
        if(__Scene != null){
            __Scene.__Layer0.Remove(this);
            if(CacheChildrens){ __Scene.__RemoveTree(this); }
        }
            
        __Scene = value;

        if(__Scene != null){
            __Scene.__Layer0.Add(this);
            if(CacheChildrens){ __Scene.__AddTree(this); }
        }
            
        foreach(var child in __Layer0){
            child.__Scene = value; // без изменения parent
        }
    }
    
    private SceneAlgorithm<T>? __Scene;
    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set => __SetScene(value, true);
    }

    private SceneNode<T>? __Parent;

    public SceneNode<T>? Parent{
        get => __Parent;
        set{
            if(__Parent == value){ return; }

            if(__Parent != null){
                __Parent.__Layer0.Remove(this);
                if(__Parent.CacheChildrens){
                    __Parent.Scene!.__RemoveTree(this);
                }
            }

            __Parent = value;

            if(__Parent != null){
                __Parent.__Layer0.Add(this);
                if(__Parent.CacheChildrens){
                    __Parent.Scene!.__AddTree(this);
                }

                __SetScene(__Parent.Scene, false);
            }
        }
    }
    
    public IReadOnlyCollection<SceneNode<T>> Layer0 => __Layer0;

    public IReadOnlyCollection<SceneNode<T>> Childrens{
        get{
            if(CacheChildrens){
                return __Childrens;
            }else{
                return __CalculateChildrens();
            }
        }
    }

    public int Count => __Layer0.Count;
    
    public bool Contains(SceneNode<T> Node) => __Layer0.Contains(Node);

    public bool ContainsDescendant(SceneNode<T> Node){
        if(!CacheChildrens){ Logger.Warn("бебебе бабабаба"); }
        return Childrens.Contains(Node);
    }
    
    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Parent = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Parent != this){ throw new Exception("Родитель Node не равен указанному родителю!"); }
        Node.Parent = null;
    }
    
    public void Clear(){
        List<SceneNode<T>> Layer0__ = __Layer0.ToList();
        foreach(SceneNode<T> VARIABLE in Layer0__){
            Remove(VARIABLE);
        }
    }
    
    // ----------------------------------------------------------------------

    internal readonly HashSet<SceneNode<T>> __Layer0    = [];
    internal readonly HashSet<SceneNode<T>> __Childrens = [];

    private HashSet<SceneNode<T>> __CalculateChildrens(){
        HashSet<SceneNode<T>> Result = [];

        void __Add(SceneNode<T> Node){
            if(!Result.Add(Node)){ return; }
            foreach(SceneNode<T> Node__ in Node.__Layer0){
                __Add(Node__);
            }
        }
        
        foreach(SceneNode<T> Node in __Layer0){
            __Add(Node);
        }
        
        return Result;
    }
    
    // ----------------------------------------------------------------------
    
    public override string ToString() => "SN(" + Self + ", " + WL.__Base.Other.ToString(Parent) + " (" + (Scene == null ? "В памяти" : "На сцене") + "), " + Layer0.Count + " (" + Childrens.Count + "))";

    public string ToHierarchyString(string Indent = "", bool Last = true){
        StringBuilder SB = new StringBuilder();

        string pointer = Last ? "└─ " : "├─ ";
        SB.Append(Indent + pointer + Self + "\n");

        // Формируем новый префикс для детей
        string childIndent = Indent + (Last ? "   " : "│  ");

        var children = __Layer0.ToList();
        for(int i = 0; i < children.Count; i++){
            bool isLast = i == children.Count - 1;
            SB.Append(children[i].ToHierarchyString(childIndent, isLast));
        }

        return SB.ToString();
    }
    
    public override bool Equals(object? obj){
        if(obj is SceneNode<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T>{
    internal SceneNode<T>? __Node;

    public SceneNode<T> Node{
        get{
            __Node ??= new SceneNode<T>((T)this);
            return __Node;
        }
    }
}