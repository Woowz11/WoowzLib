using System.Reflection;
using System.Runtime.CompilerServices;
using WLO;

namespace WL{
    [WLModule(int.MinValue, 0)]
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
                    Logger.Info("Загружен WL модуль: " + Module.Type.Name + " " + Module.Attribute!.Version);
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
        
        /// <summary>
        /// Условие типа <c>Condition ? IfTrue : IfFalse</c> но в виде функции
        /// </summary>
        /// <param name="Condition">Условие</param>
        /// <param name="IfTrue">Если равно true</param>
        /// <param name="IfFalse">Если равно false</param>
        /// <returns><c>Condition ? IfTrue : IfFalse</c></returns>
        public static object? Condition(bool Condition, object? IfTrue, object? IfFalse){
            return Condition ? IfTrue : IfFalse;
        }

        /// <summary>
        /// Папка, где запущено приложение
        /// </summary>
        public static string RunFolder => AppContext.BaseDirectory;
        
        public static class Console{
            /// <summary>
            /// Название окна консоли
            /// </summary>
            public static string Title{
                get => System.Console.Title;
                set{
                    System.Console.Title = value;
                }
            }
        }
    }
}