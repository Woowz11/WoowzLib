namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 20.02.2026 15:15
/// </summary>
public struct Vector3I : IEquatable<Vector3I>{
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

	
	public static Vector3I Lerp(Vector3I A, Vector3I B, float T) => new Vector3I(WL.Math.LerpI(A.X, B.X, T), WL.Math.LerpI(A.Y, B.Y, T), WL.Math.LerpI(A.Z, B.Z, T));
	
	public static float Distance(Vector3I A, Vector3I B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)) + WL.Math.Sqr((float)(B.Z - A.Z)));
	
	#region Override

		public override string ToString() => "Vector3I(" + X + ", " + Y + ", " + Z + ")";
		
		public string ToShortString() => X + ":" + Y + ":" + Z;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector3I Other){ return false; }
			return X == Other.X && Y == Other.Y && Z == Other.Z;
		}
		
		public bool Equals(Vector3I Other) => X.Equals(Other.X) && Y.Equals(Other.Y) && Z.Equals(Other.Z);
		
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		
		public static bool operator ==(Vector3I A, Vector3I B) => A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		
		public static bool operator !=(Vector3I A, Vector3I B) => !(A == B);
	
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