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
    
    // ----------------------------------------------------------------------

    public const string Default_Button       = "Button";
    public const string Default_Edit         = "Edit";
    public const string Default_Static       = "Static";
    public const string Default_ListBox      = "ListBox";
    public const string Default_ComboBox     = "ComboBox";
    public const string Default_ScrollBar    = "ScrollBar";
    public const string Default_Dialog       = "#32770";
    public const string Default_Popup        = "#32768";
    public const string Default_Desktop      = "#32769";
    public const string Default_ListView     = "SysListView32";
    public const string Default_TreeView     = "SysTreeView32";
    public const string Default_TabControl   = "SysTabControl32";
    public const string Default_TrackBar     = "msctls_trackbar32";
    public const string Default_ProgressBar  = "msctls_progress32";
    public const string Default_UpDown       = "msctls_updown32";
    public const string Default_ReBar        = "ReBarWindow32";
    public const string Default_ToolBar      = "ToolbarWindow32";
    public const string Default_HotKey       = "msctls_hotkey32";
    public const string Default_StatusBar    = "msctls_statusbar32";
    public const string Default_Header       = "SysHeader32";
    public const string Default_ComboBoxEx   = "ComboBoxEx32";
    public const string Default_DateTimePick = "SysDateTimePick32";
    public const string Default_MonthCal     = "SysMonthCal32";
    public const string Default_Pager        = "SysPager";
    public const string Default_Message      = "#32771";
    public const string Default_ComboLBox    = "ComboLBox";
    public const string Default_Animate      = "SysAnimate32";
    public const string Default_IPAddress    = "SysIPAddress32";
    public const string Default_Shadow       = "SysShadow";
    
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
            if(WLO.Window.WindowsIDs.TryGetValue(Handle, out int ID)){
                Window Window = WLO.Window.Windows[ID];
                
                IntPtr? Result = null;

                try{
                    Result = Event(Window, Message, WP, LP);
                }
                catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове событий у класса окна [" + this + "]!", e);
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
    /// ID класса
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