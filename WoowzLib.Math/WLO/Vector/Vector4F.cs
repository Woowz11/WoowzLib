namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 23:52
/// </summary>
public struct Vector4F{
	public static readonly int  Numbers = 4;
	public static readonly Type Type    = typeof(float);

	public Vector4F(float X = 0, float Y = 0, float Z = 0, float W = 0){
		this.X = X; this.Y = Y; this.Z = Z; this.W = W; 
	}

	public float X;
	public float Y;
	public float Z;
	public float W;

	public Vector4F Set(float X, float Y, float Z, float W){ this.X = X; this.Y = Y; this.Z = Z; this.W = W; return this; }
		
	public Vector4F ToZero(){ return Set(0, 0, 0, 0); }
	public static Vector4F Zero => new Vector4F().ToZero();
	public Vector4F ToOne(){ return Set(1, 1, 1, 1); }
	public static Vector4F One => new Vector4F().ToOne();
	public Vector4F ToMOne(){ return Set(-1, -1, -1, -1); }
	public static Vector4F MOne => new Vector4F().ToMOne();
	public Vector4F ToRight(){ return Set(1, 0, 0, 0); }
	public static Vector4F Right => new Vector4F().ToRight();
	public Vector4F ToLeft(){ return Set(-1, 0, 0, 0); }
	public static Vector4F Left => new Vector4F().ToLeft();
	public Vector4F ToUp(){ return Set(0, 1, 0, 0); }
	public static Vector4F Up => new Vector4F().ToUp();
	public Vector4F ToDown(){ return Set(0, -1, 0, 0); }
	public static Vector4F Down => new Vector4F().ToDown();
	public Vector4F ToFront(){ return Set(0, 0, 1, 0); }
	public static Vector4F Front => new Vector4F().ToFront();
	public Vector4F ToBack(){ return Set(0, 0, -1, 0); }
	public static Vector4F Back => new Vector4F().ToBack();
	public Vector4F ToAna(){ return Set(0, 0, 0, 1); }
	public static Vector4F Ana => new Vector4F().ToAna();
	public Vector4F ToKata(){ return Set(0, 0, 0, -1); }
	public static Vector4F Kata => new Vector4F().ToKata();

	#region Override

		public override string ToString(){
			return "Vector4F(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector4F other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z, W);
		}
		
		public static bool operator ==(Vector4F A, Vector4F B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		}
		
		public static bool operator !=(Vector4F A, Vector4F B){
			return !(A == B);
		}
	
		public static Vector4F operator +(Vector4F A, Vector4F B){
			return new Vector4F(A.X + B.X, A.Y + B.Y, A.Z + B.Z, A.W + B.W);
		}
		
		public static Vector4F operator +(Vector4F A, float B){
			return new Vector4F(A.X + B, A.Y + B, A.Z + B, A.W + B);
		}
		
		public static Vector4F operator ++(Vector4F A){
			return A + 1;
		}
	
		public static Vector4F operator -(Vector4F A, Vector4F B){
			return new Vector4F(A.X - B.X, A.Y - B.Y, A.Z - B.Z, A.W - B.W);
		}
		
		public static Vector4F operator -(Vector4F A, float B){
			return new Vector4F(A.X - B, A.Y - B, A.Z - B, A.W - B);
		}
		
		public static Vector4F operator --(Vector4F A){
			return A - 1;
		}
		
		public static Vector4F operator *(Vector4F A, Vector4F B){
			return new Vector4F(A.X * B.X, A.Y * B.Y, A.Z * B.Z, A.W * B.W);
		}
		
		public static Vector4F operator *(Vector4F A, float B){
			return new Vector4F(A.X * B, A.Y * B, A.Z * B, A.W * B);
		}
		
		public static Vector4F operator *(float A, Vector4F B){
			return B * A;
		}
	
	#endregion
}