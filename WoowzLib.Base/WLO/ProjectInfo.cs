using System.Reflection;

namespace WLO;

public class ProjectInfo{
    /// <summary>
    /// Название проекта
    /// </summary>
    public string Name = "Unknown Project";

    /// <summary>
    /// Версия проекта
    /// </summary>
    public Version Version = new Version();

    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Ядро проекта
    /// </summary>
    public string Engine = "WoowzLib";

    /// <summary>
    /// Версия ядра проекта
    /// </summary>
    public Version EngineVersion = new Version(Assembly.GetCallingAssembly());
}