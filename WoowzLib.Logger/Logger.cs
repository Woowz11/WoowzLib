using System.Text;
using WLO;

namespace WL;

[WLModule(-500, 2)]
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

        WL.WoowzLib.OnStop += () => {
            Console.ForegroundColor = ConsoleColor.Gray;
        };
    }
    
    public static TextWriter OriginalOut{ get; private set; }
    
    private class LoggerWriter : TextWriter{
        public override Encoding Encoding => OriginalOut.Encoding;

        public override void WriteLine(string? Message){
            Print(MessageType.Info, [string.IsNullOrEmpty(Message) ? "NULL" : Message]);
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
    
    private static void Print(MessageType Type, object[]? Message){
        try{
            string FullMessage = WL.String.Join(Message);
            string[] Lines = FullMessage.Split('\n');
            
            ConsoleColor ColorD = Type switch{
                MessageType.Info  => ConsoleColor.Gray,
                MessageType.Warn  => ConsoleColor.DarkYellow,
                MessageType.Error => ConsoleColor.DarkRed,
                MessageType.Fatal => ConsoleColor.DarkMagenta,
                MessageType.Debug => ConsoleColor.DarkGreen
            };;
            ConsoleColor ColorL = Type switch{
                MessageType.Info  => ConsoleColor.White,
                MessageType.Warn  => ConsoleColor.Yellow,
                MessageType.Error => ConsoleColor.Red,
                MessageType.Fatal => ConsoleColor.Magenta,
                MessageType.Debug => ConsoleColor.Green
            };

            for(int i = 0; i < Lines.Length; i++){
                Console.ForegroundColor = __Eval ? ColorD : ColorL;

                string Prefix = MessagePrefix(Type, i == 0);
                OriginalOut.WriteLine(Prefix + Lines[i]);

                __Eval = !__Eval;
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при отправке сообщения типа [" + Type + "]!\nСообщение: (" + WL.String.Join(Message) + ")");
        }
    }
    private static bool __Eval;
}