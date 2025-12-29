namespace WoowzLib.Core;

[AttributeUsage(AttributeTargets.Class)]
public class WLModule(int Order) : Attribute{
    public int Order{ get; } = Order;
}