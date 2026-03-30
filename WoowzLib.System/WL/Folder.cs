using Microsoft.VisualBasic.FileIO;

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
        /// Проверяет, есть ли папка по указанному пути?
        /// </summary>
        public static bool Exist(string Path) => IsFolder(Path);
        
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
        
        // ----------------------------------------------------------------------

        /// <summary>
        /// Все файлы внутри папки
        /// </summary>
        public static string[] Files(string Path){
            try{
                return !IsFolder(Path) ? throw new Exception("Указан неверный путь!") : Directory.GetFiles(Path);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении файлов внутри папки [\"" + Path + "\"]!", e);
            }
        }

        /// <summary>
        /// Все папки внутри папки
        /// </summary>
        public static string[] Folders(string Path){
            try{
                return !IsFolder(Path) ? throw new Exception("Указан неверный путь!") : Directory.GetDirectories(Path);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении папок внутри папки [\"" + Path + "\"]!", e);
            }
        }
        
        // ----------------------------------------------------------------------

        /// <summary>
        /// Удаляет папку
        /// </summary>
        public static void Delete(string Path, bool ToRecycleBin = false){
            try{
                if(!IsFolder(Path)){ throw new Exception("Указан неверный путь!"); }
                
                FileSystem.DeleteDirectory(Path, UIOption.OnlyErrorDialogs, ToRecycleBin ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при удалении папки [\"" + Path + "\"]!\nВ корзину?: " + ToRecycleBin, e);
            }
        }
        
        /// <summary>
        /// Удаляет всё содержимое папки
        /// </summary>
        public static void Clear(string Path, bool ToRecycleBin = false){
            try{
                if(!IsFolder(Path)){ throw new Exception("Указан неверный путь!"); }

                foreach(string File   in Files  (Path)){ WL.Explorer.File  .Delete(File  , ToRecycleBin); }
                foreach(string Folder in Folders(Path)){ WL.Explorer.Folder.Delete(Folder, ToRecycleBin); }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при очистке содержимого папки [\"" + Path + "\"]!\nВ корзину?: " + ToRecycleBin, e);
            }
        }
    }
}