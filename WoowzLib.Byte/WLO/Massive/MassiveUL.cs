namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 15.01.2026 1:35
/// </summary>
public struct MassiveUL{
	public MassiveUL(){
		Data = [];
		AutoSize = true;
	}

	public MassiveUL(int Size, bool AutoSize = true){
		if(Size < 0){ throw new Exception("Размер не может быть < 0!"); }
		Data = new ulong[Size];
		this.AutoSize = AutoSize;
	}
	
	public MassiveUL(ulong[] Data, bool AutoSize = true){
		this.Data = Data ?? throw new Exception("Задан пустой массив!");
		this.AutoSize = AutoSize;
	}

	private ulong[] Data;
	
	public int Size => Data.Length;
	
	public bool AutoSize;
	
	public ref ulong this[int Index]{
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
	
	public MassiveUL Set(ulong[] Data){
		try{
			this.Data = new ulong[Data.Length];
			Array.Copy(Data, this.Data, Data.Length);
			
			return this;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при установке контента в массив [" + this + "]!\nКонтент: " + Data, e);
		}
	}
	
	public MassiveUL SetSlice(int Index, ulong[] Data){
		try{
			if(Index < 0){ throw new Exception("Индекс < 0!"); }

			int EndIndex = Index + Data.Length - 1;
			
			if(EndIndex >= Size){
				if(AutoSize){
					EnsureSize(EndIndex);
				}else{
					throw new Exception("Индекс выходит за пределы!");
				}
			}
			
			Array.Copy(Data, 0, this.Data, Index, Data.Length);
			return this;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при установке части в массив [" + this + "]!\nИндекс: " + Index + "\nКонтент: " + Data, e);
		}
	}
	
	public ulong[] GetSlice(int Index, int EndIndex){
		try{
			if(Index < 0 || EndIndex < Index){ throw new Exception("Неверный диапазон! Index < 0 || EndIndex < Index"); }
			if(EndIndex >= Size){
				if(AutoSize){
					EnsureSize(EndIndex);
				}else{
					throw new Exception("Диапазон выходит за границы! EndIndex >= Size");
				}
			}
			
			int L = EndIndex - Index + 1;
			ulong[] Slice = new ulong[L];
			Array.Copy(Data, Index, Slice, 0, L);
			return Slice;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при получении части от массива [" + this + "]!\nДиапазон: " + Index + "-" + EndIndex, e);
		}
	}
	
	public MassiveUL Resize(int NewSize){
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
	public MassiveUL EnsureSize(int Index, int HowMuch = 2){
		try{
			int Required = Index + 1;
			Resize(Size == 0 ? Required : Math.Max(Size * HowMuch, Required));
		}catch(Exception e){
			throw new Exception("Произошла ошибка при увеличении размера у массива [" + this + "]!\nИндекс: " + Index + "\nНа сколько?: " + HowMuch, e);
		}
		
		return this;
	}

	public Span<ulong> AsSpan{
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
		   return "MassiveUL(0-" + (Size - 1) + ", " + AutoSize + ")";
	   }

	#endregion
}