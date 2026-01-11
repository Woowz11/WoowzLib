using System.Reflection;

namespace WL{
    public static class Explorer{
        /// <summary>
        /// Для работы с файлами
        /// </summary>
        public static class File{
            /// <summary>
            /// Проверяет, существует ли файл по указанному пути
            /// </summary>
            /// <param name="Path">Путь [<c>"test/file.json"</c>]</param>
            /// <returns>Файл существует?</returns>
            public static bool Exist(string Path) => System.IO.File.Exists(Path);
            
            /// <summary>
            /// Удаляет файл по указанному пути
            /// </summary>
            /// <param name="Path">Путь [<c>"test/file.json"</c>]</param>
            public static void Delete(string Path){
                try{
                    if(!Exist(Path)){ throw new Exception(WLO.File.Error_FileNotExist); }
                    System.IO.File.Delete(Path);
                }catch(Exception e){
                    throw new Exception("Не получилось удалить файл по пути [" + Path + "]!");
                }
            }

            /// <summary>
            /// Превращает указанный путь в путь без файла
            /// </summary>
            /// <param name="Path">Путь [<c>folder/folder2/file.txt</c>]</param>
            /// <returns>[<c>"folder/folder2/"</c>]</returns>
            public static string OnlyFolder(string Path){
                if(string.IsNullOrWhiteSpace(Path)){ return ""; }
                return System.IO.Path.GetDirectoryName(Path) + "/";
            }
        }
        
        /// <summary>
        /// Для работы с папками
        /// </summary>
        public static class Folder{
            /// <summary>
            /// Проверяет, существует ли папка по указанному пути
            /// </summary>
            /// <param name="Path">Путь [<c>test/</c>]</param>
            /// <returns>Папка существует?</returns>
            public static bool Exist(string Path) => Directory.Exists(Path);
            
            /// <summary>
            /// Создаёт папки по пути
            /// </summary>
            /// <param name="Path">Путь [<c>"folder1/folder2/folder3"</c>]</param>
            public static void Create(string Path){
                try{
                    if(!string.IsNullOrEmpty(Path) && !Exist(Path)){
                        Directory.CreateDirectory(Path);
                    }
                }catch(Exception e){
                    throw new Exception("Не получилось создать папки по пути [" + Path + "]!");
                }
            }
            
            /// <summary>
            /// Удаляет папку по указанному пути (с файлами и папками внутри)
            /// </summary>
            /// <param name="Path">Путь [<c>"test/folder/"</c>]</param>
            public static void Delete(string Path){
                try{
                    if(!Exist(Path)){ throw new Exception("Папка не найдена!"); }
                    System.IO.Directory.Delete(Path, true);
                }catch(Exception e){
                    throw new Exception("Не получилось удалить папку по пути [" + Path + "]!");
                }
            }
        }
        
        /// <summary>
        /// Для создания временных файлов
        /// </summary>
        public static class Temp{
            public  static readonly string         TempFolder;
            private static readonly List<WLO.File> TempFiles = [];
            
            static Temp(){
                TempFolder = Path.Combine(
                    Path.GetTempPath(),
                    "WoowzLib_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss")
                );

                WL.Explorer.Folder.Create(TempFolder);

                AppDomain.CurrentDomain.ProcessExit        += (_, _) => Destroy();
                AppDomain.CurrentDomain.UnhandledException += (_, _) => Destroy();
            }

            private static void Destroy(){
                try{
                    foreach(WLO.File TempFile in TempFiles){
                        try{ TempFile.Delete(); }catch{ /**/ }
                    }
                    
                    if(WL.Explorer.Folder.Exist(TempFolder)){ WL.Explorer.Folder.Delete(TempFolder); }
                }catch{ /**/ }
            }

            /// <summary>
            /// Создание временного файла
            /// </summary>
            /// <param name="Path">Путь до файла (с поддержкой папок) [<c>"test/file.txt"</c>]</param>
            /// <returns>Файл</returns>
            public static WLO.File Create(string Path){
                WLO.File TempFile = new WLO.File(System.IO.Path.Combine(TempFolder, Path));

                TempFiles.Add(TempFile);
                
                return TempFile;
            }
        }
        
        /// <summary>
        /// Для работы с ресурсами проекта
        /// </summary>
        public static class Resources{
            /// <summary>
            /// Загружает ресурс из проекта
            /// </summary>
            /// <param name="ID">ID ресурса [<c>"WoowzLib.GLFW.Native.win-x64.glfw3.dll"</c>]</param>
            /// <param name="Assembly">Сборка, где искать ресурс (если null, значит в текущей)</param>
            /// <returns>Ресурс (сохранённый во временной папке)</returns>
            public static WLO.File Load(string ID, Assembly? Assembly = null){
                Assembly Assembly__ = Assembly ?? System.Reflection.Assembly.GetExecutingAssembly();

                string? ProjectName = Assembly__.GetName().Name;
                
                try{
                    using Stream? Stream = Assembly__.GetManifestResourceStream(ID);
                    if(Stream == null){ throw new Exception("Ресурс не найден в сборке!"); }
                    
                    string ResourceName = ID.Split('.').Last();
                    if(ID.Contains('.')){
                        string[] Parts__ = ID.Split('.');
                        ResourceName = Parts__[^2] + "." + Parts__[^1];
                    }

                    WLO.File File = WL.Explorer.Temp.Create("WL\\Explorer.Resources\\" + (ProjectName ?? "Unknown") + "\\" + ResourceName);

                    using FileStream FS = new FileStream(File.Path, FileMode.Create, FileAccess.Write, FileShare.None);
                    Stream.CopyTo(FS);

                    return File;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при загрузке ресурса [" + ID + "]!\nСборка: " + (ProjectName ?? "Неизвестная сборка"), e);
                }
            }
        }
    }
}