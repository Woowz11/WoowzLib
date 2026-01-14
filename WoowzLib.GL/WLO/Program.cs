namespace WLO.GL;

public class Program : GLResource{
    public Program(Render.GL Context) : base(Context){
        try{
            __Finish(0, "Программа");
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL программы [" + this + "]!", e);
        }
    }
    
    protected override void __Destroy(){
        
    }
}