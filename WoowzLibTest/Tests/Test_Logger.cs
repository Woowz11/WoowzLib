using WLO;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест Logger
/// </summary>
public static class Test_Logger{
    public static void Run(){
        void Message(string? Message = null){
            Logger.Info (Message ?? "INFO" );
            Logger.Warn (Message ?? "WARN" );
            Logger.Error(Message ?? "ERROR");
            Logger.Fatal(Message ?? "FATAL");
            Logger.Debug(Message ?? "DEBUG");
        }
        
        Test.Run("Logger", () => {
            Test.F("Просто сообщения", () => {
                Message();
                Message("MULTI\nLINE\nMESSAGE");

                foreach(MessageStatus MS in Enum.GetValues<MessageStatus>()){
                    for(int i = 0; i < 3; i++){ Logger.Custom((byte)MS, null, "###"); }
                }
            });
        });
    }
}