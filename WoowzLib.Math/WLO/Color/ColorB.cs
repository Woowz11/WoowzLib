namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 05.02.2026 2:02
/// </summary>
public struct ColorB{
	public static readonly Type Type = typeof(byte);

	public ColorB(byte R = 0, byte G = 0, byte B = 0, byte A = 255){
		this.R = R; this.G = G; this.B = B; this.A = A; 
	}

	public byte R;
	public byte G;
	public byte B;
	public byte A;

	public byte BR => WL.System.Byte.ToColorByte(R);
	public byte BG => WL.System.Byte.ToColorByte(G);
	public byte BB => WL.System.Byte.ToColorByte(B);
	public byte BA => WL.System.Byte.ToColorByte(A);

	public ColorB SetR(byte R){ this.R = R; return this; }
	public ColorB SetG(byte G){ this.G = G; return this; }
	public ColorB SetB(byte B){ this.B = B; return this; }
	public ColorB SetA(byte A){ this.A = A; return this; }

	public ColorB Set(byte R, byte G, byte B, byte A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorB ToLightRed(){ return Set(255, 63, 63, 255); }
	public static ColorB LightRed => new ColorB().ToLightRed();
	public ColorB ToRed(){ return Set(255, 0, 0, 255); }
	public static ColorB Red => new ColorB().ToRed();
	public ColorB ToDarkRed(){ return Set(127, 0, 0, 255); }
	public static ColorB DarkRed => new ColorB().ToDarkRed();
	public ColorB ToLightOrange(){ return Set(255, 191, 63, 255); }
	public static ColorB LightOrange => new ColorB().ToLightOrange();
	public ColorB ToOrange(){ return Set(255, 127, 0, 255); }
	public static ColorB Orange => new ColorB().ToOrange();
	public ColorB ToDarkOrange(){ return Set(127, 63, 0, 255); }
	public static ColorB DarkOrange => new ColorB().ToDarkOrange();
	public ColorB ToBrown(){ return Set(127, 63, 0, 255); }
	public static ColorB Brown => new ColorB().ToBrown();
	public ColorB ToLightYellow(){ return Set(255, 255, 63, 255); }
	public static ColorB LightYellow => new ColorB().ToLightYellow();
	public ColorB ToYellow(){ return Set(255, 255, 0, 255); }
	public static ColorB Yellow => new ColorB().ToYellow();
	public ColorB ToDarkYellow(){ return Set(127, 127, 0, 255); }
	public static ColorB DarkYellow => new ColorB().ToDarkYellow();
	public ColorB ToLightGreen(){ return Set(63, 255, 63, 255); }
	public static ColorB LightGreen => new ColorB().ToLightGreen();
	public ColorB ToGreen(){ return Set(0, 255, 0, 255); }
	public static ColorB Green => new ColorB().ToGreen();
	public ColorB ToDarkGreen(){ return Set(0, 127, 0, 255); }
	public static ColorB DarkGreen => new ColorB().ToDarkGreen();
	public ColorB ToLightAqua(){ return Set(63, 255, 255, 255); }
	public static ColorB LightAqua => new ColorB().ToLightAqua();
	public ColorB ToAqua(){ return Set(0, 255, 255, 255); }
	public static ColorB Aqua => new ColorB().ToAqua();
	public ColorB ToDarkAqua(){ return Set(0, 127, 127, 255); }
	public static ColorB DarkAqua => new ColorB().ToDarkAqua();
	public ColorB ToLightBlue(){ return Set(63, 63, 255, 255); }
	public static ColorB LightBlue => new ColorB().ToLightBlue();
	public ColorB ToBlue(){ return Set(0, 0, 255, 255); }
	public static ColorB Blue => new ColorB().ToBlue();
	public ColorB ToDarkBlue(){ return Set(0, 0, 127, 255); }
	public static ColorB DarkBlue => new ColorB().ToDarkBlue();
	public ColorB ToLightPurple(){ return Set(191, 63, 255, 255); }
	public static ColorB LightPurple => new ColorB().ToLightPurple();
	public ColorB ToPurple(){ return Set(127, 0, 255, 255); }
	public static ColorB Purple => new ColorB().ToPurple();
	public ColorB ToDarkPurple(){ return Set(63, 0, 127, 255); }
	public static ColorB DarkPurple => new ColorB().ToDarkPurple();
	public ColorB ToLightMagenta(){ return Set(255, 63, 255, 255); }
	public static ColorB LightMagenta => new ColorB().ToLightMagenta();
	public ColorB ToMagenta(){ return Set(255, 0, 255, 255); }
	public static ColorB Magenta => new ColorB().ToMagenta();
	public ColorB ToDarkMagenta(){ return Set(127, 0, 127, 255); }
	public static ColorB DarkMagenta => new ColorB().ToDarkMagenta();
	public ColorB ToLightPink(){ return Set(255, 191, 255, 255); }
	public static ColorB LightPink => new ColorB().ToLightPink();
	public ColorB ToPink(){ return Set(255, 127, 255, 255); }
	public static ColorB Pink => new ColorB().ToPink();
	public ColorB ToDarkPink(){ return Set(127, 63, 127, 255); }
	public static ColorB DarkPink => new ColorB().ToDarkPink();
	public ColorB ToWhite(){ return Set(255, 255, 255, 255); }
	public static ColorB White => new ColorB().ToWhite();
	public ColorB ToLightGray(){ return Set(191, 191, 191, 255); }
	public static ColorB LightGray => new ColorB().ToLightGray();
	public ColorB ToGray(){ return Set(127, 127, 127, 255); }
	public static ColorB Gray => new ColorB().ToGray();
	public ColorB ToDarkGray(){ return Set(63, 63, 63, 255); }
	public static ColorB DarkGray => new ColorB().ToDarkGray();
	public ColorB ToBlack(){ return Set(0, 0, 0, 255); }
	public static ColorB Black => new ColorB().ToBlack();
	public ColorB ToTransparent(){ return Set(255, 255, 255, 0); }
	public static ColorB Transparent => new ColorB().ToTransparent();
	public ColorB ToOne(){ return Set(255, 255, 255, 255); }
	public static ColorB One => new ColorB().ToOne();
	public ColorB ToHalf(){ return Set(127, 127, 127, 127); }
	public static ColorB Half => new ColorB().ToHalf();
	public ColorB ToZero(){ return Set(0, 0, 0, 0); }
	public static ColorB Zero => new ColorB().ToZero();
	public ColorB ToRandom(){ return Set(WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), 1); }
	public static ColorB Random => new ColorB().ToRandom();
	public ColorB ToFullRandom(){ return Set(WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte()); }
	public static ColorB FullRandom => new ColorB().ToFullRandom();

	public uint ToRGBA (){ return WL.System.Byte.RGBA(BR, BG, BB, BA); }
	public uint ToRGBiA(){ return WL.System.Byte.RGBA(BR, BG, BB, (byte)(255 - BA)); }
	public uint ToARGB (){ return WL.System.Byte.ABGR(BA, BB, BG, BR); }

	public ColorB Clone(){ return new ColorB(R,G,B,A); }

	#region Override

		public override string ToString(){
			return "ColorB(" + R + ", " + G + ", " + B + (A == 255 ? "" : ", " + A) + ")";
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