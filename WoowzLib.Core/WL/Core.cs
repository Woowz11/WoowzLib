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
                if(value.Engine.HasValue){ WL.__Base.EngineMetadata = value.Engine.Value; }
            
                WL.__Base.ProjectMetadata = value.Project;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке информации!\nПроект: " + WL.__Base.Other.ToString(value.Project) + "\nЯдро: " + WL.__Base.Other.ToString(value.Engine), e);
            }
        }
    }
}