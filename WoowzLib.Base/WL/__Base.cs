namespace WL;

public static class __Base{
    /// <summary>
    /// WoowzLib запущен?
    /// </summary>
    public static bool WoowzLibInitialized{ get; private set; }
    
    /// <summary>
    /// Проверяет, запущен ли WoowzLib, иначе ошибка
    /// </summary>
    public static void CheckWoowzLib(){ if(!WoowzLibInitialized){ throw new Exception("WoowzLib должен быть запущен!"); } }
    
    /// <summary>
    /// Запускает WoowzLib
    /// </summary>
    public static void Initialize(){
        try{
            if(WoowzLibInitialized){ throw new Exception("WoowzLib уже запущен!"); } WoowzLibInitialized = true;
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при запуске WoowzLib!", e);
        }
    }

    /// <summary>
    /// Останавливает WoowzLib
    /// </summary>
    public static void Terminate(){
        try{
            if(!WoowzLibInitialized){ throw new Exception("WoowzLib не был запущен!"); }

            try{
                OnTerminate?.Invoke();
            }catch(Exception e){
                throw new Exception("Произошла ошибка внутри вента OnTerminate!", e);
            }
            
            WoowzLibInitialized = false;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при остановке WoowzLib!", e);
        }
    }

    /// <summary>
    /// Вызывается при остановке WoowzLib
    /// </summary>
    public static event Action? OnTerminate;
}