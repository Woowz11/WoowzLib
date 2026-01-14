namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorF{
	public static readonly Type Type = typeof(float);

	public ColorF(float R = 0, float G = 0, float B = 0, float A = 1){
		this.R = R; this.G = G; this.B = B; this.A = A; 
	}

	public float R;
	public float G;
	public float B;
	public float A;

	public ColorF Set(float R, float G, float B, float A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorF ToRed(){ return Set(1, 0, 0, 1); }
	public static ColorF Red => new ColorF().ToRed();
	public ColorF ToOrange(){ return Set(1, 0.5f, 0, 1); }
	public static ColorF Orange => new ColorF().ToOrange();
	public ColorF ToYellow(){ return Set(1, 1, 0, 1); }
	public static ColorF Yellow => new ColorF().ToYellow();
	public ColorF ToGreen(){ return Set(0, 1, 0, 1); }
	public static ColorF Green => new ColorF().ToGreen();
	public ColorF ToAqua(){ return Set(0, 1, 1, 1); }
	public static ColorF Aqua => new ColorF().ToAqua();
	public ColorF ToBlue(){ return Set(0, 0, 1, 1); }
	public static ColorF Blue => new ColorF().ToBlue();
	public ColorF ToPurple(){ return Set(0.5f, 0, 1, 1); }
	public static ColorF Purple => new ColorF().ToPurple();
	public ColorF ToMagenta(){ return Set(1, 0, 1, 1); }
	public static ColorF Magenta => new ColorF().ToMagenta();
	public ColorF ToPink(){ return Set(1, 0.5f, 1, 1); }
	public static ColorF Pink => new ColorF().ToPink();
	public ColorF ToWhite(){ return Set(1, 1, 1, 1); }
	public static ColorF White => new ColorF().ToWhite();
	public ColorF ToGray(){ return Set(0.5f, 0.5f, 0.5f, 1); }
	public static ColorF Gray => new ColorF().ToGray();
	public ColorF ToBlack(){ return Set(0, 0, 0, 1); }
	public static ColorF Black => new ColorF().ToBlack();
	public ColorF ToTransparent(){ return Set(0, 0, 0, 0); }
	public static ColorF Transparent => new ColorF().ToTransparent();


	#region Override

		public override string ToString(){
			return "ColorF(" + R + ", " + G + ", " + B + ", " + (A == 1 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorF other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorF A, ColorF B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorF A, ColorF B){
			return !(A == B);
		}
	
	#endregion
}