using WoowzLib.Core;

namespace WoowzLibTest.Core;

class Program{
    static void Main(){
        Console.WriteLine("PROGRAM");
        
        WL.Install();
        
        Console.WriteLine("END PROGRAM");
        
        Console.WriteLine("VERSION " + WL_Core.Version);
    }
}