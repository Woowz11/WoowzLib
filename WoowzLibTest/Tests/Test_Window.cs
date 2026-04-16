namespace WoowzLibTest.Tests;

/// <summary>
/// Тест WINAPI окон
/// </summary>
public static class Test_Window{
    public static void Run(){
        Test.Run("Window", () => {
            Test.F("Create / Destroy", () => {
                var wc = new WLO.WindowClass("TestWindowClass");

                var win = new WLO.Window(wc, new WLO.Window.Constructor{
                    Title = "Test",
                    Size = new WLO.Vector.Vector2UI(300, 200),
                    Visible = false
                });

                // Проверка живости
                Test.CheckResult(win.Alive, true, "Окно не создано!");

                // Проверка регистрации
                Test.CheckResult(WLO.Window.Windows.ContainsKey(win.ID), true, "Окно не в словаре!");

                // Destroy
                win.Destroy();
                WLO.Window.UpdateWindows();

                Test.CheckResult(win.Alive, false, "Окно не уничтожено!");
                Test.CheckResult(WLO.Window.Windows.ContainsKey(win.ID), false, "Окно не удалено из словаря!");
            });
            
            Test.F("Title", () => {
                var wc = new WLO.WindowClass("TestWindowClass_Title");

                var win = new WLO.Window(wc);

                win.Title = "Hello";
                Test.CheckResult(win.Title, "Hello", "Title не работает!");

                win.Destroy();
                WLO.Window.UpdateWindows();
            });

            Test.F("Position / Size", () => {
                var wc = new WLO.WindowClass("TestWindowClass_Pos");

                var win = new WLO.Window(wc, new WLO.Window.Constructor{
                    Visible = false
                });

                var pos = new WLO.Vector.Vector2I(100, 100);
                win.Position = pos;

                Test.CheckResult(win.Position, pos, "Position не работает!");

                var size = new WLO.Vector.Vector2UI(400, 300);
                win.Size = size;

                Test.CheckResult(win.Size, size, "Size не работает!");

                win.Destroy();
                WLO.Window.UpdateWindows();
            });

            Test.F("Visibility", () => {
                var wc = new WLO.WindowClass("TestWindowClass_Visible");

                var win = new WLO.Window(wc, new WLO.Window.Constructor{
                    Visible = false
                });

                Test.CheckResult(win.Visible, false, "Visible false сломан!");

                win.Visible = true;
                Test.CheckResult(win.Visible, true, "Visible true сломан!");

                win.Destroy();
                WLO.Window.UpdateWindows();
            });

            Test.F("Alpha", () => {
                var wc = new WLO.WindowClass("TestWindowClass_Alpha");

                var win = new WLO.Window(wc);

                win.Alpha = 128;
                Test.CheckResult(win.Alpha, (byte)128, "Alpha не работает!");

                win.Alpha = 255;
                Test.CheckResult(win.Alpha, (byte)255, "Alpha reset не работает!");

                win.Destroy();
                WLO.Window.UpdateWindows();
            });

            Test.F("Events", () => {
                bool called = false;

                var wc = new WLO.WindowClass("TestWindowClass_Event", (w, msg, wp, lp) => {
                    called = true;
                    return null;
                });

                var win = new WLO.Window(wc);

                // Принудительно прокачиваем message loop
                WLO.Window.UpdateWindows();

                // ❗ Это слабый тест — не гарантирует приход сообщений
                Test.CheckResult(called, true, "Event не вызвался (может быть нестабильно!)");

                win.Destroy();
                WLO.Window.UpdateWindows();
            });
        });
    }
}