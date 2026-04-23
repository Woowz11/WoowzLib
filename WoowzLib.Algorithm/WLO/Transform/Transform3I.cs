/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.365, внутри класса "Transform.cs" */
using WLO.Attribute;
using WLO.Vector;
namespace WLO.Transform;
public class Transform3I : Metadata{
	public Transform3I(string Name = "?", object? Parent = null) : base(Name, Parent){
		Position = new ReactiveProperty<Vector3I>("Позиция", this);
		Size = new ReactiveProperty<Vector3I>("Размер", this, Vector3I.One);
		Rotation = new ReactiveProperty<bool>("Поворот", this, false);
		Position.OnApply += (_, V) => {
			if(!SupportPosition){
				throw new Exception("Не поддерживает позицию!");
			}
			return Cancellable<Vector3I>.Continue(V);
		};
		Position.OnGet += (V) => {
			if(!SupportPosition){
				throw new Exception("Не поддерживает позицию!");
			}
			return V;
		};
		Position.OnChanged += (_, V) => {
			__InvokeOnChanged("Position");
		};
		Size.OnApply += (_, V) => {
			if(!SupportSize){
				throw new Exception("Не поддерживает размер!");
			}
			return Cancellable<Vector3I>.Continue(V);
		};
		Size.OnGet += (V) => {
			if(!SupportSize){
				throw new Exception("Не поддерживает размер!");
			}
			return V;
		};
		Size.OnChanged += (_, V) => {
			__InvokeOnChanged("Size");
		};
		Rotation.OnApply += (_, V) => {
			if(!SupportRotation){
				throw new Exception("Не поддерживает поворот!");
			}
			return Cancellable<bool>.Continue(V);
		};
		Rotation.OnGet += (V) => {
			if(!SupportRotation){
				throw new Exception("Не поддерживает поворот!");
			}
			return V;
		};
		Rotation.OnChanged += (_, V) => {
			__InvokeOnChanged("Rotation");
		};
	}
	
	// ----------------------------------------------------------------------
	
	public readonly ReactiveProperty<Vector3I> Position;
	public readonly ReactiveProperty<Vector3I> Size;
	[WoowzLibHint(Information.WorkInProgress)]
	public readonly ReactiveProperty<bool> Rotation;
	
	private bool __Dirty = false;
	
	// ----------------------------------------------------------------------
	
	public TransformType Type = TransformType.All;
	
	public bool SupportPosition => Flag.Contains(Type, TransformType.Position);
	public bool SupportSize     => Flag.Contains(Type, TransformType.Size    );
	public bool SupportRotation => Flag.Contains(Type, TransformType.Rotation);
	
	// ----------------------------------------------------------------------
	
	public event Action<Vector3I?, Vector3I?, bool?>? OnChanged;
	
	// ----------------------------------------------------------------------
	
	private void __InvokeOnChanged(string Name){
		if(__Dirty){
			return;
		}
		__Dirty = true;
		try{
			OnChanged?.Invoke(SupportPosition ? Position.Value : null, SupportSize ? Size.Value : null, SupportRotation ? Rotation.Value : null);
		}
		catch(Exception e){
			throw new Exception($"Произошла ошибка при вызове ивента OnChanged у {Name} [{this}]!", e);
		}
		finally{
			__Dirty = false;
		}
	}
	
	// ----------------------------------------------------------------------
	
	public override string ToString() => $"Transform3I({ToShortString()})";
	public string ToShortString() => (!SupportPosition && !SupportSize && !SupportRotation ? "Не поддерживает ничего" : WL.String.Join(", ", SupportPosition ? Position.Value.ToPositionString() : null, SupportSize ? Size.Value.ToSizeString() : null, SupportRotation ? Rotation.Value.ToString() : null));
	
	public bool Equals(Transform3I Other){
		if(ReferenceEquals(this, Other)){
			return true;
		}
		if(Type != Other.Type){
			return false;
		}
		if(SupportPosition && !Position.Value.Equals(Other.Position.Value)){
			return false;
		}
		if(SupportSize && !Size.Value.Equals(Other.Size.Value)){
			return false;
		}
		if(SupportRotation && !Rotation.Value.Equals(Other.Rotation.Value)){
			return false;
		}
		return true;
	}
	public override bool Equals(object? Object) => Object is Transform3I Other && Equals(Other);
	
	public override int GetHashCode(){
		HashCode Hash = new HashCode();
		Hash.Add(Type);
		if(SupportPosition){
			Hash.Add(Position.Value);
		}
		if(SupportSize){
			Hash.Add(Size.Value);
		}
		if(SupportRotation){
			Hash.Add(Rotation.Value);
		}
		return Hash.ToHashCode();
	}
}