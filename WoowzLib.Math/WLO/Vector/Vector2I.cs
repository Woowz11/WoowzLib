namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector2I{
	public readonly int  N = 2;
	public readonly Type T = typeof(int);
	
	public Vector2I(int X = 0, int Y = 0){
		this.X = X; this.Y = Y; 
	}

	public int X = 0;
	public int Y = 0;

	#region Override

		public override string ToString(){
			return "Vector2I(" + X + ", " + Y + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector2I other){ return false; }
			return X == other.X && Y == other.Y;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y);
		}
		
		public static bool operator ==(Vector2I A, Vector2I B){
			return A.X == B.X && A.Y == B.Y;
		}
		
		public static bool operator !=(Vector2I A, Vector2I B){
			return !(A == B);
		}
	
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
	
	#endregion
}