using System.Reflection;
using System.Runtime.Loader;
using WLO;
using Version = WLO.Version;

namespace WLO{
    /// <summary>
    /// Причины закрытия приложения
    /// </summary>
    public enum CloseReason{
        /// <summary>
        /// Приложение закрылось
        /// </summary>
        Exit,
        /// <summary>
        /// Вылет приложения
        /// </summary>
        Crash,
        /// <summary>
        /// Отмена приложения
        /// </summary>
        Cancel,
        /// <summary>
        /// Разгрузка приложения
        /// </summary>
        Unloading
    }
}

namespace WL{
    public static partial class __Base{
        static __Base(){
            void __Terminate(){
                if(!Terminated && !TerminateHooked){
                    global::Logger.Warn("WoowzLib не был завершён корректно, авто-завершение! Используйте WL.Core.Terminate()!");
                    WL.__Base.Terminate();
                }
            }
            
            // Предупреждение, если библиотека не была остановлена!
            OnClose += _ => __Terminate();
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
                    OnClose -= __Hook_Terminate;

                    __Hook_Terminate = null;
                    
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

            __Hook_Terminate = _ => Terminate();

            OnClose += __Hook_Terminate;
        }

        /// <summary>
        /// Terminate связан?
        /// </summary>
        private static bool TerminateHooked;

        /// <summary>
        /// Hook Terminate
        /// </summary>
        private static Action<CloseReason>? __Hook_Terminate;
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Вызывается при выходе из приложения (ТОЛЬКО ПРИ ВЫХОДЕ, CRASH или другие последствия не вызывают! Для этого используйте OnClose!)
        /// </summary>
        public static event EventHandler? OnExit{
            add    => AppDomain.CurrentDomain.ProcessExit += value;
            remove => AppDomain.CurrentDomain.ProcessExit -= value;
        }
        
        /// <summary>
        /// Вызывается при CRASH
        /// </summary>
        public static event UnhandledExceptionEventHandler? OnCrash{
            add    => AppDomain.CurrentDomain.UnhandledException += value;
            remove => AppDomain.CurrentDomain.UnhandledException -= value;
        }
        
        /// <summary>
        /// Вызывается при нажатиях комбинации <b>Ctrl+C, Ctrl+Break</b> в консоли
        /// </summary>
        public static event ConsoleCancelEventHandler? OnCancel{
            add    => Console.CancelKeyPress += value;
            remove => Console.CancelKeyPress -= value;
        }
        
        /// <summary>
        /// Вызывается при выгрузке приложения
        /// </summary>
        public static event Action<AssemblyLoadContext>? OnUnloading{
            add    => AssemblyLoadContext.Default.Unloading += value;
            remove => AssemblyLoadContext.Default.Unloading -= value;
        }
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Список OnClose Hook's
        /// </summary>
        private static readonly Dictionary<Action<CloseReason>, __OnCloseHook> __OnCloseHooks = new Dictionary<Action<CloseReason>, __OnCloseHook>();
        
        /// <summary>
        /// Hook для OnClose
        /// </summary>
        private readonly struct __OnCloseHook{
            public __OnCloseHook(Action<CloseReason> Action){
                this.Action = Action;

                Exit      = (_, _) => { Action(CloseReason.Exit     ); };
                Crash     = (_, _) => { Action(CloseReason.Crash    ); };
                Cancel    = (_, _) => { Action(CloseReason.Cancel   ); };
                Unloading =  _     => { Action(CloseReason.Unloading); };
            }

            public readonly Action<CloseReason> Action;
            
            public readonly EventHandler?                   Exit     ;
            public readonly UnhandledExceptionEventHandler? Crash    ;
            public readonly ConsoleCancelEventHandler?      Cancel   ;
            public readonly Action<AssemblyLoadContext>?    Unloading;
        }
        
        /// <summary>
        /// Вызывается при любом закрытии приложения
        /// </summary>
        public static event Action<CloseReason>? OnClose{
            add{
                if(value == null){ return; }
                if(__OnCloseHooks.ContainsKey(value)){ throw new Exception("Такой OnClose Hook уже есть при добавлении!"); }

                __OnCloseHook Hook = new __OnCloseHook(value);

                __OnCloseHooks[value] = Hook;

                OnExit      += Hook.Exit     ;
                OnCrash     += Hook.Crash    ;
                OnCancel    += Hook.Cancel   ;
                OnUnloading += Hook.Unloading;
            }
            remove{
                if(value == null){ return; }
                if(!__OnCloseHooks.TryGetValue(value, out __OnCloseHook Hook)){ throw new Exception("Такой OnClose Hook не найден при удалении!"); }
                
                OnExit      -= Hook.Exit     ;
                OnCrash     -= Hook.Crash    ;
                OnCancel    -= Hook.Cancel   ;
                OnUnloading -= Hook.Unloading;

                __OnCloseHooks.Remove(value);
            }
        }
    }
}