using System.Runtime.CompilerServices;

namespace WLO.Vector;

public struct Vector2UI : IEquatable<Vector2UI>{
    public Vector2UI(uint X, uint Y){ this.X = X; this.Y = Y; }
    public Vector2UI(uint XY) : this(XY, XY){}
    public Vector2UI(){}
    
    public uint X;
    public uint Y;

    public uint W{
        get => X;
        set => X = value;
    }
    public uint H{
        get => Y;
        set => Y = value;
    }
    
    // ----------------------------------------------------------------------

    public static readonly Vector2UI Zero = new Vector2UI();
    public static readonly Vector2UI One  = new Vector2UI(1);
    
    // ----------------------------------------------------------------------

    public Vector2UI WithX(uint X){ this.X = X; return this; }
    public Vector2UI WithY(uint Y){ this.Y = Y; return this; }
    public Vector2UI WithW(uint X){ this.X = X; return this; }
    public Vector2UI WithH(uint Y){ this.Y = Y; return this; }
    
    // ----------------------------------------------------------------------

    public override string ToString() => "Vector2UI(" + X + ", " + Y + ")";

    public string ToPositionString() => X + ":" + Y;

    public string ToSizeString() => W + "x" + H;

    public bool Equals(Vector2UI Other) => X == Other.X && Y == Other.Y;
    public override bool Equals(object? Object) => Object is Vector2UI Other && Equals(Other);
    
    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    // ----------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector2UI L, Vector2UI R) => L.Equals(R);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector2UI L, Vector2UI R) => !L.Equals(R);
}