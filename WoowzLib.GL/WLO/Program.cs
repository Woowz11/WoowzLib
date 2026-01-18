namespace WLO.GL;

/// <summary>
/// (PROGRAM)
/// </summary>
public class Program : GLResource{
    /// <summary>
    /// Программа содержащая в себе шейдеры (После компиляции шейдеры не имеют ценности для программы)
    /// </summary>
    public Program() : base(){
        try{
            ID = WL.GL.Native.glCreateProgram();
            if(ID <= 0){ throw new Exception("Не получилось создать шейдер в glCreateProgram!"); }
            
            __Finish(WL.GL.Native.GL_PROGRAM, "Программа");
            
            if(WL.GL.Debug.LogCreate){ Logger.Info("Создана программа [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании GL программы [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Программа содержащая в себе шейдеры (После компиляции шейдеры не имеют ценности для программы)
    /// </summary>
    public Program(params Shader[] Shaders) : this(){
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
            CheckContext(Shader.Context);
            if(!Shader.Created){ throw new Exception("Шейдер не создан!"); }
            if(ConnectedShaders.Contains(Shader)){ throw new Exception("Такой шейдер уже есть!"); }
            
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
            CheckContext(Shader.Context);
            if(!Shader.Created){ throw new Exception("Шейдер не создан!"); }
            if(!ConnectedShaders.Contains(Shader)){ throw new Exception("Шейдер не найден!"); }
            
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
    /// Изменяет шейдеры внутри программы (вписывает их внутрь, больше шейдеры нигде не используются) (Сбрасывает существующие Uniform)
    /// </summary>
    public Program Compile(){
        try{
            if(!Dirty){ return this; }

            foreach(Shader Shader in ConnectedShaders){
                if(!Shader.Compiled){ throw new Exception("Шейдер [" + Shader + "] не скомпилированный!"); }
            }

            ClearUniforms();
            
            WL.GL.Native.glLinkProgram(ID);
            WL.GL.Native.glGetProgramiv(ID, WL.GL.Native.GL_LINK_STATUS, out int Status__);
            Status = Status__;

            if(Status == 0){
                WL.GL.Native.glGetProgramiv(ID, WL.GL.Native.GL_INFO_LOG_LENGTH, out int LogSize);
                string Log = "Лог пустой!";
                if(LogSize > 0){
                    IntPtr LogLink = WL.WoowzLib.Native.Memory(LogSize);
                    WL.GL.Native.glGetProgramInfoLog(ID, LogSize, out _, LogLink);
                    Log = WL.WoowzLib.Native.FromMemoryString(LogLink) ?? "Не найден лог!";
                    WL.WoowzLib.Native.Free(LogLink);   
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

    /// <summary>
    /// Все Uniform программы
    /// </summary>
    private readonly List<Uniform> Uniforms = [];
    
    /// <summary>
    /// Добавляет Uniform в программу
    /// </summary>
    /// <param name="U"></param>
    public void __AddUniform(Uniform U){
        if(Uniforms.Contains(U)){ throw new Exception("Такой Uniform уже есть в программе!"); }
        Uniforms.Add(U);
    }
    
    /// <summary>
    /// Убирает все Uniform у программы
    /// </summary>
    private void ClearUniforms(){
        Uniforms.RemoveAll(U => U.__ClearProgram());
    }
    
    #region Override

        public override string ToString(){
            return "Program(\"" + Name + "\", " + ConnectedShaders.Count + ":" + CompiledShaders.Count + ", " + (Compiled ? "" : "НЕ СКОМПИЛИРОВАННЫЙ, ") + IDString + ", " + Context + ")";
        }

    #endregion
}

public abstract class Uniform{
    /// <summary>
    /// Uniform программы
    /// </summary>
    /// <param name="Program">Программа</param>
    /// <param name="Name">Название Uniform</param>
    /// <param name="TypeName">Тип в виде строки</param>
    /// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
    protected Uniform(Program Program, string Name, string TypeName, bool TryAnywayFind = false){
        try{
            this.Program       = Program;
            this.Name          = Name;
            this.TryAnywayFind = TryAnywayFind;

            if(!Program.Compiled){ throw new Exception("Программа не скомпилирована!"); }
            
            Location = WL.GL.Native.glGetUniformLocation(Program.ID, Name);
            if(Location < 0 && !TryAnywayFind){ throw new Exception("Не найден такой Uniform [\"" + Name + "\"] в программе, или не используется!"); }

            Program.__AddUniform(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании Uniform [" + this + "]!\nНазвание: \"" + Name + "\"\nПрограмма: " + Program, e);
        }
    }

    /// <summary>
    /// Программа к которой привязан Uniform
    /// </summary>
    public Program? Program{ get; private set; }

    /// <summary>
    /// Удаляет программу из Uniform
    /// </summary>
    public bool __ClearProgram(){
        Location = -1;
        if(TryAnywayFind){ return false; }
        Program = null;
        return true;
    }

    /// <summary>
    /// Uniform живой?
    /// </summary>
    public bool Usable => Program != null && Location >= 0;
    
    /// <summary>
    /// Название Uniform
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Позиция Uniform в программе
    /// </summary>
    public int Location{ get; private set; }

    /// <summary>
    /// Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)
    /// </summary>
    public bool TryAnywayFind;

    /// <summary>
    /// Попытаться найти Location, если его нет
    /// </summary>
    protected void TryFind(){
        try{
            if(!TryAnywayFind){ return; }
            if(Program == null || !Program.Compiled){ throw new Exception("Нет программы, или она не скомпилирована!"); }
            
            Location = WL.GL.Native.glGetUniformLocation(Program.ID, Name);
            if(Location < 0){ throw new Exception("Не найден такой Uniform [\"" + Name + "\"] в программе, или не используется!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке поиска новой локации у Uniform [\"" + Name + "\"] в программе [" + Program + "]!", e);
        }
    }
}

/// <summary>
/// Для Uniform формата (<c>float</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Float(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Float", TryAnywayFind){
    public float Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform1f(Location, __Value);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Float [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private float __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec2</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector2F(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector2F", TryAnywayFind){
    public Vector2F Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform2f(Location, __Value.X, __Value.Y);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector2F [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector2F __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec3</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector3F(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector3F", TryAnywayFind){
    public Vector3F Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform3f(Location, __Value.X, __Value.Y, __Value.Z);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector3F [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector3F __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec4</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector4F(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector4F", TryAnywayFind){
    public Vector4F Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform4f(Location, __Value.X, __Value.Y, __Value.Z, __Value.W);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector4F [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector4F __Value;
}

/// <summary>
/// Для Uniform формата (<c>int</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Int(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Int", TryAnywayFind){
    public int Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform1i(Location, __Value);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Int [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private int __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec2i</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector2I(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector2I", TryAnywayFind){
    public Vector2I Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform2i(Location, __Value.X, __Value.Y);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector2I [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector2I __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec3i</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector3I(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector3I", TryAnywayFind){
    public Vector3I Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform3i(Location, __Value.X, __Value.Y, __Value.Z);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector3I [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector3I __Value;
}

/// <summary>
/// Для Uniform формата (<c>vec4i</c>)
/// </summary>
/// <param name="Program">Программа</param>
/// <param name="Name">Название Uniform</param>
/// <param name="TryAnywayFind">Если включено, то пробует искать Location при изменении значения (Так же не отвязывает от программы)</param>
public class Uniform_Vector4I(Program Program, string Name, bool TryAnywayFind = false) : Uniform(Program, Name, "Vector4I", TryAnywayFind){
    public Vector4I Value{
        get => __Value;
        set{
            try{
                if(!Usable){ throw new Exception("Невозможно использовать этот Uniform!"); }
                if(__Value == value){ return; }
                __Value = value;
                
                Program!.Use();
                TryFind();
                WL.GL.Native.glUniform4i(Location, __Value.X, __Value.Y, __Value.Z, __Value.W);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке Uniform Vector4I [" + this + "] значения!\nЗначение: " + value);
            }
        }
    }
    private Vector4I __Value;
}