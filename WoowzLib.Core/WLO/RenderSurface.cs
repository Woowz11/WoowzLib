namespace WLO;

public interface RenderSurface{
    IntPtr RenderHandle();
    uint RenderWidth ();
    uint RenderHeight();

    event Action? RenderDestroy;
}