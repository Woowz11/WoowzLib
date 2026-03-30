/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.156, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector4I : IEquatable<Vector4I>{
	public Vector4I(int X, int Y, int Z, int W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	public Vector4I(int XYZW) : this(XYZW, XYZW, XYZW, XYZW){}
	public Vector4I(){}
	
	// ----------------------------------------------------------------------
	
	public int X;
	public int Y;
	public int Z;
	public int W;
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector4I Zero = new Vector4I(0, 0, 0, 0);
	public static readonly Vector4I One = new Vector4I(1, 1, 1, 1);
	public static readonly Vector4I NOne = new Vector4I(-1, -1, -1, -1);
	public static readonly Vector4I Right = new Vector4I(1, 0, 0, 0);
	public static readonly Vector4I Left = new Vector4I(-1, 0, 0, 0);
	public static readonly Vector4I Up = new Vector4I(0, 1, 0, 0);
	public static readonly Vector4I Down = new Vector4I(0, -1, 0, 0);
	public static readonly Vector4I Front = new Vector4I(0, 0, 1, 0);
	public static readonly Vector4I Back = new Vector4I(0, 0, -1, 0);
	public static readonly Vector4I Ana = new Vector4I(0, 0, 0, 1);
	public static readonly Vector4I Kata = new Vector4I(0, 0, 0, -1);
	public static readonly Vector4I AxisX = new Vector4I(1, 0, 0, 0);
	public static readonly Vector4I AxisY = new Vector4I(0, 1, 0, 0);
	public static readonly Vector4I AxisZ = new Vector4I(0, 0, 1, 0);
	public static readonly Vector4I AxisW = new Vector4I(0, 0, 0, 1);
	public static readonly Vector4I Double = new Vector4I(2, 2, 2, 2);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Add(int X, int Y, int Z, int W) => new Vector4I(this.X + X, this.Y + Y, this.Z + Z, this.W + W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Add(Vector4I Other) => Add(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Add(int S) => Add(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Sub(int X, int Y, int Z, int W) => new Vector4I(this.X - X, this.Y - Y, this.Z - Z, this.W - W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Sub(Vector4I Other) => Sub(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Sub(int S) => Sub(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Mul(int X, int Y, int Z, int W) => new Vector4I(this.X * X, this.Y * Y, this.Z * Z, this.W * W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Mul(Vector4I Other) => Mul(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Mul(int S) => Mul(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Div(int X, int Y, int Z, int W) => new Vector4I(this.X / X, this.Y / Y, this.Z / Z, this.W / W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Div(Vector4I Other) => Div(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4I Div(int S) => Div(S, S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector4I(" + X + ", " + Y + ", " + Z + ", " + W + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z + ", " + W;
	
	public bool Equals(Vector4I Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W;
	public override bool Equals(object? Object) => Object is Vector4I Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector4I L, Vector4I R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector4I L, Vector4I R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator +(Vector4I L, Vector4I R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator +(Vector4I V, int S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator +(int S, Vector4I V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator -(Vector4I L, Vector4I R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator -(Vector4I V, int S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator -(int S, Vector4I V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator *(Vector4I L, Vector4I R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator *(Vector4I V, int S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator *(int S, Vector4I V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator /(Vector4I L, Vector4I R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator /(Vector4I V, int S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4I operator /(int S, Vector4I V) => V / S;
}