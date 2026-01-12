using System.Runtime.InteropServices;
using File = WLO.File;

namespace WL{
    [WLModule(10)]
    public static class Native{
        public const string Error_DLLNotExist = "Не найден DLL!";
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        private static readonly Dictionary<string, IntPtr> LoadedDLL = new Dictionary<string, IntPtr>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Загружен ли указанный DLL?
        /// </summary>
        /// <param name="DLL">Указанный DLL</param>
        /// <returns>Загружен?</returns>
        public static bool Loaded(File DLL){
            return LoadedDLL.ContainsKey(DLL.Path) && LoadedDLL[DLL.Path] != IntPtr.Zero;
        }
        
        /// <summary>
        /// Загрузка DLL файла
        /// </summary>
        /// <param name="DLL">DLL файл</param>
        /// <returns>Ссылка на загруженный DLL файл</returns>
        public static IntPtr Load(File DLL){
            try{
                if(!DLL.Exist){ throw new Exception(Error_DLLNotExist); }
                if(LoadedDLL.TryGetValue(DLL.Path, out IntPtr Handle) && Handle != IntPtr.Zero){
                    throw new Exception("Этот DLL уже был загружен! Handle: " + Handle);
                }

                Handle = LoadLibrary(DLL.Path);
                if(Handle == IntPtr.Zero){
                    throw new Exception("Не получилось загрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error());
                }

                LoadedDLL[DLL.Path] = Handle;
                return Handle;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке DLL [" + DLL + "]!", e);
            }
        }

        /// <summary>
        /// Разгрузка DLL файла
        /// </summary>
        /// <param name="DLL">DLL файл</param>
        public static void Unload(File DLL){
            try{
                if(!LoadedDLL.TryGetValue(DLL.Path, out IntPtr Handle) || Handle == IntPtr.Zero){
                    throw new Exception(Error_DLLNotExist);
                }
                
                bool Result = FreeLibrary(Handle);
                if(Result){
                    LoadedDLL.Remove(DLL.Path);
                }else{
                    throw new Exception("Не получилось выгрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error());
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при разгрузке DLL [" + DLL + "]!", e);
            }
        }

        /// <summary>
        /// Получает ссылку на функцию из DLL
        /// </summary>
        /// <param name="DLL">Указанный DLL</param>
        /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
        /// <returns>Ссылка на функцию</returns>
        public static IntPtr Function(File DLL, string Name){
            try{
                if(!LoadedDLL.TryGetValue(DLL.Path, out IntPtr Handle)){
                    throw new Exception(Error_DLLNotExist);
                }

                IntPtr Proc = GetProcAddress(Handle, Name);
                return Proc == IntPtr.Zero ? throw new Exception("Функция не найдена!") : Proc;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке функции из DLL [" + DLL + "]!\nФункция: " + Name);
            }
        }

        /// <summary>
        /// Получает функцию из DLL и возвращает её в виде C# функции
        /// </summary>
        /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
        /// <param name="DLL">Указанный DLL</param>
        /// <typeparam name="D">Тип функции (точно совпадает с её параметрами и возвращаемым значением)</typeparam>
        /// <returns>Функция которую можно вызвать как C# функцию</returns>
        public static D DelegateFunction<D>(string Name, File DLL) where D : Delegate{
            return Marshal.GetDelegateForFunctionPointer<D>(Function(DLL, Name));
        }
    }
}