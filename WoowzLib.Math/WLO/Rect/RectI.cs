namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 1:12
/// </summary>
public struct RectI{
	public static readonly Type Type = typeof(int);

	public RectI(int X, int Y, int Width, int Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectI(int Width, int Height){
		this.Width = Width; this.Height = Height;
	}
	public RectI(){
		Width = 128; Height = 128;
	}

	public int X;
	public int Y;
	
	public int Width {
		get => __Width;
		set{
			if(__Width == value){ return; }
		
			if(value <= 0){ throw new Exception("Ширина не может быть <= 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private int __Width;
	
	public int Height {
		get => __Height;
		set{
			if(__Height == value){ return; }
		
			if(value <= 0){ throw new Exception("Высота не может быть <= 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private int __Height;

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