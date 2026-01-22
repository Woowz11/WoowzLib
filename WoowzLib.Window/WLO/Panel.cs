using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class Panel : WindowElement{
    public Panel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128, ColorF? Color = null){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
        if(Color.HasValue){ this.Color = Color.Value; }
    }

    public override void Render(IntPtr HDC){
        System.HDC.Fill(HDC, X, Y, Width, Height, Color.ToRGBiA());
        
        base.Render(HDC);
    }

    /// <summary>
    /// Цвет панели
    /// </summary>
    public ColorF Color = ColorF.White;
}