/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.79, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector3UI : IEquatable<Vector3UI>{
	public Vector3UI(uint X, uint Y, uint Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	
	// ----------------------------------------------------------------------
	
	public uint X = 0;
	public uint Y = 0;
	public uint Z = 0;
}