using File = WLO.File;

namespace WoowzLibGenerator.Generator;

public static class Vector{
    public static void Generate(){
        try{

            File file = new File("W:/Woowz11/Desktop/woowzlib_test_folder/test.txt");
            
            Logger.Info(new File("W:/Woowz11/Desktop/woowzlib_test_folder/test.txt").Type);
            
            Logger.Info(new File("W:/Woowz11/Desktop/woowzlib_test_folder/test").Type);
            
            Logger.Info(new File("W:/Woowz11/Desktop/w:oowzlib_test_folder/test2").Type);

        }catch(Exception e){
            throw new Exception("Произошла ошибка при генерации [Vector]!", e);
        }
    }
}