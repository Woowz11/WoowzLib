using System.Reflection;
using System.Runtime.Loader;
using WLO;
using Version = WLO.Version;

namespace WL;

public static partial class __Base{
    static __Base(){
        void __Terminate(){
            if(!Terminated && !TerminateHooked){
                global::Logger.Warn("WoowzLib не был завершён корректно, авто-завершение! Используйте WL.Core.Terminate()!");
                WL.__Base.Terminate();
            }
        }
        
        // Предупреждение, если библиотека не была остановлена!
        AppDomain.CurrentDomain.ProcessExit        += (_,_) => __Terminate();
        AppDomain.CurrentDomain.UnhandledException += (_,_) => __Terminate();
        Console.CancelKeyPress                     += (_,_) => __Terminate();
        AssemblyLoadContext.Default.Unloading      +=  _    => __Terminate();
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Информация об проекте
    /// </summary>
    public static ProjectMetadata ProjectMetadata = new ProjectMetadata();

    /// <summary>
    /// Информация об ядре
    /// </summary>
    public static ProjectMetadata EngineMetadata = new ProjectMetadata("WoowzLib",new Version(Assembly.GetCallingAssembly()), "Woowz11", "CC BY SA 4.0");

    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Остановить библиотеку
    /// </summary>
    public static void Terminate(){
        try{
            if(Terminated){ throw new Exception("Работа WoowzLib и так была завершена!"); }
            
            try{
                OnTerminate?.Invoke();
            }catch(Exception e){ global::Logger.Error("Произошла ошибка в ивенте OnTerminate!", e); }

            if(TerminateHooked){
                AppDomain.CurrentDomain.ProcessExit        -= __Hook_ProcessExit       ;
                AppDomain.CurrentDomain.UnhandledException -= __Hook_UnhandledException;
                Console.CancelKeyPress                     -= __Hook_CancelKeyPress    ;
                AssemblyLoadContext.Default.Unloading      -= __Hook_Unloading         ;
                
                __Hook_ProcessExit        = null;
                __Hook_UnhandledException = null;
                __Hook_CancelKeyPress     = null;
                __Hook_Unloading          = null;
                
                TerminateHooked = false;
            }

            Terminated = true;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при остановке WoowzLib!", e);
        }
    }

    /// <summary>
    /// Работа библиотеки была завершена
    /// </summary>
    public static bool Terminated;

    /// <summary>
    /// Вызывается при остановке библиотеки
    /// </summary>
    public static event Action? OnTerminate;

    /// <summary>
    /// Связывает Terminate автоматически с выходом из приложения
    /// </summary>
    public static void HookTerminate(){
        if(TerminateHooked){ throw new Exception("Нельзя привязать Terminate больше одного раза!"); } TerminateHooked = true;

        __Hook_ProcessExit        = (Sender, Args) => Terminate();
        __Hook_UnhandledException = (Sender, Args) => Terminate();
        __Hook_CancelKeyPress     = (Sender, Args) => Terminate();
        __Hook_Unloading          =  Context              => Terminate();
        
        AppDomain.CurrentDomain.ProcessExit        += __Hook_ProcessExit       ;
        AppDomain.CurrentDomain.UnhandledException += __Hook_UnhandledException;
        Console.CancelKeyPress                     += __Hook_CancelKeyPress    ;
        AssemblyLoadContext.Default.Unloading      += __Hook_Unloading         ;
    }

    /// <summary>
    /// Terminate связан?
    /// </summary>
    private static bool TerminateHooked;

    private static EventHandler?                   __Hook_ProcessExit       ;
    private static UnhandledExceptionEventHandler? __Hook_UnhandledException;
    private static ConsoleCancelEventHandler?      __Hook_CancelKeyPress    ;
    private static Action<AssemblyLoadContext>?    __Hook_Unloading         ;
}