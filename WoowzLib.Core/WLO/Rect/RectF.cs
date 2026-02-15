namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.02.2026 3:55
/// </summary>
public struct RectF{
	public static readonly Type Type = typeof(float);

	public RectF(float X, float Y, float Width, float Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectF(Vector2F Position, Vector2F Size){
		this.Position = Position; this.Size = Size;
	}
	public RectF(float Width, float Height){
		this.Width = Width; this.Height = Height;
	}
	public RectF(Vector2F Size){
		this.Size = Size;
	}
	public RectF(){
		Width = 128; Height = 128;
	}


	public float X;
	public float Y;
	
	public Vector2F Position{
		get => new Vector2F(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	
	public float Width {
		get => __Width;
		set{
			if(value < 0){ throw new Exception("Ширина не может быть < 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private float __Width;
	
	public float Height {
		get => __Height;
		set{
			if(value < 0){ throw new Exception("Высота не может быть < 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private float __Height;
	
	public Vector2F Size{
		get => new Vector2F(Width, Height);
		set{
			Width  = value.X;
			Height = value.Y;
		}
	}
	
	public float Left   => X;
	public float Top    => Y;
	public float Right  => X + Width ;
	public float Bottom => Y + Height;
	


	/// <summary>
	/// Находится ли указанная точка внутри Rect?
	/// </summary>
	public bool Inside(Vector2F Vector){ return Vector.X >= Left && Vector.X < Right && Vector.Y >= Top && Vector.Y < Bottom; }

	#region Override

		public override string ToString(){
			return "RectF(" + ToShortString() + ")";
		}
		
		public string ToShortString(){
			return X + ":" + Y + ", " + Width + "x" + Height;
		}
		
		public override bool Equals(object? Obj){
			if(Obj is not RectF Other){ return false; }
			return X == Other.X && Y == Other.Y && Width == Other.Width && Height == Other.Height;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Width, Height);
		}
		
		public static bool operator ==(RectF A, RectF B){
			return A.X == B.X && A.Y == B.Y && A.Width == B.Width && A.Height == B.Height;
		}
		
		public static bool operator !=(RectF A, RectF B){
			return !(A == B);
		}
	#endregion
}