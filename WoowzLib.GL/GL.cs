using System.Runtime.InteropServices;

namespace WL{
    [WLModule(35)]
    public static class GL{
        static GL(){
            DLL = WL.Native.LoadSystem("opengl32.dll");
            
            Native.glClear           = WL.Native.DelegateFunction<Native.D_glClear          >("glClear"           ,DLL);
            Native.glEnable          = WL.Native.DelegateFunction<Native.D_glEnable         >("glEnable"          ,DLL);
            Native.glDisable         = WL.Native.DelegateFunction<Native.D_glDisable        >("glDisable"         ,DLL);
            Native.glViewport        = WL.Native.DelegateFunction<Native.D_glViewport       >("glViewport"        ,DLL);
            Native.glDepthFunc       = WL.Native.DelegateFunction<Native.D_glDepthFunc      >("glDepthFunc"       ,DLL);
            Native.glGetString       = WL.Native.DelegateFunction<Native.D_glGetString      >("glGetString"       ,DLL);
            Native.glClearColor      = WL.Native.DelegateFunction<Native.D_glClearColor     >("glClearColor"      ,DLL);
            Native.wglGetProcAddress = WL.Native.DelegateFunction<Native.D_wglGetProcAddress>("wglGetProcAddress" ,DLL);
        }
        
        // <summary>
        /// Текущий opengl32.dll
        /// </summary>
        private static IntPtr DLL;

        public static class Native{
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glClearColor(float r, float g, float b, float a);
            public static D_glClearColor glClearColor = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glClear(uint mask);
            public static D_glClear glClear = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glViewport(int x, int y, int width, int height);
            public static D_glViewport glViewport = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glEnable(uint cap);
            public static D_glEnable glEnable = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDisable(uint cap);
            public static D_glDisable glDisable = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDepthFunc(uint func);
            public static D_glDepthFunc glDepthFunc = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr D_glGetString(uint name);
            public static D_glGetString glGetString = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr D_wglGetProcAddress(string name);
            public static D_wglGetProcAddress wglGetProcAddress = null!;

            public const uint GL_COLOR_BUFFER_BIT   = 0x00004000;
            public const uint GL_DEPTH_BUFFER_BIT   = 0x00000100;
            public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
            public const uint GL_DEPTH_TEST         = 0x0B71;
            public const uint GL_LESS               = 0x0201;
            public const uint GL_VERSION            = 0x1F02;

            /// <summary>
            /// Получает функцию из OpenGL и возвращает её в виде C# функции (WGL)
            /// </summary>
            /// <param name="Name">Функция из OpenGL [<c>"glGenBuffers"</c>]</param>
            /// <typeparam name="D">Тип функции (точно совпадает с её параметрами и возвращаемым значением)</typeparam>
            /// <returns>Функция которую можно вызвать как C# функцию</returns>
            public static D WGLFunction<D>(string Name) where D : Delegate{
                try{
                    IntPtr Link = wglGetProcAddress(Name);

                    if(Link == IntPtr.Zero){ throw new Exception(WL.Native.Error_FunctionNotFound); }

                    return Marshal.GetDelegateForFunctionPointer<D>(Link);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при загрузке функции из OpenGL!\nФункция: " + Name, e);
                }
            }
        }
    }
}