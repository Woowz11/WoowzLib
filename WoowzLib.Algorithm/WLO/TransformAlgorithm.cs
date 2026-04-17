using WLO.Attribute;
using WLO.Rect;
using WLO.Vector;

namespace WLO;

// =========================================================
// TRANSFORM
// =========================================================

public class TransformAlgorithm
{
    public bool CallAnyway = false;

    private int __X;
    private int __Y;
    private uint __W = 100;
    private uint __H = 100;

    private bool _applying;
    private bool _external;

    public TransformAlgorithm() { }

    public TransformAlgorithm(int x, int y, uint w, uint h)
    {
        __X = x;
        __Y = y;
        __W = w;
        __H = h;
    }

    // ===================== PROPERTIES =====================

    public int X
    {
        get => __X;
        set => Set(Apply(new Rect2I(value, __Y, __W, __H)));
    }

    public int Y
    {
        get => __Y;
        set => Set(Apply(new Rect2I(__X, value, __W, __H)));
    }

    public uint W
    {
        get => __W;
        set => Set(Apply(new Rect2I(__X, __Y, value, __H)));
    }

    public uint H
    {
        get => __H;
        set => Set(Apply(new Rect2I(__X, __Y, __W, value)));
    }

    public Vector2I Position
    {
        get => new(__X, __Y);
        set => Set(Apply(new Rect2I(value.X, value.Y, __W, __H)));
    }

    public Vector2UI Size
    {
        get => new(__W, __H);
        set => Set(Apply(new Rect2I(__X, __Y, value.W, value.H)));
    }

    public Rect2I Rect
    {
        get => new(__X, __Y, __W, __H);
        set => Set(Apply(value));
    }

    // ===================== EVENTS =====================

    public event Func<TransformAlgorithm, Vector2I, Vector2I>? OnPosition;
    public event Func<TransformAlgorithm, Vector2UI, Vector2UI>? OnSize;
    public event Func<TransformAlgorithm, Rect2I, Rect2I>? OnRect;

    // ===================== APPLY PIPELINE =====================

    private Rect2I Apply(Rect2I input)
    {
        if (_applying)
            return input;

        _applying = true;

        try
        {
            var r = input;

            // POSITION STAGE
            if (OnPosition != null)
            {
                foreach (Func<TransformAlgorithm, Vector2I, Vector2I> fn in OnPosition.GetInvocationList())
                {
                    var p = fn(this, new Vector2I(r.X, r.Y));
                    r = new Rect2I(p.X, p.Y, r.W, r.H);
                }
            }

            // SIZE STAGE
            if (OnSize != null)
            {
                foreach (Func<TransformAlgorithm, Vector2UI, Vector2UI> fn in OnSize.GetInvocationList())
                {
                    var s = fn(this, new Vector2UI(r.W, r.H));
                    r = new Rect2I(r.X, r.Y, s.W, s.H);
                }
            }

            // FINAL RECT OVERRIDE STAGE
            if (OnRect != null)
            {
                foreach (Func<TransformAlgorithm, Rect2I, Rect2I> fn in OnRect.GetInvocationList())
                {
                    r = fn(this, r);
                }
            }

            return r;
        }
        finally
        {
            _applying = false;
        }
    }

    // ===================== COMMIT =====================

    private void Set(Rect2I r)
    {
        bool changed =
            __X != r.X || __Y != r.Y ||
            __W != r.W || __H != r.H;

        bool force = CallAnyway;

        if (!changed && !force)
            return;

        __X = r.X;
        __Y = r.Y;
        __W = r.W;
        __H = r.H;

        OnPosition?.Invoke(this, new Vector2I(__X, __Y));
        OnSize?.Invoke(this, new Vector2UI(__W, __H));
        OnRect?.Invoke(this, new Rect2I(__X, __Y, __W, __H));
    }
}

// =========================================================
// INTERFACE
// =========================================================

public interface ITransform
{
    void __UpdateTransform(object? Data = null);
}

// =========================================================
// WORLD TRANSFORM
// =========================================================

[WoowzLibHint(Information.WorkInProgress | Information.Testing)]
public class WorldTransformAlgorithm<T>
    where T : SceneObject<T>, ITransform
{
    public readonly T Self;

    public readonly TransformAlgorithm Local = new();
    public readonly TransformAlgorithm World = new();

    private bool _syncing;

    public Func<SceneNode<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnParentTransform;
    public Func<SceneNode<T>, WorldTransformAlgorithm<T>, Rect2I, Rect2I>? OnParentTransformReverse;

    public WorldTransformAlgorithm(T self)
    {
        Self = self;

        Local.OnRect += (_, __) =>
        {
            SyncWorld();
            return __;
        };

        Self.Node.OnSceneChangeAfter += (_, _, _) => SyncWorld();
        Self.Node.OnParentChangeAfter += (_, _, _) => SyncWorld();
    }

    // ===================== CALL ANYWAY =====================

    public bool CallAnyway
    {
        get => Local.CallAnyway;
        set
        {
            Local.CallAnyway = value;
            World.CallAnyway = value;
        }
    }

    // ===================== SYNC =====================

    private void SyncWorld()
    {
        if (_syncing)
            return;

        _syncing = true;

        var r = Local.Rect;
        r = LocalToWorld(r);

        World.Rect = r;

        UpdateChildren(Self.Node);

        _syncing = false;
    }

    public void Recalculate(bool localToWorld)
    {
        if (localToWorld)
            World.Rect = LocalToWorld(Local.Rect);
        else
            Local.Rect = WorldToLocal(World.Rect);

        UpdateChildren(Self.Node);
    }

    // ===================== TRANSFORMS =====================

    private Rect2I LocalToWorld(Rect2I rect)
    {
        rect = Self.Node.Parents().Aggregate(rect,
            (cur, p) => OnParentTransform?.Invoke(p, this, cur) ?? cur);

        return rect;
    }

    private Rect2I WorldToLocal(Rect2I rect)
    {
        rect = Self.Node.Parents().Reverse().Aggregate(rect,
            (cur, p) => OnParentTransformReverse?.Invoke(p, this, cur) ?? cur);

        return rect;
    }

    // ===================== CHILDREN =====================

    private void UpdateChildren(SceneNode<T> node)
    {
        foreach (var child in node.Level0)
        {
            child.Self.__UpdateTransform(true);
            UpdateChildren(child);
        }
    }
}