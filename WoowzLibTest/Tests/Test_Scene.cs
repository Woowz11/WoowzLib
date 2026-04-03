using WLO;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест системы родителей и детей
/// </summary>
public static class Test_Scene{
    public static void Run(){
        Test.Run("Scene", () => {
            Test.F("Базовое (с примитивами)", () => {
                SceneAlgorithm<int> Scene = new SceneAlgorithm<int>();

                var c = Scene.Add(1);
                
                // check count == 1
                
                Scene.Remove(c);
                
                // check count == 0

                c = Scene.Add(4);
                Scene.Add(2);
                Scene.Add(128);
                
                // check count == 3
                
                // check Scene.Contains(2)

                Scene.Clear();
                
                // check count == 0

                var c2 = new SceneNode<int>(3);
                c2.Add(5);
                c2.Add(7);
                var c3 = c2.Add(3);

                Scene.Add(c3); // c3.Parent != c2, c2 in memory

                Scene.Add(c2); // c2 in scene, and childs c2 in scene too
                
                Scene.Remove(c2); // c2 now in memory, and c2 childs too
            });
        });
    }
}