/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.160, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector4F : IEquatable<Vector4F>{
	public Vector4F(float X, float Y, float Z, float W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	public Vector4F(float XYZW) : this(XYZW, XYZW, XYZW, XYZW){}
	public Vector4F(){}
	
	// ----------------------------------------------------------------------
	
	public float X;
	public float Y;
	public float Z;
	public float W;
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector4F Zero = new Vector4F(0, 0, 0, 0);
	public static readonly Vector4F One = new Vector4F(1, 1, 1, 1);
	public static readonly Vector4F NOne = new Vector4F(-1, -1, -1, -1);
	public static readonly Vector4F Half = new Vector4F(0.5f, 0.5f, 0.5f, 0.5f);
	public static readonly Vector4F Right = new Vector4F(1, 0, 0, 0);
	public static readonly Vector4F Left = new Vector4F(-1, 0, 0, 0);
	public static readonly Vector4F Up = new Vector4F(0, 1, 0, 0);
	public static readonly Vector4F Down = new Vector4F(0, -1, 0, 0);
	public static readonly Vector4F Front = new Vector4F(0, 0, 1, 0);
	public static readonly Vector4F Back = new Vector4F(0, 0, -1, 0);
	public static readonly Vector4F Ana = new Vector4F(0, 0, 0, 1);
	public static readonly Vector4F Kata = new Vector4F(0, 0, 0, -1);
	public static readonly Vector4F AxisX = new Vector4F(1, 0, 0, 0);
	public static readonly Vector4F AxisY = new Vector4F(0, 1, 0, 0);
	public static readonly Vector4F AxisZ = new Vector4F(0, 0, 1, 0);
	public static readonly Vector4F AxisW = new Vector4F(0, 0, 0, 1);
	public static readonly Vector4F Double = new Vector4F(2, 2, 2, 2);
	public static readonly Vector4F Quarter = new Vector4F(0.25f, 0.25f, 0.25f, 0.25f);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Add(float X, float Y, float Z, float W) => new Vector4F(this.X + X, this.Y + Y, this.Z + Z, this.W + W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Add(Vector4F Other) => Add(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Add(float S) => Add(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Sub(float X, float Y, float Z, float W) => new Vector4F(this.X - X, this.Y - Y, this.Z - Z, this.W - W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Sub(Vector4F Other) => Sub(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Sub(float S) => Sub(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Mul(float X, float Y, float Z, float W) => new Vector4F(this.X * X, this.Y * Y, this.Z * Z, this.W * W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Mul(Vector4F Other) => Mul(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Mul(float S) => Mul(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Div(float X, float Y, float Z, float W) => new Vector4F(this.X / X, this.Y / Y, this.Z / Z, this.W / W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Div(Vector4F Other) => Div(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4F Div(float S) => Div(S, S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector4F(" + X + ", " + Y + ", " + Z + ", " + W + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z + ", " + W;
	
	public bool Equals(Vector4F Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W;
	public override bool Equals(object? Object) => Object is Vector4F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector4F L, Vector4F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector4F L, Vector4F R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator +(Vector4F L, Vector4F R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator +(Vector4F V, float S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator +(float S, Vector4F V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator -(Vector4F L, Vector4F R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator -(Vector4F V, float S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator -(float S, Vector4F V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator *(Vector4F L, Vector4F R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator *(Vector4F V, float S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator *(float S, Vector4F V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator /(Vector4F L, Vector4F R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator /(Vector4F V, float S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4F operator /(float S, Vector4F V) => V / S;
}