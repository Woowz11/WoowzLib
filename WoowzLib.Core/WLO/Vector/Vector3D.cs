namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.02.2026 21:43
/// </summary>
public struct Vector3D : IEquatable<Vector3D>{
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

	
	public static Vector3D Lerp(Vector3D A, Vector3D B, float T) => new Vector3D(WL.Math.LerpD(A.X, B.X, T), WL.Math.LerpD(A.Y, B.Y, T), WL.Math.LerpD(A.Z, B.Z, T));
	
	public static float Distance(Vector3D A, Vector3D B) => WL.Math.Sqrt(WL.Math.Sqr((float)(B.X - A.X)) + WL.Math.Sqr((float)(B.Y - A.Y)) + WL.Math.Sqr((float)(B.Z - A.Z)));
	
	#region Override

		public override string ToString() => "Vector3D(" + X + ", " + Y + ", " + Z + ")";
		
		public string ToShortString() => X + ":" + Y + ":" + Z;
		
		public override bool Equals(object? Obj){
			if(Obj is not Vector3D Other){ return false; }
			return X == Other.X && Y == Other.Y && Z == Other.Z;
		}
		
		public bool Equals(Vector3D Other) => X.Equals(Other.X) && Y.Equals(Other.Y) && Z.Equals(Other.Z);
		
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		
		public static bool operator ==(Vector3D A, Vector3D B) => A.X == B.X && A.Y == B.Y && A.Z == B.Z;
		
		public static bool operator !=(Vector3D A, Vector3D B) => !(A == B);
	
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