namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.02.2026 21:43
/// </summary>
public struct Vector2D : IEquatable<Vector2D>{
	public static readonly int  Numbers = 2;
	public static readonly Type Type    = typeof(double);

	public Vector2D(double X = 0, double Y = 0){
		this.X = X; this.Y = Y; 
	}
	


	public double X;
	public double Y;

	public Vector2D Set(double X, double Y){ this.X = X; this.Y = Y; return this; }


	public Vector2D ToZero(){ return Set(0, 0); }
	public static Vector2D Zero => new Vector2D().ToZero();
	public Vector2D ToOne(){ return Set(1, 1); }
	public static Vector2D One => new Vector2D().ToOne();
	public Vector2D ToMOne(){ return Set(-1, -1); }
	public static Vector2D MOne => new Vector2D().ToMOne();
	public Vector2D ToRight(){ return Set(1, 0); }
	public static Vector2D Right => new Vector2D().ToRight();
	public Vector2D ToLeft(){ return Set(-1, 0); }
	public static Vector2D Left => new Vector2D().ToLeft();
	public Vector2D ToUp(){ return Set(0, 1); }
	public static Vector2D Up => new Vector2D().ToUp();
	public Vector2D ToDown(){ return Set(0, -1); }
	public static Vector2D Down => new Vector2D().ToDown();

	
	public static Vector2D Lerp(Vector2D A, Vector2D B, float T) => new Vector2D(WL.Math.LerpD(A.X, B.X, T), WL.Math.LerpD(A.Y, B.Y, T));
	
	public static float Distance(Vector2D A, Vector2D B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)));
	
	#region Override

		public override string ToString() => "Vector2D(" + X + ", " + Y + ")";
		
		public string ToShortString() => X + ":" + Y;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector2D Other){ return false; }
			return X == Other.X && Y == Other.Y;
		}
		
		public bool Equals(Vector2D Other) => X.Equals(Other.X) && Y.Equals(Other.Y);
		
		public override int GetHashCode() => HashCode.Combine(X, Y);
		
		public static bool operator ==(Vector2D A, Vector2D B) => A.X == B.X && A.Y == B.Y;
		
		public static bool operator !=(Vector2D A, Vector2D B) => !(A == B);
	
		public static Vector2D operator +(Vector2D A, Vector2D B){
			return new Vector2D(A.X + B.X, A.Y + B.Y);
		}
		
		public static Vector2D operator +(Vector2D A, double B){
			return new Vector2D(A.X + B, A.Y + B);
		}
		
		public static Vector2D operator ++(Vector2D A){
			return A + 1;
		}
	
		public static Vector2D operator -(Vector2D A, Vector2D B){
			return new Vector2D(A.X - B.X, A.Y - B.Y);
		}
		
		public static Vector2D operator -(Vector2D A, double B){
			return new Vector2D(A.X - B, A.Y - B);
		}
		
		public static Vector2D operator --(Vector2D A){
			return A - 1;
		}
		
		public static Vector2D operator *(Vector2D A, Vector2D B){
			return new Vector2D(A.X * B.X, A.Y * B.Y);
		}
		
		public static Vector2D operator *(Vector2D A, double B){
			return new Vector2D(A.X * B, A.Y * B);
		}
		
		public static Vector2D operator *(double A, Vector2D B){
			return B * A;
		}
		
															   
		
	#endregion
}