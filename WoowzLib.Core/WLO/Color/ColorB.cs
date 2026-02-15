namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.02.2026 3:55
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

	public byte BR => WL.Math.Byte.ToColorByte(R);
	public byte BG => WL.Math.Byte.ToColorByte(G);
	public byte BB => WL.Math.Byte.ToColorByte(B);
	public byte BA => WL.Math.Byte.ToColorByte(A);

	public ColorB SetR(byte R){ this.R = R; return this; }
	public ColorB SetG(byte G){ this.G = G; return this; }
	public ColorB SetB(byte B){ this.B = B; return this; }
	public ColorB SetA(byte A){ this.A = A; return this; }

	public ColorB Set(byte R, byte G, byte B, byte A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorB ToLightRed() => Set(255, 63, 63, 255);
	public static ColorB LightRed => new ColorB().ToLightRed();
	public ColorB ToRed() => Set(255, 0, 0, 255);
	public static ColorB Red => new ColorB().ToRed();
	public ColorB ToDarkRed() => Set(128, 0, 0, 255);
	public static ColorB DarkRed => new ColorB().ToDarkRed();
	public ColorB ToLightOrange() => Set(255, 191, 63, 255);
	public static ColorB LightOrange => new ColorB().ToLightOrange();
	public ColorB ToOrange() => Set(255, 128, 0, 255);
	public static ColorB Orange => new ColorB().ToOrange();
	public ColorB ToDarkOrange() => Set(128, 63, 0, 255);
	public static ColorB DarkOrange => new ColorB().ToDarkOrange();
	public ColorB ToBrown() => Set(128, 63, 0, 255);
	public static ColorB Brown => new ColorB().ToBrown();
	public ColorB ToLightYellow() => Set(255, 255, 63, 255);
	public static ColorB LightYellow => new ColorB().ToLightYellow();
	public ColorB ToYellow() => Set(255, 255, 0, 255);
	public static ColorB Yellow => new ColorB().ToYellow();
	public ColorB ToDarkYellow() => Set(128, 128, 0, 255);
	public static ColorB DarkYellow => new ColorB().ToDarkYellow();
	public ColorB ToLightGreen() => Set(63, 255, 63, 255);
	public static ColorB LightGreen => new ColorB().ToLightGreen();
	public ColorB ToGreen() => Set(0, 255, 0, 255);
	public static ColorB Green => new ColorB().ToGreen();
	public ColorB ToDarkGreen() => Set(0, 128, 0, 255);
	public static ColorB DarkGreen => new ColorB().ToDarkGreen();
	public ColorB ToLightAqua() => Set(63, 255, 255, 255);
	public static ColorB LightAqua => new ColorB().ToLightAqua();
	public ColorB ToAqua() => Set(0, 255, 255, 255);
	public static ColorB Aqua => new ColorB().ToAqua();
	public ColorB ToDarkAqua() => Set(0, 128, 128, 255);
	public static ColorB DarkAqua => new ColorB().ToDarkAqua();
	public ColorB ToLightBlue() => Set(63, 63, 255, 255);
	public static ColorB LightBlue => new ColorB().ToLightBlue();
	public ColorB ToBlue() => Set(0, 0, 255, 255);
	public static ColorB Blue => new ColorB().ToBlue();
	public ColorB ToDarkBlue() => Set(0, 0, 128, 255);
	public static ColorB DarkBlue => new ColorB().ToDarkBlue();
	public ColorB ToLightPurple() => Set(191, 63, 255, 255);
	public static ColorB LightPurple => new ColorB().ToLightPurple();
	public ColorB ToPurple() => Set(128, 0, 255, 255);
	public static ColorB Purple => new ColorB().ToPurple();
	public ColorB ToDarkPurple() => Set(63, 0, 128, 255);
	public static ColorB DarkPurple => new ColorB().ToDarkPurple();
	public ColorB ToLightMagenta() => Set(255, 63, 255, 255);
	public static ColorB LightMagenta => new ColorB().ToLightMagenta();
	public ColorB ToMagenta() => Set(255, 0, 255, 255);
	public static ColorB Magenta => new ColorB().ToMagenta();
	public ColorB ToDarkMagenta() => Set(128, 0, 128, 255);
	public static ColorB DarkMagenta => new ColorB().ToDarkMagenta();
	public ColorB ToLightPink() => Set(255, 191, 255, 255);
	public static ColorB LightPink => new ColorB().ToLightPink();
	public ColorB ToPink() => Set(255, 128, 255, 255);
	public static ColorB Pink => new ColorB().ToPink();
	public ColorB ToDarkPink() => Set(128, 63, 128, 255);
	public static ColorB DarkPink => new ColorB().ToDarkPink();
	public ColorB ToWhite() => Set(255, 255, 255, 255);
	public static ColorB White => new ColorB().ToWhite();
	public ColorB ToLightGray() => Set(191, 191, 191, 255);
	public static ColorB LightGray => new ColorB().ToLightGray();
	public ColorB ToGray() => Set(128, 128, 128, 255);
	public static ColorB Gray => new ColorB().ToGray();
	public ColorB ToDarkGray() => Set(63, 63, 63, 255);
	public static ColorB DarkGray => new ColorB().ToDarkGray();
	public ColorB ToBlack() => Set(0, 0, 0, 255);
	public static ColorB Black => new ColorB().ToBlack();
	public ColorB ToTransparent() => Set(255, 255, 255, 0);
	public static ColorB Transparent => new ColorB().ToTransparent();
	public ColorB ToOne() => Set(255, 255, 255, 255);
	public static ColorB One => new ColorB().ToOne();
	public ColorB ToHalf() => Set(128, 128, 128, 128);
	public static ColorB Half => new ColorB().ToHalf();
	public ColorB ToZero() => Set(0, 0, 0, 0);
	public static ColorB Zero => new ColorB().ToZero();
	public ColorB ToRandom() => Set(WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), 1);
	public static ColorB Random => new ColorB().ToRandom();
	public ColorB ToFullRandom() => Set(WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte(), WL.Math.Random.Fast_Byte());
	public static ColorB FullRandom => new ColorB().ToFullRandom();

	public uint ToRGBA () => WL.Math.Byte.RGBA(BR, BG, BB,              BA);
	public uint ToRGBiA() => WL.Math.Byte.RGBA(BR, BG, BB, (byte)(255 - BA));
	public uint ToARGB () => WL.Math.Byte.ABGR(BA, BB, BG,              BR);

	public ColorB Clone() => new ColorB(R, G, B, A);

	public static ColorB Lerp(ColorB A, ColorB B, float T) => new ColorB(WL.Math.LerpB(A.R, B.R, T), WL.Math.LerpB(A.G, B.G, T), WL.Math.LerpB(A.B, B.B, T), WL.Math.LerpB(A.A, B.A, T));

	public static ColorB FromHSV(float H, float S, float V){
		float R = 0, G = 0, B = 0;
		
		int I = (int)(H * 6);
		float F = H * 6 - I;
		float P = V * (1 - S);
		float Q = V * (1 - F * S);
		float T = V * (1 - (1 - F) * S);
		
		switch(I % 6){
			case 0: R = V; G = T; B = P; break;
			case 1: R = Q; G = V; B = P; break;
			case 2: R = P; G = V; B = T; break;
			case 3: R = P; G = Q; B = V; break;
			case 4: R = T; G = P; B = V; break;
			case 5: R = V; G = P; B = Q; break;
		}
		
		return new ColorB(
			(byte)(R * 255),
			(byte)(G * 255),
			(byte)(B * 255)
		);
	}

	#region Override

		// ReSharper disable once CompareOfFloatsByEqualityOperator
		public override string ToString() => "ColorB(" + R + ", " + G + ", " + B + (A == 255 ? "" : ", " + A) + ")";
		
		public override bool Equals(object? Obj){
			if(Obj is not ColorB Other){ return false; }
			return R == Other.R && G == Other.G && B == Other.B && A == Other.A;
		}
		
		public override int GetHashCode() => HashCode.Combine(R, G, B, A);
		
		public static bool operator ==(ColorB A, ColorB B) => A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		
		public static bool operator !=(ColorB A, ColorB B) => !(A == B);
		
		public static ColorB operator +(ColorB A, ColorB B) => new ColorB(WL.Math.AddB(A.R, B.R), WL.Math.AddB(A.G, B.G), WL.Math.AddB(A.B, B.B), A.A);
		
		public static ColorB operator -(ColorB A, ColorB B) => new ColorB(WL.Math.SubB(A.R, B.R), WL.Math.SubB(A.G, B.G), WL.Math.SubB(A.B, B.B), A.A);
		
		public static ColorB operator *(ColorB A, ColorB B) => new ColorB(WL.Math.MulB(A.R, B.R), WL.Math.MulB(A.G, B.G), WL.Math.MulB(A.B, B.B), WL.Math.MulB(A.A, B.A));
		
		public static ColorB operator *(ColorB A, byte B) => new ColorB(WL.Math.MulB(A.R, B), WL.Math.MulB(A.G, B), WL.Math.MulB(A.B, B), A.A);
	
	#endregion
}