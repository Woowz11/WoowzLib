/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.120, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector2F : IEquatable<Vector2F>{
	public Vector2F(float X, float Y){
		this.X = X;
		this.Y = Y;
	}
	public Vector2F(float XY) : this(XY, XY){}
	public Vector2F() : this(0){}
	
	// ----------------------------------------------------------------------
	
	public float X = 0;
	public float Y = 0;
	public float W{
		get => X;
		set => X = value;
	}
	public float H{
		get => Y;
		set => Y = value;
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Vector2F Zero = new Vector2F(0, 0);
	public Vector2F ToZero = new Vector2F(0, 0);
	public static readonly Vector2F One = new Vector2F(1, 1);
	public Vector2F ToOne = new Vector2F(1, 1);
	public static readonly Vector2F NOne = new Vector2F(-1, -1);
	public Vector2F ToNOne = new Vector2F(-1, -1);
	public static readonly Vector2F Half = new Vector2F(0.5f, 0.5f);
	public Vector2F ToHalf = new Vector2F(0.5f, 0.5f);
	public static readonly Vector2F Right = new Vector2F(1, 0);
	public Vector2F ToRight = new Vector2F(1, 0);
	public static readonly Vector2F Left = new Vector2F(-1, 0);
	public Vector2F ToLeft = new Vector2F(-1, 0);
	public static readonly Vector2F Up = new Vector2F(0, 1);
	public Vector2F ToUp = new Vector2F(0, 1);
	public static readonly Vector2F Down = new Vector2F(0, -1);
	public Vector2F ToDown = new Vector2F(0, -1);
	public static readonly Vector2F AxisX = new Vector2F(1, 0);
	public Vector2F ToAxisX = new Vector2F(1, 0);
	public static readonly Vector2F AxisY = new Vector2F(0, 1);
	public Vector2F ToAxisY = new Vector2F(0, 1);
	public static readonly Vector2F Double = new Vector2F(2, 2);
	public Vector2F ToDouble = new Vector2F(2, 2);
	public static readonly Vector2F Quarter = new Vector2F(0.25f, 0.25f);
	public Vector2F ToQuarter = new Vector2F(0.25f, 0.25f);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Add(float X, float Y) => new Vector2F(this.X + X, this.Y + Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Add(Vector2F Other) => Add(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Add(float S) => Add(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Sub(float X, float Y) => new Vector2F(this.X - X, this.Y - Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Sub(Vector2F Other) => Sub(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Sub(float S) => Sub(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Mul(float X, float Y) => new Vector2F(this.X * X, this.Y * Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Mul(Vector2F Other) => Mul(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Mul(float S) => Mul(S, S);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Div(float X, float Y) => new Vector2F(this.X / X, this.Y / Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Div(Vector2F Other) => Div(Other.X, Other.Y);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2F Div(float S) => Div(S, S);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Vector2F(" + X + ", " + Y + ")";
	public string ToShortString() => X + ", " + Y;
	public string ToPositionString() => X + ":" + Y;
	public string ToSizeString() => W + "x" + H;
	
	public bool Equals(Vector2F Other) => X == Other.X && Y == Other.Y;
	public override bool Equals(object? Object) => Object is Vector2F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2F L, Vector2F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2F L, Vector2F R) => !L.Equals(R);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator +(Vector2F L, Vector2F R) => L.Add(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator +(Vector2F V, float S) => V.Add(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator +(float S, Vector2F V) => V + S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator -(Vector2F L, Vector2F R) => L.Sub(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator -(Vector2F V, float S) => V.Sub(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator -(float S, Vector2F V) => V - S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator *(Vector2F L, Vector2F R) => L.Mul(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator *(Vector2F V, float S) => V.Mul(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator *(float S, Vector2F V) => V * S;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator /(Vector2F L, Vector2F R) => L.Div(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator /(Vector2F V, float S) => V.Div(S);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2F operator /(float S, Vector2F V) => V / S;
}