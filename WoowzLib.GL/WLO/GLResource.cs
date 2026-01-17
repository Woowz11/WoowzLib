namespace WLO.GL;

public abstract class GLResource{
    protected GLResource(Render.GL Context){
        try{
            this.Context = Context;
            Context.__MakeContext();
            Context.__Register(this);

            GlobalID = WL.GL.TotalCreatedResources;
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
    /// Глобальный ID (Полностью уникальный, НЕ GL ID)
    /// </summary>
    public readonly int GlobalID;

    /// <summary>
    /// ID ресурса в виде строки
    /// </summary>
    public string IDString => ID + "|" + GlobalID;
    
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
    /// Родительский ресурс (пока он есть, ресурс не может очиститься)
    /// </summary>
    private readonly List<GLResource> __Parent = [];

    /// <summary>
    /// Дети ресурса (уничтожает все эти ресурсы при собственном уничтожении)
    /// </summary>
    private readonly List<GLResource> __Children = [];

    /// <summary>
    /// Добавляет родителя
    /// </summary>
    /// <param name="Parent"></param>
    public void __AddParent(GLResource Parent){
        __Parent.Add(Parent);
        Parent.__Children.Add(this);
    }

    /// <summary>
    /// Убирает родителя
    /// </summary>
    /// <param name="Parent"></param>
    public void __RemoveParent(GLResource Parent){
        __Parent.Remove(Parent);
        Parent.__Children.Remove(this);
    }

    /// <summary>
    /// Проверяет, если контексты не совпадают, то выдаёт ошибку
    /// </summary>
    public void CheckContext(Render.GL OtherContext){ if(Context != OtherContext){ throw new Exception("Контексты не совпадают! [" + Context + "] != [" + OtherContext + "]"); } }
    
    /// <summary>
    /// Вызывается при уничтожении ресурса
    /// </summary>
    protected abstract void __Destroy();
    
    /// <summary>
    /// Удаление GL ресурса из контекста (выдаст ошибку если уже уничтоженный)
    /// </summary>
    public void Destroy(){
        try{
            if(!Created          ){ throw new Exception("Ресурс уже уничтоженный!"); }
            if(__Parent.Count > 0){ throw new Exception("Невозможно уничтожить, есть родитель!"); }

            TryDestroy();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении GL ресурса [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Попытка удаления GL ресурса из контекста
    /// </summary>
    public void TryDestroy(){
        try{
            if(Created && __Parent.Count == 0){
                if(WL.GL.Debug.LogDestroy){ Logger.Info("Уничтожение GL ресурса [" + this + "]!"); }
                
                Context.__MakeContext();

                foreach(GLResource Child in __Children){
                    Child.TryDestroy();
                }
                __Children.Clear();
                __Parent  .Clear();
                
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
            return "GLResource(\"" + Name + "\", " + IDString + ", " + Context + ")";
        }

        public override bool Equals(object? Obj){
            if(Obj is not GLResource Other){ return false; }
            return GlobalID == Other.GlobalID;
        }

        public override int GetHashCode() => HashCode.Combine(GlobalID);

        public static bool operator ==(GLResource? A, GLResource? B){
            if(A is null || B is null){ return false; }
            return A.Equals(B);
        }

        public static bool operator !=(GLResource? A, GLResource? B) => !(A == B);
        
    #endregion
}