namespace WoowzLib.Core;

[AttributeUsage(AttributeTargets.Class)]
public class WLModule(int Order, string Name) : Attribute{
    public int    Order{ get; } = Order;
    public string Name { get; } = Name;
}