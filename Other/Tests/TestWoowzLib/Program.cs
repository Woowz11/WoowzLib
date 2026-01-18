using Logger = WLO.Logger;

public static class Program{
    
    public static int Main(string[] Args){
        try{
            WL.WoowzLib.Start();
        }catch(Exception e){
            Logger.Fatal("ОШИБКА ВНУТРИ ПРИЛОЖЕНИЯ", e);
            return 1;
        }
        
        return 0;
    }
}