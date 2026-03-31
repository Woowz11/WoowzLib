/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.219, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector3I : IEquatable<Vector3I>{
	public Vector3I(int X, int Y, int Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	public Vector3I(int XYZ) : this(XYZ, XYZ, XYZ){}
	public Vector3I(){}
	
	// ----------------------------------------------------------------------
	
	public int X;
	public int Y;
	public int Z;
	public int W{
		get => X;
		set => X = value;
	}
	public int H{
		get => Y;
		set => Y = value;
	}
	public int D{
		get => Z;
		set => Z = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector3I Zero = new Vector3I(0, 0, 0);
	public static readonly Vector3I One = new Vector3I(1, 1, 1);
	public static readonly Vector3I NOne = new Vector3I(-1, -1, -1);
	public static readonly Vector3I Right = new Vector3I(1, 0, 0);
	public static readonly Vector3I Left = new Vector3I(-1, 0, 0);
	public static readonly Vector3I Up = new Vector3I(0, 1, 0);
	public static readonly Vector3I Down = new Vector3I(0, -1, 0);
	public static readonly Vector3I Front = new Vector3I(0, 0, 1);
	public static readonly Vector3I Back = new Vector3I(0, 0, -1);
	public static readonly Vector3I AxisX = new Vector3I(1, 0, 0);
	public static readonly Vector3I AxisY = new Vector3I(0, 1, 0);
	public static readonly Vector3I AxisZ = new Vector3I(0, 0, 1);
	public static readonly Vector3I Double = new Vector3I(2, 2, 2);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Add(int X, int Y, int Z) => new Vector3I(this.X + X, this.Y + Y, this.Z + Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Add(Vector3I Other) => Add(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Add(int S) => Add(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Sub(int X, int Y, int Z) => new Vector3I(this.X - X, this.Y - Y, this.Z - Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Sub(Vector3I Other) => Sub(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Sub(int S) => Sub(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Mul(int X, int Y, int Z) => new Vector3I(this.X * X, this.Y * Y, this.Z * Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Mul(Vector3I Other) => Mul(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Mul(int S) => Mul(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Div(int X, int Y, int Z) => new Vector3I(this.X / X, this.Y / Y, this.Z / Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Div(Vector3I Other) => Div(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3I Div(int S) => Div(S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector3I(" + X + ", " + Y + ", " + Z + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z;
	public string ToPositionString() => X + ":" + Y + ":" + Z;
	public string ToSizeString() => W + "x" + H + "x" + D;
	
	public bool Equals(Vector3I Other) => X == Other.X && Y == Other.Y && Z == Other.Z;
	public override bool Equals(object? Object) => Object is Vector3I Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector3I L, Vector3I R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector3I L, Vector3I R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator +(Vector3I L, Vector3I R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator +(Vector3I V, int S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator +(int S, Vector3I V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator -(Vector3I L, Vector3I R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator -(Vector3I V, int S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator -(int S, Vector3I V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator *(Vector3I L, Vector3I R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator *(Vector3I V, int S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator *(int S, Vector3I V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator /(Vector3I L, Vector3I R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator /(Vector3I V, int S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3I operator /(int S, Vector3I V) => V / S;
}