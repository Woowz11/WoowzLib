/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.311, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect2D : IEquatable<Rect2D>{
	public Rect2D(double X, double Y, double W, double H){
		this.X = X;
		this.Y = Y;
		this.W = W;
		this.H = H;
	}
	public Rect2D(Vector2D Position, Vector2D Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect2D(){}
	
	// ----------------------------------------------------------------------
	
	public double X;
	public double Y;
	private double __W;
	public double W{
		get => __W;
		set{
			if(value < 0){
				throw new Exception("Значение W не может быть < 0 в " + this + "!");
			}
			__W = value;
		}
	}
	private double __H;
	public double H{
		get => __H;
		set{
			if(value < 0){
				throw new Exception("Значение H не может быть < 0 в " + this + "!");
			}
			__H = value;
		}
	}
	
	public double Left{
		get => X;
		set{
			double OldRight = Right;
			X = value;
			W = WL.Math.MaxD(0, OldRight - X);
		}
	}
	public double Right{
		get => X + W;
		set{
			W = WL.Math.MaxD(0, value - X);
		}
	}
	public double Bottom{
		get => Y;
		set{
			double OldTop = Top;
			Y = value;
			H = WL.Math.MaxD(0, OldTop - Y);
		}
	}
	public double Top{
		get => Y + H;
		set{
			H = WL.Math.MaxD(0, value - Y);
		}
	}
	
	public Vector2D Position{
		get => new Vector2D(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	public Vector2D Size{
		get => new Vector2D(W, H);
		set{
			W = value.W;
			H = value.H;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Rect2D(" + ToShortString() + ")";
	public string ToShortString() => Position.ToPositionString() + ", " + Size.ToSizeString();
	
	public bool Equals(Rect2D Other) => X == Other.X && Y == Other.Y && W == Other.W && H == Other.H;
	public override bool Equals(object? Object) => Object is Rect2D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, W, H);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect2D L, Rect2D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect2D L, Rect2D R) => !L.Equals(R);
}