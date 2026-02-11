using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WL;

namespace WL{
    /// <summary>
    /// Эффект размытия при изменении размера изображения
    /// </summary>
    public enum ImageScalingInterpolation{
        Nearest, Bilinear, Bicubic, Lanczos
    }
}

namespace WLO{

    public class Image : IDisposable{
        public Image(uint Width, uint Height, ColorB? FillColor, bool lol){
            __Context = new ImageContext(this);

            Change(C => {
                C.SetSize(Width, Height);

                if(FillColor.HasValue){
                    C.Fill(FillColor.Value);
                }
            }, true);
        }

        public Image(uint Width, uint Height, ColorB Color) : this(Width, Height, Color, true){}
        public Image(uint Size, ColorB Color) : this(Size, Size, Color){}
        public Image(ColorB Color) : this(128, Color){}
        public Image(uint Width, uint Height) : this(Width, Height, ColorB.White){}
        public Image(uint Size) : this(Size, Size){}
        public Image() : this(128){}

        /// <summary>
        /// Создаёт изображение
        /// </summary>
        /// <param name="Width">Ширина</param>
        /// <param name="Height">Высота</param>
        /// <param name="Pixels">Принимает только RGBA!</param>
        public Image(uint Width, uint Height, byte[]? Pixels = null){
            __Context = new ImageContext(this);

            this.Width = Width;
            this.Height = Height;
            Pixels_RGBA = Pixels == null ? new byte[Width * Height * 4] : (byte[])Pixels.Clone();

            __Update(true);
        }

        /// <summary>
        /// Контекст изображения
        /// </summary>
        private ImageContext __Context;

        /// <summary>
        /// Ширина
        /// </summary>
        public uint Width{ get; private set; }

        /// <summary>
        /// Высота
        /// </summary>
        public uint Height{ get; private set; }

        /// <summary>
        /// Ширина x Высота
        /// </summary>
        public Vector2U Size => new Vector2U(Width, Height);

        /// <summary>
        /// Цвета
        /// </summary>
        public byte[] Pixels_RGBA{ get; private set; }

        /// <summary>
        /// Цвета
        /// </summary>
        public byte[] Pixels_BGRA{ get; private set; }

        public IntPtr __HDC         = IntPtr.Zero;
        public IntPtr __DIB         = IntPtr.Zero;
        public IntPtr __PixelsStart = IntPtr.Zero;
        public IntPtr __OldBitMap   = IntPtr.Zero;

        private void __Update(bool Create = false){
            try{
                #region PixelsBGRA

                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if(Pixels_BGRA == null || Pixels_RGBA.Length != Pixels_BGRA.Length){ Pixels_BGRA = new byte[Pixels_RGBA.Length]; }

                Parallel.For(0,
                    Pixels_RGBA.Length / 4,
                    i => {
                        int IDX = i * 4;
                        Pixels_BGRA[IDX + 0] = Pixels_RGBA[IDX + 2];
                        Pixels_BGRA[IDX + 1] = Pixels_RGBA[IDX + 1];
                        Pixels_BGRA[IDX + 2] = Pixels_RGBA[IDX + 0];
                        Pixels_BGRA[IDX + 3] = Pixels_RGBA[IDX + 3];
                    });

                #endregion

                #region DC

                if(Create && __HDC == IntPtr.Zero){
                    __HDC = WL.System.Native.Windows.CreateCompatibleDC(IntPtr.Zero);

                    WL.System.Native.Windows.BITMAPINFO BMI = new WL.System.Native.Windows.BITMAPINFO();
                    BMI.bmiHeader.biSize = (uint)WL.Math.Byte.Size<WL.System.Native.Windows.BITMAPINFOHEADER>();
                    BMI.bmiHeader.biWidth = (int)Width;
                    BMI.bmiHeader.biHeight = -(int)Height;
                    BMI.bmiHeader.biPlanes = 1;
                    BMI.bmiHeader.biBitCount = 4 * 8;
                    BMI.bmiHeader.biCompression = WL.System.Native.Windows.BI_RGB;
                    BMI.bmiHeader.biSizeImage = (uint)(Pixels_BGRA.Length);

                    __DIB = WL.System.Native.Windows.CreateDIBSection(__HDC, ref BMI, 0, out __PixelsStart, IntPtr.Zero, 0);
                    __OldBitMap = WL.System.Native.Windows.SelectObject(__HDC, __DIB);
                }

                if(__PixelsStart != IntPtr.Zero){ Marshal.Copy(Pixels_BGRA, 0, __PixelsStart, Pixels_BGRA.Length); }

                #endregion
            }
            catch(Exception e){
                throw new Exception("Произошла ошибка при генерации BGRA у изображения [" + this + "]!", e);
            }
        }

