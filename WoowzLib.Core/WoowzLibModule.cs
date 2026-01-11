[AttributeUsage(AttributeTargets.Class)]
public class WoowzLibModule(int Order) : Attribute{
    public int Order{ get; } = Order;
}