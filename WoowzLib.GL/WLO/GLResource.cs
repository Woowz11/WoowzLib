namespace WLO.GL;

public abstract class GLResource{
    protected GLResource(Render.GL Context){
        try{
            this.Context = Context;
            Context.__Register(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL ресурса [" + this + "]!\nКонтекст: " + Context, e);
        }
    }
    
    /// <summary>
    /// ID GL ресурса
    /// </summary>
    public uint ID{ get; protected set; }

    /// <summary>
    /// GL ресурс создан?
    /// </summary>
    public bool Created => ID > 0;

    /// <summary>
    /// К какому GL контексту привязан?
    /// </summary>
    public readonly Render.GL Context;

    protected abstract void __Destroy();

    /// <summary>
    /// Удаление GL ресурса из контекста (выдаст ошибку если уже уничтоженный)
    /// </summary>
    public void Destroy(){
        try{
            if(Created){
                TryDestroy();
            }else{
                throw new Exception("Ресурс уже уничтоженный!");
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении GL ресурса [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Попытка удаления GL ресурса из контекста
    /// </summary>
    public void TryDestroy(){
        try{
            if(Created){
                __Destroy();
                
                Context.__Unregister(this);
                
                ID = 0;
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке уничтожении GL ресурса [" + this + "]!", e);
        }
    }
}