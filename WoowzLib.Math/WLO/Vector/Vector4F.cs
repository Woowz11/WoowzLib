namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector4F : Vector4, VectorF{
	public int  N{ get; }
	public Type T{ get; }
	
	public float X = 0;
	public float Y = 0;
	public float Z = 0;
	public float W = 0;

	#region Override

		public override string ToString(){
			return "Vector4F(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
	
	#endregion
}