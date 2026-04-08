using WLO.Color;

namespace WLO.WLElement;

public class Panel : WLElement{
    /// <summary>
    /// Цвет заднего фона панели
    /// </summary>
    public Color4B BackgroundColor = Color4B.Silver;

    public override void Render(WLWindow Window, IntPtr HDC){
        WL.System.Draw.Fill(HDC, Transform.World.Rect, new BrushFill(BackgroundColor));
    }
}