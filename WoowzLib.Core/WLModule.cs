namespace WoowzLib.Core;

public abstract class WLModule{
    public int Version = -1;

    public virtual void Install(){}
}