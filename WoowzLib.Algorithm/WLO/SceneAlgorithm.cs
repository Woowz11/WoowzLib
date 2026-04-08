using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.WorkInProgress)]
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
    
    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Scene = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Scene != this){ throw new Exception("Сцена Node не равна указанной сцене!"); }
        Node.Scene = null;
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
        foreach(var VARIABLE in Node.__Layer0){
            __AddTree(VARIABLE);
        }
    }
    
    internal void __RemoveTree(SceneNode<T> Node){
        if(!__Childrens.Remove(Node)){ return; }
        foreach(var VARIABLE in Node.__Layer0){
            __RemoveTree(VARIABLE);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SceneAlg.(" + Count + (CacheChildrens ? "(" + __Childrens.Count + ")" : "") + ")";
    public string ToShortString() => "";
    
    public override bool Equals(object? obj){
        if(obj is SceneAlgorithm<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode(){
        return ID.GetHashCode();
    }
}

[WoowzLibHint(Information.WorkInProgress)]
public class SceneNode<T> where T : SceneObject<T>{
    public SceneNode(T Self, bool CacheChildrens = true){
        ID = TotalID++;
        this.Self = Self;
    }
    private readonly long ID;
    private static   long TotalID;

    public readonly T Self;

    public bool CacheChildrens => Scene is{ CacheChildrens: true };
    
    // ----------------------------------------------------------------------

    private SceneAlgorithm<T>? __Scene;
    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            if(__Scene == value){ return; }

            Parent = null;
            
            if(__Scene != null){
                __Scene.__Layer0.Remove(this);
                if(CacheChildrens){ __Scene.__RemoveTree(this); }
            }
            
            __Scene = value;

            if(__Scene != null){
                __Scene.__Layer0.Add(this);
                if(CacheChildrens){ __Scene.__AddTree(this); }
            }
        }
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

                Scene = __Parent.Scene;
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
    
    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Parent = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Parent != this){ throw new Exception("Родитель Node не равен указанному родителю!"); }
        Node.Parent = null;
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

    public override bool Equals(object? obj){
        if(obj is SceneNode<T> other){ return ID == other.ID; }
        return false;
    }

    public override int GetHashCode() => ID.GetHashCode();
}

public abstract class SceneObject<T> where T : SceneObject<T>{
    internal SceneNode<T>? __Node;

    public SceneNode<T> Node{
        get{
            __Node ??= new SceneNode<T>((T)this);
            return __Node;
        }
    }
}