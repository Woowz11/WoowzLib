namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 1:35
/// </summary>
public struct Vector3I{
	public static readonly int  Numbers = 3;
	public static readonly Type Type    = typeof(int);

	public Vector3I(int X = 0, int Y = 0, int Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public int X;
	public int Y;
	public int Z;

	public Vector3I Set(int X, int Y, int Z){ this.X = X; this.Y = Y; this.Z = Z; return this; }
		
	public Vector3I ToZero(){ return Set(0, 0, 0); }
	public static Vector3I Zero => new Vector3I().ToZero();
	public Vector3I ToOne(){ return Set(1, 1, 1); }
	public static Vector3I One => new Vector3I().ToOne();
	public Vector3I ToMOne(){ return Set(-1, -1, -1); }
	public static Vector3I MOne => new Vector3I().ToMOne();
	public Vector3I ToRight(){ return Set(1, 0, 0); }
	public static Vector3I Right => new Vector3I().ToRight();
	public Vector3I ToLeft(){ return Set(-1, 0, 0); }
	public static Vector3I Left => new Vector3I().ToLeft();
	public Vector3I ToUp(){ return Set(0, 1, 0); }
	public static Vector3I Up => new Vector3I().ToUp();
	public Vector3I ToDown(){ return Set(0, -1, 0); }
	public static Vector3I Down => new Vector3I().ToDown();
	public Vector3I ToFront(){ return Set(0, 0, 1); }
	public static Vector3I Front => new Vector3I().ToFront();
	public Vector3I ToBack(){ return Set(0, 0, -1); }
	public static Vector3I Back => new Vector3I().ToBack();

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