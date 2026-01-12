namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public struct ColorB(byte R = 0, byte G = 0, byte B = 0, byte A = 255){
	public readonly Type T = typeof(byte);

	public byte R;
	public byte G;
	public byte B;
	public byte A;

	#region Override

		public override string ToString(){
			return "ColorB(" + R + ", " + G + ", " + B + ", " + (A == 255 ? "" : A) + ")";
		}
		
		public override bool Equals(object? obj){
			if(obj is not ColorB other){ return false; }
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}
		
		public override int GetHashCode(){
			return HashCode.Combine(R, G, B, A);
		}
		
		public static bool operator ==(ColorB A, ColorB B){
			return A.R == B.R && A.G == B.G && A.B == B.B && A.A == B.A;
		}
		
		public static bool operator !=(ColorB A, ColorB B){
			return !(A == B);
		}
	
	#endregion
}