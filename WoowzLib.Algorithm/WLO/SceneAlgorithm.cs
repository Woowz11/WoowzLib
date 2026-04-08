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

    private static long __ID;
    public readonly long ID;

    internal readonly HashSet<SceneNode<T>> __Level0      = [];
    internal readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly object? Data;
    public readonly SceneCacheMode CacheMode;

    public SceneAlgorithm(object? data = null, SceneCacheMode mode = SceneCacheMode.SceneOnly){
        ID = __ID++;
        Data = data;
        CacheMode = mode;
    }

    public bool UseSceneCache => CacheMode != SceneCacheMode.None;
    public bool UseNodeCache  => CacheMode == SceneCacheMode.Full;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    public IReadOnlyCollection<SceneNode<T>> Childrens =>
        UseSceneCache ? __Descendants : __CalculateDescendants();

    public int Count => __Level0.Count;

    public bool Contains(SceneNode<T> node){
        return __Level0.Contains(node);
    }

    public bool ContainsDescendant(SceneNode<T> node){
        return Childrens.Contains(node);
    }

    public SceneNode<T> Add(T obj){
        return Add(obj.Node);
    }

    public SceneNode<T> Add(SceneNode<T> node){

        if(node.Scene == this){
            return node;
        }

        if(node.Scene != null){
            node.Scene.Remove(node);
        }

        node.Parent = null;

        __Level0.Add(node);
        node.__SetScene(this);

        if(UseSceneCache){
            __AddTree(node);
        }

        return node;
    }

    public void Remove(SceneNode<T> node){

        if(!__Level0.Remove(node)){
            throw new InvalidOperationException("Node не принадлежит сцене");
        }

        if(UseSceneCache){
            __RemoveTree(node);
        }

        node.__SetScene(null);
    }

    public void Clear(){
        foreach(SceneNode<T> n in __Level0.ToList()){
            Remove(n);
        }
    }

    internal void __AddTree(SceneNode<T> node){

        if(!__Descendants.Add(node)){
            return;
        }

        foreach(SceneNode<T> c in node.Level0){
            __AddTree(c);
        }
    }

    internal void __RemoveTree(SceneNode<T> node){

        if(!__Descendants.Remove(node)){
            return;
        }

        foreach(SceneNode<T> c in node.Level0){
            __RemoveTree(c);
        }
    }

    private HashSet<SceneNode<T>> __CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];

        void Recurse(SceneNode<T> n){

            if(!Result.Add(n)){ return; }

            foreach(SceneNode<T> c in n.Level0){ Recurse(c); }
        }

        foreach(SceneNode<T> n in __Level0){ Recurse(n); }

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

    public override bool Equals(object? obj){
        return obj is SceneAlgorithm<T> other && ID == other.ID;
    }

    public override int GetHashCode(){
        return ID.GetHashCode();
    }
}

[WoowzLibHint(Information.Testing)]
public class SceneNode<T> where T : SceneObject<T>{

    private static long __ID;
    public readonly long ID;

    private SceneAlgorithm<T>? SceneRef;
    private SceneNode<T>?      ParentRef;

    private readonly HashSet<SceneNode<T>> Level0Set      = [];
    private readonly HashSet<SceneNode<T>> DescendantsSet = [];

    public readonly T Self;

    public SceneNode(T self){
        ID = __ID++;
        Self = self;
    }

    public bool UseNodeCache  => SceneRef?.CacheMode == SceneCacheMode.Full;
    public bool UseSceneCache => SceneRef?.CacheMode != SceneCacheMode.None;
    public bool InMemory      => SceneRef == null;

    public IReadOnlyCollection<SceneNode<T>> Level0 => Level0Set;

    public IReadOnlyCollection<SceneNode<T>> Childrens =>
        UseNodeCache ? DescendantsSet : __CalculateDescendants();

    public int Count => Level0Set.Count;

