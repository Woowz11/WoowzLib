using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace WLO;

public class Image : IDisposable{
    /// <summary>
    /// Создаёт изображение
    /// </summary>
    /// <param name="Width">Ширина</param>
    /// <param name="Height">Высота</param>
    /// <param name="Pixels">Принимает только RGBA!</param>
    public Image(uint Width, uint Height, byte[]? Pixels = null){
        __Width = Width;
        __Height = Height;
        __Pixels_RGBA = Pixels == null ? new byte[Width * Height * 4] : (byte[])Pixels.Clone();

        __Update(true);
    }

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
    /// Цвета (R...G...B...A)
    /// </summary>
    public byte[] Pixels_RGBA => __Pixels_RGBA;
    private byte[] __Pixels_RGBA;

    /// <summary>
    /// Цвета (B...G...R...A)
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

                for(int i = 0; i < __Pixels_RGBA.Length; i += 4){
                    __Pixels_BGRA[i + 0] = __Pixels_RGBA[i + 2];
                    __Pixels_BGRA[i + 1] = __Pixels_RGBA[i + 1];
                    __Pixels_BGRA[i + 2] = __Pixels_RGBA[i + 0];
                    __Pixels_BGRA[i + 3] = __Pixels_RGBA[i + 3];
                }

            #endregion

            #region DC

                if(Create){
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
                
                Marshal.Copy(__Pixels_BGRA, 0, __PixelsStart, __Pixels_BGRA.Length);

            #endregion
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации BGRA у изображения [" + this + "]!", e);
        }
    }

    private float __R;
    private float __G;
    private float __B;
    public void __ApplyColor(float R, float G, float B){
        if(WL.Math.IsNear(__R, R) && WL.Math.IsNear(__G, G) && WL.Math.IsNear(__B, B)){ return; }
        __R = R; __G = G; __B = B;
        
        unsafe{
            byte* Link       = (byte*)__PixelsStart;
            byte* Source     = (byte*)Unsafe.AsPointer(ref __Pixels_BGRA[0]);
            int   PixelCount = (int)(__Width * __Height);
            
            // мб добавить систему выполнения на gpu? сделать проверку есть ли вулкан, и делать так...
            
            Parallel.For(0, PixelCount, i => {
                int IDX      = i * 4;
                Link[IDX + 0] = (byte)(Source[IDX + 0] * B);
                Link[IDX + 1] = (byte)(Source[IDX + 1] * G);
                Link[IDX + 2] = (byte)(Source[IDX + 2] * R);
                Link[IDX + 3] =        Source[IDX + 3]     ;
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