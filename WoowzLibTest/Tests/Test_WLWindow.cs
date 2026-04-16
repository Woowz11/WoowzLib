namespace WoowzLibTest.Tests;

/// <summary>
/// Тест WoowzLib окон
/// </summary>
public static class Test_WLWindow{
    public static void Run(){
        Test.Run("WLWindow", () => {
           Test.F("Create / Destroy", () => {
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
    }
}