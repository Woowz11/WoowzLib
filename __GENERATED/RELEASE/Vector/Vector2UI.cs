/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.347, внутри класса "Vector.cs" */
using System.Runtime.CompilerServices;
/* ReSharper disable NonReadonlyMemberInGetHashCode */
namespace WLO.Vector;
public struct Vector2UI : IEquatable<Vector2UI>{
	public Vector2UI(uint X, uint Y){
		this.X = X;
		this.Y = Y;
	}
	public Vector2UI(uint XY) : this(XY, XY){}
	public Vector2UI(){}
	
	// ----------------------------------------------------------------------
	
	public uint X;
	public uint Y;
	
	public uint W{
		get => X;
		set => X = value;
	}
	public uint H{
		get => Y;
		set => Y = value;
	}
	
	public uint L{
		get => X;
		set => X = value;
	}
	public uint T{
		get => Y;
		set => Y = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector2UI Zero = new Vector2UI(0, 0);
	public static readonly Vector2UI One = new Vector2UI(1, 1);
	public static readonly Vector2UI Max = new Vector2UI(uint.MaxValue, uint.MaxValue);
	public static readonly Vector2UI Right = new Vector2UI(1, 0);
	public static readonly Vector2UI Up = new Vector2UI(0, 1);
	public static readonly Vector2UI RightTop = new Vector2UI(1, 1);
	public static readonly Vector2UI AxisX = new Vector2UI(1, 0);
	public static readonly Vector2UI AxisY = new Vector2UI(0, 1);
	public static readonly Vector2UI Double = new Vector2UI(2, 2);
	public static readonly Vector2UI Center = new Vector2UI(0, 0);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Add(uint X, uint Y) => new Vector2UI(this.X + X, this.Y + Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Add(Vector2UI Other) => Add(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Add(uint S) => Add(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Sub(uint X, uint Y) => new Vector2UI(this.X - X, this.Y - Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Sub(Vector2UI Other) => Sub(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Sub(uint S) => Sub(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Mul(uint X, uint Y) => new Vector2UI(this.X * X, this.Y * Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Mul(Vector2UI Other) => Mul(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Mul(uint S) => Mul(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Div(uint X, uint Y) => new Vector2UI(this.X / X, this.Y / Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Div(Vector2UI Other) => Div(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2UI Div(uint S) => Div(S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Vector2UI({ToShortString()})";
	public string ToShortString() => $"{X}, {Y}";
	public string ToPositionString() => $"{X}:{Y}";
	public string ToSizeString() => $"{W}x{H}";
	
	public bool Equals(Vector2UI Other) => X == Other.X && Y == Other.Y;
	public override bool Equals(object? Object) => Object is Vector2UI Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2UI L, Vector2UI R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2UI L, Vector2UI R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator +(Vector2UI L, Vector2UI R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator +(Vector2UI V, uint S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator +(uint S, Vector2UI V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator -(Vector2UI L, Vector2UI R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator -(Vector2UI V, uint S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator -(uint S, Vector2UI V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator *(Vector2UI L, Vector2UI R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator *(Vector2UI V, uint S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator *(uint S, Vector2UI V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator /(Vector2UI L, Vector2UI R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator /(Vector2UI V, uint S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2UI operator /(uint S, Vector2UI V) => V / S;
}