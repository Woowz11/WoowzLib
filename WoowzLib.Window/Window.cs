namespace WL;

[WLModule(-100, 40)]
public class Window{
    static Window(){
        WL.WoowzLib.OnUpdate += () => {
            try{
                foreach(WLO.Window W in Windows.ToArray()){
                    W.__Update();
                }
                
                while(System.Native.Windows.PeekMessage(out System.Native.Windows.MSG Message, IntPtr.Zero, 0, 0, System.Native.Windows.PM_REMOVE)){
                    System.Native.Windows.TranslateMessage(ref Message);
                    System.Native.Windows.DispatchMessage (ref Message);
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