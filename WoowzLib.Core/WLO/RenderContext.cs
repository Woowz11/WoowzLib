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

    public void __ConnectWindow(WindowContext Window){ ConnectedWindow = Window; MakeContext(); __Start(); }

    public abstract void __Start();

    public const int __OpenGLMajor = 4;
    public const int __OpenGLMinor = 6;
}