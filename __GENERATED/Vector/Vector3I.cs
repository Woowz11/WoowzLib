/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.79, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector3I : IEquatable<Vector3I>{
	public Vector3I(int X, int Y, int Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	
	// ----------------------------------------------------------------------
	
	public int X = 0;
	public int Y = 0;
	public int Z = 0;
}