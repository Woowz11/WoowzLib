namespace WLO.Transform;
public interface ITransform<out TTransform>{
	TTransform Transform{ get; }
}