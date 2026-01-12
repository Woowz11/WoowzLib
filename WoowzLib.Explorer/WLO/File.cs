using System.Text;

namespace WLO;

/// <summary>
/// Файл
/// </summary>
public class File{
    public const string Error_FileNotExist         = "Файл не найден!";
    public const string Error_FileAlreadyDestroyed = "Файл уже уничтожен!";
    public const string Error_FileAlreadyCreated   = "Файл уже создан!";

    /// <summary>
    /// Получение или создание пустого файла
    /// </summary>
    /// <param name="Path">Путь [<c>"../example/file.txt"</c>]</param>
    public File(string Path){
        if(string.IsNullOrWhiteSpace(Path)){ throw new Exception("Указан пустой путь для создания файла!"); }
        __Path = Path;

        if(!Exist){ Create(); }
    }

    /// <summary>
    /// Путь до файла [<c>"../example/file.txt"</c>]
    /// <br />(Заглавные и прописные буквы считаются одинаково, т.е <c>"test.png"</c> == <c>"TEst.pnG"</c>)
    /// </summary>
    public string Path => __Path;

    private string __Path;

    /// <summary>
    /// Путь до родительской папки [<c>"../example/"</c>]
    /// </summary>
    public string ParentPath => WL.Explorer.File.OnlyFolder(Path);
    
    /// <summary>
    /// Имя файла (с расширением) [<c>"file.txt"</c>]
    /// </summary>
    public string FullName => System.IO.Path.GetFileName(Path);

    /// <summary>
    /// Имя файла [<c>"file"</c>]
    /// </summary>
    public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

    /// <summary>
    /// Расширение файла [<c>"txt"</c>]
    /// </summary>
    public string Extension => System.IO.Path.GetExtension(Path)?.TrimStart('.') ?? "";

    /// <summary>
    /// Файл существует?
    /// </summary>
    public bool Exist => WL.Explorer.File.Exist(Path);

    /// <summary>
    /// Узнать размер файла в байтах
    /// </summary>
    public long Size => Exist ? new FileInfo(Path).Length : throw new Exception("Невозможно узнать размер файла [" + this + "], потому-что он не существует!");

    /// <summary>
    /// Последнее время изменения файла
    /// </summary>
    public DateTime LastModified => Exist ? System.IO.File.GetLastWriteTime(Path) : throw new Exception("Невозможно узнать дату последнего изменения у файла [" + this + "], потому-что он не существует!");

    /// <summary>
    /// Чтение содержимого файла, текстовое
    /// </summary>
    /// <param name="Encoding">Кодировка [<c>Encoding.UTF8</c>]</param>
    /// <returns>Текстовое содержимое файла</returns>
    public string ReadString(Encoding? Encoding = null){
        try{
            return Exist ? System.IO.File.ReadAllText(Path, Encoding ?? Encoding.UTF8) : throw new Exception(Error_FileNotExist);
        }
        catch(Exception e){
            throw new Exception("Не получилось прочитать файл [" + this + "]!\nТип: Текст\nКодировка: " + Encoding, e);
        }
    }

    /// <summary>
    /// Заменяет содержимое файла, на текст
    /// </summary>
    /// <param name="Content">Текст [<c>"Какой-то текст\nА это на новой строке типо..."</c>]</param>
    /// <param name="Encoding">Кодировка [<c>Encoding.UTF8</c>]</param>
    public File WriteString(string Content, Encoding? Encoding = null){
        try{
            System.IO.File.WriteAllText(Path, Content ?? "", Encoding ?? Encoding.UTF8);
        }
        catch(Exception e){
            throw new Exception("Произошла ошибка при записи в файл [" + this + "]!\nТип: Текст\nКодировка: " + Encoding + "\nСодержимое: \"" + Content + "\"", e);
        }

        return this;
    }

    /// <summary>
    /// Добавляет в содержимое файла, текст
    /// </summary>
    /// <param name="Content">Текст [<c>"\nДобавленный текст!"</c>]</param>
    /// <param name="Encoding">Кодировка [<c>Encoding.UTF8</c>]</param>
    public File AddString(string Content, Encoding? Encoding = null){
        try{
            System.IO.File.AppendAllText(Path, Content ?? "", Encoding ?? Encoding.UTF8);
        }
        catch(Exception e){
            throw new Exception("Произошла ошибка при добавлении в файл [" + this + "]!\nТип: Текст\nКодировка: " + Encoding + "\nСодержимое: \"" + Content + "\"", e);
        }

        return this;
    }

    /// <summary>
    /// Чтение содержимого файла, байтовое
    /// </summary>
    /// <returns>Байтовое содержимое файла</returns>
    public byte[] ReadByte(){
        try{
            return Exist ? System.IO.File.ReadAllBytes(Path) : throw new Exception(Error_FileNotExist);
        }catch(Exception e){
            throw new Exception("Не получилось прочитать файл [" + this + "]!\nТип: Байт", e);
        }
    }
    
    /// <summary>
    /// Заменяет содержимое файла, на байты
    /// </summary>
    /// <param name="Content">Байты</param>
    public File WriteByte(byte[] Content){
        try{
            System.IO.File.WriteAllBytes(Path, Content ?? []);   
        }catch(Exception e){
            throw new Exception("Произошла ошибка при записи в файл [" + this + "]!\nТип: Байт\nСодержимое: byte[" + Content.Length + "]", e);
        }

        return this;
    }

