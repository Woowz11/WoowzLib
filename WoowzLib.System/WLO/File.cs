namespace WLO;

/// <summary>
/// Типы файла
/// </summary>
public enum FileType{
    /// <summary>
    /// Файл не найден
    /// </summary>
    Null,
    /// <summary>
    /// Файл
    /// </summary>
    File,
    /// <summary>
    /// Папка
    /// </summary>
    Folder
}

/// <summary>
/// Файл
/// </summary>
public class File{
    /// <summary>
    /// Ищет файл (или создаёт если StartContent не пуст)
    /// </summary>
    /// <param name="Path">Путь до файла</param>
    /// <param name="StartContent">Стартовое содержимое файла, если файл существует, НЕ ИЗМЕНИТ ЕГО СОДЕРЖИМОЕ!</param>
    public File(string Path, string? StartContent = null){
        try{
            Path = WL.String.Path.Normalize(Path);
            if(!WL.String.Path.IsCorrect(Path)){ throw new Exception("Указан неверный путь!"); }
            AbsolutePath = Path;
        }
        catch(Exception e){
            throw new Exception("Произошла ошибка при получении файла [\"" + Path + "\"]!\nСтартовое содержимое:\n" + WL.String.ToString(StartContent), e);
        }
    }
    
    /// <summary>
    /// Абсолютный путь до файла
    /// </summary>
    public readonly string AbsolutePath;

    /// <summary>
    /// Это файл?
    /// </summary>
    public bool IsFile => Type == FileType.File;
    
    /// <summary>
    /// Это папка?
    /// </summary>
    public bool IsFolder => Type == FileType.Folder;
    
    /// <summary>
    /// Это ничего?
    /// </summary>
    public bool IsNull => Type == FileType.Null;

    /// <summary>
    /// Это файл или папка? (Существует?)
    /// </summary>
    public bool Exist => !IsNull;

    /// <summary>
    /// Какого типа файл?
    /// </summary>
    public FileType Type{
        get{
            if(WL.String.IsEmpty(AbsolutePath)){ return FileType.Null; }
            if(WL.Explorer.File.IsFile(AbsolutePath)){ return FileType.File; }
            return WL.Explorer.Folder.IsFolder(AbsolutePath) ? FileType.Folder : FileType.Null;
        }
    }
    
    // ----------------------------------------------------------------------

}