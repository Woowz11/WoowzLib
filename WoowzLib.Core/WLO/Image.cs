using System.Runtime.InteropServices;

namespace WLO;

[Obsolete("Думаю надо сделать так, что-бы он всегда был RGBA, без BitsPerPixel")]
public class Image : IDisposable{
    public Image(uint Width, uint Height, ushort BitsPerPixel, byte[]? Pixels = null){
        __Width = Width;
        __Height = Height;
        __BitsPerPixel = BitsPerPixel;
        __Pixels_RGBA = Pixels == null ? new byte[Width * Height * BitsPerPixel] : (byte[])Pixels.Clone();

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
    /// Кол-во каналов (байтов на пиксель)
    /// </summary>
    public ushort Channels => (ushort)(__BitsPerPixel / 8);
    
    /// <summary>
    /// Кол-во бит на пиксель (1: Чёрный и белый (0.125), 8: Градации чёрно-белого (1), 24: RGB (3), 32: RGBA (4))
    /// </summary>
    public ushort BitsPerPixel => __BitsPerPixel;
    private ushort __BitsPerPixel;

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

                for(int i = 0; i < __Pixels_RGBA.Length; i += Channels){
                    switch(Channels){
                        case 1:
                            __Pixels_BGRA[i + 0] = __Pixels_RGBA[i + 0];
                            break;

                        case 2:
                            __Pixels_BGRA[i + 0] = __Pixels_RGBA[i + 1];
                            __Pixels_BGRA[i + 1] = __Pixels_RGBA[i + 0];
                            break;

                        case 3:
                            __Pixels_BGRA[i + 0] = __Pixels_RGBA[i + 2];
                            __Pixels_BGRA[i + 1] = __Pixels_RGBA[i + 1];
                            __Pixels_BGRA[i + 2] = __Pixels_RGBA[i + 0];
                            break;

                        case 4:
                            __Pixels_BGRA[i + 0] = __Pixels_RGBA[i + 2];
                            __Pixels_BGRA[i + 1] = __Pixels_RGBA[i + 1];
                            __Pixels_BGRA[i + 2] = __Pixels_RGBA[i + 0];
                            __Pixels_BGRA[i + 3] = __Pixels_RGBA[i + 3];
                            break;

                        default:
                            throw new Exception("Неподдерживаемое количество каналов! Каналов: " + Channels);
                    }

                }

            #endregion

            #region DC

                if(Create){
                    __HDC = WL.System.Native.Windows.CreateCompatibleDC(IntPtr.Zero);

                    WL.System.Native.Windows.BITMAPINFO BMI = new WL.System.Native.Windows.BITMAPINFO();
                    BMI.bmiHeader.biSize        = (uint)WL.System.Byte.Size<WL.System.Native.Windows.BITMAPINFOHEADER>();
                    BMI.bmiHeader.biWidth       =  (int)Width;
                    BMI.bmiHeader.biHeight      = -(int)Height;
                    BMI.bmiHeader.biPlanes      = 1;
                    BMI.bmiHeader.biBitCount    = BitsPerPixel;
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

    public void __ApplyColor(float R, float G, float B){
        unsafe{
            byte* Link = (byte*)__PixelsStart;
            int PixelCount = (int)(Width * Height);

            switch(Channels){
                case 1:
                    for(int i = 0; i < PixelCount * 1; i += 1){
                        Link[i + 0] = (byte)(Pixels_BGRA[i + 0] * R);
                    }

                    break;

                case 2:
                    for(int i = 0; i < PixelCount * 2; i += 2){
                        Link[i + 0] = (byte)(Pixels_BGRA[i + 0] * R);
                        Link[i + 1] = Pixels_BGRA[i + 1];
                    }

                    break;

                case 3:
                    for(int i = 0; i < PixelCount * 3; i += 3){
                        Link[i + 0] = (byte)(Pixels_BGRA[i + 0] * B);
                        Link[i + 1] = (byte)(Pixels_BGRA[i + 1] * G);
                        Link[i + 2] = (byte)(Pixels_BGRA[i + 2] * R);
                    }

                    break;
                
                case 4:
                    for(int i = 0; i < PixelCount * 4; i += 4){
                        Link[i + 0] = (byte)(Pixels_BGRA[i + 0] * B);
                        Link[i + 1] = (byte)(Pixels_BGRA[i + 1] * G);
                        Link[i + 2] = (byte)(Pixels_BGRA[i + 2] * R);
                        Link[i + 3] =        Pixels_BGRA[i + 3];
                    }

                    break;

                default:
                    throw new Exception("Неподдерживаемое количество каналов! Каналов: " + Channels);
            }
        }
    }
    
    public override string ToString() => "Image(" + __Width + "x" + __Height + "x" + Channels + ")";
    
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