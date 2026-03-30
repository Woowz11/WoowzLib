/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.160, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector2I : IEquatable<Vector2I>{
	public Vector2I(int X, int Y){
		this.X = X;
		this.Y = Y;
	}
	public Vector2I(int XY) : this(XY, XY){}
	public Vector2I(){}
	
	// ----------------------------------------------------------------------
	
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
	
	public static readonly Vector2I Zero = new Vector2I(0, 0);
	public static readonly Vector2I One = new Vector2I(1, 1);
	public static readonly Vector2I NOne = new Vector2I(-1, -1);
	public static readonly Vector2I Right = new Vector2I(1, 0);
	public static readonly Vector2I Left = new Vector2I(-1, 0);
	public static readonly Vector2I Up = new Vector2I(0, 1);
	public static readonly Vector2I Down = new Vector2I(0, -1);
	public static readonly Vector2I AxisX = new Vector2I(1, 0);
	public static readonly Vector2I AxisY = new Vector2I(0, 1);
	public static readonly Vector2I Double = new Vector2I(2, 2);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Add(int X, int Y) => new Vector2I(this.X + X, this.Y + Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Add(Vector2I Other) => Add(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Add(int S) => Add(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Sub(int X, int Y) => new Vector2I(this.X - X, this.Y - Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Sub(Vector2I Other) => Sub(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Sub(int S) => Sub(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Mul(int X, int Y) => new Vector2I(this.X * X, this.Y * Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Mul(Vector2I Other) => Mul(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Mul(int S) => Mul(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Div(int X, int Y) => new Vector2I(this.X / X, this.Y / Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Div(Vector2I Other) => Div(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2I Div(int S) => Div(S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector2I(" + X + ", " + Y + ")";
	public string ToShortString() => X + ", " + Y;
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
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator +(Vector2I L, Vector2I R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator +(Vector2I V, int S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator +(int S, Vector2I V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator -(Vector2I L, Vector2I R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator -(Vector2I V, int S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator -(int S, Vector2I V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator *(Vector2I L, Vector2I R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator *(Vector2I V, int S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator *(int S, Vector2I V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator /(Vector2I L, Vector2I R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator /(Vector2I V, int S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2I operator /(int S, Vector2I V) => V / S;
}