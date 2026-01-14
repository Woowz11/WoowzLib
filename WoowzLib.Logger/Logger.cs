using System.Text;
using WLO;

namespace WL;

[WLModule(-500)]
public class Logger{
    static Logger(){
        WL.WoowzLib.OnStarted += () => {
            try{
                OriginalOut = Console.Out;
                Console.SetOut(new LoggerWriter());
                
                WL.WoowzLib.__RemoveOnMessage();
                WL.WoowzLib.OnMessage += Print;
            }
            catch(Exception e){
                throw new Exception("Произошла ошибка при установке Logger!", e);
            }
        };
    }
    
    public static TextWriter OriginalOut{ get; private set; }
    
    private class LoggerWriter : TextWriter{
        public override Encoding Encoding => OriginalOut.Encoding;

        public override void WriteLine(string? Message){
            Print(MessageType.Info, [Message ?? "NULL"]);
        }

        public override void Write(char Message){
            OriginalOut.Write(Message);
        }
    }

    private static string MessagePrefix(MessageType Type, bool First){
        try{
            string Char = "~";
            if(First){
                Char = Type switch{
                    MessageType.Info  => "I",
                    MessageType.Warn  => "W",
                    MessageType.Error => "E",
                    MessageType.Fatal => "F",
                    MessageType.Debug => "D"
                };
            }

            return Char + ":[" + WL.Math.Time.Format("HH:mm:ss:fff") + "]: ";
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации префикса для сообщения!\nТип: " + Type + "\nПервое?: " + First, e);
        }
    }
    
    private static void Print(MessageType Type, object[] Message){
        try{
            ConsoleColor PreviousColor = Console.ForegroundColor;

            Console.ForegroundColor = Type switch{
                MessageType.Info  => ConsoleColor.White,
                MessageType.Warn  => ConsoleColor.Yellow,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Fatal => ConsoleColor.Magenta,
                MessageType.Debug => ConsoleColor.Green,
                                _ => PreviousColor
            };

            string Result = WL.String.Join(Message).Split('\n').Select((Line, Index) => (Line, Index)).Aggregate("", (Current, Item) => Current + (MessagePrefix(Type, Item.Index == 0) + Item.Line + "\n"));

            OriginalOut.Write(Result);
            
            Console.ForegroundColor = PreviousColor;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при отправке сообщения типа [" + Type + "]!\nСообщение: (" + WL.String.Join(Message) + ")");
        }
    }
}