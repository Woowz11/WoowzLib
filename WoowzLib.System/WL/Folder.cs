namespace WL;

public static partial class Explorer{
    public static class Folder{
        /// <summary>
        /// Проверяет, это папка по указанному пути?
        /// </summary>
        public static bool IsFolder(string Path){
            try{
                return !WL.String.Path.IsCorrect(Path) ? throw new Exception("Указан неверный путь!") : Directory.Exists(Path);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при проверке, папка ли путь [\"" + Path + "\"]!", e);
            }
        }
    }
}