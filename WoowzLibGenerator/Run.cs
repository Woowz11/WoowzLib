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

        string ResultFolder = "W:/Other/WoowzLib/__GENERATED";
        WL.Explorer.Folder.Clear(ResultFolder);

        WL.Explorer.File.Create(WL.String.Path.Add(ResultFolder, ".gitignore"), "DEBUG/");
        
        string ForResult = WL.String.Path.Add(ResultFolder, "RELEASE");
        string ForDebug  = WL.String.Path.Add(ResultFolder, "DEBUG");
        
        Vector.Generate(WL.String.Path.Add(ForResult, "Vector"), WL.String.Path.Add(ForDebug, "Vector"));
        Rect  .Generate(WL.String.Path.Add(ForResult, "Rect"), WL.String.Path.Add(ForDebug, "Rect"));
        
        Logger.Info("Конец генерации!");
    }
}