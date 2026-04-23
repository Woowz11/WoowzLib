/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.382, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color3F : IEquatable<Color3F>{
	public Color3F(float R = 0, float G = 0, float B = 0){
		this.R = R;
		this.G = G;
		this.B = B;
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
	
	// ----------------------------------------------------------------------
	
	public static readonly Color3F Red = new Color3F(1f, 0, 0);
	public static readonly Color3F Orange = new Color3F(1f, 0.5f, 0);
	public static readonly Color3F Yellow = new Color3F(1f, 1f, 0);
	public static readonly Color3F Lime = new Color3F(0.5f, 1f, 0);
	public static readonly Color3F Green = new Color3F(0, 1f, 0);
	public static readonly Color3F Aqua = new Color3F(0, 1f, 1f);
	public static readonly Color3F Water = new Color3F(0, 0.5f, 1f);
	public static readonly Color3F Blue = new Color3F(0, 0, 1f);
	public static readonly Color3F Purple = new Color3F(0.5f, 0, 1f);
	public static readonly Color3F Magenta = new Color3F(1f, 0, 1f);
	public static readonly Color3F Brown = new Color3F(0.5f, 0.25f, 0);
	public static readonly Color3F DarkRed = new Color3F(0.5f, 0, 0);
	public static readonly Color3F DarkYellow = new Color3F(0.5f, 0.5f, 0);
	public static readonly Color3F DarkGreen = new Color3F(0, 0.5f, 0);
	public static readonly Color3F DarkAqua = new Color3F(0, 0.5f, 0.5f);
	public static readonly Color3F DarkBlue = new Color3F(0, 0, 0.5f);
	public static readonly Color3F DarkPurple = new Color3F(0.25f, 0, 0.5f);
	public static readonly Color3F DarkMagenta = new Color3F(0.5f, 0, 0.5f);
	public static readonly Color3F Pink = new Color3F(1f, 0.5f, 1f);
	public static readonly Color3F White = new Color3F(1f, 1f, 1f);
	public static readonly Color3F Silver = new Color3F(0.75f, 0.75f, 0.75f);
	public static readonly Color3F Gray = new Color3F(0.5f, 0.5f, 0.5f);
	public static readonly Color3F Charcoal = new Color3F(0.25f, 0.25f, 0.25f);
	public static readonly Color3F Black = new Color3F(0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Color3F({ToShortString()})";
	public string ToShortString() => $"{R}, {G}, {B}";
	
	public bool Equals(Color3F Other) => R == Other.R && G == Other.G && B == Other.B;
	public override bool Equals(object? Object) => Object is Color3F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color3F L, Color3F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color3F L, Color3F R) => !L.Equals(R);
}