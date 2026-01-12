using System.Reflection;

namespace WL{
    [WLModule(5)]
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
            /// Уничтожает файл по указанному пути
            /// </summary>
            /// <param name="Path">Путь [<c>"test/file.json"</c>]</param>
            public static void Destroy(string Path){
                try{
                    if(!Exist(Path)){ throw new Exception(global::WLO.File.Error_FileAlreadyDestroyed); }
                    System.IO.File.Delete(Path);
                }catch(Exception e){
                    throw new Exception("Не получилось уничтожить файл по пути [" + Path + "]!");
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
            /// Уничтожает папку по указанному пути (с файлами и папками внутри)
            /// </summary>
            /// <param name="Path">Путь [<c>"test/folder/"</c>]</param>
            public static void Destroy(string Path){
                try{
                    if(!Exist(Path)){ throw new Exception("Папка не найдена!"); }
                    System.IO.Directory.Delete(Path, true);
                }catch(Exception e){
                    throw new Exception("Не получилось уничтожить папку по пути [" + Path + "]!");
                }
            }

            /// <summary>
            /// Уничтожает содержимое папки (с файлами и папками внутри)
            /// </summary>
            /// <param name="Path">Путь [<c>"test/folder/"</c>]</param>
            public static void Clear(string Path){
                try{
                    if(!Exist(Path)){ throw new Exception("Папка не найдена!"); }

                    foreach(string File in Directory.GetFiles(Path)){
                        System.IO.File.SetAttributes(File, FileAttributes.Normal);
                        System.IO.File.Delete(File);
                    }

                    foreach(string Folder in Directory.GetDirectories(Path)){
                        Directory.Delete(Folder, true);
                    }
                }catch(Exception e){
                    throw new Exception("Не получилось уничтожить файлы внутри папки по пути [" + Path + "]!");
                }
            }
        }
        
        /// <summary>
        /// Для создания временных файлов
        /// </summary>
        public static class Temp{
            public  static readonly string                 TempFolder;
            private static readonly List<global::WLO.File> TempFiles = [];
            
            static Temp(){
                TempFolder = Path.Combine(
                    Path.GetTempPath(),
                    "WoowzLib_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss")
                );

                WL.Explorer.Folder.Create(TempFolder);

                WL.WoowzLib.OnStop += __Destroy;
            }

            private static void __Destroy(){
                try{
                    Exception? e__ = null;
                    
                    foreach(global::WLO.File TempFile in TempFiles){
                        try{ TempFile.Destroy(); }catch(Exception e){ e__ = e; }
                    }

                    if(WL.Explorer.Folder.Exist(TempFolder)){ WL.Explorer.Folder.Destroy(TempFolder); }

                    if(e__ != null){ throw new Exception("Не получилось уничтожить все файлы!", e__); }
                }
                catch(Exception e){ throw new Exception("Произошла ошибка при очистке временных файлов!", e); }
            }

            /// <summary>
            /// Создание временного файла
            /// </summary>
            /// <param name="Path">Путь до файла (с поддержкой папок) [<c>"test/file.txt"</c>]</param>
            /// <returns>Файл</returns>
            public static global::WLO.File Create(string Path){
                global::WLO.File TempFile = new global::WLO.File(System.IO.Path.Combine(TempFolder, Path));

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
            public static global::WLO.File Load(string ID, Assembly? Assembly = null){
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

                    global::WLO.File File = WL.Explorer.Temp.Create("WL\\Explorer.Resources\\" + (ProjectName ?? "Unknown") + "\\" + ResourceName);

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