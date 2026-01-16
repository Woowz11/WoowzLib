namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 16.01.2026 12:04
/// </summary>
public struct Vector2U{
	public static readonly int  Numbers = 2;
	public static readonly Type Type    = typeof(uint);

	public Vector2U(uint X = 0, uint Y = 0){
		this.X = X; this.Y = Y; 
	}

	public uint X;
	public uint Y;

	public Vector2U Set(uint X, uint Y){ this.X = X; this.Y = Y; return this; }
		
	public Vector2U ToZero(){ return Set(0, 0); }
	public static Vector2U Zero => new Vector2U().ToZero();
	public Vector2U ToOne(){ return Set(1, 1); }
	public static Vector2U One => new Vector2U().ToOne();
	public Vector2U ToRight(){ return Set(1, 0); }
	public static Vector2U Right => new Vector2U().ToRight();
	public Vector2U ToUp(){ return Set(0, 1); }
	public static Vector2U Up => new Vector2U().ToUp();

	#region Override

		public override string ToString(){
			return "Vector2U(" + X + ", " + Y + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector2U other){ return false; }
			return X == other.X && Y == other.Y;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y);
		}
		
		public static bool operator ==(Vector2U A, Vector2U B){
			return A.X == B.X && A.Y == B.Y;
		}
		
		public static bool operator !=(Vector2U A, Vector2U B){
			return !(A == B);
		}
	
		public static Vector2U operator +(Vector2U A, Vector2U B){
			return new Vector2U(A.X + B.X, A.Y + B.Y);
		}
		
		public static Vector2U operator +(Vector2U A, uint B){
			return new Vector2U(A.X + B, A.Y + B);
		}
		
		public static Vector2U operator ++(Vector2U A){
			return A + 1;
		}
	
		public static Vector2U operator -(Vector2U A, Vector2U B){
			return new Vector2U(A.X - B.X, A.Y - B.Y);
		}
		
		public static Vector2U operator -(Vector2U A, uint B){
			return new Vector2U(A.X - B, A.Y - B);
		}
		
		public static Vector2U operator --(Vector2U A){
			return A - 1;
		}
		
		public static Vector2U operator *(Vector2U A, Vector2U B){
			return new Vector2U(A.X * B.X, A.Y * B.Y);
		}
		
		public static Vector2U operator *(Vector2U A, uint B){
			return new Vector2U(A.X * B, A.Y * B);
		}
		
		public static Vector2U operator *(uint A, Vector2U B){
			return B * A;
		}
		
		public static implicit operator Vector2I(Vector2U Other){
			return new Vector2I((int)Other.X, (int)Other.Y);
		}
	#endregion
}