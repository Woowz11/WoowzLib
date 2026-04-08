/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.329, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color3B : IEquatable<Color3B>{
	public Color3B(byte R = 0, byte G = 0, byte B = 0){
		this.R = R;
		this.G = G;
		this.B = B;
	}
	
	// ----------------------------------------------------------------------
	
	public byte R;
	public byte G;
	public byte B;
	
	public uint RGB{
		get => (uint)(R << 16 | G << 8 | B);
		set{
			R = (byte)((value >> 16) & 0xFF);
			G = (byte)((value >> 8) & 0xFF);
			B = (byte)(value & 0xFF);
		}
	}
	public uint BGR{
		get => (uint)(B << 16 | G << 8 | R);
		set{
			B = (byte)((value >> 16) & 0xFF);
			G = (byte)((value >> 8) & 0xFF);
			R = (byte)(value & 0xFF);
		}
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Color3B Red = new Color3B(255, 0, 0);
	public static readonly Color3B Orange = new Color3B(255, 127, 0);
	public static readonly Color3B Yellow = new Color3B(255, 255, 0);
	public static readonly Color3B Lime = new Color3B(127, 255, 0);
	public static readonly Color3B Green = new Color3B(0, 255, 0);
	public static readonly Color3B Aqua = new Color3B(0, 255, 255);
	public static readonly Color3B Water = new Color3B(0, 127, 255);
	public static readonly Color3B Blue = new Color3B(0, 0, 255);
	public static readonly Color3B Purple = new Color3B(127, 0, 255);
	public static readonly Color3B Magenta = new Color3B(255, 0, 255);
	public static readonly Color3B Brown = new Color3B(127, 63, 0);
	public static readonly Color3B DarkRed = new Color3B(127, 0, 0);
	public static readonly Color3B DarkYellow = new Color3B(127, 127, 0);
	public static readonly Color3B DarkGreen = new Color3B(0, 127, 0);
	public static readonly Color3B DarkAqua = new Color3B(0, 127, 127);
	public static readonly Color3B DarkBlue = new Color3B(0, 0, 127);
	public static readonly Color3B DarkPurple = new Color3B(63, 0, 127);
	public static readonly Color3B DarkMagenta = new Color3B(127, 0, 127);
	public static readonly Color3B Pink = new Color3B(255, 127, 255);
	public static readonly Color3B White = new Color3B(255, 255, 255);
	public static readonly Color3B Silver = new Color3B(191, 191, 191);
	public static readonly Color3B Gray = new Color3B(127, 127, 127);
	public static readonly Color3B Charcoal = new Color3B(63, 63, 63);
	public static readonly Color3B Black = new Color3B(0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Color3B(" + ToShortString() + ")";
	public string ToShortString() => R + ", " + G + ", " + B;
	
	public bool Equals(Color3B Other) => R == Other.R && G == Other.G && B == Other.B;
	public override bool Equals(object? Object) => Object is Color3B Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color3B L, Color3B R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color3B L, Color3B R) => !L.Equals(R);
}