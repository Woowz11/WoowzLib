using Microsoft.VisualBasic.FileIO;

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

        /// <summary>
        /// Проверяет, есть ли файл по указанному пути?
        /// </summary>
        public static bool Exist(string Path) => IsFile(Path);

        /// <summary>
        /// Получает файл
        /// </summary>
        /// <param name="Path">Путь</param>
        /// <returns>Файл, если не найден то возвращает null</returns>
        public static WLO.File? Get(string Path){
            try{
                WLO.File Result = new WLO.File(Path);
                return Result.IsFile ? Result : null;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении файла [\"" + Path + "\"]!", e);
            }
        }

        /// <summary>
        /// Создаёт файл (если уже существует, ничего не сделает)
        /// </summary>
        public static WLO.File? Create(string Path, string Content = ""){
            try{
                return IsFile(Path) ? null : new WLO.File(Path, Content);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при создании файла [\"" + Path + "\"]!\nСтартовое содержимое:\n" + Content, e);
            }
        }
        
        /// <summary>
        /// Получает файл или создаёт файл
        /// </summary>
        public static WLO.File GetOrCreate(string Path, string Content = ""){
            try{
                return IsFile(Path) ? new WLO.File(Path) : new WLO.File(Path, Content);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении или создании файла [\"" + Path + "\"]!\nСтартовое содержимое:\n" + Content, e);
            }
        }
        
        // ----------------------------------------------------------------------

        /// <summary>
        /// Удаляет файл
        /// </summary>
        public static void Delete(string Path, bool ToRecycleBin = false){
            try{
                if(!IsFile(Path)){ throw new Exception("Указан неверный путь!"); }
                
                FileSystem.DeleteFile(Path, UIOption.OnlyErrorDialogs, ToRecycleBin ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при удалении файла [\"" + Path + "\"]!\nВ корзину?: " + ToRecycleBin, e);
            }
        }
    }
}