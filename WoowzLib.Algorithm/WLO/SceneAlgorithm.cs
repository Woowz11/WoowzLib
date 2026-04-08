using System.Text;
using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.Testing)]
public class SceneAlgorithm<T> where T : SceneObject<T>{
    public SceneAlgorithm(object? Data = null, bool CacheChildrens = true){ ID = __ID++; this.Data = Data; this.CacheChildrens = CacheChildrens; }
    public readonly long ID;
    private static  long __ID;
    
    public readonly  HashSet<SceneNode<T>> __Level0      = [];
    private readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly object? Data;
    public readonly bool    CacheChildrens;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;
    public IReadOnlyCollection<SceneNode<T>> Childrens => CacheChildrens ? __Descendants : CalculateDescendants();
    public int Count => __Level0.Count;

    public bool Contains(SceneNode<T> Node) => __Level0.Contains(Node);
    public bool ContainsDescendant(SceneNode<T> Node){
        if(!CacheChildrens){ Logger.Warn("hi"); }
        return Childrens.Contains(Node);
    }

    public SceneNode<T> Add(T Object) => Add(Object.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        if(Node.Scene == this){ return Node; }

        Node.Parent = null;
        __Level0.Add(Node);
        Node.__SetScene(this);

        if(CacheChildrens){ __AddTree(Node); }

        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(!__Level0.Remove(Node)){ throw new InvalidOperationException("Node не принадлежит сцене"); }

        if(CacheChildrens){ __RemoveTree(Node); }

        Node.__SetScene(null);
    }

    public void Clear(){ foreach(SceneNode<T> Node in __Level0.ToList()){ Remove(Node); } }

    internal void __AddTree(SceneNode<T> Node){
        if(__Descendants.Add(Node)){
            foreach(SceneNode<T> child in Node.Level0){ __AddTree(child); }
        }
    }

    internal void __RemoveTree(SceneNode<T> Node){
        if(__Descendants.Remove(Node)){
            foreach(SceneNode<T> child in Node.Level0){ __RemoveTree(child); }
        }
    }

    private HashSet<SceneNode<T>> CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];
        void Recurse(SceneNode<T> N){
            if(!Result.Add(N)){ return; }
            
            foreach(SceneNode<T> C in N.Level0){ Recurse(C); }
        }

        foreach(SceneNode<T> Node in __Level0){ Recurse(Node); }

        return Result;
    }

    public override string ToString() => $"SceneAlg({Count}{(CacheChildrens ? $"({__Descendants.Count})" : "")})";

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

[WoowzLibHint(Information.Testing)]
public class SceneNode<T> where T : SceneObject<T>{
    public SceneNode(T Self){ ID = __ID++; this.Self = Self; }
    public readonly long ID;
    private static long __ID;
    
    private          SceneAlgorithm<T>?    __Scene;
    private          SceneNode<T>?         __Parent;
    private readonly HashSet<SceneNode<T>> __Level0      = [];
    private readonly HashSet<SceneNode<T>> __Descendants = [];
    
    public readonly T Self;

    public bool CacheChildrens => Scene?.CacheChildrens ?? true;
    public bool InMemory => Scene == null;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;
    public IReadOnlyCollection<SceneNode<T>> Childrens => CacheChildrens ? __Descendants : __CalculateDescendants();
    
    public int Count => __Level0.Count;
    
    public bool IsDescendantOf(SceneNode<T> Node){
        SceneNode<T>? current = this;

        while (current != null)
        {
            if (current == Node)
                return true;

            current = current.Parent;
        }

        return false;
    }
    
    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            if(__Scene == value){ return; }

            if(value != null){ Parent = null; }

            __Scene?.__RemoveTree(this);
            __Scene?.__Level0.Remove(this);

            __Scene = value;

            if(__Scene != null){
                __Scene.__Level0.Add(this);
                if(__Scene.CacheChildrens){ __Scene.__AddTree(this); }
            }

            foreach(SceneNode<T> child in __Level0){ child.__SetScene(__Scene); }
        }
    }

    public SceneNode<T>? Parent
    {
        get => __Parent;
        set
        {
            if (__Parent == value) return;

            // REMOVE из старого родителя
            if (__Parent != null)
            {
                __Parent.__Level0.Remove(this);
                __Parent.__PropagateRemove(this);

                if (__Parent.Scene != null && __Parent.CacheChildrens)
                    __Parent.Scene.__RemoveTree(this);
            }

            __Parent = value;

            if (__Parent != null)
            {
                __Parent.__Level0.Add(this);
                __Parent.__PropagateAdd(this);

                if (__Parent.Scene != null && __Parent.CacheChildrens)
                    __Parent.Scene.__AddTree(this);

                __SetScene(__Parent.Scene);
            }
        }
    }

    internal void __SetScene(SceneAlgorithm<T>? Scene){
        __Scene = Scene;
        foreach(SceneNode<T> Node in __Level0){ Node.__SetScene(Scene); }
    }

    public SceneNode<T> Add(T Object) => Add(Object.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Parent = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Parent != this){ throw new InvalidOperationException("Node родитель не совпадает"); }
        Node.Parent = null;
    }

    public void Clear(){ foreach (SceneNode<T> Node in __Level0.ToList()){ Remove(Node); } }

    private HashSet<SceneNode<T>> __CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];
        void Recurse(SceneNode<T> N){
            if(!Result.Add(N)){ return; }
            
            foreach(SceneNode<T> C in N.Level0){ Recurse(C); }
        }

        foreach(SceneNode<T> Node in __Level0){ Recurse(Node); }

        return Result;
    }

    internal void __AddTree(SceneNode<T> Node){
        if(__Descendants.Add(Node)){
            foreach (SceneNode<T> child in Node.Level0)
                __AddTree(child);
        }
    }

    internal void __RemoveTree(SceneNode<T> Node){
        if(__Descendants.Remove(Node)){
            foreach (SceneNode<T> child in Node.Level0)
                __RemoveTree(child);
        }
    }
    
    private void __PropagateAdd(SceneNode<T> node)
    {
        SceneNode<T>? current = this;
        while (current != null)
        {
            if (current.CacheChildrens)
                current.__AddTree(node);

            current = current.Parent;
        }
    }

    private void __PropagateRemove(SceneNode<T> node)
    {
        SceneNode<T>? current = this;
        while (current != null)
        {
            if (current.CacheChildrens)
                current.__RemoveTree(node);

            current = current.Parent;
        }
    }
    
    public string ToHierarchyString(string Indent = "", bool Last = true){
        StringBuilder SB = new StringBuilder();
        string Pointer = Last ? "└─ " : "├─ ";
        SB.AppendLine($"{Indent}{Pointer}{Self}");
        string ChildIndent = Indent + (Last ? "   " : "│  ");
        List<SceneNode<T>> ChildrenList = __Level0.ToList();
        for(int i = 0; i < ChildrenList.Count; i++){ SB.Append(ChildrenList[i].ToHierarchyString(ChildIndent, i == ChildrenList.Count - 1)); }
        return SB.ToString();
    }

    public override string ToString() => $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Level0.Count})";
    public override bool Equals(object? Object) => Object is SceneNode<T> Other && ID == Other.ID;
    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T> {
    private SceneNode<T>? __Node;
    public SceneNode<T> Node => __Node ??= new SceneNode<T>((T)this);
}