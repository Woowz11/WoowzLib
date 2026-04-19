/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.347, внутри класса "Transform.cs" */
using WLO.Vector;
namespace WLO.Transform;
public class Transform4F{
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector4F> Position = new ReactiveProperty<Vector4F>();
	public readonly ReactiveProperty<Vector4F> Size = new ReactiveProperty<Vector4F>();
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => (Type & TransformType.Position) != 0;
	public bool SupportSize     => (Type & TransformType.Size    ) != 0;
	public bool SupportRotation => (Type & TransformType.Rotation) != 0;
}