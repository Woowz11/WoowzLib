namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorD(double R = 0, double G = 0, double B = 0, double A = 1){
	public readonly Type T = typeof(double);

	public double R;
	public double G;
	public double B;
	public double A;

	#region Override

		public override string ToString(){
			return "ColorD(" + R + ", " + G + ", " + B + ", " + (A == 1 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorD other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorD A, ColorD B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorD A, ColorD B){
			return !(A == B);
		}
	
	#endregion
}