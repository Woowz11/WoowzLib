using System.Numerics;
using WLO.Attribute;
using WLO.Color;
using WLO.Rect;
using WLO.Vector;

namespace WLO.WLElement;

public abstract class WLElement : SceneObject<WLElement>, ITransform{
    protected WLElement(){ Name = DefaultName(); Transform = new WLElementTransform(this); }

    /// <summary>
    /// Название элемента
    /// </summary>
    public string Name;

    /// <summary>
    /// Мировая позиция и размер элемента
    /// </summary>
    public readonly WLElementTransform Transform;

    // ----------------------------------------------------------------------

    /// <summary>
    /// Стартовое название для элемента
    /// </summary>
    public virtual string DefaultName() => "Element";

    /// <summary>
    /// Рендер элемента
    /// </summary>
    /// <param name="Window">Окно</param>
    /// <param name="HDC">Куда рисовать?</param>
    public abstract void Render(WLWindow Window, IntPtr HDC);
    
    // ----------------------------------------------------------------------

    internal void __Render(WLWindow Window, IntPtr HDC){
        try{
            Render(Window, HDC);   
        }catch(Exception e){
            Logger.Error("Произошла ошибка при рендере элемента [" + this + "] у WL окна [" + Window + "]!\nHDC: " + HDC, e);
            WL.System.Draw.Fill(HDC, Transform.World.Rect, new BrushFill(Color4B.Magenta));
        }
    }

    //internal void __UpdateTransform(object? Data = null) => Transform.Recalculate((bool)Data!);
    
    // ----------------------------------------------------------------------

    public override string ToString() => GetType().Name + "(" + ToShortString() + ")";
    public void __UpdateTransform(object? Data = null){
        
    }

    public string ToShortString() => "\"" + Name + "\", "/* + Transform.Local.ToVeryShortString()*/;
}

[WoowzLibHint(Information.WorkInProgress)]
public class WLElementTransform : WorldTransformAlgorithm<WLElement>
{
    public WLElementTransform(WLElement self) : base(self) { }

    public Vector2I Anchor      = new(0, 0);
    public Vector2I PixelOffset = new(0, 0);
    public Vector2D Offset      = new(0, 0);
    public Vector2D Scale       = new(1, 1);

    public Vector4I Margin;
    public Vector4I Padding;

    public Vector2UI MinSize;
    public Vector2UI MaxSize = new(uint.MaxValue, uint.MaxValue);

    public void Recalculate(bool localToWorld)
    {
        if (!localToWorld) return;

        var parent = Self.Node.Parent?.Self.Transform.World.Rect
                     ?? new Rect2I(0, 0, 0, 0);

        var local = Local.Rect;

        // ---------------- size pipeline ----------------
        uint w = Clamp(local.W, MinSize.W, MaxSize.W);
        uint h = Clamp(local.H, MinSize.H, MaxSize.H);

        w += (uint)(Padding.X + Padding.Z + Margin.X + Margin.Z);
        h += (uint)(Padding.Y + Padding.W + Margin.Y + Margin.W);

        w = (uint)(w * Scale.X);
        h = (uint)(h * Scale.Y);

        // ---------------- position pipeline ----------------

        int cx = parent.X + (int)parent.W / 2;
        int cy = parent.Y + (int)parent.H / 2;

        int ox = (int)(parent.W * Offset.X);
        int oy = (int)(parent.H * Offset.Y);

        int x = cx + ox + PixelOffset.X - (int)(w / 2);
        int y = cy + oy + PixelOffset.Y - (int)(h / 2);

        World.Rect = new Rect2I(x, y, w, h);

        base.Recalculate(true);
    }

    private static uint Clamp(uint v, uint min, uint max)
        => v < min ? min : v > max ? max : v;
}