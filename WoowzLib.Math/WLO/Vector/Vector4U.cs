namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector4U : Vector4, VectorU{
	public int  N{ get; }
	public Type T{ get; }
	
	public uint X = 0;
	public uint Y = 0;
	public uint Z = 0;
	public uint W = 0;

	#region Override

		public override string ToString(){
			return "Vector4U(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
	
	#endregion
}