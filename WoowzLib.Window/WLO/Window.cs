using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class Window : IWindow{
    /// <summary>
    /// Создаёт окно
    /// </summary>
    /// <param name="Title">Стартовое название окна</param>
    /// <param name="Width">Стартовая ширина окна</param>
    /// <param name="Height">Стартовая высота окна</param>
    public Window(string Title = "WL Window", uint Width = 800, uint Height = 600){
        try{
            const string WindowClassName = "WoowzLib_Window";
            
            IntPtr Instance = WL.System.Native.Windows.GetModuleHandle(null);

            __Events__ = new System.Native.Windows.WndProcDelegate(__StaticEvents);
            
            System.Native.Windows.WNDCLASSEX WindowClass = new System.Native.Windows.WNDCLASSEX{
                cbSize        = (uint)Marshal.SizeOf<System.Native.Windows.WNDCLASSEX>(),
                lpfnWndProc   = Marshal.GetFunctionPointerForDelegate(__Events__),
                hInstance     = Instance,
                lpszClassName = WindowClassName,
                hCursor       = IntPtr.Zero,
                hbrBackground = IntPtr.Zero
            };

            WL.System.Native.Windows.RegisterClassEx(ref WindowClass);

            Handle = WL.System.Native.Windows.CreateWindowExW(
                0,
                WindowClassName,
                Title,
                WL.System.Native.Windows.WS_OVERLAPPEDWINDOW | WL.System.Native.Windows.WS_VISIBLE,
                0, 0,
                (int)Width, (int)Height,
                IntPtr.Zero,
                IntPtr.Zero,
                Instance,
                IntPtr.Zero
            );

            if(Handle == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри CreateWindowEx!"); }
            
            __GC__ = GCHandle.Alloc(this);
            System.Native.Windows.SetWindowLongPtr(Handle, System.Native.Windows.GWLP_USERDATA, GCHandle.ToIntPtr(__GC__));
            
            WL.Window.Windows.Add(this);
            
            this.Width  = Width;
            this.Height = Height;
            this.Title  = Title;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }
    private GCHandle                              __GC__;
    private System.Native.Windows.WndProcDelegate __Events__;
    
    public void DestroyNow(){
        try{
            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов на уничтожение окна!", e);
            }

            if(Handle == IntPtr.Zero){ throw new Exception("Ссылка на окно пустая!"); }

            foreach(WindowElement Child in Children){
                Child.Destroy();
            }
            Children.Clear();
            
            WL.System.Native.Windows.DestroyWindow(Handle);
            
            Handle = IntPtr.Zero;
            ShouldDestroy = false;

            if(__GC__.IsAllocated){ __GC__.Free(); }

            WL.Window.Windows.Remove(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Окно должно уничтожиться?
    /// </summary>
    public bool ShouldDestroy{ get; private set; }

    /// <summary>
    /// Окно живое?
    /// </summary>
    public new bool Alive => Handle != IntPtr.Zero && !ShouldDestroy;
    
    public void Destroy(){
        ShouldDestroy = true;
    }

    #region Процессы окна

        private static IntPtr __StaticEvents(IntPtr Window, uint Message, IntPtr WParam, IntPtr LParam){
            try{
                IntPtr Link = System.Native.Windows.GetWindowLongPtr(Window, System.Native.Windows.GWLP_USERDATA);
                return Link != IntPtr.Zero ? ((Window)GCHandle.FromIntPtr(Link).Target!).__Events(Message, WParam, LParam) : System.Native.Windows.DefWindowProcW(Window, Message, WParam, LParam);
            }catch(Exception e){
                throw new Exception("Произошла ошибка у __StaticEvents!", e);
            }
        }

        /// <summary>
        /// Вызывает WinAPI ивенты для окна
        /// </summary>
        /// <param name="Message">Ивент</param>
        /// <param name="WParam">Параметр 1</param>
        /// <param name="LParam">Параметр 2</param>
        private IntPtr __Events(uint Message, IntPtr WParam, IntPtr LParam){
            try{
                long LP = LParam.ToInt64();
                int Word1 = (short) (LP        & 0xFFFF);
                int Word2 = (short)((LP >> 16) & 0xFFFF);
                
                switch(Message){
                    // Закрытие окна (через крестик например)
                    case System.Native.Windows.WM_CLOSE:
                        try{
                            OnClose?.Invoke(this);
                        }catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на закрытие окна на крестик [" + this + "]!", e);
                        }
                        if(DefaultOnClose){ Destroy(); }
                        return IntPtr.Zero;
                    
                    // Обновление размера у окна
                    case System.Native.Windows.WM_SIZE:
                        __Width  = (uint)(Word1);
                        __Height = (uint)(Word2);

                        try{
                            OnResize?.Invoke(this, __Width, __Height);
                        }catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на изменение размера окна [" + this + "]!", e);
                        }
                        break;
                    
                    // Обновление позиции окна
                    case System.Native.Windows.WM_WINDOWPOSCHANGED:
                        System.Native.Windows.WINDOWPOS Position = Marshal.PtrToStructure<System.Native.Windows.WINDOWPOS>(LParam);

                        if((Position.flags & System.Native.Windows.SWP_NOMOVE) == 0){
                            __X = Position.x;
                            __Y = Position.y;

                            try{
                                OnMove?.Invoke(this, __X, __Y);
                            }
                            catch(Exception e){
                                Logger.Error("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!", e);
                            }
                        }

                        break;
                    
                    // Обновление курсора внутри окна
                    case System.Native.Windows.WM_SETCURSOR:
                        int HitTest = (short)(LParam.ToInt64() & 0xFFFF);
                        if(HitTest == System.Native.Windows.HTCLIENT){
                            System.Native.Windows.SetCursor(System.Native.Windows.CURSOR_Arrow);
                        }
                        
                        break;
                    
                    // Рисование внутри окна
                    case System.Native.Windows.WM_PAINT:
                        IntPtr HDC = System.Native.Windows.BeginPaint(Handle, out System.Native.Windows.PAINTSTRUCT PS);

                        try{
                            System.Native.Windows.GetClientRect(Handle, out System.Native.Windows.RECT ClientRect);
                            IntPtr Brush = System.Native.Windows.CreateSolidBrush(0x00808080); // пока-что так

                            System.Native.Windows.FillRect(HDC, ref ClientRect, Brush);
                            System.Native.Windows.DeleteObject(Brush);
                        }finally{
                            System.Native.Windows.EndPaint(Handle, ref PS);    
                        }
                        
                        break;
                }

                return System.Native.Windows.DefWindowProcW(Handle, Message, WParam, LParam);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обработке ивентов [" + this + "]!", e);
            }
        }
        
    #endregion

    #region Ивенты

        /// <summary>
        /// Вызывается при закрытии окна (на крестик на пример) [Окно]
        /// </summary>
        public event Action<Window>? OnClose;

        /// <summary>
        /// Использовать дефолтный ивент при закрытии окна? (на крестик к примеру)
        /// </summary>
        public bool DefaultOnClose = true;

        /// <summary>
        /// Вызывается при уничтожении окна [Окно]
        /// </summary>
        public event Action<Window>? OnDestroy;

        /// <summary>
        /// Вызывается когда меняется размер у окна [Окно, новая ширина, новая высота]
        /// </summary>
        public event Action<Window, uint, uint>? OnResize;

        /// <summary>
        /// Вызывается когда окно сдвинулось [Окно, новая X, новая Y]
        /// </summary>
        public event Action<Window, int, int>? OnMove;
        
    #endregion

    #region Дети

    private readonly List<WindowElement> Children = [];

    public Window Add(WindowElement Element){
        try{
            CheckDestroyed();
            
            if(Element.Parent != IntPtr.Zero){ throw new Exception("Этот элемент уже привязан к какому-то окну! Ссылка на окно: " + Element.Parent); }
            
            Element.__SetParent(this);
            
            Children.Add(Element);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении элемента [" + Element + "] окну [" + this + "]!", e);
        }

        return this;
    }

    #endregion
    
    public string Title{
        get => __Title;
        set{
            try{
                CheckDestroyed();
                
                if(__Title == value){ return; }
                __Title = value;

                System.Native.Windows.SetWindowText(Handle, __Title);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении названия у окна [" + this + "]!\nНазвание: \"" + value + "\"", e);
            }
        }
    }
    private string __Title;
}

public abstract class IWindow{
    /// <summary>
    /// Ссылка на окно
    /// </summary>
    public IntPtr Handle{ get; protected set; }

    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Handle != IntPtr.Zero;
    
    /// <summary>
    /// Делает проверку, уничтожено окно или нет?
    /// </summary>
    public void CheckDestroyed(){ if(!Alive){ throw new Exception("Пародия окна [" + this + "] уничтожена!"); } }
    
    /// <summary>
    /// Обновляет размер окна
    /// </summary>
    private void __UpdateSize(){
        try{
            System.Native.Windows.RECT Rect = new System.Native.Windows.RECT{
                left   = 0,
                top    = 0,
                right  = (int)__Width,
                bottom = (int)__Height
            };

            System.Native.Windows.AdjustWindowRectEx(ref Rect, System.Native.Windows.WS_OVERLAPPEDWINDOW, false, 0);

            System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, 0, 0, Rect.right - Rect.left, Rect.bottom - Rect.top, System.Native.Windows.SWP_NOZORDER | System.Native.Windows.SWP_NOMOVE);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении размера у пародии окна [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Обновляет позицию окна
    /// </summary>
    private void __UpdatePosition(){
        try{
            System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, __X, __Y, 0, 0, System.Native.Windows.SWP_NOZORDER | System.Native.Windows.SWP_NOSIZE);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении позиции у пародии окна [" + this + "]!", e);
        }
    }
    
    public uint Width{
        get => __Width;
        set{
            try{
                CheckDestroyed();
                
                if(__Width == value){ return; }
                __Width = value;

                __UpdateSize();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении ширины у пародии окна [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    protected uint __Width;
    
    public uint Height{
        get => __Height;
        set{
            try{
                CheckDestroyed();
                
                if(__Height == value){ return; }
                __Height = value;
                
                __UpdateSize();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении высоты у пародии окна [" + this + "]!\nВысота: " + value, e);
            }
        }
    }
    protected uint __Height;

    public int X{
        get => __X;
        set{
            try{
                CheckDestroyed();

                if(__X == value){ return; }
                __X = value;
                
                __UpdatePosition();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по X у пародии окна [" + this + "]!\nX: " + value, e);
            }
        }
    }
    protected int __X;
    
    public int Y{
        get => __Y;
        set{
            try{
                CheckDestroyed();

                if(__Y == value){ return; }
                __Y = value;
                
                __UpdatePosition();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении позиции по Y у пародии окна [" + this + "]!\nY: " + value, e);
            }
        }
    }
    protected int __Y;
}