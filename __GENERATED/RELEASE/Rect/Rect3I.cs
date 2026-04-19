/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.343, внутри класса "Rect.cs" */
using WLO.Vector;
using System.Runtime.CompilerServices;
namespace WLO.Rect;
/// <summary>
/// Прямоугольник, счёт позиции идёт с нижнего левого угла!
/// </summary>
public struct Rect3I : IEquatable<Rect3I>{
	public Rect3I(int X, int Y, int Z, uint W, uint H, uint D){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
		this.H = H;
		this.D = D;
	}
	public Rect3I(Vector3I Position, Vector3UI Size){
		this.Position = Position;
		this.Size = Size;
	}
	public Rect3I(){}
	
	// ----------------------------------------------------------------------
	
	public int X;
	public int Y;
	public int Z;
	public uint W;
	public uint H;
	public uint D;
	
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
	public int Back{
		get => Z;
		set{
			int OldFront = Front;
			Z = value;
			D = (uint)WL.Math.MaxI(0, OldFront - Z);
		}
	}
	public int Front{
		get => Z + (int)D;
		set{
			D = (uint)WL.Math.MaxI(0, value - Z);
		}
	}
	
	public Vector3I Position{
		get => new Vector3I(X, Y, Z);
		set{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
		}
	}
	public Vector3UI Size{
		get => new Vector3UI(W, H, D);
		set{
			W = value.W;
			H = value.H;
			D = value.D;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Rect3I({ToShortString()})";
	public string ToShortString() => $"{Position.ToPositionString()}, {Size.ToSizeString()}";
	
	public bool Equals(Rect3I Other) => X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W && H == Other.H && D == Other.D;
	public override bool Equals(object? Object) => Object is Rect3I Other && Equals(Other);
	
	public override int GetHashCode() => HashCode.Combine(X, Y, Z, W, H, D);
	
	// ----------------------------------------------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Rect3I L, Rect3I R) => L.Equals(R);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Rect3I L, Rect3I R) => !L.Equals(R);
}