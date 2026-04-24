using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.Web.WebView2.Core;
using WLO.Vector;

namespace WLO;

public class Browser : Metadata{
    public Browser(string Name = "?", object? Parent = null) : base(Name, Parent){
        try{
            __Environment = __WaitForTask(CoreWebView2Environment.CreateAsync());
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при создании браузера [{this}]!", e);
        }
    }

    public void ConnectToWindow(IntPtr HWND){
        try{
            if(__Controller != null){ throw new Exception("[WIP] Браузер уже присоединённый!"); }
            
            __Controller = __WaitForTask(__Environment!.CreateCoreWebView2ControllerAsync(HWND));
            __Core = __Controller.CoreWebView2;
            
            __UpdateBounds();
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при присоединении браузера [{this}] к окну [{HWND}]!", e);
        }
    }

    // ----------------------------------------------------------------------
    
    private Vector2UI __Bounds;
    public Vector2UI Bounds{
        get => __Bounds;
        set{
            if(__Bounds == value){ return; } __Bounds = value;
            __UpdateBounds();
        }
    }

    public void GoTo(string URL){
        try{
            __CheckCore();
            __Core!.Navigate(URL);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при открытии ссылки в браузере [{this}]!\nСсылка: \"{URL}\"", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private readonly CoreWebView2Environment? __Environment;
    private          CoreWebView2Controller?  __Controller;
    private          CoreWebView2?            __Core;
    
    private T __WaitForTask<T>(Task<T> Task, int TimeoutMS = 30000){
        TaskAwaiter<T> Awaiter = Task.GetAwaiter();
        DateTime StartTime = DateTime.UtcNow;
    
        while(!Awaiter.IsCompleted){
            if((DateTime.UtcNow - StartTime).TotalMilliseconds > TimeoutMS){ throw new TimeoutException($"Операция не завершилась за [{TimeoutMS}ms] в браузере [{this}]!"); }
        
            Window.UpdateWindows();
            Thread.Sleep(1);
        }
        return Task.Result;
    }
    
    private void __CheckCore(){ if(__Core == null){ throw new Exception("Браузер не присоеденён к окну! Отсутствует Core!"); } }

    private void __UpdateBounds(){
        if(__Controller != null){
            __Controller.Bounds = new Rectangle(0, 0, (int)__Bounds.W, (int)__Bounds.H);
            __Controller.NotifyParentWindowPositionChanged();
        }
    }
}