using WLO;
using WoowzLibTest;

public class Run{
    public static int Main(string[] Args){
        WL.Core.Metadata = (new ProjectMetadata("WoowzLibTest", null, "Woowz11"), null);
        WL.Logger.Initialize();
        WL.Core.EnableAutoTerminate();
        
        Test.Run();
        
        return 0;
    }
}