namespace WLO.Vector;

public struct Vector2UI{
    public Vector2UI(){}
    public Vector2UI(uint X, uint Y){ this.X = X; this.Y = Y; }
    
    public uint X;
    public uint Y;
    
    public uint W => X;
    public uint H => Y;
    
    // ----------------------------------------------------------------------

    public static readonly Vector2UI Zero = new Vector2UI();
}