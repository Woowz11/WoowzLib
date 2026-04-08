using System.Text;
using WLO.Attribute;

namespace WLO;

public enum SceneCacheMode {
    None,
    SceneOnly,
    Full
}

[WoowzLibHint(Information.Testing)]
public class SceneAlgorithm<T> where T : SceneObject<T>
{
    private static long __ID;
    public readonly long ID;

    internal readonly HashSet<SceneNode<T>> __Level0 = [];
    internal readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly object? Data;
    public readonly SceneCacheMode CacheMode;

    public SceneAlgorithm(object? data = null, SceneCacheMode mode = SceneCacheMode.SceneOnly)
    {
        ID = __ID++;
        Data = data;
        CacheMode = mode;
    }

    public bool UseSceneCache => CacheMode != SceneCacheMode.None;
    public bool UseNodeCache => CacheMode == SceneCacheMode.Full;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;
    public IReadOnlyCollection<SceneNode<T>> Childrens =>
        UseSceneCache ? __Descendants : CalculateDescendants();

    public int Count => __Level0.Count;

    public bool Contains(SceneNode<T> node) => __Level0.Contains(node);
    public bool ContainsDescendant(SceneNode<T> node) => Childrens.Contains(node);

    public SceneNode<T> Add(T obj) => Add(obj.Node);

    public SceneNode<T> Add(SceneNode<T> Node){
        if(Node.Scene == this) return Node;

        // 🔥 ВАЖНО: удалить из старой сцены
        if (Node.Scene != null)
        {
            Node.Scene.Remove(Node);
        }

        Node.Parent = null;

        __Level0.Add(Node);
        Node.__SetScene(this);

        if(UseSceneCache)
            __AddTree(Node);

        return Node;
    }

    public void Remove(SceneNode<T> node)
    {
        if (!__Level0.Remove(node))
            throw new InvalidOperationException("Node не принадлежит сцене");

        if (UseSceneCache)
            __RemoveTree(node);

        node.__SetScene(null);
    }

    public void Clear()
    {
        foreach (var n in __Level0.ToList())
            Remove(n);
    }

    internal void __AddTree(SceneNode<T> node)
    {
        if (__Descendants.Add(node))
        {
            foreach (var c in node.Level0)
                __AddTree(c);
        }
    }

    internal void __RemoveTree(SceneNode<T> node)
    {
        if (__Descendants.Remove(node))
        {
            foreach (var c in node.Level0)
                __RemoveTree(c);
        }
    }

    private HashSet<SceneNode<T>> CalculateDescendants()
    {
        var result = new HashSet<SceneNode<T>>();

        void Recurse(SceneNode<T> n)
        {
            if (!result.Add(n)) return;
            foreach (var c in n.Level0)
                Recurse(c);
        }

        foreach (var n in __Level0)
            Recurse(n);

        return result;
    }

    public override string ToString() =>
        $"SceneAlg({Count}{(UseSceneCache ? $"({__Descendants.Count})" : "")})";

