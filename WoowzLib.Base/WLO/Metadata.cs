namespace WLO;

/// <summary>
/// Дополнительная информация, содержит название
/// </summary>
public abstract class Metadata{
    protected Metadata(string Name = "?"){ this.Name = Name; }
    
    /// <summary>
    /// Название
    /// </summary>
    public readonly string  Name;
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"Metadata({ToMetadataString()})";

    public virtual string ToMetadataString() => $"\"{Name}\"";
}

/// <summary>
/// Дополнительная информация, содержит название и объекта к которому привязан
/// </summary>
public abstract class MetadataParenting<С> : Metadata where С : class?{
    protected MetadataParenting(string Name = "?", С? Parent = null) : base(Name){ this.Parent = Parent; }
    
    /// <summary>
    /// К какому объекту привязан?
    /// </summary>
    public readonly С? Parent;
    
    // ----------------------------------------------------------------------

    public override string ToString() => $"MetadataP({ToMetadataString()})";

    public override string ToMetadataString() => $"{base.ToMetadataString()}, {WL.__Base.Other.ToString(Parent)}";
}