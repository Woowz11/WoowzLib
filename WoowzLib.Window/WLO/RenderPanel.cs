using WLO;

namespace WL.WLO;

public class RenderPanel : WindowElement, RenderSurface{
    public RenderPanel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128){
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
    }
    
    public override void __Create(){
        try{
            Handle = System.Native.Windows.CreateWindowExW(
                0,
                "STATIC",
                "",
                System.Native.Windows.WS_CHILD | System.Native.Windows.WS_VISIBLE | System.Native.Windows.WS_CLIPCHILDREN | System.Native.Windows.WS_CLIPSIBLINGS,
                __X, __Y, (int)__Width, (int)__Height,
                Parent!.Handle,
                IntPtr.Zero, 
                System.Native.Windows.GetModuleHandle(null),
                IntPtr.Zero
            );

            __DefaultEvents__ = System.Native.Windows.GetClassLongPtrW(Handle, System.Native.Windows.GCLP_WNDPROC);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании панели [" + this + "]!", e);
        }
    }
    private IntPtr __DefaultEvents__;

    public override bool __NeedUpdateRender(){ return false; }

    public override IntPtr __Destroy(){
        RenderDestroy?.Invoke();
        
        System.Native.Windows.DestroyWindow(Handle);
        
        return IntPtr.Zero;
    }

    public override IntPtr __Events(IntPtr OtherWindow, uint Message, IntPtr WParam, IntPtr LParam){
        switch(Message){
            case System.Native.Windows.WM_COMMAND:
                return __UpdateCommand(WParam, LParam, Children);
        }
        
        return System.Native.Windows.CallWindowProcW(__DefaultEvents__, OtherWindow, Message, WParam, LParam);
    }

    public IntPtr RenderHandle() => Handle;
    public uint   RenderWidth () => Width ;
    public uint   RenderHeight() => Height;
    public event Action? RenderDestroy;
}