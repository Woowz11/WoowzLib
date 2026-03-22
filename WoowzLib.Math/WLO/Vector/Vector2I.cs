namespace WLO.Vector;

public struct Vector2I{
    public Vector2I(){}
    public Vector2I(int X, int Y){ this.X = X; this.Y = Y; }
    
    public int X;
    public int Y;

    public int W => X;
    public int H => Y;
    
    // ----------------------------------------------------------------------

    public static readonly Vector2I Zero = new Vector2I();
}