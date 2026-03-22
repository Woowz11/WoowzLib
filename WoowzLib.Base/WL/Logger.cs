using System.CodeDom.Compiler;
using System.Text;
using WLO;

namespace WLO{
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
        Debug = 4,
        /// <summary>
        /// Сообщение из вне
        /// </summary>
        External = 5
    }
}

namespace WL{
    
    public static partial class __Base{
        public static class Logger{
            /// <summary>
            /// Запуск Logger
            /// </summary>
            public static void Initialize(){
                try{
                    if(Initialized){ throw new Exception("Logger уже инициализированный!"); } Initialized = true;

                    Warned = false;
                    
                    TextWriter = new WoowzLibTextWriter(Console.Out);
                    Console.SetOut(TextWriter);

                    WL.__Base.OnTerminate += __Terminate;
                
                    OnPrint += (Status, ExtraInfo, Message) => (Status, ExtraInfo, Message);
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при инициализации Logger!", e);
                }
            }
            
            /// <summary>
            /// Logger запущенный?
            /// </summary>
            public static bool Initialized{ get; private set; }

            /// <summary>
            /// Вызывается при остановке
            /// </summary>
            private static void __Terminate(){
                try{
                    if(!Initialized){ throw new Exception("Logger не инициализирован!"); }

                    Console.SetOut(TextWriter!.Original);
                    TextWriter = null;
                    WL.__Base.OnTerminate -= __Terminate;
                    
                    Initialized = false;
                }catch(Exception e){
                    global::Logger.Error("Произошла ошибка при остановке Logger!", e);
                }
            }
            
            // ----------------------------------------------------------------------

            /// <summary>
            /// Пользователь предупреждён, что Logger не инициализирован?
            /// </summary>
            private static bool Warned;

            /// <summary>
            /// Выводит сообщение в консоль
            /// </summary>
            public static void Print(byte Status, object? ExtraInfo, string Message){
                try{
                    if(!Initialized && !Warned){
                        Warned = true;
                        global::Logger.Warn("Logger не инициализирован! Возможны ошибки, инициализируйте Logger что-бы использовать Logger.Info(...), Logger.Warn(...), и т.д.");
                    }
                    
                    (byte Status, object? ExtraInfo, string Message)? Message__ = OnPrint?.Invoke(Status, ExtraInfo, Message);
                    if(!Message__.HasValue){ return; }

                    string? Result = Output?.Invoke(Message__.Value.Status, Message__.Value.ExtraInfo, Message__.Value.Message);
                    if(Result != null){ OriginalPrint(Result); }
                }
                catch(Exception e){
                    throw new Exception("Произошла ошибка при отправке сообщения!\nСтатус: " + Status + "\nСообщение:\n" + Message, e);
                }
            }

            /// <summary>
            /// Вызывается при вызове вывода сообщения в консоль, получает: (статус, доп. информация, сообщение), если вернуть null, то сообщение не отправится, возвращает: (статус, доп. информация, сообщение)
            /// </summary>
            public static event Func<byte, object?, string, (byte, object?, string)?>? OnPrint;

            /// <summary>
            /// Функция вывода сообщения в консоль, получает: (статус, доп. информация, сообщение), возвращает: (сообщение), если вернуть null, то сообщение не отправится
            /// </summary>
            public static Func<byte, object?, string, string?>? Output = (byte Status, object? ExtraInfo, string Message) => {
                string StatusString = Status switch{
                    0 => "I",
                    1 => "W",
                    2 => "E",
                    3 => "F",
                    4 => "D",
                    5 => "?",
                    var _ => Status.ToString()
                };

                return StatusString + ": " + Message;
            };

            /// <summary>
            /// Вывод в консоль
            /// </summary>
            public static WoowzLibTextWriter? TextWriter = null;

            /// <summary>
            /// Выводит сообщение в оригинальную консоль
            /// </summary>
            public static void OriginalPrint(string Message){ if(TextWriter == null){ Console.WriteLine(Message); }else{ TextWriter.Original.WriteLine(Message); } }
            
            // ----------------------------------------------------------------------

            public sealed class WoowzLibTextWriter : TextWriter{
                public WoowzLibTextWriter(TextWriter Original){
                    this.Original = Original;
                }
                
                /// <summary>
                /// Оригинальный TextWriter
                /// </summary>
                public readonly TextWriter Original;

                /// <summary>
                /// Кодировка вывода
                /// </summary>
                public override Encoding Encoding => Original.Encoding;
                
                public override void WriteLine(string? Value) => Handle((Value ?? "") + '\n');
                public override void Write    (string? Value) => Handle(Value ?? "");
                
                private readonly StringBuilder SB = new StringBuilder();

                /// <summary>
                /// Обработка сообщений из вне
                /// </summary>
                private void Handle(string Message){
                    try{
                        SB.Append(Message);

                        while(true){
                            int NewLineIndex = SB.ToString().IndexOf('\n');
                            if(NewLineIndex == -1){ break; }

                            string Line = SB.ToString(0, NewLineIndex).TrimEnd('\r');

                            SB.Remove(0, NewLineIndex + 1);
                            
                            WL.__Base.Logger.Print((byte)MessageStatus.External, null, Line);
                        }
                    }catch(Exception e){
                        throw new Exception("Произошла ошибка при обработке сообщения в консоли!\nСообщение:\n" + Message, e);
                    }
                }
            }
        }
    }
}