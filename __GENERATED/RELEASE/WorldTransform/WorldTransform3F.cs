/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.386, внутри класса "WorldTransform.cs" */
namespace WLO.Transform;
public class WorldTransform3F : Metadata{
	public WorldTransform3F(SceneNode<ITransform<WorldTransform3F>> Node, string Name = "?", object? Parent = null) : base(Name, Parent){
		__Node = Node;
		Local = new Transform3F(Name, Parent);
	}
	
	// ----------------------------------------------------------------------
	
	public Transform3F Local{
		get;
	}
	private SceneNode<ITransform<WorldTransform3F>>? __Node;
	
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