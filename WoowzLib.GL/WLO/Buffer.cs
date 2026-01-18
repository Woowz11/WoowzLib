using WL;

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

public enum BufferUsage : uint{
    /// <summary>
    /// Буфер изменяется очень часто
    /// </summary>
    Always = WL.GL.Native.GL_STREAM_DRAW,
    /// <summary>
    /// Буфер изменяется иногда
    /// </summary>
    Maybe = WL.GL.Native.GL_DYNAMIC_DRAW,
    /// <summary>
    /// Буфер никогда не меняется
    /// </summary>
    Never = WL.GL.Native.GL_STATIC_DRAW
}

/// <summary>
/// (BUFFER)
/// </summary>
public abstract class Buffer : GLResource, ArrayByteObject{
    protected Buffer(BufferType Type, BufferUsage Usage = BufferUsage.Never) : base(){
        try{
            this.Type = Type;
            uint[] ID__ = new uint[1];
            WL.GL.Native.glGenBuffers(1, ID__);
            ID = ID__[0];
            if(ID == 0){ throw new Exception("Не получилось создать шейдер в glGenBuffers!"); }

            GLValue = GLValueFromBufferType(Type);
            
            __Finish(WL.GL.Native.GL_BUFFER, Type + " Буфер");

            this.Usage = Usage;

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

    /// <summary>
    /// Частота изменения буфера
    /// </summary>
    public BufferUsage Usage{
        get => __Usage;
        set{
            try{
                if(__Usage == value){ return; }
                __Usage = value;
            
                __UpdateData();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении частоты применения у буфера [" + this + "]!\nЧастота применения: " + value, e);
            }
        }
    }
    private BufferUsage __Usage;

    /// <summary>
    /// Значение GLValue из BufferType
    /// </summary>
    public static GLValue GLValueFromBufferType(BufferType Type){
        return Type switch{
            BufferType.Float => GLValue.Float,
            BufferType.Int   => GLValue.Int
        };
    }

    /// <summary>
    /// Значение GLValue
    /// </summary>
    public readonly GLValue GLValue;
    
    /// <summary>
    /// Кол-во значений в буфере
    /// </summary>
    public abstract int Size();

    /// <summary>
    /// Использование буфера
    /// </summary>
    public abstract void __Use();
    
    protected abstract void __UpdateData();
    
    #region Override

        public override string ToString(){
            return "Buffer(\"" + Name + "\", " + Type + "|" + Usage + ", " + Size() + ", " + IDString + ", " + Context + ")";
        }
        
        public int BSize(){
            return Size() * ElementBSize();
        }

        public abstract int ElementBSize();

        #endregion
}