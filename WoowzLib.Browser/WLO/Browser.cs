using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Web.WebView2.Core;
using WLO.Vector;

namespace WLO;

public class Browser : Metadata, IDisposable{
    public Browser(string Name = "?") : base(Name){
        try{
            __Environment = __WaitForTask(CoreWebView2Environment.CreateAsync());
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при создании браузера [{this}]!", e);
        }
    }

    public void ConnectToWindow(IntPtr Handle){
        try{
            if(this.Handle != null || __Controller != null){ throw new Exception("[WIP] Браузер уже присоединённый!"); }

            this.Handle = Handle;
            
            __Controller = __WaitForTask(__Environment!.CreateCoreWebView2ControllerAsync(Handle));
            __Core = __Controller.CoreWebView2;
            
            __UpdateBounds();
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при присоединении браузера [{this}] к окну [{Handle}]!", e);
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

    /// <summary>
    /// Текущий URL браузера, если кастомный сайт, то вернёт "about:blank"
    /// </summary>
    public string URL{
        get{
            try{
                return __CheckCore().Source;
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при получении URL из браузера [{this}]!", e);
            }
        }
        set{
            try{
                WL.String.URL.Validate(value);
                __CheckCore().Navigate(value);
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при установке URL в браузере [{this}]!\nURL: \"{value}\"", e);
            }
        }
    }

    /// <summary>
    /// Перезагружает страницу браузера
    /// </summary>
    public Browser Reload(){
        try{
            __CheckCore().Reload();
            return this;
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при обновлении страницы браузера [{this}]!", e);
        }
    }

    /// <summary>
    /// HTML текущей страницы, при установке URL будет равен "about:blank"
    /// </summary>
    /// <exception cref="Exception"></exception>
    public string HTML{
        get{
            try{
                return ExecuteScript<string>("document.documentElement.outerHTML") ?? "";
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при получении HTML из браузера [{this}]!", e);
            }
        }
        set{
            try{
                __CheckCore().NavigateToString(value);
            }catch(Exception e){
                throw new Exception($"Произошла ошибка при установке HTML в браузере [{this}]!\nHTML:\n{value}", e);
            }
        }
    }
    
    // ----------------------------------------------------------------------

    public T? ExecuteScript<T>(string JSScript){
        try{
            string Result = __WaitForTask(__CheckCore().ExecuteScriptAsync(JSScript));
            return __ParseJSONResult<T>(Result);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при вызове скрипта у браузера [{this}]!\nСкрипт:\n{JSScript}", e);
        }
    }

    public async Task<T?> ExecuteScriptAsync<T>(string JSScript){
        try{
            string Result = await __CheckCore().ExecuteScriptAsync(JSScript);
            return __ParseJSONResult<T>(Result);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при вызове async скрипта у браузера [{this}]!\nСкрипт:\n{JSScript}", e);
        }
    }
    
    // ----------------------------------------------------------------------
    
    private readonly CoreWebView2Environment? __Environment;
    private          CoreWebView2Controller?  __Controller;
    private          CoreWebView2?            __Core;

    /// <summary>
    /// Окно к которому привязан браузер
    /// </summary>
    private IntPtr? Handle;
    
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
    
    private CoreWebView2 __CheckCore(){ if(__Core == null){ throw new Exception("Браузер не присоединён к окну! Отсутствует Core!"); } return __Core!; }

    private void __UpdateBounds(){
        if(__Controller != null){
            __Controller.Bounds = new Rectangle(0, 0, (int)__Bounds.W, (int)__Bounds.H);
            __Controller.NotifyParentWindowPositionChanged();
        }
    }

    private T? __ParseJSONResult<T>(string JSONResult){
        try{
            if(WL.String.IsEmpty(JSONResult) || JSONResult == "null"){ return default!; }

            if(typeof(T) == typeof(string)){
                if(WL.String.AtLeftAndRight(JSONResult, "\"")){
                    return (T)(object)(WL.String.Unescape(WL.String.Sub(JSONResult, 1, JSONResult.Length - 2)));
                }

                return (T)(object)JSONResult;
            }

            if(typeof(T) == typeof(int)){
                return (T)(object)int.Parse(JSONResult);
            }
            
            if(typeof(T) == typeof(double)){
                return (T)(object)double.Parse(JSONResult);
            }
            
            if(typeof(T) == typeof(bool)){
                return (T)(object)bool.Parse(WL.String.LowerCase(JSONResult));
            }

            try{
                return JsonSerializer.Deserialize<T>(JSONResult);
            }catch{
                Logger.Warn($"Не получилось конвертировать результат JSON типа [{typeof(T).Name}] у браузера [{this}]!");
                return default!;
            }
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при парсинге результата JSON в браузере [{this}]!\nJSON результат:\n{JSONResult}", e);
        }
    }
    
    public void Dispose(){
        __Controller?.Close();
    }
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"Browser({ToShortString()})";

    public string ToShortString() => $"{ToMetadataString()}, \"{URL}\", {WL.String.ToString(Handle)}";
}