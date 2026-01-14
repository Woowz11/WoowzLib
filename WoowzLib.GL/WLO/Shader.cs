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
    public Shader(Render.GL Context, ShaderType Type, string Source) : base(Context){
        try{
            this.Type = Type;
            ID = WL.GL.Native.glCreateShader((uint)Type);
            if(ID <= 0){ throw new Exception("Не получилось создать шейдер в glCreateShader!"); }

            this.Source = Source;
            WL.GL.Native.glShaderSource(ID, 1, [Source], null);

            WL.GL.Native.glCompileShader(ID);
            WL.GL.Native.glGetShaderiv(ID, WL.GL.Native.GL_COMPILE_STATUS, out int Status__);
            Status = Status__;
            if(Status == 0){
                const int LogSize = 512;
                IntPtr LogLink = WL.Native.Memory(LogSize);
                WL.GL.Native.glGetShaderInfoLog(ID, LogSize, out int _, LogLink);
                string Log = WL.Native.FromMemoryString(LogLink) ?? "";
                WL.Native.Free(LogLink);

                throw new Exception("Произошла ошибка при компиляции! Лог: " + Log);
            }

            __Finish(WL.GL.Native.GL_SHADER, "Шейдер");
            
            if(WL.GL.Debug.LogCreate){ Console.WriteLine("Создан шейдер [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL шейдера [" + this + "]!\nТип: " + Type + "\nКод:\n" + Source, e);
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
    /// Код шейдера
    /// </summary>
    public readonly string Source;
    
    /// <summary>
    /// Статус компиляции
    /// </summary>
    public int Status{ get; private set; }

    /// <summary>
    /// Скомпилирован?
    /// </summary>
    public bool Compiled => Created && Status != 0;
}