namespace WL.WLO;

public class Button : WindowElement{
    public override void __Create(){
        try{
            Handle = System.Native.Windows.CreateWindowExW(
                0,
                "BUTTON",
                "Test",
                System.Native.Windows.WS_CHILD | System.Native.Windows.WS_VISIBLE,
                0, 0, 64, 32,
                Parent,
                IntPtr.Zero, 
                System.Native.Windows.GetModuleHandle(null),
                IntPtr.Zero
            );
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании кнопки [" + this + "]!", e);
        }
    }
    
    public override void __Destroy(){
        System.Native.Windows.DestroyWindow(Handle);
    }
}