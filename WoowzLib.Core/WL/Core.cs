using WLO;

namespace WL;

public static class Core{
    /// <summary>
    /// Установить информацию об проекте
    /// </summary>
    /// <param name="ProjectInfo">Информация об проекте</param>
    /// <param name="EngineInfo">Информация об ядре</param>
    public static void SetProjectInfo(ProjectInfo ProjectInfo, ProjectInfo? EngineInfo = null){
        try{
            if(EngineInfo.HasValue){ WL.__Base.EngineInfo = EngineInfo.Value; }
            
            WL.__Base.ProjectInfo = ProjectInfo;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке информации об проекте!\nПроект: " + WL.__Base.Other.ToString(ProjectInfo) + "\nЯдро: " + WL.__Base.Other.ToString(EngineInfo), e);
        }
    }

    /// <summary>
    /// Информация об проекте
    /// </summary>
    public static ProjectInfo ProjectInfo => WL.__Base.ProjectInfo;
    
    /// <summary>
    /// Информация об ядре
    /// </summary>
    public static ProjectInfo EngineInfo => WL.__Base.EngineInfo;
}