/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.355, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color4F : IEquatable<Color4F>{
	public Color4F(float R = 0, float G = 0, float B = 0, float A = 1f){
		this.R = R;
		this.G = G;
		this.B = B;
		this.A = A;
	}
	
	// ----------------------------------------------------------------------
	
	private float __R;
	public float R{
		get => __R;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет R выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__R = value;
		}
	}
	private float __G;
	public float G{
		get => __G;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет G выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__G = value;
		}
	}
	private float __B;
	public float B{
		get => __B;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет B выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__B = value;
		}
	}
	private float __A;
	public float A{
		get => __A;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет A выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__A = value;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Color4F Red = new Color4F(1f, 0, 0, 1f);
	public static readonly Color4F Orange = new Color4F(1f, 0.5f, 0, 1f);
	public static readonly Color4F Yellow = new Color4F(1f, 1f, 0, 1f);
	public static readonly Color4F Lime = new Color4F(0.5f, 1f, 0, 1f);
	public static readonly Color4F Green = new Color4F(0, 1f, 0, 1f);
	public static readonly Color4F Aqua = new Color4F(0, 1f, 1f, 1f);
	public static readonly Color4F Water = new Color4F(0, 0.5f, 1f, 1f);
	public static readonly Color4F Blue = new Color4F(0, 0, 1f, 1f);
	public static readonly Color4F Purple = new Color4F(0.5f, 0, 1f, 1f);
	public static readonly Color4F Magenta = new Color4F(1f, 0, 1f, 1f);
	public static readonly Color4F Brown = new Color4F(0.5f, 0.25f, 0, 1f);
	public static readonly Color4F DarkRed = new Color4F(0.5f, 0, 0, 1f);
	public static readonly Color4F DarkYellow = new Color4F(0.5f, 0.5f, 0, 1f);
	public static readonly Color4F DarkGreen = new Color4F(0, 0.5f, 0, 1f);
	public static readonly Color4F DarkAqua = new Color4F(0, 0.5f, 0.5f, 1f);
	public static readonly Color4F DarkBlue = new Color4F(0, 0, 0.5f, 1f);
	public static readonly Color4F DarkPurple = new Color4F(0.25f, 0, 0.5f, 1f);
	public static readonly Color4F DarkMagenta = new Color4F(0.5f, 0, 0.5f, 1f);
	public static readonly Color4F Pink = new Color4F(1f, 0.5f, 1f, 1f);
	public static readonly Color4F White = new Color4F(1f, 1f, 1f, 1f);
	public static readonly Color4F Silver = new Color4F(0.75f, 0.75f, 0.75f, 1f);
	public static readonly Color4F Gray = new Color4F(0.5f, 0.5f, 0.5f, 1f);
	public static readonly Color4F Charcoal = new Color4F(0.25f, 0.25f, 0.25f, 1f);
	public static readonly Color4F Black = new Color4F(0, 0, 0, 1f);
	public static readonly Color4F Transparent = new Color4F(0, 0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Color4F({ToShortString()})";
	public string ToShortString() => $"{R}, {G}, {B}, {A}";
	
	public bool Equals(Color4F Other) => R == Other.R && G == Other.G && B == Other.B && A == Other.A;
	public override bool Equals(object? Object) => Object is Color4F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B, A);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color4F L, Color4F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color4F L, Color4F R) => !L.Equals(R);
}