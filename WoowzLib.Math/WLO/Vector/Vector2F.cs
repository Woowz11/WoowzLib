namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 14:56
/// </summary>
public struct Vector2F{
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

	#region Override

		public override string ToString(){
			return "Vector2F(" + X + ", " + Y + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector2F other){ return false; }
			return X == other.X && Y == other.Y;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y);
		}
		
		public static bool operator ==(Vector2F A, Vector2F B){
			return A.X == B.X && A.Y == B.Y;
		}
		
		public static bool operator !=(Vector2F A, Vector2F B){
			return !(A == B);
		}
	
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