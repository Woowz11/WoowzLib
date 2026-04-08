using System.Text;
using WLO.Attribute;

namespace WLO;

public enum SceneCacheMode{
    None,
    SceneOnly,
    Full
}

[WoowzLibHint(Information.Testing)]
public class SceneAlgorithm<T> where T : SceneObject<T>{
    public SceneAlgorithm(object? Data = null, SceneCacheMode Mode = SceneCacheMode.SceneOnly){ ID = __ID++; this.Data = Data; CacheMode = Mode; }
    
    private static long __ID;
    public readonly long ID;

    internal readonly HashSet<SceneNode<T>> __Level0      = [];
    internal readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly object? Data;
    public readonly SceneCacheMode CacheMode;

    public bool UseSceneCache => CacheMode != SceneCacheMode.None;
    public bool UseNodeCache  => CacheMode == SceneCacheMode.Full;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    public IReadOnlyCollection<SceneNode<T>> Childrens => UseSceneCache ? __Descendants : __CalculateDescendants();

    public int Count => __Level0.Count;

    public bool Contains(SceneNode<T> Node) => __Level0.Contains(Node);

    public bool ContainsDescendant(SceneNode<T> node){
        if(!UseSceneCache){ Logger.Warn("gagag"); }
        return Childrens.Contains(node);
    }

    public SceneNode<T> Add(T Object) => Add(Object.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        if(Node.Scene == this){ return Node; }

        if(Node.Scene != null){ Node.Scene.Remove(Node); }

        Node.Parent = null;

        __Level0.Add(Node);
        Node.__SetScene(this);

        if(UseSceneCache){ __AddTree(Node); }

        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(!__Level0.Remove(Node)){ throw new InvalidOperationException("Node не принадлежит сцене"); }

        if(UseSceneCache){ __RemoveTree(Node); }

        Node.__SetScene(null);
    }

    public void Clear(){
        foreach(SceneNode<T> Node in __Level0.ToList()){
            Remove(Node);
        }
    }

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

    public override string ToString() => $"SceneAlg({Count}{(UseSceneCache ? $" ({__Descendants.Count})" : "")})";

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
    
    private static long __ID;
    public readonly long ID;

    private SceneAlgorithm<T>? __Scene;
    private SceneNode<T>?      __Parent;

    private readonly HashSet<SceneNode<T>> __Level0      = [];
    private readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly T Self;

    public bool UseNodeCache  => __Scene?.CacheMode == SceneCacheMode.Full;
    public bool UseSceneCache => __Scene?.CacheMode != SceneCacheMode.None;
    public bool InMemory      => __Scene == null;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    public IReadOnlyCollection<SceneNode<T>> Childrens => UseNodeCache ? __Descendants : __CalculateDescendants();

    public int Count => __Level0.Count;

    public SceneAlgorithm<T>? Scene{
        get => __Scene;
        set{
            if(__Scene == value){ return; }

            if(value != null){ Parent = null; }

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
        }
    }

    public SceneNode<T>? Parent{
        get => __Parent;
        set{
            if(__Parent == value){ return; }

            if(__Parent != null){
                __Parent.__Level0.Remove(this);
                __Parent.__PropagateRemove(this);

                if(__Parent.__Scene?.UseSceneCache == true){ __Parent.__Scene.__RemoveTree(this); }
            }

            __Parent = value;

            if(__Parent != null){
                __Parent.__Level0.Add(this);
                __Parent.__PropagateAdd(this);

                if(__Parent.__Scene?.UseSceneCache == true){ __Parent.__Scene.__AddTree(this); }

                __SetScene(__Parent.__Scene);
            }
        }
    }

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

    public SceneNode<T> Add(T Object) => Add(Object.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Parent = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){
        if(Node.Parent != this){ throw new InvalidOperationException("Node родитель не совпадает"); }

        Node.Parent = null;
    }

    public void Clear(){
        foreach(SceneNode<T> Node in __Level0.ToList()){
            Remove(Node);
        }
    }

    public string ToHierarchyString(string Indent = "", bool Last = true){

        StringBuilder SB = new StringBuilder();

        string Pointer = Last ? "└─ " : "├─ ";
        SB.AppendLine($"{Indent}{Pointer}{Self}");

        string ChildIndent = Indent + (Last ? "   " : "│  ");
        List<SceneNode<T>> List = __Level0.ToList();

        for(int i = 0; i < List.Count; i++){ SB.Append(List[i].ToHierarchyString(ChildIndent, i == List.Count - 1)); }

        return SB.ToString();
    }

    public override string ToString() => $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Count})";

    public override bool Equals(object? Object) => Object is SceneNode<T> Other && ID == Other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T>{
    private SceneNode<T>? __Node;

    public SceneNode<T> Node => __Node ??= new SceneNode<T>((T)this);
}