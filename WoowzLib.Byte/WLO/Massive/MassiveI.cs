namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 20.01.2026 15:55
/// </summary>
public struct MassiveI : ArrayByteObject{
	// надо добавить sha256...

	public MassiveI(){
		Data = [];
		AutoSize = true;
	}

	public MassiveI(int Size, bool AutoSize = true){
		if(Size < 0){ throw new Exception("Размер не может быть < 0!"); }
		Data = new int[Size];
		this.AutoSize = AutoSize;
	}
	
	public MassiveI(int[] Data, bool AutoSize = true){
		this.Data = Data ?? throw new Exception("Задан пустой массив!");
		this.AutoSize = AutoSize;
	}

	public int[] Data;
	
	public int Size => Data.Length;
	
	public bool AutoSize;
	
	public ref int this[int Index]{
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
	
	public MassiveI Set(int[] Data){
		try{
			this.Data = new int[Data.Length];
			Array.Copy(Data, this.Data, Data.Length);
			
			return this;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при установке значений в массив [" + this + "]!\nЗначения: " + Data, e);
		}
	}
	
	public MassiveI SetSlice(int Index, int[] Data){
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
			throw new Exception("Произошла ошибка при установке части значений в массив [" + this + "]!\nИндекс: " + Index + "\nЗначения: " + Data, e);
		}
	}
	
	public int[] GetSlice(int Index, int EndIndex){
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
			int[] Slice = new int[L];
			Array.Copy(Data, Index, Slice, 0, L);
			return Slice;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при получении части от массива [" + this + "]!\nДиапазон: " + Index + "-" + EndIndex, e);
		}
	}
	
	public MassiveI Resize(int NewSize){
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
	public MassiveI EnsureSize(int Index, int HowMuch = 2){
		try{
			int Required = Index + 1;
			Resize(Size == 0 ? Required : Math.Max(Size * HowMuch, Required));
		}catch(Exception e){
			throw new Exception("Произошла ошибка при увеличении размера у массива [" + this + "]!\nИндекс: " + Index + "\nНа сколько?: " + HowMuch, e);
		}
		
		return this;
	}

	public Span<int> AsSpan{
		get => Data;
		set{
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
		   return "MassiveI(0-" + (Size - 1) + ", " + AutoSize + ")";
	   }
	   
	   public int ElementBSize(){
			return sizeof(int); 
		}
	   
	   public int BSize(){
		   return Size * ElementBSize(); 
	   }

	#endregion
}