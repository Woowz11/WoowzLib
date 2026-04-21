/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.355, внутри класса "Transform.cs" */
using WLO.Attribute;
using WLO.Vector;
namespace WLO.Transform;
public class Transform2I : Metadata{
	public Transform2I(string Name = "?", object? Parent = null) : base(Name, Parent){
		Position = new ReactiveProperty<Vector2I>("Позиция", this);
		Size = new ReactiveProperty<Vector2I>("Размер", this, Vector2I.One);
		Rotation = new ReactiveProperty<bool>("Поворот", this, false);
		Position.OnApply += (_, V) => {
			if(!SupportPosition){
				throw new Exception("Не поддерживает позицию!");
			}
			return V;
		}
		Position.OnGet += (_, V) => {
			if(!SupportPosition){
				throw new Exception("Не поддерживает позицию!");
			}
			return V;
		}
		Size.OnApply += (_, V) => {
			if(!SupportSize){
				throw new Exception("Не поддерживает размер!");
			}
			return V;
		}
		Size.OnGet += (_, V) => {
			if(!SupportSize){
				throw new Exception("Не поддерживает размер!");
			}
			return V;
		}
		Rotation.OnApply += (_, V) => {
			if(!SupportRotation){
				throw new Exception("Не поддерживает поворот!");
			}
			return V;
		}
		Rotation.OnGet += (_, V) => {
			if(!SupportRotation){
				throw new Exception("Не поддерживает поворот!");
			}
			return V;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector2I> Position;
	public readonly ReactiveProperty<Vector2I> Size;
	[WoowzLibHint(Information.WorkInProgress)]
	public readonly ReactiveProperty<bool> Rotation;
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => (Type & TransformType.Position) != 0;
	public bool SupportSize     => (Type & TransformType.Size    ) != 0;
	public bool SupportRotation => (Type & TransformType.Rotation) != 0;
}