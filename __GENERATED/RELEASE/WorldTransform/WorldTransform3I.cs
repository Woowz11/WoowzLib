/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.382, внутри класса "WorldTransform.cs" */
namespace WLO.Transform;
public class WorldTransform3I : Metadata{
	public WorldTransform3I(SceneNode<ITransform<WorldTransform3I>> Node, string Name = "?", object? Parent = null) : base(Name, Parent){
		__Node = Node;
		Local = new Transform3I(Name, Parent);
	}
	
	// ----------------------------------------------------------------------
	
	public Transform3I Local{
		get;
	}
	private SceneNode<ITransform<WorldTransform3I>>? __Node;
	
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