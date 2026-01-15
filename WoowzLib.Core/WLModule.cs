[AttributeUsage(AttributeTargets.Class)]
public class WLModule(int Order, int Version) : Attribute{
    public int Order  { get; } = Order  ;
    public int Version{ get; } = Version;
}