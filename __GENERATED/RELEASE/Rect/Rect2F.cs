/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.341, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect2F : IEquatable<Rect2F>{
	public Rect2F(float X, float Y, float W, float H){
		this.X = X;
		this.Y = Y;
		this.W = W;
		this.H = H;
	}
	public Rect2F(Vector2F Position, Vector2F Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect2F(){}
	
	// ----------------------------------------------------------------------
	
	public float X;
	public float Y;
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
	
	public Vector2F Position{
		get => new Vector2F(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	public Vector2F Size{
		get => new Vector2F(W, H);
		set{
			W = value.W;
			H = value.H;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Rect2F({ToShortString()})";
	public string ToShortString() => $"{Position.ToPositionString()}, {Size.ToSizeString()}";
	
	public bool Equals(Rect2F Other) => X == Other.X && Y == Other.Y && W == Other.W && H == Other.H;
	public override bool Equals(object? Object) => Object is Rect2F Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, W, H);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect2F L, Rect2F R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect2F L, Rect2F R) => !L.Equals(R);
}