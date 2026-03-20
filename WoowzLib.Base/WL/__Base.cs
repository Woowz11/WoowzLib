using System.Reflection;
using WLO;
using Version = WLO.Version;

namespace WL;

public static partial class __Base{
    /// <summary>
    /// Информация об проекте
    /// </summary>
    public static ProjectMetadata ProjectMetadata = new ProjectMetadata();

    /// <summary>
    /// Информация об ядре
    /// </summary>
    public static ProjectMetadata EngineMetadata = new ProjectMetadata("WoowzLib",new Version(Assembly.GetCallingAssembly()), "Woowz11", "CC BY SA 4.0");
}