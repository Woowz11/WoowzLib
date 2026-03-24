using WL;

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
            
            __Window = new Window(Class, new Window.Constructor{
                Title = __Title
            });
            
            __Windows.Add(this);

            try{
                OnGlobalCreate?.Invoke(this);
            }catch(Exception e){
                Logger.Error("Произошла ошибка в ивенте OnGlobalCreate в WL окне [" + this + "]!", e);
            }
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
    /// Оригинальное WINAPI окно, изменяйте его на свой страх и риск!
    /// </summary>
    public Window Original => __Window;

    // ----------------------------------------------------------------------

    /// <summary>
    /// Вызывается при уничтожении (окно)
    /// </summary>
    public event Action<WLWindow>? OnDestroy;

    /// <summary>
    /// Вызывается при закрытии окна (окно) => (закрыть окно?)
    /// </summary>
    public event Func<WLWindow, bool>? OnClose;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Уничтожает окно
    /// </summary>
    public void Destroy(){
        try{
            if(Died){ throw new Exception("Окно уже уничтоженное!"); }
            
            __Window.Destroy();   
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке уничтожить WL окно [" + this + "]!", e);
        }
    }

    private string __Title;
    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                CheckAlive();
                
                if(__Title == value){ return; }

                __Window.Title = value;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке заголовка WL окна [" + this + "]!\nЗаголовок: " + value, e);
            }
        }
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Всего созданных оконных классов
    /// </summary>
    private static uint __TotalClasses;
    
    /// <summary>
    /// WINAPI окно
    /// </summary>
    private readonly Window __Window;

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

                // Обновляет курсор
                case Native.Raw.Windows.WM_SETCURSOR: {
                    int Hit = WL.System.Native.LoWord(LP);
                    if(Hit == Native.Raw.Windows.HTCLIENT){
                        Native.Raw.Windows.SetCursor(Native.Raw.Windows.CURSOR_Arrow);
                    }
                    
                    break;
                }

                // Изменился заголовок окна
                case Native.Raw.Windows.WM_SETTEXT:{
                    __Title = WL.System.Memory.LoadString(LP) ?? string.Empty;
                    
                    break;
                }
            }
            
        }catch(Exception e){
            Logger.Error("Произошла ошибка при обновлении WL окна [" + this + "]!", e);
        }
        return null;
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
    public static void UpdateWindows(){
        Window.UpdateWindows();
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "WLWindow()";

    public string ToFullString() => "WLWindow(" + __Window + ")";
}