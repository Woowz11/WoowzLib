using System.Reflection;
using System.Runtime.CompilerServices;

namespace WL{
    [WLModule(int.MinValue)]
    public static class WoowzLib{
        static WoowzLib(){
            AppDomain    .CurrentDomain.ProcessExit        += (_, _) => Stop();
            AppDomain    .CurrentDomain.UnhandledException += (_, _) => Stop();
            TaskScheduler.UnobservedTaskException          += (_, e) => { Stop(); e.SetObserved(); };
            Console      .CancelKeyPress                   += (_, e) => { Stop(); e.Cancel = false; };
        }
        private static void Stop(){
            if(!Started){ return; }
            Started = false;

            try{
                OnStop?.Invoke();
            }catch(Exception e){
                Console.WriteLine("Произошла ошибка при вызове ивентов на остановку приложения!");
                Console.WriteLine(e);
            }
            
            Console.WriteLine("Остановлен WL!");
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

                Console.WriteLine("Установка WL:");

                string RunFolder = AppContext.BaseDirectory;
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
                    Console.WriteLine("Загружен WL модуль: " + Module.Type.Name);
                    RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
                }
            
                Console.WriteLine("Установка WL завершена!");
            }catch(Exception e){
                throw new Exception("Произошла ошибка при запуске WoowzLib!");
            }
        }

        /// <summary>
        /// Ивент вызывается при остановке всего приложения
        /// </summary>
        public static event Action? OnStop;
    }
}