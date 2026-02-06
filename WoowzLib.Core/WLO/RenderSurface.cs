namespace WLO;

public interface RenderSurface{
    /// <summary>
    /// Ширина области рендера
    /// </summary>
    /// <returns></returns>
    uint Render_Width ();
    
    /// <summary>
    /// Высота области рендера
    /// </summary>
    /// <returns></returns>
    uint Render_Height();

    /// <summary>
    /// Цвета области рендера
    /// </summary>
    /// <returns></returns>
    byte[] Render_PixelsRGBA();
    
    event Action? RenderDestroy;
    
    bool RenderAlive();
}