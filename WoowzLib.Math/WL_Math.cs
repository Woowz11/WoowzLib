using WoowzLib.Core;
using WoowzLib.Math;

namespace WoowzLib.Math{

    [WLModule(0, "Math")]
    public class WL_Math{
        public int Add(int a, int b) => a + b;
    }
}

namespace WoowzLib.Core{

    public static partial class WL{
        public static WL_Math Math = new WL_Math();
    }
}