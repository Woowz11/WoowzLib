namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.02.2026 21:43
/// </summary>
public struct Vector2F : IEquatable<Vector2F>{
	public static readonly int  Numbers = 2;
	public static readonly Type Type    = typeof(float);

	public Vector2F(float X = 0, float Y = 0){
		this.X = X; this.Y = Y; 
	}
	


	public float X;
	public float Y;

	public Vector2F Set(float X, float Y){ this.X = X; this.Y = Y; return this; }


	public Vector2F ToZero(){ return Set(0, 0); }
	public static Vector2F Zero => new Vector2F().ToZero();
	public Vector2F ToOne(){ return Set(1, 1); }
	public static Vector2F One => new Vector2F().ToOne();
	public Vector2F ToMOne(){ return Set(-1, -1); }
	public static Vector2F MOne => new Vector2F().ToMOne();
	public Vector2F ToRight(){ return Set(1, 0); }
	public static Vector2F Right => new Vector2F().ToRight();
	public Vector2F ToLeft(){ return Set(-1, 0); }
	public static Vector2F Left => new Vector2F().ToLeft();
	public Vector2F ToUp(){ return Set(0, 1); }
	public static Vector2F Up => new Vector2F().ToUp();
	public Vector2F ToDown(){ return Set(0, -1); }
	public static Vector2F Down => new Vector2F().ToDown();

	
	public static Vector2F Lerp(Vector2F A, Vector2F B, float T) => new Vector2F(WL.Math.Lerp(A.X, B.X, T), WL.Math.Lerp(A.Y, B.Y, T));
	
	public static float Distance(Vector2F A, Vector2F B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)));
	
	#region Override

		public override string ToString() => "Vector2F(" + X + ", " + Y + ")";
		
		public string ToShortString() => X + ":" + Y;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector2F Other){ return false; }
			return X == Other.X && Y == Other.Y;
		}
		
		public bool Equals(Vector2F Other) => X.Equals(Other.X) && Y.Equals(Other.Y);
		
		public override int GetHashCode() => HashCode.Combine(X, Y);
		
		public static bool operator ==(Vector2F A, Vector2F B) => A.X == B.X && A.Y == B.Y;
		
		public static bool operator !=(Vector2F A, Vector2F B) => !(A == B);
	
		public static Vector2F operator +(Vector2F A, Vector2F B){
			return new Vector2F(A.X + B.X, A.Y + B.Y);
		}
		
		public static Vector2F operator +(Vector2F A, float B){
			return new Vector2F(A.X + B, A.Y + B);
		}
		
		public static Vector2F operator ++(Vector2F A){
			return A + 1;
		}
	
		public static Vector2F operator -(Vector2F A, Vector2F B){
			return new Vector2F(A.X - B.X, A.Y - B.Y);
		}
		
		public static Vector2F operator -(Vector2F A, float B){
			return new Vector2F(A.X - B, A.Y - B);
		}
		
		public static Vector2F operator --(Vector2F A){
			return A - 1;
		}
		
		public static Vector2F operator *(Vector2F A, Vector2F B){
			return new Vector2F(A.X * B.X, A.Y * B.Y);
		}
		
		public static Vector2F operator *(Vector2F A, float B){
			return new Vector2F(A.X * B, A.Y * B);
		}
		
		public static Vector2F operator *(float A, Vector2F B){
			return B * A;
		}
		
															   
		
	#endregion
}