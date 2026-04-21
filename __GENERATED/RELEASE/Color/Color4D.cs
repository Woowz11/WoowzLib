/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.355, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color4D : IEquatable<Color4D>{
	public Color4D(double R = 0, double G = 0, double B = 0, double A = 1.0){
		this.R = R;
		this.G = G;
		this.B = B;
		this.A = A;
	}
	
	// ----------------------------------------------------------------------
	
	private double __R;
	public double R{
		get => __R;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет R выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__R = value;
		}
	}
	private double __G;
	public double G{
		get => __G;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет G выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__G = value;
		}
	}
	private double __B;
	public double B{
		get => __B;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет B выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__B = value;
		}
	}
	private double __A;
	public double A{
		get => __A;
		set{
			if(value is < 0 or > 1){
				throw new Exception($"Цвет A выходит за пределы [0, 1] у [{this}]!\nЗначение: {value}");
			}
			__A = value;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public static readonly Color4D Red = new Color4D(1.0, 0, 0, 1.0);
	public static readonly Color4D Orange = new Color4D(1.0, 0.5, 0, 1.0);
	public static readonly Color4D Yellow = new Color4D(1.0, 1.0, 0, 1.0);
	public static readonly Color4D Lime = new Color4D(0.5, 1.0, 0, 1.0);
	public static readonly Color4D Green = new Color4D(0, 1.0, 0, 1.0);
	public static readonly Color4D Aqua = new Color4D(0, 1.0, 1.0, 1.0);
	public static readonly Color4D Water = new Color4D(0, 0.5, 1.0, 1.0);
	public static readonly Color4D Blue = new Color4D(0, 0, 1.0, 1.0);
	public static readonly Color4D Purple = new Color4D(0.5, 0, 1.0, 1.0);
	public static readonly Color4D Magenta = new Color4D(1.0, 0, 1.0, 1.0);
	public static readonly Color4D Brown = new Color4D(0.5, 0.25, 0, 1.0);
	public static readonly Color4D DarkRed = new Color4D(0.5, 0, 0, 1.0);
	public static readonly Color4D DarkYellow = new Color4D(0.5, 0.5, 0, 1.0);
	public static readonly Color4D DarkGreen = new Color4D(0, 0.5, 0, 1.0);
	public static readonly Color4D DarkAqua = new Color4D(0, 0.5, 0.5, 1.0);
	public static readonly Color4D DarkBlue = new Color4D(0, 0, 0.5, 1.0);
	public static readonly Color4D DarkPurple = new Color4D(0.25, 0, 0.5, 1.0);
	public static readonly Color4D DarkMagenta = new Color4D(0.5, 0, 0.5, 1.0);
	public static readonly Color4D Pink = new Color4D(1.0, 0.5, 1.0, 1.0);
	public static readonly Color4D White = new Color4D(1.0, 1.0, 1.0, 1.0);
	public static readonly Color4D Silver = new Color4D(0.75, 0.75, 0.75, 1.0);
	public static readonly Color4D Gray = new Color4D(0.5, 0.5, 0.5, 1.0);
	public static readonly Color4D Charcoal = new Color4D(0.25, 0.25, 0.25, 1.0);
	public static readonly Color4D Black = new Color4D(0, 0, 0, 1.0);
	public static readonly Color4D Transparent = new Color4D(0, 0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Color4D({ToShortString()})";
	public string ToShortString() => $"{R}, {G}, {B}, {A}";
	
	public bool Equals(Color4D Other) => R == Other.R && G == Other.G && B == Other.B && A == Other.A;
	public override bool Equals(object? Object) => Object is Color4D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B, A);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color4D L, Color4D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color4D L, Color4D R) => !L.Equals(R);
}