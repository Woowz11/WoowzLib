namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 20.02.2026 15:15
/// </summary>
public struct Vector2I : IEquatable<Vector2I>{
	public static readonly int  Numbers = 2;
	public static readonly Type Type    = typeof(int);

	public Vector2I(int X = 0, int Y = 0){
		this.X = X; this.Y = Y; 
	}
	
	public Vector2I(WL.System.Native.Windows.POINT Point){ Set(Point); }

	public int X;
	public int Y;

	public Vector2I Set(int X, int Y){ this.X = X; this.Y = Y; return this; }
	public Vector2I Set(WL.System.Native.Windows.POINT Point){ X = (int)Point.x; Y = (int)Point.y; return this; }

	public Vector2I ToZero(){ return Set(0, 0); }
	public static Vector2I Zero => new Vector2I().ToZero();
	public Vector2I ToOne(){ return Set(1, 1); }
	public static Vector2I One => new Vector2I().ToOne();
	public Vector2I ToMOne(){ return Set(-1, -1); }
	public static Vector2I MOne => new Vector2I().ToMOne();
	public Vector2I ToRight(){ return Set(1, 0); }
	public static Vector2I Right => new Vector2I().ToRight();
	public Vector2I ToLeft(){ return Set(-1, 0); }
	public static Vector2I Left => new Vector2I().ToLeft();
	public Vector2I ToUp(){ return Set(0, 1); }
	public static Vector2I Up => new Vector2I().ToUp();
	public Vector2I ToDown(){ return Set(0, -1); }
	public static Vector2I Down => new Vector2I().ToDown();

	
	public static Vector2I Lerp(Vector2I A, Vector2I B, float T) => new Vector2I(WL.Math.LerpI(A.X, B.X, T), WL.Math.LerpI(A.Y, B.Y, T));
	
	public static float Distance(Vector2I A, Vector2I B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)));
	
	#region Override

		public override string ToString() => "Vector2I(" + X + ", " + Y + ")";
		
		public string ToShortString() => X + ":" + Y;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector2I Other){ return false; }
			return X == Other.X && Y == Other.Y;
		}
		
		public bool Equals(Vector2I Other) => X.Equals(Other.X) && Y.Equals(Other.Y);
		
		public override int GetHashCode() => HashCode.Combine(X, Y);
		
		public static bool operator ==(Vector2I A, Vector2I B) => A.X == B.X && A.Y == B.Y;
		
		public static bool operator !=(Vector2I A, Vector2I B) => !(A == B);
	
		public static Vector2I operator +(Vector2I A, Vector2I B){
			return new Vector2I(A.X + B.X, A.Y + B.Y);
		}
		
		public static Vector2I operator +(Vector2I A, int B){
			return new Vector2I(A.X + B, A.Y + B);
		}
		
		public static Vector2I operator ++(Vector2I A){
			return A + 1;
		}
	
		public static Vector2I operator -(Vector2I A, Vector2I B){
			return new Vector2I(A.X - B.X, A.Y - B.Y);
		}
		
		public static Vector2I operator -(Vector2I A, int B){
			return new Vector2I(A.X - B, A.Y - B);
		}
		
		public static Vector2I operator --(Vector2I A){
			return A - 1;
		}
		
		public static Vector2I operator *(Vector2I A, Vector2I B){
			return new Vector2I(A.X * B.X, A.Y * B.Y);
		}
		
		public static Vector2I operator *(Vector2I A, int B){
			return new Vector2I(A.X * B, A.Y * B);
		}
		
		public static Vector2I operator *(int A, Vector2I B){
			return B * A;
		}
		
															   
		public static implicit operator WL.System.Native.Windows.POINT(Vector2I Other){
			return new WL.System.Native.Windows.POINT{ x = Other.X, y = Other.Y };
		}
		
		public static implicit operator Vector2I(WL.System.Native.Windows.POINT Other){
			return new Vector2I(Other.x, Other.y);
		}
		
		public static implicit operator System.Drawing.Point(Vector2I Other){
			return new System.Drawing.Point{ X = Other.X, Y = Other.Y };
		}
		
		public static implicit operator Vector2I(System.Drawing.Point Other){
			return new Vector2I(Other.X, Other.Y);
		}
	#endregion
}