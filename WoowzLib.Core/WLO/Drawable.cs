namespace WLO;

/// <summary>
/// Принимает RenderAPI для рендера в него изображения
/// </summary>
public abstract class Drawable{

}

public sealed class DrawableWindow : Drawable{
    public IntPtr Handle;

    public DrawableWindow(IntPtr Handle){
        this.Handle = Handle;
    }
}