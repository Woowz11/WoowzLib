namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector3D : Vector3, VectorD{
	public int  N{ get; }
	public Type T{ get; }
	
	public double X = 0;
	public double Y = 0;
	public double Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3D(" + X + ", " + Y + ", " + Z + ")";
		}
	
	#endregion
}