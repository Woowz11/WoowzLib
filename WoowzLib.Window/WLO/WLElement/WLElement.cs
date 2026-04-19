using System.Numerics;
using WLO.Attribute;
using WLO.Color;
using WLO.Rect;
using WLO.Vector;

namespace WLO.WLElement;

public abstract class WLElement : SceneObject<WLElement>/*, ITransform*/{
    protected WLElement(){ Name = DefaultName(); Transform = new WLElementTransform(/*this*/); }

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
            //WL.System.Draw.Fill(HDC, Transform.World.Rect, new BrushFill(Color4B.Magenta));
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
public class WLElementTransform : WorldTransformAlgorithm<WLElement> {
   
}