using WLO;
using WLO.Rect;
using WLO.Vector;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест трансформации
/// </summary>
public static class Test_Transform{
     private class TestObject : SceneObject<TestObject>, ITransform
    {
        public int Updates = 0;

        void ITransform.__UpdateTransform(object? Data){
            Updates++;
        }
    }

    public static void Run(){
        Test.Run("Transform", () =>
        {
            Test.F("Базовые сеттеры", () =>
            {
                var t = new TransformAlgorithm();

                t.X = 10;
                t.Y = 20;
                t.W = 30;
                t.H = 40;

                Test.CheckResult(t.Rect.X, 10, "X");
                Test.CheckResult(t.Rect.Y, 20, "Y");
                Test.CheckResult(t.Rect.W, 30u, "W");
                Test.CheckResult(t.Rect.H, 40u, "H");
            });

            Test.F("OnPosition изменяет значение", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, pos) => new Vector2I(pos.X + 5, pos.Y + 5);
                
                t.Position = new Vector2I(10, 10);

                Test.CheckResult(t.Position.X, 15, "X modified");
                Test.CheckResult(t.Position.Y, 15, "Y modified");
            });

            Test.F("OnSize изменяет значение", () =>
            {
                var t = new TransformAlgorithm();

                t.OnSize += (self, size) => new Vector2UI(size.W * 2, size.H * 2);

                t.Size = new Vector2UI(10, 10);

                Test.CheckResult(t.Size.W, 20u, "W modified");
                Test.CheckResult(t.Size.H, 20u, "H modified");
            });

            Test.F("OnRect изменяет всё", () =>
            {
                var t = new TransformAlgorithm();

                t.OnRect += (self, rect) => new Rect2I(rect.X + 1, rect.Y + 2, rect.W + 3, rect.H + 4);
                
                t.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(t.Rect.X, 2, "X");
                Test.CheckResult(t.Rect.Y, 3, "Y");
                Test.CheckResult(t.Rect.W, 4u, "W");
                Test.CheckResult(t.Rect.H, 5u, "H");
            });

            Test.F("CallAnyway", () =>
            {
                var t = new TransformAlgorithm();

                int calls = 0;

                t.OnPosition += (self, pos) =>
                {
                    calls++;
                    return pos;
                };

                t.Position = new Vector2I(1, 1);
                t.Position = new Vector2I(1, 1);

                Test.CheckResult(calls, 1, "No CallAnyway");

                t.CallAnyway = true;
                t.Position = new Vector2I(1, 1);

                Test.CheckResult(calls, 2, "CallAnyway works");
            });

            Test.F("WorldTransform Local -> World", () =>
            {
                var obj = new TestObject();
                var wt = new WorldTransformAlgorithm<TestObject>(obj);

                wt.OnParentTransform += (parent, self, rect) =>
                    new Rect2I(rect.X + 10, rect.Y + 10, rect.W, rect.H);

                var parent = new TestObject();
                parent.Node.Add(obj);

                wt.Local.Rect = new Rect2I(5, 5, 10, 10);

                Test.CheckResult(wt.World.Rect.X, 15, "World X");
                Test.CheckResult(wt.World.Rect.Y, 15, "World Y");
            });

            Test.F("WorldTransform World -> Local", () =>
            {
                var obj = new TestObject();
                var wt = new WorldTransformAlgorithm<TestObject>(obj);

                wt.OnParentTransformReverse += (parent, self, rect) =>
                    new Rect2I(rect.X - 10, rect.Y - 10, rect.W, rect.H);

                var parent = new TestObject();
                parent.Node.Add(obj);

                wt.World.Rect = new Rect2I(20, 20, 10, 10);

                Test.CheckResult(wt.Local.Rect.X, 10, "Local X");
                Test.CheckResult(wt.Local.Rect.Y, 10, "Local Y");
            });

            Test.F("Recalculate", () =>
            {
                var obj = new TestObject();
                var wt = new WorldTransformAlgorithm<TestObject>(obj);

                wt.Local.Rect = new Rect2I(1, 2, 3, 4);

                wt.Recalculate(true);

                Test.CheckResult(wt.World.Rect.X, 1, "Recalc X");
                Test.CheckResult(wt.World.Rect.Y, 2, "Recalc Y");
            });

            Test.F("Обновление детей", () =>
            {
                var root = new TestObject();
                var child = root.Node.Add(new TestObject());

                var wt = new WorldTransformAlgorithm<TestObject>(root);

                wt.Local.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(child.Self.Updates > 0, true, "Child updated");
            });

            Test.F("Защита от зацикливания", () =>
            {
                var obj = new TestObject();
                var wt = new WorldTransformAlgorithm<TestObject>(obj);

                int calls = 0;

                wt.Local.OnRect += (t, r) =>
                {
                    calls++;
                    return r;
                };

                wt.Local.Rect = new Rect2I(1, 1, 1, 1);

                // если Sync сломан → будет бесконечность
                Test.CheckResult(calls, 1, "No recursion");
            });
            
            Test.F("Порядок вызова событий", () =>
            {
                var t = new TransformAlgorithm();

                List<string> order = new();

                t.OnPosition += (self, pos) => {
                    order.Add("Position");
                    return pos;
                };

                t.OnSize += (self, size) => {
                    order.Add("Size");
                    return size;
                };

                t.OnRect += (self, rect) => {
                    order.Add("Rect");
                    return rect;
                };

                t.Rect = new Rect2I(1, 2, 3, 4);

                Test.CheckResult(string.Join(",", order), "Position,Size,Rect", "Event order");
            });
            
            Test.F("Каскадное изменение (OnPosition влияет на OnRect)", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, pos) => new Vector2I(pos.X + 10, pos.Y + 10);

                t.Rect = new Rect2I(0, 0, 5, 5);

                Test.CheckResult(t.Rect.X, 10, "Cascade X");
                Test.CheckResult(t.Rect.Y, 10, "Cascade Y");
            });
            
            Test.F("OnRect перекрывает OnPosition и OnSize", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, pos) => new Vector2I(100, 100);
                t.OnSize += (self, size) => new Vector2UI(200, 200);

                t.OnRect += (self, rect) => new Rect2I(1, 2, 3, 4);

                t.Rect = new Rect2I(0, 0, 0, 0);

                Test.CheckResult(t.Rect.X, 1, "Rect override X");
                Test.CheckResult(t.Rect.Y, 2, "Rect override Y");
                Test.CheckResult(t.Rect.W, 3u, "Rect override W");
                Test.CheckResult(t.Rect.H, 4u, "Rect override H");
            });
            
            Test.F("Множественные подписчики (цепочка)", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, pos) => new Vector2I(pos.X + 1, pos.Y + 1);
                t.OnPosition += (self, pos) => new Vector2I(pos.X * 2, pos.Y * 2);

                t.Position = new Vector2I(1, 1);

                // (1+1)=2 → (2*2)=4
                Test.CheckResult(t.Position.X, 4, "Chain X");
                Test.CheckResult(t.Position.Y, 4, "Chain Y");
            });
            
            Test.F("Изменение внутри события (reentrancy)", () =>
            {
                var t = new TransformAlgorithm();

                int calls = 0;

                t.OnPosition += (self, pos) =>
                {
                    calls++;

                    if(calls == 1){
                        self.X = pos.X + 1; // триггер внутри
                    }

                    return pos;
                };

                t.X = 5;

                Test.CheckResult(calls > 1, true, "Reentrancy happened");
            });
            
            Test.F("Согласованность Rect и полей", () =>
            {
                var t = new TransformAlgorithm();

                t.Rect = new Rect2I(10, 20, 30, 40);

                Test.CheckResult(t.X, 10, "X sync");
                Test.CheckResult(t.Y, 20, "Y sync");
                Test.CheckResult(t.W, 30u, "W sync");
                Test.CheckResult(t.H, 40u, "H sync");

                t.X = 5;

                Test.CheckResult(t.Rect.X, 5, "Rect sync X");
            });
            
            Test.F("CallAnyway + события", () =>
            {
                var t = new TransformAlgorithm();
                int calls = 0;

                t.OnRect += (self, rect) =>
                {
                    calls++;
                    return rect;
                };

                t.Rect = new Rect2I(1,1,1,1);
                t.Rect = new Rect2I(1,1,1,1);

                Test.CheckResult(calls, 1, "No CallAnyway Rect");

                t.CallAnyway = true;
                t.Rect = new Rect2I(1,1,1,1);

                Test.CheckResult(calls, 2, "CallAnyway Rect");
            });
            
            Test.F("WorldTransform цепочка родителей", () =>
            {
                var root = new TestObject();
                var child = root.Node.Add(new TestObject());
                var sub = child.Add(new TestObject());

                var wt = new WorldTransformAlgorithm<TestObject>(sub.Self);

                wt.OnParentTransform += (parent, self, rect) =>
                    new Rect2I(rect.X + 1, rect.Y + 1, rect.W, rect.H);

                wt.Local.Rect = new Rect2I(0, 0, 1, 1);

                // 2 родителя → +2
                Test.CheckResult(wt.World.Rect.X, 2, "Chain parent X");
                Test.CheckResult(wt.World.Rect.Y, 2, "Chain parent Y");
            });
        });
    }
}