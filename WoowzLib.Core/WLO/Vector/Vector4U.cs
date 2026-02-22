namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 20.02.2026 15:15
/// </summary>
public struct Vector4U : IEquatable<Vector4U>{
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

	
	public static Vector4U Lerp(Vector4U A, Vector4U B, float T) => new Vector4U(WL.Math.LerpU(A.X, B.X, T), WL.Math.LerpU(A.Y, B.Y, T), WL.Math.LerpU(A.Z, B.Z, T), WL.Math.LerpU(A.W, B.W, T));
	
	public static float Distance(Vector4U A, Vector4U B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)) + WL.Math.Sqr((float)(B.Z - A.Z)) + WL.Math.Sqr((float)(B.W - A.W)));
	
	#region Override

		public override string ToString() => "Vector4U(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		
		public string ToShortString() => X + ":" + Y + ":" + Z + ":" + W;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector4U Other){ return false; }
			return X == Other.X && Y == Other.Y && Z == Other.Z && W == Other.W;
		}
		
		public bool Equals(Vector4U Other) => X.Equals(Other.X) && Y.Equals(Other.Y) && Z.Equals(Other.Z) && W.Equals(Other.W);
		
		public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
		
		public static bool operator ==(Vector4U A, Vector4U B) => A.X == B.X && A.Y == B.Y && A.Z == B.Z && A.W == B.W;
		
		public static bool operator !=(Vector4U A, Vector4U B) => !(A == B);
	
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
		
		public static implicit operator Vector4I(Vector4U Other){
			return new Vector4I((int)Other.X, (int)Other.Y, (int)Other.Z, (int)Other.W);
		}
															   
		
	#endregion
}