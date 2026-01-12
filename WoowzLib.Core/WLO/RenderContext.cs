using WLO;

namespace WL.WLO;

public class RenderContext{
    private static long CurrentContext;
    
    protected void MakeContext(){
        if(CurrentContext == ConnectedWindow.ID){ return; }

        ConnectedWindow.__UpdateContext();
    }

    private WindowContext ConnectedWindow;

    public void __ConnectWindow(WindowContext Window){ ConnectedWindow = Window; }
}