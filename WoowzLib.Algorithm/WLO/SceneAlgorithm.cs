using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLO.Attribute;

namespace WLO;

[WoowzLibHint(Information.Testing)]
public class SceneAlgorithm<T> where T : SceneObject<T>
{
    public readonly HashSet<SceneNode<T>> _roots = new();
    private readonly HashSet<SceneNode<T>> _cachedDescendants = new();
    private static long _globalId;

    public long Id { get; }
    public object? Data { get; }
    public bool CacheChildrens { get; }

    public SceneAlgorithm(object? data = null, bool cacheChildrens = true)
    {
        Id = _globalId++;
        Data = data;
        CacheChildrens = cacheChildrens;
    }

    public IReadOnlyCollection<SceneNode<T>> RootNodes => _roots;
    public IReadOnlyCollection<SceneNode<T>> Childrens => CacheChildrens ? _cachedDescendants : CalculateDescendants();
    public int Count => _roots.Count;

    public bool Contains(SceneNode<T> node) => _roots.Contains(node);
    public bool ContainsDescendant(SceneNode<T> node) => Childrens.Contains(node);

    public SceneNode<T> Add(T obj) => Add(obj.Node);

    public SceneNode<T> Add(SceneNode<T> node)
    {
        if (node.Scene == this) return node;

        node.Parent = null; // разрываем родителя
        _roots.Add(node);
        node.SetScene(this);

        if (CacheChildrens)
            AddTree(node);

        return node;
    }

    public void Remove(SceneNode<T> node)
    {
        if (!_roots.Remove(node))
            throw new InvalidOperationException("Node не принадлежит сцене");

        if (CacheChildrens)
            RemoveTree(node);

        node.SetScene(null); // только сцена, родитель остаётся
    }

    public void Clear()
    {
        foreach (var node in _roots.ToList())
            Remove(node);
    }

    internal void AddTree(SceneNode<T> node)
    {
        if (_cachedDescendants.Add(node))
        {
            foreach (var child in node.Children)
                AddTree(child);
        }
    }

    internal void RemoveTree(SceneNode<T> node)
    {
        if (_cachedDescendants.Remove(node))
        {
            foreach (var child in node.Children)
                RemoveTree(child);
        }
    }

    private HashSet<SceneNode<T>> CalculateDescendants()
    {
        var result = new HashSet<SceneNode<T>>();
        void Recurse(SceneNode<T> n)
        {
            if (!result.Add(n)) return;
            foreach (var c in n.Children)
                Recurse(c);
        }

        foreach (var root in _roots)
            Recurse(root);

        return result;
    }

    public override string ToString() => $"SceneAlg({Count}{(CacheChildrens ? $"({_cachedDescendants.Count})" : "")})";

    public string ToHierarchyString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(ToString());
        var roots = _roots.ToList();
        for (int i = 0; i < roots.Count; i++)
            sb.Append(roots[i].ToHierarchyString("", i == roots.Count - 1));
        return sb.ToString();
    }

    public override bool Equals(object? obj) => obj is SceneAlgorithm<T> other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public class SceneNode<T> where T : SceneObject<T>
{
    private static long _globalId;
    private SceneAlgorithm<T>? _scene;
    private SceneNode<T>? _parent;
    private readonly HashSet<SceneNode<T>> _children = new();

    public long Id { get; }
    public readonly T Self;

    public SceneNode(T self)
    {
        Id = _globalId++;
        Self = self;
    }

    public bool CacheChildrens => Scene?.CacheChildrens ?? true;
    public bool InMemory => Scene == null;

    public IReadOnlyCollection<SceneNode<T>> Children => _children;

    public int Count => _children.Count;
    
    public SceneAlgorithm<T>? Scene
    {
        get => _scene;
        set
        {
            if (_scene == value) return;

            if (value != null)
                Parent = null; // разрываем родителя при присвоении сцены

            _scene?.RemoveTree(this);
            _scene?._roots.Remove(this);

            _scene = value;

            if (_scene != null)
            {
                _scene._roots.Add(this);
                if (_scene.CacheChildrens)
                    _scene.AddTree(this);
            }

            foreach (var child in _children)
                child.SetScene(_scene);
        }
    }

    public SceneNode<T>? Parent
    {
        get => _parent;
        set
        {
            if (_parent == value) return;

            _parent?._children.Remove(this);
            if (_parent?.Scene != null && _parent.CacheChildrens)
                _parent.Scene.RemoveTree(this);

            _parent = value;

            if (_parent != null)
            {
                _parent._children.Add(this);
                if (_parent.Scene != null && _parent.CacheChildrens)
                    _parent.Scene.AddTree(this);

                SetScene(_parent.Scene);
            }
        }
    }

    internal void SetScene(SceneAlgorithm<T>? scene)
    {
        _scene = scene;
        foreach (var child in _children)
            child.SetScene(scene);
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
        foreach (var child in _children.ToList())
            Remove(child);
    }

    private HashSet<SceneNode<T>> CalculateDescendants()
    {
        var result = new HashSet<SceneNode<T>>();
        void Recurse(SceneNode<T> n)
        {
            if (!result.Add(n)) return;
            foreach (var c in n.Children)
                Recurse(c);
        }

        foreach (var child in _children)
            Recurse(child);

        return result;
    }

    public string ToHierarchyString(string indent = "", bool last = true)
    {
        var sb = new StringBuilder();
        var pointer = last ? "└─ " : "├─ ";
        sb.AppendLine($"{indent}{pointer}{Self}");
        var childIndent = indent + (last ? "   " : "│  ");
        var childrenList = _children.ToList();
        for (int i = 0; i < childrenList.Count; i++)
            sb.Append(childrenList[i].ToHierarchyString(childIndent, i == childrenList.Count - 1));
        return sb.ToString();
    }

    public override string ToString() => $"SN({Self}, {(Parent != null ? Parent.Self.ToString() : "null")}, {Children.Count})";
    public override bool Equals(object? obj) => obj is SceneNode<T> other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}

[WoowzLibHint(Information.Testing)]
public abstract class SceneObject<T> where T : SceneObject<T>
{
    private SceneNode<T>? _node;
    public SceneNode<T> Node => _node ??= new SceneNode<T>((T)this);
}