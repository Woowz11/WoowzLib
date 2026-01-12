namespace WLO;

public class GL : RenderContext{
    public GL(){
        
    }
    
    public void Test(){
        MakeContext();
        
        WL.GL.Native.glClear(WL.GL.Native.GL_COLOR_BUFFER_BIT);
    }

    /// <summary>
    /// Цвет заднего фона
    /// </summary>
    public ColorF BackgroundColor{
        get => __BackgroundColor;
        set{
            try{
                if(__BackgroundColor == value){ return; }
                __BackgroundColor = value;

                MakeContext();
                WL.GL.Native.glClearColor(__BackgroundColor.R, __BackgroundColor.G, __BackgroundColor.B, __BackgroundColor.A);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке цвета заднего фона GL [" + this + "]!", e);
            }
        }
    }
    private ColorF __BackgroundColor;
}