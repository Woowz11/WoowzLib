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
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.None);

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
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.SceneOnly);

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
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);

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
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);

                var a = scene.Add(new TestObject());
                var b = scene.Add(new TestObject());
                var x = a.Add(new TestObject());

                b.Add(x);

                Test.CheckResult(x.Parent, b, "Reparent");
                Test.CheckResult(a.Childrens.Contains(x), false, "Removed from old parent");
            });
            
            Test.F("Scene transfer", () =>
            {
                var s1 = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);
                var s2 = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);

                var node = s1.Add(new TestObject());

                s2.Add(node);

                Test.CheckResult(node.Scene, s2, "Scene transfer");
                Test.CheckResult(s1.Contains(node), false, "Removed from old scene");
            });
            
            Test.F("Проверка Contains и ContainsDescendant без кеша", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.None);
                var root = scene.Add(new TestObject{ VALUE = 10 });
                var child = root.Add(new TestObject{ VALUE = 20 });
                var grand = child.Add(new TestObject{ VALUE = 30 });

                // Корневой объект
                Test.CheckResult(scene.Contains(root), true, "Contains корень");
                // Потомки
                Test.CheckResult(scene.ContainsDescendant(child), true, "ContainsDescendant ребёнок");
                Test.CheckResult(scene.ContainsDescendant(grand), true, "ContainsDescendant внук");

                // Проверка уровня Node
                Test.CheckResult(root.Contains(child), true, "Node.Contains ребёнок");
                Test.CheckResult(root.ContainsDescendant(grand), true, "Node.ContainsDescendant внук");
            });
            
            Test.F("Защита от циклов", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();
                var root = scene.Add(new TestObject());
                var child = root.Add(new TestObject());

                bool exceptionThrown = false;
                try{
                    root.Parent = child; // Попытка сделать родителем своего потомка
                }catch(Exception){
                    exceptionThrown = true;
                }

                Test.CheckResult(exceptionThrown, true, "Нельзя сделать родителем потомка");
            });
            
            Test.F("Очистка сцены и кешей", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);
                var root = scene.Add(new TestObject());
                var child = root.Add(new TestObject());
                var grand = child.Add(new TestObject());

                scene.Clear();

                Test.CheckResult(scene.Count, 0, "Count после Clear");
                Test.CheckResult(root.Parent, null, "Root.Parent после Clear");
                Test.CheckResult(child.Scene, null, "Child.Scene после Clear");
                Test.CheckResult(grand.Scene, null, "Grand.Scene после Clear");

                // Кеши должны быть очищены
                Test.CheckResult(scene.ContainsDescendant(grand), false, "ContainsDescendant после Clear");
            });
            
            Test.F("Перепривязка нескольких уровней", () =>
            {
                var scene = new SceneAlgorithm<TestObject>(Mode: SceneCacheMode.Full);

                var a = scene.Add(new TestObject{ VALUE = 1 });
                var b = scene.Add(new TestObject{ VALUE = 2 });
                var c = a.Add(new TestObject{ VALUE = 3 });
                var d = c.Add(new TestObject{ VALUE = 4 });

                // Перепривязка "сверху"
                b.Add(c);

                Test.CheckResult(c.Parent, b, "C.Parent после reparent");
                Test.CheckResult(d.Parent, c, "D.Parent остаётся прежним");
                Test.CheckResult(a.Childrens.Contains(c), false, "A.Childrens не содержит C после reparent");
                Test.CheckResult(b.Childrens.Contains(c), true, "B.Childrens содержит C после reparent");
            });
            
            Test.F("Установка Scene через Node.Scene", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();
                var node = new TestObject().Node;

                node.Scene = scene;

                Test.CheckResult(node.Scene, scene, "Node.Scene установлена");
                Test.CheckResult(scene.Contains(node), true, "Сцена содержит Node после установки через Node.Scene");
            });
            
            Test.F("Event: OnAfterAdd", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var log = new List<int>();

                scene.OnAfterAdd += (s, n) => log.Add(1);
                scene.OnAfterAdd += (s, n) => log.Add(2);

                var node = scene.Add(new TestObject());

                Test.CheckResult(log.Count, 2, "call count");
                Test.CheckResult(log[0], 1, "order 1");
                Test.CheckResult(log[1], 2, "order 2");
            });
            
            Test.F("Event: OnAfterRemove", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var log = new List<int>();

                scene.OnAfterRemove += (s, n) => log.Add(10);
                scene.OnAfterRemove += (s, n) => log.Add(20);

                var node = scene.Add(new TestObject());

                scene.Remove(node);

                Test.CheckResult(log.SequenceEqual(new[] { 10, 20 }), true, "order + calls");
            });
            
            Test.F("Event: OnBeforeRemove cancel", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var node = scene.Add(new TestObject());

                scene.OnBeforeRemove += (s, n) => false; // блокируем удаление

                scene.Remove(node);

                Test.CheckResult(scene.Contains(node), true, "node still exists");
            });
            
            Test.F("Event: OnBeforeAdd transform", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                scene.OnBeforeAdd += (s, n) =>
                {
                    n.Self.VALUE = 999;
                    return n;
                };

                var node = scene.Add(new TestObject());

                Test.CheckResult(node.Self.VALUE, 999, "modified before add");
            });
            
            Test.F("Event: OnBeforeAdd replace node", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var replacement = new SceneNode<TestObject>(new TestObject { VALUE = 777 });

                scene.OnBeforeAdd += (s, n) => replacement;

                var node = scene.Add(new TestObject { VALUE = 1 });

                Test.CheckResult(node, replacement, "node replaced");
            });
            
            Test.F("Event: OnSceneChangeAfter", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var node = new TestObject().Node;

                SceneAlgorithm<TestObject>? oldScene = null;
                SceneAlgorithm<TestObject>? newScene = null;

                node.OnSceneChangeAfter += (n, old, neu) =>
                {
                    oldScene = old;
                    newScene = neu;
                };

                node.Scene = scene;

                Test.CheckResult(oldScene, null, "old scene");
                Test.CheckResult(newScene, scene, "new scene");
            });
            
            Test.F("Event: OnParentChangeAfter", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var a = scene.Add(new TestObject());
                var b = scene.Add(new TestObject());
                var c = a.Add(new TestObject());

                SceneNode<TestObject>? oldParent = null;
                SceneNode<TestObject>? newParent = null;

                c.OnParentChangeAfter += (n, old, neu) =>
                {
                    oldParent = old;
                    newParent = neu;
                };

                b.Add(c);

                Test.CheckResult(oldParent, a, "old parent");
                Test.CheckResult(newParent, b, "new parent");
            });
            
            Test.F("Event: OnSceneChangeBefore cancel", () =>
            {
                var scene = new SceneAlgorithm<TestObject>();

                var node = new TestObject().Node;

                bool afterCalled = false;

                node.OnSceneChangeBefore += (n, old, neu) => false;
                node.OnSceneChangeAfter += (n, old, neu) => afterCalled = true;

                node.Scene = scene;

                Test.CheckResult(afterCalled, false, "after must not fire");
                Test.CheckResult(node.Scene, null, "scene unchanged");
            });
        });
    }
}