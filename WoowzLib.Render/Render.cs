using WLO;

namespace WL;

[WLModule(-50, 1)]
public class Render{
    static Render(){
        try{
            WL.WoowzLib.OnStop += () => {
                try{
                    if(DLL == IntPtr.Zero){ return; }
                    System.Native.Unload(DLL); DLL = IntPtr.Zero;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при остановке рендера!", e);
                }
            };
            
            DLL = System.Native.Load(WL.Explorer.Resources.Load("WoowzLib.vulkan-1.dll", typeof(WL.Render).Assembly).Path);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации рендера!", e);
        }
    }
    
    /// <summary>
    /// Ссылка на vulkan-1.dll
    /// </summary>
    private static IntPtr DLL = IntPtr.Zero;
}