namespace WL;

public static partial class String{
    public static class Path{
        static Path(){
            InvalidPathChars = System.IO.Path.GetInvalidPathChars();
            InvalidNameChars = System.IO.Path.GetInvalidFileNameChars();
        }

        /// <summary>
        /// Запрещённые символы в пути
        /// </summary>
        public static readonly char[] InvalidPathChars;

        /// <summary>
        /// Запрещённые символы в названии файла
        /// </summary>
        public static readonly char[] InvalidNameChars;
        
        /// <summary>
        /// Проверяет, указанная строка путь? (может быть пустой)
        /// </summary>
        public static bool IsCorrect(string Path){
            if(WL.String.IsEmpty(Path)){ return true; }

            if(InvalidPathChars.Any(c => WL.String.Contains(Path, c))){ return false; }

            // Проверка на диск
            Path = WL.String.Path.Disk(Path, out char? Disk, out bool DiskError);
            if(DiskError){ return false; }
            
            string[] Parts = WL.String.Path.Split(Path);

            foreach(string File in Parts){
                if(WL.String.IsWhiteSpace(File)){ return false; }

                if(InvalidNameChars.Any(c => WL.String.Contains(File, c))){ return false; }
            }
            
            return true;
        }

        /// <summary>
        /// Нормализует путь (превращает в один стиль)
        /// </summary>
        public static string Normalize(string Path){
            Path = WL.String.Trim(Path);
            if(WL.String.IsEmpty(Path)){ return WL.String.Empty; }

            string Result = WL.String.Replace(Path, '\\', '/');

            if(Result.Length > 1 && WL.String.AtRight(Result, '/')){
                Result = Result[..^1];
            }
            
            return Result;
        }
        
        // ----------------------------------------------------------------------

        /// <summary>
        /// Разделяет путь по символам '/' или '\\', удаляет пустоту в конце если есть
        /// </summary>
        public static string[] Split(string Path){
            if(WL.String.IsWhiteSpace(Path)){ return []; }

            string[] Parts = WL.String.Split(Path, '/', '\\');

            if(Parts.Length > 0 && WL.String.IsWhiteSpace(Parts[^1])){
                Array.Resize(ref Parts, Parts.Length - 1);
            }

            return Parts;
        }

        /// <summary>
        /// Добавляет путь в конец пути
        /// </summary>
        public static string Add(string Path, string Added){
            if(WL.String.IsWhiteSpace(Path)){ return Normalize(Added); }

            Path  = Normalize(Path );
            Added = Normalize(Added);

            return Path + "/" + Added;
        }
        
        // ----------------------------------------------------------------------

        /// <summary>
        /// Получает диск по указанному пути
        /// </summary>
        /// <param name="Path">Путь</param>
        /// <param name="Disk">Диск</param>
        /// <param name="Error">Неверно указан диск</param>
        /// <returns>Путь без диска</returns>
        public static string Disk(string Path, out char? Disk, out bool Error){
            Disk = null;
            Error = false;
            
            if(Path.Length > 2 && Path[1] == ':'){
                if(!char.IsLetter(Path[0]) || Path[2] != '/'){ Error = true; return Path; }

                Disk = Path[0];
                Path = Path[3..];
            }

            return Path;
        }
    }
}