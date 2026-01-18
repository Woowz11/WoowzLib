namespace WLO;

/// <summary>
/// Сгенерировано через GeneratorWoowzLib!
/// Сгенерирован: 18.01.2026 14:53
/// </summary>
public struct MassiveUS : ArrayByteObject{
	// надо добавить sha256...

	public MassiveUS(){
		Data = [];
		AutoSize = true;
	}

	public MassiveUS(int Size, bool AutoSize = true){
		if(Size < 0){ throw new Exception("Размер не может быть < 0!"); }
		Data = new ushort[Size];
		this.AutoSize = AutoSize;
	}
	
	public MassiveUS(ushort[] Data, bool AutoSize = true){
		this.Data = Data ?? throw new Exception("Задан пустой массив!");
		this.AutoSize = AutoSize;
	}

	public ushort[] Data;
	
	public int Size => Data.Length;
	
	public bool AutoSize;
	
	public ref ushort this[int Index]{
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
	
	public MassiveUS Set(ushort[] Data){
		try{
			this.Data = new ushort[Data.Length];
			Array.Copy(Data, this.Data, Data.Length);
			
			return this;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при установке значений в массив [" + this + "]!\nЗначения: " + Data, e);
		}
	}
	
	public MassiveUS SetSlice(int Index, ushort[] Data){
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
	
	public ushort[] GetSlice(int Index, int EndIndex){
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
			ushort[] Slice = new ushort[L];
			Array.Copy(Data, Index, Slice, 0, L);
			return Slice;
		}catch(Exception e){
			throw new Exception("Произошла ошибка при получении части от массива [" + this + "]!\nДиапазон: " + Index + "-" + EndIndex, e);
		}
	}
	
	public MassiveUS Resize(int NewSize){
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
	public MassiveUS EnsureSize(int Index, int HowMuch = 2){
		try{
			int Required = Index + 1;
			Resize(Size == 0 ? Required : Math.Max(Size * HowMuch, Required));
		}catch(Exception e){
			throw new Exception("Произошла ошибка при увеличении размера у массива [" + this + "]!\nИндекс: " + Index + "\nНа сколько?: " + HowMuch, e);
		}
		
		return this;
	}

	public Span<ushort> AsSpan{
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
		   return "MassiveUS(0-" + (Size - 1) + ", " + AutoSize + ")";
	   }
	   
	   public int ElementBSize(){
			return sizeof(ushort); 
		}
	   
	   public int BSize(){
		   return Size * ElementBSize(); 
	   }

	#endregion
}