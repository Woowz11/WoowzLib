namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 11.02.2026 18:25
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

	public byte BR => WL.Math.Byte.ToColorByte(R);
	public byte BG => WL.Math.Byte.ToColorByte(G);
	public byte BB => WL.Math.Byte.ToColorByte(B);
	public byte BA => WL.Math.Byte.ToColorByte(A);

	public ColorI SetR(int R){ this.R = R; return this; }
	public ColorI SetG(int G){ this.G = G; return this; }
	public ColorI SetB(int B){ this.B = B; return this; }
	public ColorI SetA(int A){ this.A = A; return this; }

	public ColorI Set(int R, int G, int B, int A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorI ToLightRed() => Set(255, 63, 63, 255);
	public static ColorI LightRed => new ColorI().ToLightRed();
	public ColorI ToRed() => Set(255, 0, 0, 255);
	public static ColorI Red => new ColorI().ToRed();
	public ColorI ToDarkRed() => Set(127, 0, 0, 255);
	public static ColorI DarkRed => new ColorI().ToDarkRed();
	public ColorI ToLightOrange() => Set(255, 191, 63, 255);
	public static ColorI LightOrange => new ColorI().ToLightOrange();
	public ColorI ToOrange() => Set(255, 127, 0, 255);
	public static ColorI Orange => new ColorI().ToOrange();
	public ColorI ToDarkOrange() => Set(127, 63, 0, 255);
	public static ColorI DarkOrange => new ColorI().ToDarkOrange();
	public ColorI ToBrown() => Set(127, 63, 0, 255);
	public static ColorI Brown => new ColorI().ToBrown();
	public ColorI ToLightYellow() => Set(255, 255, 63, 255);
	public static ColorI LightYellow => new ColorI().ToLightYellow();
	public ColorI ToYellow() => Set(255, 255, 0, 255);
	public static ColorI Yellow => new ColorI().ToYellow();
	public ColorI ToDarkYellow() => Set(127, 127, 0, 255);
	public static ColorI DarkYellow => new ColorI().ToDarkYellow();
	public ColorI ToLightGreen() => Set(63, 255, 63, 255);
	public static ColorI LightGreen => new ColorI().ToLightGreen();
	public ColorI ToGreen() => Set(0, 255, 0, 255);
	public static ColorI Green => new ColorI().ToGreen();
	public ColorI ToDarkGreen() => Set(0, 127, 0, 255);
	public static ColorI DarkGreen => new ColorI().ToDarkGreen();
	public ColorI ToLightAqua() => Set(63, 255, 255, 255);
	public static ColorI LightAqua => new ColorI().ToLightAqua();
	public ColorI ToAqua() => Set(0, 255, 255, 255);
	public static ColorI Aqua => new ColorI().ToAqua();
	public ColorI ToDarkAqua() => Set(0, 127, 127, 255);
	public static ColorI DarkAqua => new ColorI().ToDarkAqua();
	public ColorI ToLightBlue() => Set(63, 63, 255, 255);
	public static ColorI LightBlue => new ColorI().ToLightBlue();
	public ColorI ToBlue() => Set(0, 0, 255, 255);
	public static ColorI Blue => new ColorI().ToBlue();
	public ColorI ToDarkBlue() => Set(0, 0, 127, 255);
	public static ColorI DarkBlue => new ColorI().ToDarkBlue();
	public ColorI ToLightPurple() => Set(191, 63, 255, 255);
	public static ColorI LightPurple => new ColorI().ToLightPurple();
	public ColorI ToPurple() => Set(127, 0, 255, 255);
	public static ColorI Purple => new ColorI().ToPurple();
	public ColorI ToDarkPurple() => Set(63, 0, 127, 255);
	public static ColorI DarkPurple => new ColorI().ToDarkPurple();
	public ColorI ToLightMagenta() => Set(255, 63, 255, 255);
	public static ColorI LightMagenta => new ColorI().ToLightMagenta();
	public ColorI ToMagenta() => Set(255, 0, 255, 255);
	public static ColorI Magenta => new ColorI().ToMagenta();
	public ColorI ToDarkMagenta() => Set(127, 0, 127, 255);
	public static ColorI DarkMagenta => new ColorI().ToDarkMagenta();
	public ColorI ToLightPink() => Set(255, 191, 255, 255);
	public static ColorI LightPink => new ColorI().ToLightPink();
	public ColorI ToPink() => Set(255, 127, 255, 255);
	public static ColorI Pink => new ColorI().ToPink();
	public ColorI ToDarkPink() => Set(127, 63, 127, 255);
	public static ColorI DarkPink => new ColorI().ToDarkPink();
	public ColorI ToWhite() => Set(255, 255, 255, 255);
	public static ColorI White => new ColorI().ToWhite();
	public ColorI ToLightGray() => Set(191, 191, 191, 255);
	public static ColorI LightGray => new ColorI().ToLightGray();
	public ColorI ToGray() => Set(127, 127, 127, 255);
	public static ColorI Gray => new ColorI().ToGray();
	public ColorI ToDarkGray() => Set(63, 63, 63, 255);
	public static ColorI DarkGray => new ColorI().ToDarkGray();
	public ColorI ToBlack() => Set(0, 0, 0, 255);
	public static ColorI Black => new ColorI().ToBlack();
	public ColorI ToTransparent() => Set(255, 255, 255, 0);
	public static ColorI Transparent => new ColorI().ToTransparent();
	public ColorI ToOne() => Set(255, 255, 255, 255);
	public static ColorI One => new ColorI().ToOne();
	public ColorI ToHalf() => Set(127, 127, 127, 127);
	public static ColorI Half => new ColorI().ToHalf();
	public ColorI ToZero() => Set(0, 0, 0, 0);
	public static ColorI Zero => new ColorI().ToZero();
	public ColorI ToRandom() => Set(WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), 1);
	public static ColorI Random => new ColorI().ToRandom();
	public ColorI ToFullRandom() => Set(WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255), WL.Math.Random.Fast_Int(0, 255));
	public static ColorI FullRandom => new ColorI().ToFullRandom();

	public uint ToRGBA () => WL.Math.Byte.RGBA(BR, BG, BB,              BA);
	public uint ToRGBiA() => WL.Math.Byte.RGBA(BR, BG, BB, (byte)(255 - BA));
	public uint ToARGB () => WL.Math.Byte.ABGR(BA, BB, BG,              BR);

	public ColorI Clone() => new ColorI(R, G, B, A);

	public static ColorI Lerp(ColorI A, ColorI B, float T) => new ColorI(WL.Math.LerpI(A.R, B.R, T), WL.Math.LerpI(A.G, B.G, T), WL.Math.LerpI(A.B, B.B, T), WL.Math.LerpI(A.A, B.A, T));

	#region Override

		// ReSharper disable once CompareOfFloatsByEqualityOperator
		public override string ToString() => "ColorI(" + R + ", " + G + ", " + B + (A == 255 ? "" : ", " + A) + ")";
		
		public override bool Equals(object? Obj){
			if(Obj is not ColorI Other){ return false; }
			return R == Other.R && G == Other.G && B == Other.B && A == Other.A;
		}
		
		public override int GetHashCode() => HashCode.Combine(R, G, B, A);
		
		public static bool operator ==(ColorI A, ColorI B) => A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		
		public static bool operator !=(ColorI A, ColorI B) => !(A == B);
		
		public static ColorI operator +(ColorI A, ColorI B) => new ColorI(A.R + B.R, A.G + B.G, A.B + B.B, A.A);
		
		public static ColorI operator -(ColorI A, ColorI B) => new ColorI(A.R - B.R, A.G - B.G, A.B - B.B, A.A);
		
		public static ColorI operator *(ColorI A, ColorI B) => new ColorI(A.R * B.R, A.G * B.G, A.B * B.B, A.A);
		
		public static ColorI operator *(ColorI A, int B) => new ColorI(A.R * B, A.G * B, A.B * B, A.A);
	
	#endregion
}