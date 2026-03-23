using System.Runtime.CompilerServices;

namespace WLO.Vector;

public struct Vector2I : IEquatable<Vector2I>{
    public Vector2I(int X, int Y){ this.X = X; this.Y = Y; }
    public Vector2I(int XY) : this(XY, XY){}
    public Vector2I(){}
    
    public int X;
    public int Y;

    public int W{
        get => X;
        set => X = value;
    }
    public int H{
        get => Y;
        set => Y = value;
    }
    
    // ----------------------------------------------------------------------

    public static readonly Vector2I Zero = new Vector2I();
    public static readonly Vector2I One  = new Vector2I(1);
    public static readonly Vector2I NOne = new Vector2I(-1);
    
    // ----------------------------------------------------------------------

    public Vector2I WithX(int X){ this.X = X; return this; }
    public Vector2I WithY(int Y){ this.Y = Y; return this; }
    public Vector2I WithW(int X){ this.X = X; return this; }
    public Vector2I WithH(int Y){ this.Y = Y; return this; }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "Vector2I(" + X + ", " + Y + ")";

    public string ToPositionString() => X + ":" + Y;

    public string ToSizeString() => W + "x" + H;

    public bool Equals(Vector2I Other) => X == Other.X && Y == Other.Y;
    public override bool Equals(object? Object) => Object is Vector2I Other && Equals(Other);

    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    // ----------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector2I L, Vector2I R) => L.Equals(R);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector2I L, Vector2I R) => !L.Equals(R);
}