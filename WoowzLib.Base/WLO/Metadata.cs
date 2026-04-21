namespace WLO;

public abstract class Metadata{
    protected Metadata(string Name = "?", object? Parent = null){
        this.Name = Name;
        this.Parent = Parent;
    }
    
    /// <summary>
    /// Название
    /// </summary>
    public readonly string  Name;
    
    /// <summary>
    /// К какому объекту привязан?
    /// </summary>
    public readonly object? Parent;
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"Metadata({ToMetadataString()})";

    public string ToMetadataString() => $"\"{Name}\", {WL.__Base.Other.ToString(Parent)}";
}