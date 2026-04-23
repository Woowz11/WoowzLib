/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.382, внутри класса "WorldTransform.cs" */
namespace WLO.Transform;
public class WorldTransform3D : Metadata{
	public WorldTransform3D(SceneNode<ITransform<WorldTransform3D>> Node, string Name = "?", object? Parent = null) : base(Name, Parent){
		__Node = Node;
		Local = new Transform3D(Name, Parent);
	}
	
	// ----------------------------------------------------------------------
	
	public Transform3D Local{
		get;
	}
	private SceneNode<ITransform<WorldTransform3D>>? __Node;
	
	// ----------------------------------------------------------------------
	
	public TransformType Type{
		get => Local.Type;
		set => Local.Type = value;
	}
	
	public bool SupportPosition => Local.SupportPosition;
	public bool SupportSize     => Local.SupportSize;
	public bool SupportRotation => Local.SupportRotation;
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	
	// ----------------------------------------------------------------------
	
	
}