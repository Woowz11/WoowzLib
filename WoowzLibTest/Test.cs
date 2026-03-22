using WoowzLibTest.Tests;

namespace WoowzLibTest;

public static class Test{
    /// <summary>
    /// Всего ошибок
    /// </summary>
    public static int TotalErrors = 0;
    
    /// <summary>
    /// Тест
    /// </summary>
    public static void F(string Name, Action Action){
        try{
            Action();
            
            Logger.Info("\t+ " + Name);
        }catch(Exception e){
            TotalErrors++;
            
            Logger.Info("\t- " + Name);
            Logger.Error(e);
        }
    }

    /// <summary>
    /// Тест с проверкой результата
    /// </summary>
    public static void F<T>(string Name, T Expected, Func<T> Action){
        try{
            T Result = Action();

            if(EqualityComparer<T>.Default.Equals(Expected, Result)){
                Logger.Info("\t+ " + Name + " | " + WL.__Base.Other.ToString(Expected) + " == " + WL.__Base.Other.ToString(Result));   
            }else{
                TotalErrors++;
                Logger.Info("\t- " + Name + " | " + WL.__Base.Other.ToString(Expected) + " != " + WL.__Base.Other.ToString(Result));
            }
        }catch(Exception e){
            TotalErrors++;
            Logger.Info("\t- " + Name);
            Logger.Error(e);
        }
    }

    /// <summary>
    /// Run функция
    /// </summary>
    public static void Run(string Name, Action Action){
        Logger.Info("Запуск теста \"" + Name + "\"!");
        TotalErrors = 0;
        
        Action();

        if(TotalErrors > 0){
            Logger.Info("Тест \"" + Name + "\", прошёл с ошибками! Ошибок: " + TotalErrors);
        }else{
            Logger.Info("Тест \"" + Name + "\", прошёл успешно!");
        }
    }
    
    public static void Run(){
        Test_CSharp.Run();
        Test_Base.Run();
    }
}