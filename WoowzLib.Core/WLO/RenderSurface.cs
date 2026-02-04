namespace WLO;

public interface RenderSurface{
    uint RenderWidth ();
    uint RenderHeight();

    event Action? RenderDestroy;

    bool RenderAlive();
}