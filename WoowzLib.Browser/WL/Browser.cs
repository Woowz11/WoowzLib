using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Web.WebView2.Core;

namespace WL;

public static class Browser{
    public static async Task Init(IntPtr hwnd)
    {
        var swTotal = Stopwatch.StartNew();

        try
        {
            Logger.Info("[WV2] INIT START");
            Logger.Info($"[WV2] HWND = 0x{hwnd.ToInt64():X}");

            Dump(hwnd);
            
            // ===== CRITICAL PRECHECK =====
            Logger.Info("[WV2] HOST PRECHECK");

            if (!IsWindow(hwnd))
            {
                throw new Exception("HWND is invalid");
            }

            if (IsHungAppWindow(hwnd))
            {
                throw new Exception("HWND is HUNG (message loop broken)");
            }

            int style = GetWindowLong(hwnd, GWL_STYLE);

            if ((style & 0x10000000) == 0) // WS_VISIBLE
            {
                Logger.Warn("HWND is NOT visible (WebView2 may stall)");
            }

            Logger.Info("[WV2] PRECHECK OK");
            
            LogThreadState("ENTRY");

            // 1. ENV CREATE
            var swEnv = Stopwatch.StartNew();
            Logger.Info("[WV2] Creating CoreWebView2Environment...");

            CoreWebView2Environment env = null;

            try
            {
                env = await CoreWebView2Environment.CreateAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("[WV2] ENV CREATE FAILED", ex);
                throw;
            }

            swEnv.Stop();
            Logger.Info($"[WV2] ENV OK in {swEnv.ElapsedMilliseconds}ms");

            LogThreadState("AFTER_ENV");

            // 2. CONTROLLER CREATE
            var swCtrl = Stopwatch.StartNew();
            Logger.Info("[WV2] Creating Controller...");

            CoreWebView2Controller controller = null;
            
            try
            {
                controller = await env.CreateCoreWebView2ControllerAsync(hwnd);
            }
            catch (Exception ex)
            {
                Logger.Error("[WV2] CONTROLLER CREATE FAILED", ex);

                Logger.Info("[WV2] Possible causes:");
                Logger.Info(" - invalid HWND (destroyed or wrong thread)");
                Logger.Info(" - missing message loop (PeekMessage/DispatchMessage not running)");
                Logger.Info(" - COM apartment mismatch (STA vs MTA)");
                Logger.Info(" - WebView2 runtime not installed");

                throw;
            }

            swCtrl.Stop();
            Logger.Info($"[WV2] CONTROLLER OK in {swCtrl.ElapsedMilliseconds}ms");

            if (controller == null)
            {
                Logger.Error("[WV2] controller == null (unexpected)");
                return;
            }

            // 3. CORE CHECK
            var core = controller.CoreWebView2;

            if (core == null)
            {
                Logger.Error("[WV2] CoreWebView2 == null");
                Logger.Info("Runtime not ready or initialization incomplete");
                return;
            }

            Logger.Info("[WV2] CORE OK");

            // 4. EVENTS
            core.NavigationStarting += (_, __) =>
                Logger.Info("[WV2] NAV START");

            core.ContentLoading += (_, __) =>
                Logger.Info("[WV2] CONTENT LOADING");

            core.NavigationCompleted += (_, e) =>
                Logger.Info($"[WV2] NAV COMPLETE success={e.IsSuccess} error={e.WebErrorStatus}");

            core.ProcessFailed += (_, e) =>
                Logger.Error($"[WV2] PROCESS FAILED: {e.ProcessFailedKind}");

            // 5. BOUNDS CHECK
            controller.Bounds = new Rectangle(0, 0, 800, 600);
            Logger.Info("[WV2] Bounds set 800x600");

            // 6. NAVIGATE
            Logger.Info("[WV2] Navigate start");
            core.Navigate("https://woowz11.github.io/woowzsite/quare.html");

            core.OpenDevToolsWindow();
            
            swTotal.Stop();
            Logger.Info($"[WV2] INIT DONE in {swTotal.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            Logger.Error("[WV2] FATAL INIT ERROR", ex);
        }
    }

    private static void LogThreadState(string stage)
    {
        var t = Thread.CurrentThread;

        Logger.Info($"[WV2] THREAD [{stage}]");
        Logger.Info($"   ManagedId = {t.ManagedThreadId}");
        Logger.Info($"   Name      = {t.Name}");
        Logger.Info($"   IsSTA     = {t.GetApartmentState()}");
        Logger.Info($"   IsPool    = {t.IsThreadPoolThread}");
    }
    
    [DllImport("user32.dll")]
    static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    // =========================
    // USER32
    // =========================
    [DllImport("user32.dll")]
    static extern bool IsHungAppWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    // =========================
    // GDI32
    // =========================
    [DllImport("gdi32.dll")]
    static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    // =========================
    // KERNEL32
    // =========================
    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();
    
    const int GWL_STYLE   = -16;
    const int GWL_EXSTYLE = -20;

    public static void Dump(IntPtr hwnd)
    {
        Logger.Info("========== HWND DIAGNOSTIC ==========");

        Logger.Info($"IsWindow = {IsWindow(hwnd)}");
        Logger.Info($"IsVisible = {IsWindowVisible(hwnd)}");

        uint pid;
        var tid = GetWindowThreadProcessId(hwnd, out pid);

        Logger.Info($"ThreadId = {tid}");
        Logger.Info($"ProcessId = {pid}");

        Logger.Info($"Foreground == hwnd ? {GetForegroundWindow() == hwnd}");

        int style = GetWindowLong(hwnd, GWL_STYLE);
        int exstyle = GetWindowLong(hwnd, GWL_EXSTYLE);

        Logger.Info($"STYLE = 0x{style:X}");
        Logger.Info($"EXSTYLE = 0x{exstyle:X}");

        Logger.Info($"WS_CHILD = {(style & 0x40000000) != 0}");
        Logger.Info($"WS_POPUP = {(style & unchecked((int)0x80000000)) != 0}");
        Logger.Info($"WS_VISIBLE = {(style & 0x10000000) != 0}");

        // ===== ДОПОЛНИТЕЛЬНЫЕ КРИТИЧНЫЕ ПРОВЕРКИ =====

        Logger.Info("---- WebView2 HOST READINESS ----");

        // 1. Window class name
        try
        {
            var sb = new StringBuilder(256);
            GetClassName(hwnd, sb, sb.Capacity);
            Logger.Info($"ClassName = {sb}");
        }
        catch (Exception ex)
        {
            Logger.Error("GetClassName failed", ex);
        }

        // 2. Parent check
        IntPtr parent = GetParent(hwnd);
        Logger.Info($"Parent = 0x{parent.ToInt64():X}");

        // 3. Thread mismatch risk
        Logger.Info($"SameThreadAsCurrent = {tid == GetCurrentThreadId()}");

        // 4. Window state flags
        Logger.Info($"IsHung = {IsHungAppWindow(hwnd)}");

        // 5. DPI (важно для compositor init)
        try
        {
            IntPtr hdc = GetDC(hwnd);
            int dpi = GetDeviceCaps(hdc, 88);
            ReleaseDC(hwnd, hdc);

            Logger.Info($"DPI = {dpi}");
        }
        catch { }

        Logger.Info("=====================================");
    }
}