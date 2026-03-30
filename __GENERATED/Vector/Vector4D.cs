/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.79, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector4D : IEquatable<Vector4D>{
	public Vector4D(double X, double Y, double Z, double W){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
		this.W = W;
	}
	
	// ----------------------------------------------------------------------
	
	public double X = 0;
	public double Y = 0;
	public double Z = 0;
	public double W = 0;
}