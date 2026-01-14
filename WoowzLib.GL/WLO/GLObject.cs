namespace WLO.GL;

public abstract class GLObject{
    protected GLObject(){}
    
    /// <summary>
    /// ID GL объекта
    /// </summary>
    public uint ID{ get; protected set; }

    /// <summary>
    /// GL объект создан?
    /// </summary>
    public bool Created => ID > 0;

    protected abstract uint __Create();

    protected abstract void __Destroy();

    
    public void Create(){
        try{
            if(!Created){
                ID = __Create();
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL объекта [" + this + "]!", e);
        }
    }

    public void Destroy(){
        try{
            if(Created){
                __Destroy();
                ID = 0;
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке GL объекта [" + this + "]!", e);
        }
    }
}