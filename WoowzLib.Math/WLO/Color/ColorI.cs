namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 05.02.2026 2:02
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

	public byte BR => WL.System.Byte.ToColorByte(R);
	public byte BG => WL.System.Byte.ToColorByte(G);
	public byte BB => WL.System.Byte.ToColorByte(B);
	public byte BA => WL.System.Byte.ToColorByte(A);

	public ColorI SetR(int R){ this.R = R; return this; }
	public ColorI SetG(int G){ this.G = G; return this; }
	public ColorI SetB(int B){ this.B = B; return this; }
	public ColorI SetA(int A){ this.A = A; return this; }

	public ColorI Set(int R, int G, int B, int A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorI ToLightRed(){ return Set(255, 63, 63, 255); }
	public static ColorI LightRed => new ColorI().ToLightRed();
	public ColorI ToRed(){ return Set(255, 0, 0, 255); }
	public static ColorI Red => new ColorI().ToRed();
	public ColorI ToDarkRed(){ return Set(127, 0, 0, 255); }
	public static ColorI DarkRed => new ColorI().ToDarkRed();
	public ColorI ToLightOrange(){ return Set(255, 191, 63, 255); }
	public static ColorI LightOrange => new ColorI().ToLightOrange();
	public ColorI ToOrange(){ return Set(255, 127, 0, 255); }
	public static ColorI Orange => new ColorI().ToOrange();
	public ColorI ToDarkOrange(){ return Set(127, 63, 0, 255); }
	public static ColorI DarkOrange => new ColorI().ToDarkOrange();
	public ColorI ToBrown(){ return Set(127, 63, 0, 255); }
	public static ColorI Brown => new ColorI().ToBrown();
	public ColorI ToLightYellow(){ return Set(255, 255, 63, 255); }
	public static ColorI LightYellow => new ColorI().ToLightYellow();
	public ColorI ToYellow(){ return Set(255, 255, 0, 255); }
	public static ColorI Yellow => new ColorI().ToYellow();
	public ColorI ToDarkYellow(){ return Set(127, 127, 0, 255); }
	public static ColorI DarkYellow => new ColorI().ToDarkYellow();
	public ColorI ToLightGreen(){ return Set(63, 255, 63, 255); }
	public static ColorI LightGreen => new ColorI().ToLightGreen();
	public ColorI ToGreen(){ return Set(0, 255, 0, 255); }
	public static ColorI Green => new ColorI().ToGreen();
	public ColorI ToDarkGreen(){ return Set(0, 127, 0, 255); }
	public static ColorI DarkGreen => new ColorI().ToDarkGreen();
	public ColorI ToLightAqua(){ return Set(63, 255, 255, 255); }
	public static ColorI LightAqua => new ColorI().ToLightAqua();
	public ColorI ToAqua(){ return Set(0, 255, 255, 255); }
	public static ColorI Aqua => new ColorI().ToAqua();
	public ColorI ToDarkAqua(){ return Set(0, 127, 127, 255); }
	public static ColorI DarkAqua => new ColorI().ToDarkAqua();
	public ColorI ToLightBlue(){ return Set(63, 63, 255, 255); }
	public static ColorI LightBlue => new ColorI().ToLightBlue();
	public ColorI ToBlue(){ return Set(0, 0, 255, 255); }
	public static ColorI Blue => new ColorI().ToBlue();
	public ColorI ToDarkBlue(){ return Set(0, 0, 127, 255); }
	public static ColorI DarkBlue => new ColorI().ToDarkBlue();
	public ColorI ToLightPurple(){ return Set(191, 63, 255, 255); }
	public static ColorI LightPurple => new ColorI().ToLightPurple();
	public ColorI ToPurple(){ return Set(127, 0, 255, 255); }
	public static ColorI Purple => new ColorI().ToPurple();
	public ColorI ToDarkPurple(){ return Set(63, 0, 127, 255); }
	public static ColorI DarkPurple => new ColorI().ToDarkPurple();
	public ColorI ToLightMagenta(){ return Set(255, 63, 255, 255); }
	public static ColorI LightMagenta => new ColorI().ToLightMagenta();
	public ColorI ToMagenta(){ return Set(255, 0, 255, 255); }
	public static ColorI Magenta => new ColorI().ToMagenta();
	public ColorI ToDarkMagenta(){ return Set(127, 0, 127, 255); }
	public static ColorI DarkMagenta => new ColorI().ToDarkMagenta();
	public ColorI ToLightPink(){ return Set(255, 191, 255, 255); }
	public static ColorI LightPink => new ColorI().ToLightPink();
	public ColorI ToPink(){ return Set(255, 127, 255, 255); }
	public static ColorI Pink => new ColorI().ToPink();
	public ColorI ToDarkPink(){ return Set(127, 63, 127, 255); }
	public static ColorI DarkPink => new ColorI().ToDarkPink();
	public ColorI ToWhite(){ return Set(255, 255, 255, 255); }
	public static ColorI White => new ColorI().ToWhite();
	public ColorI ToLightGray(){ return Set(191, 191, 191, 255); }
	public static ColorI LightGray => new ColorI().ToLightGray();
	public ColorI ToGray(){ return Set(127, 127, 127, 255); }
	public static ColorI Gray => new ColorI().ToGray();
	public ColorI ToDarkGray(){ return Set(63, 63, 63, 255); }
	public static ColorI DarkGray => new ColorI().ToDarkGray();
	public ColorI ToBlack(){ return Set(0, 0, 0, 255); }
	public static ColorI Black => new ColorI().ToBlack();
	public ColorI ToTransparent(){ return Set(255, 255, 255, 0); }
	public static ColorI Transparent => new ColorI().ToTransparent();
	public ColorI ToOne(){ return Set(255, 255, 255, 255); }
	public static ColorI One => new ColorI().ToOne();
	public ColorI ToHalf(){ return Set(127, 127, 127, 127); }
	public static ColorI Half => new ColorI().ToHalf();
	public ColorI ToZero(){ return Set(0, 0, 0, 0); }
	public static ColorI Zero => new ColorI().ToZero();
	public ColorI ToRandom(){ return Set(WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), 1); }
	public static ColorI Random => new ColorI().ToRandom();
	public ColorI ToFullRandom(){ return Set(WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255)); }
	public static ColorI FullRandom => new ColorI().ToFullRandom();

	public uint ToRGBA (){ return WL.System.Byte.RGBA(BR, BG, BB, BA); }
	public uint ToRGBiA(){ return WL.System.Byte.RGBA(BR, BG, BB, (byte)(255 - BA)); }
	public uint ToARGB (){ return WL.System.Byte.ABGR(BA, BB, BG, BR); }

	public ColorI Clone(){ return new ColorI(R,G,B,A); }

	#region Override

		public override string ToString(){
			return "ColorI(" + R + ", " + G + ", " + B + (A == 255 ? "" : ", " + A) + ")";
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