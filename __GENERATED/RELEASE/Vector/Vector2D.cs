/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.386, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector2D : IEquatable<Vector2D>{
	public Vector2D(double X, double Y){
		this.X = X;
		this.Y = Y;
	}
	public Vector2D(double XY) : this(XY, XY){}
	public Vector2D(){}
	
	// ----------------------------------------------------------------------
	
	public double X;
	public double Y;
	
	public double W{
		get => X;
		set => X = value;
	}
	public double H{
		get => Y;
		set => Y = value;
	}
	
	public double L{
		get => X;
		set => X = value;
	}
	public double T{
		get => Y;
		set => Y = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector2D Zero = new Vector2D(0, 0);
	public static readonly Vector2D One = new Vector2D(1, 1);
	public static readonly Vector2D NOne = new Vector2D(-1, -1);
	public static readonly Vector2D Half = new Vector2D(0.5, 0.5);
	public static readonly Vector2D Max = new Vector2D(double.MaxValue, double.MaxValue);
	public static readonly Vector2D Right = new Vector2D(1, 0);
	public static readonly Vector2D Left = new Vector2D(-1, 0);
	public static readonly Vector2D Up = new Vector2D(0, 1);
	public static readonly Vector2D RightTop = new Vector2D(1, 1);
	public static readonly Vector2D RightBottom = new Vector2D(1, -1);
	public static readonly Vector2D LeftTop = new Vector2D(-1, 1);
	public static readonly Vector2D LeftBottom = new Vector2D(-1, -1);
	public static readonly Vector2D Down = new Vector2D(0, -1);
	public static readonly Vector2D AxisX = new Vector2D(1, 0);
	public static readonly Vector2D AxisY = new Vector2D(0, 1);
	public static readonly Vector2D Double = new Vector2D(2, 2);
	public static readonly Vector2D Quarter = new Vector2D(0.25, 0.25);
	public static readonly Vector2D Center = new Vector2D(0, 0);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Add(double X, double Y) => new Vector2D(this.X + X, this.Y + Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Add(Vector2D Other) => Add(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Add(double S) => Add(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Sub(double X, double Y) => new Vector2D(this.X - X, this.Y - Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Sub(Vector2D Other) => Sub(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Sub(double S) => Sub(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Mul(double X, double Y) => new Vector2D(this.X * X, this.Y * Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Mul(Vector2D Other) => Mul(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Mul(double S) => Mul(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Div(double X, double Y) => new Vector2D(this.X / X, this.Y / Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Div(Vector2D Other) => Div(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D Div(double S) => Div(S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Vector2D({ToShortString()})";
	public string ToShortString() => $"{X}, {Y}";
	public string ToPositionString() => $"{X}:{Y}";
	public string ToSizeString() => $"{W}x{H}";
	
	public bool Equals(Vector2D Other) => X == Other.X && Y == Other.Y;
	public override bool Equals(object? Object) => Object is Vector2D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2D L, Vector2D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2D L, Vector2D R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(Vector2D L, Vector2D R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(Vector2D V, double S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(double S, Vector2D V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(Vector2D L, Vector2D R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(Vector2D V, double S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(double S, Vector2D V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(Vector2D L, Vector2D R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(Vector2D V, double S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(double S, Vector2D V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(Vector2D L, Vector2D R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(Vector2D V, double S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(double S, Vector2D V) => V / S;
}