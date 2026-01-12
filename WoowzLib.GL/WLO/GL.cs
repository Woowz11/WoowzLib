using WL.WLO;

namespace WLO;

public class GL : RenderContext{
    public void Test(){
        WL.GL.Native.glClearColor(1, 0, 0, 1);
        WL.GL.Native.glClear(WL.GL.Native.GL_COLOR_BUFFER_BIT);
    }
}