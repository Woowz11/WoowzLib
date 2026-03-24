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

            __Window = new Window(Class, new Window.Constructor{
                Title = Config.Title
            });
            
            __Windows.Add(this);
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
    /// Оригинальное WINAPI окно, изменяйте его на свой страх и риск!
    /// </summary>
    public Window Original => __Window;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Обновление ивентов окна
    /// </summary>
    private IntPtr? __Events(Window Window, uint Message, IntPtr WP, IntPtr LP){
        try{

            switch(Message){
                case Native.Raw.Windows.WM_DESTROY: {
                    __Destroy();
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
    /// Вызывается при уничтожении (окно)
    /// </summary>
    public event Action<WLWindow>? OnDestroy;

    /// <summary>
    /// Вызывается при закрытии окна (окно) => (закрыть окно?)
    /// </summary>
    public event Func<WLWindow, bool>? OnClose;
    
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
            
            Alive = false;
            
            __Windows.Remove(this);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении WL окна [" + this + "]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Все WL окна
    /// </summary>
    public static IReadOnlyList<WLWindow> Windows => __Windows;
    private static readonly List<WLWindow> __Windows = [];

    /// <summary>
    /// Обновляет все окна, нужно вызвать 1 раз в потоке
    /// </summary>
    public static void UpdateWindows(){
        Window.UpdateWindows();
    }
}