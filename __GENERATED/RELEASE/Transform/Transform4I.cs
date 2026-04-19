/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.347, внутри класса "Transform.cs" */
using WLO.Vector;
namespace WLO.Transform;
public class Transform4I{
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector4I> Position = new ReactiveProperty<Vector4I>();
	public readonly ReactiveProperty<Vector4I> Size = new ReactiveProperty<Vector4I>();
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => (Type & TransformType.Position) != 0;
	public bool SupportSize     => (Type & TransformType.Size    ) != 0;
	public bool SupportRotation => (Type & TransformType.Rotation) != 0;
}