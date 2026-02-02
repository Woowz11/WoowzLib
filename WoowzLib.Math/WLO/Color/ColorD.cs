namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 02.02.2026 23:01
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

	public byte BR => WL.System.Byte.ToColorByte(R);
	public byte BG => WL.System.Byte.ToColorByte(G);
	public byte BB => WL.System.Byte.ToColorByte(B);
	public byte BA => WL.System.Byte.ToColorByte(A);

	public ColorD SetR(double R){ this.R = R; return this; }
	public ColorD SetG(double G){ this.G = G; return this; }
	public ColorD SetB(double B){ this.B = B; return this; }
	public ColorD SetA(double A){ this.A = A; return this; }

	public ColorD Set(double R, double G, double B, double A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorD ToLightRed(){ return Set(1, 0.25, 0.25, 1); }
	public static ColorD LightRed => new ColorD().ToLightRed();
	public ColorD ToRed(){ return Set(1, 0, 0, 1); }
	public static ColorD Red => new ColorD().ToRed();
	public ColorD ToDarkRed(){ return Set(0.5, 0, 0, 1); }
	public static ColorD DarkRed => new ColorD().ToDarkRed();
	public ColorD ToLightOrange(){ return Set(1, 0.75, 0.25, 1); }
	public static ColorD LightOrange => new ColorD().ToLightOrange();
	public ColorD ToOrange(){ return Set(1, 0.5, 0, 1); }
	public static ColorD Orange => new ColorD().ToOrange();
	public ColorD ToDarkOrange(){ return Set(0.5, 0.25, 0, 1); }
	public static ColorD DarkOrange => new ColorD().ToDarkOrange();
	public ColorD ToBrown(){ return Set(0.5, 0.25, 0, 1); }
	public static ColorD Brown => new ColorD().ToBrown();
	public ColorD ToLightYellow(){ return Set(1, 1, 0.25, 1); }
	public static ColorD LightYellow => new ColorD().ToLightYellow();
	public ColorD ToYellow(){ return Set(1, 1, 0, 1); }
	public static ColorD Yellow => new ColorD().ToYellow();
	public ColorD ToDarkYellow(){ return Set(0.5, 0.5, 0, 1); }
	public static ColorD DarkYellow => new ColorD().ToDarkYellow();
	public ColorD ToLightGreen(){ return Set(0.25, 1, 0.25, 1); }
	public static ColorD LightGreen => new ColorD().ToLightGreen();
	public ColorD ToGreen(){ return Set(0, 1, 0, 1); }
	public static ColorD Green => new ColorD().ToGreen();
	public ColorD ToDarkGreen(){ return Set(0, 0.5, 0, 1); }
	public static ColorD DarkGreen => new ColorD().ToDarkGreen();
	public ColorD ToLightAqua(){ return Set(0.25, 1, 1, 1); }
	public static ColorD LightAqua => new ColorD().ToLightAqua();
	public ColorD ToAqua(){ return Set(0, 1, 1, 1); }
	public static ColorD Aqua => new ColorD().ToAqua();
	public ColorD ToDarkAqua(){ return Set(0, 0.5, 0.5, 1); }
	public static ColorD DarkAqua => new ColorD().ToDarkAqua();
	public ColorD ToLightBlue(){ return Set(0.25, 0.25, 1, 1); }
	public static ColorD LightBlue => new ColorD().ToLightBlue();
	public ColorD ToBlue(){ return Set(0, 0, 1, 1); }
	public static ColorD Blue => new ColorD().ToBlue();
	public ColorD ToDarkBlue(){ return Set(0, 0, 0.5, 1); }
	public static ColorD DarkBlue => new ColorD().ToDarkBlue();
	public ColorD ToLightPurple(){ return Set(0.75, 0.25, 1, 1); }
	public static ColorD LightPurple => new ColorD().ToLightPurple();
	public ColorD ToPurple(){ return Set(0.5, 0, 1, 1); }
	public static ColorD Purple => new ColorD().ToPurple();
	public ColorD ToDarkPurple(){ return Set(0.25, 0, 0.5, 1); }
	public static ColorD DarkPurple => new ColorD().ToDarkPurple();
	public ColorD ToLightMagenta(){ return Set(1, 0.25, 1, 1); }
	public static ColorD LightMagenta => new ColorD().ToLightMagenta();
	public ColorD ToMagenta(){ return Set(1, 0, 1, 1); }
	public static ColorD Magenta => new ColorD().ToMagenta();
	public ColorD ToDarkMagenta(){ return Set(0.5, 0, 0.5, 1); }
	public static ColorD DarkMagenta => new ColorD().ToDarkMagenta();
	public ColorD ToLightPink(){ return Set(1, 0.75, 1, 1); }
	public static ColorD LightPink => new ColorD().ToLightPink();
	public ColorD ToPink(){ return Set(1, 0.5, 1, 1); }
	public static ColorD Pink => new ColorD().ToPink();
	public ColorD ToDarkPink(){ return Set(0.5, 0.25, 0.5, 1); }
	public static ColorD DarkPink => new ColorD().ToDarkPink();
	public ColorD ToWhite(){ return Set(1, 1, 1, 1); }
	public static ColorD White => new ColorD().ToWhite();
	public ColorD ToLightGray(){ return Set(0.75, 0.75, 0.75, 1); }
	public static ColorD LightGray => new ColorD().ToLightGray();
	public ColorD ToGray(){ return Set(0.5, 0.5, 0.5, 1); }
	public static ColorD Gray => new ColorD().ToGray();
	public ColorD ToDarkGray(){ return Set(0.25, 0.25, 0.25, 1); }
	public static ColorD DarkGray => new ColorD().ToDarkGray();
	public ColorD ToBlack(){ return Set(0, 0, 0, 1); }
	public static ColorD Black => new ColorD().ToBlack();
	public ColorD ToTransparent(){ return Set(1, 1, 1, 0); }
	public static ColorD Transparent => new ColorD().ToTransparent();
	public ColorD ToOne(){ return Set(1, 1, 1, 1); }
	public static ColorD One => new ColorD().ToOne();
	public ColorD ToHalf(){ return Set(0.5, 0.5, 0.5, 0.5); }
	public static ColorD Half => new ColorD().ToHalf();
	public ColorD ToZero(){ return Set(0, 0, 0, 0); }
	public static ColorD Zero => new ColorD().ToZero();
	public ColorD ToRandom(){ return Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), 1); }
	public static ColorD Random => new ColorD().ToRandom();
	public ColorD ToFullRandom(){ return Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1()); }
	public static ColorD FullRandom => new ColorD().ToFullRandom();

	public uint ToRGBA (){ return WL.System.Byte.RGBA(BR, BG, BB, BA); }
	public uint ToRGBiA(){ return WL.System.Byte.RGBA(BR, BG, BB, (byte)(255 - BA)); }
	public uint ToARGB (){ return WL.System.Byte.ABGR(BA, BB, BG, BR); }

	public ColorD Clone(){ return new ColorD(R,G,B,A); }

	#region Override

		public override string ToString(){
			return "ColorD(" + R + ", " + G + ", " + B + (A == 1 ? "" : ", " + A) + ")";
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