        public Image Change(Action<ImageContext> Action, bool Create = false){
            try{
                if(__CanChange){ throw new Exception("Изображение уже изменяется!"); }
                __CanChange = true;
                Action(__Context);
                __Update(Create);
                __CanChange = false;

                return this;
            }
            catch(Exception e){
                throw new Exception("Произошла ошибка при изменении изображения [" + this + "]!", e);
            }
        }
        private bool __CanChange = false;

        public class ImageContext(Image Image){
            private readonly Image __Image = Image;
            private void __CanChange(){ if(!__Image.__CanChange){ throw new Exception("Нельзя сейчас изменять изображение [" + __Image + "], оно сейчас не в режиме редактирования!"); } }
            private bool __Initialized() => __Image.Pixels_RGBA != null;
            private byte[] __Pixels(){
                if(!__Initialized()){ __Image.Pixels_RGBA = new byte[Width * Height * 4]; }
                return __Image.Pixels_RGBA;
            }
            
            /// <summary>
            /// Ширина
            /// </summary>
            public uint Width{
                get => __Image.Width;
                set => SetWidth(value);
            }

            /// <summary>
            /// Высота
            /// </summary>
            public uint Height{
                get => __Image.Height;
                set => SetHeight(value);
            }
            
            /// <summary>
            /// Ширина x Высота
            /// </summary>
            public Vector2U Size{
                get => __Image.Size;
                set => SetSize(value.X, value.Y);
            }

            /// <summary>
            /// Установить новую ширину
            /// </summary>
            /// <param name="Width">Новая ширина</param>
            /// <param name="ScalingInterpolation">Тип размытия</param>
            public ImageContext SetWidth(uint Width, ImageScalingInterpolation ScalingInterpolation = ImageScalingInterpolation.Nearest){
                try{
                    return SetSize(Width, Height, ScalingInterpolation);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при изменении ширины у изображения [" + this + "]!\nШирина: " + Width + "\nРазмытие: " + ScalingInterpolation, e);
                }
            }

            /// <summary>
            /// Установить новую высоту
            /// </summary>
            /// <param name="Height">Новая высота</param>
            /// <param name="ScalingInterpolation">Тип размытия</param>
            public ImageContext SetHeight(uint Height, ImageScalingInterpolation ScalingInterpolation = ImageScalingInterpolation.Nearest){
                try{
                    return SetSize(Width, Height, ScalingInterpolation);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при изменении высоты у изображения [" + this + "]!\nВысота: " + Height + "\nРазмытие: " + ScalingInterpolation, e);
                }
            }

            /// <summary>
            /// Установить новую ширину и высоту
            /// </summary>
            /// <param name="Width">Новая ширина</param>
            /// <param name="Height">Новая высота</param>
            /// <param name="ScalingInterpolation">Тип размытия</param>
            public ImageContext SetSize(uint Width, uint Height, ImageScalingInterpolation ScalingInterpolation = ImageScalingInterpolation.Nearest){
                try{
                    __CanChange();
                    if(__Image.Width == Width && __Image.Height == Height){ return this; }
                    if(Width  == 0){ throw new Exception("Ширина не может быть равна 0!"); }
                    if(Height == 0){ throw new Exception("Высота не может быть равна 0!"); }
                    
                    if(__Initialized()){
                        
                    }
                    
                    __Image.Width  = Width;
                    __Image.Height = Height;
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при изменении ширины и высоты у изображения [" + this + "]!\nШирина: " + Width + "\nВысота: " + Height + "\nРазмытие: " + ScalingInterpolation, e);
                }
            }

