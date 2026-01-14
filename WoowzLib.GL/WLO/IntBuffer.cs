namespace WLO.GL;

public class IntBuffer : Buffer{
    public IntBuffer(Render.GL Context) : base(Context, BufferType.Int){

    }
    /// <summary>
    /// Использовать этот Int буфер
    /// </summary>
    public Buffer Use(){
        Context.CurrentIntBuffer = this;
        return this;
    }
}