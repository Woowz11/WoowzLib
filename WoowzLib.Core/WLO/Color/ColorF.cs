namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 12.02.2026 19:38
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

	public byte BR => WL.Math.Byte.ToColorByte(R);
	public byte BG => WL.Math.Byte.ToColorByte(G);
	public byte BB => WL.Math.Byte.ToColorByte(B);
	public byte BA => WL.Math.Byte.ToColorByte(A);

	public ColorF SetR(float R){ this.R = R; return this; }
	public ColorF SetG(float G){ this.G = G; return this; }
	public ColorF SetB(float B){ this.B = B; return this; }
	public ColorF SetA(float A){ this.A = A; return this; }

	public ColorF Set(float R, float G, float B, float A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorF ToLightRed() => Set(1, 0.25f, 0.25f, 1);
	public static ColorF LightRed => new ColorF().ToLightRed();
	public ColorF ToRed() => Set(1, 0, 0, 1);
	public static ColorF Red => new ColorF().ToRed();
	public ColorF ToDarkRed() => Set(0.5f, 0, 0, 1);
	public static ColorF DarkRed => new ColorF().ToDarkRed();
	public ColorF ToLightOrange() => Set(1, 0.75f, 0.25f, 1);
	public static ColorF LightOrange => new ColorF().ToLightOrange();
	public ColorF ToOrange() => Set(1, 0.5f, 0, 1);
	public static ColorF Orange => new ColorF().ToOrange();
	public ColorF ToDarkOrange() => Set(0.5f, 0.25f, 0, 1);
	public static ColorF DarkOrange => new ColorF().ToDarkOrange();
	public ColorF ToBrown() => Set(0.5f, 0.25f, 0, 1);
	public static ColorF Brown => new ColorF().ToBrown();
	public ColorF ToLightYellow() => Set(1, 1, 0.25f, 1);
	public static ColorF LightYellow => new ColorF().ToLightYellow();
	public ColorF ToYellow() => Set(1, 1, 0, 1);
	public static ColorF Yellow => new ColorF().ToYellow();
	public ColorF ToDarkYellow() => Set(0.5f, 0.5f, 0, 1);
	public static ColorF DarkYellow => new ColorF().ToDarkYellow();
	public ColorF ToLightGreen() => Set(0.25f, 1, 0.25f, 1);
	public static ColorF LightGreen => new ColorF().ToLightGreen();
	public ColorF ToGreen() => Set(0, 1, 0, 1);
	public static ColorF Green => new ColorF().ToGreen();
	public ColorF ToDarkGreen() => Set(0, 0.5f, 0, 1);
	public static ColorF DarkGreen => new ColorF().ToDarkGreen();
	public ColorF ToLightAqua() => Set(0.25f, 1, 1, 1);
	public static ColorF LightAqua => new ColorF().ToLightAqua();
	public ColorF ToAqua() => Set(0, 1, 1, 1);
	public static ColorF Aqua => new ColorF().ToAqua();
	public ColorF ToDarkAqua() => Set(0, 0.5f, 0.5f, 1);
	public static ColorF DarkAqua => new ColorF().ToDarkAqua();
	public ColorF ToLightBlue() => Set(0.25f, 0.25f, 1, 1);
	public static ColorF LightBlue => new ColorF().ToLightBlue();
	public ColorF ToBlue() => Set(0, 0, 1, 1);
	public static ColorF Blue => new ColorF().ToBlue();
	public ColorF ToDarkBlue() => Set(0, 0, 0.5f, 1);
	public static ColorF DarkBlue => new ColorF().ToDarkBlue();
	public ColorF ToLightPurple() => Set(0.75f, 0.25f, 1, 1);
	public static ColorF LightPurple => new ColorF().ToLightPurple();
	public ColorF ToPurple() => Set(0.5f, 0, 1, 1);
	public static ColorF Purple => new ColorF().ToPurple();
	public ColorF ToDarkPurple() => Set(0.25f, 0, 0.5f, 1);
	public static ColorF DarkPurple => new ColorF().ToDarkPurple();
	public ColorF ToLightMagenta() => Set(1, 0.25f, 1, 1);
	public static ColorF LightMagenta => new ColorF().ToLightMagenta();
	public ColorF ToMagenta() => Set(1, 0, 1, 1);
	public static ColorF Magenta => new ColorF().ToMagenta();
	public ColorF ToDarkMagenta() => Set(0.5f, 0, 0.5f, 1);
	public static ColorF DarkMagenta => new ColorF().ToDarkMagenta();
	public ColorF ToLightPink() => Set(1, 0.75f, 1, 1);
	public static ColorF LightPink => new ColorF().ToLightPink();
	public ColorF ToPink() => Set(1, 0.5f, 1, 1);
	public static ColorF Pink => new ColorF().ToPink();
	public ColorF ToDarkPink() => Set(0.5f, 0.25f, 0.5f, 1);
	public static ColorF DarkPink => new ColorF().ToDarkPink();
	public ColorF ToWhite() => Set(1, 1, 1, 1);
	public static ColorF White => new ColorF().ToWhite();
	public ColorF ToLightGray() => Set(0.75f, 0.75f, 0.75f, 1);
	public static ColorF LightGray => new ColorF().ToLightGray();
	public ColorF ToGray() => Set(0.5f, 0.5f, 0.5f, 1);
	public static ColorF Gray => new ColorF().ToGray();
	public ColorF ToDarkGray() => Set(0.25f, 0.25f, 0.25f, 1);
	public static ColorF DarkGray => new ColorF().ToDarkGray();
	public ColorF ToBlack() => Set(0, 0, 0, 1);
	public static ColorF Black => new ColorF().ToBlack();
	public ColorF ToTransparent() => Set(1, 1, 1, 0);
	public static ColorF Transparent => new ColorF().ToTransparent();
	public ColorF ToOne() => Set(1, 1, 1, 1);
	public static ColorF One => new ColorF().ToOne();
	public ColorF ToHalf() => Set(0.5f, 0.5f, 0.5f, 0.5f);
	public static ColorF Half => new ColorF().ToHalf();
	public ColorF ToZero() => Set(0, 0, 0, 0);
	public static ColorF Zero => new ColorF().ToZero();
	public ColorF ToRandom() => Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), 1);
	public static ColorF Random => new ColorF().ToRandom();
	public ColorF ToFullRandom() => Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1());
	public static ColorF FullRandom => new ColorF().ToFullRandom();

	public uint ToRGBA () => WL.Math.Byte.RGBA(BR, BG, BB,              BA);
	public uint ToRGBiA() => WL.Math.Byte.RGBA(BR, BG, BB, (byte)(255 - BA));
	public uint ToARGB () => WL.Math.Byte.ABGR(BA, BB, BG,              BR);

	public ColorF Clone() => new ColorF(R, G, B, A);

	public static ColorF Lerp(ColorF A, ColorF B, float T) => new ColorF(WL.Math.Lerp(A.R, B.R, T), WL.Math.Lerp(A.G, B.G, T), WL.Math.Lerp(A.B, B.B, T), WL.Math.Lerp(A.A, B.A, T));

	public static ColorF FromHSV(float H, float S, float V){
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
		
		return new ColorF(
			(float)(R * 1),
			(float)(G * 1),
			(float)(B * 1)
		);
	}

	#region Override

		// ReSharper disable once CompareOfFloatsByEqualityOperator
		public override string ToString() => "ColorF(" + R + ", " + G + ", " + B + (A == 1 ? "" : ", " + A) + ")";
		
		public override bool Equals(object? Obj){
			if(Obj is not ColorF Other){ return false; }
			return R == Other.R && G == Other.G && B == Other.B && A == Other.A;
		}
		
		public override int GetHashCode() => HashCode.Combine(R, G, B, A);
		
		public static bool operator ==(ColorF A, ColorF B) => A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		
		public static bool operator !=(ColorF A, ColorF B) => !(A == B);
		
		public static ColorF operator +(ColorF A, ColorF B) => new ColorF(A.R + B.R, A.G + B.G, A.B + B.B, A.A);
		
		public static ColorF operator -(ColorF A, ColorF B) => new ColorF(A.R - B.R, A.G - B.G, A.B - B.B, A.A);
		
		public static ColorF operator *(ColorF A, ColorF B) => new ColorF(A.R * B.R, A.G * B.G, A.B * B.B, A.A);
		
		public static ColorF operator *(ColorF A, float B) => new ColorF(A.R * B, A.G * B, A.B * B, A.A);
	
	#endregion
}