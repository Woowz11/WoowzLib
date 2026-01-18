using System.Runtime.InteropServices;
using File = WLO.File;

namespace WL{
    [WLModule(-2500, 3)]
    public static class Native{
        public const string Error_DLLNotExist      = "Не найден DLL!";
        public const string Error_FunctionNotFound = "Функция не найдена!";

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
                return !DLL.Exist ? throw new Exception(Error_DLLNotExist) : LoadSystem(DLL.Path);
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
                UnloadSystem(DLL.Path);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при разгрузке DLL [" + DLL + "]!", e);
            }
        }
        
        /// <summary>
        /// Загрузка системного DLL файла
        /// </summary>
        /// <param name="DLLName">Название системного DLL файла</param>
        /// <returns>Ссылка на загруженный DLL файл</returns>
        public static IntPtr LoadSystem(string DLLName){
            try{
                if(string.IsNullOrWhiteSpace(DLLName)){ throw new Exception("Имя DLL файла пустое!"); }
                if(LoadedDLL.TryGetValue(DLLName, out IntPtr Handle) && Handle != IntPtr.Zero){
                    throw new Exception("Этот DLL уже был загружен! Handle: " + Handle);
                }

                Handle = WL.Windows.Kernel.LoadLibrary(DLLName);
                if(Handle == IntPtr.Zero){
                    throw new Exception("Не получилось загрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error());
                }

                LoadedDLL[DLLName] = Handle;
                return Handle;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке системного DLL [" + DLLName + "]!", e);
            }
        }
        
        /// <summary>
        /// Разгрузка системного DLL файла
        /// </summary>
        /// <param name="DLLName">Название системного DLL файла</param>
        public static void UnloadSystem(string DLLName){
            try{
                if(!LoadedDLL.TryGetValue(DLLName, out IntPtr Handle) || Handle == IntPtr.Zero){
                    throw new Exception(Error_DLLNotExist);
                }
                
                bool Result = WL.Windows.Kernel.FreeLibrary(Handle);
                if(Result){
                    LoadedDLL.Remove(DLLName);
                }else{
                    throw new Exception("Не получилось выгрузить DLL внутри kernel32! Ошибка: " + Marshal.GetLastWin32Error());
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при разгрузке системного DLL [" + DLLName + "]!", e);
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
                return FunctionSystem(DLL.Path, Name);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке функции из DLL [" + DLL + "]!\nФункция: " + Name);
            }
        }
        
        /// <summary>
        /// Получает ссылку на функцию из системного DLL
        /// </summary>
        /// <param name="DLLName">Название системного DLL файла</param>
        /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
        /// <returns>Ссылка на функцию</returns>
        public static IntPtr FunctionSystem(string DLLName, string Name){
            try{
                if(!LoadedDLL.TryGetValue(DLLName, out IntPtr Handle)){
                    throw new Exception(Error_DLLNotExist);
                }

                return FunctionSystem(Handle, Name);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке функции из системного DLL [" + DLLName + "]!\nФункция: " + Name);
            }
        }
        
        /// <summary>
        /// Получает ссылку на функцию из системного DLL (по ссылке)
        /// </summary>
        /// <param name="DLL">Ссылка на DLL</param>
        /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
        /// <returns>Ссылка на функцию</returns>
        public static IntPtr FunctionSystem(IntPtr DLL, string Name){
            try{
                IntPtr Proc = WL.Windows.Kernel.GetProcAddress(DLL, Name);
                return Proc == IntPtr.Zero ? throw new Exception(Error_FunctionNotFound) : Proc;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при загрузке функции из системного DLL (IntPtr) [" + DLL.ToInt64() + "]!\nФункция: " + Name);
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
        
        /// <summary>
        /// Получает функцию из ссылки на DLL и возвращает её в виде C# функции
        /// </summary>
        /// <param name="Name">Функция из DLL [<c>"glfwCreateWindow"</c>]</param>
        /// <param name="DLL">Ссылка на DLL</param>
        /// <typeparam name="D">Тип функции (точно совпадает с её параметрами и возвращаемым значением)</typeparam>
        /// <returns>Функция которую можно вызвать как C# функцию</returns>
        public static D DelegateFunction<D>(string Name, IntPtr DLL) where D : Delegate{
            return Marshal.GetDelegateForFunctionPointer<D>(FunctionSystem(DLL, Name));
        }
    }
}