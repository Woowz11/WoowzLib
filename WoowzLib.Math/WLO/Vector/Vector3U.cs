namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector3U(uint X = 0, uint Y = 0, uint Z = 0){
	public readonly int  N = 3;
	public readonly Type T = typeof(uint);

	public uint X;
	public uint Y;
	public uint Z;

	#region Override

		public override string ToString(){
			return "Vector3U(" + X + ", " + Y + ", " + Z + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector3U other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z);
		}
		
		public static bool operator ==(Vector3U A, Vector3U B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		}
		
		public static bool operator !=(Vector3U A, Vector3U B){
			return !(A == B);
		}
	
		public static Vector3U operator +(Vector3U A, Vector3U B){
			return new Vector3U(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}
		
		public static Vector3U operator +(Vector3U A, uint B){
			return new Vector3U(A.X + B, A.Y + B, A.Z + B);
		}
		
		public static Vector3U operator ++(Vector3U A){
			return A + 1;
		}
	
		public static Vector3U operator -(Vector3U A, Vector3U B){
			return new Vector3U(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
		}
		
		public static Vector3U operator -(Vector3U A, uint B){
			return new Vector3U(A.X - B, A.Y - B, A.Z - B);
		}
		
		public static Vector3U operator --(Vector3U A){
			return A - 1;
		}
		
		public static Vector3U operator *(Vector3U A, Vector3U B){
			return new Vector3U(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
		
		public static Vector3U operator *(Vector3U A, uint B){
			return new Vector3U(A.X * B, A.Y * B, A.Z * B);
		}
		
		public static Vector3U operator *(uint A, Vector3U B){
			return B * A;
		}
	
	#endregion
}