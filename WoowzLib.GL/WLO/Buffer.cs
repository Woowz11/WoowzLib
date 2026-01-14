namespace WLO.GL;

public class Buffer : GLResource{
    public Buffer(Render.GL Context) : base(Context){}
    protected override void __Destroy(){
        throw new NotImplementedException();
    }
}