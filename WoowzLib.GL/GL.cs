using System.Runtime.InteropServices;
using WLO;

namespace WL{
    /// <summary>
    /// Типы значений для GL
    /// </summary>
    public enum GLValue : uint{
        Float         = WL.GL.Native.GL_FLOAT,
        Double        = WL.GL.Native.GL_DOUBLE,
        Byte          = WL.GL.Native.GL_BYTE,
        UnsignedByte  = WL.GL.Native.GL_UNSIGNED_BYTE,
        Short         = WL.GL.Native.GL_SHORT,
        UnsignedShort = WL.GL.Native.GL_UNSIGNED_SHORT,
        Int           = WL.GL.Native.GL_INT,
        UnsignedInt   = WL.GL.Native.GL_UNSIGNED_INT
    }
    
    [WLModule(10, 0)]
    public static class GL{
        static GL(){
            DLL = WL.Native.LoadSystem("opengl32.dll");
            
            Native.glClear           = WL.Native.DelegateFunction<Native.D_glClear          >("glClear"           ,DLL);
            Native.glEnable          = WL.Native.DelegateFunction<Native.D_glEnable         >("glEnable"          ,DLL);
            Native.glDisable         = WL.Native.DelegateFunction<Native.D_glDisable        >("glDisable"         ,DLL);
            Native.glViewport        = WL.Native.DelegateFunction<Native.D_glViewport       >("glViewport"        ,DLL);
            Native.glDepthFunc       = WL.Native.DelegateFunction<Native.D_glDepthFunc      >("glDepthFunc"       ,DLL);
            Native.glGetFloatv       = WL.Native.DelegateFunction<Native.D_glGetFloatv      >("glGetFloatv"       ,DLL);
            Native.glGetString       = WL.Native.DelegateFunction<Native.D_glGetString      >("glGetString"       ,DLL);
            Native.glLineWidth       = WL.Native.DelegateFunction<Native.D_glLineWidth      >("glLineWidth"       ,DLL);
            Native.glClearColor      = WL.Native.DelegateFunction<Native.D_glClearColor     >("glClearColor"      ,DLL);
            Native.wglGetProcAddress = WL.Native.DelegateFunction<Native.D_wglGetProcAddress>("wglGetProcAddress" ,DLL);
        }

