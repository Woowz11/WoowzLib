namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 13:30
/// </summary>
public struct RectF{
	public static readonly Type Type = typeof(float);

	public RectF(float X, float Y, float Width, float Height){
		this.X = X; this.Y = Y; this.Width = Width; this.Height = Height;
	}
	public RectF(float Width, float Height){
		this.Width = Width; this.Height = Height;
	}
	public RectF(){
		Width = 128; Height = 128;
	}

	public float X;
	public float Y;
	
	public float Width {
		get => __Width;
		set{
			if(__Width == value){ return; }
		
			if(value <= 0){ throw new Exception("Ширина не может быть <= 0 у [" + this + "]!"); }
			__Width = value;
		}
	}
	private float __Width;
	
	public float Height {
		get => __Height;
		set{
			if(__Height == value){ return; }
		
			if(value <= 0){ throw new Exception("Высота не может быть <= 0 у [" + this + "]!"); }
			__Height = value;
		}
	}
	private float __Height;

	#region Override

		public override string ToString(){
			return "RectF(" + X + ":" + Y + ", " + Width + "x" + Height + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not RectF other){ return false; }
			return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
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