namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector3U{
	public static readonly int  Numbers = 3;
	public static readonly Type Type    = typeof(uint);

	public Vector3U(uint X = 0, uint Y = 0, uint Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public uint X;
	public uint Y;
	public uint Z;

	public Vector3U Set(uint X, uint Y, uint Z){ this.X = X; this.Y = Y; this.Z = Z; return this; }
		
	public Vector3U ToZero(){ return Set(0, 0, 0); }
	public static Vector3U Zero => new Vector3U().ToZero();
	public Vector3U ToOne(){ return Set(1, 1, 1); }
	public static Vector3U One => new Vector3U().ToOne();
	public Vector3U ToRight(){ return Set(1, 0, 0); }
	public static Vector3U Right => new Vector3U().ToRight();
	public Vector3U ToUp(){ return Set(0, 1, 0); }
	public static Vector3U Up => new Vector3U().ToUp();
	public Vector3U ToFront(){ return Set(0, 0, 1); }
	public static Vector3U Front => new Vector3U().ToFront();

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