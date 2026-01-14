namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector3D{
	public static readonly int  Numbers = 3;
	public static readonly Type Type    = typeof(double);

	public Vector3D(double X = 0, double Y = 0, double Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public double X;
	public double Y;
	public double Z;

	public Vector3D Set(double X, double Y, double Z){ this.X = X; this.Y = Y; this.Z = Z; return this; }
		
	public Vector3D ToZero(){ return Set(0, 0, 0); }
	public static Vector3D Zero => new Vector3D().ToZero();
	public Vector3D ToOne(){ return Set(1, 1, 1); }
	public static Vector3D One => new Vector3D().ToOne();
	public Vector3D ToMOne(){ return Set(-1, -1, -1); }
	public static Vector3D MOne => new Vector3D().ToMOne();
	public Vector3D ToRight(){ return Set(1, 0, 0); }
	public static Vector3D Right => new Vector3D().ToRight();
	public Vector3D ToLeft(){ return Set(-1, 0, 0); }
	public static Vector3D Left => new Vector3D().ToLeft();
	public Vector3D ToUp(){ return Set(0, 1, 0); }
	public static Vector3D Up => new Vector3D().ToUp();
	public Vector3D ToDown(){ return Set(0, -1, 0); }
	public static Vector3D Down => new Vector3D().ToDown();
	public Vector3D ToFront(){ return Set(0, 0, 1); }
	public static Vector3D Front => new Vector3D().ToFront();
	public Vector3D ToBack(){ return Set(0, 0, -1); }
	public static Vector3D Back => new Vector3D().ToBack();

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