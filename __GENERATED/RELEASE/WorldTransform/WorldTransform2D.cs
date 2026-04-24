/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.386, внутри класса "WorldTransform.cs" */
namespace WLO.Transform;
public class WorldTransform2D : Metadata{
	public WorldTransform2D(SceneNode<ITransform<WorldTransform2D>> Node, string Name = "?", object? Parent = null) : base(Name, Parent){
		__Node = Node;
		Local = new Transform2D(Name, Parent);
	}
	
	// ----------------------------------------------------------------------
	
	public Transform2D Local{
		get;
	}
	private SceneNode<ITransform<WorldTransform2D>>? __Node;
	
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