using WoowzLib.Core;

namespace WoowzLib.Math;

[WLModule(0)]
public class WL_Math{
    static WL_Math(){
        Console.WriteLine("math");
        WL.M.Math = new WL_Math();
    }
    
    public int Add(int a, int b) => a + b;
}
