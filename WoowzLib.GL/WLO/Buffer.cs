namespace WLO.GL;

public enum BufferType : uint{
    /// <summary>
    /// (ARRAY)
    /// </summary>
    Float = WL.GL.Native.GL_ARRAY_BUFFER,
    /// <summary>
    /// (ELEMENT)
    /// </summary>
    Int = WL.GL.Native.GL_ELEMENT_ARRAY_BUFFER,
    Uniform,
    ShaderStorage,
    PixelPack,
    PixelUnpack,
    CopyRead,
    CopyWrite,
    TransformFeedback
}

public abstract class Buffer : GLResource{
    public Buffer(Render.GL Context, BufferType Type) : base(Context){
        try{
            this.Type = Type;
            uint[] ID__ = new uint[1];
            WL.GL.Native.glGenBuffers(1, ID__);
            ID = ID__[0];
            if(ID == 0){ throw new Exception("Не получилось создать шейдер в glGenBuffers!"); }

            __Finish(WL.GL.Native.GL_BUFFER, "Буфер");

            if(WL.GL.Debug.LogCreate){ Logger.Info("Создан буфер [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL буфера [" + this + "]!", e);
        }
    }
    
    protected override void __Destroy(){
        uint ID__ = ID;
        WL.GL.Native.glDeleteBuffers(1, ref ID__);
    }

    /// <summary>
    /// Тип буфера
    /// </summary>
    public readonly BufferType Type;
}