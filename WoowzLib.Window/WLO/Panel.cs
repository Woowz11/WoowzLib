using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class Panel : WindowElement{
    public Panel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
    }
    
    public override void __Create(){
        try{
           
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании панели [" + this + "]!", e);
        }
    }
    
    public override void __Destroy(){
        
    }

    public override void Render(IntPtr HDC){
        System.HDC.Fill(HDC, new System.Native.Windows.RECT{left = X, top = Y, right = X + (int)Width, bottom = Y + (int)Height}, Color.ToRGBiA());
        
        base.Render(HDC);
    }

    /// <summary>
    /// Цвет панели
    /// </summary>
    public ColorF Color = ColorF.White;
}