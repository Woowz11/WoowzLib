namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector4I{
	public static readonly int  Numbers = 4;
	public static readonly Type Type    = typeof(int);

	public Vector4I(int X = 0, int Y = 0, int Z = 0, int W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public int X;
	public int Y;
	public int Z;
	public int W;

	public Vector4I Set(int X, int Y, int Z, int W){ this.X = X; this.Y = Y; this.Z = Z; this.W = W; return this; }
		
	public Vector4I ToZero(){ return Set(0, 0, 0, 0); }
	public static Vector4I Zero => new Vector4I().ToZero();
	public Vector4I ToOne(){ return Set(1, 1, 1, 1); }
	public static Vector4I One => new Vector4I().ToOne();
	public Vector4I ToMOne(){ return Set(-1, -1, -1, -1); }
	public static Vector4I MOne => new Vector4I().ToMOne();
	public Vector4I ToRight(){ return Set(1, 0, 0, 0); }
	public static Vector4I Right => new Vector4I().ToRight();
	public Vector4I ToLeft(){ return Set(-1, 0, 0, 0); }
	public static Vector4I Left => new Vector4I().ToLeft();
	public Vector4I ToUp(){ return Set(0, 1, 0, 0); }
	public static Vector4I Up => new Vector4I().ToUp();
	public Vector4I ToDown(){ return Set(0, -1, 0, 0); }
	public static Vector4I Down => new Vector4I().ToDown();
	public Vector4I ToFront(){ return Set(0, 0, 1, 0); }
	public static Vector4I Front => new Vector4I().ToFront();
	public Vector4I ToBack(){ return Set(0, 0, -1, 0); }
	public static Vector4I Back => new Vector4I().ToBack();
	public Vector4I ToAna(){ return Set(0, 0, 0, 1); }
	public static Vector4I Ana => new Vector4I().ToAna();
	public Vector4I ToKata(){ return Set(0, 0, 0, -1); }
	public static Vector4I Kata => new Vector4I().ToKata();

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