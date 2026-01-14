namespace WLO;

public abstract class RenderContext{
    private static long CurrentContext;
    
    protected void MakeContext(){
        try{
            if(CurrentContext == ConnectedWindow.ID){ return; }

            ConnectedWindow.__UpdateContext();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке контекста!", e);
        }
    }

    public WindowContext ConnectedWindow{ get; private set; }

    public void __ConnectWindow(WindowContext Window){ ConnectedWindow = Window; }

    public abstract void __Start();
}