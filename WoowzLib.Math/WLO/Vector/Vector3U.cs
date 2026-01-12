namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector3U : Vector3, VectorU{
	public int  N{ get; }
	public Type T{ get; }
	
	public uint X = 0;
	public uint Y = 0;
	public uint Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3U(" + X + ", " + Y + ", " + Z + ")";
		}
	
	#endregion
}