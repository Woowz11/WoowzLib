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
                
                (ProjectMetadata Project, ProjectMetadata? Engine)? ChangedMetadata = OnMetadataChanged?.Invoke(Project, Engine);
                if(ChangedMetadata.HasValue){
                    Project = ChangedMetadata.Value.Project;
                    Engine  = ChangedMetadata.Value.Engine ;
                }
                
                if(Engine.HasValue){ WL.__Base.EngineMetadata = Engine.Value; }
            
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
    
    /// <summary>
    /// Вызывается при вызове вывода сообщения в консоль, возвращает: (статус, сообщение), получает: (статус, сообщение), если вернуть null, то сообщение не отправится
    /// </summary>
    public static event Func<byte, object?, string, (byte, object?, string)?>? OnPrint{
        add    => WL.__Base.Logger.OnPrint += value;
        remove => WL.__Base.Logger.OnPrint -= value;
    }

    /// <summary>
    /// Функция вывода сообщения в консоль
    /// </summary>
    public static Action<byte, object?, string>? Output{
        get => WL.__Base.Logger.Output;
        set => WL.__Base.Logger.Output = value;
    }
}