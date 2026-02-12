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

    /// <summary>
    /// Эффект смешивания
    /// </summary>
    public enum ImageBlend{
        /// <summary>
        /// Полная замена
        /// </summary>
        Fixed,
        /// <summary>
        /// Смешивание по прозрачности
        /// </summary>
        Alpha
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

                return this;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении изображения [" + this + "]!", e);
            }finally{
                __Update(Create);
                __CanChange = false;
            }
        }
        private bool __CanChange = false;

        public class ImageContext(Image Image){
            private void __CanChange(){ if(!Image.__CanChange){ throw new Exception("Нельзя сейчас изменять изображение [" + Image + "], оно сейчас не в режиме редактирования!"); } }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            private bool __Initialized() => Image.Pixels_RGBA != null;
            private void __TryCreatePixels(){ if(!__Initialized()){ Image.Pixels_RGBA = new byte[Width * Height * 4]; } }
            
            /// <summary>
            /// Ширина
            /// </summary>
            public uint Width{
                get => Image.Width;
                set => SetWidth(value);
            }

            /// <summary>
            /// Высота
            /// </summary>
            public uint Height{
                get => Image.Height;
                set => SetHeight(value);
            }
            
            /// <summary>
            /// Ширина x Высота
            /// </summary>
            public Vector2U Size{
                get => Image.Size;
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
                    if(Image.Width == Width && Image.Height == Height){ return this; }
                    if(Width  == 0){ throw new Exception("Ширина не может быть равна 0!"); }
                    if(Height == 0){ throw new Exception("Высота не может быть равна 0!"); }
                    
                    if(__Initialized()){
                        
                    }
                    
                    Image.Width  = Width;
                    Image.Height = Height;
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при изменении ширины и высоты у изображения [" + this + "]!\nШирина: " + Width + "\nВысота: " + Height + "\nРазмытие: " + ScalingInterpolation, e);
                }
            }

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

            /// <summary>
            /// Выходит за пределы изображения?
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool OutOfBounds(int X, int Y) => X >= Width || Y >= Height || X < 0 || Y < 0;
            /// <summary>
            /// Выходит за пределы изображения?
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool OutOfBounds(uint X, uint Y) => X >= Width || Y >= Height;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int IDX(uint X, uint Y) => (int)(Y * Width + X) * 4;

            /// <summary>
            /// Устанавливает цвет пикселя
            /// </summary>
            /// <param name="X">X</param>
            /// <param name="Y">Y</param>
            /// <param name="Color">Цвет</param>
            /// <param name="Blend">Смешивание</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ImageContext SetPixel(uint X, uint Y, ColorB Color, ImageBlend Blend = ImageBlend.Fixed){
                __CanChange();
                
                if(OutOfBounds(X, Y)){ return this; }

                int IDX__ = IDX(X, Y);
                switch(Blend){
                    case ImageBlend.Fixed: {
                        Image.Pixels_RGBA[IDX__ + 0] = Color.R;
                        Image.Pixels_RGBA[IDX__ + 1] = Color.G;
                        Image.Pixels_RGBA[IDX__ + 2] = Color.B;
                        Image.Pixels_RGBA[IDX__ + 3] = Color.A;
                        break;
                    }
                    
                    case ImageBlend.Alpha:{
                        if(Color.A == 0){ return this; }

                        byte DstR = Image.Pixels_RGBA[IDX__ + 0];
                        byte DstG = Image.Pixels_RGBA[IDX__ + 1];
                        byte DstB = Image.Pixels_RGBA[IDX__ + 2];
                        byte DstA = Image.Pixels_RGBA[IDX__ + 3];

                        float A  = Color.A / 255f;
                        float IA = 1 - A;
                        
                        Image.Pixels_RGBA[IDX__ + 0] = (byte)(Color.R * A + DstR * IA);
                        Image.Pixels_RGBA[IDX__ + 1] = (byte)(Color.G * A + DstG * IA);
                        Image.Pixels_RGBA[IDX__ + 2] = (byte)(Color.B * A + DstB * IA);
                        Image.Pixels_RGBA[IDX__ + 3] = (byte)(WL.Math.Min(255, Color.A + DstA));
                        break;    
                    }
                }

                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ColorB GetPixel(uint X, uint Y){
                int IDX__ = IDX(X, Y);
                return new ColorB(Image.Pixels_RGBA[IDX__], Image.Pixels_RGBA[IDX__ + 1], Image.Pixels_RGBA[IDX__ + 2], Image.Pixels_RGBA[IDX__ + 3]);
            }
            
            /// <summary>
            /// Заполняет всё пространство цветом
            /// </summary>
            /// <param name="Color">Цвет</param>
            /// <param name="Blend">Смешивание</param>
            public ImageContext Fill(ColorB? Color = null, ImageBlend Blend = ImageBlend.Fixed){
                try{
                    __CanChange();
                    __TryCreatePixels();
                    ColorB Color__ = Color ?? ColorB.White;

                    For(((X, Y, W, H) => {
                        SetPixel(X, Y, Color__, Blend);
                    }));
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при заполнении цветом изображения [" + this + "]!\nЦвет: " + Color, e);
                }
            }

            /// <summary>
            /// Пустой цвет
            /// </summary>
            public readonly ColorB EmptyColor = ColorB.Transparent;
            
            /// <summary>
            /// Заполняет всё пространство прозрачным цветом
            /// </summary>
            public ImageContext Clear() => Fill(EmptyColor);

            /// <summary>
            /// Закрашивает определённую область
            /// </summary>
            /// <param name="X">X</param>
            /// <param name="Y">Y</param>
            /// <param name="Width">Ширина</param>
            /// <param name="Height">Высота</param>
            /// <param name="Color">Цвет</param>
            /// <param name="Blend">Смешивание</param>
            public ImageContext Fill(int X, int Y, uint Width, uint Height, ColorB? Color = null, ImageBlend Blend = ImageBlend.Fixed){
                try{
                    __CanChange();
                    ColorB Color__ = Color ?? ColorB.White;

                    if(OutOfBounds(X, Y)){ return this; }

                    int EndX = (int)(X + Width );
                    int EndY = (int)(Y + Height);

                    if(EndX > this.Width ){ EndX = (int)this.Width ; }
                    if(EndY > this.Height){ EndY = (int)this.Height; }

                    if(X >= EndX || Y >= EndY){ return this; }

                    for(int Y__ = Y; Y__ < EndY; Y__++){
                        for(int X__ = X; X__ < EndX; X__++){
                            SetPixel((uint)X__, (uint)Y__, Color__, Blend);
                        }   
                    }
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при заполнении цветом области изображения [" + this + "]!\nX: " + X + "\nY: " + Y + "\nШирина: " + Width + "\nВысота: " + Height + "\nЦвет: " + Color, e);
                }
            }

            /// <summary>
            /// Рисует рамку
            /// </summary>
            /// <param name="X">X</param>
            /// <param name="Y">Y</param>
            /// <param name="Width">Ширина</param>
            /// <param name="Height">Высота</param>
            /// <param name="Thickness">Толщина</param>
            /// <param name="Color">Цвет</param>
            /// <param name="Blend">Смешивание</param>
            public ImageContext Border(int X, int Y, uint Width, uint Height, uint Thickness = 1, ColorB? Color = null, ImageBlend Blend = ImageBlend.Fixed){
                try{
                    __CanChange();
                    
                    if(Thickness == 0){ return this; }

                    ColorB Color__ = Color ?? ColorB.White;

                    Fill(X, Y, Width, Thickness, Color__, Blend);

                    if(Height > Thickness){ Fill(X, (int)(Y + Height - Thickness), Width, Thickness, Color__, Blend); }

                    Fill(X, (int)(Y + Thickness), Thickness, Height - 2 * Thickness, Color__, Blend);
                    
                    if(Width > Thickness){ Fill((int)(X + Width - Thickness), (int)(Y + Thickness), Thickness, Height - 2 * Thickness, Color__, Blend); }
                    
                    return this;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при рисовании рамки у изображения [" + this + "]!\nX: " + X + "\nY: " + Y + "\nШирина: " + Width + "\nВысота: " + Height + "\nТолщина: " + Thickness + "\nЦвет: " + Color, e);
                }
            }
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