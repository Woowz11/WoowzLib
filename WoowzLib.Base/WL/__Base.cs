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
        /// Сам пользователь указал закрытие в коде (вызвал OnTerminate)
        /// </summary>
        User
    }
    
    /// <summary>
    /// Операционная система
    /// </summary>
    public enum OS{ Android, FreeBSD, IOS, Linux, MacCatalyst, MacOS, TvOS, WatchOS, Windows, Unknown }
}

namespace WL{
    public static partial class __Base{
        static __Base(){
            // Детект операционной системы
            OS DetectOS(){
                if(OperatingSystem.IsWindows    ()){ return OS.Windows    ; }
                if(OperatingSystem.IsLinux      ()){ return OS.Linux      ; }
                if(OperatingSystem.IsMacOS      ()){ return OS.MacOS      ; }
                if(OperatingSystem.IsMacCatalyst()){ return OS.MacCatalyst; }
                if(OperatingSystem.IsFreeBSD    ()){ return OS.FreeBSD    ; }
                if(OperatingSystem.IsAndroid    ()){ return OS.Android    ; }
                if(OperatingSystem.IsIOS        ()){ return OS.IOS        ; }
                if(OperatingSystem.IsTvOS       ()){ return OS.TvOS       ; }
                if(OperatingSystem.IsWatchOS    ()){ return OS.WatchOS    ; }
                
                return OS.Unknown;
            }
            
            CurrentOS = DetectOS();

            if(CurrentOS != OS.Windows){ throw new Exception($"WoowzLib работает только на Windows OS! А сейчас: {CurrentOS}"); }
            
            void __Terminate(CloseReason CloseReason){
                if(!Terminated && !TerminateHooked){
                    global::Logger.Warn("WoowzLib не был завершён корректно, авто-завершение! Используйте WL.Core.Terminate()!");
                    Terminate(CloseReason);
                }
            }
            
            // Предупреждение, если библиотека не была остановлена!
            OnClose += __Terminate;
        }
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Информация об проекте
        /// </summary>
        public static ProjectMetadata ProjectMetadata = new ProjectMetadata();

        /// <summary>
        /// Информация об ядре
        /// </summary>
        public static ProjectMetadata EngineMetadata = new ProjectMetadata("WoowzLib", new Version(Assembly.GetCallingAssembly()), "Woowz11", "CC BY SA 4.0");

        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Остановить библиотеку
        /// </summary>
        public static void Terminate(CloseReason Reason){
            try{
                if(__InTerminate){ return; }
                if(Terminated){ throw new Exception("Работа WoowzLib и так была завершена!"); }

                __InTerminate = true;
                
                // Вызов, в обратном порядке
                Delegate[]? OnTerminate__ = OnTerminate?.GetInvocationList();
                if(OnTerminate__ != null){
                    for(int i = OnTerminate__.Length - 1; i >= 0; i--){
                        try{
                            ((Action<CloseReason>)OnTerminate__[i])(Reason);
                        }catch(Exception e){
                            global::Logger.Error($"Произошла ошибка в ивенте OnTerminate!\nИндекс: {i}", e);
                        }
                    }
                }

                if(TerminateHooked){
                    OnClose -= Terminate;
                    
                    TerminateHooked = false;
                }

                __InTerminate = false;
                Terminated = true;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при остановке WoowzLib!", e);
            }
        }
        private static bool __InTerminate;

        /// <summary>
        /// Работа библиотеки была завершена
        /// </summary>
        public static bool Terminated;

        /// <summary>
        /// Вызывается при остановке библиотеки
        /// </summary>
        public static event Action<CloseReason>? OnTerminate;

        /// <summary>
        /// Связывает Terminate автоматически с выходом из приложения
        /// </summary>
        public static void HookTerminate(){
            if(TerminateHooked){ throw new Exception("Нельзя привязать Terminate в WoowzLib больше одного раза!"); } TerminateHooked = true;

            OnClose += Terminate;
        }

        /// <summary>
        /// Terminate связан?
        /// </summary>
        private static bool TerminateHooked;
        
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

                Exit   = (_, _) => { Action(CloseReason.Exit  ); };
                Crash  = (_, _) => { Action(CloseReason.Crash ); };
                Cancel = (_, _) => { Action(CloseReason.Cancel); };
            }

            public readonly Action<CloseReason> Action;
            
            public readonly EventHandler?                   Exit  ;
            public readonly UnhandledExceptionEventHandler? Crash ;
            public readonly ConsoleCancelEventHandler?      Cancel;
        }
        
        /// <summary>
        /// Вызывается при любом закрытии приложения
        /// </summary>
        public static event Action<CloseReason>? OnClose{
            add{
                if(value == null){ return; }
                if(__OnCloseHooks.ContainsKey(value)){ throw new Exception("Такой OnClose Hook уже есть при добавлении в WoowzLib!"); }

                __OnCloseHook Hook = new __OnCloseHook(value);

                __OnCloseHooks[value] = Hook;

                OnExit   += Hook.Exit  ;
                OnCrash  += Hook.Crash ;
                OnCancel += Hook.Cancel;
            }
            remove{
                if(value == null){ return; }
                if(!__OnCloseHooks.TryGetValue(value, out __OnCloseHook Hook)){ throw new Exception("Такой OnClose Hook не найден при удалении в WoowzLib!"); }
                
                OnExit   -= Hook.Exit  ;
                OnCrash  -= Hook.Crash ;
                OnCancel -= Hook.Cancel;

                __OnCloseHooks.Remove(value);
            }
        }
        
        // ----------------------------------------------------------------------
        
        /// <summary>
        /// Текущая операционная система
        /// </summary>
        public static readonly OS CurrentOS;
    }
}