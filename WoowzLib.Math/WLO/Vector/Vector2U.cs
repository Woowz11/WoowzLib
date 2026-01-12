namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector2U{
	public readonly int  N = 2;
	public readonly Type T = typeof(uint);
	
	public Vector2U(uint X = 0, uint Y = 0){
		this.X = X; this.Y = Y; 
	}

	public uint X = 0;
	public uint Y = 0;

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
	
	#endregion
}