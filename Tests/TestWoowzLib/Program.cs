public static class Program{
    public static int Main(string[] Args){
        
        WL.Native.LoadDLL();
        
        
        
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(10);
        }

        Console.ReadKey(true);
        return 0;
    }
}