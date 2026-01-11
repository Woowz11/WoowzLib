 public static class Program{
    public static int Main(string[] Args){
        WL.WoowzLib.Start();
        
        WL.GLSL.Start();
        
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(10);
        }

        Console.ReadKey(true);
        
        WL.GLSL.Stop();
        return 0;
    }
}