using WLO.Color;

namespace WLO.WLElement;

public abstract class WLElement{
    protected WLElement(){
        Name = DefaultName();
        Transform = new TransformAlgorithm();
    }
    
    /// <summary>
    /// Позиция и размер элемента
    /// </summary>
    public TransformAlgorithm Transform;

    /// <summary>
    /// Название элемента
    /// </summary>
    public string Name;
    
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
            WL.System.Draw.Fill(HDC, Transform.Rect, new BrushFill(Color4B.Magenta));
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => GetType().Name + "(" + ToShortString() + ")";

    public string ToShortString() => Transform.ToVeryShortString();
}