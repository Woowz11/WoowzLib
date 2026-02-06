using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml;

namespace WLO;

public class Image : IDisposable{
    /// <summary>
    /// Создаёт изображение
    /// </summary>
    /// <param name="Width">Ширина</param>
    /// <param name="Height">Высота</param>
    /// <param name="Pixels">Принимает только RGBA!</param>
    public Image(uint Width, uint Height, byte[]? Pixels = null){
        __Context = new ImageContext(this);
        
        __Width = Width;
        __Height = Height;
        __Pixels_RGBA = Pixels == null ? new byte[Width * Height * 4] : (byte[])Pixels.Clone();

        __Update(true);
    }

    private ImageContext __Context;
    
    /// <summary>
    /// Ширина
    /// </summary>
    public uint Width => __Width;
    private uint __Width;

    /// <summary>
    /// Высота
    /// </summary>
    public uint Height => __Height;
    private uint __Height;

    /// <summary>
    /// Цвета
    /// </summary>
    public byte[] Pixels_RGBA => __Pixels_RGBA;
    private byte[] __Pixels_RGBA;

    /// <summary>
    /// Цвета
    /// </summary>
    public byte[] Pixels_BGRA => __Pixels_BGRA;
    private byte[] __Pixels_BGRA;
    
    public IntPtr __HDC         = IntPtr.Zero;
    public IntPtr __DIB         = IntPtr.Zero;
    public IntPtr __PixelsStart = IntPtr.Zero;
    public IntPtr __OldBitMap   = IntPtr.Zero;
    
    private void __Update(bool Create = false){
        try{
            #region PixelsBGRA

                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if(__Pixels_BGRA == null || __Pixels_RGBA.Length != __Pixels_BGRA.Length){ __Pixels_BGRA = new byte[__Pixels_RGBA.Length]; }

                Parallel.For(0, __Pixels_RGBA.Length / 4, i => {
                    int IDX = i * 4;
                    __Pixels_BGRA[IDX + 0] = __Pixels_RGBA[IDX + 2];
                    __Pixels_BGRA[IDX + 1] = __Pixels_RGBA[IDX + 1];
                    __Pixels_BGRA[IDX + 2] = __Pixels_RGBA[IDX + 0];
                    __Pixels_BGRA[IDX + 3] = __Pixels_RGBA[IDX + 3];
                });

            #endregion

            #region DC

                if(Create && __HDC == IntPtr.Zero){
                    __HDC = WL.System.Native.Windows.CreateCompatibleDC(IntPtr.Zero);

                    WL.System.Native.Windows.BITMAPINFO BMI = new WL.System.Native.Windows.BITMAPINFO();
                    BMI.bmiHeader.biSize        = (uint)WL.Math.Byte.Size<WL.System.Native.Windows.BITMAPINFOHEADER>();
                    BMI.bmiHeader.biWidth       =  (int)Width;
                    BMI.bmiHeader.biHeight      = -(int)Height;
                    BMI.bmiHeader.biPlanes      = 1;
                    BMI.bmiHeader.biBitCount    = 4 * 8;
                    BMI.bmiHeader.biCompression = WL.System.Native.Windows.BI_RGB;
                    BMI.bmiHeader.biSizeImage   = (uint)(__Pixels_BGRA.Length);
                    
                    __DIB       = WL.System.Native.Windows.CreateDIBSection(__HDC, ref BMI, 0, out __PixelsStart, IntPtr.Zero, 0);
                    __OldBitMap = WL.System.Native.Windows.SelectObject    (__HDC, __DIB);
                }

                if(__PixelsStart != IntPtr.Zero){ Marshal.Copy(__Pixels_BGRA, 0, __PixelsStart, __Pixels_BGRA.Length); }

                #endregion
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации BGRA у изображения [" + this + "]!", e);
        }
    }

    public Image Change(Action<ImageContext> Action){
        try{
            if(__CanChange){ throw new Exception("Изображение уже изменяется!"); }
            __CanChange = true;
            Action(__Context);
            __Update();
            __CanChange = false;
            
            return this;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при изменении изображения [" + this + "]!", e);
        }
    }
    private bool __CanChange = false;
    
    public class ImageContext(Image Image){
        private readonly Image __Image     = Image;
        private          bool  __CanChange => __Image.__CanChange;

        public ColorB this[uint X, uint Y]{
            get => GetPixel(X, Y);
            set => SetPixel(X, Y, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IDX(uint X, uint Y) => ((int)Y * (int)__Image.Width + (int)X) * 4;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ImageContext SetPixel(uint X, uint Y, ColorB Color){
            int IDX__ = IDX(X, Y);
            __Image.__Pixels_RGBA[IDX__ + 0] = Color.R;
            __Image.__Pixels_RGBA[IDX__ + 1] = Color.G;
            __Image.__Pixels_RGBA[IDX__ + 2] = Color.B;
            __Image.__Pixels_RGBA[IDX__ + 3] = Color.A;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorB GetPixel(uint X, uint Y){
            int IDX__ = IDX(X, Y);
            return new ColorB(__Image.__Pixels_RGBA[IDX__], __Image.__Pixels_RGBA[IDX__ + 1], __Image.__Pixels_RGBA[IDX__ + 2], __Image.__Pixels_RGBA[IDX__ + 3]);
        }
    }

    private byte __R;
    private byte __G;
    private byte __B;
    public void __ApplyColor(byte R, byte G, byte B){
        if(__R == R && __G == G && __B == B){ return; }
        __R = R; __G = G; __B = B;
        
        unsafe{
            byte* Link       = (byte*)__PixelsStart;
            byte* Source     = (byte*)Unsafe.AsPointer(ref __Pixels_BGRA[0]);
            int   PixelCount = (int)(__Width * __Height);
            
            // мб добавить систему выполнения на gpu? сделать проверку есть ли вулкан, и делать так...
            
            Parallel.For(0, PixelCount, i => {
                int IDX       = i * 4;
                Link[IDX + 0] = (byte)((Source[IDX + 0] * B) / 255);
                Link[IDX + 1] = (byte)((Source[IDX + 1] * G) / 255);
                Link[IDX + 2] = (byte)((Source[IDX + 2] * R) / 255);
                Link[IDX + 3] =         Source[IDX + 3]            ;
            });
        }
    }
    
    public override string ToString() => "Image(" + __Width + "x" + __Height + ")";
    
    public void Dispose(){
        if (__HDC != IntPtr.Zero){
            if (__DIB != IntPtr.Zero){ WL.System.Native.Windows.SelectObject(__HDC, __OldBitMap); }
            WL.System.Native.Windows.DeleteDC(__HDC);
            __HDC = IntPtr.Zero;
            __DIB = IntPtr.Zero;
            __PixelsStart = IntPtr.Zero;
        }
    }
}