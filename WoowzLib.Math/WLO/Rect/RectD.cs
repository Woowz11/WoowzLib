namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct RectD{
	public static readonly Type Type = typeof(double);

	public RectD(double X, double Y, double Width, double Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectD(double Width, double Height) : this(0, 0, Width, Height){}

	public double X;
	public double Y;
	
	public double Width {
		get => __Width;
		set{
			if(__Width == value){ return; }
		
			if(value <= 0){ throw new Exception("Ширина не может быть <= 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private double __Width;
	
	public double Height {
		get => __Height;
		set{
			if(__Height == value){ return; }
		
			if(value <= 0){ throw new Exception("Высота не может быть <= 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private double __Height;

	#region Override

		public override string ToString(){
			return "RectD(" + X + ":" + Y + ", " + Width + "x" + Height + ")";
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