/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.311, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector3D : IEquatable<Vector3D>{
	public Vector3D(double X, double Y, double Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	public Vector3D(double XYZ) : this(XYZ, XYZ, XYZ){}
	public Vector3D(){}
	
	// ----------------------------------------------------------------------
	
	public double X;
	public double Y;
	public double Z;
	public double W{
		get => X;
		set => X = value;
	}
	public double H{
		get => Y;
		set => Y = value;
	}
	public double D{
		get => Z;
		set => Z = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector3D Zero = new Vector3D(0, 0, 0);
	public static readonly Vector3D One = new Vector3D(1, 1, 1);
	public static readonly Vector3D NOne = new Vector3D(-1, -1, -1);
	public static readonly Vector3D Half = new Vector3D(0.5, 0.5, 0.5);
	public static readonly Vector3D Right = new Vector3D(1, 0, 0);
	public static readonly Vector3D Left = new Vector3D(-1, 0, 0);
	public static readonly Vector3D Up = new Vector3D(0, 1, 0);
	public static readonly Vector3D Down = new Vector3D(0, -1, 0);
	public static readonly Vector3D Front = new Vector3D(0, 0, 1);
	public static readonly Vector3D Back = new Vector3D(0, 0, -1);
	public static readonly Vector3D AxisX = new Vector3D(1, 0, 0);
	public static readonly Vector3D AxisY = new Vector3D(0, 1, 0);
	public static readonly Vector3D AxisZ = new Vector3D(0, 0, 1);
	public static readonly Vector3D Double = new Vector3D(2, 2, 2);
	public static readonly Vector3D Quarter = new Vector3D(0.25, 0.25, 0.25);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Add(double X, double Y, double Z) => new Vector3D(this.X + X, this.Y + Y, this.Z + Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Add(Vector3D Other) => Add(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Add(double S) => Add(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Sub(double X, double Y, double Z) => new Vector3D(this.X - X, this.Y - Y, this.Z - Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Sub(Vector3D Other) => Sub(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Sub(double S) => Sub(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Mul(double X, double Y, double Z) => new Vector3D(this.X * X, this.Y * Y, this.Z * Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Mul(Vector3D Other) => Mul(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Mul(double S) => Mul(S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Div(double X, double Y, double Z) => new Vector3D(this.X / X, this.Y / Y, this.Z / Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Div(Vector3D Other) => Div(Other.X, Other.Y, Other.Z);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3D Div(double S) => Div(S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector3D(" + ToShortString() + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z;
	public string ToPositionString() => X + ":" + Y + ":" + Z;
	public string ToSizeString() => W + "x" + H + "x" + D;
	
	public bool Equals(Vector3D Other) => X == Other.X && Y == Other.Y && Z == Other.Z;
	public override bool Equals(object? Object) => Object is Vector3D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector3D L, Vector3D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector3D L, Vector3D R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator +(Vector3D L, Vector3D R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator +(Vector3D V, double S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator +(double S, Vector3D V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator -(Vector3D L, Vector3D R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator -(Vector3D V, double S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator -(double S, Vector3D V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator *(Vector3D L, Vector3D R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator *(Vector3D V, double S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator *(double S, Vector3D V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator /(Vector3D L, Vector3D R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator /(Vector3D V, double S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3D operator /(double S, Vector3D V) => V / S;
}