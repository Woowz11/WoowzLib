namespace WLO.GL;

public class Program : GLResource{
    public Program(Render.GL Context) : base(Context){
        try{
            ID = WL.GL.Native.glCreateProgram();
            if(ID <= 0){ throw new Exception("Не получилось создать шейдер в glCreateProgram!"); }
            
            __Finish(WL.GL.Native.GL_PROGRAM, "Программа");
            
            if(WL.GL.Debug.LogCreate){ Console.WriteLine("Создана программа [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL программы [" + this + "]!", e);
        }
    }
    
    protected override void __Destroy(){
        WL.GL.Native.glDeleteProgram(ID);
    }

    /// <summary>
    /// Присоединённые шейдеры
    /// </summary>
    private readonly List<Shader> ConnectedShaders = [];
    
    /// <summary>
    /// Присоединяет шейдер к программе
    /// </summary>
    /// <param name="Shader"></param>
    public Program Connect(Shader Shader){
        try{
            if(!Shader.Compiled){ throw new Exception("Шейдер не скомпилирован!"); }
            if(ConnectedShaders.Contains(Shader)){ throw new Exception("Такой шейдер уже есть!"); }
            
            Context.__MakeContext();
            WL.GL.Native.glAttachShader(ID, Shader.ID);
            ConnectedShaders.Add(Shader);

            if(WL.GL.Debug.LogProgram){ Console.WriteLine("Присоединён шейдер [" + Shader + "] программе [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при присоединении шейдера [" + Shader + "] программе [" + this + "]!", e);
        }

        return this;
    }

    /// <summary>
    /// Отсоединяет шейдер у программы
    /// </summary>
    /// <param name="Shader"></param>
    /// <returns></returns>
    public Program Disconnect(Shader Shader){
        try{
            if(!Shader.Compiled){ throw new Exception("Шейдер не скомпилирован!"); }
            if(!ConnectedShaders.Contains(Shader)){ throw new Exception("Шейдер не найден!"); }
            
            Context.__MakeContext();
            WL.GL.Native.glDetachShader(ID, Shader.ID);
            ConnectedShaders.Remove(Shader);
            
            if(WL.GL.Debug.LogProgram){ Console.WriteLine("Отсоединён шейдер [" + Shader + "] у программы [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при отсоединении шейдера [" + Shader + "] у программы [" + this + "]!", e);
        }

        return this;
    }

    /// <summary>
    /// Отсоединяет все шейдеры у программы
    /// </summary>
    /// <returns></returns>
    public Program Clear(){
        try{
            if(ConnectedShaders.Count == 0){ return this; }

            Context.__MakeContext();
            foreach(Shader Shader in ConnectedShaders){
                WL.GL.Native.glDetachShader(ID, Shader.ID);
            }
            ConnectedShaders.Clear();
            
            if(WL.GL.Debug.LogProgram){ Console.WriteLine("Отсоединены все шейдеры у программы [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при отсоединении всех шейдеров у программы [" + this + "]", e);
        }

        return this;
    }
    
    /// <summary>
    /// Изменяет шейдеры внутри программы (вписывает их внутрь, больше шейдеры нигде не используются)
    /// </summary>
    public Program Compile(){
        try{
            Context.__MakeContext();
            WL.GL.Native.glLinkProgram(ID);
            WL.GL.Native.glGetProgramiv(ID, WL.GL.Native.GL_LINK_STATUS, out int Status__);
            Status = Status__;

            if(Status == 0){
                const int LogSize = 1024;
                IntPtr LogLink = WL.Native.Memory(LogSize);
                WL.GL.Native.glGetProgramInfoLog(ID, LogSize, out _, LogLink);
                string Log = WL.Native.FromMemoryString(LogLink) ?? "";
                WL.Native.Free(LogLink);

                throw new Exception("Произошла ошибка при компиляции! Лог: " + Log);
            }
            
            if(WL.GL.Debug.LogProgram){ Console.WriteLine("Скомпилирована программа [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при компиляции программы [" + this + "]!", e);
        }

        return this;
    }
    
    /// <summary>
    /// Статус компиляции
    /// </summary>
    public int Status{ get; private set; }

    /// <summary>
    /// Скомпилирован?
    /// </summary>
    public bool Compiled => Created && Status != 0;
}