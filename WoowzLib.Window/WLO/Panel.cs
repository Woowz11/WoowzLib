namespace WLO;

public class Panel : WindowElement{
    public Panel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128, ColorB? Color = null, Image? Image = null){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
        if(Color.HasValue){ this.Color = Color.Value; }
        this.Image = Image;
    }

    public override void Render(IntPtr HDC){
        if(Image != null){
            WL.System.HDC.Image(HDC, X_Final, Y_Final, Width_Final, Height_Final, Image, Color);
        }else{
            WL.System.HDC.Fill(HDC, X_Final, Y_Final, Width_Final, Height_Final, Color);   
        }
        
        base.Render(HDC);
    }

    /// <summary>
    /// Цвет панели
    /// </summary>
    public ColorB Color = ColorB.White;

    /// <summary>
    /// Текстура
    /// </summary>
    public Image? Image = null;
}