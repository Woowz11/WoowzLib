namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector4F(float X = 0, float Y = 0, float Z = 0, float W = 0){
	public readonly int  N = 4;
	public readonly Type T = typeof(float);

	public float X;
	public float Y;
	public float Z;
	public float W;

	#region Override

		public override string ToString(){
			return "Vector4F(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector4F other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z, W);
		}
		
		public static bool operator ==(Vector4F A, Vector4F B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		}
		
		public static bool operator !=(Vector4F A, Vector4F B){
			return !(A == B);
		}
	
		public static Vector4F operator +(Vector4F A, Vector4F B){
			return new Vector4F(A.X + B.X, A.Y + B.Y, A.Z + B.Z, A.W + B.W);
		}
		
		public static Vector4F operator +(Vector4F A, float B){
			return new Vector4F(A.X + B, A.Y + B, A.Z + B, A.W + B);
		}
		
		public static Vector4F operator ++(Vector4F A){
			return A + 1;
		}
	
		public static Vector4F operator -(Vector4F A, Vector4F B){
			return new Vector4F(A.X - B.X, A.Y - B.Y, A.Z - B.Z, A.W - B.W);
		}
		
		public static Vector4F operator -(Vector4F A, float B){
			return new Vector4F(A.X - B, A.Y - B, A.Z - B, A.W - B);
		}
		
		public static Vector4F operator --(Vector4F A){
			return A - 1;
		}
		
		public static Vector4F operator *(Vector4F A, Vector4F B){
			return new Vector4F(A.X * B.X, A.Y * B.Y, A.Z * B.Z, A.W * B.W);
		}
		
		public static Vector4F operator *(Vector4F A, float B){
			return new Vector4F(A.X * B, A.Y * B, A.Z * B, A.W * B);
		}
		
		public static Vector4F operator *(float A, Vector4F B){
			return B * A;
		}
	
	#endregion
}