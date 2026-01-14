namespace WLO;

public abstract class WindowContext{
    /// <summary>
    /// Уникальный ID окна (основан на Handle)
    /// </summary>
    public long ID{ get; protected set; }
    
    public abstract void __UpdateContext();

    public uint __Width { get; protected set; }
    public uint __Height{ get; protected set; }
}