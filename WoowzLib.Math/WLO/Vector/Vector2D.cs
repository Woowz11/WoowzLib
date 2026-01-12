namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector2D : Vector2, VectorD{
	public int  N{ get; }
	public Type T{ get; }
	
	public double X = 0;
	public double Y = 0;

	#region Override

		public override string ToString(){
			return "Vector2D(" + X + ", " + Y + ")";
		}
	
	#endregion
}