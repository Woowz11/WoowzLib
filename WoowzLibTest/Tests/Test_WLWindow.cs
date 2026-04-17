using WLO;
using WLO.Rect;
using WLO.Vector;
using WLO.WLElement;

namespace WoowzLibTest.Tests;

/// <summary>
/// Тест WoowzLib окон
/// </summary>
public static class Test_WLWindow{
    
    private class TestElement : WLElement{
        public override void Render(WLWindow Window, IntPtr HDC) { }
    }
    
    public static void Run(){
        Test.Run("WLWindow", () => {
           Test.F("Create/Destroy", () => {
                var win = new WLO.WLWindow(new WLO.WLWindow.Constructor{
                    Title = "Test"
                });

                Test.CheckResult(win.Alive, true, "Окно не живое после создания!");
                Test.CheckResult(WLO.WLWindow.Windows.Contains(win), true, "Окно не в списке!");

                win.Destroy();
                WLO.WLWindow.UpdateWindows();

                Test.CheckResult(win.Died, true, "Окно не уничтожилось!");
                Test.CheckResult(WLO.WLWindow.Windows.Contains(win), false, "Окно не удалилось из списка!");
            });

            // -------------------------

            Test.F("Title", () => {
                var win = new WLO.WLWindow();

                win.Title = "Hello";
                Test.CheckResult(win.Title, "Hello", "Title не установился!");

                // Проверка interception
                win.OnTitle += (w, t) => "Intercepted";

                win.Title = "New";
                Test.CheckResult(win.Title, "Intercepted", "OnTitle не перехватывает!");

                win.Destroy();
                WLO.WLWindow.UpdateWindows();
            });

            // -------------------------

            Test.F("OnRect interception", () => {
                var win = new WLO.WLWindow();

                win.OnRect += (w, rect) => {
                    // форсим фиксированный размер
                    return new WLO.Rect.Rect2I(rect.X, rect.Y, 500, 500);
                };

                win.Original.Size = new WLO.Vector.Vector2UI(300, 300);

                WLO.WLWindow.UpdateWindows();

                var size = win.Size;

                // ❗ важно: это зависит от WM_WINDOWPOSCHANGING
                Test.CheckResult(size.W == 500 && size.H == 500, true, "OnRect не перехватил размер!");

                win.Destroy();
                WLO.WLWindow.UpdateWindows();
            });

            // -------------------------

            Test.F("Render (basic)", () => {
                var win = new WLO.WLWindow();

                bool renderCalled = false;
                bool postCalled = false;

                win.OnRender += (w, hdc, size) => {
                    renderCalled = true;
                };

                win.OnPostRender += (w, hdc, size) => {
                    postCalled = true;
                };

                win.Render();

                Test.CheckResult(renderCalled, true, "OnRender не вызвался!");
                Test.CheckResult(postCalled, true, "OnPostRender не вызвался!");

                win.Destroy();
                WLO.WLWindow.UpdateWindows();
            });

            // -------------------------

            Test.F("Double buffer resize", () => {
                var win = new WLO.WLWindow();

                win.Render(); // инициализация буфера

                var oldSize = win.ClientSize;

                win.Original.Size = new WLO.Vector.Vector2UI(oldSize.W + 100, oldSize.H + 100);

                WLO.WLWindow.UpdateWindows();

                // просто проверка, что не упало
                win.Render();

                Test.CheckResult(true, true, "Resize вызвал краш!");

                win.Destroy();
                WLO.WLWindow.UpdateWindows();
            });
        });
        
        Test.Run("WLElement", () => {
            // -------------------------
            // 1. Базовый Local -> World
            // -------------------------
            Test.F("Local -> World базовый (CSS layout)", () =>
            {
                var root = new TestElement();
                var child = root.Node.Add(new TestElement());

                root.Transform.Local.Rect = new Rect2I(0, 0, 100, 100);
                child.Self.Transform.Local.Rect = new Rect2I(10, 10, 20, 20);

                var wt = child.Self.Transform;

                Test.CheckResult(wt.World.Rect.X, 10, "X");
                Test.CheckResult(wt.World.Rect.Y, 10, "Y");
                Test.CheckResult(wt.World.Rect.W, 20u, "W");
                Test.CheckResult(wt.World.Rect.H, 20u, "H");
            });
            
            Test.F("Anchor влияет на позицию (CSS alignment)", () =>
            {
                var root = new TestElement();
                var child = root.Node.Add(new TestElement());

                root.Transform.Local.Rect = new Rect2I(0, 0, 100, 100);

                var t = child.Self.Transform;

                t.Anchor = new Vector2I(0, 0); // центр (-1,-1) в твоей нормализации = центр
                t.Local.Rect = new Rect2I(0, 0, 20, 20);

                // центр родителя (100x100) = (50,50)
                // центр элемента = (10,10)
                Test.CheckResult(t.World.Rect.X, 40, "Anchor X");
                Test.CheckResult(t.World.Rect.Y, 40, "Anchor Y");
            });
            
            Test.F("PixelOffset (absolute shift)", () =>
            {
                var root = new TestElement();
                var child = root.Node.Add(new TestElement());

                root.Transform.Local.Rect = new Rect2I(0, 0, 100, 100);

                var t = child.Self.Transform;

                t.PixelOffset = new Vector2I(5, 7);
                t.Local.Rect = new Rect2I(0, 0, 10, 10);

                Test.CheckResult(t.World.Rect.X, 5, "Offset X");
                Test.CheckResult(t.World.Rect.Y, 7, "Offset Y");
            });
            
            Test.F("Scale влияет на размер (visual layer)", () =>
            {
                var root = new TestElement();
                var child = root.Node.Add(new TestElement());

                var t = child.Self.Transform;

                t.Scale = new Vector2D(2, 2);
                t.Local.Rect = new Rect2I(0, 0, 10, 10);

                Test.CheckResult(t.World.Rect.W, 20u, "Scaled W");
                Test.CheckResult(t.World.Rect.H, 20u, "Scaled H");
            });
            
            Test.F("Min/Max size clamp (content box)", () =>
            {
                var el = new TestElement();
                var t = el.Transform;

                t.MinSize = new Vector2UI(50, 50);
                t.MaxSize = new Vector2UI(100, 100);

                t.Local.Rect = new Rect2I(0, 0, 10, 200);

                Test.CheckResult(t.World.Rect.W, 50u, "Min clamp W");
                Test.CheckResult(t.World.Rect.H, 100u, "Max clamp H");
            });
            
            Test.F("Margin увеличивает внешний размер (box model)", () =>
            {
                var el = new TestElement();
                var t = el.Transform;

                t.Margin = new Vector4I(1, 2, 3, 4); // L T R B
                t.Local.Rect = new Rect2I(0, 0, 10, 10);

                // 10 + 1 + 3 = 14
                Test.CheckResult(t.World.Rect.W, 14u, "Margin W");

                // 10 + 2 + 4 = 16
                Test.CheckResult(t.World.Rect.H, 16u, "Margin H");
            });
            
            Test.F("Offset (relative parent space)", () =>
            {
                var root = new TestElement();
                var child = root.Node.Add(new TestElement());

                root.Transform.Local.Rect = new Rect2I(0, 0, 100, 100);

                var t = child.Self.Transform;

                t.Offset = new Vector2D(0.5, 0.5); // центр родителя
                t.Local.Rect = new Rect2I(0, 0, 10, 10);

                Test.CheckResult(t.World.Rect.X, 50, "Offset X");
                Test.CheckResult(t.World.Rect.Y, 50, "Offset Y");
            });
            
            /*Test.F("Inverse transform (approx, non-deterministic)", () =>
            {
                var el = new TestElement();
                var t = el.Transform;

                t.Local.Rect = new Rect2I(10, 10, 20, 20);

                var world = t.World.Rect;
                t.World.Rect = world;

                Test.CheckResult(
                    Math.Abs(t.Local.Rect.X - 10) <= 1,
                    true,
                    "Reverse X approx"
                );
            });*/
            
            Test.F("Chain transform (parent accumulation)", () =>
            {
                var root = new TestElement();
                var mid = root.Node.Add(new TestElement());
                var child = mid.Add(new TestElement());

                root.Transform.Local.Rect = new Rect2I(10, 10, 100, 100);
                mid.Self.Transform.Local.Rect = new Rect2I(5, 5, 50, 50);
                child.Self.Transform.Local.Rect = new Rect2I(2, 2, 10, 10);

                var t = child.Self.Transform;

                Test.CheckResult(t.World.Rect.X, 17, "Chain X");
                Test.CheckResult(t.World.Rect.Y, 17, "Chain Y");
            });
        });
    }
}