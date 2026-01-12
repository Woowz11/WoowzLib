namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector2I : Vector2, VectorI{
	public int  N{ get; }
	public Type T{ get; }
	
	public int X = 0;
	public int Y = 0;

	#region Override

		public override string ToString(){
			return "Vector2I(" + X + ", " + Y + ")";
		}
	
	#endregion
}