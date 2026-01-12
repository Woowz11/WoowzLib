namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector3I{
	public readonly int  N = 3;
	public readonly Type T = typeof(int);
	
	public Vector3I(int X = 0, int Y = 0, int Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public int X = 0;
	public int Y = 0;
	public int Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3I(" + X + ", " + Y + ", " + Z + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector3I other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z);
		}
		
		public static bool operator ==(Vector3I A, Vector3I B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		}
		
		public static bool operator !=(Vector3I A, Vector3I B){
			return !(A == B);
		}
	
		public static Vector3I operator +(Vector3I A, Vector3I B){
			return new Vector3I(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}
		
		public static Vector3I operator +(Vector3I A, int B){
			return new Vector3I(A.X + B, A.Y + B, A.Z + B);
		}
		
		public static Vector3I operator ++(Vector3I A){
			return A + 1;
		}
	
		public static Vector3I operator -(Vector3I A, Vector3I B){
			return new Vector3I(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
		}
		
		public static Vector3I operator -(Vector3I A, int B){
			return new Vector3I(A.X - B, A.Y - B, A.Z - B);
		}
		
		public static Vector3I operator --(Vector3I A){
			return A - 1;
		}
		
		public static Vector3I operator *(Vector3I A, Vector3I B){
			return new Vector3I(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
		
		public static Vector3I operator *(Vector3I A, int B){
			return new Vector3I(A.X * B, A.Y * B, A.Z * B);
		}
		
		public static Vector3I operator *(int A, Vector3I B){
			return B * A;
		}
	
	#endregion
}