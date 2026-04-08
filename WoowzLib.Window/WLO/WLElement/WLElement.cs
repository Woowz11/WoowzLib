using System.Numerics;
using WLO.Color;
using WLO.Rect;
using WLO.Vector;

namespace WLO.WLElement;

public abstract class WLElement : SceneObject<WLElement>{
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
    
    // ----------------------------------------------------------------------

    public override string ToString() => GetType().Name + "(" + ToShortString() + ")";

    public string ToShortString() => "\"" + Name + "\", " + Transform.Local.ToVeryShortString();
}

public class WLElementTransform : WorldTransformAlgorithm<WLElement>{
    public WLElementTransform(WLElement Self) : base(Self){
        OnSceneTransform = (Scene, _, Rect) => {
            WLElementTransform Transform = _.Self.Transform;
            WLWindow Window = (WLWindow)Scene.Data!;
            
            return Rect;
        };
        
        OnParentTransform = (Parent, _, Rect) => {
            WLElementTransform T = Parent.Self.Transform;
            
            return ApplyTransform(Local.Rect, Rect, Anchor, T.Anchor, PixelOffset, T.PixelOffset, Offset, T.Offset, Scale, T.Scale, Padding, T.Padding, Margin, T.Margin, MinSize, T.MinSize, MaxSize, T.MaxSize);
        };
        
        // ----------------------------------------------------------------------
        
        OnParentTransformReverse = (Parent, _, Rect) => {
            WLElementTransform T = Parent.Self.Transform;
            
            return ApplyTransformReverse(Local.Rect, Rect, Anchor, T.Anchor, PixelOffset, T.PixelOffset, Offset, T.Offset, Scale, T.Scale, Padding, T.Padding, Margin, T.Margin, MinSize, T.MinSize, MaxSize, T.MaxSize);
        };
        
        OnSceneTransformReverse = (Scene, _, Rect) => {
            WLElementTransform Transform = _.Self.Transform;
            WLWindow Window = (WLWindow)Scene.Data!;
            
            return Rect;
        };
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Центр расчёта
    /// </summary>
    public Vector2I Anchor = Vector2I.LeftTop;
    
    /// <summary>
    /// Относительная позиция по пикселям
    /// </summary>
    public Vector2I PixelOffset = Vector2I.Zero;

    /// <summary>
    /// Относительная позиция по размеру
    /// </summary>
    public Vector2D Offset = Vector2D.Zero;

    /// <summary>
    /// Относительный размер по размеру
    /// </summary>
    public Vector2D Scale = Vector2D.One;

    /// <summary>
    /// Внутренний отступ элемента
    /// </summary>
    public Vector4I Padding = Vector4I.Zero;
    
    /// <summary>
    /// Внешний отступ элемента
    /// </summary>
    public Vector4I Margin = Vector4I.Zero;
    
    /// <summary>
    /// Минимальный размер
    /// </summary>
    public Vector2UI MinSize = Vector2UI.Zero;

    /// <summary>
    /// Максимальный размер
    /// </summary>
    public Vector2UI MaxSize = Vector2UI.Max;
    
    // ----------------------------------------------------------------------

    public static Rect2I ApplyTransform(Rect2I A, Rect2I B, Vector2I AAnchor, Vector2I BAnchor, Vector2I APixelOffset, Vector2I BPixelOffset, Vector2D AOffset, Vector2D BOffset, Vector2D AScale, Vector2D BScale, Vector4I APadding, Vector4I BPadding, Vector4I AMargin, Vector4I BMargin, Vector2UI AMinSize, Vector2UI BMinSize, Vector2UI AMaxSize, Vector2UI BMaxSize){
        uint W = (uint)WL.Math.RoundD(A.W * AScale.X / BScale.X) + (uint)(AMargin.L + AMargin.R);
        uint H = (uint)WL.Math.RoundD(A.H * AScale.Y / BScale.Y) + (uint)(AMargin.T + AMargin.B);

        W = WL.Math.ClampUI(W, AMinSize.X, AMaxSize.X);
        H = WL.Math.ClampUI(H, AMinSize.Y, AMaxSize.Y);

        int AOX = (int)Math.Round((AAnchor.X + 1) * 0.5 * W);
        int AOY = (int)Math.Round((AAnchor.Y + 1) * 0.5 * H);

        int BOX = (int)Math.Round((BAnchor.X + 1) * 0.5 * B.W);
        int BOY = (int)Math.Round((BAnchor.Y + 1) * 0.5 * B.H);

        int X = B.X + BOX - AOX + (int)WL.Math.RoundD(B.W * (AOffset.X + BOffset.X)) + APixelOffset.X - BPixelOffset.X;

        int Y = B.Y + BOY - AOY + (int)WL.Math.RoundD(B.H * (AOffset.Y + BOffset.Y)) + APixelOffset.Y - BPixelOffset.Y;

        return new Rect2I(X, Y, W, H);
    }
    
    public static Rect2I ApplyTransformReverse(Rect2I A, Rect2I B, Vector2I AAnchor, Vector2I BAnchor, Vector2I APixelOffset, Vector2I BPixelOffset, Vector2D AOffset, Vector2D BOffset, Vector2D AScale, Vector2D BScale, Vector4I APadding, Vector4I BPadding, Vector4I AMargin, Vector4I BMargin, Vector2UI AMinSize, Vector2UI BMinSize, Vector2UI AMaxSize, Vector2UI BMaxSize){
        uint W = (uint)WL.Math.RoundD(A.W * BScale.X / AScale.X) - (uint)(AMargin.L + AMargin.R);
        uint H = (uint)WL.Math.RoundD(A.H * BScale.Y / AScale.Y) - (uint)(AMargin.T + AMargin.B);

        W = WL.Math.ClampUI(W, AMinSize.X, AMaxSize.X);
        H = WL.Math.ClampUI(H, AMinSize.Y, AMaxSize.Y);

        int AOX = (int)Math.Round((AAnchor.X + 1) * 0.5 * W);
        int AOY = (int)Math.Round((AAnchor.Y + 1) * 0.5 * H);

        int BOX = (int)Math.Round((BAnchor.X + 1) * 0.5 * B.W);
        int BOY = (int)Math.Round((BAnchor.Y + 1) * 0.5 * B.H);

        int X = A.X - (B.X + BOX - AOX + (int)WL.Math.RoundD(B.W * (AOffset.X + BOffset.X)) + APixelOffset.X - BPixelOffset.X);

        int Y = A.Y - (B.Y + BOY - AOY + (int)WL.Math.RoundD(B.H * (AOffset.Y + BOffset.Y)) + APixelOffset.Y - BPixelOffset.Y);

        return new Rect2I(X, Y, W, H);
    }
}