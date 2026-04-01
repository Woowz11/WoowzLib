/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.264, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector3F : IEquatable<Vector3F>{
	public Vector3F(float X, float Y, float Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	public Vector3F(float XYZ) : this(XYZ, XYZ, XYZ){}
	public Vector3F(){}
	
	// ----------------------------------------------------------------------
	
	public float X;
	public float Y;
	public float Z;
	public float W{
		get => X;
		set => X = value;
	}
	public float H{
		get => Y;
		set => Y = value;
	}
	public float D{
		get => Z;
		set => Z = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector3F Zero = new Vector3F(0, 0, 0);
	public static readonly Vector3F One = new Vector3F(1, 1, 1);
	public static readonly Vector3F NOne = new Vector3F(-1, -1, -1);
	public static readonly Vector3F Half = new Vector3F(0.5f, 0.5f, 0.5f);
	public static readonly Vector3F Right = new Vector3F(1, 0, 0);
	public static readonly Vector3F Left = new Vector3F(-1, 0, 0);
	public static readonly Vector3F Up = new Vector3F(0, 1, 0);
	public static readonly Vector3F Down = new Vector3F(0, -1, 0);
	public static readonly Vector3F Front = new Vector3F(0, 0, 1);
	public static readonly Vector3F Back = new Vector3F(0, 0, -1);
	public static readonly Vector3F AxisX = new Vector3F(1, 0, 0);
	public static readonly Vector3F AxisY = new Vector3F(0, 1, 0);
	public static readonly Vector3F AxisZ = new Vector3F(0, 0, 1);
	public static readonly Vector3F Double = new Vector3F(2, 2, 2);
	public static readonly Vector3F Quarter = new Vector3F(0.25f, 0.25f, 0.25f);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Add(float X, float Y, float Z) => new Vector3F(this.X + X, this.Y + Y, this.Z + Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Add(Vector3F Other) => Add(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Add(float S) => Add(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Sub(float X, float Y, float Z) => new Vector3F(this.X - X, this.Y - Y, this.Z - Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Sub(Vector3F Other) => Sub(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Sub(float S) => Sub(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Mul(float X, float Y, float Z) => new Vector3F(this.X * X, this.Y * Y, this.Z * Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Mul(Vector3F Other) => Mul(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Mul(float S) => Mul(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Div(float X, float Y, float Z) => new Vector3F(this.X / X, this.Y / Y, this.Z / Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Div(Vector3F Other) => Div(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3F Div(float S) => Div(S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector3F(" + ToShortString() + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z;
	public string ToPositionString() => X + ":" + Y + ":" + Z;
	public string ToSizeString() => W + "x" + H + "x" + D;
	
	public bool Equals(Vector3F Other) => X == Other.X && Y == Other.Y && Z == Other.Z;
	public override bool Equals(object? Object) => Object is Vector3F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector3F L, Vector3F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector3F L, Vector3F R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator +(Vector3F L, Vector3F R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator +(Vector3F V, float S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator +(float S, Vector3F V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator -(Vector3F L, Vector3F R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator -(Vector3F V, float S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator -(float S, Vector3F V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator *(Vector3F L, Vector3F R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator *(Vector3F V, float S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator *(float S, Vector3F V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator /(Vector3F L, Vector3F R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator /(Vector3F V, float S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3F operator /(float S, Vector3F V) => V / S;
}