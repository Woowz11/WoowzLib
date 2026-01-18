using System.Runtime.InteropServices;

namespace WL.WLO;

public class Window{
    public Window(uint Width = 800, uint Height = 600){
        try{
            this.Width  = Width;
            this.Height = Height;
            
            IntPtr Instance = WL.System.Native.Windows.GetModuleHandle(null);

            System.Native.Windows.WNDCLASSEX WindowClass = new System.Native.Windows.WNDCLASSEX{
                cbSize = (uint)Marshal.SizeOf<System.Native.Windows.WNDCLASSEX>(),
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new System.Native.Windows.WndProcDelegate(System.Native.Windows.WindowProc)),
                hInstance = Instance,
                lpszClassName = "WoowzLibWindow",
                hCursor = IntPtr.Zero,
                hbrBackground = IntPtr.Zero
            };

            WL.System.Native.Windows.RegisterClassEx(ref WindowClass);

            Handle = WL.System.Native.Windows.CreateWindowEx(
                0,
                "WoowzLibWindow",
                "WoowzLibWindow",
                WL.System.Native.Windows.WS_OVERLAPPEDWINDOW | WL.System.Native.Windows.WS_VISIBLE,
                0, 0,
                (int)__Width, (int)__Height,
                IntPtr.Zero,
                IntPtr.Zero,
                Instance,
                IntPtr.Zero
            );

            if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри CreateWindowEx!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Ссылка на окно
    /// </summary>
    public IntPtr Handle{ get; private set; }

    public uint Width{
        get => __Width;
        set{
            __Width = value;
        }
    }
    private uint __Width;
    
    public uint Height{
        get => __Height;
        set{
            __Height = value;
        }
    }
    private uint __Height;
}