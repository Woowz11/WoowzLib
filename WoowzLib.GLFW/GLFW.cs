using System.Runtime.InteropServices;
using WLO;
using WLO.GLFW;
using File = WLO.File;

namespace WL{
    [WLModule(30, 0)]
    public static class GLFW{
        static GLFW(){
            WL.WoowzLib.OnStop += () => __Destroy(true);
        }

        /// <summary>
        /// Открытые окна
        /// </summary>
        internal static readonly HashSet<WindowBase> Windows = [];
        
        /// <summary>
        /// Текущий glfw3.dll
        /// </summary>
        private static File? DLL;

        /// <summary>
        /// Установлен GLFW?
        /// </summary>
        public static bool Stared => DLL != null;

        /// <summary>
        /// Уничтожен GLFW?
        /// </summary>
        public static bool Destroyed => !Stared;
        
        /// <summary>
        /// Запуск GLFW
        /// </summary>
        public static void Start(){
            try{
                if(Stared){ throw new Exception("GLFW уже был загружен!"); }

                DLL = WL.Explorer.Resources.Load("WoowzLib.glfw3.dll", typeof(WL.GLFW).Assembly);
                WL.Native.Load(DLL);

                Native.glfwInit                      = WL.Native.DelegateFunction<Native.D_glfwInit                     >("glfwInit"                     ,DLL);
                Native.glfwTerminate                 = WL.Native.DelegateFunction<Native.D_glfwTerminate                >("glfwTerminate"                ,DLL);
                Native.glfwWindowHint                = WL.Native.DelegateFunction<Native.D_glfwWindowHint               >("glfwWindowHint"               ,DLL);
                Native.glfwShowWindow                = WL.Native.DelegateFunction<Native.D_glfwShowWindow               >("glfwShowWindow"               ,DLL);
                Native.glfwHideWindow                = WL.Native.DelegateFunction<Native.D_glfwHideWindow               >("glfwHideWindow"               ,DLL);
                Native.glfwPollEvents                = WL.Native.DelegateFunction<Native.D_glfwPollEvents               >("glfwPollEvents"               ,DLL);
                Native.glfwFocusWindow               = WL.Native.DelegateFunction<Native.D_glfwFocusWindow              >("glfwFocusWindow"              ,DLL);
                Native.glfwSwapBuffers               = WL.Native.DelegateFunction<Native.D_glfwSwapBuffers              >("glfwSwapBuffers"              ,DLL);
                Native.glfwCreateWindow              = WL.Native.DelegateFunction<Native.D_glfwCreateWindow             >("glfwCreateWindow"             ,DLL);
                Native.glfwSetWindowPos              = WL.Native.DelegateFunction<Native.D_glfwSetWindowPos             >("glfwSetWindowPos"             ,DLL);
                Native.glfwGetWindowPos              = WL.Native.DelegateFunction<Native.D_glfwGetWindowPos             >("glfwGetWindowPos"             ,DLL);
                Native.glfwSwapInterval              = WL.Native.DelegateFunction<Native.D_glfwSwapInterval             >("glfwSwapInterval"             ,DLL);
                Native.glfwSetWindowSize             = WL.Native.DelegateFunction<Native.D_glfwSetWindowSize            >("glfwSetWindowSize"            ,DLL);
                Native.glfwDestroyWindow             = WL.Native.DelegateFunction<Native.D_glfwDestroyWindow            >("glfwDestroyWindow"            ,DLL);
                Native.glfwSetWindowTitle            = WL.Native.DelegateFunction<Native.D_glfwSetWindowTitle           >("glfwSetWindowTitle"           ,DLL);
                Native.glfwSetWindowAttrib           = WL.Native.DelegateFunction<Native.D_glfwSetWindowAttrib          >("glfwSetWindowAttrib"          ,DLL);
                Native.glfwGetWindowAttrib           = WL.Native.DelegateFunction<Native.D_glfwGetWindowAttrib          >("glfwGetWindowAttrib"          ,DLL);
                Native.glfwWindowShouldClose         = WL.Native.DelegateFunction<Native.D_glfwWindowShouldClose        >("glfwWindowShouldClose"        ,DLL);
                Native.glfwMakeContextCurrent        = WL.Native.DelegateFunction<Native.D_glfwMakeContextCurrent       >("glfwMakeContextCurrent"       ,DLL);
                Native.glfwSetWindowPosCallback      = WL.Native.DelegateFunction<Native.D_glfwSetWindowPosCallback     >("glfwSetWindowPosCallback"     ,DLL);
                Native.glfwSetWindowSizeCallback     = WL.Native.DelegateFunction<Native.D_glfwSetWindowSizeCallback    >("glfwSetWindowSizeCallback"    ,DLL);
                Native.glfwSetWindowCloseCallback    = WL.Native.DelegateFunction<Native.D_glfwSetWindowCloseCallback   >("glfwSetWindowCloseCallback"   ,DLL);
                Native.glfwSetWindowFocusCallback    = WL.Native.DelegateFunction<Native.D_glfwSetWindowFocusCallback   >("glfwSetWindowFocusCallback"   ,DLL);
                Native.glfwSetWindowIconifyCallback  = WL.Native.DelegateFunction<Native.D_glfwSetWindowIconifyCallback >("glfwSetWindowIconifyCallback" ,DLL);
                Native.glfwSetWindowMaximizeCallback = WL.Native.DelegateFunction<Native.D_glfwSetWindowMaximizeCallback>("glfwSetWindowMaximizeCallback",DLL);
                
                int Result = Native.glfwInit();
                if(Result == 0){ throw new Exception("glfwInit вернул 0!"); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске GLFW!", e);
            }
        }

        /// <summary>
        /// Остановка GLFW
        /// </summary>
        public static void Stop(){
            try{
                if(Destroyed){ throw new Exception("GLFW и не был загружен!"); }
                __Destroy(false);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при остановке GLFW!", e);
            }
        }

        /// <summary>
        /// Обновление GLFW (Нужно вызывать каждый кадр (внутри while))
        /// </summary>
        public static void Tick(){
            try{
                Native.glfwPollEvents();
                foreach(WindowBase window in Windows.ToArray()){
                    if(window.ShouldDestroy){ window.Destroy(); }
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обновлении GLFW!", e);
            }
        }
        
        private static void __Destroy(bool Warn){
            try{
                if(Destroyed){ return; }

                if(Windows.Count > 0){ Logger.Warn("Оставшиеся окна были закрыты через WL.GLFW.Stop()!"); }

                foreach(WindowBase Window in Windows.ToArray()){
                    Window.Destroy();
                }
                Windows.Clear();
                
                Native.glfwTerminate.Invoke();
                
                WL.Native.Unload(DLL);
                DLL = null;
                
                if(Warn){ Logger.Warn("Авто-остановка GLFW!"); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при базовой остановке GLFW!", e);
            }
        }
        
        public static class Native{
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int D_glfwInit();
            public static D_glfwInit glfwInit = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwTerminate();
            public static D_glfwTerminate glfwTerminate = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr D_glfwCreateWindow(int width, int height, IntPtr title, IntPtr monitor, IntPtr share);
            public static D_glfwCreateWindow glfwCreateWindow = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwMakeContextCurrent(IntPtr window);
            public static D_glfwMakeContextCurrent glfwMakeContextCurrent = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwShowWindow(IntPtr window);
            public static D_glfwShowWindow glfwShowWindow = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwPollEvents();
            public static D_glfwPollEvents glfwPollEvents = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int D_glfwWindowShouldClose(IntPtr window);
            public static D_glfwWindowShouldClose glfwWindowShouldClose = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowSize(IntPtr window, int width, int height);
            public static D_glfwSetWindowSize glfwSetWindowSize = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowPos(IntPtr window, int x, int y);
            public static D_glfwSetWindowPos glfwSetWindowPos = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowTitle(IntPtr window, IntPtr title);
            public static D_glfwSetWindowTitle glfwSetWindowTitle = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwDestroyWindow(IntPtr window);
            public static D_glfwDestroyWindow glfwDestroyWindow = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowCloseCallback(IntPtr window);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowCloseCallback(IntPtr window, WindowCloseCallback cb);
            public static D_glfwSetWindowCloseCallback glfwSetWindowCloseCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowSizeCallback(IntPtr window, int width, int height);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowSizeCallback(IntPtr window, WindowSizeCallback cb);
            public static D_glfwSetWindowSizeCallback glfwSetWindowSizeCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowPosCallback(IntPtr window, int xpos, int ypos);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowPosCallback(IntPtr window, WindowPosCallback cb);
            public static D_glfwSetWindowPosCallback glfwSetWindowPosCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowFocusCallback(IntPtr window, int focused);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowFocusCallback(IntPtr window, WindowFocusCallback cb);
            public static D_glfwSetWindowFocusCallback glfwSetWindowFocusCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowIconifyCallback(IntPtr window, int iconified);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowIconifyCallback(IntPtr window, WindowIconifyCallback cb);
            public static D_glfwSetWindowIconifyCallback glfwSetWindowIconifyCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void WindowMaximizeCallback(IntPtr window, int maximized);
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowMaximizeCallback(IntPtr window, WindowMaximizeCallback cb);
            public static D_glfwSetWindowMaximizeCallback glfwSetWindowMaximizeCallback = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwFocusWindow(IntPtr window);
            public static D_glfwFocusWindow glfwFocusWindow = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSwapBuffers(IntPtr window);
            public static D_glfwSwapBuffers glfwSwapBuffers = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwGetWindowPos(IntPtr window, out int xpos, out int ypos);
            public static D_glfwGetWindowPos glfwGetWindowPos = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int D_glfwGetWindowAttrib(IntPtr window, int attrib);
            public static D_glfwGetWindowAttrib glfwGetWindowAttrib = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSetWindowAttrib(IntPtr window, int attrib, int value);
            public static D_glfwSetWindowAttrib glfwSetWindowAttrib = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwWindowHint(int hint, int value);
            public static D_glfwWindowHint glfwWindowHint = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwHideWindow(IntPtr window);
            public static D_glfwHideWindow glfwHideWindow = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glfwSwapInterval(int interval);
            public static D_glfwSwapInterval glfwSwapInterval = null!;
            
            public const int GLFW_FOCUSED                 = 0x00020001;
            public const int GLFW_RESIZABLE               = 0x00020003;
            public const int GLFW_DECORATED               = 0x00020005;
            public const int GLFW_VISIBLE                 = 0x00020004;
            public const int GLFW_FLOATING                = 0x00020007;
            public const int GLFW_TRANSPARENT_FRAMEBUFFER = 0x0002000A;
            public const int GLFW_RED_BITS                = 0x00021001;
            public const int GLFW_GREEN_BITS              = 0x00021002;
            public const int GLFW_BLUE_BITS               = 0x00021003;
            public const int GLFW_ALPHA_BITS              = 0x00021004;
            public const int GLFW_DEPTH_BITS              = 0x00021005;
            public const int GLFW_STENCIL_BITS            = 0x00021006;
            public const int GLFW_SAMPLES                 = 0x0002100D;
            public const int GLFW_DOUBLEBUFFER            = 0x0002100A;
            public const int GLFW_CONTEXT_VERSION_MAJOR   = 0x00022002;
            public const int GLFW_CONTEXT_VERSION_MINOR   = 0x00022003;
            public const int GLFW_OPENGL_PROFILE          = 0x00022008;
            public const int GLFW_OPENGL_FORWARD_COMPAT   = 0x00022006;
            public const int GLFW_OPENGL_CORE_PROFILE     = 0x00032001;
            public const int GLFW_OPENGL_COMPAT_PROFILE   = 0x00032002;
        }
    }
}