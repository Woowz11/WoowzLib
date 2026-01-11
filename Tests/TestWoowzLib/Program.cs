 public static class Program{
    public static int Main(string[] Args){

        WL.Explorer.Temp.Create("pokpok/gaga.txt").WriteString("HELLO!!!!");
        
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(10);
        }

        Console.ReadKey(true);
        return 0;
    }
}