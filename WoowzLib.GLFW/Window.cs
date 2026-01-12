using System.Runtime.InteropServices;

namespace WLO.GLFW;

/// <summary>
/// GLFW окно
/// </summary>
public class Window : IDisposable{
    public Window(int Width = 800, int Height = 600, string Title = "WL Window"){
        try{
            if(!WL.GLFW.Stared){ throw new Exception("GLFW не запущен!"); }

            IntPtr Title__ = Marshal.StringToHGlobalAnsi(Title);
            
            Handle = WL.GLFW.Native.glfwCreateWindow(Width, Height, Title__, IntPtr.Zero, IntPtr.Zero);
            
            Marshal.FreeHGlobal(Title__);

            if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри glfwCreateWindow!"); }
            
            __Width  = Width;
            __Height = Height;
            __Title  = Title;
            
            ID = Handle.ToInt64();

            WL.GLFW.Windows.Add(this);

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
                __Width  = Width;
                __Height = Height;

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
                    OnPosition?.Invoke(this, X, Y);   
                }catch(Exception e){
                    Console.WriteLine("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!\nX: " + X + "\nY: " + Y);
                    Console.WriteLine(e);
                }
            };
            WL.GLFW.Native.glfwSetWindowPosCallback(Handle, __PositionCallback);

            __FocusCallback = (W, Focused) => {
                
            };
            WL.GLFW.Native.glfwSetWindowFocusCallback(Handle, __FocusCallback);

            __IconifyCallback = (W, Iconify) => {
                
            };
            WL.GLFW.Native.glfwSetWindowIconifyCallback(Handle, __IconifyCallback);

            __MaximizeCallback = (W, Maximized) => {
                
            };
            WL.GLFW.Native.glfwSetWindowMaximizeCallback(Handle, __MaximizeCallback);
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
    /// Уникальный ID окна (основан на Handle)
    /// </summary>
    public long ID{ get; private set; }

    /// <summary>
    /// Уничтожено окно?
    /// </summary>
    public bool Destroyed => Handle == IntPtr.Zero;

    /// <summary>
    /// Окно должно уничтожиться? (При получении уничтожает окно если должно)
    /// </summary>
    public bool ShouldDestroy{ get; private set; }
    
    /// <summary>
    /// Проверяет, уничтожено окно или нет? (Выдаёт ошибку)
    /// </summary>
    public Window CheckDestroyed(){ if(Destroyed){ throw new Exception("Окно [" + this + "] уничтожено!"); } return this; }

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
        public event WindowEvent_Position? OnPosition;

        #region Delegates

            public delegate void WindowEvent(Window Window);
            public delegate void WindowEvent_Size(Window Window, int Width, int Height);
            public delegate void WindowEvent_Position(Window Window, int X, int Y);
            
        #endregion
        
    #endregion
        
    /// <summary>
    /// Ширина окна
    /// </summary>
    public int Width{
        get => __Width;
        set{
            try{
                if(__Width == value){ return; }
                __Width = value;
                
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowSize(Handle, __Width, __Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке ширины окну [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    private int __Width;
    
    /// <summary>
    /// Высота окна
    /// </summary>
    public int Height{
        get => __Height;
        set{
            try{
                if(__Height == value){ return; }
                __Height = value;
            
                CheckDestroyed();
                WL.GLFW.Native.glfwSetWindowSize(Handle, __Width, __Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке высоты окну [" + this + "]!\nВысота: " + value, e);
            }
        }
    }
    private int __Height;
    
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
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                if(__Title == value){ return; }
                __Title = value;

                CheckDestroyed();

                IntPtr Title__ = Marshal.StringToHGlobalAnsi(__Title);
                WL.GLFW.Native.glfwSetWindowTitle(Handle, Title__);
                Marshal.FreeHGlobal(Title__);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке названия окну [" + this + "]!\nНазвание: \"" + value + "\"");
            }
        }
    }
    private string __Title;
    
    /// <summary>
    /// Уничтожает окно
    /// </summary>
    public void Destroy(){
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
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!");
        }
    }

    /// <summary>
    /// Добавляет окно в очередь на уничтожение
    /// </summary>
    public void WaitDestroy(){
        ShouldDestroy = true;
    }
    
    #region Overwrite

        public void Dispose(){
            try{
                if(Destroyed){ return; }
                Destroy();
            }catch{ /**/ }
        }
        
        public override string ToString(){
            return "GLFW.Window(" + (Destroyed ? "Уничтожено" : ID) + ", \"" + Title + "\", " + Width + "x" + Height + ")";
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