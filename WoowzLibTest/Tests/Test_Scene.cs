using WLO;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест системы родителей и детей
/// </summary>
public static class Test_Scene{
    private class TestObject : SceneObject<TestObject>{
        public int VALUE;
    }
    
    public static void Run(){
        Test.Run("Scene", () => {
            Test.F("Базовое (с примитивами)", () => {
                SceneAlgorithm<TestObject> Scene = new SceneAlgorithm<TestObject>();

                SceneNode<TestObject> c = Scene.Add(new TestObject{ VALUE = 1 });
                
                Logger.Debug(WL.String.ToTableString(Scene.Layer0));
                Logger.Debug(WL.String.ToTableString(Scene.Childrens));
                
                Test.CheckResult(Scene.Count, 1, "1");
                
                Scene.Remove(c);
                
                Test.CheckResult(Scene.Count, 0, "2");

                return;
                
                c = Scene.Add(new TestObject{ VALUE = 4 });
                Scene.Add(new TestObject{ VALUE = 2 });
                Scene.Add(new TestObject{ VALUE = 128 });
                
                // check count == 3
                
                // check Scene.Contains(2)

                Scene.Clear();
                
                // check count == 0

                var c2 = new SceneNode<TestObject>(new TestObject{ VALUE = 3 });
                c2.Add(new TestObject{ VALUE = 5 });
                c2.Add(new TestObject{ VALUE = 6 });
                var c3 = c2.Add(new TestObject{ VALUE = 3 });

                Scene.Add(c3); // c3.Parent != c2, c2 in memory

                Scene.Add(c2); // c2 in scene, and childs c2 in scene too
                
                Scene.Remove(c2); // c2 now in memory, and c2 childs too
            });
        });
    }
}