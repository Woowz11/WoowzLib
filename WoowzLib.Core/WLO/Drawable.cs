namespace WLO;

/// <summary>
/// Принимает RenderAPI для рендера в него изображения
/// </summary>
public abstract class Drawable{ }

/// <summary>
/// Принимает RenderAPI для рендера в окно
/// </summary>
/// <param name="handle"></param>
public sealed class DrawableWindow(IntPtr handle) : Drawable{
    public readonly IntPtr Handle = handle;
}