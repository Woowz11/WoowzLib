namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorF(float R = 0, float G = 0, float B = 0, float A = 1){
	public readonly Type T = typeof(float);

	public float R;
	public float G;
	public float B;
	public float A;

	#region Override

		public override string ToString(){
			return "ColorF(" + R + ", " + G + ", " + B + ", " + (A == 1 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorF other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorF A, ColorF B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorF A, ColorF B){
			return !(A == B);
		}
	
	#endregion
}