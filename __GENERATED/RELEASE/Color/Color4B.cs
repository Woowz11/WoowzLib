/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.311, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color4B : IEquatable<Color4B>{
	public Color4B(byte R = 0, byte G = 0, byte B = 0, byte A = 255){
		this.R = R;
		this.G = G;
		this.B = B;
		this.A = A;
	}
	
	// ----------------------------------------------------------------------
	
	public byte R;
	public byte G;
	public byte B;
	public byte A;
	
	public uint RGBA{
		get => (uint)(R << 24 | G << 16 | B << 8 | A);
		set{
			R = (byte)((value >> 24) & 0xFF);
			G = (byte)((value >> 16) & 0xFF);
			B = (byte)((value >> 8) & 0xFF);
			A = (byte)(value & 0xFF);
		}
	}
	public uint ABGR{
		get => (uint)(A << 24 | B << 16 | G << 8 | R);
		set{
			A = (byte)((value >> 24) & 0xFF);
			B = (byte)((value >> 16) & 0xFF);
			G = (byte)((value >> 8) & 0xFF);
			R = (byte)(value & 0xFF);
		}
	}
	/// <summary>
	/// Подходит для WINAPI
	/// </summary>
	public uint AiBGR{
		get => (uint)((255 - A) << 24 | B << 16 | G << 8 | R);
		set{
			A = (byte)(255 - ((value >> 24) & 0xFF));
			B = (byte)((value >> 16) & 0xFF);
			G = (byte)((value >> 8) & 0xFF);
			R = (byte)(value & 0xFF);
		}
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Color4B Red = new Color4B(255, 0, 0, 255);
	public static readonly Color4B Orange = new Color4B(255, 127, 0, 255);
	public static readonly Color4B Yellow = new Color4B(255, 255, 0, 255);
	public static readonly Color4B Lime = new Color4B(127, 255, 0, 255);
	public static readonly Color4B Green = new Color4B(0, 255, 0, 255);
	public static readonly Color4B Aqua = new Color4B(0, 255, 255, 255);
	public static readonly Color4B Water = new Color4B(0, 127, 255, 255);
	public static readonly Color4B Blue = new Color4B(0, 0, 255, 255);
	public static readonly Color4B Purple = new Color4B(127, 0, 255, 255);
	public static readonly Color4B Magenta = new Color4B(255, 0, 255, 255);
	public static readonly Color4B Brown = new Color4B(127, 63, 0, 255);
	public static readonly Color4B DarkRed = new Color4B(127, 0, 0, 255);
	public static readonly Color4B DarkYellow = new Color4B(127, 127, 0, 255);
	public static readonly Color4B DarkGreen = new Color4B(0, 127, 0, 255);
	public static readonly Color4B DarkAqua = new Color4B(0, 127, 127, 255);
	public static readonly Color4B DarkBlue = new Color4B(0, 0, 127, 255);
	public static readonly Color4B DarkPurple = new Color4B(63, 0, 127, 255);
	public static readonly Color4B DarkMagenta = new Color4B(127, 0, 127, 255);
	public static readonly Color4B Pink = new Color4B(255, 127, 255, 255);
	public static readonly Color4B White = new Color4B(255, 255, 255, 255);
	public static readonly Color4B Silver = new Color4B(191, 191, 191, 255);
	public static readonly Color4B Gray = new Color4B(127, 127, 127, 255);
	public static readonly Color4B Charcoal = new Color4B(63, 63, 63, 255);
	public static readonly Color4B Black = new Color4B(0, 0, 0, 255);
	public static readonly Color4B Transparent = new Color4B(0, 0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Color4B(" + ToShortString() + ")";
	public string ToShortString() => R + ", " + G + ", " + B + ", " + A;
	
	public bool Equals(Color4B Other) => R == Other.R && G == Other.G && B == Other.B && A == Other.A;
	public override bool Equals(object? Object) => Object is Color4B Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B, A);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color4B L, Color4B R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color4B L, Color4B R) => !L.Equals(R);
}