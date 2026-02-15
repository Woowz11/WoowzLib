namespace WL;

[WLModule(-100, 36)]
public class Window{
    static Window(){
        WL.WoowzLib.OnUpdate += () => {
            try{
                foreach(WLO.Window W in Windows.ToArray()){
                    W.__Update();
                }
            }catch(Exception e){
                throw new Exception("Произошла ошибка при обновлении всех окон!", e);
            }
        };
    }
    
    /// <summary>
    /// Созданные окна
    /// </summary>
    public static readonly List<WLO.Window> Windows = [];
    
    /// <summary>
    /// Версия модуля
    /// </summary>
    public static string Version => WL.System.GetVersion(WL.WoowzLib.LoadedModules["Window"]);
}