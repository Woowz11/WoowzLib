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
        
        /// <summary>
        /// Получает папку
        /// </summary>
        /// <param name="Path">Путь</param>
        /// <returns>Папка, если не найдена то возвращает null</returns>
        public static WLO.File? Get(string Path){
            try{
                WLO.File Result = new WLO.File(Path, null, true);
                return Result.IsFolder ? Result : null;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении папки [\"" + Path + "\"]!", e);
            }
        }
        
        /// <summary>
        /// Создаёт папку (если уже существует, ничего не сделает)
        /// </summary>
        public static WLO.File? Create(string Path, string Content = ""){
            try{
                return IsFolder(Path) ? null : new WLO.File(Path, "", true);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при создании папки [\"" + Path + "\"]!\nСтартовое содержимое:\n" + Content, e);
            }
        }
        
        /// <summary>
        /// Получает папку или создаёт папку
        /// </summary>
        public static WLO.File GetOrCreate(string Path){
            try{
                return IsFolder(Path) ? new WLO.File(Path, null, true) : new WLO.File(Path, "", true);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении или создании папки [\"" + Path + "\"]!", e);
            }
        }
    }
}