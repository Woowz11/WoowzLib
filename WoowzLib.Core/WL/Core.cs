namespace WL;

public static class Core{
    public static void test(){
        Console.WriteLine(WL.__Base.ProjectInfo.Name);
        Console.WriteLine(WL.__Base.ProjectInfo.Version);
        Console.WriteLine(WL.__Base.ProjectInfo.Engine);
        Console.WriteLine(WL.__Base.ProjectInfo.EngineVersion);
    }
}