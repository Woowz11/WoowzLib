/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.300, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector4D : IEquatable<Vector4D>{
	public Vector4D(double X, double Y, double Z, double W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	public Vector4D(double XYZW) : this(XYZW, XYZW, XYZW, XYZW){}
	public Vector4D(){}
	
	// ----------------------------------------------------------------------
	
	public double X;
	public double Y;
	public double Z;
	public double W;
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector4D Zero = new Vector4D(0, 0, 0, 0);
	public static readonly Vector4D One = new Vector4D(1, 1, 1, 1);
	public static readonly Vector4D NOne = new Vector4D(-1, -1, -1, -1);
	public static readonly Vector4D Half = new Vector4D(0.5, 0.5, 0.5, 0.5);
	public static readonly Vector4D Right = new Vector4D(1, 0, 0, 0);
	public static readonly Vector4D Left = new Vector4D(-1, 0, 0, 0);
	public static readonly Vector4D Up = new Vector4D(0, 1, 0, 0);
	public static readonly Vector4D Down = new Vector4D(0, -1, 0, 0);
	public static readonly Vector4D Front = new Vector4D(0, 0, 1, 0);
	public static readonly Vector4D Back = new Vector4D(0, 0, -1, 0);
	public static readonly Vector4D Ana = new Vector4D(0, 0, 0, 1);
	public static readonly Vector4D Kata = new Vector4D(0, 0, 0, -1);
	public static readonly Vector4D AxisX = new Vector4D(1, 0, 0, 0);
	public static readonly Vector4D AxisY = new Vector4D(0, 1, 0, 0);
	public static readonly Vector4D AxisZ = new Vector4D(0, 0, 1, 0);
	public static readonly Vector4D AxisW = new Vector4D(0, 0, 0, 1);
	public static readonly Vector4D Double = new Vector4D(2, 2, 2, 2);
	public static readonly Vector4D Quarter = new Vector4D(0.25, 0.25, 0.25, 0.25);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Add(double X, double Y, double Z, double W) => new Vector4D(this.X + X, this.Y + Y, this.Z + Z, this.W + W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Add(Vector4D Other) => Add(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Add(double S) => Add(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Sub(double X, double Y, double Z, double W) => new Vector4D(this.X - X, this.Y - Y, this.Z - Z, this.W - W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Sub(Vector4D Other) => Sub(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Sub(double S) => Sub(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Mul(double X, double Y, double Z, double W) => new Vector4D(this.X * X, this.Y * Y, this.Z * Z, this.W * W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Mul(Vector4D Other) => Mul(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Mul(double S) => Mul(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Div(double X, double Y, double Z, double W) => new Vector4D(this.X / X, this.Y / Y, this.Z / Z, this.W / W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Div(Vector4D Other) => Div(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4D Div(double S) => Div(S, S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector4D(" + ToShortString() + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z + ", " + W;
	
	public bool Equals(Vector4D Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W;
	public override bool Equals(object? Object) => Object is Vector4D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector4D L, Vector4D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector4D L, Vector4D R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator +(Vector4D L, Vector4D R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator +(Vector4D V, double S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator +(double S, Vector4D V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator -(Vector4D L, Vector4D R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator -(Vector4D V, double S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator -(double S, Vector4D V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator *(Vector4D L, Vector4D R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator *(Vector4D V, double S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator *(double S, Vector4D V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator /(Vector4D L, Vector4D R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator /(Vector4D V, double S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4D operator /(double S, Vector4D V) => V / S;
}