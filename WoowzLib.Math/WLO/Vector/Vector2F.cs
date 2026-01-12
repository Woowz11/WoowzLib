namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// </summary>
public class Vector2F : Vector2, VectorF{
	public int  N{ get; }
	public Type T{ get; }
	
	public float X = 0;
	public float Y = 0;

	#region Override

		public override string ToString(){
			return "Vector2F(" + X + ", " + Y + ")";
		}
	
	#endregion
}