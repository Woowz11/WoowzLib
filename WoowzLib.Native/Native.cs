using System.Runtime.InteropServices;
using File = WLO.File;

namespace WL{
    [WoowzLibModule(10)]
    public static class Native{
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

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
                if(!DLL.Exist){ throw new Exception("DLL файл не найден!"); }
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
                    throw new Exception("Не найден DLL!");
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
    }
}