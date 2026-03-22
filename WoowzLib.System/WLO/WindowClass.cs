using WL;

namespace WLO;

/// <summary>
/// Класс для WINAPI окна
/// </summary>
public class WindowClass{
    public WindowClass(string Name, WindowEvent? Event = null){
        try{
            this.Name = Name;

            this.Event = Event ?? new WindowEvent((Window, Message, WP, LP) => null);
            
            Register();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании класса WINAPI окна!", e);
        }
    }

    [Obsolete]
    public static WindowClass FromExisting(string Name){
        try{
            WindowClass Result = new WindowClass(Name);

            return Result;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при получении существующего класса WINAPI окна!\nНазвание: \"" + Name + "\"", e);
        }
    }
    
    [Obsolete]
    public static WindowClass FromExisting(ushort Atom){
        try{

            return null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при получении существующего класса WINAPI окна!\nAtom: " + Atom, e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Регистрирует класс
    /// </summary>
    private void Register(){
        try{
            Native.Raw.Windows.WNDCLASSEX Class = new Native.Raw.Windows.WNDCLASSEX(Name){
                style         = 0,
                cbClsExtra    = 0,
                cbWndExtra    = 0,
                hIcon         = IntPtr.Zero,
                hCursor       = IntPtr.Zero,
                hbrBackground = IntPtr.Zero,
                lpszMenuName  = null!,
                hIconSm       = IntPtr.Zero,
                lpfnWndProc   = Events,
                hInstance     = Native.Raw.Windows.GetModuleHandle(null)
            };

            Atom = Native.Raw.Windows.RegisterClassEx(ref Class);
            if(Atom == 0){ throw new Exception("Произошла ошибка в RegisterClassEx!\nОшибка: " + WL.System.LastOSError()); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при регистрации класса [" + this + "] окна!", e);
        }
    }

    /// <summary>
    /// Делегат для Events
    /// </summary>
    public delegate IntPtr? WindowEvent(Window Window, uint Message, long WP, long LP);
    
    /// <summary>
    /// События
    /// </summary>
    public IntPtr Events(IntPtr Window, uint Message, IntPtr WP, IntPtr LP){
        try{
            if(WLO.Window.Windows.TryGetValue(Window, out Window? Window__)){
                IntPtr? Result = null;
                
                try{
                    Result = Event(Window__, Message, WP.ToInt64(), LP.ToInt64());
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове событий у класса окна [" + this + "]!", e);
                }
                
                if(Message == Native.Raw.Windows.WM_DESTROY){
                    Window__.__Destroy();
                    Native.Raw.Windows.PostQuitMessage(0);
                }

                if(Result.HasValue){ return Result.Value; }
            }
            
            return Native.Raw.Windows.DefWindowProc(Window, Message, WP, LP);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении событий у класса окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Название класса
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Ивент класса, верните null, что-бы продолжить ивенты окна
    /// </summary>
    public readonly WindowEvent Event;
    
    /// <summary>
    /// Ссылка на класс
    /// </summary>
    public ushort Atom{ get; private set; }
}