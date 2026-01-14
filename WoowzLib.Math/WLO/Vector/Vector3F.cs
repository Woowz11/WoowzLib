namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 14.01.2026 14:31
/// </summary>
public struct Vector3F{
	public static readonly int  Numbers = 3;
	public static readonly Type Type    = typeof(float);

	public Vector3F(float X = 0, float Y = 0, float Z = 0){
		this.X = X; this.Y = Y; this.Z = Z; 
	}

	public float X;
	public float Y;
	public float Z;

	public Vector3F Set(float X, float Y, float Z){ this.X = X; this.Y = Y; this.Z = Z; return this; }
		
	public Vector3F ToZero(){ return Set(0, 0, 0); }
	public static Vector3F Zero => new Vector3F().ToZero();
	public Vector3F ToOne(){ return Set(1, 1, 1); }
	public static Vector3F One => new Vector3F().ToOne();
	public Vector3F ToMOne(){ return Set(-1, -1, -1); }
	public static Vector3F MOne => new Vector3F().ToMOne();
	public Vector3F ToRight(){ return Set(1, 0, 0); }
	public static Vector3F Right => new Vector3F().ToRight();
	public Vector3F ToLeft(){ return Set(-1, 0, 0); }
	public static Vector3F Left => new Vector3F().ToLeft();
	public Vector3F ToUp(){ return Set(0, 1, 0); }
	public static Vector3F Up => new Vector3F().ToUp();
	public Vector3F ToDown(){ return Set(0, -1, 0); }
	public static Vector3F Down => new Vector3F().ToDown();
	public Vector3F ToFront(){ return Set(0, 0, 1); }
	public static Vector3F Front => new Vector3F().ToFront();
	public Vector3F ToBack(){ return Set(0, 0, -1); }
	public static Vector3F Back => new Vector3F().ToBack();

	#region Override

		public override string ToString(){
			return "Vector3F(" + X + ", " + Y + ", " + Z + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not Vector3F other){ return false; }
			return X == other.X && Y == other.Y && Z == other.Z;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(X, Y, Z);
		}
		
		public static bool operator ==(Vector3F A, Vector3F B){
			return A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		}
		
		public static bool operator !=(Vector3F A, Vector3F B){
			return !(A == B);
		}
	
		public static Vector3F operator +(Vector3F A, Vector3F B){
			return new Vector3F(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
		}
		
		public static Vector3F operator +(Vector3F A, float B){
			return new Vector3F(A.X + B, A.Y + B, A.Z + B);
		}
		
		public static Vector3F operator ++(Vector3F A){
			return A + 1;
		}
	
		public static Vector3F operator -(Vector3F A, Vector3F B){
			return new Vector3F(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
		}
		
		public static Vector3F operator -(Vector3F A, float B){
			return new Vector3F(A.X - B, A.Y - B, A.Z - B);
		}
		
		public static Vector3F operator --(Vector3F A){
			return A - 1;
		}
		
		public static Vector3F operator *(Vector3F A, Vector3F B){
			return new Vector3F(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
		}
		
		public static Vector3F operator *(Vector3F A, float B){
			return new Vector3F(A.X * B, A.Y * B, A.Z * B);
		}
		
		public static Vector3F operator *(float A, Vector3F B){
			return B * A;
		}
	
	#endregion
}