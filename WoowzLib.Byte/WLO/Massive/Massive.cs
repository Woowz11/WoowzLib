namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 1:12
/// </summary>
public struct Massive<T> where T : unmanaged{
	public Massive(){
		Data = [];
		AutoSize = true;
	}

	public Massive(int Size, bool AutoSize = true){
		if(Size < 0){ throw new Exception("Размер не может быть < 0!"); }
		Data = new T[Size];
		this.AutoSize = AutoSize;
	}
	
	public Massive(T[] Data, bool AutoSize = true){
		this.Data = Data ?? throw new Exception("Задан пустой массив!");
		this.AutoSize = AutoSize;
	}

	private T[] Data;
	
	public int Size => Data.Length;
	
	public bool AutoSize;
	
	public ref T this[int Index]{
		get{
			if(Index < 0){ throw new Exception("Индекс < 0!"); }
			if(Index >= Size){
				if(AutoSize){
					EnsureSize(Index);
				}else{
					throw new Exception("Индекс выходит за пределы у таблицы [" + this + "]! Индекс: " + Index);
				}
			}
			return ref Data[Index];
		}
	}
	
	public Massive<T> Resize(int NewSize){
		try{
			Array.Resize(ref Data, NewSize);
		}catch(Exception e){
			throw new Exception("Произошла ошибка при изменении размера у массива [" + this + "]!\nНовый размер: " + NewSize, e);
		}
		
		return this;
	}
	
	/// <summary>
	/// Увеличивает размер массива, если индекс выходит за края (в указанное кол-во раз)
	/// </summary>
	public Massive<T> EnsureSize(int Index, int HowMuch = 2){
		try{
			int Required = Index + 1;
			Resize(Size == 0 ? Required : Math.Max(Size * HowMuch, Required));
		}catch(Exception e){
			throw new Exception("Произошла ошибка при увеличении размера у массива [" + this + "]!\nИндекс: " + Index + "\nНа сколько?: " + HowMuch, e);
		}
		
		return this;
	}

	public Span<T> AsSpan{
		get => Data;
		set {
			if(value.Length != Size){
				if(AutoSize){
					Resize(value.Length);
				}else{
					throw new Exception("Размеры Span и массива различаются!");
				}
			}
			
			value.CopyTo(Data);
		}
	}

	#region Override

	   public override string ToString(){
		   return "Massive<T>(0-" + (Size - 1) + ", " + AutoSize + ")";
	   }

	#endregion
}