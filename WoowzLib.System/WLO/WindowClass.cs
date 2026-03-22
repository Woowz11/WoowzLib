using WL;

namespace WLO;

/// <summary>
/// Класс для WINAPI окна
/// </summary>
public class WindowClass{
    public WindowClass(string Name){
        try{
            this.Name = Name;
            
            __WindowProc = Native.Raw.Windows.DefaultWndProc;
            
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

    private readonly Native.Raw.Windows.WndProcDelegate __WindowProc;
    
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
                lpfnWndProc   = __WindowProc,
                hInstance     = Native.Raw.Windows.GetModuleHandle(null)
            };

            Atom = Native.Raw.Windows.RegisterClassEx(ref Class);
            if(Atom == 0){ throw new Exception("Произошла ошибка в RegisterClassEx!\nОшибка: " + WL.System.LastOSError()); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при регистрации класса [" + this + "] окна!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Название класса
    /// </summary>
    public readonly string Name;
    
    /// <summary>
    /// Ссылка на класс
    /// </summary>
    public ushort Atom{ get; private set; }
}