    public string ToHierarchyString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ToString());

        var roots = __Level0.ToList();
        for (int i = 0; i < roots.Count; i++)
            sb.Append(roots[i].ToHierarchyString("", i == roots.Count - 1));

        return sb.ToString();
    }

    public override bool Equals(object? obj) =>
        obj is SceneAlgorithm<T> other && ID == other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public class SceneNode<T> where T : SceneObject<T>
{
    private static long __ID;
    public readonly long ID;

    private SceneAlgorithm<T>? __Scene;
    private SceneNode<T>? __Parent;

    private readonly HashSet<SceneNode<T>> __Level0 = [];
    private readonly HashSet<SceneNode<T>> __Descendants = [];

    public readonly T Self;

    public SceneNode(T self)
    {
        ID = __ID++;
        Self = self;
    }

    public bool UseNodeCache => __Scene?.CacheMode == SceneCacheMode.Full;
    public bool UseSceneCache => __Scene?.CacheMode != SceneCacheMode.None;
    public bool InMemory => __Scene == null;

    public IReadOnlyCollection<SceneNode<T>> Level0 => __Level0;

    public IReadOnlyCollection<SceneNode<T>> Childrens =>
        UseNodeCache ? __Descendants : __CalculateDescendants();

    public int Count => __Level0.Count;

    public SceneAlgorithm<T>? Scene
    {
        get => __Scene;
        set
        {
            if (__Scene == value) return;

            if (value != null)
                Parent = null;

            if (__Scene != null)
            {
                __Scene.__Level0.Remove(this);
                if (__Scene.UseSceneCache)
                    __Scene.__RemoveTree(this);
            }

            __Scene = value;

            if (__Scene != null)
            {
                __Scene.__Level0.Add(this);
                if (__Scene.UseSceneCache)
                    __Scene.__AddTree(this);
            }

            foreach (var c in __Level0)
                c.__SetScene(__Scene);
        }
    }

    public SceneNode<T>? Parent
    {
        get => __Parent;
        set
        {
            if (__Parent == value) return;

            if (__Parent != null)
            {
                __Parent.__Level0.Remove(this);
                __Parent.__PropagateRemove(this);

                if (__Parent.__Scene?.UseSceneCache == true)
                    __Parent.__Scene.__RemoveTree(this);
            }

            __Parent = value;

            if (__Parent != null)
            {
                __Parent.__Level0.Add(this);
                __Parent.__PropagateAdd(this);

                if (__Parent.__Scene?.UseSceneCache == true)
                    __Parent.__Scene.__AddTree(this);

                __SetScene(__Parent.__Scene);
            }
        }
    }

    internal void __SetScene(SceneAlgorithm<T>? scene)
    {
        __Scene = scene;
        foreach (var c in __Level0)
            c.__SetScene(scene);
    }

    internal void __AddTree(SceneNode<T> node)
    {
        if (__Descendants.Add(node))
        {
            foreach (var c in node.Level0)
                __AddTree(c);
        }
    }

    internal void __RemoveTree(SceneNode<T> node)
    {
        if (__Descendants.Remove(node))
        {
            foreach (var c in node.Level0)
                __RemoveTree(c);
        }
    }

    private void __PropagateAdd(SceneNode<T> node)
    {
        var current = this;
        while (current != null)
        {
            if (current.UseNodeCache)
                current.__AddTree(node);

            current = current.Parent;
        }
    }

    private void __PropagateRemove(SceneNode<T> node)
    {
        var current = this;
        while (current != null)
        {
            if (current.UseNodeCache)
                current.__RemoveTree(node);

            current = current.Parent;
        }
    }

    private HashSet<SceneNode<T>> __CalculateDescendants()
    {
        var result = new HashSet<SceneNode<T>>();

        void Recurse(SceneNode<T> n)
        {
            if (!result.Add(n)) return;
            foreach (var c in n.Level0)
                Recurse(c);
        }

        foreach (var c in __Level0)
            Recurse(c);

        return result;
    }

    public SceneNode<T> Add(T obj) => Add(obj.Node);

    public SceneNode<T> Add(SceneNode<T> node)
    {
        node.Parent = this;
        return node;
    }

    public void Remove(SceneNode<T> node)
    {
        if (node.Parent != this)
            throw new InvalidOperationException("Node родитель не совпадает");

        node.Parent = null;
    }

    public void Clear()
    {
        foreach (var c in __Level0.ToList())
            Remove(c);
    }

    public string ToHierarchyString(string indent = "", bool last = true)
    {
        var sb = new StringBuilder();
        var pointer = last ? "└─ " : "├─ ";

        sb.AppendLine($"{indent}{pointer}{Self}");

        var childIndent = indent + (last ? "   " : "│  ");
        var list = __Level0.ToList();

        for (int i = 0; i < list.Count; i++)
            sb.Append(list[i].ToHierarchyString(childIndent, i == list.Count - 1));

        return sb.ToString();
    }

    public override string ToString() =>
        $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Count})";

    public override bool Equals(object? obj) =>
        obj is SceneNode<T> other && ID == other.ID;

    public override int GetHashCode() => ID.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T>
{
    private SceneNode<T>? __Node;
    public SceneNode<T> Node => __Node ??= new SceneNode<T>((T)this);
}