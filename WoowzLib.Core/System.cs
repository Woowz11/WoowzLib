using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using WLO;

namespace WL{
    
    [WLModule(int.MinValue + 1, 14)]
    public class System{
        /// <summary>
        /// Папка, где запущено приложение
        /// </summary>
        public static string RunFolder => AppContext.BaseDirectory;

        /// <summary>
        /// Тип приложения
        /// </summary>
        public static ProgramType ProgramType{ get; private set; }
        
        /// <summary>
        /// Какая ОС?
        /// </summary>
        public static OSType OSType{ get; private set; }

        /// <summary>
        /// Присоединение WoowzLib x System
        /// </summary>
        public static void __ConnectWoowzLib(ProgramType ProgramType__, OSType OSType__){
            ProgramType = ProgramType__;
            OSType      = OSType__;
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
                get => global::System.Console.OutputEncoding;
                set => global::System.Console.OutputEncoding = value;
            }
            
            /// <summary>
            /// Кодировка ввода
            /// </summary>
            public static Encoding InEncoding{
                get => global::System.Console.InputEncoding;
                set => global::System.Console.InputEncoding = value;
            }

            /// <summary>
            /// Видно консоль?
            /// </summary>
            public static bool Visible{
                get => Native.Windows.IsWindowVisible(Handle);
                set{
                    Native.Windows.ShowWindow(Handle, value ? Native.Windows.SW_SHOW : Native.Windows.SW_HIDE);
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
                        }else{
                            if(TD.Tick      == -1){ TD.Tick      = 0; }
                            if(TD.DeltaTick == -1){ TD.DeltaTick = 0; }

                            TD.Tick++;
                            TD.DeltaTick += TD.DeltaTimeS;
                            
                            __TickData[UniqueID] = TD;
                        }

                        Action.Invoke(TD);
                        Stop (UniqueID);
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
                }
                catch(Exception e){
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

                    bool HasOldTD = __TickData.TryGetValue(UniqueID, out TickData OldTD);
                    
                    TickData TD = new TickData{
                        StartTime = StartTime, StopTime = StopTime,
                        Tick      = HasOldTD ? OldTD.Tick      : -1,
                        DeltaTick = HasOldTD ? OldTD.DeltaTick : -1
                    };

                    __TickData[UniqueID] = TD;

                    return TD;
                }
                catch(Exception e){
                    throw new Exception("Произошла ошибка при остановке вычисления информации по поводу потока!\nID: " + UniqueID, e);
                }
            }
        }
        
        public static class Byte{
            /// <summary>
            /// Создаёт число из 4-х байтов (AA, BB, CC, DD) -> 0xAABBCCDD
            /// </summary>
            public static uint Byte4(byte B1, byte B2, byte B3, byte B4){ return (uint)(B1 << 24 | B2 << 16 | B3 << 8 | B4); }

            public static byte Byte4_1(uint V){ return (byte)((V >> 24) & 0xFF); }
            public static byte Byte4_2(uint V){ return (byte)((V >> 16) & 0xFF); }
            public static byte Byte4_3(uint V){ return (byte)((V >> 8 ) & 0xFF); }
            public static byte Byte4_4(uint V){ return (byte)( V        & 0xFF); }

            /// <summary>
            /// Создаёт RGBA
            /// </summary>
            public static uint RGBA(byte R, byte G, byte B, byte A){ return Byte4(R, G, B, A); }
                
            /// <summary>
            /// Создаёт ABGR
            /// </summary>
            public static uint ABGR(byte A, byte B, byte G, byte R){ return Byte4(A, B, G, R); }

            /// <summary>
            /// Превращает 0xAABBCCDD -> 0xDDCCBBAA 
            /// </summary>
            public static uint Byte4_Inverse(uint V){ return Byte4(Byte4_4(V), Byte4_3(V), Byte4_2(V), Byte4_1(V)); }

            /// <summary>
            /// Превращает RGBA -> ABGR
            /// </summary>
            public static uint RGBA_To_ABGR(uint RGBA){ return Byte.Byte4_Inverse(RGBA); }

            /// <summary>
            /// Превращает ABGR -> RGBA
            /// </summary>
            public static uint ABGR_To_RGBA(uint ABGR){ return Byte.Byte4_Inverse(ABGR); }

            public static byte ToColorByte(byte   V){ return        V       ; }
            public static byte ToColorByte(int    V){ return (byte) V       ; }
            public static byte ToColorByte(float  V){ return (byte)(V * 255); }
            public static byte ToColorByte(double V){ return (byte)(V * 255); }
        }
        
        public static class HDC{
            /// <summary>
            /// Рисование в окне
            /// </summary>
            /// <param name="Window">Ссылка на окно</param>
            /// <param name="Action">Рисование</param>
            public static void PaintWindow(IntPtr Window, Action<IntPtr> Action){
                try{
                    IntPtr HDC__ = Native.Windows.BeginPaint(Window, out Native.Windows.PAINTSTRUCT PS);
                    try{
                        Action.Invoke(HDC__);   
                    }finally{
                        Native.Windows.EndPaint(Window, ref PS);
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при рисовании в окно [" + Window + "]!", e);
                }
            }

            /// <summary>
            /// Получает размер окна
            /// </summary>
            /// <param name="Window">Ссылка на окно</param>
            public static Native.Windows.RECT WindowSize(IntPtr Window){
                Native.Windows.GetClientRect(Window, out Native.Windows.RECT Result);
                return Result;
            }

            /// <summary>
            /// Создаёт кисть с цветом (Нужно уничтожать!)
            /// </summary>
            /// <param name="Color">Цвет RGBA</param>
            public static IntPtr CreateBrush(uint Color){ return Native.Windows.CreateSolidBrush(Byte.RGBA_To_ABGR(Color)); }

            public static void DestroyBrush(IntPtr Brush){ Native.Windows.DeleteObject(Brush); }

            /// <summary>
            /// Заполнить цветом область
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Rect">Область</param>
            /// <param name="Brush">Кисть</param>
            public static void Fill(IntPtr HDC, Native.Windows.RECT Rect, IntPtr Brush) => Native.Windows.FillRect(HDC, ref Rect, Brush);

            /// <summary>
            /// Заполнить цветом область
            /// </summary>
            /// <param name="HDC">Куда рисовать?</param>
            /// <param name="Rect">Область</param>
            public static void Fill(IntPtr HDC, Native.Windows.RECT Rect, uint Color){
                IntPtr Brush = CreateBrush(Color);
                Fill(HDC, Rect, Brush);
                DestroyBrush(Brush);
            }
        }
        
        public static class Native{
            public const string Error_DLLNotExist      = "Не найден DLL!";
            public const string Error_FunctionNotFound = "Функция не найдена!";

            private static readonly Dictionary<string, IntPtr> LoadedDLL = new Dictionary<string, IntPtr>(StringComparer.OrdinalIgnoreCase);
            
            /// <summary>
            /// Загружен ли указанный DLL?
            /// </summary>
            /// <param name="DLLPath">Путь до DLL</param>
            /// <returns>Загружен?</returns>
            public static bool Loaded(string DLLPath){
                return LoadedDLL.ContainsKey(DLLPath) && LoadedDLL[DLLPath] != IntPtr.Zero;
            }
            
            /// <summary>
            /// Загрузка DLL файла
            /// </summary>
            /// <param name="DLLName">Название DLL файла</param>
            /// <returns>Ссылка на загруженный DLL файл</returns>
            public static IntPtr Load(string DLLName){
                try{
                    if(string.IsNullOrWhiteSpace(DLLName)){ throw new Exception("Имя DLL файла пустое!"); }
                    if(LoadedDLL.TryGetValue(DLLName, out IntPtr Handle) && Handle != IntPtr.Zero){ throw new Exception("Этот DLL уже был загружен! Handle: " + Handle); }

                    Handle = Windows.LoadLibrary(DLLName);
                    if(Handle == IntPtr.Zero){ throw new Exception("Не получилось загрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error()); }

                    LoadedDLL[DLLName] = Handle;
                    return Handle;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при загрузке DLL [" + DLLName + "]!", e);
                }
            }
            
            /// <summary>
            /// Разгрузка DLL файла
            /// </summary>
            /// <param name="DLLName">Название DLL файла</param>
            public static void Unload(string DLLName){
                try{
                    if(!LoadedDLL.TryGetValue(DLLName, out IntPtr Handle) || Handle == IntPtr.Zero){ throw new Exception(Error_DLLNotExist); }
                    
                    if(!Windows.FreeLibrary(Handle)){ throw new Exception("Не получилось выгрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error()); }
                    
                    LoadedDLL.Remove(DLLName);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при разгрузке DLL [" + DLLName + "]!", e);
                }
            }
            
            /// <summary>
            /// Разгрузка DLL файла
            /// </summary>
            /// <param name="DLL">Ссылка на DLL файла</param>
            public static void Unload(IntPtr DLL){
                try{
                    if(DLL == IntPtr.Zero){ throw new Exception("Указанная ссылка пустая!"); }
                    
                    if(!Windows.FreeLibrary(DLL)){ throw new Exception("Не получилось выгрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error()); }

                    string? KTR = (from KVP in LoadedDLL where KVP.Value == DLL select KVP.Key).FirstOrDefault();

                    if(KTR != null){
                        LoadedDLL.Remove(KTR);
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при разгрузке DLL (IntPtr) [" + DLL + "]!", e);
                }
            }
            
            /// <summary>
            /// Получает ссылку на функцию из DLL
            /// </summary>
            /// <param name="DLLName">Название DLL файла</param>
            /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
            /// <returns>Ссылка на функцию</returns>
            public static IntPtr Function(string DLLName, string Name){
                try{
                    if(!LoadedDLL.TryGetValue(DLLName, out IntPtr Handle)){ throw new Exception(Error_DLLNotExist); }

                    return Function(Handle, Name);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при загрузке функции из DLL [" + DLLName + "]!\nФункция: " + Name);
                }
            }
            
            /// <summary>
            /// Получает ссылку на функцию из DLL (по ссылке)
            /// </summary>
            /// <param name="DLL">Ссылка на DLL</param>
            /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
            /// <returns>Ссылка на функцию</returns>
            public static IntPtr Function(IntPtr DLL, string Name){
                try{
                    IntPtr Proc = Windows.GetProcAddress(DLL, Name);
                    return Proc == IntPtr.Zero ? throw new Exception(Error_FunctionNotFound) : Proc;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при загрузке функции из DLL (IntPtr) [" + DLL + "]!\nФункция: " + Name);
                }
            }
            
            /// <summary>
            /// Получает функцию из ссылки на DLL и возвращает её в виде C# функции
            /// </summary>
            /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
            /// <param name="DLL">Ссылка на DLL</param>
            /// <typeparam name="D">Тип функции (точно совпадает с её параметрами и возвращаемым значением)</typeparam>
            /// <returns>Функция которую можно вызвать как C# функцию</returns>
            public static D DelegateFunction<D>(string Name, IntPtr DLL) where D : Delegate{
                return Marshal.GetDelegateForFunctionPointer<D>(Function(DLL, Name));
            }
            
            /// <summary>
            /// Сохраняет строку в память (Нужно очищать!)
            /// </summary>
            /// <param name="S">Строка</param>
            /// <returns>Ссылка на строку</returns>
            public static IntPtr MemoryString(string S){
                return Marshal.StringToHGlobalAnsi(S);
            }

            /// <summary>
            /// Сохраняет строку в память (с поддержкой уникальных символов) (Нужно очищать!)
            /// </summary>
            /// <param name="S">Строка</param>
            /// <returns>Ссылка на строку</returns>
            public static IntPtr MemoryStringUTF(string S){
                byte[] Bytes = global::System.Text.Encoding.UTF8.GetBytes(S + '\0');
                IntPtr Link = MemoryEmpty(Bytes.Length);
                Marshal.Copy(Bytes, 0, Link, Bytes.Length);
                return Link;
            }

            /// <summary>
            /// Даёт ссылку на память указанного размера
            /// </summary>
            /// <param name="ByteSize">Какого размера дать ссылку на память?</param>
            /// <returns>Ссылка на память</returns>
            public static IntPtr MemoryEmpty(int ByteSize){
                return Marshal.AllocHGlobal(ByteSize);
            }

            /// <summary>
            /// Сохраняет struct в память (Нужно очищать!)
            /// </summary>
            /// <param name="Data">Сам struct</param>
            /// <typeparam name="T">Тип struct</typeparam>
            /// <returns>Ссылка на struct</returns>
            public static IntPtr Memory<T>(T Data) where T : struct{
                IntPtr Link = MemoryEmpty<T>();
                Marshal.StructureToPtr(Data, Link, false);
                return Link;
            }
            
            /// <summary>
            /// Выделяет память размера указанного struct (Нужно очищать!)
            /// </summary>
            /// <typeparam name="T">Тип struct</typeparam>
            /// <param name="Count">Кол-во struct</param>
            /// <returns>Ссылка на память</returns>
            public static IntPtr MemoryEmpty<T>(int Count = 1) where T : struct{
                return Marshal.AllocHGlobal(Marshal.SizeOf<T>() * Count);
            }

            /// <summary>
            /// Освобождает память
            /// </summary>
            /// <param name="Link">Ссылка на занятую ячейку</param>
            public static void Free(IntPtr Link){
                Marshal.FreeHGlobal(Link);
            }

            /// <summary>
            /// Получает строку из памяти
            /// </summary>
            /// <param name="Link">Ссылка на строку</param>
            /// <returns>Строка (если память пуста, то вернёт <c>null</c>)</returns>
            public static string? FromMemoryString(IntPtr Link){
                return Marshal.PtrToStringAnsi(Link);
            }

            /// <summary>
            /// Присоединяет WinAPI ивент для окна окну
            /// </summary>
            /// <param name="Window">Окно</param>
            /// <param name="Event">Ивент [Окно, действие, параметр W, параметр L]</param>
            /// <returns>Ссылка на ивент</returns>
            public static Windows.WndProcDelegate ConnectEventsToWindow(IntPtr Window, Func<IntPtr, uint, IntPtr, IntPtr, IntPtr> Event){
                Windows.WndProcDelegate Events__ = new System.Native.Windows.WndProcDelegate((Window, Message, WParam, LParam) => Event(Window, Message, WParam, LParam));

                IntPtr p = Marshal.GetFunctionPointerForDelegate(Events__);
                System.Native.Windows.SetWindowLongPtrW(Window, System.Native.Windows.GWLP_WNDPROC, p);

                return Events__;
            }
            
            public static class Windows{
                private const string DLL_Kernel = "kernel32.dll";
                private const string DLL_User   = "user32.dll";
                private const string DLL_GDI    = "gdi32.dll";
                
                [DllImport(DLL_Kernel)]
                public static extern IntPtr GetConsoleWindow();
                
                [DllImport(DLL_User)]
                public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
                
                [DllImport(DLL_User)]
                public static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);
                
                [DllImport(DLL_Kernel, SetLastError = true, CharSet = CharSet.Unicode)]
                public static extern IntPtr LoadLibrary(string lpFileName);
                
                [DllImport(DLL_Kernel, SetLastError = true)]
                public static extern bool FreeLibrary(IntPtr hModule);
                
                [DllImport(DLL_Kernel, SetLastError = true, CharSet = CharSet.Ansi)]
                public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
                
                [DllImport(DLL_Kernel)]
                public static extern bool AllocConsole();
                
                [DllImport(DLL_User)]
                public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

                [DllImport(DLL_GDI)]
                public static extern bool SetViewportOrgEx(
                    IntPtr hdc,
                    int x,
                    int y,
                    out POINT lpPoint
                );
                
                [DllImport(DLL_GDI)]
                public static extern bool BitBlt(
                    IntPtr hDestDC,
                    int xDest,
                    int yDest,
                    int nWidth,
                    int nHeight,
                    IntPtr hSrcDC,
                    int xSrc,
                    int ySrc,
                    uint dwRop
                );
                
                [DllImport(DLL_User)]
                public static extern bool IsWindowVisible(IntPtr hWnd);
                
                [DllImport(DLL_User, SetLastError = true)]
                public static extern IntPtr GetDC(IntPtr hWnd);

                [DllImport(DLL_User, SetLastError = true)]
                public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
                
                [DllImport(DLL_GDI, CallingConvention = CallingConvention.StdCall)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SwapBuffers(IntPtr hdc);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern IntPtr CreateWindowExW(
                    uint dwExStyle,
                    string lpClassName,
                    string lpWindowName,
                    uint dwStyle,
                    int X,
                    int Y,
                    int nWidth,
                    int nHeight,
                    IntPtr hWndParent,
                    IntPtr hMenu,
                    IntPtr hInstance,
                    IntPtr lpParam
                );
                
                [DllImport(DLL_User, SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DestroyWindow(IntPtr hWnd);

                [DllImport(DLL_Kernel, CharSet = CharSet.Unicode)]
                public static extern IntPtr GetModuleHandle(string? lpModuleName);

                [DllImport(DLL_User, SetLastError = true, CharSet = CharSet.Unicode)]
                public static extern ushort RegisterClassExW(ref WNDCLASSEX lpwcx);

                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct WNDCLASSEX{
                    public uint   cbSize;
                    public uint   style;
                    public IntPtr lpfnWndProc;
                    public int    cbClsExtra;
                    public int    cbWndExtra;
                    public IntPtr hInstance;
                    public IntPtr hIcon;
                    public IntPtr hCursor;
                    public IntPtr hbrBackground;
                    [MarshalAs(UnmanagedType.LPWStr)]
                    public string lpszMenuName;
                    [MarshalAs(UnmanagedType.LPWStr)]
                    public string lpszClassName;
                    public IntPtr hIconSm;
                }
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode)]
                public static extern IntPtr DefWindowProcW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
                
                public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
                
                [DllImport(DLL_User)]
                public static extern bool UpdateWindow(IntPtr hWnd);
                
                public static IntPtr EmptyWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam){ return DefWindowProcW(hWnd, msg, wParam, lParam); }
                
                [StructLayout(LayoutKind.Sequential)]
                public struct MSG{
                    public IntPtr hwnd;
                    public uint   message;
                    public IntPtr wParam;
                    public IntPtr lParam;
                    public uint   time;
                    public POINT  pt;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct POINT{
                    public int x;
                    public int y;
                }

                [DllImport(DLL_User)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

                [DllImport(DLL_User)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool TranslateMessage([In] ref MSG lpMsg);

                [DllImport(DLL_User)]
                public static extern IntPtr DispatchMessage([In] ref MSG lpMsg);
                
                [DllImport(DLL_User)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);
                
                [DllImport(DLL_GDI)]
                public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode)]
                public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

                public static string GetWindowTitle(IntPtr hwnd){
                    StringBuilder sb = new StringBuilder(256);
                    int len = GetWindowText(hwnd, sb, sb.Capacity);
                    return sb.ToString(0, len);
                }
                
                [DllImport(DLL_User)]
                public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

                [DllImport(DLL_GDI, SetLastError = true)]
                public static extern int SaveDC(IntPtr hdc);
                
                [DllImport(DLL_GDI, SetLastError = true)]
                public static extern int IntersectClipRect(
                    IntPtr hdc,
                    int left,
                    int top,
                    int right,
                    int bottom
                );
                
                [DllImport(DLL_GDI, SetLastError = true)]
                public static extern bool RestoreDC(
                    IntPtr hdc,
                    int savedDC
                );
                
                [StructLayout(LayoutKind.Sequential)]
                public struct RECT{
                    public int left;
                    public int top;
                    public int right;
                    public int bottom;
                }
                
                [DllImport(DLL_User)]
                public static extern IntPtr GetWindowDC(IntPtr hWnd);
                
                [StructLayout(LayoutKind.Sequential)]
                public struct PIXELFORMATDESCRIPTOR {
                    public ushort nSize;
                    public ushort nVersion;
                    public uint   dwFlags;
                    public byte   iPixelType;
                    public byte   cColorBits;
                    public byte   cRedBits;
                    public byte   cRedShift;
                    public byte   cGreenBits;
                    public byte   cGreenShift;
                    public byte   cBlueBits;
                    public byte   cBlueShift;
                    public byte   cAlphaBits;
                    public byte   cAlphaShift;
                    public byte   cAccumBits;
                    public byte   cAccumRedBits;
                    public byte   cAccumGreenBits;
                    public byte   cAccumBlueBits;
                    public byte   cAccumAlphaBits;
                    public byte   cDepthBits;
                    public byte   cStencilBits;
                    public byte   cAuxBuffers;
                    public byte   iLayerType;
                    public byte   bReserved;
                    public uint   dwLayerMask;
                    public uint   dwVisibleMask;
                    public uint   dwDamageMask;
                }
                
                [DllImport(DLL_GDI)]
                public static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR ppfd);

                [DllImport(DLL_GDI)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SetPixelFormat(IntPtr hdc, int format, ref PIXELFORMATDESCRIPTOR ppfd);
                
                [DllImport(DLL_GDI)]
                public static extern int GetPixelFormat(IntPtr hdc);

                [DllImport(DLL_GDI)]
                public static extern int DescribePixelFormat(
                    IntPtr hdc,
                    int iPixelFormat,
                    int nBytes,
                    out PIXELFORMATDESCRIPTOR ppfd
                );
                
                [DllImport(DLL_Kernel)]
                public static extern uint GetLastError();
                
                [DllImport(DLL_User, SetLastError = true)]
                public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern IntPtr SetWindowLongPtrW(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
                
                [DllImport(DLL_User)]
                public static extern IntPtr SetCursor(IntPtr hCursor);
               
                [DllImport(DLL_User, SetLastError = true)]
                public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
                
                public static IntPtr CURSOR_Arrow = System.Native.Windows.LoadCursor(IntPtr.Zero, System.Native.Windows.IDC_ARROW);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern bool SetWindowTextW(IntPtr hWnd, string lpString);
                
                [DllImport(DLL_User, SetLastError = true)]
                public static extern bool SetWindowPos(
                    IntPtr hWnd,
                    IntPtr hWndInsertAfter,
                    int X,
                    int Y,
                    int cx,
                    int cy,
                    uint uFlags
                );
                
                [DllImport(DLL_User, SetLastError = true)]
                public static extern bool AdjustWindowRectEx(
                    ref RECT lpRect,
                    uint dwStyle,
                    bool bMenu,
                    uint dwExStyle
                );
                
                [StructLayout(LayoutKind.Sequential)]
                public struct WINDOWPOS
                {
                    public IntPtr hwnd;
                    public IntPtr hwndInsertAfter;
                    public int    x;
                    public int    y;
                    public int    cx;
                    public int    cy;
                    public uint   flags;
                }
                
                [StructLayout(LayoutKind.Sequential)]
                public struct PAINTSTRUCT
                {
                    public IntPtr hdc;
                    public bool   fErase;
                    public RECT   rcPaint;
                    public bool   fRestore;
                    public bool   fIncUpdate;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                    public byte[] rgbReserved;
                }
                
                [DllImport(DLL_User)]
                public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

                [DllImport(DLL_User)]
                public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

                [DllImport(DLL_User)]
                public static extern int FillRect(IntPtr hDC, ref RECT lprc, IntPtr hbr);

                [DllImport(DLL_GDI)]
                public static extern IntPtr CreateSolidBrush(uint color);

                [DllImport(DLL_GDI)]
                public static extern bool DeleteObject(IntPtr hObject);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern uint GetClassLongW(IntPtr hWnd, int nIndex);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern IntPtr GetClassLongPtrW(IntPtr hWnd, int nIndex);
                
                [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern IntPtr CallWindowProcW(
                    IntPtr lpPrevWndFunc,
                    IntPtr hWnd,
                    uint Msg,
                    IntPtr wParam,
                    IntPtr lParam
                );
                
                [DllImport(DLL_User)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool EnumChildWindows(IntPtr hwndParent, EnumChildProc lpEnumFunc, IntPtr lParam);
                
                public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);
                
                [DllImport("user32.dll")]
                public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
                
                [DllImport("user32.dll", CharSet = CharSet.Unicode)]
                public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
                
                public static void SetChildRedraw(IntPtr parent, bool enable, string classFilter = null)
                {
                    EnumChildWindows(parent, (hwnd, lParam) =>
                    {
                        if(classFilter != null){
                            StringBuilder sb = new StringBuilder(MAX_CLASS_NAME);
                            GetClassName(hwnd, sb, sb.Capacity);
                            if (!sb.ToString().Contains(classFilter)) return true;
                        }

                        SendMessage(hwnd, WM_SETREDRAW, new IntPtr(enable ? 1 : 0), IntPtr.Zero);

                        if(enable){
                            InvalidateRect(hwnd, IntPtr.Zero, true);
                        }

                        return true;
                    }, IntPtr.Zero);
                }
                
                public static readonly IntPtr HWND_TOP       = new IntPtr(0);
                public static readonly IntPtr HWND_BOTTOM    = new IntPtr(1);
                public static readonly IntPtr HWND_TOPMOST   = new IntPtr(-1);
                public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
                
                public const int  MAX_CLASS_NAME      = 256;
                public const int  GCLP_WNDPROC        = -24;
                public const int  SW_HIDE             = 0;
                public const int  SW_SHOW             = 5;
                public const uint WS_EX_NOACTIVATE    = 0x08000000;
                public const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
                public const uint PM_REMOVE           = 0x0001;
                public const int  HORZRES             = 8;
                public const int  VERTRES             = 10;
                public const uint PFD_DRAW_TO_WINDOW  = 0x00000004;
                public const uint PFD_SUPPORT_OPENGL  = 0x00000020;
                public const uint PFD_DOUBLEBUFFER    = 0x00000001;
                public const byte PFD_TYPE_RGBA       = 0;
                public const byte PFD_MAIN_PLANE      = 0;
                public const int  GWLP_USERDATA       = -21;
                public const uint WM_DESTROY          = 0x0002;
                public const uint WM_MOVE             = 0x0003;
                public const uint WM_SHOWWINDOW       = 0x0018;
                public const uint WM_KEYDOWN          = 0x0100;
                public const uint WM_KEYUP            = 0x0101;
                public const uint WM_CHAR             = 0x0102;
                public const uint WM_MOUSEMOVE        = 0x0200;
                public const uint WM_LBUTTONDOWN      = 0x0201;
                public const uint WM_LBUTTONUP        = 0x0202;
                public const uint WM_RBUTTONDOWN      = 0x0204;
                public const uint WM_RBUTTONUP        = 0x0205;
                public const uint WM_MOUSEWHEEL       = 0x020A;
                public const uint WM_PAINT            = 0x000F;
                public const uint WM_SETCURSOR        = 0x0020;
                public const uint WM_ACTIVATE         = 0x0006;
                public const uint WM_SETFOCUS         = 0x0007;
                public const uint WM_KILLFOCUS        = 0x0008;
                public const uint WM_SIZE             = 0x0005;
                public const uint WM_CLOSE            = 0x0010;
                public const uint WM_WINDOWPOSCHANGED = 0x0047;
                public const uint WM_COMMAND          = 0x0111;
                public const uint WM_SETTEXT          = 0x000C;
                public const uint SWP_NOSIZE          = 0x0001;
                public const uint SWP_NOMOVE          = 0x0002;
                public const uint SWP_NOZORDER        = 0x0004;
                public const uint SWP_NOREDRAW        = 0x0008;
                public const uint SWP_NOACTIVATE      = 0x0010;
                public const uint SWP_FRAMECHANGED    = 0x0020;
                public const uint SWP_SHOWWINDOW      = 0x0040;
                public const uint SWP_HIDEWINDOW      = 0x0080;
                public const uint SWP_NOCOPYBITS      = 0x0100;
                public const uint SWP_NOOWNERZORDER   = 0x0200;
                public const uint SWP_NOSENDCHANGING  = 0x0400;
                public const uint SWP_DRAWFRAME       = SWP_FRAMECHANGED;
                public const uint SWP_NOREPOSITION    = SWP_NOOWNERZORDER;
                public const int  HTCLIENT            = 1;
                public const int  HTCAPTION           = 2;
                public const int  IDC_ARROW           = 32512;
                public const int  IDC_IBEAM           = 32513;
                public const int  IDC_WAIT            = 32514;
                public const int  IDC_CROSS           = 32515;
                public const int  IDC_UPARROW         = 32516;
                public const int  IDC_SIZE            = 32640;
                public const int  IDC_ICON            = 32641;
                public const int  IDC_SIZENWSE        = 32642;
                public const int  IDC_SIZENESW        = 32643;
                public const int  IDC_SIZEWE          = 32644;
                public const int  IDC_SIZENS          = 32645;
                public const int  IDC_SIZEALL         = 32646;
                public const int  IDC_NO              = 32648;
                public const int  IDC_HAND            = 32649;
                public const int  IDC_APPSTARTING     = 32650;
                public const uint WS_OVERLAPPED       = 0x00000000;
                public const uint WS_POPUP            = 0x80000000;
                public const uint WS_CHILD            = 0x40000000;
                public const uint WS_MINIMIZE         = 0x20000000;
                public const uint WS_VISIBLE          = 0x10000000;
                public const uint WS_DISABLED         = 0x08000000;
                public const uint WS_CLIPSIBLINGS     = 0x04000000;
                public const uint WS_CLIPCHILDREN     = 0x02000000;
                public const uint WS_MAXIMIZE         = 0x01000000;
                public const uint WS_CAPTION          = 0x00C00000;
                public const uint WS_BORDER           = 0x00800000;
                public const uint WS_DLGFRAME         = 0x00400000;
                public const uint WS_VSCROLL          = 0x00200000;
                public const uint WS_HSCROLL          = 0x00100000;
                public const uint WS_SYSMENU          = 0x00080000;
                public const uint WS_THICKFRAME       = 0x00040000;
                public const uint WS_GROUP            = 0x00020000;
                public const uint WS_TABSTOP          = 0x00010000;
                public const uint BN_CLICKED          = 0;
                public const int  GWLP_WNDPROC        = -4;
                public const uint SS_OWNERDRAW        = 0x000B;
                public const uint CS_HREDRAW          = 0x0002;
                public const uint CS_VREDRAW          = 0x0001;
                public const uint WM_SETREDRAW        = 0x000B;
                public const uint WM_ERASEBKGND       = 0x0014;
                public const uint SRCCOPY             = 0x00CC0020;
            }
        }
    }
}

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
        public double FPS => WL.System.Tick.DeltaTimeToFPS(DeltaTime);
        
        /// <summary>
        /// Подходит для умножения (Если DeltaTime совпадает с целью, то равен 1)
        /// </summary>
        /// <param name="TargetDelta">Целевой DeltaTime</param>
        public double Delta(double TargetDelta){ return TargetDelta / DeltaTime; }
        
        /// <summary>
        /// Подходит для умножения (Если FPS совпадает с целью, то равен 1)
        /// </summary>
        /// <param name="TargetFPS">Целевой FPS</param>
        public double DeltaFPS(double TargetFPS){ return Delta(WL.System.Tick.FPSToDeltaTime(TargetFPS)); }

        /// <summary>
        /// Сколько тиков прошло (+ 1) (Есть только в Limit!)
        /// </summary>
        public int Tick;

        /// <summary>
        /// Сколько тиков прошло (+ Delta(...)) (Есть только в Limit!)
        /// </summary>
        public double DeltaTick;
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

    /// <summary>
    /// Тип операционной системы
    /// </summary>
    public enum OSType{
        Windows,
        Linux,
        OSX,
        FreeBSD,
        Unknown
    }
}