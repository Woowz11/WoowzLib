using System.Runtime.InteropServices;
using WL;

namespace WLO;

public class Window{
    /// <summary>
    /// Создаёт окно
    /// </summary>
    /// <param name="Title">Стартовое название окна</param>
    /// <param name="Width">Стартовая ширина окна</param>
    /// <param name="Height">Стартовая высота окна</param>
    /// <param name="BackgroundColor">Цвет заднего фона</param>
    public Window(string? Title = null, uint Width = 800, uint Height = 600, ColorB? BackgroundColor = null){
        try{
            Title ??= WL.WoowzLib.ProjectInfo.Name;

            const string WindowClassName = "WoowzLib_Window";
            
            IntPtr Instance = WL.System.Native.Windows.GetModuleHandle(null);

            __Events__ = WL.System.Native.Windows.EmptyWindowProc;
            
            WL.System.Native.Windows.WNDCLASSEX WindowClass = new WL.System.Native.Windows.WNDCLASSEX{
                cbSize        = (uint)Marshal.SizeOf<WL.System.Native.Windows.WNDCLASSEX>(),
                lpfnWndProc   = Marshal.GetFunctionPointerForDelegate(__Events__),
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

            ID = __IDs;
            __IDs++;
            
            __Events__ = WL.System.Native.ConnectEventsToWindow(Handle, __Events);
            
            WL.Window.Windows.Add(this);
            
            __Title  = Title;
            
            this.Width  = Width;
            this.Height = Height;

            if(BackgroundColor.HasValue){ this.BackgroundColor = BackgroundColor.Value; }

            __UpdateBuffer();
            
            RenderMessage("Не начат рендер!", ColorB.Red);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly WL.System.Native.Windows.WndProcDelegate __Events__;
    
    public void DestroyNow(){
        try{
            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов на уничтожение окна [" + this + "]!", e);
            }

            if(Handle == IntPtr.Zero){ throw new Exception("Ссылка на окно пустая!"); }

            foreach(WindowElement Child in __Children.ToArray()){
                Child.ToMemory();
            }
            
            WL.System.Native.Windows.DestroyWindow(Handle);
            
            Handle = IntPtr.Zero;
            ShouldDestroy = false;

            WL.Window.Windows.Remove(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Уникальное ID окна
    /// </summary>
    public readonly int ID;
    private static int __IDs;
    
    /// <summary>
    /// Ссылка на окно
    /// </summary>
    public IntPtr Handle{ get; protected set; }
    
    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive => Handle != IntPtr.Zero && !ShouldDestroy;
    
    /// <summary>
    /// Делает проверку, уничтожено окно или нет?
    /// </summary>
    public void CheckDestroyed(){ if(!Alive){ throw new Exception("Пародия окна [" + this + "] уничтожена!"); } }
    
    /// <summary>
    /// Окно должно уничтожиться?
    /// </summary>
    public bool ShouldDestroy{ get; private set; }
    
    public void Destroy(){
        ShouldDestroy = true;
    }

    #region Процессы окна

        private bool           __MouseInside;
        private WindowElement? __MouseElement;
        public void __Update(){
            try{
                if(ShouldDestroy){ DestroyNow(); return; }

                if(!WL.System.Native.Windows.GetCursorPos(out WL.System.Native.Windows.POINT P)){ WL.System.Native.Windows.ThrowWin32Error("Получение позиции мыши"); }
                Vector2I MousePosition = new Vector2I(P);
                this.MousePosition = ToClient(MousePosition);

                MouseElement = Hit(this.MousePosition);
                
                if(__MouseElement != MouseElement){
                    if(__MouseElement != null){
                        try{
                            __MouseElement.__InvokeOnMouseInside(false);
                        }catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на \"Мышь вошла в элемент [" + MouseElement + "]?\" [" + this + "]!\nВошла: false", e);
                        }
                    }

                    if(MouseElement != null){
                        try{
                            MouseElement.__InvokeOnMouseInside(true);
                        }catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на \"Мышь вошла в элемент [" + MouseElement + "]?\" [" + this + "]!\nВошла: true", e);
                        }
                    }
                    
                    __MouseElement = MouseElement;
                }
                
                MouseInside = Inside(MousePosition);
                
                if(__MouseInside != MouseInside){
                    __MouseInside = MouseInside;
                    try{
                        OnMouseInside?.Invoke(this, MouseInside);
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка при вызове ивентов на \"Мышь вошла в окно?\" [" + this + "]!\nВошла: " + MouseInside, e);
                    }
                }
                
                try{
                    OnUpdate?.Invoke(this);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на обновление окна [" + this + "]!", e);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обновлении окна [" + this + "]!", e);
            }
        }

