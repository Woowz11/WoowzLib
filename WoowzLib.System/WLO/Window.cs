using WL;

namespace WLO;

/// <summary>
/// WINAPI окно
/// </summary>
public class Window{
    public Window(WindowClass Class){
        try{
            this.Class = Class;
            
            Handle = Native.Raw.Windows.CreateWindowExW(
                0,
                Class.Name,
                "Test window woowzlib",
                Native.Raw.Windows.WS_OVERLAPPEDWINDOW,
                100, 100,
                800, 600,
                IntPtr.Zero,
                IntPtr.Zero,
                Native.Raw.Windows.GetModuleHandle(null),
                IntPtr.Zero
            );

            if(Handle == IntPtr.Zero){ throw new Exception("Произошла ошибка в CreateWindowExW!\nОшибка: " + WL.System.LastOSError()); }
            
            Windows[Handle] = this;
            try{
                OnCreate?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnCreate при создании WINAPI окна [" + this + "]!", e);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании WINAPI окна!", e);
        }
    }

    public void Destroy(){
        try{
            if(!Alive){ throw new Exception("Окно уже уничтожено!"); }

        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении WINAPI окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Ссылка на окно (ID окна)
    /// </summary>
    public IntPtr Handle{ get; private set; }

    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Handle != IntPtr.Zero;
    
    /// <summary>
    /// Класс окна
    /// </summary>
    public readonly WindowClass Class;
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Вызывается при уничтожении окна
    /// </summary>
    public void __Destroy(){
        try{
            if(!Alive){ return; }

            try{
                OnDestroy?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnDestroy при уничтожении WINAPI окна [" + this + "]!", e);
            }
            Windows.Remove(Handle);
            Handle = IntPtr.Zero;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при вызове уничтожения WINAPI окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Все запущенные окна
    /// </summary>
    public static readonly Dictionary<IntPtr, Window> Windows = new Dictionary<IntPtr, Window>();

    /// <summary>
    /// Вызывается при создании окна
    /// </summary>
    public static event Action<Window>? OnCreate;

    /// <summary>
    /// Вызывается при уничтожении окна
    /// </summary>
    public static event Action<Window>? OnDestroy;

    /// <summary>
    /// Обновляет окна (отправляет сообщения по окнам)
    /// </summary>
    public static void UpdateWindows(){
        while(Native.Raw.Windows.PeekMessage(out Native.Raw.Windows.MSG Message, IntPtr.Zero, 0, 0, Native.Raw.Windows.PM_REMOVE)){
            Native.Raw.Windows.TranslateMessage(ref Message);
            Native.Raw.Windows.DispatchMessage (ref Message);
        }
    }
}