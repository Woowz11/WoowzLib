 public static class Program{
    public static int Main(string[] Args){

        WL.Explorer.Resources.Load("WoowzLib.GLFW.Native.win-x64.glfw3.dll", typeof(WL.GLSL).Assembly);
        
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(10);
        }

        Console.ReadKey(true);
        return 0;
    }
}