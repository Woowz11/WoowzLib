using WLO;
using WLO.WinForm;

namespace WL.Windows;

/// <summary>
/// Что-бы работать с Windows Form, нужно в настройках проекта добавить <c>&lt;UseWindowsForms&gt;true&lt;/UseWindowsForms&gt;</c>, и TargetFramework должен заканчиваться на <c>*-windows</c>
/// </summary>
[WLModule(15, 3)]
public class Form{
    static Form(){
        WL.WoowzLib.OnStop += __Destroy;
        
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
    }

    /// <summary>
    /// Открытые окна
    /// </summary>
    internal static readonly HashSet<Window> Windows = [];
    
    /// <summary>
    /// Обновление WinForm (Нужно вызывать каждый кадр (внутри while))
    /// </summary>
    public static void Tick(){
        try{
            Application.DoEvents();
            foreach(Window Window in Windows.ToArray()){
                if(Window.ShouldDestroy){ Window.Destroy(); }
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при обновлении WinForm!", e);
        }
    }
    
    private static void __Destroy(){
        try{
            if(Destroyed){ return; }

            if(Windows.Count > 0){ Logger.Warn("Оставшиеся окна были закрыты через WL.Windows.WinForm.Stop()!"); }

            foreach(Window Window in Windows.ToArray()){
                Window.Destroy();
            }
            Windows.Clear();

            Destroyed = true;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при базовой остановке WinForm!", e);
        }
    }
    private static bool Destroyed;
}