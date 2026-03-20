using System.Reflection;
using WLO;
using Version = WLO.Version;

namespace WL;

public static partial class __Base{
    /// <summary>
    /// Информация об проекте
    /// </summary>
    public static ProjectInfo ProjectInfo = new ProjectInfo();

    /// <summary>
    /// Информация об ядре
    /// </summary>
    public static ProjectInfo EngineInfo = new ProjectInfo("WoowzLib",new Version(Assembly.GetCallingAssembly()), "Woowz11", "CC BY SA 4.0");
}