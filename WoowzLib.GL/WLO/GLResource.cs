namespace WLO.GL;

public abstract class GLResource{
    protected GLResource(Render.GL Context){
        try{
            this.Context = Context;
            Context.__MakeContext();
            Context.__Register(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL ресурса [" + this + "]!\nКонтекст: " + Context, e);
        }
    }

    protected void __Finish(uint LabelType, string Name){
        try{
            this.LabelType = LabelType;
            this.Name      = Name + " (" + WL.GL.TotalCreatedResources + ")";

            Finished = true;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при завершении создании GL ресурса [" + this + "]!\nНазвание: \"" + Name + "\"");
        }
    }

    /// <summary>
    /// Инициализация завершена
    /// </summary>
    private bool Finished;
    
    /// <summary>
    /// ID GL ресурса
    /// </summary>
    public uint ID{ get; protected set; }

    /// <summary>
    /// GL ресурс создан?
    /// </summary>
    public bool Created => ID > 0 && Finished;

    /// <summary>
    /// Тип названия
    /// </summary>
    private uint LabelType;
    
    /// <summary>
    /// Имя ресурса
    /// </summary>
    public string Name{
        get => __Name;
        set{
            try{
                if(__Name == value){ return; }
                __Name = value;

                if(Created){
                    if(LabelType <= 0){ return; }

                    WL.GL.Native.glObjectLabel(LabelType, ID, __Name.Length, __Name);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при названии GL ресурса [" + this + "]!\nНазвание: \"" + value + "\"");
            }
        }
    }
    private string __Name;
    
    /// <summary>
    /// К какому GL контексту привязан?
    /// </summary>
    public readonly Render.GL Context;

    /// <summary>
    /// Вызывается при уничтожении ресурса
    /// </summary>
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
                if(WL.GL.Debug.LogDestroy){ Logger.Info("Уничтожение GL ресурса [" + this + "]!"); }
                
                Context.__MakeContext();
                
                __Destroy();
                
                Context.__Unregister(this);
                
                ID = 0;
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке уничтожении GL ресурса [" + this + "]!", e);
        }
    }

    #region Override

        public override string ToString(){
            return "GLResource(\"" + Name + "\", " + ID + ", " + Context + ")";
        }

    #endregion
}