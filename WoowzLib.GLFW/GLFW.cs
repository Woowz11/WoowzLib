using System.Runtime.InteropServices;
using File = WLO.File;

namespace WL{
    [WoowzLibModule(30)]
    public static class GLFW{
        static GLFW(){
            WL.WoowzLib.StopEvent(() => __Destroy(true));
        }
        
        /// <summary>
        /// Текущий glfw3.dll
        /// </summary>
        private static File? DLL;

        /// <summary>
        /// Установлен GLFW?
        /// </summary>
        public static bool Stared => DLL != null;
        
        /// <summary>
        /// Запуск GLFW
        /// </summary>
        public static void Start(){
            try{
                if(Stared){ throw new Exception("GLFW уже был загружен!"); }

                DLL = WL.Explorer.Resources.Load("WoowzLib.GLFW.glfw3.dll", typeof(WL.GLFW).Assembly);
                WL.Native.Load(DLL);

                Native.glfwInit               = Marshal.GetDelegateForFunctionPointer<Native.D_glfwInit              >(WL.Native.Function(DLL, "glfwInit"              ));
                Native.glfwTerminate          = Marshal.GetDelegateForFunctionPointer<Native.D_glfwTerminate         >(WL.Native.Function(DLL, "glfwTerminate"         ));
                Native.glfwCreateWindow       = Marshal.GetDelegateForFunctionPointer<Native.D_glfwCreateWindow      >(WL.Native.Function(DLL, "glfwCreateWindow"      ));
                Native.glfwMakeContextCurrent = Marshal.GetDelegateForFunctionPointer<Native.D_glfwMakeContextCurrent>(WL.Native.Function(DLL, "glfwMakeContextCurrent"));
                Native.glfwShowWindow         = Marshal.GetDelegateForFunctionPointer<Native.D_glfwShowWindow        >(WL.Native.Function(DLL, "glfwShowWindow"        ));
                Native.glfwPollEvents         = Marshal.GetDelegateForFunctionPointer<Native.D_glfwPollEvents        >(WL.Native.Function(DLL, "glfwPollEvents"        ));
                Native.glfwWindowShouldClose  = Marshal.GetDelegateForFunctionPointer<Native.D_glfwWindowShouldClose >(WL.Native.Function(DLL, "glfwWindowShouldClose" ));
                Native.glfwSetWindowSize      = Marshal.GetDelegateForFunctionPointer<Native.D_glfwSetWindowSize     >(WL.Native.Function(DLL, "glfwSetWindowSize"     ));
                Native.glfwSetWindowTitle     = Marshal.GetDelegateForFunctionPointer<Native.D_glfwSetWindowTitle    >(WL.Native.Function(DLL, "glfwSetWindowTitle"    ));
                
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
                if(!Stared){ throw new Exception("GLFW и не был загружен!"); }
                __Destroy(false);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при остановке GLFW!", e);
            }
        }

        private static void __Destroy(bool Warn){
            try{
                if(!Stared){ return; }
                
                Native.glfwTerminate?.Invoke();
                
                WL.Native.Unload(DLL);
                DLL = null;
                if(Warn){ Console.WriteLine("Авто-остановка GLFW!"); }
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
            public delegate void D_glfwSetWindowTitle(IntPtr window, IntPtr title);
            public static D_glfwSetWindowTitle glfwSetWindowTitle = null!;
        }
    }
}