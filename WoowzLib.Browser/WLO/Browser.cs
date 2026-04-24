using System;
using System.Drawing;
using Microsoft.Web.WebView2.Core;
using WLO;
using WLO.Vector;

namespace WoowzLib.Browser.WLO
{
    public class Browser : Metadata
    {
        private CoreWebView2Environment __Environment;
        private CoreWebView2Controller __Controller;
        private CoreWebView2 __Core;
        private Vector2UI __Bounds;

        // Конструктор – ничего асинхронного, просто подготовка
        public Browser(string Name = "?", object? Parent = null) : base(Name, Parent) { }

        // Синхронный метод, который не блокирует поток (обрабатывает сообщения внутри)
        public void ConnectToWindow(IntPtr HWND)
        {
            if (__Controller != null) return;

            // 1. Создаём окружение синхронно (с обработкой сообщений)
            if (__Environment == null)
                __Environment = WaitForTask(CoreWebView2Environment.CreateAsync());

            // 2. Создаём контроллер синхронно (с обработкой сообщений)
            __Controller = WaitForTask(__Environment.CreateCoreWebView2ControllerAsync(HWND));
            __Core = __Controller.CoreWebView2;

            // 3. Применяем установленные ранее bounds
            if (__Bounds.W > 0 && __Bounds.H > 0)
            {
                __Controller.Bounds = new Rectangle(0, 0, (int)__Bounds.W, (int)__Bounds.H);
                __Controller.NotifyParentWindowPositionChanged();
            }
        }

        // Вспомогательный метод: ожидает завершения Task, не блокируя поток,
        // а обрабатывая сообщения Windows через ваш существующий WLWindow.UpdateWindows()
        private T WaitForTask<T>(Task<T> task)
        {
            var awaiter = task.GetAwaiter();
            while (!awaiter.IsCompleted)
            {
                // Обрабатываем все накопившиеся сообщения (ваш внешний message pump)
                Window.UpdateWindows();   // или Window.UpdateWindows() – как у вас названо
                // Небольшая пауза, чтобы не грузить CPU (можно убрать или оставить 1 мс)
                System.Threading.Thread.Sleep(1);
            }
            return task.Result;
        }

        public Vector2UI Bounds
        {
            get => __Bounds;
            set
            {
                __Bounds = value;
                if (__Controller != null)
                {
                    var rect = new Rectangle(0, 0, (int)value.W, (int)value.H);
                    __Controller.Bounds = rect;
                    __Controller.NotifyParentWindowPositionChanged();
                }
            }
        }

        public void GoTo(string URL)
        {
            if (__Core == null)
                throw new InvalidOperationException("Browser not connected to a window.");
            __Core.Navigate(URL);
        }
    }
}