        public static void __StartWGL(){
            try{
                if(WGLStarted){ return; } WGLStarted = true;

                if(Debug.LogMain){ Logger.Info("Инициализация остальных GL функций через WGL!"); }

                Native.glUniform1f                = Native.WGLFunction<Native.D_glUniform1f               >("glUniform1f"               );
                Native.glUniform2f                = Native.WGLFunction<Native.D_glUniform2f               >("glUniform2f"               );
                Native.glUniform3f                = Native.WGLFunction<Native.D_glUniform3f               >("glUniform3f"               );
                Native.glUniform4f                = Native.WGLFunction<Native.D_glUniform4f               >("glUniform4f"               );
                Native.glGenBuffers               = Native.WGLFunction<Native.D_glGenBuffers              >("glGenBuffers"              );
                Native.glBindBuffer               = Native.WGLFunction<Native.D_glBindBuffer              >("glBindBuffer"              );
                Native.glBufferData               = Native.WGLFunction<Native.D_glBufferData              >("glBufferData"              );
                Native.glUseProgram               = Native.WGLFunction<Native.D_glUseProgram              >("glUseProgram"              );
                Native.glDrawArrays               = Native.WGLFunction<Native.D_glDrawArrays              >("glDrawArrays"              );
                Native.glObjectLabel              = Native.WGLFunction<Native.D_glObjectLabel             >("glObjectLabel"             );
                Native.glGetShaderiv              = Native.WGLFunction<Native.D_glGetShaderiv             >("glGetShaderiv"             );
                Native.glLinkProgram              = Native.WGLFunction<Native.D_glLinkProgram             >("glLinkProgram"             );
                Native.glDrawElements             = Native.WGLFunction<Native.D_glDrawElements            >("glDrawElements"            );
                Native.glCreateShader             = Native.WGLFunction<Native.D_glCreateShader            >("glCreateShader"            );
                Native.glShaderSource             = Native.WGLFunction<Native.D_glShaderSource            >("glShaderSource"            );
                Native.glAttachShader             = Native.WGLFunction<Native.D_glAttachShader            >("glAttachShader"            );
                Native.glDetachShader             = Native.WGLFunction<Native.D_glDetachShader            >("glDetachShader"            );
                Native.glDeleteShader             = Native.WGLFunction<Native.D_glDeleteShader            >("glDeleteShader"            );
                Native.glGetProgramiv             = Native.WGLFunction<Native.D_glGetProgramiv            >("glGetProgramiv"            );
                Native.glCompileShader            = Native.WGLFunction<Native.D_glCompileShader           >("glCompileShader"           );
                Native.glCreateProgram            = Native.WGLFunction<Native.D_glCreateProgram           >("glCreateProgram"           );
                Native.glDeleteProgram            = Native.WGLFunction<Native.D_glDeleteProgram           >("glDeleteProgram"           );
                Native.glBufferSubData            = Native.WGLFunction<Native.D_glBufferSubData           >("glBufferSubData"           );
                Native.glDeleteBuffers            = Native.WGLFunction<Native.D_glDeleteBuffers           >("glDeleteBuffers"           );
                Native.glGenVertexArrays          = Native.WGLFunction<Native.D_glGenVertexArrays         >("glGenVertexArrays"         );
                Native.glBindVertexArray          = Native.WGLFunction<Native.D_glBindVertexArray         >("glBindVertexArray"         );
                Native.glGetShaderInfoLog         = Native.WGLFunction<Native.D_glGetShaderInfoLog        >("glGetShaderInfoLog"        );
                Native.glUniformMatrix4fv         = Native.WGLFunction<Native.D_glUniformMatrix4fv        >("glUniformMatrix4fv"        );
                Native.glGetProgramInfoLog        = Native.WGLFunction<Native.D_glGetProgramInfoLog       >("glGetProgramInfoLog"       );
                Native.glGetUniformLocation       = Native.WGLFunction<Native.D_glGetUniformLocation      >("glGetUniformLocation"      );
                Native.glDeleteVertexArrays       = Native.WGLFunction<Native.D_glDeleteVertexArrays      >("glDeleteVertexArrays"      );
                Native.glVertexAttribPointer      = Native.WGLFunction<Native.D_glVertexAttribPointer     >("glVertexAttribPointer"     );
                Native.glVertexAttribIPointer     = Native.WGLFunction<Native.D_glVertexAttribIPointer    >("glVertexAttribIPointer"    );
                Native.glEnableVertexAttribArray  = Native.WGLFunction<Native.D_glEnableVertexAttribArray >("glEnableVertexAttribArray" );
                Native.glDisableVertexAttribArray = Native.WGLFunction<Native.D_glDisableVertexAttribArray>("glDisableVertexAttribArray");
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке OpenGL функций через WGL!", e);
            }
        }
        private static bool WGLStarted;
        
        // <summary>
        /// Текущий opengl32.dll
        /// </summary>
        private static IntPtr DLL;

        /// <summary>
        /// Всего созданных ресурсов во всех контекстах
        /// </summary>
        public static int TotalCreatedResources{ get; private set; }
        public static void __AddToTotalCreatedResources(){ TotalCreatedResources++; }
        
        /// <summary>
        /// Всего созданных контекстов
        /// </summary>
        public static int TotalCreatedGL{ get; private set; }
        public static void __AddToTotalCreatedGL(){ TotalCreatedGL++; }

        public static class Debug{
            /// <summary>
            /// Выводить сообщения об самом GL?
            /// </summary>
            public static bool LogMain;
            
            /// <summary>
            /// Выводить сообщения об создании GL ресурсов?
            /// </summary>
            public static bool LogCreate;

            /// <summary>
            /// Выводить сообщения об уничтожении GL ресурсов?
            /// </summary>
            public static bool LogDestroy;
            
            /// <summary>
            /// Выводить сообщения по поводу программы?
            /// </summary>
            public static bool LogProgram;

            /// <summary>
            /// Выводить сообщения по поводу использования?
            /// </summary>
            public static bool LogUse;

            /// <summary>
            /// Выводить сообщения по поводу буферов?
            /// </summary>
            public static bool LogBuffer;

            /// <summary>
            /// Выводить сообщения по поводу VertexConfig?
            /// </summary>
            public static bool LogVertexConfig;
        }
        
