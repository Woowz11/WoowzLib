namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 05.02.2026 2:02
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

	public byte BR => WL.System.Byte.ToColorByte(R);
	public byte BG => WL.System.Byte.ToColorByte(G);
	public byte BB => WL.System.Byte.ToColorByte(B);
	public byte BA => WL.System.Byte.ToColorByte(A);

	public ColorF SetR(float R){ this.R = R; return this; }
	public ColorF SetG(float G){ this.G = G; return this; }
	public ColorF SetB(float B){ this.B = B; return this; }
	public ColorF SetA(float A){ this.A = A; return this; }

	public ColorF Set(float R, float G, float B, float A){ this.R = R; this.G = G; this.B = B; this.A = A; return this; }
	
	public ColorF ToLightRed(){ return Set(1, 0.25f, 0.25f, 1); }
	public static ColorF LightRed => new ColorF().ToLightRed();
	public ColorF ToRed(){ return Set(1, 0, 0, 1); }
	public static ColorF Red => new ColorF().ToRed();
	public ColorF ToDarkRed(){ return Set(0.5f, 0, 0, 1); }
	public static ColorF DarkRed => new ColorF().ToDarkRed();
	public ColorF ToLightOrange(){ return Set(1, 0.75f, 0.25f, 1); }
	public static ColorF LightOrange => new ColorF().ToLightOrange();
	public ColorF ToOrange(){ return Set(1, 0.5f, 0, 1); }
	public static ColorF Orange => new ColorF().ToOrange();
	public ColorF ToDarkOrange(){ return Set(0.5f, 0.25f, 0, 1); }
	public static ColorF DarkOrange => new ColorF().ToDarkOrange();
	public ColorF ToBrown(){ return Set(0.5f, 0.25f, 0, 1); }
	public static ColorF Brown => new ColorF().ToBrown();
	public ColorF ToLightYellow(){ return Set(1, 1, 0.25f, 1); }
	public static ColorF LightYellow => new ColorF().ToLightYellow();
	public ColorF ToYellow(){ return Set(1, 1, 0, 1); }
	public static ColorF Yellow => new ColorF().ToYellow();
	public ColorF ToDarkYellow(){ return Set(0.5f, 0.5f, 0, 1); }
	public static ColorF DarkYellow => new ColorF().ToDarkYellow();
	public ColorF ToLightGreen(){ return Set(0.25f, 1, 0.25f, 1); }
	public static ColorF LightGreen => new ColorF().ToLightGreen();
	public ColorF ToGreen(){ return Set(0, 1, 0, 1); }
	public static ColorF Green => new ColorF().ToGreen();
	public ColorF ToDarkGreen(){ return Set(0, 0.5f, 0, 1); }
	public static ColorF DarkGreen => new ColorF().ToDarkGreen();
	public ColorF ToLightAqua(){ return Set(0.25f, 1, 1, 1); }
	public static ColorF LightAqua => new ColorF().ToLightAqua();
	public ColorF ToAqua(){ return Set(0, 1, 1, 1); }
	public static ColorF Aqua => new ColorF().ToAqua();
	public ColorF ToDarkAqua(){ return Set(0, 0.5f, 0.5f, 1); }
	public static ColorF DarkAqua => new ColorF().ToDarkAqua();
	public ColorF ToLightBlue(){ return Set(0.25f, 0.25f, 1, 1); }
	public static ColorF LightBlue => new ColorF().ToLightBlue();
	public ColorF ToBlue(){ return Set(0, 0, 1, 1); }
	public static ColorF Blue => new ColorF().ToBlue();
	public ColorF ToDarkBlue(){ return Set(0, 0, 0.5f, 1); }
	public static ColorF DarkBlue => new ColorF().ToDarkBlue();
	public ColorF ToLightPurple(){ return Set(0.75f, 0.25f, 1, 1); }
	public static ColorF LightPurple => new ColorF().ToLightPurple();
	public ColorF ToPurple(){ return Set(0.5f, 0, 1, 1); }
	public static ColorF Purple => new ColorF().ToPurple();
	public ColorF ToDarkPurple(){ return Set(0.25f, 0, 0.5f, 1); }
	public static ColorF DarkPurple => new ColorF().ToDarkPurple();
	public ColorF ToLightMagenta(){ return Set(1, 0.25f, 1, 1); }
	public static ColorF LightMagenta => new ColorF().ToLightMagenta();
	public ColorF ToMagenta(){ return Set(1, 0, 1, 1); }
	public static ColorF Magenta => new ColorF().ToMagenta();
	public ColorF ToDarkMagenta(){ return Set(0.5f, 0, 0.5f, 1); }
	public static ColorF DarkMagenta => new ColorF().ToDarkMagenta();
	public ColorF ToLightPink(){ return Set(1, 0.75f, 1, 1); }
	public static ColorF LightPink => new ColorF().ToLightPink();
	public ColorF ToPink(){ return Set(1, 0.5f, 1, 1); }
	public static ColorF Pink => new ColorF().ToPink();
	public ColorF ToDarkPink(){ return Set(0.5f, 0.25f, 0.5f, 1); }
	public static ColorF DarkPink => new ColorF().ToDarkPink();
	public ColorF ToWhite(){ return Set(1, 1, 1, 1); }
	public static ColorF White => new ColorF().ToWhite();
	public ColorF ToLightGray(){ return Set(0.75f, 0.75f, 0.75f, 1); }
	public static ColorF LightGray => new ColorF().ToLightGray();
	public ColorF ToGray(){ return Set(0.5f, 0.5f, 0.5f, 1); }
	public static ColorF Gray => new ColorF().ToGray();
	public ColorF ToDarkGray(){ return Set(0.25f, 0.25f, 0.25f, 1); }
	public static ColorF DarkGray => new ColorF().ToDarkGray();
	public ColorF ToBlack(){ return Set(0, 0, 0, 1); }
	public static ColorF Black => new ColorF().ToBlack();
	public ColorF ToTransparent(){ return Set(1, 1, 1, 0); }
	public static ColorF Transparent => new ColorF().ToTransparent();
	public ColorF ToOne(){ return Set(1, 1, 1, 1); }
	public static ColorF One => new ColorF().ToOne();
	public ColorF ToHalf(){ return Set(0.5f, 0.5f, 0.5f, 0.5f); }
	public static ColorF Half => new ColorF().ToHalf();
	public ColorF ToZero(){ return Set(0, 0, 0, 0); }
	public static ColorF Zero => new ColorF().ToZero();
	public ColorF ToRandom(){ return Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), 1); }
	public static ColorF Random => new ColorF().ToRandom();
	public ColorF ToFullRandom(){ return Set(WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1(), WL.Math.Random.Fast_0_1()); }
	public static ColorF FullRandom => new ColorF().ToFullRandom();

	public uint ToRGBA (){ return WL.System.Byte.RGBA(BR, BG, BB, BA); }
	public uint ToRGBiA(){ return WL.System.Byte.RGBA(BR, BG, BB, (byte)(255 - BA)); }
	public uint ToARGB (){ return WL.System.Byte.ABGR(BA, BB, BG, BR); }

	public ColorF Clone(){ return new ColorF(R,G,B,A); }

	#region Override

		public override string ToString(){
			return "ColorF(" + R + ", " + G + ", " + B + (A == 1 ? "" : ", " + A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorF other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorF A, ColorF B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorF A, ColorF B){
			return !(A == B);
		}
	
	#endregion
}