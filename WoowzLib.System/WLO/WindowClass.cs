using WL;
using WLO.Vector;

namespace WLO;

/// <summary>
/// Класс для окна
/// </summary>
/// <param name="Name">Название класса</param>
/// <param name="Event">Обработка событий окна</param>
/// <param name="Instance">Область, где создать класс</param>
public class WindowClass{
    public WindowClass(string Name, WindowEvent? Event = null, IntPtr? Instance = null){
        try{
            this.Name = Name;

            this.Event = Event ?? ((Window, Message, WP, LP) => null);

            __WndProcDelegate = __Events;
            
            __Register(Instance ?? WL.System.Instance);
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

    /// <summary>
    /// Проверяет, существует указанный класс или нет
    /// </summary>
    /// <param name="ClassName">Название класса</param>
    /// <param name="Instance">Область, где искать</param>
    /// <param name="Class">Если нашёл класс, возвращает его</param>
    public static bool Exists(string ClassName, IntPtr Instance, out Native.Raw.Windows.WNDCLASS Class) => Native.Raw.Windows.GetClassInfo(Instance, ClassName, out Class);
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Регистрирует класс
    /// </summary>
    private void __Register(IntPtr Instance){
        try{
            if(Exists(Name, Instance, out Native.Raw.Windows.WNDCLASS _)){ throw new Exception("Такой класс уже зарегистрирован!"); }
            
            Native.Raw.Windows.WNDCLASSEX Class = new Native.Raw.Windows.WNDCLASSEX(Name){
                style         = 0,
                cbClsExtra    = 0,
                cbWndExtra    = 0,
                hIcon         = IntPtr.Zero,
                hCursor       = IntPtr.Zero,
                hbrBackground = IntPtr.Zero,
                lpszMenuName  = null!,
                hIconSm       = IntPtr.Zero,
                lpfnWndProc   = WL.System.Memory.SaveDelegate(__WndProcDelegate),
                hInstance     = Instance
            };
            
            Atom = Native.Raw.Windows.RegisterClassEx(ref Class);
            if(Atom == 0){ throw new Exception("Произошла ошибка в RegisterClassEx!\nОшибка: " + WL.System.LastOSError()); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при регистрации класса [" + this + "] окна!\nДескриптор: " + Instance, e);
        }
    }

    /// <summary>
    /// Делегат для Events (Окно, сообщение, информация 1, информация 2) => (Результат (если вернуть null, то вызовется DefWindowProc))
    /// </summary>
    public delegate IntPtr? WindowEvent(Window Window, uint Message, IntPtr WP, IntPtr LP);
    
    /// <summary>
    /// События
    /// </summary>
    private IntPtr __Events(IntPtr Handle, uint Message, IntPtr WP, IntPtr LP){
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
    private readonly Native.Raw.Windows.WndProcDelegate __WndProcDelegate;
    
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