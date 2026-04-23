using WLO;

namespace WL;

public static class Browser{
    public static Task<WoowzLib.Browser.WLO.Browser> AttachToWindow(Window Window){
        try{
            return WoowzLib.Browser.WLO.Browser.CreateAsync(Window.Handle);
        }catch(Exception e){
            throw new Exception($"Произошла ошибка при присоединении браузера окну [{Window}]!", e);
        }
    }
}