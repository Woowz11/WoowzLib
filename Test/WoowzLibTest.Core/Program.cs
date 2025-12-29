using WoowzLib.Core;

namespace WoowzLibTest.Core;

class Program{
    static void Main(){
        Console.WriteLine("RUN");
        
        WL.Install();
        
        Console.WriteLine("test");
        Console.WriteLine(WL.M.Math.Add(5,3));
    }
}