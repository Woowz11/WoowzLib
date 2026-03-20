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
    /// Вызывается при изменении информации, возвращает: (Изменённую информацию об проекте, Изменённую информацию об ядре), получает: (Информация об проекте, Информация об ядре)
    /// </summary>
    public static event Func<ProjectMetadata, ProjectMetadata?, (ProjectMetadata Project, ProjectMetadata? Engine)>? OnMetadataChanged;
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Остановить библиотеку
    /// </summary>
    public static void Terminate() => WL.__Base.Terminate();
    
    /// <summary>
    /// Вызывается при остановке библиотеки
    /// </summary>
    public static event Action OnTerminate{
        add    => WL.__Base.OnTerminate += value;
        remove => WL.__Base.OnTerminate -= value;
    }

    /// <summary>
    /// Автоматически вызывает Terminate, при закрытии или краше приложения!
    /// </summary>
    public static void EnableAutoTerminate() => WL.__Base.HookTerminate();
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Запуск Logger
    /// </summary>
    public static void BaseLoggerInitialize() => WL.__Base.Logger.Initialize();
    
    /// <summary>
    /// Вызывается при вызове вывода сообщения в консоль, возвращает: (статус, сообщение), получает: (статус, сообщение), если вернуть null, то сообщение не отправится
    /// </summary>
    public static event Func<byte, object?, string, (byte, object?, string)?>? OnPrint{
        add    => WL.__Base.Logger.OnPrint += value;
        remove => WL.__Base.Logger.OnPrint -= value;
    }

    /// <summary>
    /// Функция вывода сообщения в консоль, получает: (статус, доп. информация, сообщение), возвращает: (сообщение), если вернуть null, то сообщение не отправится
    /// </summary>
    public static Func<byte, object?, string, string?>? Output{
        get => WL.__Base.Logger.Output;
        set => WL.__Base.Logger.Output = value;
    }
}