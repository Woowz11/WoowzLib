namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector4I : Vector4, VectorI{
	public int  N{ get; }
	public Type T{ get; }
	
	public int X = 0;
	public int Y = 0;
	public int Z = 0;
	public int W = 0;

	#region Override

		public override string ToString(){
			return "Vector4I(" + X + ", " + Y + ", " + Z + ", " + W + ")";
		}
	
	#endregion
}