        /// <summary>
        /// Для работы с GLSL
        /// </summary>
        public static class GLSL{
            /// <summary>
            /// Конвертирует WoowzLib GLSL в обычный GLSL
            /// </summary>
            /// <param name="WLGLSL">WLGLSL код</param>
            /// <returns>GLSL код</returns>
            public static string WLGLSLToGLSL(string WLGLSL){
                try{
                    string GLSL = "//Конвертировано из WLGLSL! (WoowzLib GLSL)\n#version " + RenderContext.__OpenGLMajor + RenderContext.__OpenGLMinor + "0 core\n\n";

                    GLSL += WLGLSL;
                    
                    return GLSL;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при конвертации WLGLSL в GLSL!\nКод: " + WLGLSL);
                }
            }
        }
        
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
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glObjectLabel(uint identifier, uint name, int length, string label);
            public static D_glObjectLabel glObjectLabel = null!;

            public delegate void D_glGetProgramiv(uint program, uint pname, out int param);
            public static D_glGetProgramiv glGetProgramiv = null!;

            public delegate void D_glGetProgramInfoLog(uint program, int maxLength, out int length, IntPtr infoLog);
            public static D_glGetProgramInfoLog glGetProgramInfoLog = null!;

            public delegate void D_glDeleteProgram(uint program);
            public static D_glDeleteProgram glDeleteProgram = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDetachShader(uint program, uint shader);
            public static D_glDetachShader glDetachShader = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glGenBuffers(int n, uint[] buffers);
            public static D_glGenBuffers glGenBuffers = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glBindBuffer(uint target, uint buffer);
            public static D_glBindBuffer glBindBuffer = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glBufferData(uint target, IntPtr size, IntPtr data, uint usage);
            public static D_glBufferData glBufferData = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glBufferSubData(uint target, IntPtr offset, IntPtr size, IntPtr data);
            public static D_glBufferSubData glBufferSubData = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDeleteBuffers(int n, ref uint buffers);
            public static D_glDeleteBuffers glDeleteBuffers = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glGenVertexArrays(int n, uint[] arrays);
            public static D_glGenVertexArrays glGenVertexArrays = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glBindVertexArray(uint array);
            public static D_glBindVertexArray glBindVertexArray = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDeleteVertexArrays(int n, ref uint arrays);
            public static D_glDeleteVertexArrays glDeleteVertexArrays = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glEnableVertexAttribArray(uint index);
            public static D_glEnableVertexAttribArray glEnableVertexAttribArray = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDisableVertexAttribArray(uint index);
            public static D_glDisableVertexAttribArray glDisableVertexAttribArray = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glVertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
            public static D_glVertexAttribPointer glVertexAttribPointer = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glVertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer);
            public static D_glVertexAttribIPointer glVertexAttribIPointer = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDrawArrays(uint mode, int first, int count);
            public static D_glDrawArrays glDrawArrays = null!;

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glDrawElements(uint mode, int count, uint type, IntPtr indices);
            public static D_glDrawElements glDrawElements = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glLineWidth(float width);
            public static D_glLineWidth glLineWidth = null!;
            
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void D_glGetFloatv(uint pname, float[] data);
            public static D_glGetFloatv glGetFloatv = null!;
            
