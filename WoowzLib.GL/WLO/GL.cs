using WL.WLO;

namespace WLO;

public class GL : RenderContext{
    public void Test(float r, float g, float b){
        MakeContext();
        
        WL.GL.Native.glClearColor(r, g, b, 1);
        WL.GL.Native.glClear(WL.GL.Native.GL_COLOR_BUFFER_BIT);
    }
}