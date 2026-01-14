namespace WLO.GL;

public class FloatBuffer : Buffer{
    public FloatBuffer(Render.GL Context) : base(Context, BufferType.Float){

    }
    /// <summary>
    /// Использовать этот Float буфер
    /// </summary>
    public Buffer Use(){
        Context.CurrentFloatBuffer = this;
        return this;
    }
}