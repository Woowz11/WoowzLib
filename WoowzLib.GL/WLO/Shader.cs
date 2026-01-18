namespace WLO.GL;

/// <summary>
/// Типы шейдеров
/// </summary>
public enum ShaderType : uint{
    /// <summary>
    /// Вершинный шейдер (VERTEX)<br/>
    /// Обрабатывает вершины
    /// </summary>
    Vertex = WL.GL.Native.GL_VERTEX_SHADER,
    /// <summary>
    /// Фрагментный шейдер (FRAGMENT)<br/>
    /// Обрабатывает пиксели на экране
    /// </summary>
    Fragment = WL.GL.Native.GL_FRAGMENT_SHADER,
    /// <summary>
    /// Геометрический шейдер (GEOMETRY)<br/>
    /// ?
    /// </summary>
    Geometry = WL.GL.Native.GL_GEOMETRY_SHADER,
    /// <summary>
    /// (COMPUTE)<br/>
    /// ?
    /// </summary>
    Compute = WL.GL.Native.GL_COMPUTE_SHADER,
    /// <summary>
    /// (TESS_CONTROL)<br/>
    /// ?
    /// </summary>
    TessellationControl = WL.GL.Native.GL_TESS_CONTROL_SHADER,
    /// <summary>
    /// (TESS_EVALUATION)<br/>
    /// ?
    /// </summary>
    TesselationEvaluation = WL.GL.Native.GL_TESS_EVALUATION_SHADER,
    /// <summary>
    /// 
    /// </summary>
    Task, // что за NV?
    /// <summary>
    /// 
    /// </summary>
    Mesh
}

/// <summary>
/// Шейдер
/// </summary>
public class Shader : GLResource{
    /// <summary>
    /// Создаёт шейдер
    /// </summary>
    /// <param name="Context">На какой контекст создавать?</param>
    /// <param name="Type">Тип шейдера</param>
    /// <param name="Code">WLGLSL или GLSL код шейдера</param>
    /// <param name="ThatWLGLSL">Преобразовать код из WLGLSL в GLSL?</param>
    public Shader(ShaderType Type, string Code, bool ThatWLGLSL = true) : base(){
        try{
            this.Type = Type;

            CompiledUseWLGLSL = ThatWLGLSL;
            
            WLGLSL = (ThatWLGLSL ? Code : "//Шейдер скомпилирован на чистом GLSL!");
            GLSL   = (ThatWLGLSL ? WL.GL.GLSL.WLGLSLToGLSL(Code) : Code);
            
            ID = WL.GL.Native.glCreateShader((uint)Type);
            if(ID <= 0){ throw new Exception("Не получилось создать шейдер в glCreateShader!"); }
            WL.GL.Native.glShaderSource(ID, 1, [GLSL], null);

            WL.GL.Native.glCompileShader(ID);
            WL.GL.Native.glGetShaderiv(ID, WL.GL.Native.GL_COMPILE_STATUS, out int Status__);
            Status = Status__;
            if(Status == 0){
                WL.GL.Native.glGetShaderiv(ID, WL.GL.Native.GL_INFO_LOG_LENGTH, out int LogSize);
                string Log = "Лог пустой!";
                if(LogSize > 0){
                    IntPtr LogLink = WL.WoowzLib.Native.Memory(LogSize);
                    WL.GL.Native.glGetShaderInfoLog(ID, LogSize, out _, LogLink);
                    Log = WL.WoowzLib.Native.FromMemoryString(LogLink) ?? "Не найден лог!";
                    WL.WoowzLib.Native.Free(LogLink);   
                }

                throw new Exception("Произошла ошибка при компиляции! Лог: " + Log);
            }

            __Finish(WL.GL.Native.GL_SHADER, Type + " Шейдер");
            
            if(WL.GL.Debug.LogCreate){ Logger.Info("Создан шейдер [" + this + "]!\nКод (WLGLSL):\n" + Code + "\nКод (GLSL):\n" + GLSL); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL шейдера [" + this + "]!\nТип: " + Type + "\nКод:\n" + Code, e);
        }
    }
    
    protected override void __Destroy(){
        WL.GL.Native.glDeleteShader(ID);
    }

    /// <summary>
    /// Тип шейдера
    /// </summary>
    public readonly ShaderType Type;
    
    /// <summary>
    /// (Стартовый) Код шейдера
    /// </summary>
    public readonly string WLGLSL;
    
    /// <summary>
    /// Код шейдера
    /// </summary>
    public readonly string GLSL;

    /// <summary>
    /// Скомпилировано с использованием WLGLSL?
    /// </summary>
    public readonly bool CompiledUseWLGLSL;
    
    /// <summary>
    /// Статус компиляции
    /// </summary>
    public int Status{ get; private set; }

    /// <summary>
    /// Скомпилирован?
    /// </summary>
    public bool Compiled => Created && Status != 0;
    
    #region Override

        public override string ToString(){
            return "Shader(\"" + Name + "\", " + Type + ", " + (Compiled ? "" : "НЕ СКОМПИЛИРОВАННЫЙ, ") + IDString + ", " + Context + ")";
        }

    #endregion
}