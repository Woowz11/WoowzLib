public static class Logger{
    /// <summary>
    /// Информационное сообщение
    /// </summary>
    public static void Info(params object[]? Message){ WL.WoowzLib.__Print(Logger.MessageType.Info, Message); }
    /// <summary>
    /// Предупреждающее сообщение
    /// </summary>
    public static void Warn(params object[]? Message){ WL.WoowzLib.__Print(Logger.MessageType.Warn, Message); }
    /// <summary>
    /// Ошибка
    /// </summary>
    public static void Error(params object[]? Message){ WL.WoowzLib.__Print(Logger.MessageType.Error, Message); }
    /// <summary>
    /// Фатальная ошибка
    /// </summary>
    public static void Fatal(params object[]? Message){ WL.WoowzLib.__Print(Logger.MessageType.Fatal, Message); }
    /// <summary>
    /// Отладочное сообщение
    /// </summary>
    public static void Debug(params object[]? Message){ WL.WoowzLib.__Print(Logger.MessageType.Debug, Message); }
    
    public enum MessageType{
        Info, Warn, Error, Fatal, Debug
    }
}