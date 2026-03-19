namespace WL;

public static class Core{
    /// <summary>
    /// Запускает WoowzLib
    /// </summary>
    public static void Initialize() => WL.__Base.Initialize();

    /// <summary>
    /// Останавливает WoowzLib
    /// </summary>
    public static void Terminate() => WL.__Base.Terminate();
}