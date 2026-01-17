using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using WLO;

namespace WLO{
    public struct TickData{
        /// <summary>
        /// Когда началось вычисление
        /// </summary>
        public double StartTime;
        /// <summary>
        /// Когда закончилось вычисление
        /// </summary>
        public double StopTime;
        /// <summary>
        /// Время выполнения в миллисекундах
        /// </summary>
        public double DeltaTime => StopTime - StartTime;
        /// <summary>
        /// Время выполнения в секундах
        /// </summary>
        public double DeltaTimeS => DeltaTime / 1000.0;
        /// <summary>
        /// Кадров в секунду
        /// </summary>
        public double FPS => WL.WoowzLib.Tick.DeltaTimeToFPS(DeltaTime);
        /// <summary>
        /// Подходит для умножения (Если DeltaTime совпадает с целью, то равен 1)
        /// </summary>
        /// <param name="TargetDelta">Целевой DeltaTime</param>
        public double Delta(double TargetDelta){ return TargetDelta / DeltaTime; }
        /// <summary>
        /// Подходит для умножения (Если FPS совпадает с целью, то равен 1)
        /// </summary>
        /// <param name="TargetFPS">Целевой FPS</param>
        public double DeltaFPS(double TargetFPS){ return Delta(WL.WoowzLib.Tick.FPSToDeltaTime(TargetFPS)); }
    }

    /// <summary>
    /// Тип скомпилированной программы
    /// </summary>
    public enum ProgramType{
        /// <summary>
        /// Консольное приложение (Exe)
        /// </summary>
        Console,
        /// <summary>
        /// Оконное приложение (WinExe)
        /// </summary>
        Window,
        /// <summary>
        /// Никакое, возможно библиотека (возможно ещё Module)
        /// </summary>
        None
    }
}

namespace WL{
    [WLModule(int.MinValue, 2)]
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

                #region Детект типа программы

                    WLO.ProgramType PT = ProgramType.None;
                    
                    if(Assembly.GetEntryAssembly() != null){
                        PT = WL.Windows.Kernel.GetConsoleWindow() != IntPtr.Zero ? ProgramType.Console : ProgramType.Window;
                    }
                    
                    ProgramType = PT;

                #endregion

                if(ProgramType == ProgramType.Window){
                    WL.Windows.Kernel.AllocConsole();
                }

                Console.__SetHandle(WL.Windows.Kernel.GetConsoleWindow());
                if(Console.Handle == IntPtr.Zero){ Logger.Warn("Не найдена консоль! Возможны ошибки"); }

                if(ProgramType == ProgramType.Window){
                    Console.Visible = false;
                }
                
                Console.OutEncoding = Encoding.UTF8;
                Console.InEncoding  = Encoding.UTF8;
                
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

        /// <summary>
        /// Тип приложения
        /// </summary>
        public static ProgramType ProgramType{ get; private set; }

        public static class Console{
            /// <summary>
            /// Ссылка на консоль
            /// </summary>
            public static IntPtr Handle{ get; private set; }
            public static void __SetHandle(IntPtr Handle__){ Handle = Handle__; }

            /// <summary>
            /// Название окна консоли
            /// </summary>
            public static string Title{
                get => System.Console.Title;
                set => System.Console.Title = value;
            }

            /// <summary>
            /// Кодировка вывода
            /// </summary>
            public static Encoding OutEncoding{
                get => System.Console.OutputEncoding;
                set => System.Console.OutputEncoding = value;
            }
            
            /// <summary>
            /// Кодировка ввода
            /// </summary>
            public static Encoding InEncoding{
                get => System.Console.InputEncoding;
                set => System.Console.InputEncoding = value;
            }

            /// <summary>
            /// Видно консоль?
            /// </summary>
            public static bool Visible{
                get => WL.Windows.Kernel.IsWindowVisible(Handle);
                set{
                    WL.Windows.Kernel.ShowWindow(Handle, value ? WL.Windows.Kernel.SW_SHOW : WL.Windows.Kernel.SW_HIDE);
                }
            }
        }
        
        public static class Tick{
            private static readonly Stopwatch __Stopwatch = Stopwatch.StartNew();

            /// <summary>
            /// Сколько ТИКОВ прошло после запуска приложения
            /// </summary>
            public static long ProgramLifeTick => __Stopwatch.ElapsedTicks;
            
            /// <summary>
            /// Сколько миллисекунд прошло после запуска приложения
            /// </summary>
            public static double ProgramLifeTime => ProgramLifeTick * 1000.0 / Stopwatch.Frequency;

