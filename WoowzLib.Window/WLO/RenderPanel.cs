using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class RenderPanel : WindowElement, RenderSurface{
    public RenderPanel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
    }

    public override void Render(IntPtr HDC){
        System.HDC.Fill(HDC, X_Final, Y_Final, Width_Final, Height_Final, ColorF.White.ToRGBiA());
        
        base.Render(HDC);
    }


    public uint RenderWidth() => Width;
    public uint RenderHeight() => Height;
    public event Action? RenderDestroy;
    public bool RenderAlive(){ return !InMemory; }
}