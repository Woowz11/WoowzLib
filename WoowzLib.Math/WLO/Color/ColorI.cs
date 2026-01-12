namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorI(int R = 0, int G = 0, int B = 0, int A = 255){
	public readonly Type T = typeof(int);

	public int R;
	public int G;
	public int B;
	public int A;

	#region Override

		public override string ToString(){
			return "ColorI(" + R + ", " + G + ", " + B + ", " + (A == 255 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorI other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorI A, ColorI B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorI A, ColorI B){
			return !(A == B);
		}
	
	#endregion
}