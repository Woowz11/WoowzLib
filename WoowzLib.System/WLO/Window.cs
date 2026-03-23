using WL;
using WLO.Vector;

namespace WLO;

/// <summary>
/// WINAPI окно
/// </summary>
public class Window{
    static Window(){
        WL.Core.OnTerminate += () => {
            foreach(Window Window in Windows.Values.ToArray()){
                try{
                    if(Window.Alive){ Window.Destroy(); }
                    
                    Window.__Destroy();
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при очистке оставшихся WINAPI окон! Окно: " + Window);
                }
            }
            
            Windows.Clear();
        };
    }
    
    public Window(WindowClass Class, string Title, Vector2I Position, Vector2UI Size){
        try{
            this.Class = Class;
            
            Handle = Native.Raw.Windows.CreateWindowExW(
                0,
                Class.Name,
                Title,
                Native.Raw.Windows.WS_OVERLAPPEDWINDOW | Native.Raw.Windows.WS_VISIBLE,
                Position.X, Position.Y,
                (int)Size.W, (int)Size.H,
                IntPtr.Zero,
                IntPtr.Zero,
                Native.Raw.Windows.GetModuleHandle(null),
                IntPtr.Zero
            );
            __Title    = Title;
            __Position = Position;
            __Size     = Size;

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
    public Window(WindowClass Class, string Title, Vector2I Position) : this(Class, Title, Position, new Vector2UI(800, 600)){}
    public Window(WindowClass Class, string Title = "Window") : this(Class, Title, Vector2I.Zero){}

    /// <summary>
    /// Уничтожить окно
    /// </summary>
    public void Destroy(){
        try{
            if(!Alive){ throw new Exception("Окно уже уничтожено!"); }

            Native.Raw.Windows.DestroyWindow(Handle);
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
    /// Проверяет, живое ли окно
    /// </summary>
    public void CheckAlive(){ if(!Alive){ throw new Exception("Окно не живое!"); } }
    
    /// <summary>
    /// Класс окна
    /// </summary>
    public readonly WindowClass Class;
    
    // ----------------------------------------------------------------------

    #region Заголовок

        internal string __Title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title{
            get => __Title;
            set{
                try{
                    CheckAlive();

                    string Title = value;

                    try{
                        string? ChangedTitle = OnTitleChange?.Invoke(this, value);
                        if(ChangedTitle != null){ Title = ChangedTitle; }
                    }catch(Exception e){
                        throw new Exception("Ошибка внутри ивента OnTitleChange!", e);
                    }
                    
                    if(__Title == Title){ return; }

                    if(!Native.Raw.Windows.SetWindowTextW(Handle, Title)){ throw new Exception("Произошла ошибка в SetWindowTextW!\nОшибка: " + WL.System.LastOSError()); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке заголовка окну [" + this + "]!\nЗаголовок: \"" + value + "\"", e);
                }
            }
        }

        /// <summary>
        /// Вызывается при изменении заголовка окна, (Окно, заголовок) => (изменённый заголовок)
        /// </summary>
        public event Func<Window, string, string>? OnTitleChange;

    #endregion

    #region Позиция

        internal Vector2I __Position;
        /// <summary>
        /// Позиция окна
        /// </summary>
        public Vector2I Position{
            get => __Position;
            set{
                try{
                    CheckAlive();

                    Vector2I Position = value;
                    
                    try{
                        Vector2I? ChangedPosition = OnPositionChange?.Invoke(this, value);
                        if(ChangedPosition.HasValue){ Position = ChangedPosition.Value; }
                    }catch(Exception e){
                        throw new Exception("Ошибка внутри ивента OnPositionChange!", e);
                    }
                    
                    if(__Position == Position){ return; }
                    
                    __UpdateWindowBounds(Position, __Size);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке позиции окна [" + this + "]!\nПозиция: " + value.ToPositionString(), e);
                }
            }
        }

        /// <summary>
        /// Позиция окна по X
        /// </summary>
        public int X{
            get => Position.X;
            set => Position = __Position.WithX(value);
        }
        
        /// <summary>
        /// Позиция окна по Y
        /// </summary>
        public int Y{
            get => Position.Y;
            set => Position = __Position.WithY(value);
        }
        
        /// <summary>
        /// Вызывается при изменении позиции окна, (Окно, позиция) => (изменённая позиция)
        /// </summary>
        public event Func<Window, Vector2I, Vector2I>? OnPositionChange;

    #endregion

    #region Размер

        internal Vector2UI __Size;
        /// <summary>
        /// Размер окна
        /// </summary>
        public Vector2UI Size{
            get => __Size;
            set{
                try{
                    CheckAlive();
                    
                    Vector2UI Size = value;
                    
                    try{
                        Vector2UI? ChangedSize = OnSizeChange?.Invoke(this, value);
                        if(ChangedSize.HasValue){ Size = ChangedSize.Value; }
                    }catch(Exception e){
                        throw new Exception("Ошибка внутри ивента OnSizeChange!", e);
                    }
                    
                    if(__Size == Size){ return; }

                    __UpdateWindowBounds(__Position, Size);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке размера окна [" + this + "]!\nРазмер: " + value.ToSizeString(), e);
                }
            }
        }
        
        /// <summary>
        /// Ширина окна
        /// </summary>
        public uint W{
            get => Size.W;
            set => Size = __Size.WithW(value);
        }
        
        /// <summary>
        /// Высота окна
        /// </summary>
        public uint H{
            get => Size.H;
            set => Size = __Size.WithH(value);
        }
        
        /// <summary>
        /// Вызывается при изменении размера окна, (Окно, размер) => (изменённый размер)
        /// </summary>
        public event Func<Window, Vector2UI, Vector2UI>? OnSizeChange;

    #endregion
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Вызывается при уничтожении окна
    /// </summary>
    internal void __Destroy(){
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

    /// <summary>
    /// Обновляет позицию и размер окна
    /// </summary>
    private void __UpdateWindowBounds(Vector2I Position, Vector2UI Size){
        if(!Native.Raw.Windows.SetWindowPos(Handle, IntPtr.Zero, Position.X, Position.Y, (int)Size.W, (int)Size.H, Native.Raw.Windows.SWP_NOZORDER | Native.Raw.Windows.SWP_NOACTIVATE)){
            throw new Exception("Произошла ошибка в SetWindowPos!\nОшибка: " + WL.System.LastOSError());
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
    
    // ----------------------------------------------------------------------

    public override string ToString() => "Window(\"" + Title + "\", " + Handle + ", " + Size.ToSizeString() + ", " + Position.ToPositionString() + ", " + Class + ")";

    public override bool Equals(object? Object){
        if(Object is not Window Other){ return false; }
        if(ReferenceEquals(this, Other)){ return true; }
        if(Handle == IntPtr.Zero || Other.Handle == IntPtr.Zero){ return false; }
        return Handle == Other.Handle;
    }
    
    private readonly int __ID = Interlocked.Increment(ref __NextID); private static int __NextID;
    public override int GetHashCode() => __ID;
}