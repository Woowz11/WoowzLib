namespace WL.WLO;

public class RenderContext{
    public IntPtr Surface;

    public bool Alive => Surface != IntPtr.Zero;
}