using WL;
using WLO.Attribute;
using WLO.Color;
using WLO.Rect;
using WLO.Vector;

namespace WLO;

/// <summary>
/// Улучшенное окно
/// </summary>
public class WLWindow{
    public class Constructor{
        /// <summary>
        /// Стартовый заголовок окна
        /// </summary>
        public string Title = "Новое окно";
    }
    
    /// <summary>
    /// Создаёт окно
    /// </summary>
    /// <param name="Config">Настройки</param>
    public WLWindow(Constructor? Config = null){
        try{
            Config ??= new Constructor();
            
            WindowClass Class = new WindowClass("WLWindow_Class_" + __TotalClasses, __Events); __TotalClasses++;

            __Title = Config.Title;
            
            Original = new Window(Class, new Window.Constructor{
                Title = __Title
            });
            
            __Windows.Add(this);

            try{
                OnGlobalCreate?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnGlobalCreate в WL окне [" + this + "]!", e);
            }

            Scene = new SceneAlgorithm<WLElement.WLElement>(this, SceneCacheMode.SceneOnly);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании WL окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Окно живое?
    /// </summary>
    public bool Alive = true;

    /// <summary>
    /// Окно мертво?
    /// </summary>
    public bool Died => !Alive;

    /// <summary>
    /// Проверяет, живое ли окно?
    /// </summary>
    public void CheckAlive(){ if(Died){ throw new Exception("Окно мёртвое!"); } }
    
    /// <summary>
    /// Оригинальное WINAPI окно
    /// </summary>
    public Window Original{ get; }

    /// <summary>
    /// Элементы окна
    /// </summary>
    public readonly SceneAlgorithm<WLElement.WLElement> Scene;

    // ----------------------------------------------------------------------

    /// <summary>
    /// Вызывается при уничтожении (Окно)
    /// </summary>
    public event Action<WLWindow>? OnDestroy;

    /// <summary>
    /// Вызывается при закрытии окна (Окно) => (Закрыть окно?)
    /// </summary>
    public event Func<WLWindow, bool>? OnClose;

    /// <summary>
    /// Вызывается при изменении заголовка окна (Окно, Заголовок) => (Новый заголовок (если вернуть null, не изменит))
    /// </summary>
    public event Func<WLWindow, string, string?>? OnTitle;

    /// <summary>
    /// Вызывается в начале рендера (Окно, HDC, Размер HDC)
    /// </summary>
    public event Action<WLWindow, IntPtr, Vector2UI>? OnRender;
    
    /// <summary>
    /// Вызывается в конце рендера (Окно, HDC, Размер HDC)
    /// </summary>
    public event Action<WLWindow, IntPtr, Vector2UI>? OnPostRender;

    [WoowzLibHint(Information.WorkInProgress)] 
    public event Action<WLWindow, Vector2I>? OnPosition;
    
    [WoowzLibHint(Information.WorkInProgress)]
    public event Action<WLWindow, Vector2UI>? OnSize;

    /// <summary>
    /// Вызывается при изменении позиции или размера окна (Окно, Позиция и размер) => (Новая позиция и размер (если вернуть null, не изменит))
    /// </summary>
    public event Func<WLWindow, Rect2I, Rect2I?>? OnRect;
    
    // ----------------------------------------------------------------------

    #region Заголовок

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title{
            get => __Title;
            set{
                try{
                    CheckAlive();

                    string Title__ = value;
                    
                    try{
                        string? NewTitle = OnTitle?.Invoke(this, Title__);
                        if(NewTitle != null){ Title__ = NewTitle; }
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка в ивенте OnTitle в WL окне [" + this + "]!\nЗаголовок: " + value, e);
                    }
                    
                    if(__Title == Title__){ return; }

                    Original.Title = Title__;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке заголовка WL окна [" + this + "]!\nЗаголовок: " + value, e);
                }
            }
        }
        private string __Title;

    #endregion

    #region Позиция

        /// <summary>
        /// Координата X окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public int X{
            get => __X;
            set{
                try{
                    CheckAlive();
                    
                    if(__X == value){ return; }
                    
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке координаты X WL окна [" + this + "]!\nКоордината X: " + value, e);
                }
            }
        }
        private int __X;

        /// <summary>
        /// Координата Y окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public int Y{
            get => __Y;
            set{
                try{
                    CheckAlive();
                    
                    if(__Y == value){ return; }
                    
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке координаты Y WL окна [" + this + "]!\nКоордината Y: " + value, e);
                }
            }
        }
        private int __Y;
        
