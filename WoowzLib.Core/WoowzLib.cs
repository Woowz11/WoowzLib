using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using WLO;

namespace WL{
    [WLModule(int.MinValue, 11)]
    public static class WoowzLib{
        static WoowzLib(){
            try{
                AppDomain.CurrentDomain.ProcessExit += (_, _) => __Stop();
                AppDomain.CurrentDomain.UnhandledException += (_, _) => __Stop();
                TaskScheduler.UnobservedTaskException += (_, e) => {
                    __Stop();
                    e.SetObserved();
                };

                global::System.Console.CancelKeyPress += (_, e) => {
                    __Stop();
                    e.Cancel = false;
                };

                OnMessage += (Type, Message) => {
                    Message ??= [null!];

                    string Prefix = Type switch{
                        MessageType.Warn => "[WARN] ",
                        MessageType.Error => "[ERROR] ",
                        MessageType.Fatal => "[FATAL] ",
                        MessageType.Debug => "[DEBUG] ",
                        _ => "",
                    };

                    global::System.Console.WriteLine(Prefix + (Message[0]?.ToString() ?? "NULL"));

                    for(int i = 1; i < Message.Length; i++){
                        global::System.Console.WriteLine(Message[i]?.ToString() ?? "NULL");
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

                #region Детект ОС

                    OSType OSType = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSType.Windows : (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSType.Linux : (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSType.OSX : (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ? OSType.FreeBSD : OSType.Unknown)));

                    if(OSType != OSType.Windows){ throw new Exception("WoowzLib пока-что работает только на Windows!"); }

                    #endregion
                
                #region Детект типа программы

                    WLO.ProgramType ProgramType = ProgramType.None;
                    
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

                Logger.Info("Установка WL [" + OSType + "] [\"" + WL.System.RunFolder + "\"]:");
                
                foreach(string DLL in Directory.GetFiles( WL.System.RunFolder, "WoowzLib.*.dll")){
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
                    Logger.Info("Загружен WL модуль: [" + Module.Attribute!.Order + "] " + Module.Type.Name + " " + Module.Attribute!.Version);
                    RuntimeHelpers.RunClassConstructor(Module.Type.TypeHandle);
                }
            
                Logger.Info("Установка WL завершена!");
                
                try{
                    OnStart?.Invoke();   
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при вызове ивентов после запуска всех модулей WoowzLib!", e);
                }
            }catch(Exception e){
                Started = false;
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
        public static event Action? OnStart; 
        
        /// <summary>
        /// Ивент вызывается при отправке сообщений через Logger
        /// </summary>
        public static event Action<MessageType, object[]?>? OnMessage;

        /// <summary>
        /// Отправляет сообщение в OnMessage
        /// </summary>
        /// <param name="Type">Тип сообщения</param>
        /// <param name="Message">Сообщение</param>
        public static void __Print(MessageType Type, object[]? Message){
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