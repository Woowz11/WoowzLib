using WL;
using WLO.Attribute;

namespace WLO;

/// <summary>
/// Состояние рисования (нужно очищать!)
/// </summary>
[RequireTesting(TestingInformation.WorkInProgress, "ну доделайте")]
public class DeviceContext{
    /// <summary>
    /// Device Context окна
    /// </summary>
    public DeviceContext(Window Window){
        try{
            Window.CheckAlive();

            Handle = WL.System.Draw.DeviceContextPointer(Window);
            
            this.Window = Window;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании DeviceContext через окно [" + Window + "]!", e);
        }
    }
    
    /// <summary>
    /// Ссылка на Device Context
    /// </summary>
    public IntPtr Handle{ get; internal set; }
    
    /// <summary>
    /// Привязанное окно
    /// </summary>
    public Window? Window{ get; internal set; }

    /// <summary>
    /// Очищает Device Context
    /// </summary>
    public void Release() => WL.System.Draw.ReleaseDeviceContext(Window, this);

    // ----------------------------------------------------------------------
    
    //wip
}