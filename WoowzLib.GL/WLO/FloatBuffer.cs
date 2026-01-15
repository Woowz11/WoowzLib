namespace WLO.GL;

public class FloatBuffer : Buffer{
    public FloatBuffer(Render.GL Context, MassiveF? Data = null, BufferUsage Usage = BufferUsage.Never) : base(Context, BufferType.Float){
        this.Usage = Usage;
        if(Data.HasValue){ Set(Data.Value); }
    }
    public FloatBuffer(Render.GL Context, float[] Data, BufferUsage Usage = BufferUsage.Never) : base(Context, BufferType.Float){
        this.Usage = Usage;
        Set(Data);
    }
    
    /// <summary>
    /// Использовать этот Float буфер
    /// </summary>
    public FloatBuffer Use(){
        Context.CurrentFloatBuffer = this;
        return this;
    }
    public override void __Use(){ Use(); }

    /// <summary>
    /// Значения
    /// </summary>
    private MassiveF Data = new MassiveF();
    
    public FloatBuffer Set(float[] Data){
        try{
            __SetAlways(Data);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке значений в FloatBuffer [" + this + "]!\nЗначения: " + Data, e);
        }
        
        return this;
    }
    public FloatBuffer Set(MassiveF Data){ return Set(Data.Data); }

    private void __SetAlways(float[] Data){
        Use();
        unsafe{
            fixed(float* Link = Data){
                WL.GL.Native.glBufferData(
                    (uint)Type,
                    (nint)(Data.Length * sizeof(float)),
                    (IntPtr)Link,
                    (uint)Usage
                );
            }
        }
        
        this.Data.Set(Data);
        
        if(WL.GL.Debug.LogBuffer){ Logger.Info("Установлены значения [" + Data.Length + "] буферу [" + this + "]!"); }
    }
    
    public FloatBuffer SetSlice(int Index, float[] Data){
        try{
            int OffsetBytes = Index * ElementBSize();
            int SliceBytes = Data.Length * ElementBSize();

            if(OffsetBytes + SliceBytes > BSize()){
                throw new Exception("Индекс и значения выходят за границы!");
            }

            Use();
            unsafe{
                fixed(float* Link = Data){
                    WL.GL.Native.glBufferSubData(
                        (uint)Type,
                        (nint)OffsetBytes,
                        (nint)SliceBytes,
                        (IntPtr)Link
                    );
                }
            }

            this.Data.SetSlice(Index, Data);
            
            if(WL.GL.Debug.LogBuffer){ Logger.Info("Установлены частичные значения [" + Index + ", " + Data.Length + "] буферу [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке частичных значений в FloatBuffer [" + this + "]!\nИндекс: " + Index + "\nЗначения: " + Data, e);
        }
        
        return this;
    }
    
    protected override void __UpdateData(){
        try{
            __SetAlways(Data.Data);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении значений у FloatBuffer [" + this + "]!", e);
        }
    }

    public override int Size(){
        return Data.Size;
    }

    public override int ElementBSize(){
        return Data.ElementBSize();
    }
}