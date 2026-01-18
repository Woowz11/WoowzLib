namespace WL;

[WLModule(-100, 2)]
public class Window{
    public static readonly List<WLO.Window> Windows = [];
    
    /// <summary>
    /// Обновляет окна
    /// </summary>
    public static void Update(){
        try{
            foreach(WLO.Window W in Windows.ToArray()){
                if(W.ShouldDestroy){ W.DestroyNow(); }
            }
            
            while(System.Native.Windows.PeekMessage(out System.Native.Windows.MSG Message, IntPtr.Zero, 0, 0, System.Native.Windows.PM_REMOVE)){
                System.Native.Windows.TranslateMessage(ref Message);
                System.Native.Windows.DispatchMessage (ref Message);
            }   
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении всех окон!", e);
        }
    }
}