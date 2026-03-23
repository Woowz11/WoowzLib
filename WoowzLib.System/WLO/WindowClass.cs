using WL;
using WLO.Vector;

namespace WLO;

/// <summary>
/// Класс для окна
/// </summary>
public class WindowClass{
    public WindowClass(string Name, WindowEvent? Event = null){
        try{
            this.Name = Name;

            this.Event = Event ?? ((Window, Message, WP, LP) => null);

            __WndProcDelegate = Events;
            
            Register();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании класса окна!", e);
        }
    }

    [Obsolete]
    public static WindowClass FromExisting(string Name){
        try{
            WindowClass Result = new WindowClass(Name);

            return Result;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при получении существующего класса окна!\nНазвание: \"" + Name + "\"", e);
        }
    }
    
    [Obsolete]
    public static WindowClass FromExisting(ushort Atom){
        try{

            return null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при получении существующего класса окна!\nAtom: " + Atom, e);
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
                lpfnWndProc   = __WndProcDelegate,
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
    public delegate IntPtr? WindowEvent(Window Window, uint Message, IntPtr WP, IntPtr LP);

    private readonly Native.Raw.Windows.WndProcDelegate __WndProcDelegate;
    /// <summary>
    /// События
    /// </summary>
    public IntPtr Events(IntPtr Handle, uint Message, IntPtr WP, IntPtr LP){
        try{
            if(WLO.Window.Windows.TryGetValue(Handle, out Window? Window)){
                IntPtr? Result = null;
                
                try{
                    Result = Event(Window, Message, WP, LP);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове событий у класса окна [" + this + "]!", e);
                }

                switch(Message){
                    case Native.Raw.Windows.WM_SETTEXT: {
                        string? Title = WL.System.Memory.LoadString(LP);

                        if(Title != null){ Window.__OnTitle(Title); } break;
                    }

                    case Native.Raw.Windows.WM_MOVE: {
                        if(!Native.Raw.Windows.GetWindowRect(Handle, out Native.Raw.Windows.RECT Rect)){ throw new Exception("Произошла ошибка в GetWindowRect в WM_MOVE!\nОшибка: " + WL.System.LastOSError()); }
                        
                        Window.__OnPosition(new Vector2I(Rect.left, Rect.top)); break;
                    }

                    case Native.Raw.Windows.WM_SIZE: {
                        if(!Native.Raw.Windows.GetWindowRect(Handle, out Native.Raw.Windows.RECT Rect)){ throw new Exception("Произошла ошибка в GetWindowRect в WM_SIZE!\nОшибка: " + WL.System.LastOSError()); }
                        
                        Window.__OnSize(new Vector2UI((uint)(Rect.width), (uint)(Rect.height))); break;
                    }
                    
                    case Native.Raw.Windows.WM_DESTROY: {
                        Window.__Destroy();
                        Native.Raw.Windows.PostQuitMessage(0);
                        break;
                    }
                }

                if(Result.HasValue){ return Result.Value; }
            }
        }catch(Exception e){
            Logger.Error("Произошла ошибка при обновлении событий у класса окна [" + this + "]!", e);
        }
        
        return Native.Raw.Windows.DefWindowProc(Handle, Message, WP, LP);
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
    
    // ----------------------------------------------------------------------

    public override string ToString() => "WindowClass(\"" + Name + "\", " + Atom + ")";

    public override bool Equals(object? Object){
        if(Object is not WindowClass Other){ return false; }
        if(Atom != 0 && Other.Atom != 0){ return Atom == Other.Atom; }
        return string.Equals(Name, Other.Name, StringComparison.Ordinal);
    }

    private readonly int __ID = Interlocked.Increment(ref __NextID); private static int __NextID;
    public override int GetHashCode() => __ID;
}