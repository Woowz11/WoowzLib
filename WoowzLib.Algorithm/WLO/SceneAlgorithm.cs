using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.WorkInProgress)]
public class SceneAlgorithm<T> where T : SceneObject<T>{
    // Переменная, указывающая на сколько возможен по глубине иерархия


    public SceneAlgorithm(object? Data = null){ ID = TotalID++; this.Data = Data; }
    private readonly long ID;
    private static   long TotalID;

    /// <summary>
    /// Дополнительная информация, возможно привязанный компонент
    /// </summary>
    public object? Data{ get; private set; }

    // ----------------------------------------------------------------------

    public readonly List<SceneNode<T>>    Layer0    = [];
    public readonly HashSet<SceneNode<T>> Childrens = [];

    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        __Add(Node);
        return Node;
    }
    
    // ----------------------------------------------------------------------

    internal void __Add(SceneNode<T> Node){
        if(Node.Scene == this){ return; }
        
        if(Node.Parent != null){ Node.Parent.__Remove(Node); }

        if(Node.Scene != null){
            Node.Scene.__Remove(Node);
        }

        Node.Scene = this;
        
        Layer0.Add(Node);
        
        __RegisterTree(Node);
    }

    internal void __Remove(SceneNode<T> Node){
        if(Node.Scene != this){ throw new Exception("Node не привязан к этой сцене!"); }

        Layer0.Remove(Node);
        
        __UnregisterTree(Node);

        Node.Scene = null;
    }

    internal void __RegisterTree(SceneNode<T> Node){
        if(!Childrens.Add(Node)){ return; }

        foreach(SceneNode<T> VARIABLE in Node.Childrens){
            Childrens.Add(VARIABLE);
        }
    }
    
    internal void __UnregisterTree(SceneNode<T> Node){
        if(!Childrens.Remove(Node)){ return; }

        foreach(SceneNode<T> VARIABLE in Node.Childrens){
            Childrens.Remove(VARIABLE);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "SceneAlg.(" + Layer0.Count + " (" + Count + "))";
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
    public SceneNode(T Self){
        ID = TotalID++;
        this.Self = Self;
    }
    private readonly long ID;
    private static   long TotalID;

    public readonly T Self;

    // ----------------------------------------------------------------------

    public SceneAlgorithm<T>? Scene;
    public SceneNode<T>?      Parent;
    
    public readonly List<SceneNode<T>>    Layer0    = [];
    public readonly HashSet<SceneNode<T>> Childrens = [];
    
    public SceneNode<T> Add(T Object) => Add(Object.Node);
    
    public SceneNode<T> Add(SceneNode<T> Node){
        __Add(Node);
        return Node;
    }
    
    // ----------------------------------------------------------------------

    internal void __Add(SceneNode<T> Node){
        
    }

    internal void __Remove(SceneNode<T> Node){
        
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