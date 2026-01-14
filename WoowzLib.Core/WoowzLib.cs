using System.Reflection;
using System.Runtime.CompilerServices;
using WLO;

namespace WL{
    [WLModule(int.MinValue)]
    public static class WoowzLib{
        static WoowzLib(){
            AppDomain     .CurrentDomain.ProcessExit        += (_, _) => Stop();
            AppDomain     .CurrentDomain.UnhandledException += (_, _) => Stop();
            TaskScheduler .UnobservedTaskException          += (_, e) => { Stop(); e.SetObserved(); };
            System.Console.CancelKeyPress                   += (_, e) => { Stop(); e.Cancel = false; };

            OnMessage += (Type, Message) => {
                string Prefix = Type switch{
                    MessageType.Warn  => "[WARN] ",
                    MessageType.Error => "[ERROR] ",
                    MessageType.Fatal => "[FATAL] ",
                    MessageType.Debug => "[DEBUG] ",
                                    _ => "",
                };

                System.Console.WriteLine(Prefix + Message[0]);

                for(int i = 1; i < Message.Length; i++){
                    System.Console.WriteLine(Message[i]?.ToString() ?? "NULL");
                }
            };
        }
        private static void Stop(){
            if(!Started){ return; }
            Started = false;

            try{
                OnStop?.Invoke();
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов на остановку приложения!", e);
            }
            
            Logger.Info("Остановлен WL!");
        }

        /// <summary>
        /// WoowzLib запущен?
        /// </summary>
        public static bool Started{ get; private set; }

        /// <summary>
        /// Запуск WoowzLib и его модулей
        /// </summary>
        public static void Start(){
            try{
                if(Started){ throw new Exception("WoowzLib уже был запущен!"); }
                Started = true;

                string RunFolder = AppContext.BaseDirectory;
                
                Console.Title = "WoowzLib Program";
                
                Logger.Info("Установка WL [\"" + RunFolder + "\"]:");
                
                foreach(string DLL in Directory.GetFiles(RunFolder, "WoowzLib.*.dll")){
                    Assembly.LoadFrom(DLL);
                }

                var Modules = AppDomain.CurrentDomain.GetAssemblies()
                       .Where(A => A.FullName != null && A.FullName.Contains("WoowzLib"))
                       .SelectMany(A => A.GetTypes().Select(T => new{
                           Type = T,
                           Attribute = T.GetCustomAttribute<WLModule>()
                       }))
                       .Where(A => A.Attribute != null)
                       .ToList().OrderBy(A => A.Attribute!.Order);

                foreach(var Module in Modules){
                    Logger.Info("Загружен WL модуль: " + Module.Type.Name);
                    RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
                }
            
                Logger.Info("Установка WL завершена!");

                try{
                    OnStarted?.Invoke();   
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при вызове ивентов после запуска всех модулей WoowzLib!", e);
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске WoowzLib!", e);
            }
        }

        /// <summary>
        /// Ивент вызывается при остановке всего приложения
        /// </summary>
        public static event Action? OnStop;

        /// <summary>
        /// Ивент вызывается после запуска всех модулей
        /// </summary>
        public static event Action? OnStarted; 
        
        /// <summary>
        /// Ивент вызывается при отправке сообщений через Logger
        /// </summary>
        public static event Action<MessageType, object[]>? OnMessage;

        /// <summary>
        /// Отправляет сообщение в OnMessage
        /// </summary>
        /// <param name="Type">Тип сообщения</param>
        /// <param name="Message">Сообщение</param>
        public static void __Print(MessageType Type, object[] Message){
            OnMessage?.Invoke(Type, Message);
        }

        /// <summary>
        /// Очистка <c>OnMessage</c> ивента
        /// </summary>
        public static void __RemoveOnMessage(){
            OnMessage = null;
        }
        
        public static class Console{
            /// <summary>
            /// Название окна консоли
            /// </summary>
            public static string Title{
                get => __Title;
                set{
                    try{
                        if(__Title == value){ return; }
                        __Title = value;

                        System.Console.Title = value;
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при установке названия окну консоли!\nНазвание: \"" + value + "\"", e);
                    }
                }
            }
            private static string __Title;
        }
    }
}