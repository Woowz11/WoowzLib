using WoowzLib.Core;

namespace WoowzLib.Math;

[WLModuleA(0)]
public class WL_Math : WLModule{
    static WL_Math(){
        
    }
    
    public int Add(int a, int b) => a + b;
}