        /// <summary>
        /// Вызывает WinAPI ивенты для окна
        /// </summary>
        /// <param name="Window">Вызываемое окно</param>
        /// <param name="Message">Ивент</param>
        /// <param name="WParam">Параметр 1</param>
        /// <param name="LParam">Параметр 2</param>
        private IntPtr __Events(IntPtr Window, uint Message, IntPtr WParam, IntPtr LParam){
            try{
                long  LP      = (long)LParam;
                short LWord_L = (short) (LP        & 0xFFFF);
                short HWord_L = (short)((LP >> 16) & 0xFFFF);

                ulong  WP      = (ulong)WParam;
                ushort LWord_W = (ushort)(WP & 0xFFFF);
                ushort HWord_W = (ushort)(WP >> 16   );
                
                switch(Message){
                    // Закрытие окна (через крестик например)
                    case WL.System.Native.Windows.WM_CLOSE:{
                        try{
                            OnClose?.Invoke(this);
                        }
                        catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на закрытие окна на крестик [" + this + "]!", e);
                        }

                        if(DefaultOnClose){ Destroy(); }
                        return IntPtr.Zero;
                    }
                    
                    // Обновление размера у окна
                    case WL.System.Native.Windows.WM_SIZE:{
                        __Width = (uint)(LWord_L);
                        __Height = (uint)(HWord_L);

                        __UpdateBuffer();

                        try{
                            OnResize?.Invoke(this, __Width, __Height);
                        }
                        catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на изменение размера окна [" + this + "]!\nШирина: " + __Width + "\nВысота: " + __Height, e);
                        }

                        break;
                    }
                    
                    // Обновление позиции окна
                    case WL.System.Native.Windows.WM_WINDOWPOSCHANGED:{
                        WL.System.Native.Windows.WINDOWPOS Position__ = Marshal.PtrToStructure<WL.System.Native.Windows.WINDOWPOS>(LParam);

                        if((Position__.flags & WL.System.Native.Windows.SWP_NOMOVE) == 0){
                            __X = Position__.x;
                            __Y = Position__.y;

                            try{
                                OnMove?.Invoke(this, __X, __Y);
                            }
                            catch(Exception e){
                                Logger.Error("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!\nX: " + __X + "\nY: " + __Y, e);
                            }
                        }

                        break;
                    }
                    
                    // Обновление курсора внутри окна
                    case WL.System.Native.Windows.WM_SETCURSOR:{
                        int HitTest = (short)(LParam.ToInt64() & 0xFFFF);
                        if(HitTest == WL.System.Native.Windows.HTCLIENT){
                            WL.System.Native.Windows.SetCursor(WL.System.Native.Windows.CURSOR_Arrow);
                        }

                        break;
                    }
                    
                    // Сдвинулась мышь внутри окна
                    case WL.System.Native.Windows.WM_MOUSEMOVE:{
                        int X__ = LWord_L;
                        int Y__ = HWord_L;

                        try{
                            OnMouseMove?.Invoke(this, X__, Y__);
                        }
                        catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на изменение позиции мыши внутри окна [" + this + "]!\nX: " + X__ + "\nY: " + Y__, e);
                        }

                        break;
                    }
                    
                    // Нажали клавишу
                    case WL.System.Native.Windows.WM_SYSKEYDOWN:
                    case WL.System.Native.Windows.WM_KEYDOWN:{
                        int Code = WParam.ToInt32();
                        Key Key  = Input.Keyboard.GetKey(Code);
                        
                        if(!__PressedKeys.Add(Code)){ break; }

                        try{
                            OnKeyboardDown?.Invoke(this, Key, Code);
                        }
                        catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на нажатии клавиши [" + Key + " (" + Code + ")] внутри окна [" + this + "]!", e);
                        }

