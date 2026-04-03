using WLO;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест системы родителей и детей
/// </summary>
public static class Test_Scene{
    public static void Run(){
        Test.Run("Scene", () => {
            Test.F("idk", () => {
                SceneAlgorithm<int> Scene = new SceneAlgorithm<int>();
                
                Test.CheckResult(Scene.Contains(2), false, "Contains не работает!");
                
                Scene.Add(5);
                Scene.Add(2);
                Scene.Add(-2);
                
                Test.CheckResult(Scene.Contains(2), true, "Contains 2 не работает!");
            });
        });
    }
}