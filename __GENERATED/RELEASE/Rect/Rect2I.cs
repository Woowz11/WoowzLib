/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.329, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect2I : IEquatable<Rect2I>{
	public Rect2I(int X, int Y, uint W, uint H){
		this.X = X;
		this.Y = Y;
		this.W = W;
		this.H = H;
	}
	public Rect2I(Vector2I Position, Vector2UI Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect2I(){}
	
	// ----------------------------------------------------------------------
	
	public int X;
	public int Y;
	public uint W;
	public uint H;
	
	public int Left{
		get => X;
		set{
			int OldRight = Right;
			X = value;
			W = (uint)WL.Math.MaxI(0, OldRight - X);
		}
	}
	public int Right{
		get => X + (int)W;
		set{
			W = (uint)WL.Math.MaxI(0, value - X);
		}
	}
	public int Bottom{
		get => Y;
		set{
			int OldTop = Top;
			Y = value;
			H = (uint)WL.Math.MaxI(0, OldTop - Y);
		}
	}
	public int Top{
		get => Y + (int)H;
		set{
			H = (uint)WL.Math.MaxI(0, value - Y);
		}
	}
	
	public Vector2I Position{
		get => new Vector2I(X, Y);
		set{
			X = value.X;
			Y = value.Y;
		}
	}
	public Vector2UI Size{
		get => new Vector2UI(W, H);
		set{
			W = value.W;
			H = value.H;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => "Rect2I(" + ToShortString() + ")";
	public string ToShortString() => Position.ToPositionString() + ", " + Size.ToSizeString();
	
	public bool Equals(Rect2I Other) => X == Other.X && Y == Other.Y && W == Other.W && H == Other.H;
	public override bool Equals(object? Object) => Object is Rect2I Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, W, H);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect2I L, Rect2I R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect2I L, Rect2I R) => !L.Equals(R);
}