using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Rect{
    
    // ----------------------------------------------------------------------
    
    private static string OutFolder      = null!;
    private static string OutFolderDebug = null!;
    public static void Generate(string OutFolder__, string OutFolderDebug__){
        try{
            OutFolder = OutFolder__; 
            WL.Explorer.Folder.GetOrCreate(OutFolder);

            OutFolderDebug = OutFolderDebug__;
            WL.Explorer.Folder.GetOrCreate(OutFolderDebug);

        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Rect]!", e);
        }
    }
    
    // ----------------------------------------------------------------------
}