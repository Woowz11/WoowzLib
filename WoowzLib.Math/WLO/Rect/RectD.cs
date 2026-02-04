namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 04.02.2026 23:05
/// </summary>
public struct RectD{
	public static readonly Type Type = typeof(double);

	public RectD(double X, double Y, double Width, double Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectD(Vector2D Position, Vector2D Size){
		this.Position = Position; this.Size = Size;
	}
	public RectD(double Width, double Height){
		this.Width = Width; this.Height = Height;
	}
	public RectD(Vector2D Size){
		this.Size = Size;
	}
	public RectD(){
		Width = 128; Height = 128;
	}


	public double X;
	public double Y;
	
	public Vector2D Position{
		get => new Vector2D(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	
	public double Width {
		get => __Width;
		set{
			if(value < 0){ throw new Exception("Ширина не может быть < 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private double __Width;
	
	public double Height {
		get => __Height;
		set{
			if(value < 0){ throw new Exception("Высота не может быть < 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private double __Height;
	
	public Vector2D Size{
		get => new Vector2D(Width, Height);
		set{
			Width  = value.X;
			Height = value.Y;
		}
	}
	
	public double Left   => X;
	public double Top    => Y;
	public double Right  => X + Width ;
	public double Bottom => Y + Height;
	


	/// <summary>
	/// Находится ли указанная точка внутри Rect?
	/// </summary>
	public bool Inside(Vector2D Vector){ return Vector.X >= Left && Vector.X < Right && Vector.Y >= Top && Vector.Y < Bottom; }

	#region Override

		public override string ToString(){
			return "RectD(" + ToShortString() + ")";
		}
		
		public string ToShortString(){
			return X + ":" + Y + ", " + Width + "x" + Height;
		}
		
		public override bool Equals(object? obj){
			if(obj is not RectD other){ return false; }
			return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Width, Height);
		}
		
		public static bool operator ==(RectD A, RectD B){
			return A.X == B.X && A.Y == B.Y && A.Width == B.Width && A.Height == B.Height;
		}
		
		public static bool operator !=(RectD A, RectD B){
			return !(A == B);
		}
	#endregion
}