    /// <summary>
    /// Очищает содержимое файла
    /// </summary>
    public File Clear(){
        try{
            using(FileStream FS = new FileStream(Path, FileMode.Truncate, FileAccess.Write)){}
        }catch(Exception e){
            throw new Exception("Произошла ошибка при очистке файла [" + this + "]!");
        }

        return this;
    }

    /// <summary>
    /// Перемещает файл в новое место
    /// </summary>
    /// <param name="NewPath">Новый путь для файла [<c>"newfolder/file.txt"</c>]</param>
    /// <param name="Overwrite">Перезаписать существующий файл если есть, иначе ошибка</param>
    public File Move(string NewPath, bool Overwrite = false){
        try{
            if(string.IsNullOrWhiteSpace(NewPath)){ throw new Exception("Новый путь не может быть пустым!"); }
            if(!Exist){ throw new Exception(Error_FileNotExist); }

            WL.Explorer.Folder.Create(WL.Explorer.File.OnlyFolder(NewPath));

            if(WL.Explorer.File.Exist(NewPath)){
                if(Overwrite){
                    WL.Explorer.File.Destroy(NewPath);
                }else{
                    throw new Exception("Файл уже существует по новому пути!");
                }
            }
            
            System.IO.File.Move(Path, NewPath);
            __Path = NewPath;
        }catch(Exception e){
            throw new Exception("Не получилось переместить файл [" + this + "]!\nНовый путь: \"" + NewPath + "\"\nЗаменить: " + Overwrite, e);
        }

        return this;
    }
    
    /// <summary>
    /// Клонирует файл в новое место
    /// </summary>
    /// <param name="NewPath">Новый путь для файла [<c>"newfolder/file.txt"</c>]</param>
    /// <param name="Overwrite">Перезаписать существующий файл если есть, иначе ошибка</param>
    public File Clone(string NewPath, bool Overwrite = false){
        try{
            if(string.IsNullOrWhiteSpace(NewPath)){ throw new Exception("Новый путь не может быть пустым!"); }
            if(!Exist){ throw new Exception(Error_FileNotExist); }

            WL.Explorer.Folder.Create(WL.Explorer.File.OnlyFolder(NewPath));

            if(WL.Explorer.File.Exist(NewPath)){
                if(Overwrite){
                    WL.Explorer.File.Destroy(NewPath);
                }else{
                    throw new Exception("Файл уже существует по новому пути!");
                }
            }

            return new File(NewPath).WriteByte(ReadByte());
        }catch(Exception e){
            throw new Exception("Не получилось клонировать файл [" + this + "]!\nНовый путь: \"" + NewPath + "\"\nЗаменить: " + Overwrite, e);
        }
    }

    /// <summary>
    /// Получает FileStream для работы с файлом
    /// </summary>
    /// <param name="Mode">Режим открытия файла</param>
    /// <param name="Access">Доступ к файлу</param>
    /// <param name="Share">Режим совместного доступа</param>
    /// <returns>FileStream для чтения/записи</returns>
    public FileStream Stream(FileMode Mode = FileMode.OpenOrCreate, FileAccess Access = FileAccess.ReadWrite, FileShare Share = FileShare.None){
        try{
            if(!Exist){ throw new Exception(Error_FileNotExist); }
            WL.Explorer.Folder.Create(ParentPath);
            return new FileStream(Path, Mode, Access, Share);
        }catch(Exception e){
            throw new Exception("Не удалось открыть FileStream для файла [" + this + "]!\nРежим открытия: " + Mode + "\nДоступ: " + Access + "\nРежим совместного доступа: " + Share, e);
        }
    }
    
    /// <summary>
    /// Создаёт файл, если он не существует (Вызывается при создании <c>new File(...)</c>)
    /// </summary>
    public File Create(){
        try{
            if(Exist){ throw new Exception(Error_FileAlreadyCreated); }

            WL.Explorer.Folder.Create(ParentPath);

            using(FileStream FS = System.IO.File.Create(Path)){}
        }catch(Exception e){
            throw new Exception("Не получилось создать файл [" + this + "]!");
        }

        return this;
    }
    
    /// <summary>
    /// Уничтожает файл
    /// </summary>
    public void Destroy(){
        try{
            WL.Explorer.File.Destroy(Path);
        }catch(Exception e){
            throw new Exception("Не получилось уничтожить файл [" + this + "]!", e);
        }
    }

    #region Override

        public override string ToString(){
            return "File(\"" + Path + "\", " + (Exist ? Size : "Не существует") + ")";
        }

        public override bool Equals(object? Obj){
            if(Obj is File F){
                return string.Equals(Path, F.Path, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode() => Path.GetHashCode(StringComparison.OrdinalIgnoreCase);

        public static bool operator ==(File? A, File? B){
            if(ReferenceEquals(A, B)){ return true; }
            if(A is null || B is null){ return false; }
            return A.Equals(B);
        }

        public static bool operator !=(File? A, File? B) => !(A == B);

    #endregion
}