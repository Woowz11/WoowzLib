namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorI{
	public static readonly Type Type = typeof(int);

	public ColorI(int R = 0, int G = 0, int B = 0, int A = 255){
		this.R = R; this.G = G; this.B = B; this.A = A; 
	}

	public int R;
	public int G;
	public int B;
	public int A;

	public ColorI Set(int R, int G, int B, int A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorI ToRed(){ return Set(255, 0, 0, 255); }
	public static ColorI Red => new ColorI().ToRed();
	public ColorI ToOrange(){ return Set(255, 127, 0, 255); }
	public static ColorI Orange => new ColorI().ToOrange();
	public ColorI ToYellow(){ return Set(255, 255, 0, 255); }
	public static ColorI Yellow => new ColorI().ToYellow();
	public ColorI ToGreen(){ return Set(0, 255, 0, 255); }
	public static ColorI Green => new ColorI().ToGreen();
	public ColorI ToAqua(){ return Set(0, 255, 255, 255); }
	public static ColorI Aqua => new ColorI().ToAqua();
	public ColorI ToBlue(){ return Set(0, 0, 255, 255); }
	public static ColorI Blue => new ColorI().ToBlue();
	public ColorI ToPurple(){ return Set(127, 0, 255, 255); }
	public static ColorI Purple => new ColorI().ToPurple();
	public ColorI ToMagenta(){ return Set(255, 0, 255, 255); }
	public static ColorI Magenta => new ColorI().ToMagenta();
	public ColorI ToPink(){ return Set(255, 127, 255, 255); }
	public static ColorI Pink => new ColorI().ToPink();
	public ColorI ToWhite(){ return Set(255, 255, 255, 255); }
	public static ColorI White => new ColorI().ToWhite();
	public ColorI ToGray(){ return Set(127, 127, 127, 255); }
	public static ColorI Gray => new ColorI().ToGray();
	public ColorI ToBlack(){ return Set(0, 0, 0, 255); }
	public static ColorI Black => new ColorI().ToBlack();
	public ColorI ToTransparent(){ return Set(0, 0, 0, 0); }
	public static ColorI Transparent => new ColorI().ToTransparent();


	#region Override

		public override string ToString(){
			return "ColorI(" + R + ", " + G + ", " + B + ", " + (A == 255 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorI other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorI A, ColorI B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorI A, ColorI B){
			return !(A == B);
		}
	
	#endregion
}