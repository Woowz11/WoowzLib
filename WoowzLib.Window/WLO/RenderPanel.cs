using Math = WL.Math;

namespace WLO;

public class RenderPanel : WindowElement, RenderSurface{
    public RenderPanel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;

        Img = new Image(Width, Height);
    }

    public override void Render(IntPtr HDC){
        Img.Change(Context => {
            for(uint y = 0; y < Img.Height; y++){
                for(uint x = 0; x < Img.Width; x++){
                    Context[x, y] = new ColorB(Math.Random.Fast_Byte(), Math.Random.Fast_Byte(), Math.Random.Fast_Byte(), 255);
                }
            }
        });
        
        WL.System.HDC.Image(HDC, X_Final, Y_Final, Width_Final, Height_Final, Img);
        
        base.Render(HDC);
    }

    public Image Img;
    
    public byte[] PixelsRGBA => Img.Pixels_RGBA;
    
    public uint Render_Width() => Width;
    public uint Render_Height() => Height;
    public byte[] Render_PixelsRGBA() => PixelsRGBA;
    public event Action? RenderDestroy;
    public bool RenderAlive(){ return !InMemory; }
}