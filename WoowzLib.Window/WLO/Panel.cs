namespace WL.WLO;

public class Panel : WindowElement{
    public Panel(int X = 0, int Y = 0, uint Width = 128, uint Height = 128){
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
                System.Native.Windows.WS_CHILD | System.Native.Windows.WS_VISIBLE,
                __X, __Y, (int)__Width, (int)__Height,
                Parent!.Handle,
                IntPtr.Zero, 
                System.Native.Windows.GetModuleHandle(null),
                IntPtr.Zero
            );
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании панели [" + this + "]!", e);
        }
    }
    
    public override void __Destroy(){
        System.Native.Windows.DestroyWindow(Handle);
    }
}