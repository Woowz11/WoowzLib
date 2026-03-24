using System.Numerics;
using System.Text;
using WL;
using WLO.Attribute;
using WLO.Vector;

namespace WLO;

/// <summary>
/// окно
/// </summary>
public class Window{
    static Window(){
        WL.Core.OnTerminate += CloseReason => {
            foreach(Window Window in Windows.Values.ToArray()){
                try{
                    if(Window.Alive){ Window.Destroy(); }
                    
                    Window.__Destroy();
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при очистке оставшихся окон! Окно: " + Window, e);
                }
            }
            
            Windows.Clear();
            WindowsIDs.Clear();
        };
    }
    
    /// <summary>
    /// Значения для конструктора окна
    /// </summary>
    public class WindowConstructor{
        
        /// <summary>
        /// Стартовый заголовок окна
        /// </summary>
        public string Title = "Window";
        
        /// <summary>
        /// Стартовая позиция окна
        /// </summary>
        public Vector2I Position = Vector2I.Zero;
        
        /// <summary>
        /// Стартовый размер окна
        /// </summary>
        public Vector2UI Size = new Vector2UI(800, 600);

        /// <summary>
        /// Видно окно при старте?
        /// </summary>
        public bool Visible = true;

        /// <summary>
        /// Родительское окно при старте
        /// </summary>
        public Window? Parent = null;
    }
    
    public Window(WindowClass Class, WindowConstructor? Config = null){
        try{
            Config ??= new WindowConstructor();
            
            this.Class = Class;
            ClassName  = Class.Name;

            Hierarchy = new SceneNode<Window>(this);

            __CreateWindow(ClassName, Config);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна!\nКласс: " + Class + "\nКонфиг: " + WL.__Base.Other.ToString(Config), e);
        }
    }
    
    public Window(string ExistingClass, WindowConstructor? Config = null){
        try{
            Config ??= new WindowConstructor();

            Class     = null;
            ClassName = ExistingClass;
            
            Hierarchy = new SceneNode<Window>(this);

            __CreateWindow(ClassName, Config);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна!\nЗагруженный класс: " + ExistingClass + "\nКонфиг: " + WL.__Base.Other.ToString(Config), e);
        }
    }

