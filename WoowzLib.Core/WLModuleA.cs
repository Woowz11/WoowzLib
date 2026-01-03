namespace WoowzLib.Core;

[AttributeUsage(AttributeTargets.Class)]
public class WLModuleA(int Order) : Attribute{
    public int Order{ get; } = Order;
}