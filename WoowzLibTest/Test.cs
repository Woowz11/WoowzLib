using WoowzLibTest.Tests;

namespace WoowzLibTest;

public static class Test{
    /// <summary>
    /// Всего ошибок
    /// </summary>
    public static int TotalErrors = 0;

    /// <summary>
    /// Проваленных тестов
    /// </summary>
    public static int FailedTests = 0;
    
    /// <summary>
    /// Тест
    /// </summary>
    public static void F(string Name, Action Action){
        try{
            if(WL.String.IsWhiteSpace(Name)){ return; }
            
            Action();
            
            Logger.Info("\t+ \"" + Name + "\"");
        }catch(Exception e){
            TotalErrors++;
            
            Logger.Info("\t- \"" + Name + "\"");
            Logger.Error(e);
        }
    }

    /// <summary>
    /// Тест с проверкой результата
    /// </summary>
    public static void F<T>(string Name, T Expected, Func<T> Action){
        try{
            if(WL.String.IsWhiteSpace(Name)){ return; }
            
            T Result = Action();

            if(EqualityComparer<T>.Default.Equals(Expected, Result)){
                Logger.Info("\t+ \"" + Name + "\" | " + WL.__Base.Other.ToBeautifulString(Expected) + " == " + WL.__Base.Other.ToBeautifulString(Result));   
            }else{
                TotalErrors++;
                Logger.Info("\t- \"" + Name + "\" | " + WL.__Base.Other.ToBeautifulString(Expected) + " != " + WL.__Base.Other.ToBeautifulString(Result));
            }
        }catch(Exception e){
            TotalErrors++;
            Logger.Info("\t- " + Name);
            Logger.Error(e);
        }
    }

    /// <summary>
    /// Сравнивает значения
    /// </summary>
    public static void CheckResult<T>(T Result, T Expected, string ErrorMessage = "Значения не равны!"){ if(!EqualityComparer<T>.Default.Equals(Expected, Result)){ throw new Exception(ErrorMessage + "\n" + WL.String.ToBeautifulString(Expected) + " != " + WL.String.ToBeautifulString(Result)); } }
    
    /// <summary>
    /// Не сравнивает значения
    /// </summary>
    public static void NotCheckResult<T>(T Result, T NotExpected, string ErrorMessage = "Значения равны!"){ if(EqualityComparer<T>.Default.Equals(NotExpected, Result)){ throw new Exception(ErrorMessage + "\n" + WL.String.ToBeautifulString(NotExpected) + " == " + WL.String.ToBeautifulString(Result)); } }
    
    /// <summary>
    /// Run функция
    /// </summary>
    public static void Run(string Name, Action Action){
        Logger.Info("Запуск теста \"" + Name + "\"!");
        TotalErrors = 0;
        
        Action();

        if(TotalErrors > 0){
            Logger.Info("Тест \"" + Name + "\", прошёл с ошибками! Ошибок: " + TotalErrors);
            FailedTests++;
        }else{
            Logger.Info("Тест \"" + Name + "\", прошёл успешно!");
        }
        
        Logger.Info("");
    }
    
    public static void Run(){
        Test_CSharp.Run();
        Test_Base  .Run();
        Test_Vector.Run();
        Test_String.Run();
        Test_Path  .Run();

        if(FailedTests > 0){
            Logger.Error("Есть проваленные тесты! Проваленных тестов: " + FailedTests);
        }
    }
}