    public SceneAlgorithm<T>? Scene{
        get => SceneRef;
        set{

            if(SceneRef == value){
                return;
            }

            if(value != null){
                Parent = null;
            }

            if(SceneRef != null){

                SceneRef.__Level0.Remove(this);

                if(SceneRef.UseSceneCache){
                    SceneRef.__RemoveTree(this);
                }
            }

            SceneRef = value;

            if(SceneRef != null){

                SceneRef.__Level0.Add(this);

                if(SceneRef.UseSceneCache){
                    SceneRef.__AddTree(this);
                }
            }

            foreach(SceneNode<T> c in Level0Set){
                c.__SetScene(SceneRef);
            }
        }
    }

    public SceneNode<T>? Parent{
        get => ParentRef;
        set{

            if(ParentRef == value){
                return;
            }

            if(ParentRef != null){

                ParentRef.Level0Set.Remove(this);
                ParentRef.__PropagateRemove(this);

                if(ParentRef.SceneRef?.UseSceneCache == true){
                    ParentRef.SceneRef.__RemoveTree(this);
                }
            }

            ParentRef = value;

            if(ParentRef != null){

                ParentRef.Level0Set.Add(this);
                ParentRef.__PropagateAdd(this);

                if(ParentRef.SceneRef?.UseSceneCache == true){
                    ParentRef.SceneRef.__AddTree(this);
                }

                __SetScene(ParentRef.SceneRef);
            }
        }
    }

    internal void __SetScene(SceneAlgorithm<T>? scene){

        SceneRef = scene;

        foreach(SceneNode<T> c in Level0Set){
            c.__SetScene(scene);
        }
    }

    internal void __AddTree(SceneNode<T> node){

        if(!DescendantsSet.Add(node)){
            return;
        }

        foreach(SceneNode<T> c in node.Level0){
            __AddTree(c);
        }
    }

    internal void __RemoveTree(SceneNode<T> node){

        if(!DescendantsSet.Remove(node)){
            return;
        }

        foreach(SceneNode<T> c in node.Level0){
            __RemoveTree(c);
        }
    }

    private void __PropagateAdd(SceneNode<T> node){

        SceneNode<T>? current = this;

        while(current != null){

            if(current.UseNodeCache){
                current.__AddTree(node);
            }

            current = current.Parent;
        }
    }

    private void __PropagateRemove(SceneNode<T> node){

        SceneNode<T>? current = this;

        while(current != null){

            if(current.UseNodeCache){
                current.__RemoveTree(node);
            }

            current = current.Parent;
        }
    }

    private HashSet<SceneNode<T>> __CalculateDescendants(){
        HashSet<SceneNode<T>> Result = [];

        void Recurse(SceneNode<T> N){
            if(!Result.Add(N)){ return; }

            foreach(SceneNode<T> C in N.Level0){ Recurse(C); }
        }

        foreach(SceneNode<T> C in Level0Set){ Recurse(C); }

        return Result;
    }

    public SceneNode<T> Add(T Object) => Add(Object.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        Node.Parent = this;
        return Node;
    }

    public void Remove(SceneNode<T> Node){

        if(Node.Parent != this){
            throw new InvalidOperationException("Node родитель не совпадает");
        }

        Node.Parent = null;
    }

    public void Clear(){
        foreach(SceneNode<T> Node in Level0Set.ToList()){
            Remove(Node);
        }
    }

    public string ToHierarchyString(string Indent = "", bool Last = true){

        StringBuilder SB = new StringBuilder();

        string pointer = Last ? "└─ " : "├─ ";
        SB.AppendLine($"{Indent}{pointer}{Self}");

        string childIndent = Indent + (Last ? "   " : "│  ");
        List<SceneNode<T>> list = Level0Set.ToList();

        for(int i = 0; i < list.Count; i++){
            SB.Append(list[i].ToHierarchyString(childIndent, i == list.Count - 1));
        }

        return SB.ToString();
    }

    public override string ToString() => $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Count})";

    public override bool Equals(object? obj) => obj is SceneNode<T> other && ID == other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T>{
    private SceneNode<T>? __Node;

    public SceneNode<T> Node => __Node ??= new SceneNode<T>((T)this);
}