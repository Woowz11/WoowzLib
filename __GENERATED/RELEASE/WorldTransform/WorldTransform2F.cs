/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.386, внутри класса "WorldTransform.cs" */
namespace WLO.Transform;
public class WorldTransform2F : Metadata{
	public WorldTransform2F(SceneNode<ITransform<WorldTransform2F>> Node, string Name = "?", object? Parent = null) : base(Name, Parent){
		__Node = Node;
		Local = new Transform2F(Name, Parent);
	}
	
	// ----------------------------------------------------------------------
	
	public Transform2F Local{
		get;
	}
	private SceneNode<ITransform<WorldTransform2F>>? __Node;
	
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