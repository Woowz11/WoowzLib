using System.Text;
using WLO;

namespace WL;

[WLModule(-500, 8)]
public class Logger{
    static Logger(){
        WL.WoowzLib.OnStart += () => {
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
            Print(global::Logger.MessageType.Info, [Message]);
        }

        public override void Write(char Message){
            OriginalOut.Write(Message);
        }
    }

    private static string MessagePrefix(global::Logger.MessageType Type, bool First){
        try{
            string Char = "~";
            if(First){
                Char = Type switch{
                    global::Logger.MessageType.Info  => "I",
                    global::Logger.MessageType.Warn  => "W",
                    global::Logger.MessageType.Error => "E",
                    global::Logger.MessageType.Fatal => "F",
                    global::Logger.MessageType.Debug => "D"
                };
            }

            return Char + ":[" + WL.Math.Time.Format("HH:mm:ss:fff") + "]: ";
        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации префикса для сообщения!\nТип: " + Type + "\nПервое?: " + First, e);
        }
    }
    
    private static void Print(global::Logger.MessageType Type, object[]? Message){
        try{
            if(Message == null){ Message = [null]; }

            string FullMessage = WL.String.Join(Message);
            string[] Lines = FullMessage.Split('\n');
            
            ConsoleColor ColorD = Type switch{
                global::Logger.MessageType.Info  => ConsoleColor.Gray,
                global::Logger.MessageType.Warn  => ConsoleColor.DarkYellow,
                global::Logger.MessageType.Error => ConsoleColor.DarkRed,
                global::Logger.MessageType.Fatal => ConsoleColor.DarkMagenta,
                global::Logger.MessageType.Debug => ConsoleColor.DarkGreen
            };
            ConsoleColor ColorL = Type switch{
                global::Logger.MessageType.Info  => ConsoleColor.White,
                global::Logger.MessageType.Warn  => ConsoleColor.Yellow,
                global::Logger.MessageType.Error => ConsoleColor.Red,
                global::Logger.MessageType.Fatal => ConsoleColor.Magenta,
                global::Logger.MessageType.Debug => ConsoleColor.Green
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