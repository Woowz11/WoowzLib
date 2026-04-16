/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.334, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect3D : IEquatable<Rect3D>{
	public Rect3D(double X, double Y, double Z, double W, double H, double D){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
		this.H = H;
		this.D = D;
	}
	public Rect3D(Vector3D Position, Vector3D Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect3D(){}
	
	// ----------------------------------------------------------------------
	
	public double X;
	public double Y;
	public double Z;
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
	private double __D;
	public double D{
		get => __D;
		set{
			if(value < 0){
				throw new Exception("Значение D не может быть < 0 в " + this + "!");
			}
			__D = value;
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
	public double Back{
		get => Z;
		set{
			double OldFront = Front;
			Z = value;
			D = WL.Math.MaxD(0, OldFront - Z);
		}
	}
	public double Front{
		get => Z + D;
		set{
			D = WL.Math.MaxD(0, value - Z);
		}
	}
	
	public Vector3D Position{
		get => new Vector3D(X, Y, Z);
		set{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
		}
	}
	public Vector3D Size{
		get => new Vector3D(W, H, D);
		set{
			W = value.W;
			H = value.H;
			D = value.D;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Rect3D(" + ToShortString() + ")";
	public string ToShortString() => Position.ToPositionString() + ", " + Size.ToSizeString();
	
	public bool Equals(Rect3D Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W && H == Other.H && D == Other.D;
	public override bool Equals(object? Object) => Object is Rect3D Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W, H, D);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect3D L, Rect3D R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect3D L, Rect3D R) => !L.Equals(R);
}