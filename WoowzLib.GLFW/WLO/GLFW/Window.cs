namespace WLO.GLFW;

/// <summary>
/// GLFW окно
/// </summary>
public class Window{
    /// <summary>
    /// Создаёт GLFW окно
    /// </summary>
    /// <param name="Width">Ширина окна</param>
    /// <param name="Height">Высота окна</param>
    /// <param name="Title">Название окна</param>
    /// <param name="TransparentBuffer">Поддержка прозрачности (прозрачный фон)</param>
    /// <param name="Resizable">Можно изменять размер окна курсором?</param>
    public Window(uint Width = 800, uint Height = 600, string Title = "WL Window", bool TransparentBuffer = true, bool Resizable = true){
        try{
            if(!WL.GLFW.Stared){ throw new Exception("GLFW не запущен!"); }

            HasTransparentBuffer = TransparentBuffer;
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_TRANSPARENT_FRAMEBUFFER, TransparentBuffer ? 1 : 0);

            this.Resizable = Resizable;
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_RESIZABLE, Resizable ? 1 : 0);

            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_CONTEXT_VERSION_MAJOR, RenderAPI.__OpenGLMajor            );
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_CONTEXT_VERSION_MINOR, RenderAPI.__OpenGLMinor            );
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_OPENGL_PROFILE       , WL.GLFW.Native.GLFW_OPENGL_CORE_PROFILE);
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_OPENGL_FORWARD_COMPAT, 1                                      );
            
            IntPtr Title__ = WL.Native.MemoryStringUTF(Title);

            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_VISIBLE, 0);
            Handle = WL.GLFW.Native.glfwCreateWindow((int)Width, (int)Height, Title__, IntPtr.Zero, IntPtr.Zero);
            
            WL.Native.Free(Title__);

            if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри glfwCreateWindow!"); }

            HandleWin = WL.GLFW.Native.glfwGetWin32Window(Handle);
            
            __Width  = Width;
            __Height = Height;
            
            __Title  = Title;
            
            __X = -1;
            __Y = -1;
            Position = new Vector2I(64, 64);

            Focused = WL.GLFW.Native.glfwGetWindowAttrib(Handle, WL.GLFW.Native.GLFW_FOCUSED) == 1;
            
            ID = Handle.ToInt64();

            WL.GLFW.Windows.Add(this);

            __Visible = false; Visible = true ;

            __VSync = true; VSync = false;

            __CloseCallback = (W) => {
                try{
                    OnClose?.Invoke(this);
                    
                    if(!DisableOnClose){ if(!ShouldDestroy){ WaitDestroy(); } }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при закрытии окна [" + this + "], через крестик!", e);
                }
            };
            WL.GLFW.Native.glfwSetWindowCloseCallback(Handle, __CloseCallback);

            __SizeCallback = (W, Width, Height) => {
                __Width  = (uint)Width;
                __Height = (uint)Height;

                try{
                    OnResize?.Invoke(this, __Width, __Height);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение размера окна [" + this + "]!\nШирина: " + Width + "\nВысота: " + Height, e);
                }
            };
            WL.GLFW.Native.glfwSetWindowSizeCallback(Handle, __SizeCallback);

            __PositionCallback = (W, X, Y) => {
                __X = X;
                __Y = Y;

                try{
                    OnMove?.Invoke(this, X, Y);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!\nX: " + X + "\nY: " + Y, e);
                }
            };
            WL.GLFW.Native.glfwSetWindowPosCallback(Handle, __PositionCallback);

            __FocusCallback = (W, Focused) => {
                this.Focused = Focused == 1;
                
                try{
                    OnFocus?.Invoke(this, this.Focused);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение фокуса окна [" + this + "]!\nФокус: " + this.Focused, e);
                }
            };
            WL.GLFW.Native.glfwSetWindowFocusCallback(Handle, __FocusCallback);

            __IconifyCallback = (W, Iconify) => {
                
            };
            WL.GLFW.Native.glfwSetWindowIconifyCallback(Handle, __IconifyCallback);

            __MaximizeCallback = (W, Maximized) => {
                
            };
            WL.GLFW.Native.glfwSetWindowMaximizeCallback(Handle, __MaximizeCallback);

            Drawable = new DrawableWindow(HandleWin);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GLFW окна [" + this + "]!", e);
        }
    }
    private readonly WL.GLFW.Native.WindowCloseCallback   ? __CloseCallback   ;
    private readonly WL.GLFW.Native.WindowSizeCallback    ? __SizeCallback    ;
    private readonly WL.GLFW.Native.WindowPosCallback     ? __PositionCallback;
    private readonly WL.GLFW.Native.WindowFocusCallback   ? __FocusCallback   ;
    private readonly WL.GLFW.Native.WindowIconifyCallback ? __IconifyCallback ;
    private readonly WL.GLFW.Native.WindowMaximizeCallback? __MaximizeCallback;

    /// <summary>
    /// Уникальный ID окна (основан на Handle)
    /// </summary>
    public long ID{ get; protected set; }
    
    /// <summary>
    /// Ссылка на окно (<c>GLFWwindow*</c>)
    /// </summary>
    public IntPtr Handle{ get; private set; }

    public IntPtr HandleWin;
    
    /// <summary>
    /// Уничтожено окно?
    /// </summary>
    public bool Destroyed => Handle == IntPtr.Zero;
    
    /// <summary>
    /// Проверяет, уничтожено окно или нет? (Выдаёт ошибку)
    /// </summary>
    public Window CheckDestroyed(){ return Destroyed ? throw new Exception("Окно [" + this + "] уничтожено!") : this; }

    public DrawableWindow Drawable;
    
    /// <summary>
    /// Завершает рендер (меняет буфер рендера с буфером экрана местами)
    /// </summary>
    public Window StopRender(){
        try{
            CheckDestroyed(); WL.GLFW.Native.glfwSwapBuffers(Handle);   
        }catch(Exception e){
            throw new Exception("Произошла ошибка при завершении рендера у окна [" + this + "]!", e);
        }
        
        return this;
    }

    /// <summary>
    /// Есть поддержка прозрачности?
    /// </summary>
    public bool HasTransparentBuffer{ get; private set; }

    /// <summary>
    /// Можно изменять размер курсором?
    /// </summary>
    public bool Resizable{ get; private set; }

    #region Events

        /// <summary>
        /// Вызывается перед уничтожением окна
        /// </summary>
        public event WindowEvent? OnDestroy;
        
        /// <summary>
        /// Вызывается перед закрытием окна (на крестик)
        /// </summary>
        public event WindowEvent? OnClose;
        /// <summary>
        /// Отключает дефолтный ивент OnClose
        /// </summary>
        public bool DisableOnClose;

        /// <summary>
        /// Вызывается при изменении размера окна
        /// </summary>
        public event WindowEvent_Size? OnResize;
        
        /// <summary>
        /// Вызывается при изменении позиции окна
        /// </summary>
        public event WindowEvent_Position? OnMove;
        
        /// <summary>
        /// Вызывается при изменении фокуса окна
        /// </summary>
        public event WindowEvent_Focus? OnFocus;

        #region Delegates

            public delegate void WindowEvent(Window Window);
            public delegate void WindowEvent_Size(Window Window, uint Width, uint Height);
            public delegate void WindowEvent_Position(Window Window, int X, int Y);
            public delegate void WindowEvent_Focus(Window Window, bool Focus);
            
        #endregion
        
    #endregion

    /// <summary>
    /// Окно в фокусе?
    /// </summary>
    public bool Focused{ get; private set; }

    /// <summary>
    /// Установить окно в фокус
    /// </summary>
    public Window Focus(){
        try{
            if(Focused){ return this; }
            Focused = true;

            CheckDestroyed();
            WL.GLFW.Native.glfwFocusWindow(Handle);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке фокуса на окно [" + this + "]!", e);
        }

        return this;
    }
    
    /// <summary>
    /// Ширина окна
    /// </summary>
    public uint Width{
        get => __Width;
        set{
            try{
                if(__Width == value){ return; }
                //if(value < ) ДОБАВИТЬ ЛИМИТЫ РАЗМЕРА ОКНА glfwSetWindowSizeLimits
                
                __Width = value;
                
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowSize(Handle, (int)__Width, (int)__Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке ширины окну [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    private uint __Width;
    
    /// <summary>
    /// Высота окна
    /// </summary>
    public uint Height{
        get => __Height;
        set{
            try{
                if(__Height == value){ return; }
                __Height = value;
            
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowSize(Handle, (int)__Width, (int)__Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке высоты окну [" + this + "]!\nВысота: " + value, e);
            }
        }
    }
    private uint __Height;
    
    /// <summary>
    /// Размер окна
    /// </summary>
    public Vector2U Size{
        get => new Vector2U(__Width, __Height);
        set{
            try{
                if(__Width == value.X && __Height == value.Y){ return; }
                __Width  = value.X;
                __Height = value.Y;
            
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowSize(Handle, (int)__Width, (int)__Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке размера окну [" + this + "]!\nРазмер: " + value, e);
            }
        }
    }
    
    /// <summary>
    /// Позиция окна по X
    /// </summary>
    public int X{
        get => __X;
        set{
            try{
                if(__X == value){ return; }
                __X = value;
                
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowPos(Handle, __X, __Y);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции по X у окна [" + this + "]!\nX: " + value, e);
            }
        }
    }
    private int __X;
    
    /// <summary>
    /// Позиция окна по Y
    /// </summary>
    public int Y{
        get => __Y;
        set{
            try{
                if(__Y == value){ return; }
                __Y = value;
            
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowPos(Handle, __X, __Y);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции по Y у окна [" + this + "]!\nY: " + value, e);
            }
        }
    }
    private int __Y;

    /// <summary>
    /// Позиция окна
    /// </summary>
    public Vector2I Position{
        get => new Vector2I(__X, __Y);
        set{
            try{
                if(__X == value.X && __Y == value.Y){ return; }
                __X = value.X;
                __Y = value.Y;
                
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowPos(Handle, __X, __Y);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции у окна [" + this + "]!\nПозиция: " + value, e);
            }
        }
    }

    /// <summary>
    /// Позиция и размер окна
    /// </summary>
    public RectI Rect{
        get => new RectI(__X, __Y, (int)__Width, (int)__Height);
        set{
            try{
                Position = new Vector2I(      value.X    ,       value.Y     );
                Size     = new Vector2U((uint)value.Width, (uint)value.Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции и размера у окна [" + this + "]!\nRect: " + value, e);
            }
        }
    }

    /// <summary>
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                if(__Title == value){ return; }
                __Title = value;

                CheckDestroyed();

                IntPtr Title__ = WL.Native.MemoryStringUTF(__Title);
                WL.GLFW.Native.glfwSetWindowTitle(Handle, Title__);
                WL.Native.Free(Title__);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке названия окну [" + this + "]!\nНазвание: \"" + value + "\"", e);
            }
        }
    }
    private string __Title;

    /// <summary>
    /// Видно окно?
    /// </summary>
    public bool Visible{
        get => __Visible;
        set{
            try{
                if(__Visible == value){ return; }
                __Visible = value;

                CheckDestroyed();

                if(__Visible){
                    WL.GLFW.Native.glfwShowWindow(Handle);
                }else{
                    WL.GLFW.Native.glfwHideWindow(Handle);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке видимости у окна [" + this + "]!\nВидимость: " + value, e);
            }
        }
    }
    private bool __Visible;

    /// <summary>
    /// Ограничение по FPS (на 60, ограничивает StopRender())
    /// </summary>
    public bool VSync{
        get => __VSync;
        set{
            try{
                if(__VSync == value){ return; }
                __VSync = value;
                
                WL.GLFW.Native.glfwSwapInterval(__VSync ? 1 : 0);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке VSync у окна [" + this + "]!\nVSync: " + value, e);
            }
        }
    }
    private bool __VSync;
    
    /// <summary>
    /// Окно должно уничтожиться? (При получении уничтожает окно если должно)
    /// </summary>
    public bool ShouldDestroy{ get; private set; }
    
    /// <summary>
    /// Добавляет окно в очередь на уничтожение
    /// </summary>
    public void WaitDestroy(){
        ShouldDestroy = true;
    }

    /// <summary>
    /// Уничтожает окно
    /// </summary>
    public void Destroy(){
        try{
            CheckDestroyed();

            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов уничтожения окна [" + this + "]!", e);
            }
            
            WL.GLFW.Native.glfwDestroyWindow(Handle);
            WL.GLFW.Windows.Remove(this);
            
            Handle = IntPtr.Zero;
            ID     = -1;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!", e);
        }
    }
    
    #region Overwride
    
        public override string ToString(){
            return "GLFW.Window(\"" + Title + "\", " + X + ":" + Y + ", " + Width + "x" + Height + ", " + (Destroyed ? "Уничтожено" : ID) + ")";
        }

        public override bool Equals(object? obj){
            if(obj is not Window other){ return false; }
            return ID == other.ID;
        }

        public override int GetHashCode(){
            return ID.GetHashCode();
        }

        public static bool operator ==(Window? A, Window? B){
            if(ReferenceEquals(A, B)){ return true; }
            if(A is null || B is null){ return false; }
            return A.ID == B.ID;
        }

        public static bool operator !=(Window? A, Window? B){
            return !(A == B);
        }

    #endregion
}