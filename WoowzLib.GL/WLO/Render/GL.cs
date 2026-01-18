using WLO.GL;
using Buffer = WLO.GL.Buffer;

namespace WLO.Render;

/// <summary>
/// OpenGL рендер для окна
/// </summary>
public class GL(RenderAPI? parent = null) : RenderAPI(parent){
    public void __TryStart(){
        try{
            if(Started){ return; } Started = true;

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

            if(Version.X < __OpenGLMajor || Version.Y < __OpenGLMinor){ Logger.Warn("Установлена не максимальная версия GL [" + Major + "." + Minor + "] < [" + __OpenGLMajor + "." + __OpenGLMinor + "], возможны ошибки!"); }
            
            WL.GL.__StartWGL();
                
            WL.GL.TotalGL.Add(this);
            ID = WL.GL.TotalCreatedGL;

            // Отключение VSync
            WL.GL.Native.wglSwapIntervalEXT(0);
            
            __BackgroundColor = ColorF.Black;
            BackgroundColor   = ColorF.Orange;

            __Viewport = new RectI();
            Viewport   = new RectI(128, 128);

            float[] LineWidthLimit__ = new float[2];
            WL.GL.Native.glGetFloatv(WL.GL.Native.GL_ALIASED_LINE_WIDTH_RANGE, LineWidthLimit__);
            LineWidthLimit = new Vector2F(LineWidthLimit__[0], LineWidthLimit__[1]);
            
            if(WL.GL.Debug.LogMain){ Logger.Info("Создан GL контекст [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации стартовых значений GL [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Был найден контекст и GL инициализировался полностью
    /// </summary>
    public bool Started{ get; private set; }

    public void __Destroy(bool Warn){
        try{
            if(Warn){ Logger.Warn("Авто-очистка GL [" + this + "]!" + (!Started ? " (GL был без контекста)" : "")); }
            if(!Started){ return; }

            ClearALLResources();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке GL [" + this + "]!", e);
        }
    }

    private IntPtr Handle = IntPtr.Zero;
    
    protected override void __StartContext(Drawable Target){
        try{
            if(Target is not DrawableWindow DWindow){ throw new Exception("Цель пока-что поддерживается только в виде окна!"); }

            IntPtr HDC = WL.Windows.Kernel.GetDC(DWindow.Handle);

            if(Handle == IntPtr.Zero){
                Handle = WL.GL.Native.wglCreateContext(HDC);
                if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать контекст GL в wglCreateContext! HDC: " + HDC); }

                if(Parent is GL ParentGL){
                    if(!WL.GL.Native.wglShareLists(ParentGL.Handle, Handle)){ throw new Exception("Не получилось поделиться ресурсами GL через wglShareLists! Родительский GL: " + ParentGL); }
                }
            }

            if(!WL.GL.Native.wglMakeCurrent(HDC, Handle)){ throw new Exception("Не получилось установить контекст GL через wglMakeCurrent! HDC: " + HDC); }

            WL.Windows.Kernel.ReleaseDC(DWindow.Handle, HDC);
            
            __TryStart();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке контекста GL [" + this + "]!\nЦель: " + Target, e);
        }
    }
    
    protected override void __StopContext(){
        WL.GL.Native.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
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
    /// Лимиты по ширине линии
    /// </summary>
    public Vector2F LineWidthLimit{ get; private set; }

    #region Uses

        /// <summary>
        /// Текущая программа в контексте
        /// </summary>
        public Program? CurrentProgram{
            get => __CurrentProgram;
            set{
                try{
                    if(__CurrentProgram == value){ return; }
                    if(value != null && !value.Created){ throw new Exception("Программа не создана!"); }

                    __CurrentProgram = value;
                    WL.GL.Native.glUseProgram(value?.ID ?? 0);

                    if(WL.GL.Debug.LogUse){ Logger.Info("Программа [" + (value?.ToString() ?? "НИКАКАЯ") + "] используется!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при использовании программы [" + (value?.ToString() ?? "НИКАКАЯ") + "] у GL [" + this + "]!", e);
                }
            }
        }
        private Program? __CurrentProgram;
        
        /// <summary>
        /// Текущий Float буфер в контексте
        /// </summary>
        public FloatBuffer? CurrentFloatBuffer{
            get => __CurrentFloatBuffer;
            set{
                try{
                    if(__CurrentFloatBuffer == value){ return; }
                    if(value != null && !value.Created){ throw new Exception("Буфер не создан!"); }

                    __CurrentFloatBuffer = value;
                    WL.GL.Native.glBindBuffer((uint)BufferType.Float, value?.ID ?? 0);

                    if(WL.GL.Debug.LogUse){ Logger.Info("Float Буфер [" + (value?.ToString() ?? "НИКАКОЙ") + "] используется!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при использовании Float буфер [" + (value?.ToString() ?? "НИКАКОЙ") + "] у GL [" + this + "]!", e);
                }
            }
        }
        private FloatBuffer? __CurrentFloatBuffer;
        
        /// <summary>
        /// Текущий Int буфер в контексте
        /// </summary>
        public IntBuffer? CurrentIntBuffer{
            get => __CurrentIntBuffer;
            set{
                try{
                    if(__CurrentIntBuffer == value){ return; }
                    if(value != null && !value.Created){ throw new Exception("Буфер не создан!"); }

                    __CurrentIntBuffer = value;
                    WL.GL.Native.glBindBuffer((uint)BufferType.Int, value?.ID ?? 0);

                    if(WL.GL.Debug.LogUse){ Logger.Info("Int Буфер [" + (value?.ToString() ?? "НИКАКОЙ") + "] используется!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при использовании Int буфер [" + (value?.ToString() ?? "НИКАКОЙ") + "] у GL [" + this + "]!", e);
                }
            }
        }
        private IntBuffer? __CurrentIntBuffer;
        
        /// <summary>
        /// Текущий VertexConfig в контексте
        /// </summary>
        public VertexConfig? CurrentVertexConfig{
            get => __CurrentVertexConfig;
            set{
                try{
                    if(__CurrentVertexConfig == value){ return; }
                    if(value != null && !value.Created){ throw new Exception("VertexConfig не создан!"); }

                    __CurrentVertexConfig = value;
                    WL.GL.Native.glBindVertexArray(value?.ID ?? 0);

                    if(WL.GL.Debug.LogUse){ Logger.Info("VertexConfig [" + (value?.ToString() ?? "НИКАКОЙ") + "] используется!"); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при использовании VertexConfig [" + (value?.ToString() ?? "НИКАКОЙ") + "] у GL [" + this + "]!", e);
                }
            }
        }
        private VertexConfig? __CurrentVertexConfig;

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
                
                WL.GL.Native.glViewport(__Viewport.X, __Viewport.Y, __Viewport.Width, __Viewport.Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении области рендера GL [" + this + "]!\nОбласть: " + value, e);
            }
        }
    }
    private RectI __Viewport;

    /// <summary>
    /// Ширина линий
    /// </summary>
    [Obsolete("Не реализовал, почему-то не работало, нужно разбираться мне лень", true)]
    public float LineWidth{
        get => __LineWidth;
        set{
            try{
                if(value > LineWidthLimit.Y){ throw new Exception("Выходит за пределы [" + value + " > " + LineWidthLimit.Y + "]!"); }
                if(value < LineWidthLimit.X){ throw new Exception("Выходит за пределы [" + value + " < " + LineWidthLimit.X + "]!"); }

                if(__LineWidth == value){ return; }
                __LineWidth = value;
                
                WL.GL.Native.glLineWidth(__LineWidth);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке ширины линий в GL [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    private float __LineWidth;
    
    #region Override

        public override string ToString(){
            return "GL(" + ID + ")";
        }

    #endregion
}