namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector4D{
	public readonly int  N = 4;
	public readonly Type T = typeof(double);
	
	public Vector4D(double X = 0, double Y = 0, double Z = 0, double W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public double X = 0;
	public double Y = 0;
	public double Z = 0;
	public double W = 0;

	#region Override

		public override string ToString(){
			return "Vector4D(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector4D other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z, W);
		}
		
		public static bool operator ==(Vector4D A, Vector4D B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		}
		
		public static bool operator !=(Vector4D A, Vector4D B){
			return !(A == B);
		}
	
		public static Vector4D operator +(Vector4D A, Vector4D B){
			return new Vector4D(A.X + B.X, A.Y + B.Y, A.Z + B.Z, A.W + B.W);
		}
		
		public static Vector4D operator +(Vector4D A, double B){
			return new Vector4D(A.X + B, A.Y + B, A.Z + B, A.W + B);
		}
		
		public static Vector4D operator ++(Vector4D A){
			return A + 1;
		}
	
		public static Vector4D operator -(Vector4D A, Vector4D B){
			return new Vector4D(A.X - B.X, A.Y - B.Y, A.Z - B.Z, A.W - B.W);
		}
		
		public static Vector4D operator -(Vector4D A, double B){
			return new Vector4D(A.X - B, A.Y - B, A.Z - B, A.W - B);
		}
		
		public static Vector4D operator --(Vector4D A){
			return A - 1;
		}
		
		public static Vector4D operator *(Vector4D A, Vector4D B){
			return new Vector4D(A.X * B.X, A.Y * B.Y, A.Z * B.Z, A.W * B.W);
		}
		
		public static Vector4D operator *(Vector4D A, double B){
			return new Vector4D(A.X * B, A.Y * B, A.Z * B, A.W * B);
		}
		
		public static Vector4D operator *(double A, Vector4D B){
			return B * A;
		}
	
	#endregion
}