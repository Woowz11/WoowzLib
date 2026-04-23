/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.376, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect3F : IEquatable<Rect3F>{
	public Rect3F(float X, float Y, float Z, float W, float H, float D){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
		this.H = H;
		this.D = D;
	}
	public Rect3F(Vector3F Position, Vector3F Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect3F(){}
	
	// ----------------------------------------------------------------------
	
	public float X;
	public float Y;
	public float Z;
	private float __W;
	public float W{
		get => __W;
		set{
			if(value < 0){
				throw new Exception($"Значение W не может быть < 0 у [{this}]!\nЗначение: {value}");
			}
			__W = value;
		}
	}
	private float __H;
	public float H{
		get => __H;
		set{
			if(value < 0){
				throw new Exception($"Значение H не может быть < 0 у [{this}]!\nЗначение: {value}");
			}
			__H = value;
		}
	}
	private float __D;
	public float D{
		get => __D;
		set{
			if(value < 0){
				throw new Exception($"Значение D не может быть < 0 у [{this}]!\nЗначение: {value}");
			}
			__D = value;
		}
	}
	
	public float Left{
		get => X;
		set{
			float OldRight = Right;
			X = value;
			W = WL.Math.MaxF(0, OldRight - X);
		}
	}
	public float Right{
		get => X + W;
		set{
			W = WL.Math.MaxF(0, value - X);
		}
	}
	public float Bottom{
		get => Y;
		set{
			float OldTop = Top;
			Y = value;
			H = WL.Math.MaxF(0, OldTop - Y);
		}
	}
	public float Top{
		get => Y + H;
		set{
			H = WL.Math.MaxF(0, value - Y);
		}
	}
	public float Back{
		get => Z;
		set{
			float OldFront = Front;
			Z = value;
			D = WL.Math.MaxF(0, OldFront - Z);
		}
	}
	public float Front{
		get => Z + D;
		set{
			D = WL.Math.MaxF(0, value - Z);
		}
	}
	
	public Vector3F Position{
		get => new Vector3F(X, Y, Z);
		set{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
		}
	}
	public Vector3F Size{
		get => new Vector3F(W, H, D);
		set{
			W = value.W;
			H = value.H;
			D = value.D;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Rect3F({ToShortString()})";
	public string ToShortString() => $"{Position.ToPositionString()}, {Size.ToSizeString()}";
	
	public bool Equals(Rect3F Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W && H == Other.H && D == Other.D;
	public override bool Equals(object? Object) => Object is Rect3F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W, H, D);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect3F L, Rect3F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect3F L, Rect3F R) => !L.Equals(R);
}