namespace WL.WLO;

public class Button : WindowElement{
    public Button(string Text = "Button", int X = 0, int Y = 0, uint Width = 64, uint Height = 32){
        this.Text = Text;
        this.X = X;
        this.Y = Y;
        this.Width  = Width;
        this.Height = Height;
    }
    
    public override void __Create(){
        try{
            Handle = System.Native.Windows.CreateWindowExW(
                0,
                "BUTTON",
                __Text,
                System.Native.Windows.WS_CHILD | System.Native.Windows.WS_VISIBLE,
                __X, __Y, (int)__Width, (int)__Height,
                Parent!.Handle,
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
    
    public string Text{
        get => __Text;
        set{
            try{
                if(Created){ CheckDestroyed(); }

                if(__Text == value){ return; }
                __Text = value;

                if(Created){ System.Native.Windows.SetWindowText(Handle, __Text); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении названия текста у кнопки [" + this + "]!\nТекст: \"" + value + "\"", e);
            }
        }
    }
    private string __Text;
}