/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.347, внутри класса "Transform.cs" */
using WLO.Vector;
namespace WLO.Transform;
public class Transform3F{
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector3F> Position = new ReactiveProperty<Vector3F>();
	public readonly ReactiveProperty<Vector3F> Size = new ReactiveProperty<Vector3F>();
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => (Type & TransformType.Position) != 0;
	public bool SupportSize     => (Type & TransformType.Size    ) != 0;
	public bool SupportRotation => (Type & TransformType.Rotation) != 0;
}