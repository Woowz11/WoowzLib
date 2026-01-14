namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector4U{
	public static readonly int  Numbers = 4;
	public static readonly Type Type    = typeof(uint);

	public Vector4U(uint X = 0, uint Y = 0, uint Z = 0, uint W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public uint X;
	public uint Y;
	public uint Z;
	public uint W;

	public Vector4U Set(uint X, uint Y, uint Z, uint W){ this.X = X; this.Y = Y; this.Z = Z; this.W = W; return this; }
		
	public Vector4U ToZero(){ return Set(0, 0, 0, 0); }
	public static Vector4U Zero => new Vector4U().ToZero();
	public Vector4U ToOne(){ return Set(1, 1, 1, 1); }
	public static Vector4U One => new Vector4U().ToOne();
	public Vector4U ToRight(){ return Set(1, 0, 0, 0); }
	public static Vector4U Right => new Vector4U().ToRight();
	public Vector4U ToUp(){ return Set(0, 1, 0, 0); }
	public static Vector4U Up => new Vector4U().ToUp();
	public Vector4U ToFront(){ return Set(0, 0, 1, 0); }
	public static Vector4U Front => new Vector4U().ToFront();
	public Vector4U ToAna(){ return Set(0, 0, 0, 1); }
	public static Vector4U Ana => new Vector4U().ToAna();

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