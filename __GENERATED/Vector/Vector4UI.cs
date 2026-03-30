/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.79, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector4UI : IEquatable<Vector4UI>{
	public Vector4UI(uint X, uint Y, uint Z, uint W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	
	// ----------------------------------------------------------------------
	
	public uint X = 0;
	public uint Y = 0;
	public uint Z = 0;
	public uint W = 0;
}