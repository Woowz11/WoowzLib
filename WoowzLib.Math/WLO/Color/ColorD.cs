namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 06.02.2026 2:08
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
	
	public ColorD ToLightRed() => Set(1, 0.25, 0.25, 1);
	public static ColorD LightRed => new ColorD().ToLightRed();
	public ColorD ToRed() => Set(1, 0, 0, 1);
	public static ColorD Red => new ColorD().ToRed();
	public ColorD ToDarkRed() => Set(0.5, 0, 0, 1);
	public static ColorD DarkRed => new ColorD().ToDarkRed();
	public ColorD ToLightOrange() => Set(1, 0.75, 0.25, 1);
	public static ColorD LightOrange => new ColorD().ToLightOrange();
	public ColorD ToOrange() => Set(1, 0.5, 0, 1);
	public static ColorD Orange => new ColorD().ToOrange();
	public ColorD ToDarkOrange() => Set(0.5, 0.25, 0, 1);
	public static ColorD DarkOrange => new ColorD().ToDarkOrange();
	public ColorD ToBrown() => Set(0.5, 0.25, 0, 1);
	public static ColorD Brown => new ColorD().ToBrown();
	public ColorD ToLightYellow() => Set(1, 1, 0.25, 1);
	public static ColorD LightYellow => new ColorD().ToLightYellow();
	public ColorD ToYellow() => Set(1, 1, 0, 1);
	public static ColorD Yellow => new ColorD().ToYellow();
	public ColorD ToDarkYellow() => Set(0.5, 0.5, 0, 1);
	public static ColorD DarkYellow => new ColorD().ToDarkYellow();
	public ColorD ToLightGreen() => Set(0.25, 1, 0.25, 1);
	public static ColorD LightGreen => new ColorD().ToLightGreen();
	public ColorD ToGreen() => Set(0, 1, 0, 1);
	public static ColorD Green => new ColorD().ToGreen();
	public ColorD ToDarkGreen() => Set(0, 0.5, 0, 1);
	public static ColorD DarkGreen => new ColorD().ToDarkGreen();
	public ColorD ToLightAqua() => Set(0.25, 1, 1, 1);
	public static ColorD LightAqua => new ColorD().ToLightAqua();
	public ColorD ToAqua() => Set(0, 1, 1, 1);
	public static ColorD Aqua => new ColorD().ToAqua();
	public ColorD ToDarkAqua() => Set(0, 0.5, 0.5, 1);
	public static ColorD DarkAqua => new ColorD().ToDarkAqua();
	public ColorD ToLightBlue() => Set(0.25, 0.25, 1, 1);
	public static ColorD LightBlue => new ColorD().ToLightBlue();
	public ColorD ToBlue() => Set(0, 0, 1, 1);
	public static ColorD Blue => new ColorD().ToBlue();
	public ColorD ToDarkBlue() => Set(0, 0, 0.5, 1);
	public static ColorD DarkBlue => new ColorD().ToDarkBlue();
	public ColorD ToLightPurple() => Set(0.75, 0.25, 1, 1);
	public static ColorD LightPurple => new ColorD().ToLightPurple();
	public ColorD ToPurple() => Set(0.5, 0, 1, 1);
	public static ColorD Purple => new ColorD().ToPurple();
	public ColorD ToDarkPurple() => Set(0.25, 0, 0.5, 1);
	public static ColorD DarkPurple => new ColorD().ToDarkPurple();
	public ColorD ToLightMagenta() => Set(1, 0.25, 1, 1);
	public static ColorD LightMagenta => new ColorD().ToLightMagenta();
	public ColorD ToMagenta() => Set(1, 0, 1, 1);
	public static ColorD Magenta => new ColorD().ToMagenta();
	public ColorD ToDarkMagenta() => Set(0.5, 0, 0.5, 1);
	public static ColorD DarkMagenta => new ColorD().ToDarkMagenta();
	public ColorD ToLightPink() => Set(1, 0.75, 1, 1);
	public static ColorD LightPink => new ColorD().ToLightPink();
	public ColorD ToPink() => Set(1, 0.5, 1, 1);
	public static ColorD Pink => new ColorD().ToPink();
	public ColorD ToDarkPink() => Set(0.5, 0.25, 0.5, 1);
	public static ColorD DarkPink => new ColorD().ToDarkPink();
	public ColorD ToWhite() => Set(1, 1, 1, 1);
	public static ColorD White => new ColorD().ToWhite();
	public ColorD ToLightGray() => Set(0.75, 0.75, 0.75, 1);
	public static ColorD LightGray => new ColorD().ToLightGray();
	public ColorD ToGray() => Set(0.5, 0.5, 0.5, 1);
	public static ColorD Gray => new ColorD().ToGray();
	public ColorD ToDarkGray() => Set(0.25, 0.25, 0.25, 1);
	public static ColorD DarkGray => new ColorD().ToDarkGray();
	public ColorD ToBlack() => Set(0, 0, 0, 1);
	public static ColorD Black => new ColorD().ToBlack();
	public ColorD ToTransparent() => Set(1, 1, 1, 0);
	public static ColorD Transparent => new ColorD().ToTransparent();
	public ColorD ToOne() => Set(1, 1, 1, 1);
	public static ColorD One => new ColorD().ToOne();
	public ColorD ToHalf() => Set(0.5, 0.5, 0.5, 0.5);
	public static ColorD Half => new ColorD().ToHalf();
	public ColorD ToZero() => Set(0, 0, 0, 0);
	public static ColorD Zero => new ColorD().ToZero();
	public ColorD ToRandom() => Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), 1);
	public static ColorD Random => new ColorD().ToRandom();
	public ColorD ToFullRandom() => Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1());
	public static ColorD FullRandom => new ColorD().ToFullRandom();

	public uint ToRGBA () => WL.System.Byte.RGBA(BR, BG, BB,              BA);
	public uint ToRGBiA() => WL.System.Byte.RGBA(BR, BG, BB, (byte)(255 - BA));
	public uint ToARGB () => WL.System.Byte.ABGR(BA, BB, BG,              BR);

	public ColorD Clone() => new ColorD(R, G, B, A);

	public static ColorD Lerp(ColorD A, ColorD B, float T) => new ColorD(WL.Math.LerpD(A.R, B.R, T), WL.Math.LerpD(A.G, B.G, T), WL.Math.LerpD(A.B, B.B, T), WL.Math.LerpD(A.A, B.A, T));

	#region Override

		// ReSharper disable once CompareOfFloatsByEqualityOperator
		public override string ToString() => "ColorD(" + R + ", " + G + ", " + B + (A == 1 ? "" : ", " + A) + ")";
		
		public override bool Equals(object? Obj){
			if(Obj is not ColorD Other){ return false; }
			return R == Other.R && G == Other.G && B == Other.B && A == Other.A;
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