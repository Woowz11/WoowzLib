public static class Program{
    public static int Main(string[] Args){
        
        string folder = "W:\\Woowz11\\Desktop\\TESTS\\";

        File f = new File(folder + "test.txt");

        f.AddString("\nHELLO!!!)))");
        
        Console.WriteLine(f.ReadString());
        
        while (!Console.KeyAvailable)
        {
            Thread.Sleep(10);
        }

        Console.ReadKey(true);
        return 0;
    }
}