            /// <summary>
            /// Заполняет всё пространство цветом
            /// </summary>
            /// <param name="Color">Цвет</param>
            public ImageContext Fill(ColorB? Color = null){
                try{
                    __CanChange();
                    ColorB Color__ = Color ?? ColorB.White;
                    
                    WL.Math.Byte.FillArray(__Pixels(), Width * Height, Color__.R, Color__.G, Color__.B, Color__.A);
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при заполнении цветом изображения [" + this + "]!\nЦвет: " + Color, e);
                }
            }

            /// <summary>
            /// Заполняет всё пространство прозрачным цветом
            /// </summary>
            public ImageContext Clear() => Fill(ColorB.Transparent);

            /// <summary>
            /// Вызывает Action на каждый пиксель изображения
            /// </summary>
            /// <param name="Action">Действие [X, Y, Ширина, Высота]</param>
            public ImageContext For(Action<uint, uint, uint, uint> Action){
                try{
                    Parallel.For(0, Height, Y => {
                        for(uint X = 0; X < Width; X++){
                            try{
                                Action(X, (uint)Y, Width, Height);
                            }catch(Exception e){
                                throw new Exception("Произошла ошибка в пикселе [" + X + "x" + Y + "]!", e);
                            }
                        } 
                    });
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при переборе всех пикселей у изображения [" + this + "]!", e);
                }
            }
            
            public ColorB this[uint X, uint Y]{
                get => GetPixel(X, Y);
                set => SetPixel(X, Y, value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int IDX(uint X, uint Y) => ((int)Y * (int)__Image.Width + (int)X) * 4;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ImageContext SetPixel(uint X, uint Y, ColorB Color){
                int IDX__ = IDX(X, Y);
                __Image.Pixels_RGBA[IDX__ + 0] = Color.R;
                __Image.Pixels_RGBA[IDX__ + 1] = Color.G;
                __Image.Pixels_RGBA[IDX__ + 2] = Color.B;
                __Image.Pixels_RGBA[IDX__ + 3] = Color.A;

                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ColorB GetPixel(uint X, uint Y){
                int IDX__ = IDX(X, Y);
                return new ColorB(__Image.Pixels_RGBA[IDX__], __Image.Pixels_RGBA[IDX__ + 1], __Image.Pixels_RGBA[IDX__ + 2], __Image.Pixels_RGBA[IDX__ + 3]);
            }

            /*public uint Width{
                get => __Image.Width;
                set{

                }
            }

            public uint Height{
                get => __Image.Height;
                set{

                }
            }

            public ImageContext Resize(uint Width, uint Height){
                this.Width = Width;
                this.Height = Height;
            }

            public ImageContext OverwriteRGBA(byte[] Colors){

            }

            public ImageContext OverwriteBGRA(byte[] Colors){

            }

            public ImageContext OverwriteRGB(byte[] Colors, byte Alpha = 255){

            }
            public ImageContext OverwriteBGR(byte[] Colors, byte Alpha = 255){

            }

            public ImageContext Set*/
        }

        private byte __R;
        private byte __G;
        private byte __B;
        public void __ApplyColor(byte R, byte G, byte B){
            if(__R == R && __G == G && __B == B){ return; }
            __R = R;
            __G = G;
            __B = B;

            unsafe{
                byte* Link = (byte*)__PixelsStart;
                byte* Source = (byte*)Unsafe.AsPointer(ref Pixels_BGRA[0]);
                int PixelCount = (int)(Width * Height);

                // мб добавить систему выполнения на gpu? сделать проверку есть ли вулкан, и делать так...

                Parallel.For(0,
                    PixelCount,
                    i => {
                        int IDX = i * 4;
                        Link[IDX + 0] = (byte)((Source[IDX + 0] * B) / 255);
                        Link[IDX + 1] = (byte)((Source[IDX + 1] * G) / 255);
                        Link[IDX + 2] = (byte)((Source[IDX + 2] * R) / 255);
                        Link[IDX + 3] = Source[IDX + 3];
                    });
            }
        }

        public override string ToString() => "Image(" + Width + "x" + Height + ")";

        public void Dispose(){
            if(__HDC != IntPtr.Zero){
                if(__DIB != IntPtr.Zero){ WL.System.Native.Windows.SelectObject(__HDC, __OldBitMap); }
                WL.System.Native.Windows.DeleteDC(__HDC);
                __HDC = IntPtr.Zero;
                __DIB = IntPtr.Zero;
                __PixelsStart = IntPtr.Zero;
            }
        }
    }
}