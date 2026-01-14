namespace WLO.GL;

public class Program : GLResource{
    public Program(Render.GL Context) : base(Context){
        try{
            ID = WL.GL.Native.glCreateProgram();
            if(ID <= 0){ throw new Exception("Не получилось создать шейдер в glCreateProgram!"); }
            
            __Finish(WL.GL.Native.GL_PROGRAM, "Программа");
            
            if(WL.GL.Debug.LogCreate){ Logger.Info("Создана программа [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL программы [" + this + "]!", e);
        }
    }

    public Program(Render.GL Context, params Shader[] Shaders) : this(Context){
        foreach(Shader Shader in Shaders){
            Connect(Shader);
        }

        Compile();
    }
    
    protected override void __Destroy(){
        WL.GL.Native.glDeleteProgram(ID);
    }

    /// <summary>
    /// Присоединённые шейдеры
    /// </summary>
    private readonly List<Shader> ConnectedShaders = [];

    /// <summary>
    /// Скомпилированные шейдеры
    /// </summary>
    private readonly List<Shader> CompiledShaders = [];

    /// <summary>
    /// Произошли какие-то изменения?
    /// </summary>
    private bool Dirty = true;
    
    /// <summary>
    /// Присоединяет шейдер к программе
    /// </summary>
    /// <param name="Shader"></param>
    public Program Connect(Shader Shader){
        try{
            if(!Shader.Created){ throw new Exception("Шейдер не создан!"); }
            if(ConnectedShaders.Contains(Shader)){ throw new Exception("Такой шейдер уже есть!"); }
            
            Context.__MakeContext();
            WL.GL.Native.glAttachShader(ID, Shader.ID);
            ConnectedShaders.Add(Shader);

            Dirty = true;
            
            if(WL.GL.Debug.LogProgram){ Logger.Info("Присоединён шейдер [" + Shader + "] программе [" + this + "]!"); }
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
            if(!Shader.Created){ throw new Exception("Шейдер не создан!"); }
            if(!ConnectedShaders.Contains(Shader)){ throw new Exception("Шейдер не найден!"); }
            
            Context.__MakeContext();
            WL.GL.Native.glDetachShader(ID, Shader.ID);
            ConnectedShaders.Remove(Shader);
            
            Dirty = true;
            
            if(WL.GL.Debug.LogProgram){ Logger.Info("Отсоединён шейдер [" + Shader + "] у программы [" + this + "]!"); }
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
            
            Dirty = true;
            
            if(WL.GL.Debug.LogProgram){ Logger.Info("Отсоединены все шейдеры у программы [" + this + "]!"); }
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
            if(!Dirty){ return this; }

            foreach(Shader Shader in ConnectedShaders){
                if(!Shader.Compiled){ throw new Exception("Шейдер [" + Shader + "] не скомпилированный!"); }
            }
            
            Context.__MakeContext();

            WL.GL.Native.glLinkProgram(ID);
            WL.GL.Native.glGetProgramiv(ID, WL.GL.Native.GL_LINK_STATUS, out int Status__);
            Status = Status__;

            if(Status == 0){
                WL.GL.Native.glGetProgramiv(ID, WL.GL.Native.GL_INFO_LOG_LENGTH, out int LogSize);
                string Log = "Лог пустой!";
                if(LogSize > 0){
                    IntPtr LogLink = WL.Native.Memory(LogSize);
                    WL.GL.Native.glGetProgramInfoLog(ID, LogSize, out _, LogLink);
                    Log = WL.Native.FromMemoryString(LogLink) ?? "Не найден лог!";
                    WL.Native.Free(LogLink);   
                }

                throw new Exception("Произошла ошибка при компиляции! Лог: " + Log);
            }
            
            CompiledShaders.Clear();
            CompiledShaders.AddRange(ConnectedShaders);

            Dirty = false;
            
            if(WL.GL.Debug.LogProgram){ Logger.Info("Скомпилирована программа [" + this + "]!"); }
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

    /// <summary>
    /// Использовать эту программу
    /// </summary>
    public Program Use(){
        Context.CurrentProgram = this;
        return this;
    }
}