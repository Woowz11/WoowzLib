namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector2F{
	public readonly int  N = 2;
	public readonly Type T = typeof(float);
	
	public Vector2F(float X = 0, float Y = 0){
		this.X = X; this.Y = Y; 
	}

	public float X = 0;
	public float Y = 0;

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