using System.Runtime.InteropServices;

namespace WLO.GLFW;

public abstract class WindowBase : WindowContext, IDisposable{
    public abstract void Destroy();
    public abstract void Dispose();
    
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
}

/// <summary>
/// GLFW окно
/// </summary>
public class Window<TRender> : WindowBase where TRender : RenderContext, new(){
    /// <summary>
    /// Создаёт окно
    /// </summary>
    /// <param name="Width">Ширина окна</param>
    /// <param name="Height">Высота окна</param>
    /// <param name="Title">Название окна</param>
    /// <param name="TransparentBuffer">Поддержка прозрачности (прозрачный фон)</param>
    /// <param name="Resizable">Можно изменять размер окна курсором?</param>
    public Window(int Width = 800, int Height = 600, string Title = "WL Window", bool TransparentBuffer = true, bool Resizable = true){
        try{
            if(!WL.GLFW.Stared){ throw new Exception("GLFW не запущен!"); }

            IntPtr Title__ = Marshal.StringToHGlobalAnsi(Title);

            HasTransparentBuffer = TransparentBuffer;
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_TRANSPARENT_FRAMEBUFFER, TransparentBuffer ? 1 : 0);

            this.Resizable = Resizable;
            WL.GLFW.Native.glfwWindowHint(WL.GLFW.Native.GLFW_RESIZABLE, Resizable ? 1 : 0);
            
            Handle = WL.GLFW.Native.glfwCreateWindow(Width, Height, Title__, IntPtr.Zero, IntPtr.Zero);
            
            Marshal.FreeHGlobal(Title__);

            if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри glfwCreateWindow!"); }
            
            __Width  = (uint)Width;
            __Height = (uint)Height;
            __Title  = Title;

            WL.GLFW.Native.glfwGetWindowPos(Handle, out int X, out int Y);
            __X = X;
            __Y = Y;

            __Focused = WL.GLFW.Native.glfwGetWindowAttrib(Handle, WL.GLFW.Native.GLFW_FOCUSED) == 1;
            
            ID = Handle.ToInt64();

            WL.GLFW.Windows.Add(this);

            Visible = true;

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
                    OnResize?.Invoke(this, Width, Height);   
                }catch(Exception e){
                    Console.WriteLine("Произошла ошибка при вызове ивентов на изменение размера окна [" + this + "]!\nШирина: " + Width + "\nВысота: " + Height);
                    Console.WriteLine(e);
                }
            };
            WL.GLFW.Native.glfwSetWindowSizeCallback(Handle, __SizeCallback);

            __PositionCallback = (W, X, Y) => {
                __X = X;
                __Y = Y;

                try{
                    OnMove?.Invoke(this, X, Y);   
                }catch(Exception e){
                    Console.WriteLine("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!\nX: " + X + "\nY: " + Y);
                    Console.WriteLine(e);
                }
            };
            WL.GLFW.Native.glfwSetWindowPosCallback(Handle, __PositionCallback);

            __FocusCallback = (W, Focused) => {
                __Focused = Focused == 1;
                
                try{
                    OnFocus?.Invoke(this, __Focused);   
                }catch(Exception e){
                    Console.WriteLine("Произошла ошибка при вызове ивентов на изменение фокуса окна [" + this + "]!\nФокус: " + __Focused);
                    Console.WriteLine(e);
                }
            };
            WL.GLFW.Native.glfwSetWindowFocusCallback(Handle, __FocusCallback);

            __IconifyCallback = (W, Iconify) => {
                
            };
            WL.GLFW.Native.glfwSetWindowIconifyCallback(Handle, __IconifyCallback);

            __MaximizeCallback = (W, Maximized) => {
                
            };
            WL.GLFW.Native.glfwSetWindowMaximizeCallback(Handle, __MaximizeCallback);
            
            Render = new TRender();
            Render.__ConnectWindow(this);
            Render.__Start();

        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }
    private readonly WL.GLFW.Native.WindowCloseCallback   ? __CloseCallback   ;
    private readonly WL.GLFW.Native.WindowSizeCallback    ? __SizeCallback    ;
    private readonly WL.GLFW.Native.WindowPosCallback     ? __PositionCallback;
    private readonly WL.GLFW.Native.WindowFocusCallback   ? __FocusCallback   ;
    private readonly WL.GLFW.Native.WindowIconifyCallback ? __IconifyCallback ;
    private readonly WL.GLFW.Native.WindowMaximizeCallback? __MaximizeCallback;

    /// <summary>
    /// Ссылка на окно (<c>GLFWwindow*</c>)
    /// </summary>
    public IntPtr Handle{ get; private set; }

    /// <summary>
    /// Рендер окна
    /// </summary>
    public TRender Render{ get; private set; }

    /// <summary>
    /// Уничтожено окно?
    /// </summary>
    public bool Destroyed => Handle == IntPtr.Zero;
    
    /// <summary>
    /// Проверяет, уничтожено окно или нет? (Выдаёт ошибку)
    /// </summary>
    public Window<TRender> CheckDestroyed(){ if(Destroyed){ throw new Exception("Окно [" + this + "] уничтожено!"); } return this; }

    /// <summary>
    /// Завершает рендер (меняет буфер рендера с буфером экрана местами)
    /// </summary>
    public Window<TRender> FinishRender(){ WL.GLFW.Native.glfwSwapBuffers(Handle); return this; }

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

            public delegate void WindowEvent(Window<TRender> Window);
            public delegate void WindowEvent_Size(Window<TRender> Window, int Width, int Height);
            public delegate void WindowEvent_Position(Window<TRender> Window, int X, int Y);
            public delegate void WindowEvent_Focus(Window<TRender> Window, bool Focus);
            
        #endregion
        
    #endregion

    /// <summary>
    /// Окно в фокусе?
    /// </summary>
    public bool Focused{ get => __Focused; }
    private bool __Focused;

    /// <summary>
    /// Установить окно в фокус
    /// </summary>
    public Window<TRender> Focus(){
        try{
            if(Focused){ return this; }
            __Focused = true;

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
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                if(__Title == value){ return; }
                __Title = value;

                CheckDestroyed();

                IntPtr Title__ = WL.Native.MemoryString(__Title);
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
    /// Уничтожает окно
    /// </summary>
    public override void Destroy(){
        try{
            CheckDestroyed();

            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Console.WriteLine("Произошла ошибка при вызове ивентов уничтожения окна [" + this + "]!");
                Console.WriteLine(e);
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

        public override void Dispose(){
            try{
                if(Destroyed){ return; }
                Destroy();
            }catch{ /**/ }
        }
        
        public override void __UpdateContext(){
            try{
                CheckDestroyed();
                
                WL.GLFW.Native.glfwMakeContextCurrent(Handle);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке GLFW контекста у окна [" + this + "]!", e);
            }
        }
        
        public override string ToString(){
            return "GLFW.Window<" + Render + ">(" + (Destroyed ? "Уничтожено" : ID) + ", \"" + Title + "\", " + X + ":" + Y + ", " + Width + "x" + Height + ")";
        }

        public override bool Equals(object? obj){
            if(obj is not Window<TRender> other){ return false; }
            return ID == other.ID;
        }

        public override int GetHashCode(){
            return ID.GetHashCode();
        }

        public static bool operator ==(Window<TRender>? A, Window<TRender>? B){
            if(ReferenceEquals(A, B)){ return true; }
            if(A is null || B is null){ return false; }
            return A.ID == B.ID;
        }

        public static bool operator !=(Window<TRender>? A, Window<TRender>? B){
            return !(A == B);
        }

    #endregion
}