using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using WLO;

namespace WL{
    [WLModule(int.MinValue, 51)]
    public static class WoowzLib{
        static WoowzLib(){
            try{
                AppDomain.CurrentDomain.ProcessExit += (_, _) => __Stop();
                AppDomain.CurrentDomain.UnhandledException += (_, _) => __Stop();
                TaskScheduler.UnobservedTaskException += (_, e) => {
                    __Stop();
                    e.SetObserved();
                };

                Console.CancelKeyPress += (_, e) => {
                    __Stop();
                    e.Cancel = false;
                };

                OnMessage += (Type, Message) => {
                    Message ??= [null!];

                    string Prefix = Type switch{
                        Logger.MessageType.Warn  => "[WARN] ",
                        Logger.MessageType.Error => "[ERROR] ",
                        Logger.MessageType.Fatal => "[FATAL] ",
                        Logger.MessageType.Debug => "[DEBUG] ",
                        _ => "",
                    };

                    Console.WriteLine(Prefix + (Message[0]?.ToString() ?? WL.System.StringNull));

                    for(int i = 1; i < Message.Length; i++){
                        Console.WriteLine(Message[i]?.ToString() ?? WL.System.StringNull);
                    }
                };
            }catch(Exception e){
                throw new Exception("Произошла ошибка при главной инициализации WoowzLib!", e);
            }
        }
        
        private static void __Stop(){
            if(!Started){ return; }
            Started = false;

            try{
                OnStop?.Invoke();
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов на остановку WoowzLib!", e);
            }
            
            WL.System.__DisconnectWoowzLib();
            
            Logger.Info("Остановлен WL!");
        }

        /// <summary>
        /// WoowzLib запущен?
        /// </summary>
        public static bool Started{ get; private set; }

        /// <summary>
        /// Информация об проекте
        /// </summary>
        public static WoowzLibInfo ProjectInfo{ get; private set; }

        /// <summary>
        /// Запуск WoowzLib и его модулей
        /// <param name="Info">Дополнительная информация</param>
        /// </summary>
        public static void Start(WoowzLibInfo Info = default){
            try{
                if(Started){ throw new Exception("WoowzLib уже был запущен!"); }
                Started = true;

                ProjectInfo = Info;
                
                #region Детект ОС

                    OSType OSType = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSType.Windows : (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSType.Linux : (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSType.OSX : (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ? OSType.FreeBSD : OSType.Unknown)));

                    if(OSType != OSType.Windows){ throw new Exception("WoowzLib пока-что работает только на Windows!"); }

                    #endregion
                
                #region Детект типа программы

                    ProgramType ProgramType = ProgramType.None;
                    
                    if(Assembly.GetEntryAssembly() != null){
                        ProgramType = WL.System.Native.Windows.GetConsoleWindow() != IntPtr.Zero ? ProgramType.Console : ProgramType.Window;
                    }
                

                #endregion

                if(ProgramType == ProgramType.Window){
                    WL.System.Native.Windows.AllocConsole();
                }
                
                WL.System.__ConnectWoowzLib(ProgramType, OSType);

                WL.System.Console.__SetHandle(WL.System.Native.Windows.GetConsoleWindow());
                if(WL.System.Console.Handle == IntPtr.Zero){ Logger.Warn("Не найдена консоль! Возможны ошибки"); }

                if(ProgramType == ProgramType.Window){
                    WL.System.Console.Visible = false;
                }
                
                WL.System.Console.OutEncoding = Encoding.UTF8;
                WL.System.Console.InEncoding  = Encoding.UTF8;
                
                Console.Title = "WoowzLib Program";

                Logger.Info("Установка WL [" + OSType + " : \"" + ProjectInfo.Name + " " + ProjectInfo.Version + "\" на \"" + ProjectInfo.Engine + " " + ProjectInfo.EngineVersion + "\"] [\"" + WL.System.RunFolder + "\"]:");
                
                foreach(string DLL in Directory.GetFiles( WL.System.RunFolder, "WoowzLib.*.dll")){
                    Assembly Assembly = Assembly.LoadFrom(DLL);
                    LoadedModules[Assembly.GetName().Name!.Replace("WoowzLib.", "") ] = Assembly;
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
                    Logger.Info("Загружен WL модуль: [" + Module.Attribute!.Order + "] " + Module.Type.Name + " " + Module.Attribute!.Version);
                    RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
                }
            
                Logger.Info("Установка WL завершена!");
                
                try{
                    OnStart?.Invoke();   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов после запуска всех модулей WoowzLib!", e);
                }
            }catch(Exception e){
                Started = false;
                throw new Exception("Произошла ошибка при запуске WoowzLib!", e);
            }
        }

        /// <summary>
        /// Нужно вызывать каждый кадр внутри while, желательно без задержек, иначе будет тормозить
        /// </summary>
        public static void Update(){
            try{
                try{
                    OnUpdate?.Invoke();   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов обновления WoowzLib!", e);
                }
                
                while(System.Native.Windows.PeekMessage(out System.Native.Windows.MSG Message, IntPtr.Zero, 0, 0, System.Native.Windows.PM_REMOVE)){
                    System.Native.Windows.TranslateMessage(ref Message);
                    System.Native.Windows.DispatchMessage (ref Message);
                }
                
                System.Sound.__Update();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обновлении WoowzLib!", e);
            }
        }
        
        /// <summary>
        /// Все загруженные модули
        /// </summary>
        public static readonly Dictionary<string, Assembly> LoadedModules = new Dictionary<string, Assembly>();

        /// <summary>
        /// Версия модуля
        /// </summary>
        public static string Version => WL.System.GetVersion(LoadedModules["Core"]);

        /// <summary>
        /// Вызывается при остановке всего приложения
        /// </summary>
        public static event Action? OnStop;

        /// <summary>
        /// Вызывается после запуска всех модулей
        /// </summary>
        public static event Action? OnStart; 
        
        /// <summary>
        /// Вызывается при отправке сообщений через Logger
        /// </summary>
        public static event Action<Logger.MessageType, object[]?>? OnMessage;

        /// <summary>
        /// Вызывается каждый кадр
        /// </summary>
        public static event Action? OnUpdate;

        /// <summary>
        /// Отправляет сообщение в OnMessage
        /// </summary>
        /// <param name="Type">Тип сообщения</param>
        /// <param name="Message">Сообщение</param>
        public static void __Print(Logger.MessageType Type, object[]? Message){
            OnMessage?.Invoke(Type, Message);
        }

        /// <summary>
        /// Очистка <c>OnMessage</c> ивента
        /// </summary>
        public static void __RemoveOnMessage(){
            OnMessage = null;
        }
    }
}

namespace WLO{
    public readonly struct WoowzLibInfo(string Name = "New Project", uint Version = 0, string Engine = "WoowzLib", uint EngineVersion = 0, string Author = "Anonymous", string License = "MIT"){
        /// <summary>
        /// Название проекта
        /// </summary>
        public readonly string Name = Name;
        
        /// <summary>
        /// Версия проекта
        /// </summary>
        public readonly uint Version = Version;
        
        /// <summary>
        /// Движок проекта
        /// </summary>
        public readonly string Engine = Engine;

        /// <summary>
        /// Версия движка проекта
        /// </summary>
        public readonly uint EngineVersion = EngineVersion;
        
        /// <summary>
        /// Автор проекта
        /// </summary>
        public readonly string Author = Author;
        
        /// <summary>
        /// Лицензия проекта
        /// </summary>
        public readonly string License = License;
    }
}