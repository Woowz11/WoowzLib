using WLO;
using WoowzLibGenerator.Generator;

namespace WoowzLibGenerator;

public class Run{
    public static int Main(string[] Args){
        try{
            WL.Core.Metadata = (new ProjectMetadata("WoowzLibGenerator", null, "Woowz11"), null);
            WL.Core.BaseLoggerInitialize();
            WL.Core.EnableAutoTerminate();
            
            Generate();    
        }catch(Exception e){
            Logger.Fatal("Произошла ошибка во время генерации!", e);
            return 1;
        }
        
        return 0;
    }

    public static void Generate(){
        Logger.Info("Начало генерации!");
        
        Vector.Generate();
        
        Logger.Info("Конец генерации!");
    }
}