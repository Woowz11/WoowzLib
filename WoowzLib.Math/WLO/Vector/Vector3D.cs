namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector3D{
	public readonly int  N = 3;
	public readonly Type T = typeof(double);
	
	public Vector3D(double X = 0, double Y = 0, double Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public double X = 0;
	public double Y = 0;
	public double Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3D(" + X + ", " + Y + ", " + Z + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector3D other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z);
		}
		
		public static bool operator ==(Vector3D A, Vector3D B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		}
		
		public static bool operator !=(Vector3D A, Vector3D B){
			return !(A == B);
		}
	
		public static Vector3D operator +(Vector3D A, Vector3D B){
			return new Vector3D(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}
		
		public static Vector3D operator +(Vector3D A, double B){
			return new Vector3D(A.X + B, A.Y + B, A.Z + B);
		}
		
		public static Vector3D operator ++(Vector3D A){
			return A + 1;
		}
	
		public static Vector3D operator -(Vector3D A, Vector3D B){
			return new Vector3D(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
		}
		
		public static Vector3D operator -(Vector3D A, double B){
			return new Vector3D(A.X - B, A.Y - B, A.Z - B);
		}
		
		public static Vector3D operator --(Vector3D A){
			return A - 1;
		}
		
		public static Vector3D operator *(Vector3D A, Vector3D B){
			return new Vector3D(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
		
		public static Vector3D operator *(Vector3D A, double B){
			return new Vector3D(A.X * B, A.Y * B, A.Z * B);
		}
		
		public static Vector3D operator *(double A, Vector3D B){
			return B * A;
		}
	
	#endregion
}