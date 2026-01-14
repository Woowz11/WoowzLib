using WLO.GL;

namespace WLO.Render;

/// <summary>
/// OpenGL рендер для окна
/// </summary>
public class GL : RenderContext{
    public override void __Start(){
        try{
            IntPtr VersionLink = WL.GL.Native.glGetString(WL.GL.Native.GL_VERSION);
            string? __Version = WL.Native.FromMemoryString(VersionLink);
            
            FullVersion = __Version ?? "Unknown";
            
            if(string.IsNullOrWhiteSpace(__Version)){ throw new Exception("Не получилось определить версию GL!"); }
            
            int Major = -1, Minor = -1;
            string[] Parts = FullVersion.Split('.', ' ');
            if(Parts.Length >= 2){
                int.TryParse(Parts[0], out Major);
                int.TryParse(Parts[1], out Minor);
            }
            Version = new Vector2I(Major, Minor);

            if(Version.X < __OpenGLMajor || Version.Y < __OpenGLMinor){ Logger.Warn("Установлена не максимальная версия GL [" + Major + "." + Minor + "] < [" + RenderContext.__OpenGLMajor + "." + RenderContext.__OpenGLMinor + "], возможны ошибки!"); }

            WL.GL.__StartWGL();
            
            WL.GL.__AddToTotalCreatedGL();
            ID = WL.GL.TotalCreatedGL;
            
            BackgroundColor = ColorF.Orange;
            
            Viewport = new RectI(0, 0, (int)ConnectedWindow.__Width, (int)ConnectedWindow.__Height);

            if(WL.GL.Debug.LogMain){ Logger.Info("Создан GL контекст [" + this + "] окну [" + ConnectedWindow + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации стартовых значений GL [" + this + "]!", e);
        }
    }

    public override void __Stop(){
        try{
            if(WL.GL.Debug.LogMain){ Logger.Info("Авто-очистка GL [" + this + "]!"); }

            ClearALLResources();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке GL [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Полная версия GL [<c>"4.6.0 NVIDIA 572.70"</c>]
    /// </summary>
    public string FullVersion{ get; private set; }
    
    /// <summary>
    /// Версия GL (Сначала Major, потом Minor)
    /// </summary>
    public Vector2I Version{ get; private set; }

    /// <summary>
    /// Проверяет, что контексты ресурса совпадают
    /// </summary>
    private void CheckGLResourceContext(GLResource? Resource){ if(Resource == null){ return; } if(Resource.Context != this){ throw new Exception("Контекст ресурса [" + Resource + "] и указанного GL [" + this + "] не совпадают!"); } }

    #region Uses
    
        /// <summary>
        /// Текущая программа в контексте
        /// </summary>
        public Program? CurrentProgram{ get; private set; }

        public GL UseProgram(Program? Program){
            try{
                if(CurrentProgram == Program){ return this; }
                CheckGLResourceContext(Program);
                if(Program != null && !Program.Created){ throw new Exception("Программа не создана!"); }

                __MakeContext();

                CurrentProgram = Program;
                WL.GL.Native.glUseProgram(Program?.ID ?? 0);

                if(WL.GL.Debug.LogUse){ Logger.Info("Программа [" + (Program?.ToString() ?? "НИКАКАЯ") + "] используется!"); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при использовании программы [" + (Program?.ToString() ?? "НИКАКАЯ") + "] у GL [" + this + "]!", e);
            }

            return this;
        }

        #endregion
    
    /// <summary>
    /// ID контекста
    /// </summary>
    public int ID{ get; private set; }
    
    /// <summary>
    /// Всего созданных ресурсов в этом контексте
    /// </summary>
    public int TotalCreatedResources{ get; private set; }

    /// <summary>
    /// Привязанные GL ресурсы к этому контексту
    /// </summary>
    private readonly List<GLResource> Resources = [];
    public void __Register  (GLResource Resource){
        Resources.Add(Resource);
        WL.GL.__AddToTotalCreatedResources();
        TotalCreatedResources++;
    }
    public void __Unregister(GLResource Resource) => Resources.Remove(Resource);

    /// <summary>
    /// Очищает все ресурсы внутри этого контекста!
    /// </summary>
    public GL ClearALLResources(){
        try{
            if(WL.GL.Debug.LogDestroy){ Logger.Info("Очистка всех ресурсов [" + this + "]!"); }
            
            foreach(GLResource Resource in Resources.ToArray()){
                Resource.TryDestroy();
            }
            Resources.Clear();
            
            if(WL.GL.Debug.LogDestroy){ Logger.Info("Завершена очистка всех ресурсов [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке всех ресурсов GL [" + this + "]!", e);
        }

        return this;
    }
    
    /// <summary>
    /// Цвет заднего фона
    /// </summary>
    public ColorF BackgroundColor{
        get => __BackgroundColor;
        set{
            try{
                if(__BackgroundColor == value){ return; }
                __BackgroundColor = value;

                __MakeContext();
                WL.GL.Native.glClearColor(__BackgroundColor.R, __BackgroundColor.G, __BackgroundColor.B, __BackgroundColor.A);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке цвета заднего фона GL [" + this + "]!\nЦвет: " + value, e);
            }
        }
    }
    private ColorF __BackgroundColor;

    /// <summary>
    /// Очищает рендер (Viewport не влияет, очищает всю область)
    /// <param name="Color">Буфер цвета</param>
    /// <param name="Depth">Буфер глубины</param>
    /// <param name="Stencil">Буфер трафарета</param>
    /// </summary>
    public GL Clear(bool Color = true, bool Depth = true, bool Stencil = false){
        try{
            if(!Color && !Depth && !Stencil){ return this; }

            uint Mask = 0;
            if(Color  ){ Mask |= WL.GL.Native.GL_COLOR_BUFFER_BIT  ; }
            if(Depth  ){ Mask |= WL.GL.Native.GL_DEPTH_BUFFER_BIT  ; }
            if(Stencil){ Mask |= WL.GL.Native.GL_STENCIL_BUFFER_BIT; }

            __MakeContext();
            WL.GL.Native.glClear(Mask);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке рендера GL [" + this + "]!\nЦвет: " + Color + "\nГлубина: " + Depth + "\nТрафарет: " + Stencil, e);
        }

        return this;
    }

    /// <summary>
    /// Текущая область рендера
    /// </summary>
    public RectI Viewport{
        get => __Viewport;
        set{
            try{
                if(__Viewport == value){ return; }
                __Viewport = value;
                
                __MakeContext();
                WL.GL.Native.glViewport(__Viewport.X, __Viewport.Y, __Viewport.Width, __Viewport.Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении области рендера GL [" + this + "]!\nОбласть: " + value, e);
            }
        }
    }
    private RectI __Viewport;
    
    #region Override

        public override string ToString(){
            return "GL(" + ID + ")";
        }

    #endregion
}