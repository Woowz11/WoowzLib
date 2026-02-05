using System.Runtime.InteropServices;

namespace WLO;

public class Image : IDisposable{
    public Image(uint Width, uint Height, ushort BitsPerPixel, byte[]? Pixels = null){
        __Width = Width;
        __Height = Height;
        __BitsPerPixel = BitsPerPixel;
        __PixelsRGBA = Pixels == null ? new byte[Width * Height * BitsPerPixel] : (byte[])Pixels.Clone();

        __Update(true);
    }

    public uint Width => __Width;
    private uint __Width;

    public uint Height => __Height;
    private uint __Height;

    public ushort BitsPerPixel => __BitsPerPixel;
    private ushort __BitsPerPixel;

    public byte[] PixelsRGBA => __PixelsRGBA;
    private byte[] __PixelsRGBA;

    public byte[] PixelsBGRA => __PixelsBGRA;
    private byte[] __PixelsBGRA;

    /// <summary>
    /// Кол-во каналов (байтов на пиксель)
    /// </summary>
    public ushort Channels => (ushort)(__BitsPerPixel / 8);
    
    private IntPtr __HDCMem = IntPtr.Zero;
    private IntPtr __HDIB = IntPtr.Zero;
    private IntPtr __PtrBits = IntPtr.Zero;
    private IntPtr __OldBMP = IntPtr.Zero;
    
    public IntPtr HDCMem => __HDCMem;
    public IntPtr HDIB => __HDIB;
    public IntPtr PtrBits => __PtrBits;
    
    private void __Update(bool Create = false){
        try{
            #region PixelsBGRA

                if(__PixelsBGRA == null || __PixelsRGBA.Length != __PixelsBGRA.Length){ __PixelsBGRA = new byte[__PixelsRGBA.Length]; }

                for(int i = 0; i < __PixelsRGBA.Length; i += Channels){
                    switch(Channels){
                        case 1:
                            __PixelsBGRA[i + 0] = __PixelsRGBA[i + 0];
                            break;

                        case 2:
                            __PixelsBGRA[i + 0] = __PixelsRGBA[i + 1];
                            __PixelsBGRA[i + 1] = __PixelsRGBA[i + 0];
                            break;

                        case 3:
                            __PixelsBGRA[i + 0] = __PixelsRGBA[i + 2];
                            __PixelsBGRA[i + 1] = __PixelsRGBA[i + 1];
                            __PixelsBGRA[i + 2] = __PixelsRGBA[i + 0];
                            break;

                        case 4:
                            __PixelsBGRA[i + 0] = __PixelsRGBA[i + 2];
                            __PixelsBGRA[i + 1] = __PixelsRGBA[i + 1];
                            __PixelsBGRA[i + 2] = __PixelsRGBA[i + 0];
                            __PixelsBGRA[i + 3] = __PixelsRGBA[i + 3];
                            break;

                        default:
                            throw new Exception("Неподдерживаемое количество каналов!\nКаналов: " + Channels);
                    }

                }

            #endregion

            #region DC

                if(Create){
                    if (__HDCMem != IntPtr.Zero) return; // уже создано

                    __HDCMem = WL.System.Native.Windows.CreateCompatibleDC(IntPtr.Zero);

                    WL.System.Native.Windows.BITMAPINFO bmi = new WL.System.Native.Windows.BITMAPINFO();
                    bmi.bmiHeader.biSize = (uint)Marshal.SizeOf<WL.System.Native.Windows.BITMAPINFOHEADER>();
                    bmi.bmiHeader.biWidth = (int)Width;
                    bmi.bmiHeader.biHeight = (int)Height; // top-down
                    bmi.bmiHeader.biPlanes = 1;
                    bmi.bmiHeader.biBitCount = 32;
                    bmi.bmiHeader.biCompression = WL.System.Native.Windows.BI_RGB;
                    bmi.bmiHeader.biSizeImage = (uint)(Width * Height * 4);

                    __HDIB = WL.System.Native.Windows.CreateDIBSection(__HDCMem, ref bmi, 0, out __PtrBits, IntPtr.Zero, 0);
                    __OldBMP = WL.System.Native.Windows.SelectObject(__HDCMem, __HDIB);
                }
                
                Marshal.Copy(__PixelsBGRA, 0, __PtrBits, __PixelsBGRA.Length);

            #endregion
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации BGRA у изображения [" + this + "]!", e);
        }
    }
    
    public override string ToString() => "Image(" + __Width + "x" + __Height + "x" + Channels + ")";
    
    public void Dispose()
    {
        if (__HDCMem != IntPtr.Zero)
        {
            if (__HDIB != IntPtr.Zero) WL.System.Native.Windows.SelectObject(__HDCMem, __OldBMP);
            WL.System.Native.Windows.DeleteDC(__HDCMem);
            __HDCMem = IntPtr.Zero;
            __HDIB = IntPtr.Zero;
            __PtrBits = IntPtr.Zero;
        }
    }
}