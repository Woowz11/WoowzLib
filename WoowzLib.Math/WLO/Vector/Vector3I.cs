namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector3I : Vector3, VectorI{
	public int  N{ get; }
	public Type T{ get; }
	
	public int X = 0;
	public int Y = 0;
	public int Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3I(" + X + ", " + Y + ", " + Z + ")";
		}
	
	#endregion
}