            public const uint GL_COLOR_BUFFER_BIT            = 0x00004000;
            public const uint GL_DEPTH_BUFFER_BIT            = 0x00000100;
            public const uint GL_STENCIL_BUFFER_BIT          = 0x00000400;
            public const uint GL_DEPTH_TEST                  = 0x0B71;
            public const uint GL_STENCIL_TEST                = 0x0B90;
            public const uint GL_BLEND                       = 0x0BE2;
            public const uint GL_CULL_FACE                   = 0x0B44;
            public const uint GL_SCISSOR_TEST                = 0x0C11;
            public const uint GL_LESS                        = 0x0201;
            public const uint GL_LEQUAL                      = 0x0203;
            public const uint GL_GREATER                     = 0x0204;
            public const uint GL_GEQUAL                      = 0x0206;
            public const uint GL_EQUAL                       = 0x0202;
            public const uint GL_NOTEQUAL                    = 0x0205;
            public const uint GL_ALWAYS                      = 0x0207;
            public const uint GL_NEVER                       = 0x0200;
            public const uint GL_VERTEX_SHADER               = 0x8B31;
            public const uint GL_FRAGMENT_SHADER             = 0x8B30;
            public const uint GL_GEOMETRY_SHADER             = 0x8DD9;
            public const uint GL_COMPUTE_SHADER              = 0x91B9;
            public const uint GL_TESS_CONTROL_SHADER         = 0x8E88;
            public const uint GL_TESS_EVALUATION_SHADER      = 0x8E87;
            public const uint GL_COMPILE_STATUS              = 0x8B81;
            public const uint GL_LINK_STATUS                 = 0x8B82;
            public const uint GL_INFO_LOG_LENGTH             = 0x8B84;
            public const uint GL_PROGRAM                     = 0x82E2;
            public const uint GL_SHADER                      = 0x82E1;
            public const uint GL_BUFFER                      = 0x82E0;
            public const uint GL_VERTEX_ARRAY                = 0x9154;
            public const uint GL_TEXTURE                     = 0x1702;
            public const uint GL_ARRAY_BUFFER                = 0x8892;
            public const uint GL_ELEMENT_ARRAY_BUFFER        = 0x8893;
            public const uint GL_UNIFORM_BUFFER              = 0x8A11;
            public const uint GL_SHADER_STORAGE_BUFFER       = 0x90D2;
            public const uint GL_PIXEL_PACK_BUFFER           = 0x88EB;
            public const uint GL_PIXEL_UNPACK_BUFFER         = 0x88EC;
            public const uint GL_COPY_READ_BUFFER            = 0x8F36;
            public const uint GL_COPY_WRITE_BUFFER           = 0x8F37;
            public const uint GL_TRANSFORM_FEEDBACK_BUFFER   = 0x8C8E;
            public const uint GL_STATIC_DRAW                 = 0x88E4;
            public const uint GL_DYNAMIC_DRAW                = 0x88E8;
            public const uint GL_STREAM_DRAW                 = 0x88E0;
            public const uint GL_RGB                         = 0x1907;
            public const uint GL_RGBA                        = 0x1908;
            public const uint GL_DEPTH_COMPONENT             = 0x1902;
            public const uint GL_TEXTURE_2D                  = 0x0DE1;
            public const uint GL_TEXTURE0                    = 0x84C0;
            public const uint GL_TEXTURE_MIN_FILTER          = 0x2801;
            public const uint GL_TEXTURE_MAG_FILTER          = 0x2800;
            public const uint GL_NEAREST                     = 0x2600;
            public const uint GL_LINEAR                      = 0x2601;
            public const uint GL_REPEAT                      = 0x2901;
            public const uint GL_CLAMP_TO_EDGE               = 0x812F;
            public const uint GL_FRAMEBUFFER                 = 0x8D40;
            public const uint GL_COLOR_ATTACHMENT0           = 0x8CE0;
            public const uint GL_DEPTH_ATTACHMENT            = 0x8D00;
            public const uint GL_STENCIL_ATTACHMENT          = 0x8D20;
            public const uint GL_RENDERBUFFER                = 0x8D41;
            public const uint GL_R32F                        = 0x822E;
            public const uint GL_RGBA32F                     = 0x8814;
            public const uint GL_NO_ERROR                    = 0;
            public const uint GL_INVALID_ENUM                = 0x0500;
            public const uint GL_INVALID_VALUE               = 0x0501;
            public const uint GL_INVALID_OPERATION           = 0x0502;
            public const uint GL_OUT_OF_MEMORY               = 0x0505;
            public const uint GL_VERSION                     = 0x1F02;
            public const uint GL_BYTE                        = 0x1400;
            public const uint GL_UNSIGNED_BYTE               = 0x1401;
            public const uint GL_SHORT                       = 0x1402;
            public const uint GL_UNSIGNED_SHORT              = 0x1403;
            public const uint GL_INT                         = 0x1404;
            public const uint GL_UNSIGNED_INT                = 0x1405;
            public const uint GL_FLOAT                       = 0x1406;
            public const uint GL_DOUBLE                      = 0x140A;
            public const uint GL_INT_2_10_10_10_REV          = 0x8D9F;
            public const uint GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368;
            public const uint GL_UNSIGNED_NORMALIZED         = 0x8C17;
            public const uint GL_POINTS                      = 0x0000;
            public const uint GL_LINES                       = 0x0001;
            public const uint GL_LINE_LOOP                   = 0x0002;
            public const uint GL_LINE_STRIP                  = 0x0003;
            public const uint GL_TRIANGLES                   = 0x0004;
            public const uint GL_TRIANGLE_STRIP              = 0x0005;
            public const uint GL_TRIANGLE_FAN                = 0x0006;
            public const uint GL_QUADS                       = 0x0007;
            public const uint GL_QUAD_STRIP                  = 0x0008;
            public const uint GL_POLYGON                     = 0x0009;
            public const uint GL_ALIASED_LINE_WIDTH_RANGE    = 0x846E;
            public const uint GL_LINE_SMOOTH                 = 0x0B20;

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