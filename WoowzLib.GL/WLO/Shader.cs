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
    /// 
    /// </summary>
    Fragment = WL.GL.Native.GL_FRAGMENT_SHADER,
    /// <summary>
    /// 
    /// </summary>
    Geometry,
    /// <summary>
    /// 
    /// </summary>
    Compute,
    /// <summary>
    /// 
    /// </summary>
    TessellationControl,
    /// <summary>
    /// 
    /// </summary>
    TesselationEvaluation,
    /// <summary>
    /// 
    /// </summary>
    Task,
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
            WL.GL.Native.glShaderSource(ID, 1, [Source], [Source.Length]);

            WL.GL.Native.glCompileShader(ID);
            WL.GL.Native.glGetShaderiv(ID, WL.GL.Native.GL_COMPILE_STATUS, out int Status);
            if(Status == 0){
                const int LogSize = 512;
                IntPtr LogLink = WL.Native.Memory(LogSize);
                WL.GL.Native.glGetShaderInfoLog(ID, LogSize, out int Length, LogLink);
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
}