            /// <summary>
            /// Конвертирует FPS в DeltaTime
            /// </summary>
            public static double FPSToDeltaTime(double FPS){
                return FPS == 0 ? 0 : 1000.0 / FPS;
            }

            /// <summary>
            /// Конвертирует DeltaTime в FPS
            /// </summary>
            public static double DeltaTimeToFPS(double DeltaTime){
                return DeltaTime == 0 ? 0 : 1000.0 / DeltaTime;
            }
            
            /// <summary>
            /// Ограничивает скорость потока по указанному DeltaTime (Стоит учитывать, что TickData берётся прошлого кадра!)
            /// </summary>
            /// <param name="UniqueID">Уникальный ID, не должны совпадать с другими функциями</param>
            /// <param name="TargetDeltaTime">Целевое время между кадрами</param>
            /// <param name="Action">Действие, которое выполняется если DeltaTime совпадает</param>
            public static void Limit(int UniqueID, double TargetDeltaTime, Action<TickData> Action){
                try{
                    bool Do = false;
                    
                    double Time = ProgramLifeTime;
                    
                    if(Timers.TryGetValue(UniqueID, out double StartTime)){
                        double Elapsed = Time - StartTime;

                        if(Elapsed >= TargetDeltaTime){ Do = true; }
                    }else{
                        Start(UniqueID);
                        Do = true;
                    }

                    if(Do){
                        if(!__TickData.TryGetValue(UniqueID, out TickData TD)){
                            TD = new TickData();
                        }
                        Action.Invoke(TD);
                        Stop(UniqueID);
                        Start(UniqueID);
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при ограничении потока через DeltaTime!\nID: " + UniqueID + "\nЦель: " + TargetDeltaTime, e);
                }
            }

            /// <summary>
            /// Ограничивает скорость потока по указанному FPS (Стоит учитывать, что TickData берётся прошлого кадра!)
            /// </summary>
            /// <param name="UniqueID">Уникальный ID, не должны совпадать с другими функциями</param>
            /// <param name="TargetFPS">Целевое FPS</param>
            /// <param name="Action">Действие, которое выполняется если FPS совпадает</param>
            public static void LimitFPS(int UniqueID, double TargetFPS, Action<TickData> Action){
                try{
                    Limit(UniqueID, FPSToDeltaTime(TargetFPS), Action);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при ограничении потока через FPS!\nID: " + UniqueID + "\nЦель: " + TargetFPS, e);
                }
            }

            /// <summary>
            /// Все запущенные вычисления информации по поводу потока
            /// </summary>
            private static readonly Dictionary<int, double> Timers = [];

            /// <summary>
            /// Все текущие вычисления информации по поводу потока
            /// </summary>
            private static readonly Dictionary<int, TickData> __TickData = [];
            
            /// <summary>
            /// Начинает вычисление информации по поводу потока (DeltaTime, FPS, ...)
            /// </summary>
            /// <param name="UniqueID">Уникальный ID, не должны совпадать с другими функциями</param>
            public static void Start(int UniqueID){
                try{
                    if(Timers.ContainsKey(UniqueID)){ throw new Exception("Запущено вычисление информации по поводу потока, хотя ещё прошлое не было завершено!"); }
                    Timers[UniqueID] = ProgramLifeTime;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при старте вычисления информации по поводу потока!\nID: " + UniqueID, e);
                }
            }

            /// <summary>
            /// Заканчивает вычисление информации по поводу потока, становятся доступными DeltaTime, FPS, ...
            /// </summary>
            /// <param name="UniqueID">Уникальный ID, должен совпадать с Start() функцией</param>
            public static TickData Stop(int UniqueID){
                try{
                    if(!Timers.TryGetValue(UniqueID, out double StartTime)){ throw new Exception("Попытка остановить вычисление информации по поводу потока не удалась, ещё не было запущено!"); }
                    double StopTime = ProgramLifeTime;
                    
                    Timers.Remove(UniqueID);

                    TickData TD = new TickData{
                        StartTime = StartTime,
                        StopTime  = StopTime
                    };
                    
                    __TickData[UniqueID] = TD;
                    
                    return TD;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при остановке вычисления информации по поводу потока!\nID: " + UniqueID, e);
                }
            }
        }
    }
}

namespace WL.Windows{
    public static class Kernel{
        [DllImport("kernel32")]
        public static extern IntPtr GetConsoleWindow();
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpFileName);
        
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
    }
}