/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.347, внутри класса "Transform.cs" */
using WLO.Vector;
namespace WLO.Transform;
public class Transform3D{
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector3D> Position = new ReactiveProperty<Vector3D>();
	public readonly ReactiveProperty<Vector3D> Size = new ReactiveProperty<Vector3D>();
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => (Type & TransformType.Position) != 0;
	public bool SupportSize     => (Type & TransformType.Size    ) != 0;
	public bool SupportRotation => (Type & TransformType.Rotation) != 0;
}