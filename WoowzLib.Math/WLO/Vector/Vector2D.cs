namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector2D(double X = 0, double Y = 0){
	public readonly int  N = 2;
	public readonly Type T = typeof(double);

	public double X;
	public double Y;

	#region Override

		public override string ToString(){
			return "Vector2D(" + X + ", " + Y + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector2D other){ return false; }
			return X == other.X && Y == other.Y;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y);
		}
		
		public static bool operator ==(Vector2D A, Vector2D B){
			return A.X == B.X && A.Y == B.Y;
		}
		
		public static bool operator !=(Vector2D A, Vector2D B){
			return !(A == B);
		}
	
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