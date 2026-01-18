using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class Window{
    public Window(uint Width = 800, uint Height = 600){
        try{
            this.Width  = Width;
            this.Height = Height;
            
            IntPtr Instance = WL.System.Native.Windows.GetModuleHandle(null);

            __WPD = new System.Native.Windows.WndProcDelegate(__WndProc);
            
            System.Native.Windows.WNDCLASSEX WindowClass = new System.Native.Windows.WNDCLASSEX{
                cbSize = (uint)Marshal.SizeOf<System.Native.Windows.WNDCLASSEX>(),
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(__WPD),
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
            
            __SELFHANDLE = GCHandle.Alloc(this);
            System.Native.Windows.SetWindowLongPtr(Handle, System.Native.Windows.GWLP_USERDATA, GCHandle.ToIntPtr(__SELFHANDLE));
            
            WL.Window.Windows.Add(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }
    private GCHandle                              __SELFHANDLE;
    private System.Native.Windows.WndProcDelegate __WPD;

    /// <summary>
    /// Ссылка на окно
    /// </summary>
    public IntPtr Handle{ get; private set; }
    
    public void DestroyNow(){
        try{
            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                throw new Exception("Произошла ошибка при вызове ивентов на уничтожение окна!", e);
            }

            if(Handle == IntPtr.Zero){ throw new Exception("Ссылка на окно пустая!"); }

            WL.System.Native.Windows.DestroyWindow(Handle);
            
            Handle = IntPtr.Zero;
            ShouldDestroy = false;

            if(__SELFHANDLE.IsAllocated){ __SELFHANDLE.Free(); }

            WL.Window.Windows.Remove(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Окно должно уничтожиться?
    /// </summary>
    public bool ShouldDestroy{ get; private set; }

    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Handle != IntPtr.Zero && !ShouldDestroy;
    
    public void Destroy(){
        ShouldDestroy = true;
    }

    #region Процессы окна

        private static IntPtr __WndProc(IntPtr Window, uint Message, IntPtr WParam, IntPtr LParam){
            try{
                IntPtr Link = System.Native.Windows.GetWindowLongPtr(Window, System.Native.Windows.GWLP_USERDATA);
                if(Link != IntPtr.Zero){
                    Window W = (Window)GCHandle.FromIntPtr(Link).Target;
                    return W.__InstanceWndProc(Message, WParam, LParam);
                }

                return System.Native.Windows.DefWindowProc(Window, Message, WParam, LParam);
            }catch(Exception e){
                throw new Exception("Произошла ошибка у __WndProc!", e);
            }
        }

        private IntPtr __InstanceWndProc(uint Message, IntPtr WParam, IntPtr LParam){
            try{
                switch(Message){
                    case System.Native.Windows.WM_CLOSE:
                        ShouldDestroy = true;
                        
                        Logger.Info("CLOSE......");
                        
                        return IntPtr.Zero;
                }

                return System.Native.Windows.DefWindowProc(Handle, Message, WParam, LParam);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обработке ивентов [" + this + "]!", e);
            }
        }
        
    #endregion

    #region Ивенты

        /// <summary>
        /// Вызывается при закрытии окна (на крестик на пример)
        /// </summary>
        public event Action<Window>? OnClose;

        /// <summary>
        /// Вызывается при уничтожении окна
        /// </summary>
        public event Action<Window>? OnDestroy;

    #endregion
    
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