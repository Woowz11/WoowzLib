namespace WLO;

public class GL : RenderContext{
    public override void __Start(){
        try{
            IntPtr VersionLink = WL.GL.Native.glGetString(WL.GL.Native.GL_VERSION);
            string? __Version = WL.Native.FromMemoryString(VersionLink);
            if(string.IsNullOrWhiteSpace(__Version)){ throw new Exception("Не получилось определить версию GL!"); }

            FullVersion = __Version;
            
            int Major = -1, Minor = -1;
            string[] Parts = FullVersion.Split('.', ' ');
            if(Parts.Length >= 2){
                int.TryParse(Parts[0], out Major);
                int.TryParse(Parts[1], out Minor);
            }
            Version = new Vector2I(Major, Minor);

            if(Version.X < __OpenGLMajor || Version.Y < __OpenGLMinor){ Console.WriteLine("Установлена не максимальная версия GL [" + Major + "." + Minor + "] < [" + RenderContext.__OpenGLMajor + "." + RenderContext.__OpenGLMinor + "], возможны ошибки!"); }

            BackgroundColor = ColorF.Orange;
            
            Viewport = new RectI(0, 0, (int)ConnectedWindow.__Width, (int)ConnectedWindow.__Height);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации стартовых значений GL [" + this + "]!", e);
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
    /// Цвет заднего фона
    /// </summary>
    public ColorF BackgroundColor{
        get => __BackgroundColor;
        set{
            try{
                if(__BackgroundColor == value){ return; }
                __BackgroundColor = value;

                MakeContext();
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

            MakeContext();
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
                
                MakeContext();
                WL.GL.Native.glViewport(__Viewport.X, __Viewport.Y, __Viewport.Width, __Viewport.Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении области рендера GL [" + this + "]!\nОбласть: " + value, e);
            }
        }
    }
    private RectI __Viewport;
}