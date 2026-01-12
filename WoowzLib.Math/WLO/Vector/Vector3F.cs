namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector3F(float X = 0, float Y = 0, float Z = 0){
	public readonly int  N = 3;
	public readonly Type T = typeof(float);

	public float X;
	public float Y;
	public float Z;

	#region Override

		public override string ToString(){
			return "Vector3F(" + X + ", " + Y + ", " + Z + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector3F other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z);
		}
		
		public static bool operator ==(Vector3F A, Vector3F B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		}
		
		public static bool operator !=(Vector3F A, Vector3F B){
			return !(A == B);
		}
	
		public static Vector3F operator +(Vector3F A, Vector3F B){
			return new Vector3F(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}
		
		public static Vector3F operator +(Vector3F A, float B){
			return new Vector3F(A.X + B, A.Y + B, A.Z + B);
		}
		
		public static Vector3F operator ++(Vector3F A){
			return A + 1;
		}
	
		public static Vector3F operator -(Vector3F A, Vector3F B){
			return new Vector3F(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
		}
		
		public static Vector3F operator -(Vector3F A, float B){
			return new Vector3F(A.X - B, A.Y - B, A.Z - B);
		}
		
		public static Vector3F operator --(Vector3F A){
			return A - 1;
		}
		
		public static Vector3F operator *(Vector3F A, Vector3F B){
			return new Vector3F(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
		
		public static Vector3F operator *(Vector3F A, float B){
			return new Vector3F(A.X * B, A.Y * B, A.Z * B);
		}
		
		public static Vector3F operator *(float A, Vector3F B){
			return B * A;
		}
	
	#endregion
}