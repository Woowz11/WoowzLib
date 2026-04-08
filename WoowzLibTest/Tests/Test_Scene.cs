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
            Test.F("Базовый тест", () => {
                SceneAlgorithm<TestObject> Scene = new SceneAlgorithm<TestObject>();

                Test.CheckResult(Scene.Count, 0, "Count");
                
                SceneNode<TestObject> c = Scene.Add(new TestObject{ VALUE = 1 });
                
                Test.CheckResult(Scene.Count, 1, "Add");
                
                Scene.Remove(c);
                
                Test.CheckResult(Scene.Count, 0, "Remove");
                
                c = Scene.Add(new TestObject{ VALUE = 4 });
                Scene.Add(new TestObject{ VALUE = 2 });
                Scene.Add(new TestObject{ VALUE = 128 });
                
                Test.CheckResult(Scene.Count, 3, "Add 2");
                
                Test.CheckResult(Scene.Contains(c), true, "Contains");

                Scene.Clear();
                
                Test.CheckResult(Scene.Count, 0, "Clear");

                var to = new TestObject{ VALUE = 1002020 };
                to.Node.Scene = Scene;
                
                Test.CheckResult(Scene.Count, 1, "Node.Scene");
                
                var c2 = new SceneNode<TestObject>(new TestObject{ VALUE = 3 });
                
                Test.CheckResult(c2.Count, 0, "Node Count");
                
                c2.Add(new TestObject{ VALUE = 5 });
                var c4 = c2.Add(new TestObject{ VALUE = 6 });
                var c3 = c2.Add(new TestObject{ VALUE = 3 });

                Test.CheckResult(c2.Count, 3, "Node Add");
                
                Test.CheckResult(c2.InMemory, true, "InMemory");
                
                Scene.Add(c3);

                Test.CheckResult(c3.Parent, null, "Установка сцены, удаление родителя");
                Test.CheckResult(c3.InMemory, false, "InMemory 2");
                
                Scene.Add(c2);
                Test.CheckResult(c4.Scene, Scene, "Установка сцены всем");
                Test.CheckResult(c4.Parent, c2, "Node.Parent");
                
                Test.CheckResult(Scene.ContainsDescendant(c4), true, "ContainsDescendant");
                
                Scene.Remove(c2);
                Test.CheckResult(c4.Scene, null, "Установка сцены всем 2");
                Test.CheckResult(c4.Parent, c2, "Node.Parent 2");
                
                Test.CheckResult(Scene.ContainsDescendant(c4), false, "ContainsDescendant 2");

                c = c3;
                for(int i = 0; i < 30; i++){
                    c = c.Add(new TestObject());
                }
                
                Logger.Debug(Scene.ToHierarchyString());
            });
            
            Test.F("SceneCacheMode.None", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.None);

                var root = scene.Add(new TestObject());
                var child = root.Add(new TestObject());
                var sub = child.Add(new TestObject());

                Test.CheckResult(scene.Count, 1, "Root count");
                Test.CheckResult(root.Count, 1, "Child count");

                // нет кеша → должно работать через пересчёт
                Test.CheckResult(scene.ContainsDescendant(sub), true, "Descendant calc");

                scene.Remove(root);

                Test.CheckResult(scene.Count, 0, "Remove root");
                Test.CheckResult(sub.Scene, null, "Scene cleared");
            });
            
            Test.F("SceneCacheMode.SceneOnly", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.SceneOnly);

                var root = scene.Add(new TestObject());
                var child = root.Add(new TestObject());
                var sub = child.Add(new TestObject());

                // проверка кеша сцены
                Test.CheckResult(scene.ContainsDescendant(sub), true, "Scene cache works");

                // удаление
                scene.Remove(root);

                Test.CheckResult(scene.ContainsDescendant(sub), false, "Cache updated");
            });
            
            Test.F("SceneCacheMode.Full", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.Full);

                var root = scene.Add(new TestObject());
                var child = root.Add(new TestObject());
                var sub = child.Add(new TestObject());

                // проверка кеша ноды
                Test.CheckResult(root.Childrens.Contains(sub), true, "Node cache works");

                // проверка propagate вверх
                var deep = sub.Add(new TestObject());

                Test.CheckResult(root.Childrens.Contains(deep), true, "Propagate add");

                // удаление
                child.Remove(sub);

                Test.CheckResult(root.Childrens.Contains(sub), false, "Propagate remove");
            });
            
            Test.F("Reparenting", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.Full);

                var a = scene.Add(new TestObject());
                var b = scene.Add(new TestObject());
                var x = a.Add(new TestObject());

                b.Add(x);

                Test.CheckResult(x.Parent, b, "Reparent");
                Test.CheckResult(a.Childrens.Contains(x), false, "Removed from old parent");
            });
            
            Test.F("Scene transfer", () =>
            {
                var s1 = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.Full);
                var s2 = new SceneAlgorithm<TestObject>(mode: SceneCacheMode.Full);

                var node = s1.Add(new TestObject());

                s2.Add(node);

                Test.CheckResult(node.Scene, s2, "Scene transfer");
                Test.CheckResult(s1.Contains(node), false, "Removed from old scene");
            });
        });
    }
}