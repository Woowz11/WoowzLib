namespace WLO;

public class GL : RenderContext{
    public override void __Start(){
        try{
            BackgroundColor = ColorF.Orange;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации стартовых значений GL [" + this + "]!", e);
        }
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

                MakeContext();
                WL.GL.Native.glClearColor(__BackgroundColor.R, __BackgroundColor.G, __BackgroundColor.B, __BackgroundColor.A);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке цвета заднего фона GL [" + this + "]!\nЦвет: " + value, e);
            }
        }
    }
    private ColorF __BackgroundColor;

    /// <summary>
    /// Очищает рендер
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
}