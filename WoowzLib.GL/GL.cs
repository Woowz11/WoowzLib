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

        public static void __StartWGL(){
            try{
                if(WGLStarted){ return; } WGLStarted = true;
                
                Native.glUniform1f          = Native.WGLFunction<Native.D_glUniform1f         >("glUniform1f"         );
                Native.glUniform2f          = Native.WGLFunction<Native.D_glUniform2f         >("glUniform2f"         );
                Native.glUniform3f          = Native.WGLFunction<Native.D_glUniform3f         >("glUniform3f"         );
                Native.glUniform4f          = Native.WGLFunction<Native.D_glUniform4f         >("glUniform4f"         );
                Native.glUseProgram         = Native.WGLFunction<Native.D_glUseProgram        >("glUseProgram"        );
                Native.glGetShaderiv        = Native.WGLFunction<Native.D_glGetShaderiv       >("glGetShaderiv"       );
                Native.glLinkProgram        = Native.WGLFunction<Native.D_glLinkProgram       >("glLinkProgram"       );
                Native.glCreateShader       = Native.WGLFunction<Native.D_glCreateShader      >("glCreateShader"      );
                Native.glShaderSource       = Native.WGLFunction<Native.D_glShaderSource      >("glShaderSource"      );
                Native.glAttachShader       = Native.WGLFunction<Native.D_glAttachShader      >("glAttachShader"      );
                Native.glDeleteShader       = Native.WGLFunction<Native.D_glDeleteShader      >("glDeleteShader"      );
                Native.glCompileShader      = Native.WGLFunction<Native.D_glCompileShader     >("glCompileShader"     );
                Native.glCreateProgram      = Native.WGLFunction<Native.D_glCreateProgram     >("glCreateProgram"     );
                Native.glGetShaderInfoLog   = Native.WGLFunction<Native.D_glGetShaderInfoLog  >("glGetShaderInfoLog"  );
                Native.glUniformMatrix4fv   = Native.WGLFunction<Native.D_glUniformMatrix4fv  >("glUniformMatrix4fv"  );
                Native.glGetUniformLocation = Native.WGLFunction<Native.D_glGetUniformLocation>("glGetUniformLocation");
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке OpenGL функций через WGL!", e);
            }
        }
        private static bool WGLStarted;
        
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
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate uint D_glCreateShader(uint type);
            public static D_glCreateShader glCreateShader = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glShaderSource(uint shader, int count, string[] strings, int[]? lengths);
            public static D_glShaderSource glShaderSource = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glCompileShader(uint shader);
            public static D_glCompileShader glCompileShader = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glGetShaderiv(uint shader, uint pname, out int param);
            public static D_glGetShaderiv glGetShaderiv = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr D_glGetShaderInfoLog(uint shader, int maxLength, out int length, IntPtr infoLog);
            public static D_glGetShaderInfoLog glGetShaderInfoLog = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDeleteShader(uint shader);
            public static D_glDeleteShader glDeleteShader = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate uint D_glCreateProgram();
            public static D_glCreateProgram glCreateProgram = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glAttachShader(uint program, uint shader);
            public static D_glAttachShader glAttachShader = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glLinkProgram(uint program);
            public static D_glLinkProgram glLinkProgram = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUseProgram(uint program);
            public static D_glUseProgram glUseProgram = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate int D_glGetUniformLocation(uint program, string name);
            public static D_glGetUniformLocation glGetUniformLocation = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUniform1f(int location, float v0);
            public static D_glUniform1f glUniform1f = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUniform2f(int location, float v0, float v1);
            public static D_glUniform2f glUniform2f = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUniform3f(int location, float v0, float v1, float v2);
            public static D_glUniform3f glUniform3f = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUniform4f(int location, float v0, float v1, float v2, float v3);
            public static D_glUniform4f glUniform4f = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glUniformMatrix4fv(int location, int count, bool transpose, float[] value);
            public static D_glUniformMatrix4fv glUniformMatrix4fv = null!;

            public const uint GL_COLOR_BUFFER_BIT   = 0x00004000;
            public const uint GL_DEPTH_BUFFER_BIT   = 0x00000100;
            public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
            public const uint GL_DEPTH_TEST         = 0x0B71;
            public const uint GL_LESS               = 0x0201;
            public const uint GL_VERSION            = 0x1F02;
            public const uint GL_VERTEX_SHADER      = 0x8B31;
            public const uint GL_FRAGMENT_SHADER    = 0x8B30;
            public const uint GL_COMPILE_STATUS     = 0x8B81;
            public const uint GL_LINK_STATUS        = 0x8B82;
            public const uint GL_INFO_LOG_LENGTH    = 0x8B84;

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