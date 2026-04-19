/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.341, внутри класса "Color.cs" */
using WLO.Attribute;
using System.Runtime.CompilerServices;
namespace WLO.Color;
[WoowzLibHint(Information.New)]
public struct Color3D : IEquatable<Color3D>{
	public Color3D(double R = 0, double G = 0, double B = 0){
		this.R = R;
		this.G = G;
		this.B = B;
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
	
	// ----------------------------------------------------------------------
	
	public static readonly Color3D Red = new Color3D(1.0, 0, 0);
	public static readonly Color3D Orange = new Color3D(1.0, 0.5, 0);
	public static readonly Color3D Yellow = new Color3D(1.0, 1.0, 0);
	public static readonly Color3D Lime = new Color3D(0.5, 1.0, 0);
	public static readonly Color3D Green = new Color3D(0, 1.0, 0);
	public static readonly Color3D Aqua = new Color3D(0, 1.0, 1.0);
	public static readonly Color3D Water = new Color3D(0, 0.5, 1.0);
	public static readonly Color3D Blue = new Color3D(0, 0, 1.0);
	public static readonly Color3D Purple = new Color3D(0.5, 0, 1.0);
	public static readonly Color3D Magenta = new Color3D(1.0, 0, 1.0);
	public static readonly Color3D Brown = new Color3D(0.5, 0.25, 0);
	public static readonly Color3D DarkRed = new Color3D(0.5, 0, 0);
	public static readonly Color3D DarkYellow = new Color3D(0.5, 0.5, 0);
	public static readonly Color3D DarkGreen = new Color3D(0, 0.5, 0);
	public static readonly Color3D DarkAqua = new Color3D(0, 0.5, 0.5);
	public static readonly Color3D DarkBlue = new Color3D(0, 0, 0.5);
	public static readonly Color3D DarkPurple = new Color3D(0.25, 0, 0.5);
	public static readonly Color3D DarkMagenta = new Color3D(0.5, 0, 0.5);
	public static readonly Color3D Pink = new Color3D(1.0, 0.5, 1.0);
	public static readonly Color3D White = new Color3D(1.0, 1.0, 1.0);
	public static readonly Color3D Silver = new Color3D(0.75, 0.75, 0.75);
	public static readonly Color3D Gray = new Color3D(0.5, 0.5, 0.5);
	public static readonly Color3D Charcoal = new Color3D(0.25, 0.25, 0.25);
	public static readonly Color3D Black = new Color3D(0, 0, 0);
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Color3D({ToShortString()})";
	public string ToShortString() => $"{R}, {G}, {B}";
	
	public bool Equals(Color3D Other) => R == Other.R && G == Other.G && B == Other.B;
	public override bool Equals(object? Object) => Object is Color3D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(R, G, B);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Color3D L, Color3D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Color3D L, Color3D R) => !L.Equals(R);
}