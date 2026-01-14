namespace WLO.GL;

public class Shader : GLResource{
    public Shader(Render.GL Context) : base(Context){
        try{
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL шейдера [" + this + "]!", e);
        }
    }
    
    protected override void __Destroy(){
        
    }
}