        /// <summary>
        /// Позиция окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public Vector2I Position{
            get => new Vector2I(__X, __Y);
            set{
                try{
                    CheckAlive();
                    
                    if(__X == value.X && __Y == value.Y){ return; }
                    
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке позиции WL окна [" + this + "]!\nПозиция: " + value, e);
                }
            }
        }

    #endregion

    #region Размер

        /// <summary>
        /// Ширина окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public uint W{
            get => __W;
            set{
                try{
                    CheckAlive();
                    
                    if(__W == value){ return; }
                    
                    
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке ширины WL окна [" + this + "]!\nШирина: " + value, e);
                }
            }
        }
        private uint __W;

        /// <summary>
        /// Высота окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public uint H{
            get => __H;
            set{
                try{
                    CheckAlive();
                    
                    if(__H == value){ return; }
                    
                    
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке высоты WL окна [" + this + "]!\nВысота: " + value, e);
                }
            }
        }
        private uint __H;
        
        /// <summary>
        /// Размер окна
        /// </summary>
        [WoowzLibHint(Information.WorkInProgress)]
        public Vector2UI Size{
            get => new Vector2UI(__W, __H);
            set{
                try{
                    CheckAlive();
                    
                    if(__W == value.W && __H == value.H){ return; }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при установке размера WL окна [" + this + "]!\nРазмер: " + value, e);
                }
            }
        }

    #endregion

    /// <summary>
    /// Позиция и размер окна
    /// </summary>
    [WoowzLibHint(Information.WorkInProgress)]
    public Rect2I Rect{
        get => new Rect2I(Position, Size);
        set{
            try{
                CheckAlive();
                    
                if(__X == value.X && __Y == value.Y && __W == value.W && __H == value.H){ return; }
                
                
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции и размера WL окна [" + this + "]!\nПрямоугольник: " + value, e);
            }
        }
    }
    
    /// <summary>
    /// Размер клиентской области
    /// </summary>
    public Vector2UI ClientSize => Original.ClientSize;
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Цвет заднего фона
    /// </summary>
    public Color4B BackgroundColor = Color4B.Black;

    /// <summary>
    /// Очищать буфер рендера при вызове рендера?
    /// </summary>
    public bool ClearRenderBuffer = true;

    /// <summary>
    /// Включить двойной буфер рендера?
    /// </summary>
    public bool DoubleRenderBuffer = true;

    /// <summary>
    /// Рендерить элементы окна?
    /// </summary>
    public bool RenderElements = true;
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Уничтожает окно
    /// </summary>
    public void Destroy(){
        try{
            if(Died){ throw new Exception("Окно уже уничтоженное!"); }
            
            Original.Destroy();   
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке уничтожить WL окно [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Рендерит окно
    /// </summary>
    public void Render(){
        IntPtr? HDC__ = null;

        try{
            CheckAlive();

            HDC__ = Original.HDC()!;
            IntPtr WindowHDC = HDC__.Value;

            Vector2UI RenderSize__ = ClientSize;
            if(RenderSize__ != __RenderSize){ __UpdateDoubleBuffer(WindowHDC, RenderSize__); }

            IntPtr HDC = DoubleRenderBuffer ? __DoubleBufferHDC : WindowHDC;
            
            #region Рендер

                if(ClearRenderBuffer){
                    WL.System.Draw.Fill(HDC, new Rect2I(Vector2I.Zero, __RenderSize), new BrushFill(BackgroundColor));
                }

                try{
                    OnRender?.Invoke(this, HDC, ClientSize);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в ивенте OnRender в WL окне [" + this + "]!", e);
                }

                if(RenderElements){ __RenderElements(HDC, ClientSize); }

                try{
                    OnPostRender?.Invoke(this, HDC, ClientSize);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в ивенте OnPostRender в WL окне [" + this + "]!", e);
                }

            #endregion

            if(DoubleRenderBuffer){
                if(__RenderSize != Original.ClientSize){ Logger.Error("DILODA"); }
                WL.System.Draw.CopyHDC(WindowHDC, HDC, __RenderSize);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка во время рендера WL окна [" + this + "]!", e);
        }finally{
            if(HDC__.HasValue){ Original.HDC(HDC__.Value); }
        }
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Всего созданных оконных классов
    /// </summary>
    private static uint __TotalClasses;

    /// <summary>
    /// HDC двойного буфера
    /// </summary>
    private IntPtr __DoubleBufferHDC;

    /// <summary>
    /// Изображение двойного буфера
    /// </summary>
    private IntPtr __DoubleBufferBitmap;

    /// <summary>
    /// Область рендера
    /// </summary>
    private Vector2UI __RenderSize;

    /// <summary>
    /// Уничтожает окно
    /// </summary>
    private void __Destroy(){
        try{
            if(!Alive){ return; }

            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnDestroy в WL окне [" + this + "]!", e);
            }
            
            try{
                OnGlobalDestroy?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnGlobalDestroy в WL окне [" + this + "]!", e);
            }
            
            __DestroyDoubleBuffer();
            
            Alive = false;
            
            __Windows.Remove(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении WL окна [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Обновление ивентов окна
    /// </summary>
    private IntPtr? __Events(Window Window, uint Message, IntPtr WP, IntPtr LP){
        try{

            switch(Message){
                // Вызывается перед уничтожением
                case Native.Raw.Windows.WM_DESTROY: { __Destroy(); break; }

                // Вызывается при закрытии окна
                case Native.Raw.Windows.WM_CLOSE: {
                    bool Destroy;
                    
                    try{
                        Destroy = OnClose?.Invoke(this) ?? true;
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка в ивенте OnClose в WL окне [" + this + "]!", e);
                        Destroy = false;
                    }
                    
                    if(!Destroy){ return IntPtr.Zero; }
                    break;
                }
                
                // Рисование внутри окна
                case Native.Raw.Windows.WM_PAINT: { break; }

                // Очистка окна
                case Native.Raw.Windows.WM_ERASEBKGND: { return 1; }
                
                // Обработка элементов у окна
                case Native.Raw.Windows.WM_COMMAND: { return IntPtr.Zero; }
                
                // Обновляет курсор
                case Native.Raw.Windows.WM_SETCURSOR: {
                    int Hit = WL.System.Native.LoWord(LP);
                    if(Hit == Native.Raw.Windows.HTCLIENT){
                        Native.Raw.Windows.SetCursor(Native.Raw.Windows.CURSOR_Arrow);
                    }
                    
                    break;
                }

                // Изменился заголовок окна
                case Native.Raw.Windows.WM_SETTEXT: {
                    __Title = WL.System.Memory.LoadString(LP) ?? string.Empty;
                    
                    break;
                }

                // Перед изменением позиции и размера окна
                case Native.Raw.Windows.WM_WINDOWPOSCHANGING: {
                    Native.Raw.Windows.WINDOWPOS WindowPos = WL.System.Memory.LoadStruct<Native.Raw.Windows.WINDOWPOS>(LP);
                    bool Changed = false;

                    __X = WindowPos.x;
                    __Y = WindowPos.y;

                    __W = (uint)WindowPos.cx;
                    __H = (uint)WindowPos.cy;
                    
                    try{
                        Rect2I Rect = new Rect2I(__X, __Y, __W, __H);
                        Rect2I? NewRect = OnRect?.Invoke(this, Rect);
                        if(NewRect != null){
                            if(Rect != NewRect){
                                Changed = true;
                                WindowPos.x  = NewRect.Value.X;
                                WindowPos.y  = NewRect.Value.Y;
                                WindowPos.cx = (int)NewRect.Value.W;
                                WindowPos.cy = (int)NewRect.Value.H;
                                
                                __X = WindowPos.x;
                                __Y = WindowPos.y;

                                __W = (uint)WindowPos.cx;
                                __H = (uint)WindowPos.cy;
                            }
                        }
                    }catch(Exception e){
                        Logger.Error("Произошла ошибка при вызове ивента OnPosition у WL окна [" + this + "] внутри WINDOWPOSCHANGED!", e);
                    }
                    
                    if(Changed){ WL.System.Memory.SetStruct(LP, WindowPos); return IntPtr.Zero; } break;
                }
            }
            
        }catch(Exception e){
            Logger.Error("Произошла ошибка при обновлении WL окна [" + this + "]!", e);
        }
        return null;
    }

    /// <summary>
    /// Обновляет двойной буфер
    /// </summary>
    private void __UpdateDoubleBuffer(IntPtr HDC, Vector2UI RenderSize){
        try{
            __DestroyDoubleBuffer();

            __RenderSize = RenderSize;
            
            __DoubleBufferHDC    = WL.System.Draw.CreateMemoryHDC   (HDC              );
            __DoubleBufferBitmap = WL.System.Draw.CreateMemoryBitmap(HDC, __RenderSize);

            WL.System.Draw.SelectBitmap(__DoubleBufferHDC, __DoubleBufferBitmap);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении двойного буфера у WL окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Уничтожает двойной буфер
    /// </summary>
    private void __DestroyDoubleBuffer(){
        try{
            if(__DoubleBufferBitmap != IntPtr.Zero){ WL.System.Draw.DestroyBitmap(__DoubleBufferBitmap); __DoubleBufferBitmap = IntPtr.Zero; }
            if(__DoubleBufferHDC    != IntPtr.Zero){ WL.System.Draw.DestroyHDC   (__DoubleBufferHDC   ); __DoubleBufferHDC    = IntPtr.Zero; }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении двойного буфера у WL окна [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Рендерит элементы окна
    /// </summary>
    private void __RenderElements(IntPtr HDC, Vector2UI ClientSize){
        try{
            void __Render(SceneNode<WLElement.WLElement> Element){
                Element.Self.__Render(this, HDC);
                
                foreach(SceneNode<WLElement.WLElement> Element__ in Element.Level0){
                    __Render(Element__);
                }
            }
            
            foreach(SceneNode<WLElement.WLElement> Element in Scene.Level0){
                __Render(Element);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при рендере элементов WL окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Все WL окна
    /// </summary>
    public static IReadOnlyList<WLWindow> Windows => __Windows;
    private static readonly List<WLWindow> __Windows = [];
    
    /// <summary>
    /// Вызывается при создании окна
    /// </summary>
    public static event Action<WLWindow>? OnGlobalCreate;

    /// <summary>
    /// Вызывается при уничтожении окна
    /// </summary>
    public static event Action<WLWindow>? OnGlobalDestroy;

    /// <summary>
    /// Обновляет все окна, нужно вызвать 1 раз в потоке
    /// </summary>
    public static void UpdateWindows() => Window.UpdateWindows();
    
    // ----------------------------------------------------------------------

    public override string ToString() => "WLWindow()";

    public string ToFullString() => "WLWindow(" + Original + ")";
}