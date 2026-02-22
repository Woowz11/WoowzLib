namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 20.02.2026 15:15
/// </summary>
public struct Vector3U : IEquatable<Vector3U>{
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

	
	public static Vector3U Lerp(Vector3U A, Vector3U B, float T) => new Vector3U(WL.Math.LerpU(A.X, B.X, T), WL.Math.LerpU(A.Y, B.Y, T), WL.Math.LerpU(A.Z, B.Z, T));
	
	public static float Distance(Vector3U A, Vector3U B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)) + WL.Math.Sqr((float)(B.Z - A.Z)));
	
	#region Override

		public override string ToString() => "Vector3U(" + X + ", " + Y + ", " + Z + ")";
		
		public string ToShortString() => X + ":" + Y + ":" + Z;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector3U Other){ return false; }
			return X == Other.X && Y == Other.Y && Z == Other.Z;
		}
		
		public bool Equals(Vector3U Other) => X.Equals(Other.X) && Y.Equals(Other.Y) && Z.Equals(Other.Z);
		
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		
		public static bool operator ==(Vector3U A, Vector3U B) => A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		
		public static bool operator !=(Vector3U A, Vector3U B) => !(A == B);
	
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
		
		public static implicit operator Vector3I(Vector3U Other){
			return new Vector3I((int)Other.X, (int)Other.Y, (int)Other.Z);
		}
															   
		
	#endregion
}