using WL;

public static class Logger{
    /// <summary>
    /// Информационное сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Info(params object[] Message) => Custom((byte)MessageStatus.Default, null, Message);
    
    /// <summary>
    /// Предупреждающее сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Warn(params object[] Message) => Custom((byte)MessageStatus.Warning, null, Message);
    
    /// <summary>
    /// Ошибочное сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Error(params object[] Message) => Custom((byte)MessageStatus.Error, null, Message);
    
    /// <summary>
    /// Фатальное сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Fatal(params object[] Message) => Custom((byte)MessageStatus.Fatal, null, Message);
    
    /// <summary>
    /// Отладочное сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Debug(params object[] Message) => Custom((byte)MessageStatus.Debug, null, Message);
    
    /// <summary>
    /// Собственное сообщение
    /// </summary>
    /// <param name="Message">Сообщение</param>
    public static void Custom(byte Status, object? ExtraInfo, params object[] Message){ WL.__Base.Logger.Print(
        Status,
        ExtraInfo,
        WL.__Base.Other.JoinString(Message)
    ); }
}