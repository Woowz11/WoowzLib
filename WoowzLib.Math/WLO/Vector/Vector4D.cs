namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector4D : Vector4, VectorD{
	public int  N{ get; }
	public Type T{ get; }
	
	public double X = 0;
	public double Y = 0;
	public double Z = 0;
	public double W = 0;

	#region Override

		public override string ToString(){
			return "Vector4D(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
	
	#endregion
}