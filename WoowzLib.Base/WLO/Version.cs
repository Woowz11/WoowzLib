using System.Reflection;

namespace WLO;

public readonly struct Version{
    /// <summary>
    /// Загрузка версии на прямую, используя числа
    /// </summary>
    public Version(int Major, int Minor = 0, int Patch = 0, int Build = 0){ this.Major = Major; this.Minor = Minor; this.Patch = Patch; this.Build = Build; }
    
    /// <summary>
    /// Загрузка версии на прямую, используя числа
    /// </summary>
    /// <param name="Version">Версия (Major, Minor, Patch, Build)</param>
    public Version((int Major, int Minor, int Patch, int Build) Version) : this(Version.Major, Version.Minor, Version.Patch, Version.Build){}
    
    /// <summary>
    /// Загрузка версии по строке
    /// </summary>
    /// <param name="Version">Строка формата "0.0.0.0", или "612.27.3", и т.д</param>
    public Version(string Version) : this(Parse(Version)){}
    
    /// <summary>
    /// Получает версию из Assembly, если не указывать, то получит из текущего проекта
    /// </summary>
    public Version(Assembly? Assembly = null) : this(AssemblyVersion(Assembly)){}
    
    /// <summary>
    /// Получает версию из текущего проекта
    /// </summary>
    public Version() : this(AssemblyVersion(null)){}
    
    /// <summary>
    /// Глобальное обновление API
    /// </summary>
    public int Major{ get; }
    
    /// <summary>
    /// Нововведения
    /// </summary>
    public int Minor{ get; }
    
    /// <summary>
    /// Баг-фиксы
    /// </summary>
    public int Patch{ get; }
    
    /// <summary>
    /// Кол-во сборок
    /// </summary>
    public int Build{ get; }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Загрузка версии по строке
    /// </summary>
    /// <param name="Version">Строка формата "0.0.0.0", или "612.27.3", и т.д (удаляет всё после символа '+')</param>
    public static (int Major, int Minor, int Patch, int Build) Parse(string Version){
        try{
            // Убирает всё после '+'
            int PlusIndex = Version.IndexOf('+');
            if(PlusIndex >= 0){ Version = Version[..PlusIndex]; }
            
            string[] Parts = Version.Split('.');
            if(!Parts.All(P => int.TryParse(P, out int _))){ throw new Exception("Неверный формат версии!"); }

            int Major = int.Parse(Parts[0]);
            int Minor = Parts.Length > 1 ? int.Parse(Parts[1]) : 0;
            int Patch = Parts.Length > 2 ? int.Parse(Parts[2]) : 0;
            int Build = Parts.Length > 3 ? int.Parse(Parts[3]) : 0;

            return (Major, Minor, Patch, Build);
        }catch(Exception e){
            throw new Exception($"Не получилось получить версию из строки [\"{Version}\"]!", e);
        }
    }
    
    /// <summary>
    /// Получает версию из Assembly, если не указывать, то получит из текущего проекта
    /// </summary>
    public static string AssemblyVersion(Assembly? Assembly){
        try{
            Assembly ??= Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            AssemblyInformationalVersionAttribute? Attribute = Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            return Attribute?.InformationalVersion ?? "0.0.0.0";
        }catch(Exception e){
            throw new Exception($"Не получилось получить версию из Assembly [{WL.__Base.Other.ToString(Assembly)}]!", e);
        }
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"{Major}.{Minor}.{Patch}.{Build}";
}