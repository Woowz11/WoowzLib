namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector2U : Vector2, VectorU{
	public int  N{ get; }
	public Type T{ get; }
	
	public uint X = 0;
	public uint Y = 0;

	#region Override

		public override string ToString(){
			return "Vector2U(" + X + ", " + Y + ")";
		}
	
	#endregion
}