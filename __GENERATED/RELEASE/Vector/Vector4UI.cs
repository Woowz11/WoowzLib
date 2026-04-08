/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.329, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector4UI : IEquatable<Vector4UI>{
	public Vector4UI(uint X, uint Y, uint Z, uint W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	public Vector4UI(uint XYZW) : this(XYZW, XYZW, XYZW, XYZW){}
	public Vector4UI(){}
	
	// ----------------------------------------------------------------------
	
	public uint X;
	public uint Y;
	public uint Z;
	public uint W;
	
	public uint L{
		get => X;
		set => X = value;
	}
	public uint T{
		get => Y;
		set => Y = value;
	}
	public uint R{
		get => Z;
		set => Z = value;
	}
	public uint B{
		get => W;
		set => W = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector4UI Zero = new Vector4UI(0, 0, 0, 0);
	public static readonly Vector4UI One = new Vector4UI(1, 1, 1, 1);
	public static readonly Vector4UI Max = new Vector4UI(uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue);
	public static readonly Vector4UI Right = new Vector4UI(1, 0, 0, 0);
	public static readonly Vector4UI Up = new Vector4UI(0, 1, 0, 0);
	public static readonly Vector4UI RightTop = new Vector4UI(1, 1, 0, 0);
	public static readonly Vector4UI Front = new Vector4UI(0, 0, 1, 0);
	public static readonly Vector4UI Ana = new Vector4UI(0, 0, 0, 1);
	public static readonly Vector4UI AxisX = new Vector4UI(1, 0, 0, 0);
	public static readonly Vector4UI AxisY = new Vector4UI(0, 1, 0, 0);
	public static readonly Vector4UI AxisZ = new Vector4UI(0, 0, 1, 0);
	public static readonly Vector4UI AxisW = new Vector4UI(0, 0, 0, 1);
	public static readonly Vector4UI Double = new Vector4UI(2, 2, 2, 2);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Add(uint X, uint Y, uint Z, uint W) => new Vector4UI(this.X + X, this.Y + Y, this.Z + Z, this.W + W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Add(Vector4UI Other) => Add(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Add(uint S) => Add(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Sub(uint X, uint Y, uint Z, uint W) => new Vector4UI(this.X - X, this.Y - Y, this.Z - Z, this.W - W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Sub(Vector4UI Other) => Sub(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Sub(uint S) => Sub(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Mul(uint X, uint Y, uint Z, uint W) => new Vector4UI(this.X * X, this.Y * Y, this.Z * Z, this.W * W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Mul(Vector4UI Other) => Mul(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Mul(uint S) => Mul(S, S, S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Div(uint X, uint Y, uint Z, uint W) => new Vector4UI(this.X / X, this.Y / Y, this.Z / Z, this.W / W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Div(Vector4UI Other) => Div(Other.X, Other.Y, Other.Z, Other.W);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4UI Div(uint S) => Div(S, S, S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector4UI(" + ToShortString() + ")";
	public string ToShortString() => X + ", " + Y + ", " + Z + ", " + W;
	
	public bool Equals(Vector4UI Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W;
	public override bool Equals(object? Object) => Object is Vector4UI Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector4UI L, Vector4UI R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector4UI L, Vector4UI R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator +(Vector4UI L, Vector4UI R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator +(Vector4UI V, uint S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator +(uint S, Vector4UI V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator -(Vector4UI L, Vector4UI R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator -(Vector4UI V, uint S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator -(uint S, Vector4UI V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator *(Vector4UI L, Vector4UI R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator *(Vector4UI V, uint S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator *(uint S, Vector4UI V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator /(Vector4UI L, Vector4UI R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator /(Vector4UI V, uint S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector4UI operator /(uint S, Vector4UI V) => V / S;
}