/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.311, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector3UI : IEquatable<Vector3UI>{
	public Vector3UI(uint X, uint Y, uint Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	public Vector3UI(uint XYZ) : this(XYZ, XYZ, XYZ){}
	public Vector3UI(){}
	
	// ----------------------------------------------------------------------
	
	public uint X;
	public uint Y;
	public uint Z;
	public uint W{
		get => X;
		set => X = value;
	}
	public uint H{
		get => Y;
		set => Y = value;
	}
	public uint D{
		get => Z;
		set => Z = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector3UI Zero = new Vector3UI(0, 0, 0);
	public static readonly Vector3UI One = new Vector3UI(1, 1, 1);
	public static readonly Vector3UI Right = new Vector3UI(1, 0, 0);
	public static readonly Vector3UI Up = new Vector3UI(0, 1, 0);
	public static readonly Vector3UI Front = new Vector3UI(0, 0, 1);
	public static readonly Vector3UI AxisX = new Vector3UI(1, 0, 0);
	public static readonly Vector3UI AxisY = new Vector3UI(0, 1, 0);
	public static readonly Vector3UI AxisZ = new Vector3UI(0, 0, 1);
	public static readonly Vector3UI Double = new Vector3UI(2, 2, 2);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Add(uint X, uint Y, uint Z) => new Vector3UI(this.X + X, this.Y + Y, this.Z + Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Add(Vector3UI Other) => Add(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Add(uint S) => Add(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Sub(uint X, uint Y, uint Z) => new Vector3UI(this.X - X, this.Y - Y, this.Z - Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Sub(Vector3UI Other) => Sub(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Sub(uint S) => Sub(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Mul(uint X, uint Y, uint Z) => new Vector3UI(this.X * X, this.Y * Y, this.Z * Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Mul(Vector3UI Other) => Mul(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Mul(uint S) => Mul(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Div(uint X, uint Y, uint Z) => new Vector3UI(this.X / X, this.Y / Y, this.Z / Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Div(Vector3UI Other) => Div(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3UI Div(uint S) => Div(S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector3UI(" + ToShortString() + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z;
	public string ToPositionString() => X + ":" + Y + ":" + Z;
	public string ToSizeString() => W + "x" + H + "x" + D;
	
	public bool Equals(Vector3UI Other) => X == Other.X && Y == Other.Y && Z == Other.Z;
	public override bool Equals(object? Object) => Object is Vector3UI Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector3UI L, Vector3UI R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector3UI L, Vector3UI R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator +(Vector3UI L, Vector3UI R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator +(Vector3UI V, uint S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator +(uint S, Vector3UI V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator -(Vector3UI L, Vector3UI R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator -(Vector3UI V, uint S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator -(uint S, Vector3UI V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator *(Vector3UI L, Vector3UI R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator *(Vector3UI V, uint S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator *(uint S, Vector3UI V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator /(Vector3UI L, Vector3UI R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator /(Vector3UI V, uint S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3UI operator /(uint S, Vector3UI V) => V / S;
}