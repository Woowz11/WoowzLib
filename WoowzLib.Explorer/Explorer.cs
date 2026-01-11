namespace WL{
    public static class Explorer{
        public static class Folder{
            
        }
        
        public static class File{
            
        }
        
        public static class Temp{
            public static readonly  string           TempFolder;
            private static readonly List<FileStream> OpenFS = [];
            
            static Temp(){
                TempFolder = Path.Combine(
                    Path.GetTempPath(),
                    "WoowzLib_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss")
                );

                Directory.CreateDirectory(TempFolder);

                AppDomain.CurrentDomain.ProcessExit        += (_, _) => Destroy();
                AppDomain.CurrentDomain.UnhandledException += (_, _) => Destroy();
            }

            private static void Destroy(){
                try{
                    foreach(FileStream FS in OpenFS){
                        try{ FS.Dispose(); }catch{ /**/ }
                    }
                    
                    if(Directory.Exists(TempFolder)){ Directory.Delete(TempFolder, true); }
                }catch{ /**/ }
            }

            /// <summary>
            /// Создание временного файла
            /// </summary>
            /// <param name="Path">Путь до файла (с поддержкой папок (WIP)) [<c>test/file.txt</c>]</param>
            /// <returns>Файл</returns>
            public static FileStream Create(string Path){
                Path = System.IO.Path.Combine(TempFolder, Path);

                FileStream FS = new FileStream(
                    Path,
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.None
                );

                OpenFS.Add(FS);
                
                return FS;
            }
        }
    }
}