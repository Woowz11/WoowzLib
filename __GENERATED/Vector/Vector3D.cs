/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.79, внутри класса "Vector.cs" */
namespace WLO.Vector;
public static class Vector3D : IEquatable<Vector3D>{
	public Vector3D(double X, double Y, double Z){
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	
	// ----------------------------------------------------------------------
	
	public double X = 0;
	public double Y = 0;
	public double Z = 0;
}