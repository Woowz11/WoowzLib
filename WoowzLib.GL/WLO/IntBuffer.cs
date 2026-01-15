namespace WLO.GL;

public class IntBuffer : Buffer{
    public IntBuffer(Render.GL Context) : base(Context, BufferType.Int){}
    
    /// <summary>
    /// Использовать этот Int буфер
    /// </summary>
    public IntBuffer Use(){
        Context.CurrentIntBuffer = this;
        return this;
    }
    
    protected override void __UpdateData(){
        
    }
    
    public override int Size(){
        return 0;
    }
    
    public override int ElementBSize(){
        return 0;
    }
    
    public override void __Use(){  }
}