namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct Vector4I(int X = 0, int Y = 0, int Z = 0, int W = 0){
	public readonly int  N = 4;
	public readonly Type T = typeof(int);

	public int X;
	public int Y;
	public int Z;
	public int W;

	#region Override

		public override string ToString(){
			return "Vector4I(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector4I other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z, W);
		}
		
		public static bool operator ==(Vector4I A, Vector4I B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		}
		
		public static bool operator !=(Vector4I A, Vector4I B){
			return !(A == B);
		}
	
		public static Vector4I operator +(Vector4I A, Vector4I B){
			return new Vector4I(A.X + B.X, A.Y + B.Y, A.Z + B.Z, A.W + B.W);
		}
		
		public static Vector4I operator +(Vector4I A, int B){
			return new Vector4I(A.X + B, A.Y + B, A.Z + B, A.W + B);
		}
		
		public static Vector4I operator ++(Vector4I A){
			return A + 1;
		}
	
		public static Vector4I operator -(Vector4I A, Vector4I B){
			return new Vector4I(A.X - B.X, A.Y - B.Y, A.Z - B.Z, A.W - B.W);
		}
		
		public static Vector4I operator -(Vector4I A, int B){
			return new Vector4I(A.X - B, A.Y - B, A.Z - B, A.W - B);
		}
		
		public static Vector4I operator --(Vector4I A){
			return A - 1;
		}
		
		public static Vector4I operator *(Vector4I A, Vector4I B){
			return new Vector4I(A.X * B.X, A.Y * B.Y, A.Z * B.Z, A.W * B.W);
		}
		
		public static Vector4I operator *(Vector4I A, int B){
			return new Vector4I(A.X * B, A.Y * B, A.Z * B, A.W * B);
		}
		
		public static Vector4I operator *(int A, Vector4I B){
			return B * A;
		}
	
	#endregion
}