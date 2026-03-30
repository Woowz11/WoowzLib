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
    /// <param name="Path">Путь до файла</param>
    /// <param name="StartContent">Стартовое содержимое файла, если файл существует, НЕ ИЗМЕНИТ ЕГО СОДЕРЖИМОЕ!</param>
    /// <param name="ThatFolder">Это 100% должна быть папка?</param>
    /// </summary>
    public File(string Path, string? StartContent = null, bool ThatFolder = false){
        try{
            Path = WL.String.Path.Normalize(Path);
            if(!WL.String.Path.IsCorrect(Path)){ throw new Exception("Указан неверный путь!"); }
            AbsolutePath = Path;

            __ThatFolder = ThatFolder;

            if(StartContent != null && IsNull){
                if(__ThatFolder){
                    CreateFolder();
                }else{
                    CreateFile(StartContent);   
                }
            }
        }
        catch(Exception e){
            throw new Exception("Произошла ошибка при получении файла [" + this + "]!\nПуть: \"" + Path + "\"\nСтартовое содержимое:\n" + WL.String.ToString(StartContent), e);
        }
    }
    
    /// <summary>
    /// Абсолютный путь до файла
    /// </summary>
    public readonly string AbsolutePath;

    private readonly bool __ThatFolder;

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
            if(!__ThatFolder && WL.Explorer.File.IsFile(AbsolutePath)){ return FileType.File; }
            return WL.Explorer.Folder.IsFolder(AbsolutePath) ? FileType.Folder : FileType.Null;
        }
    }
    
    /// <summary>
    /// Текстовое содержимое файла (не используйте +=! лучше AppendText, или File += "Text"!)
    /// </summary>
    public string Content{
        get{
            try{
                return !IsFile ? throw new Exception("Файл не найден!") : System.IO.File.ReadAllText(AbsolutePath);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при чтении текстового содержимого файла [" + this + "]!", e);
            }
        }
        set{
            try{
                if(!IsFile){ throw new Exception("Файл не найден!"); }
                System.IO.File.WriteAllText(AbsolutePath, value);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке текстового содержимого файла [" + this + "]!\nНовое содержимое:\n" + value, e);
            }
        }
    }
    
    /// <summary>
    /// Бинарное содержимое файла
    /// </summary>
    public byte[] Bytes{
        get{
            try{
                return !IsFile ? throw new Exception("Файл не найден!") : System.IO.File.ReadAllBytes(AbsolutePath);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при чтении бинарного содержимого файла [" + this + "]!", e);
            }
        }
        set{
            try{
                if(!IsFile){ throw new Exception("Файл не найден!"); }
                System.IO.File.WriteAllBytes(AbsolutePath, value);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке бинарного содержимого файла [" + this + "]!\nНовое содержимое:\n" + value, e);
            }
        }
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Создаёт файл
    /// <param name="StartContent">Стартовое содержимое</param>
    /// </summary>
    public File CreateFile(string StartContent = ""){
        try{
            if(IsFile){ throw new Exception("Файл уже созданный!"); }
            
            System.IO.File.WriteAllText(AbsolutePath, StartContent);

            return this;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании файла [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Создаёт папку
    /// </summary>
    public File CreateFolder(){
        try{
            if(IsFolder){ throw new Exception("Папка уже создана!"); }

            Directory.CreateDirectory(AbsolutePath);

            return this;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании папки [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Добавляет текст в конец содержимого файла
    /// </summary>
    public File AppendText(string Text){
        try{
            if(!IsFile){ throw new Exception("Файл не найден!"); }
            if(WL.String.IsEmpty(Text)){ return this; }
            System.IO.File.AppendAllText(AbsolutePath, Text);

            return this;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении текста в текстовое содержимое файла [" + this + "]!\nДобавляемое:\n" + Text, e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    public static File operator +(File File, string Text){
        File.AppendText(Text);
        return File;
    }
}