using System.Runtime.Loader;
using WLO;

namespace WL;

public static class Core{
    /// <summary>
    /// Информация
    /// </summary>
    public static (ProjectMetadata Project, ProjectMetadata? Engine) Metadata{
        get => (WL.__Base.ProjectMetadata, WL.__Base.EngineMetadata);
        set{
            try{
                ProjectMetadata  Project = value.Project;
                ProjectMetadata? Engine  = value.Engine ;

                try{
                    (ProjectMetadata Project, ProjectMetadata? Engine)? ChangedMetadata = OnMetadataChanged?.Invoke(Project, Engine);
                    if(ChangedMetadata.HasValue){
                        Project = ChangedMetadata.Value.Project;
                        Engine = ChangedMetadata.Value.Engine;
                    }
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в ивенте OnMetadataChanged!", e);
                }
                
                if(Engine.HasValue){ WL.__Base.EngineMetadata = Engine.Value; }

                WL.__Base.Terminated      = false;
                WL.__Base.ProjectMetadata = Project;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке информации!\nПроект: " + WL.__Base.Other.ToString(value.Project) + "\nЯдро: " + WL.__Base.Other.ToString(value.Engine), e);
            }
        }
    }

    /// <summary>
    /// Вызывается при изменении информации, (Информация об проекте, Информация об ядре) => (Изменённая информация об проекте, Изменённая информация об ядре)
    /// </summary>
    public static event Func<ProjectMetadata, ProjectMetadata?, (ProjectMetadata Project, ProjectMetadata? Engine)>? OnMetadataChanged;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Остановить библиотеку
    /// </summary>
    public static void Terminate() => WL.__Base.Terminate(CloseReason.User);
    
    /// <summary>
    /// Вызывается при остановке библиотеки
    /// </summary>
    public static event Action<CloseReason> OnTerminate{
        add    => WL.__Base.OnTerminate += value;
        remove => WL.__Base.OnTerminate -= value;
    }

    /// <summary>
    /// Автоматически вызывает Terminate, при закрытии или краше приложения!
    /// </summary>
    public static void EnableAutoTerminate() => WL.__Base.HookTerminate();
    
    /// <summary>
    /// Вызывается при выходе из приложения (ТОЛЬКО ПРИ ВЫХОДЕ, CRASH или другие последствия не вызывают! Для этого используйте OnClose!)
    /// </summary>
    public static event EventHandler? OnExit{
        add    => WL.__Base.OnExit += value;
        remove => WL.__Base.OnExit -= value;
    }
        
    /// <summary>
    /// Вызывается при CRASH
    /// </summary>
    public static event UnhandledExceptionEventHandler? OnCrash{
        add    => WL.__Base.OnCrash += value;
        remove => WL.__Base.OnCrash -= value;
    }
        
    /// <summary>
    /// Вызывается при нажатиях комбинации <b>Ctrl+C, Ctrl+Break</b> в консоли
    /// </summary>
    public static event ConsoleCancelEventHandler? OnCancel{
        add    => WL.__Base.OnCancel += value;
        remove => WL.__Base.OnCancel -= value;
    }
    
    /// <summary>
    /// Вызывается при любом закрытии приложения
    /// </summary>
    public static event Action<CloseReason>? OnClose{
        add    => WL.__Base.OnClose += value;
        remove => WL.__Base.OnClose -= value;
    }

    // ----------------------------------------------------------------------

    /// <summary>
    /// Запуск Logger
    /// </summary>
    public static void BaseLoggerInitialize() => WL.__Base.Logger.Initialize();
    
    /// <summary>
    /// Вызывается при вызове вывода сообщения в консоль, (статус, сообщение) => (изменённый статус, изменённый сообщение), если вернуть null, то сообщение не отправится
    /// </summary>
    public static event Func<byte, object?, string, (byte, object?, string)?>? OnPrint{
        add    => WL.__Base.Logger.OnPrint += value;
        remove => WL.__Base.Logger.OnPrint -= value;
    }

    /// <summary>
    /// Функция вывода сообщения в консоль, (статус, дополнительная информация, сообщение) => (финальное сообщение), если вернуть null, то сообщение не отправится
    /// </summary>
    public static Func<byte, object?, string, string?>? Output{
        get => WL.__Base.Logger.Output;
        set => WL.__Base.Logger.Output = value;
    }
    
    // ----------------------------------------------------------------------
    
    /// <summary>
    /// Текущая операционная система
    /// </summary>
    public static OS CurrentOS => WL.__Base.CurrentOS;
}