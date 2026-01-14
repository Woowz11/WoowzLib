namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorD{
	public static readonly Type Type = typeof(double);

	public ColorD(double R = 0, double G = 0, double B = 0, double A = 1){
		this.R = R; this.G = G; this.B = B; this.A = A; 
	}

	public double R;
	public double G;
	public double B;
	public double A;

	public ColorD Set(double R, double G, double B, double A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorD ToRed(){ return Set(1, 0, 0, 1); }
	public static ColorD Red => new ColorD().ToRed();
	public ColorD ToOrange(){ return Set(1, 0.5, 0, 1); }
	public static ColorD Orange => new ColorD().ToOrange();
	public ColorD ToYellow(){ return Set(1, 1, 0, 1); }
	public static ColorD Yellow => new ColorD().ToYellow();
	public ColorD ToGreen(){ return Set(0, 1, 0, 1); }
	public static ColorD Green => new ColorD().ToGreen();
	public ColorD ToAqua(){ return Set(0, 1, 1, 1); }
	public static ColorD Aqua => new ColorD().ToAqua();
	public ColorD ToBlue(){ return Set(0, 0, 1, 1); }
	public static ColorD Blue => new ColorD().ToBlue();
	public ColorD ToPurple(){ return Set(0.5, 0, 1, 1); }
	public static ColorD Purple => new ColorD().ToPurple();
	public ColorD ToMagenta(){ return Set(1, 0, 1, 1); }
	public static ColorD Magenta => new ColorD().ToMagenta();
	public ColorD ToPink(){ return Set(1, 0.5, 1, 1); }
	public static ColorD Pink => new ColorD().ToPink();
	public ColorD ToWhite(){ return Set(1, 1, 1, 1); }
	public static ColorD White => new ColorD().ToWhite();
	public ColorD ToGray(){ return Set(0.5, 0.5, 0.5, 1); }
	public static ColorD Gray => new ColorD().ToGray();
	public ColorD ToBlack(){ return Set(0, 0, 0, 1); }
	public static ColorD Black => new ColorD().ToBlack();
	public ColorD ToTransparent(){ return Set(0, 0, 0, 0); }
	public static ColorD Transparent => new ColorD().ToTransparent();


	#region Override

		public override string ToString(){
			return "ColorD(" + R + ", " + G + ", " + B + ", " + (A == 1 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorD other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorD A, ColorD B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorD A, ColorD B){
			return !(A == B);
		}
	
	#endregion
}