using System.Drawing;
using Microsoft.Web.WebView2.Core;
using WLO.Vector;

namespace WoowzLib.Browser.WLO;

public class Browser{
    private Browser(CoreWebView2Environment Environment, CoreWebView2Controller Controller){
        __Environment = Environment;
        __Controller = Controller;

        __Core = __Controller.CoreWebView2;

        Bounds = new Vector2UI(800, 600);
        
        GoTo("https://woowz11.github.io/woowzsite/quare.html");
    }

    public static async Task<Browser> CreateAsync(IntPtr HWND){
        try{
            CoreWebView2Environment? Environment = await CoreWebView2Environment.CreateAsync();
            CoreWebView2Controller Controller = await Environment.CreateCoreWebView2ControllerAsync(HWND);
            
            return new Browser(Environment, Controller);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при создании браузера!", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private Vector2UI __Bounds;
    public Vector2UI Bounds{
        get => __Bounds;
        set{
            if(__Bounds == value){ return; } __Bounds = value;

            __Controller.Bounds = new Rectangle(0, 0, (int)value.W, (int)value.H);
        }
    }

    public void GoTo(string URL) => __Core.Navigate(URL);
    
    // ----------------------------------------------------------------------
    
    private CoreWebView2Environment __Environment;

    private CoreWebView2Controller __Controller;

    private CoreWebView2 __Core;
}

/*
using System.Drawing;
using Microsoft.Web.WebView2.Core;
using WLO.Vector;

namespace WoowzLib.Browser.WLO;

public class Browser{
    private Browser(){
        try{
            Task<CoreWebView2Environment>? TaskEnvironment = CoreWebView2Environment.CreateAsync();
            TaskEnvironment.Wait();
            __Environment = TaskEnvironment.Result;
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при создании браузера [{this}]!", e);
        }
    }

    public Task<CoreWebView2Controller> ConnectToWindow(IntPtr HWND) => __Environment.CreateCoreWebView2ControllerAsync(HWND);
    
    // ----------------------------------------------------------------------
    
    private Vector2UI __Bounds;
    public Vector2UI Bounds{
        get => __Bounds;
        set{
            if(__Bounds == value){ return; } __Bounds = value;

            __Controller.Bounds = new Rectangle(0, 0, (int)value.W, (int)value.H);
        }
    }

    public void GoTo(string URL) => __Core.Navigate(URL);
    
    // ----------------------------------------------------------------------
    
    private CoreWebView2Environment __Environment;

    private CoreWebView2Controller __Controller;

    private CoreWebView2 __Core;
}
*/