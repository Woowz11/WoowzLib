namespace WLO;

public abstract class RenderContext{
    private static long CurrentContext;
    
    protected void MakeContext(){
        try{
            if(ConnectedWindow == null){ throw new Exception("Не установлено окно!"); }
            if(CurrentContext == ConnectedWindow.ID){ return; }
            CurrentContext = ConnectedWindow.ID;
            
            ConnectedWindow.__UpdateContext();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке контекста!", e);
        }
    }

    public WindowContext? ConnectedWindow{ get; private set; }

    public void __ConnectWindow(WindowContext Window){
        try{
            if(ConnectedWindow != null){ throw new Exception("Присоединённое окно уже есть!"); }

            ConnectedWindow = Window;
            MakeContext();
            __Start();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при присоединении окна [" + Window + "] рендеру [" + this + "]!", e);
        }
    }
    
    public void __UnconnectWindow(){
        try{
            if(ConnectedWindow == null){ throw new Exception("Нету окна!"); }
            
            __Stop();
            ConnectedWindow = null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при отсоединении окна у рендера [" + this + "]!", e);
        }
    }

    public abstract void __Start();
    public abstract void __Stop ();

    /// <summary>
    /// Рендер существует?
    /// </summary>
    public bool Started => ConnectedWindow != null;
    
    public const int __OpenGLMajor = 4;
    public const int __OpenGLMinor = 6;
}