                        break;
                    }
                    
                    // Отпустили клавишу
                    case WL.System.Native.Windows.WM_SYSKEYUP:
                    case WL.System.Native.Windows.WM_KEYUP: {
                        int Code = WParam.ToInt32();
                        Key Key  = Input.Keyboard.GetKey(Code);

                        __PressedKeys.Remove(Code);
                        
                        try{
                            OnKeyboardUp?.Invoke(this, Key, Code);   
                        }catch(Exception e){
                            Logger.Error("Произошла ошибка при вызове ивентов на отжатии клавиши [" + Key + " (" + Code + ")] внутри окна [" + this + "]!", e);
                        }
                        break;
                    }
                    
                    // Рисование внутри окна
                    case WL.System.Native.Windows.WM_PAINT:{
                        break;
                    }
                    
                    case WL.System.Native.Windows.WM_ERASEBKGND:{
                        return 1;
                    }
                    
                    // Обработка элементов у окна
                    case WL.System.Native.Windows.WM_COMMAND:{
                        return IntPtr.Zero;
                    }
                }

                return WL.System.Native.Windows.DefWindowProcW(Handle, Message, WParam, LParam);
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
        /// Вызывается когда меняется размер у окна [Окно, Новая ширина, Новая высота]
        /// </summary>
        public event Action<Window, uint, uint>? OnResize;

        /// <summary>
        /// Вызывается когда окно сдвинулось [Окно, Новый X, Новый Y]
        /// </summary>
        public event Action<Window, int, int>? OnMove;

        /// <summary>
        /// Вызывается когда мышь внутри окна сдвинулась [Окно, X, Y]
        /// </summary>
        public event Action<Window, int, int>? OnMouseMove;

        /// <summary>
        /// Вызывается когда мышь входит или выходит из окна [Окно, Входит?]
        /// </summary>
        public event Action<Window, bool>? OnMouseInside;

        /// <summary>
        /// Вызывается при обновлении окна [Окно]
        /// </summary>
        public event Action<Window>? OnUpdate;

        /// <summary>
        /// Вызывается при нажатии клавиши [Окно, Клавиша, Код клавиши]
        /// </summary>
        public event Action<Window, Key, int>? OnKeyboardDown;
        
        /// <summary>
        /// Вызывается при отжатии клавиши [Окно, Клавиша, Код клавиши]
        /// </summary>
        public event Action<Window, Key, int>? OnKeyboardUp;
        
    #endregion

    #region Дети

        /// <summary>
        /// Привязанные элементы к окну
        /// </summary>
        public readonly List<WindowElement> __Children = [];

        /// <summary>
        /// Добавить элемент к окну
        /// </summary>
        /// <param name="Element">Элемент</param>
        public Window Add(WindowElement Element){
            Element.Window = this;
            return this;
        }

    #endregion

    #region Рендер

        private IntPtr HDC{
            get{
                try{
                    CheckDestroyed();

                    IntPtr HDC = WL.System.Native.Windows.GetDC(Handle);
                    if(HDC == IntPtr.Zero){ throw new Exception("Не найден HDC у окна!"); }
                    return HDC;
                }
                catch(Exception e){
                    throw new Exception("Произошла ошибка при получении HDC у окна [" + this + "]!", e);
                }
            }   
        }
    
        private IntPtr BackBuffer;
        private IntPtr BackBufferBitMap;

        private void __UpdateBuffer(){
            IntPtr HDC = this.HDC;
            try{
                if(BackBufferBitMap != IntPtr.Zero){ WL.System.Native.Windows.DeleteObject(BackBufferBitMap); BackBufferBitMap = IntPtr.Zero; }
                if(BackBuffer == IntPtr.Zero){
                    BackBuffer = WL.System.Native.Windows.CreateCompatibleDC(HDC);
                    if(BackBuffer == IntPtr.Zero){ throw new Exception("Произошла ошибка при создании BackBuffer!"); }
                }

                BackBufferBitMap = WL.System.Native.Windows.CreateCompatibleBitmap(HDC, (int)Width, (int)Height);
                if(BackBufferBitMap == IntPtr.Zero){ throw new Exception("Произошла ошибка при создании BackBufferBitMap!"); }

                WL.System.Native.Windows.SelectObject(BackBuffer, BackBufferBitMap);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обновлении буфера у окна [" + this + "]!", e);
            }finally{
                WL.System.Native.Windows.ReleaseDC(Handle, HDC);
            }
        }
            
        public Window Render(ColorB BackgroundColor, bool RenderElements, Action<IntPtr>? PreRender, Action<IntPtr>? PostRender){
            try{
                WL.System.HDC.Fill(BackBuffer, 0, 0, Width, Height, BackgroundColor);
                
                PreRender?.Invoke(BackBuffer);
                
                if(RenderElements){
                    foreach(WindowElement Child in __Children){
                        if(Child.Parent == null){
                            Child.BaseRender(BackBuffer);
                        }
                    }   
                }
                
                PostRender?.Invoke(BackBuffer);

                IntPtr HDC = this.HDC;
                try{
                    WL.System.Native.Windows.BitBlt(HDC, 0, 0, (int)Width, (int)Height, BackBuffer, 0, 0, WL.System.Native.Windows.SRCCOPY);   
                }finally{
                    WL.System.Native.Windows.ReleaseDC(Handle, HDC);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при рендере окна [" + this + "]!", e);
            }

            return this;
        }

        public Window Render(){ return Render(BackgroundColor, true, null, null); }
        public Window RenderMessage(string Message, ColorB BackgroundColor){
            try{
                return Render(BackgroundColor,
                    false,
                    null,
                    HDC => {
                        WL.System.Native.Windows.SetBkMode(BackBuffer, WL.System.Native.Windows.TRANSPARENT);
                        WL.System.HDC.Text(BackBuffer, (int)(Width * 0.5f), (int)(Height * 0.5f), Message);
                    });
            }
            catch(Exception e){
                throw new Exception("Произошла ошибка при рендере сообщения в окне ["+ this + "]!\nСообщение: \"" + Message + "\"", e);
            }
        }

    #endregion

    /// <summary>
    /// Нажатые клавиши
    /// </summary>
    private readonly HashSet<int> __PressedKeys = new HashSet<int>();

    /// <summary>
    /// Клавиша зажатая?
    /// </summary>
    public bool KeyboardKeyPressed(int Code) => __PressedKeys.Contains(Code);
    /// <summary>
    /// Клавиша зажатая?
    /// </summary>
    public bool KeyboardKeyPressed(Key Key) => KeyboardKeyPressed((int)Key);
    
    /// <summary>
    /// Мышь находится внутри окна?
    /// </summary>
    public bool MouseInside{ get; private set; }

    /// <summary>
    /// Позиция мыши относительно этого окна
    /// </summary>
    public Vector2I MousePosition{ get; private set; }

    /// <summary>
    /// Элемент на котором сейчас мышь
    /// </summary>
    public WindowElement? MouseElement{ get; private set; }

    /// <summary>
    /// Превращает мировую координату в относительную (клиентскую) от окна
    /// </summary>
    public Vector2I ToClient(Vector2I WorldVector){
        try{
            CheckDestroyed();
            WL.System.Native.Windows.POINT P = WorldVector;
            WL.System.Native.Windows.ScreenToClient(Handle, ref P);
            return new Vector2I(P);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при изменении мировой координаты в относительную (клиентскую) от окна [" + this + "]!\nW. Вектор: " + WorldVector, e);
        }
    }

    /// <summary>
    /// Превращает относительную от окна координату в мировую координату
    /// </summary>
    public Vector2I ToWorld(Vector2I ClientVector){
        try{
            CheckDestroyed();
            WL.System.Native.Windows.POINT P = ClientVector;
            WL.System.Native.Windows.ClientToScreen(Handle, ref P);
            return new Vector2I(P);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при изменении относительной от окна [" + this + "] координаты в мировую!\nC. Вектор: " + ClientVector, e);
        }
    }

    /// <summary>
    /// Указанная мировая позиция, находится внутри окна?
    /// </summary>
    public bool Inside(Vector2I WorldVector){
        try{
            return RectFull.Inside(WorldVector);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при определении, находится ли точка внутри окна [" + this + "]!\nW. Вектор: " + WorldVector, e);
        }
    }

    /// <summary>
    /// Находит элемент на котором сейчас мышь в окне
    /// </summary>
    public WindowElement? Hit(Vector2I ClientVector){
        try{
            foreach(WindowElement Child in __Children){
                if(Child.Parent != null){ continue; }
                WindowElement? R = Child.Hit(ClientVector);
                if(R != null){ return R; }
            }
            return null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при определении, элемента на котором точка внутри окна [" + this + "]!\nC. Вектор: " + ClientVector, e);
        }
    }

    #region Трансформация

        #region Позиция

            /// <summary>
            /// Позиция по X окна
            /// </summary>
            public int X{
                get => __X;
                set{
                    try{
                        CheckDestroyed();

                        if(__X == value){ return; }
                        __X = value;
                    
                        __UpdatePosition();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении позиции по X у окна [" + this + "]!\nX: " + value, e);
                    }
                }
            }
            protected int __X;
        
            /// <summary>
            /// Позиция по Y окна
            /// </summary>
            public int Y{
                get => __Y;
                set{
                    try{
                        CheckDestroyed();

                        if(__Y == value){ return; }
                        __Y = value;
                    
                        __UpdatePosition();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении позиции по Y у окна [" + this + "]!\nY: " + value, e);
                    }
                }
            }
            protected int __Y;
        
            /// <summary>
            /// Позиция окна
            /// </summary>
            public Vector2I Position{
                get => new Vector2I(X, Y);
                set{
                    try{
                        CheckDestroyed();
                    
                        if(__X == value.X && __Y == value.Y){ return; }
                        __X = value.X;
                        __Y = value.Y;
                    
                        __UpdateSize();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении позиции у окна [" + this + "]!\nПозиция: " + value, e);
                    }
                }
            }
            
            /// <summary>
            /// Обновляет позицию окна
            /// </summary>
            private void __UpdatePosition(){
                try{
                    WL.System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, __X, __Y, 0, 0, WL.System.Native.Windows.SWP_NOZORDER | WL.System.Native.Windows.SWP_NOSIZE);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при обновлении позиции у окна [" + this + "]!", e);
                }
            }

        #endregion

        #region Размер

            /// <summary>
            /// Ширина окна
            /// </summary>
            public uint Width{
                get => __Width;
                set{
                    try{
                        CheckDestroyed();
                    
                        if(__Width == value){ return; }
                        __Width = value;

                        __UpdateSize();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении ширины у окна [" + this + "]!\nШирина: " + value, e);
                    }
                }
            }
            protected uint __Width;
        
            /// <summary>
            /// Высота окна
            /// </summary>
            public uint Height{
                get => __Height;
                set{
                    try{
                        CheckDestroyed();
                    
                        if(__Height == value){ return; }
                        __Height = value;
                    
                        __UpdateSize();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении высоты у окна [" + this + "]!\nВысота: " + value, e);
                    }
                }
            }
            protected uint __Height;

            /// <summary>
            /// Размер окна
            /// </summary>
            public Vector2U Size{
                get => new Vector2U(Width, Height);
                set{
                    try{
                        CheckDestroyed();
                    
                        if(__Width == value.X && __Height == value.Y){ return; }
                        __Width  = value.X;
                        __Height = value.Y;
                    
                        __UpdateSize();
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при изменении размера у окна [" + this + "]!\nРазмер: " + value, e);
                    }
                }
            }
            
            /// <summary>
            /// Обновляет размер окна
            /// </summary>
            private void __UpdateSize(){
                try{
                    WL.System.Native.Windows.RECT Rect = new WL.System.Native.Windows.RECT{
                        left   = 0,
                        top    = 0,
                        right  = (int)__Width,
                        bottom = (int)__Height
                    };

                    WL.System.Native.Windows.AdjustWindowRectEx(ref Rect, WL.System.Native.Windows.WS_OVERLAPPEDWINDOW, false, 0);

                    WL.System.Native.Windows.SetWindowPos(Handle, IntPtr.Zero, 0, 0, Rect.right - Rect.left, Rect.bottom - Rect.top, WL.System.Native.Windows.SWP_NOZORDER | WL.System.Native.Windows.SWP_NOMOVE);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при обновлении размера у окна [" + this + "]!", e);
                }
            }

        #endregion
    
        /// <summary>
        /// Позиция и размер окна
        /// </summary>
        public RectI Rect{
            get => new RectI(X, Y, (int)Width, (int)Height);
            set{
                try{
                    CheckDestroyed();
                
                    if(__X == value.X && __Y == value.Y && __Width == value.Width && __Height == value.Height){ return; }
                    __X = value.X;
                    __Y = value.Y;
                    __Width  = (uint)value.Width ;
                    __Height = (uint)value.Height;
                
                    __UpdatePosition();
                    __UpdateSize    ();
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при изменении позиции и размера у окна [" + this + "]!\nRect: " + value, e);
                }
            }
        }
    
        /// <summary>
        /// Позиция и размер окна (полная, с учётом декораций и т.д)
        /// </summary>
        public RectI RectFull{
            get{
                try{
                    if(!WL.System.Native.Windows.GetWindowRect(Handle, out WL.System.Native.Windows.RECT Rect)){
                        WL.System.Native.Windows.ThrowWin32Error();
                    }

                    return new RectI(Rect);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при получении полной позиции и размера окна [" + this + "]!", e);
                }
            }
        }
        
    #endregion
    
    /// <summary>
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                CheckDestroyed();
                
                if(__Title == value){ return; }
                __Title = value;

                WL.System.Native.Windows.SetWindowTextW(Handle, __Title);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при изменении названия у окна [" + this + "]!\nНазвание: \"" + value + "\"", e);
            }
        }
    }
    private string __Title;

    /// <summary>
    /// Цвет заднего фона
    /// </summary>
    public ColorB BackgroundColor = ColorB.White;
    
    #region Override

        public override string ToString(){
            return "Window(" + ID + " (" + Handle + ")" + ", \"" + Title + "\", " + Rect.ToShortString() + ")";
        }
		    
        public override bool Equals(object? Obj){
            if(Obj is not Window Other){ return false; }
            return ID == Other.ID;
        }
		    
        public override int GetHashCode(){
            return ID.GetHashCode();
        }
		
    #endregion
}