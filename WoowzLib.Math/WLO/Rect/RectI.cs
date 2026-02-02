namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 02.02.2026 20:30
/// </summary>
public struct RectI{
	public static readonly Type Type = typeof(int);

	public RectI(int X, int Y, int Width, int Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectI(Vector2I Position, Vector2I Size){
		this.Position = Position; this.Size = Size;
	}
	public RectI(int Width, int Height){
		this.Width = Width; this.Height = Height;
	}
	public RectI(Vector2I Size){
		this.Size = Size;
	}
	public RectI(){
		Width = 128; Height = 128;
	}
	public RectI(WL.System.Native.Windows.RECT Rect){ X = Rect.left; Y = Rect.top; Width = Rect.right - Rect.left; Height = Rect.bottom - Rect.top; }

	public int X;
	public int Y;
	
	public Vector2I Position{
		get => new Vector2I(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	
	public int Width {
		get => __Width;
		set{
			if(value < 0){ throw new Exception("Ширина не может быть < 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private int __Width;
	
	public int Height {
		get => __Height;
		set{
			if(value < 0){ throw new Exception("Высота не может быть < 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private int __Height;
	
	public Vector2I Size{
		get => new Vector2I(Width, Height);
		set{
			Width  = value.X;
			Height = value.Y;
		}
	}
	
	public int Left   => X;
	public int Top    => Y;
	public int Right  => X + Width ;
	public int Bottom => Y + Height;
	
	public WL.System.Native.Windows.RECT ToRect(){ return new WL.System.Native.Windows.RECT{ left = Left, top = Top, right = Right, bottom = Bottom }; }

	/// <summary>
	/// Находится ли указанная точка внутри Rect?
	/// </summary>
	public bool Inside(Vector2I Vector){ return Vector.X >= Left && Vector.X < Right && Vector.Y >= Top && Vector.Y < Bottom; }

	#region Override

		public override string ToString(){
			return "RectI(" + X + ":" + Y + ", " + Width + "x" + Height + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not RectI other){ return false; }
			return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Width, Height);
		}
		
		public static bool operator ==(RectI A, RectI B){
			return A.X == B.X && A.Y == B.Y && A.Width == B.Width && A.Height == B.Height;
		}
		
		public static bool operator !=(RectI A, RectI B){
			return !(A == B);
		}
	#endregion
}