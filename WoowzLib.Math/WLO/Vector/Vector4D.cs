namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector4D{
	public static readonly int  Numbers = 4;
	public static readonly Type Type    = typeof(double);

	public Vector4D(double X = 0, double Y = 0, double Z = 0, double W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public double X;
	public double Y;
	public double Z;
	public double W;

	public Vector4D Set(double X, double Y, double Z, double W){ this.X = X; this.Y = Y; this.Z = Z; this.W = W; return this; }
		
	public Vector4D ToZero(){ return Set(0, 0, 0, 0); }
	public static Vector4D Zero => new Vector4D().ToZero();
	public Vector4D ToOne(){ return Set(1, 1, 1, 1); }
	public static Vector4D One => new Vector4D().ToOne();
	public Vector4D ToMOne(){ return Set(-1, -1, -1, -1); }
	public static Vector4D MOne => new Vector4D().ToMOne();
	public Vector4D ToRight(){ return Set(1, 0, 0, 0); }
	public static Vector4D Right => new Vector4D().ToRight();
	public Vector4D ToLeft(){ return Set(-1, 0, 0, 0); }
	public static Vector4D Left => new Vector4D().ToLeft();
	public Vector4D ToUp(){ return Set(0, 1, 0, 0); }
	public static Vector4D Up => new Vector4D().ToUp();
	public Vector4D ToDown(){ return Set(0, -1, 0, 0); }
	public static Vector4D Down => new Vector4D().ToDown();
	public Vector4D ToFront(){ return Set(0, 0, 1, 0); }
	public static Vector4D Front => new Vector4D().ToFront();
	public Vector4D ToBack(){ return Set(0, 0, -1, 0); }
	public static Vector4D Back => new Vector4D().ToBack();
	public Vector4D ToAna(){ return Set(0, 0, 0, 1); }
	public static Vector4D Ana => new Vector4D().ToAna();
	public Vector4D ToKata(){ return Set(0, 0, 0, -1); }
	public static Vector4D Kata => new Vector4D().ToKata();

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