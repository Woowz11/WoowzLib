using System.Diagnostics;

namespace WL;

/// <summary>
/// Состояние сообщения
/// </summary>
public enum MessageStatus : byte{
    /// <summary>
    /// Обычное сообщение
    /// </summary>
    Default = 0,
    /// <summary>
    /// Предупреждение
    /// </summary>
    Warning = 1,
    /// <summary>
    /// Ошибка
    /// </summary>
    Error = 2,
    /// <summary>
    /// Crash, фатальная ошибка
    /// </summary>
    Fatal = 3,
    /// <summary>
    /// Отладка
    /// </summary>
    Debug = 4
}

public static partial class __Base{
    public static class Logger{
        static Logger(){
            OnPrint += (Status, ExtraInfo, Message) => (Status, ExtraInfo, Message);
        }
        
        /// <summary>
        /// Выводит сообщение в консоль
        /// </summary>
        public static void Print(byte Status, object? ExtraInfo, string Message){
            try{
                (byte Status, object? ExtraInfo, string Message)? Message__ = OnPrint?.Invoke(Status, ExtraInfo, Message);
                if(!Message__.HasValue){ return; }
                
                Output?.Invoke(Message__.Value.Status, Message__.Value.ExtraInfo, Message__.Value.Message);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при отправке сообщения!\nСтатус: " + Status + "\nСообщение:\n" + Message, e);
            }
        }

        /// <summary>
        /// Вызывается при вызове вывода сообщения в консоль, возвращает: (статус, доп. информация, сообщение), получает: (статус, доп. информация, сообщение), если вернуть null, то сообщение не отправится
        /// </summary>
        public static event Func<byte, object?, string, (byte, object?, string)?>? OnPrint;
        
        /// <summary>
        /// Функция вывода сообщения в консоль, получает: (статус, доп. информация, сообщение)
        /// </summary>
        public static Action<byte, object?, string>? Output = (byte Status, object? ExtraInfo, string Message) => {
            string StatusString = Status switch{
                0     => "I",
                1     => "W",
                2     => "E",
                3     => "F",
                4     => "D",
                var _ => Status.ToString()
            };
            
            Console.WriteLine(StatusString + ": " + Message);
        };
    }
}