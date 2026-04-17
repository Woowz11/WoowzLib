using WLO;
using WLO.Rect;
using WLO.Vector;

namespace WoowzLibTest.Tests;

public static class Test_Transform
{
    private class TestObject : SceneObject<TestObject>, ITransform
    {
        public int Updates = 0;

        void ITransform.__UpdateTransform(object? Data)
        {
            Updates++;
        }
    }

    public static void Run()
    {
        Test.Run("Transform (FULL COVERAGE)", () =>
        {
            // =========================================================
            // 1. X/Y/W/H direct consistency
            // =========================================================

            Test.F("Primitive fields consistency", () =>
            {
                var t = new TransformAlgorithm();

                t.X = 10;
                t.Y = 20;
                t.W = 30;
                t.H = 40;

                var r = t.Rect;

                Test.CheckResult(r.X, 10, "X");
                Test.CheckResult(r.Y, 20, "Y");
                Test.CheckResult(r.W, 30u, "W");
                Test.CheckResult(r.H, 40u, "H");
            });

            // =========================================================
            // 2. Position alias correctness
            // =========================================================

            Test.F("Position alias sync", () =>
            {
                var t = new TransformAlgorithm();

                t.Position = new Vector2I(5, 7);

                Test.CheckResult(t.X, 5, "X");
                Test.CheckResult(t.Y, 7, "Y");

                var p = t.Position;
                Test.CheckResult(p.X, 5, "PX");
                Test.CheckResult(p.Y, 7, "PY");
            });

            // =========================================================
            // 3. Size alias correctness
            // =========================================================

            Test.F("Size alias sync", () =>
            {
                var t = new TransformAlgorithm();

                t.Size = new Vector2UI(11, 22);

                Test.CheckResult(t.W, 11u, "W");
                Test.CheckResult(t.H, 22u, "H");
            });

            // =========================================================
            // 4. Rect full overwrite
            // =========================================================

            Test.F("Rect full overwrite", () =>
            {
                var t = new TransformAlgorithm();

                t.Rect = new Rect2I(1, 2, 3, 4);

                Test.CheckResult(t.X, 1, "X");
                Test.CheckResult(t.Y, 2, "Y");
                Test.CheckResult(t.W, 3u, "W");
                Test.CheckResult(t.H, 4u, "H");
            });

            // =========================================================
            // 5. OnPosition pipeline modification
            // =========================================================

            Test.F("OnPosition modifies state", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, pos) =>
                    new Vector2I(pos.X + 10, pos.Y + 10);

                t.Position = new Vector2I(1, 1);

                Test.CheckResult(t.X, 11, "X");
                Test.CheckResult(t.Y, 11, "Y");
            });

            // =========================================================
            // 6. OnSize pipeline modification
            // =========================================================

            Test.F("OnSize modifies state", () =>
            {
                var t = new TransformAlgorithm();

                t.OnSize += (self, size) =>
                    new Vector2UI(size.W * 2, size.H * 2);

                t.Size = new Vector2UI(2, 3);

                Test.CheckResult(t.W, 4u, "W");
                Test.CheckResult(t.H, 6u, "H");
            });

            // =========================================================
            // 7. OnRect override priority
            // =========================================================

            Test.F("OnRect overrides all", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, p) => new Vector2I(100, 100);
                t.OnSize += (self, s) => new Vector2UI(200, 200);

                t.OnRect += (self, r) => new Rect2I(9, 8, 7, 6);

                t.Rect = new Rect2I(0, 0, 0, 0);

                Test.CheckResult(t.Rect.X, 9, "X");
                Test.CheckResult(t.Rect.Y, 8, "Y");
                Test.CheckResult(t.Rect.W, 7u, "W");
                Test.CheckResult(t.Rect.H, 6u, "H");
            });

            // =========================================================
            // 8. Chain execution order
            // =========================================================

            Test.F("Multi OnPosition chain order", () =>
            {
                var t = new TransformAlgorithm();

                t.OnPosition += (self, p) => new Vector2I(p.X + 1, p.Y + 1);
                t.OnPosition += (self, p) => new Vector2I(p.X * 2, p.Y * 2);

                t.Position = new Vector2I(1, 1);

                Test.CheckResult(t.X, 4, "X");
                Test.CheckResult(t.Y, 4, "Y");
            });

            // =========================================================
            // 9. CallAnyway behavior
            // =========================================================

            Test.F("CallAnyway forces event", () =>
            {
                var t = new TransformAlgorithm();
                int calls = 0;

                t.OnRect += (self, r) =>
                {
                    calls++;
                    return r;
                };

                t.Rect = new Rect2I(1, 1, 1, 1);
                t.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(calls, 1, "no repeat");

                t.CallAnyway = true;
                t.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(calls, 2, "forced call");
            });

            // =========================================================
            // 10. Reentrancy safety
            // =========================================================

            Test.F("Reentrancy does not break state", () =>
            {
                var t = new TransformAlgorithm();

                int calls = 0;

                t.OnPosition += (self, p) =>
                {
                    calls++;

                    if (calls == 1)
                        self.X = p.X + 1;

                    return p;
                };

                t.X = 5;

                Test.CheckResult(calls >= 1, true, "executed");
            });

            // =========================================================
            // 11. Rect consistency after mutation
            // =========================================================

            Test.F("Rect always consistent with fields", () =>
            {
                var t = new TransformAlgorithm();

                t.X = 3;
                t.Y = 4;

                var r = t.Rect;

                Test.CheckResult(r.X, 3, "X");
                Test.CheckResult(r.Y, 4, "Y");

                t.Rect = new Rect2I(9, 9, 9, 9);

                Test.CheckResult(t.X, 9, "X sync");
                Test.CheckResult(t.Y, 9, "Y sync");
            });

            // =========================================================
            // 12. World transform propagation
            // =========================================================

            Test.F("World transform parent chain", () =>
            {
                var root = new TestObject();
                var child = root.Node.Add(new TestObject());
                var sub = child.Add(new TestObject());

                var wt = new WorldTransformAlgorithm<TestObject>(sub.Self);

                wt.OnParentTransform += (p, self, r) =>
                    new Rect2I(r.X + 1, r.Y + 1, r.W, r.H);

                wt.Local.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(wt.World.Rect.X, 3, "X");
                Test.CheckResult(wt.World.Rect.Y, 3, "Y");
            });

            // =========================================================
            // 13. Child update propagation
            // =========================================================

            Test.F("Child update propagation", () =>
            {
                var root = new TestObject();
                var child = root.Node.Add(new TestObject());

                var wt = new WorldTransformAlgorithm<TestObject>(root);

                wt.Local.Rect = new Rect2I(1, 1, 1, 1);

                Test.CheckResult(child.Self.Updates > 0, true, "updated");
            });

            // =========================================================
            // 14. No infinite recursion guard
            // =========================================================

            Test.F("No recursion crash", () =>
            {
                var t = new TransformAlgorithm();

                t.OnRect += (self, r) => r;

                for (int i = 0; i < 100; i++)
                {
                    t.Rect = new Rect2I(i, i, (uint)i, (uint)i);
                }

                Test.CheckResult(t.Rect.X, 99, "stable");
            });
        });
    }
}