namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector3F : Vector3, VectorF{
	public int  N{ get; }
	public Type T{ get; }
	
	public float X = 0;
	public float Y = 0;
	public float Z = 0;

	#region Override

		public override string ToString(){
			return "Vector3F(" + X + ", " + Y + ", " + Z + ")";
		}
	
	#endregion
}