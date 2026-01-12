namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector4U{
	public readonly int  N = 4;
	public readonly Type T = typeof(uint);
	
	public Vector4U(uint X = 0, uint Y = 0, uint Z = 0, uint W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public uint X = 0;
	public uint Y = 0;
	public uint Z = 0;
	public uint W = 0;

	#region Override

		public override string ToString(){
			return "Vector4U(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector4U other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z, W);
		}
		
		public static bool operator ==(Vector4U A, Vector4U B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		}
		
		public static bool operator !=(Vector4U A, Vector4U B){
			return !(A == B);
		}
	
		public static Vector4U operator +(Vector4U A, Vector4U B){
			return new Vector4U(A.X + B.X, A.Y + B.Y, A.Z + B.Z, A.W + B.W);
		}
		
		public static Vector4U operator +(Vector4U A, uint B){
			return new Vector4U(A.X + B, A.Y + B, A.Z + B, A.W + B);
		}
		
		public static Vector4U operator ++(Vector4U A){
			return A + 1;
		}
	
		public static Vector4U operator -(Vector4U A, Vector4U B){
			return new Vector4U(A.X - B.X, A.Y - B.Y, A.Z - B.Z, A.W - B.W);
		}
		
		public static Vector4U operator -(Vector4U A, uint B){
			return new Vector4U(A.X - B, A.Y - B, A.Z - B, A.W - B);
		}
		
		public static Vector4U operator --(Vector4U A){
			return A - 1;
		}
		
		public static Vector4U operator *(Vector4U A, Vector4U B){
			return new Vector4U(A.X * B.X, A.Y * B.Y, A.Z * B.Z, A.W * B.W);
		}
		
		public static Vector4U operator *(Vector4U A, uint B){
			return new Vector4U(A.X * B, A.Y * B, A.Z * B, A.W * B);
		}
		
		public static Vector4U operator *(uint A, Vector4U B){
			return B * A;
		}
	
	#endregion
}