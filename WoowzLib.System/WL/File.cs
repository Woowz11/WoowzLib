namespace WL;

public static partial class Explorer{
    public static class File{
        /// <summary>
        /// Проверяет, это файл по указанному пути?
        /// </summary>
        public static bool IsFile(string Path){
            try{
                return !WL.String.Path.IsCorrect(Path) ? throw new Exception("Указан неверный путь!") : global::System.IO.File.Exists(Path);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при проверке, файл ли путь [\"" + Path + "\"]!", e);
            }
        }
    }
}