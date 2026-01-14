using System.Reflection;
using System.Runtime.CompilerServices;
using WLO;

namespace WL{
    [WLModule(int.MinValue)]
    public static class WoowzLib{
        static WoowzLib(){
            AppDomain    .CurrentDomain.ProcessExit        += (_, _) => Stop();
            AppDomain    .CurrentDomain.UnhandledException += (_, _) => Stop();
            TaskScheduler.UnobservedTaskException          += (_, e) => { Stop(); e.SetObserved(); };
            Console      .CancelKeyPress                   += (_, e) => { Stop(); e.Cancel = false; };

            OnMessage += (Type, Message) => {
                string Prefix = Type switch{
                    MessageType.Warn  => "[WARN] ",
                    MessageType.Error => "[ERROR] ",
                    MessageType.Fatal => "[FATAL] ",
                    MessageType.Debug => "[DEBUG] ",
                                    _ => "",
                };

                Console.WriteLine(Prefix + Message[0]);

                for(int i = 1; i < Message.Length; i++){
                    Console.WriteLine(Message[i]?.ToString() ?? "NULL");
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
                
                Logger.Info("Установка WL [" + RunFolder + "]:");
                
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
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске WoowzLib!", e);
            }
        }

        /// <summary>
        /// Ивент вызывается при остановке всего приложения
        /// </summary>
        public static event Action? OnStop;

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
    }
}