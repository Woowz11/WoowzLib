using System.Runtime.InteropServices;
using WL.Windows;

namespace WLO;

/// <summary>
/// Принимает RenderAPI для рендера в него изображения
/// </summary>
public abstract class Drawable{}

/// <summary>
/// Принимает RenderAPI для рендера в окно
/// </summary>
/// <param name="Handle"></param>
public sealed class DrawableWindow : Drawable{
    public DrawableWindow(IntPtr Handle){
        try{
            if(Handle == IntPtr.Zero){ throw new Exception("Окно пустое!"); }

            this.Handle = Handle;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании DrawableWindow [" + this + "]!\nОкно: " + Handle, e);
        }
    }
    
    public readonly IntPtr Handle;

    /// <summary>
    /// Пустой контекст (невидимое окно)
    /// </summary>
    public static DrawableWindow? Empty{ get; private set; }

    /// <summary>
    /// Создаёт невидимое окно
    /// </summary>
    public static void __CreateEmpty(){
        try{
            if(Empty != null){ throw new Exception("Окно уже создано!"); }

            ManualResetEvent? __Event = new ManualResetEvent(false);
            
            Thread WindowThread = new Thread(() => {
                Kernel.WNDCLASSEX WindowClass = new Kernel.WNDCLASSEX{
                    cbSize = (uint)Marshal.SizeOf<Kernel.WNDCLASSEX>(),
                    style = 0,
                    lpfnWndProc = Marshal.GetFunctionPointerForDelegate(new Kernel.WndProcDelegate(Kernel.WindowProc)),
                    cbClsExtra = 0,
                    cbWndExtra = 0,
                    hInstance = IntPtr.Zero,
                    hIcon = IntPtr.Zero,
                    hCursor = IntPtr.Zero,
                    hbrBackground = IntPtr.Zero,
                    lpszMenuName = null,
                    lpszClassName = "WoowzLib_EmptyDrawableWindowClass",
                    hIconSm = IntPtr.Zero
                };

                WL.Windows.Kernel.RegisterClassEx(ref WindowClass);

                IntPtr Window = WL.Windows.Kernel.CreateWindowEx(
                    0,
                    WindowClass.lpszClassName,
                    "KAKA",
                    WL.Windows.Kernel.WS_OVERLAPPEDWINDOW | WL.Windows.Kernel.WS_VISIBLE,
                    100, 100, 400, 300,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero
                );

                WL.Windows.Kernel.ShowWindow(Window, WL.Windows.Kernel.SW_SHOW);

                WL.Windows.Kernel.UpdateWindow(Window);

                Empty = new DrawableWindow(Window);

                __Event.Set();
                
                while(WL.Windows.Kernel.GetMessage(out Kernel.MSG Message, IntPtr.Zero, 0, 0)){
                    WL.Windows.Kernel.TranslateMessage(ref Message);
                    WL.Windows.Kernel.DispatchMessage(ref Message);
                }
            });
            WindowThread.IsBackground = true;
            
            WindowThread.Start();

            __Event.WaitOne();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании невидимого окна!", e);
        }
    }

    /// <summary>
    /// Уничтожает невидимое окно
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void __DestroyEmpty(){
        try{
            if(Empty == null){ return; }
            WL.Windows.Kernel.DestroyWindow(Empty.Handle);
            Empty = null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении невидимого окна!", e);
        }
    }
}