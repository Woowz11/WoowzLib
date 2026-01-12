namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorB(byte R = 0, byte G = 0, byte B = 0, byte A = 255){
	public readonly Type T = typeof(byte);

	public byte R;
	public byte G;
	public byte B;
	public byte A;

	public ColorB Set(byte R, byte G, byte B, byte A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorB ToRed(){ return Set(255, 0, 0, 255); }
	public static readonly ColorB Red = new ColorB().ToRed();
	public ColorB ToOrange(){ return Set(255, 127, 0, 255); }
	public static readonly ColorB Orange = new ColorB().ToOrange();
	public ColorB ToYellow(){ return Set(255, 255, 0, 255); }
	public static readonly ColorB Yellow = new ColorB().ToYellow();
	public ColorB ToGreen(){ return Set(0, 255, 0, 255); }
	public static readonly ColorB Green = new ColorB().ToGreen();
	public ColorB ToAqua(){ return Set(0, 255, 255, 255); }
	public static readonly ColorB Aqua = new ColorB().ToAqua();
	public ColorB ToBlue(){ return Set(0, 0, 255, 255); }
	public static readonly ColorB Blue = new ColorB().ToBlue();
	public ColorB ToPurple(){ return Set(127, 0, 255, 255); }
	public static readonly ColorB Purple = new ColorB().ToPurple();
	public ColorB ToMagenta(){ return Set(255, 0, 255, 255); }
	public static readonly ColorB Magenta = new ColorB().ToMagenta();
	public ColorB ToPink(){ return Set(255, 127, 255, 255); }
	public static readonly ColorB Pink = new ColorB().ToPink();
	public ColorB ToWhite(){ return Set(255, 255, 255, 255); }
	public static readonly ColorB White = new ColorB().ToWhite();
	public ColorB ToGray(){ return Set(127, 127, 127, 255); }
	public static readonly ColorB Gray = new ColorB().ToGray();
	public ColorB ToBlack(){ return Set(0, 0, 0, 255); }
	public static readonly ColorB Black = new ColorB().ToBlack();
	public ColorB ToTransparent(){ return Set(0, 0, 0, 0); }
	public static readonly ColorB Transparent = new ColorB().ToTransparent();


	#region Override

		public override string ToString(){
			return "ColorB(" + R + ", " + G + ", " + B + ", " + (A == 255 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorB other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorB A, ColorB B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorB A, ColorB B){
			return !(A == B);
		}
	
	#endregion
}