    /// <summary>
    /// Уничтожить окно
    /// </summary>
    public void Destroy(){
        try{
            if(!Alive){ throw new Exception("Окно уже уничтожено!"); }

            Native.Raw.Windows.DestroyWindow(Handle);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + ToShortString() + "]!", e);
        }
    }

    /// <summary>
    /// Уникальный ID окна
    /// </summary>
    public readonly int ID = Interlocked.Increment(ref __NextID); private static int __NextID;
    
    /// <summary>
    /// Ссылка на окно (Удаляется, после удаления окна)
    /// </summary>
    public IntPtr Handle{ get; private set; }

    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Native.Raw.Windows.IsWindow(Handle);
    
    /// <summary>
    /// Проверяет, живое ли окно
    /// </summary>
    public void CheckAlive(){ if(!Alive){ throw new Exception("Окно не живое!"); } }
    
    /// <summary>
    /// Класс окна (если указан)
    /// </summary>
    public readonly WindowClass? Class;

    /// <summary>
    /// Название класса
    /// </summary>
    public readonly string ClassName;

    /// <summary>
    /// Child/Parent окна
    /// </summary>
    public readonly SceneNode<Window> Hierarchy;
    
    // ----------------------------------------------------------------------

    #region Заголовок

        /// <summary>
        /// Вызывается при изменении заголовка окна, (Окно, заголовок)
        /// </summary>
        public event Action<Window, string>? OnTitle;
        
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title{
            get{
                try{
                    CheckAlive();

                    StringBuilder SB = new StringBuilder(512);

                    int Length = Native.Raw.Windows.GetWindowText(Handle, SB, SB.Capacity);
                    
                    return Length > 0 ? SB.ToString() : string.Empty;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при получении заголовка окна [" + ToShortString() + "]!", e);
                }
            }
            set{
                try{
                    CheckAlive();
                    
                    if(Title == value){ return; }

                    try{
                        OnTitle?.Invoke(this, value);
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка внутри ивента OnTitle у окна [" + ToShortString() + "]!\nЗаголовок: " + value, e);
                    }
                    
                    if(!Native.Raw.Windows.SetWindowTextW(Handle, value)){ throw new Exception("Произошла ошибка в SetWindowTextW!\nОшибка: " + WL.System.LastOSError()); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке заголовка окну [" + ToShortString() + "]!\nЗаголовок: \"" + value + "\"", e);
                }
            }
        }

    #endregion

    #region Позиция

        /// <summary>
        /// Вызывается при изменении позиции окна, (Окно, позиция, клиентская позиция)
        /// </summary>
        public event Action<Window, Vector2I, Vector2I>? OnPosition;
        internal void __OnPosition(Vector2I Position){
            try{
                OnPosition?.Invoke(this, Position, ClientPosition);
            }catch(Exception e){
                Logger.Error("Произошла ошибка внутри ивента OnPosition у окна [" + ToShortString() + "]!\nПозиция: " + Position, e);
            }
        }
    
        #region Screen

            /// <summary>
            /// Позиция окна (с учётом рамки)
            /// </summary>
            public Vector2I Position{
                get{
                    try{
                        CheckAlive();

                        if(!Native.Raw.Windows.GetWindowRect(Handle, out Native.Raw.Windows.RECT Rect)){ throw new Exception("Произошла ошибка в GetWindowRect!\nОшибка: " + WL.System.LastOSError()); }

                        return new Vector2I(Rect.left, Rect.top);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении позиции окна [" + ToShortString() + "]!", e);
                    }
                }
                set{
                    try{
                        CheckAlive();
                            
                        if(Position == value){ return; }
                        
                        __UpdateWindowPosition(value);
                        
                        __OnPosition(value);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке позиции окна [" + ToShortString() + "]!\nПозиция: " + value.ToPositionString(), e);
                    }
                }
            }

        #endregion

        #region Client

            /// <summary>
            /// Клиентская позиция окна (без учёта рамки)
            /// </summary>
            [RequireTesting(TestingInformation.New, "Неизвестно, верная формула или нет")]
            public Vector2I ClientPosition{
                get{
                    try{
                        CheckAlive();

                        return ClientToScreen(Vector2I.Zero);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении клиентской позиции окна [" + ToShortString() + "]!", e);
                    }
                }
                set{
                    try{
                        CheckAlive();

                        Vector2I __ClientPosition = ClientPosition;
                        
                        if(__ClientPosition == value){ return; }

                        Vector2I Offset = __ClientPosition - Position;
                        Vector2I __Position = value - Offset;
                        Position = __Position;
                        
                        __OnPosition(__Position);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке клиентской позиции окна [" + ToShortString() + "]!\nПозиция: " + value.ToPositionString(), e);
                    }
                }
            }

        #endregion

    #endregion

    #region Размер
    
        /// <summary>
        /// Вызывается при изменении размера окна, (Окно, размер, клиентский размер)
        /// </summary>
        public event Action<Window, Vector2UI, Vector2UI>? OnSize;
        internal void __OnSize(Vector2UI Size){
            try{
                OnSize?.Invoke(this, Size, ClientSize);
            }catch(Exception e){
                Logger.Error("Произошла ошибка внутри ивента OnSize у окна [" + ToShortString() + "]!\nРазмер: " + Size, e);
            }
        }

        #region Screen

            /// <summary>
            /// Размер окна (с учётом рамки)
            /// </summary>
            public Vector2UI Size{
                get{
                    try{
                        CheckAlive();
                        
                        if(!Native.Raw.Windows.GetWindowRect(Handle, out Native.Raw.Windows.RECT Rect)){ throw new Exception("Произошла ошибка в GetWindowRect!\nОшибка: " + WL.System.LastOSError()); }

                        return new Vector2UI((uint)Rect.width, (uint)Rect.height);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении размера окна [" + ToShortString() + "]!", e);
                    }
                }
                set{
                    try{
                        CheckAlive();
                        
                        if(Size == value){ return; }

                        __UpdateWindowSize(value);
                        
                        __OnSize(value);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке размера окна [" + ToShortString() + "]!\nРазмер: " + value.ToSizeString(), e);
                    }
                }
            }

        #endregion

        #region Client

            /// <summary>
            /// Размер окна (без учёта рамки)
            /// </summary>
            public Vector2UI ClientSize{
                get{
                    try{
                        CheckAlive();
                        
                        if(!Native.Raw.Windows.GetClientRect(Handle, out Native.Raw.Windows.RECT Rect)){ throw new Exception("Произошла ошибка в GetClientRect!\nОшибка:" + WL.System.LastOSError()); }
                        
                        return new Vector2UI((uint)(Rect.width), (uint)(Rect.height));
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при получении клиентского размера окна [" + ToShortString() + "]!", e);
                    }
                }
                set{
                    try{
                        CheckAlive();
                            
                        if(ClientSize == value){ return; }

                        Native.Raw.Windows.RECT Rect = new Native.Raw.Windows.RECT(0, 0, (int)value.W, (int)value.H);

                        if(!Native.Raw.Windows.AdjustWindowRectEx(ref Rect, Style, false, 0)){ throw new Exception("Произошла ошибка в AdjustWindowRectEx!\nОшибка: " + WL.System.LastOSError()); }

                        Size = new Vector2UI((uint)Rect.width, (uint)Rect.height);
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке клиентского размера окна [" + ToShortString() + "]!\nРазмер: " + value.ToSizeString(), e);
                    }
                }
            }

        #endregion

    #endregion

    #region Видимость

        /// <summary>
        /// Вызывается при изменении видимости окна, (Окно, видимый?)
        /// </summary>
        public event Action<Window, bool>? OnVisible;
    
        /// <summary>
        /// Окно видимое?
        /// </summary>
        public bool Visible{
            get{
                try{
                    CheckAlive();

                    return (Style & Native.Raw.Windows.WS_VISIBLE) != 0;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при получении видимости окна [" + ToShortString() + "]!", e);
                }
            }
            set{
                try{
                    CheckAlive();
                    
                    if(Visible == value){ return; }

                    Native.Raw.Windows.ShowWindow(Handle, value ? Native.Raw.Windows.SW_SHOW : Native.Raw.Windows.SW_HIDE);
                    
                    try{
                        OnVisible?.Invoke(this, value);
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка внутри ивента OnVisible у окна [" + ToShortString() + "]!\nВидимость: " + value, e);
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке видимости окну [" + ToShortString() + "]!\nВидимость: " + value, e);
                }
            }
        }

    #endregion

    #region Стиль

        /// <summary>
        /// Стиль окна
        /// </summary>
        public uint Style{
            get{
                try{
                    CheckAlive();

                    return (uint)Native.Raw.Windows.GetWindowLong(Handle, Native.Raw.Windows.GWL_STYLE);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при получении стиля окна [" + ToShortString() + "]!", e);
                }
            }
            set{
                try{
                    CheckAlive();
                    
                    if(Style == value){ return; }

                    uint Style__ = value;

                    if(Hierarchy.Parent != null && !HasStyle(Style__, Native.Raw.Windows.WS_CHILD)){ Style__ |= Native.Raw.Windows.WS_CHILD; }
                    if(Visible && !HasStyle(Style__, Native.Raw.Windows.WS_VISIBLE)){ Style__ |= Native.Raw.Windows.WS_VISIBLE; }
                    
                    Native.Raw.Windows.SetWindowLong(Handle, Native.Raw.Windows.GWL_STYLE, (int)Style__);

                    Native.Raw.Windows.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, Native.Raw.Windows.SWP_NOMOVE | Native.Raw.Windows.SWP_NOSIZE | Native.Raw.Windows.SWP_NOZORDER | Native.Raw.Windows.SWP_FRAMECHANGED);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке стиля окну [" + ToShortString() + "]!\nСтиль: " + value, e);
                }
            }
        }

        /// <summary>
        /// Есть стиль в стиле?
        /// </summary>
        public static bool HasStyle(uint Style, uint Flag) => (Style & Flag) != 0;

    #endregion
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Конвертирует клиентские координаты окна в экранные координаты
    /// </summary>
    public Vector2I ClientToScreen(Vector2I Client){
        try{
            Native.Raw.Windows.POINT Point = new Native.Raw.Windows.POINT(Client.X, Client.Y);

            return !Native.Raw.Windows.ClientToScreen(Handle, ref Point) ? throw new Exception("Произошла ошибка в ClientToScreen!\nОшибка: " + WL.System.LastOSError()) : new Vector2I(Point.X, Point.Y);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при конвертации Client -> Screen координат у окна [" + this + "]!\nClient координаты: " + Client, e);
        }
    }

    /// <summary>
    /// Конвертирует экранные координаты в клиентские координаты окна
    /// </summary>
    public Vector2I ScreenToClient(Vector2I Screen){
        try{
            Native.Raw.Windows.POINT Point = new Native.Raw.Windows.POINT(Screen.X, Screen.Y);

            return !Native.Raw.Windows.ClientToScreen(Handle, ref Point) ? throw new Exception("Произошла ошибка в ScreenToClient!\nОшибка: " + WL.System.LastOSError()) : new Vector2I(Point.X, Point.Y);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при конвертации Screen -> Client координат у окна [" + this + "]!\nScreen координаты: " + Screen, e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Вызывается при уничтожении окна
    /// </summary>
    internal void __Destroy(){
        try{
            if(!Windows.ContainsKey(ID)){ return; }
            
            Hierarchy.ClearAll();
            Hierarchy.Parent = null;
            Hierarchy.CanUse = false;
            
            try{
                OnDestroy?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnDestroy при уничтожении окна [" + this + "]!", e);
            }
            Windows.Remove(ID);

            IntPtr ToRemove = IntPtr.Zero;
            foreach(KeyValuePair<IntPtr, int> Pair in WindowsIDs.Where(Pair => Pair.Value == ID)){
                ToRemove = Pair.Key;
            }
            if(ToRemove != IntPtr.Zero){
                WindowsIDs.Remove(ToRemove);
            }
            
            Handle = IntPtr.Zero;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при вызове уничтожения окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Обновляет позицию окна
    /// </summary>
    private void __UpdateWindowPosition(Vector2I Position){
        if(!Native.Raw.Windows.SetWindowPos(Handle, IntPtr.Zero, Position.X, Position.Y, 0, 0, Native.Raw.Windows.SWP_NOZORDER | Native.Raw.Windows.SWP_NOACTIVATE | Native.Raw.Windows.SWP_NOSIZE)){
            throw new Exception("Произошла ошибка в SetWindowPos, внутри __UpdateWindowPosition!\nОшибка: " + WL.System.LastOSError() + "\nПозиция: " + Position);
        }
    }

    /// <summary>
    /// Обновляет размер окна
    /// </summary>
    private void __UpdateWindowSize(Vector2UI Size){
        if(!Native.Raw.Windows.SetWindowPos(Handle, IntPtr.Zero, 0, 0, (int)Size.W, (int)Size.H, Native.Raw.Windows.SWP_NOZORDER | Native.Raw.Windows.SWP_NOACTIVATE | Native.Raw.Windows.SWP_NOMOVE)){
            throw new Exception("Произошла ошибка в SetWindowPos, внутри __UpdateWindowSize!\nОшибка: " + WL.System.LastOSError() + "\nРазмер: " + Size);
        }
    }

    /// <summary>
    /// Создаёт окно
    /// </summary>
    /// <param name="Class">Название класса</param>
    private void __CreateWindow(string Class, WindowConstructor Config){
        uint Style__ = Native.Raw.Windows.WS_OVERLAPPEDWINDOW;

        if(Config.Parent != null){ Style__ = Native.Raw.Windows.WS_CHILD; }
        
        Handle = Native.Raw.Windows.CreateWindowExW(
            0,
            Class,
            Config.Title!,
            Style__,
            Config.Position.X, Config.Position.Y,
            (int)Config.Size.W, (int)Config.Size.H,
            Config.Parent?.Handle ?? IntPtr.Zero,
            IntPtr.Zero,
            Native.Raw.Windows.GetModuleHandle(null),
            IntPtr.Zero
        );

        if(Handle == IntPtr.Zero){
            int OSError = WL.System.LastOSError();
            if(OSError == Native.Raw.Windows.ERROR_CLASS_DOES_NOT_EXIST){
                throw new Exception("Не найден оконный класс \"" + Class + "\"!");
            }else{
                throw new Exception("Произошла ошибка в CreateWindowExW!\nОшибка: " + OSError);
            }
        }

        Hierarchy.Parent = Config.Parent?.Hierarchy;
        
        if(Config.Visible){ Visible = true; }
            
        Windows[ID] = this;
        WindowsIDs[Handle] = ID;
        try{
            OnCreate?.Invoke(this);
        }catch(Exception e){
            Logger.Error("Произошла ошибка в ивенте OnCreate при создании окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Все запущенные окна
    /// </summary>
    public static readonly Dictionary<int, Window> Windows = new Dictionary<int, Window>();

    /// <summary>
    /// Все ID окон
    /// </summary>
    public static readonly Dictionary<IntPtr, int> WindowsIDs = new Dictionary<IntPtr, int>();

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

        foreach(Window Window in Windows.Values.ToArray()){
            if(!Window.Alive){
                Window.__Destroy();
            }
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "Window(" + ID + ", " + (Alive ? ("\"" + Title + "\", " + Handle + ", " + Size.ToSizeString() + ", " + Position.ToPositionString() + ", " + (Hierarchy.Parent?.Self.ToShortString() ?? "Нет родителя")) : "Уничтожено") + ", " + (Class == null ? ClassName : Class) + ")";

    public string ToShortString() => "Window(" + ID + ", " + (Alive ? Handle : "Уничтожено") + ", " + (Class == null ? ClassName : Class) + ")";
    
    public override bool Equals(object? Object){
        if(Object is not Window Other){ return false; }
        if(ReferenceEquals(this, Other)){ return true; }
        return ID == Other.ID;
    }
    
    public override int GetHashCode() => ID;
}