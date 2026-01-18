namespace WLO;

public enum MessageType{
    Info, Warn, Error, Fatal, Debug
}

public static class Logger{
    /// <summary>
    /// Информационное сообщение
    /// </summary>
    public static void Info(params object[]? Message){ WL.WoowzLib.__Print(MessageType.Info, Message); }
    /// <summary>
    /// Предупреждающее сообщение
    /// </summary>
    public static void Warn(params object[]? Message){ WL.WoowzLib.__Print(MessageType.Warn, Message); }
    /// <summary>
    /// Ошибка
    /// </summary>
    public static void Error(params object[]? Message){ WL.WoowzLib.__Print(MessageType.Error, Message); }
    /// <summary>
    /// Фатальная ошибка
    /// </summary>
    public static void Fatal(params object[]? Message){ WL.WoowzLib.__Print(MessageType.Fatal, Message); }
    /// <summary>
    /// Отладочное сообщение
    /// </summary>
    public static void Debug(params object[]? Message){ WL.WoowzLib.__Print(MessageType.Debug, Message); }
}