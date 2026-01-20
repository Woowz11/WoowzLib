using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class Window : WindowContext{
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
            
            System.Native.Windows.WNDCLASSEX WindowClass = new System.Native.Windows.WNDCLASSEX{
                cbSize        = (uint)Marshal.SizeOf<System.Native.Windows.WNDCLASSEX>(),
                lpfnWndProc   = Marshal.GetFunctionPointerForDelegate(new System.Native.Windows.WndProcDelegate(System.Native.Windows.EmptyWindowProc)),
                hInstance     = Instance,
                lpszClassName = WindowClassName,
                hCursor       = IntPtr.Zero,
                hbrBackground = IntPtr.Zero
            };

            WL.System.Native.Windows.RegisterClassExW(ref WindowClass);

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
            
            __Events__ = System.Native.ConnectEventsToWindow(Handle, __Events);
            
            WL.Window.Windows.Add(this);
            
            this.Width  = Width;
            this.Height = Height;
            this.Title  = Title;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly System.Native.Windows.WndProcDelegate __Events__;
    
    public void DestroyNow(){
        try{
            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов на уничтожение окна [" + this + "]!", e);
            }

            if(Handle == IntPtr.Zero){ throw new Exception("Ссылка на окно пустая!"); }
            
            foreach(WindowElement Child in Children){
                Child.Destroy();
            } 
            Children.Clear();
            
            WL.System.Native.Windows.DestroyWindow(Handle);
            
            Handle = IntPtr.Zero;
            ShouldDestroy = false;

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
    
        /// <summary>
        /// Вызывает WinAPI ивенты для окна
        /// </summary>
        /// <param name="Message">Ивент</param>
        /// <param name="WParam">Параметр 1</param>
        /// <param name="LParam">Параметр 2</param>
        public override IntPtr __Events(IntPtr OtherWindow, uint Message, IntPtr WParam, IntPtr LParam){
            try{
                long LP = (long)LParam;
                short   LWord_L = (short) (LP        & 0xFFFF);
                short   HWord_L = (short)((LP >> 16) & 0xFFFF);

                ulong WP = (ulong)WParam;
                ushort LWord_W = (ushort)(WP & 0xFFFF);
                ushort HWord_W = (ushort)(WP >> 16   );
                
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
                        __Width  = (uint)(LWord_L);
                        __Height = (uint)(HWord_L);

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
                        break;
                    
                    // Обработка элементов у окна
                    case System.Native.Windows.WM_COMMAND:
                        return __UpdateCommand(WParam, LParam, Children);
                }

                return System.Native.Windows.DefWindowProcW(Handle, Message, WParam, LParam);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обработке ивентов [" + this + "]!", e);
            }
        }

        public override void Render(IntPtr HDC){
            System.HDC.Fill(HDC, System.HDC.WindowSize(Handle), ColorF.Orange.ToRGBiA());
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

        /// <summary>
        /// Привязанные элементы к окну
        /// </summary>
        private readonly List<WindowElement> Children = [];

        /// <summary>
        /// Добавить элемент к окну
        /// </summary>
        /// <param name="Element">Элемент</param>
        public override void Add(WindowElement Element){
            try{
                CheckDestroyed();
                
                if(Element.Parent != null){ throw new Exception("Этот элемент уже привязан к какому-то окну! Ссылка на окно: " + Element.Parent); }
                
                Element.__SetParent(this);
                
                Children.Add(Element);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при добавлении элемента [" + Element + "] окну [" + this + "]!", e);
            }
        }

    #endregion

    /// <summary>
    /// Обновляет рендер у окна и его детей, нужен для корректного отображения элементов и всего
    /// </summary>
    public void UpdateRender(){ IntPtr HDC = System.Native.Windows.GetDC(Handle); __UpdateRender(HDC, Children); System.Native.Windows.ReleaseDC(Handle, HDC); }
    
    /// <summary>
    /// Обновляет размер окна
    /// </summary>
    public override void __UpdateSize(){
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
    /// Обновляет позиции у элементов по Z
    /// </summary>
    public void __UpdateZOrder(){ __UpdateZOrder(Children); }
    
    public string Title{
        get => __Title;
        set{
            try{
                CheckDestroyed();
                
                if(__Title == value){ return; }
                __Title = value;

                System.Native.Windows.SetWindowTextW(Handle, __Title);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении названия у окна [" + this + "]!\nНазвание: \"" + value + "\"", e);
            }
        }
    }
    private string __Title;
}

public abstract class WindowContext{
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
    /// Добавить элемент к окну
    /// </summary>
    /// <param name="Element">Элемент</param>
    public abstract void Add(WindowElement Element);
        
    /// <summary>
    /// Добавляет элементы к окну
    /// </summary>
    /// <param name="Elements">Элементы</param>
    public void Add(params WindowElement[] Elements){
        try{
            CheckDestroyed();
                
            if(Elements == null){ throw new Exception("Не указаны элементы!"); }

            foreach(WindowElement Element in Elements){
                Add(Element);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении элементов [" + Elements + "] окну [" + this + "]!", e);
        }
    }
    
    public abstract IntPtr __Events(IntPtr OtherWindow, uint Message, IntPtr WParam, IntPtr LParam);

    public IntPtr __UpdateCommand(IntPtr WParam, IntPtr LParam, List<WindowElement> Children){
        ulong WP = (ulong)WParam;
        ushort NotifyCode = (ushort)(WP & 0xFFFF);
        ushort ElementID  = (ushort)(WP >> 16   );
                        
        IntPtr ElementHandle = LParam;
        
        WindowElement? Element = Children.FirstOrDefault(E => E.Handle == ElementHandle);

        /*if(Element is Button Button){
            if(NotifyCode == System.Native.Windows.BN_CLICKED){
                Button.__InvokeOnClick();
            }
        }*/
        
        return IntPtr.Zero;
    }
    
    /// <summary>
    /// Обновляет размер окна
    /// </summary>
    public virtual void __UpdateSize(){
        try{
            System.Native.Windows.RECT Rect = new System.Native.Windows.RECT{
                left   = 0,
                top    = 0,
                right  = (int)__Width,
                bottom = (int)__Height
            };

            System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, 0, 0, Rect.right - Rect.left, Rect.bottom - Rect.top, System.Native.Windows.SWP_NOZORDER | System.Native.Windows.SWP_NOMOVE);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении размера у пародии окна [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Обновляет позицию окна
    /// </summary>
    protected void __UpdatePosition(){
        try{
            System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, __X, __Y, 0, 0, System.Native.Windows.SWP_NOZORDER | System.Native.Windows.SWP_NOSIZE);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении позиции у пародии окна [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Обновляет позиции у элементов по Z
    /// </summary>
    [Obsolete("надо доделать будет", false)]
    public void __UpdateZOrder(List<WindowElement> Children){
        try{
            List<WindowElement> Sorted = Children
                 .Select((K, V) => new{ WE = K, I = V })
                 .OrderBy(KVP => KVP.WE.Z)
                 .ThenBy (KVP => KVP.I)
                 .Select (KVP => KVP.WE)
                 .ToList();

            for(int i = 0; i < Sorted.Count; i++){
                WindowElement WE = Sorted[i];
                try{
                    if(!WE.Created){ continue; }
                    if(!WE.Alive){ throw new Exception("Элемент уничтоженный!"); }
                    
                    IntPtr InsertAfter = i == 0 ? Handle : Sorted[i - 1].Handle;

                    WL.System.Native.Windows.SetWindowPos(WE.Handle, InsertAfter, 0, 0, 0, 0, WL.System.Native.Windows.SWP_NOMOVE | System.Native.Windows.SWP_NOSIZE);

                }catch(Exception e){
                    throw new Exception("Произошла ошибка при обновлении позиции у элемента [" + WE + "]!", e);
                }
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении позиций у элементов по Z у окна [" + this + "]!", e);
        }
    }
    
    public abstract void Render(IntPtr HDC);

    public void __UpdateRender(IntPtr HDC, List<WindowElement> Children){
        try{
            if(!Alive){ throw new Exception("Окно не живо!"); }

            Render(HDC);
            
            foreach(WindowElement WE in Children){
                //System.Native.Windows.SaveDC(HDC);
                //System.Native.Windows.IntersectClipRect(HDC, X, Y, X + (int)Width, Y + (int)Height);
                
                System.Native.Windows.SetViewportOrgEx(HDC, WE.X, WE.Y, out System.Native.Windows.POINT _);
                
                WE.__UpdateRender(HDC);
                
                System.Native.Windows.SetViewportOrgEx(HDC, -WE.X, -WE.Y, out System.Native.Windows.POINT _);

                //System.Native.Windows.RestoreDC(HDC, -1);

            }  
        }catch(Exception e){
            throw new Exception("Произошла ошибка при рендере HDC у окна [" + Handle + "]!", e);
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