using WLO.Attribute;

namespace WL;

public static partial class String{
    [WoowzLibHint(Information.NeedRemake)]
    public static class Path{
        /// <summary>
        /// Объединяет пути
        /// </summary>
        /// <param name="Parts">Пути</param>
        /// <returns>Объединённый путь</returns>
        public static string Combine(params string[] Parts){
            try{
                if(Parts.Length == 0){ throw new Exception("Не указаны пути!"); }

                return Normalize(System.IO.Path.Combine(Parts));
            }catch(Exception e){
                throw new Exception("Произошла ошибка при объединении путей [" + Parts + "]!", e);
            }
        }

        /// <summary>
        /// Нормализует путь (Убирает .., и меняет сплеши)
        /// </summary>
        public static string Normalize(string Path){
            try{
                if(string.IsNullOrWhiteSpace(Path)){ throw new Exception("Путь пустой!"); }

                string FullPath = System.IO.Path.GetFullPath(Path);

                if(FullPath.Length > 3 && FullPath.EndsWith(System.IO.Path.DirectorySeparatorChar)){
                    FullPath = FullPath.TrimEnd(System.IO.Path.DirectorySeparatorChar);
                }

                return FullPath;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при нормализации пути [\"" + Path + "\"]!", e);
            }
        }

        /// <summary>
        /// Возвращает последнее имя в пути
        /// </summary>
        public static string LastName(string Path) => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// Возвращает пред-последнее имя в пути
        /// </summary>
        public static string? ParentName(string Path) => System.IO.Path.GetDirectoryName(Path);

        /// <summary>
        /// Возвращает расширение в пути
        /// </summary>
        public static string Extension(string Path) => System.IO.Path.GetExtension(Path);

        /// <summary>
        /// Меняет расширение в пути
        /// </summary>
        /// <param name="Path">Путь</param>
        /// <param name="NewExtension">Новое расширение</param>
        public static string ChangeExtension(string Path, string NewExtension) => System.IO.Path.ChangeExtension(Path, NewExtension);
    }
}