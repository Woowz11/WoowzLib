namespace WL;

public static class Math{
	/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.200, внутри класса "Math.cs" */
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static float AbsF(float A) => float.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static double AbsD(double A) => double.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static int AbsI(int A) => int.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static short AbsS(short A) => short.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static long AbsL(long A) => long.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 127 -> 127, -127 -> 127)
	/// </summary>
	public static sbyte AbsSB(sbyte A) => sbyte.Abs(A);
	/// <summary>
	/// Делает число не отрицательным (0 -> 0, 1 -> 1, -1 -> 1)
	/// </summary>
	public static decimal AbsDE(decimal A) => decimal.Abs(A);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static float MinF(float A, float B) => float.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static double MinD(double A, double B) => double.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static int MinI(int A, int B) => int.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static uint MinUI(uint A, uint B) => uint.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static short MinS(short A, short B) => short.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static ushort MinUS(ushort A, ushort B) => ushort.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static long MinL(long A, long B) => long.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static ulong MinUL(ulong A, ulong B) => ulong.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static byte MinB(byte A, byte B) => byte.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static sbyte MinSB(sbyte A, sbyte B) => sbyte.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static decimal MinDE(decimal A, decimal B) => decimal.Min(A, B);
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static float MinF(params float[] A){
		if(A.Length == 0){
			return 0;
		}
		float M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinF(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static double MinD(params double[] A){
		if(A.Length == 0){
			return 0;
		}
		double M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinD(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static int MinI(params int[] A){
		if(A.Length == 0){
			return 0;
		}
		int M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinI(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static uint MinUI(params uint[] A){
		if(A.Length == 0){
			return 0;
		}
		uint M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinUI(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static short MinS(params short[] A){
		if(A.Length == 0){
			return 0;
		}
		short M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinS(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static ushort MinUS(params ushort[] A){
		if(A.Length == 0){
			return 0;
		}
		ushort M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinUS(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static long MinL(params long[] A){
		if(A.Length == 0){
			return 0;
		}
		long M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinL(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static ulong MinUL(params ulong[] A){
		if(A.Length == 0){
			return 0;
		}
		ulong M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinUL(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static byte MinB(params byte[] A){
		if(A.Length == 0){
			return 0;
		}
		byte M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinB(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static sbyte MinSB(params sbyte[] A){
		if(A.Length == 0){
			return 0;
		}
		sbyte M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinSB(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает минимальное число из указанных
	/// </summary>
	public static decimal MinDE(params decimal[] A){
		if(A.Length == 0){
			return 0;
		}
		decimal M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MinDE(M, A[i]);
		}
		return M;
	}
	
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static float MaxF(float A, float B) => float.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static double MaxD(double A, double B) => double.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static int MaxI(int A, int B) => int.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static uint MaxUI(uint A, uint B) => uint.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static short MaxS(short A, short B) => short.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static ushort MaxUS(ushort A, ushort B) => ushort.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static long MaxL(long A, long B) => long.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static ulong MaxUL(ulong A, ulong B) => ulong.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static byte MaxB(byte A, byte B) => byte.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static sbyte MaxSB(sbyte A, sbyte B) => sbyte.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static decimal MaxDE(decimal A, decimal B) => decimal.Max(A, B);
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static float MaxF(params float[] A){
		if(A.Length == 0){
			return 0;
		}
		float M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxF(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static double MaxD(params double[] A){
		if(A.Length == 0){
			return 0;
		}
		double M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxD(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static int MaxI(params int[] A){
		if(A.Length == 0){
			return 0;
		}
		int M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxI(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static uint MaxUI(params uint[] A){
		if(A.Length == 0){
			return 0;
		}
		uint M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxUI(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static short MaxS(params short[] A){
		if(A.Length == 0){
			return 0;
		}
		short M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxS(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static ushort MaxUS(params ushort[] A){
		if(A.Length == 0){
			return 0;
		}
		ushort M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxUS(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static long MaxL(params long[] A){
		if(A.Length == 0){
			return 0;
		}
		long M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxL(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static ulong MaxUL(params ulong[] A){
		if(A.Length == 0){
			return 0;
		}
		ulong M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxUL(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static byte MaxB(params byte[] A){
		if(A.Length == 0){
			return 0;
		}
		byte M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxB(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static sbyte MaxSB(params sbyte[] A){
		if(A.Length == 0){
			return 0;
		}
		sbyte M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxSB(M, A[i]);
		}
		return M;
	}
	/// <summary>
	/// Выбирает максимальное число из указанных
	/// </summary>
	public static decimal MaxDE(params decimal[] A){
		if(A.Length == 0){
			return 0;
		}
		decimal M = A[0];
		for(int i = 1; i < A.Length; i++){
			M = WL.Math.MaxDE(M, A[i]);
		}
		return M;
	}
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Синус числа (0 -> 0, π/2 -> 1, π -> 0)
	/// </summary>
	public static float SinF(float A) => float.Sin(A);
	/// <summary>
	/// Синус числа (0 -> 0, π/2 -> 1, π -> 0)
	/// </summary>
	public static double SinD(double A) => double.Sin(A);
	/// <summary>
	/// Косинус числа (0 -> 1, π/2 -> 0, π -> -1)
	/// </summary>
	public static float CosF(float A) => float.Cos(A);
	/// <summary>
	/// Косинус числа (0 -> 1, π/2 -> 0, π -> -1)
	/// </summary>
	public static double CosD(double A) => double.Cos(A);
	/// <summary>
	/// Тангенс числа (0 -> 0, π/2 -> ∞, π -> 0)
	/// </summary>
	public static float TanF(float A) => float.Tan(A);
	/// <summary>
	/// Тангенс числа (0 -> 0, π/2 -> ∞, π -> 0)
	/// </summary>
	public static double TanD(double A) => double.Tan(A);
	/// <summary>
	/// Котангенс (0 -> ∞, π/2 -> 0, π -> ∞)
	/// </summary>
	public static float CotF(float A) => 1 / WL.Math.TanF(A);
	/// <summary>
	/// Котангенс (0 -> ∞, π/2 -> 0, π -> ∞)
	/// </summary>
	public static double CotD(double A) => 1 / WL.Math.TanD(A);
	/// <summary>
	/// Арксинус числа [-1, 1] (0 -> 0, 1 -> π/2, -1 -> -π/2)
	/// </summary>
	public static float ASinF(float A) => float.Asin(A);
	/// <summary>
	/// Арксинус числа [-1, 1] (0 -> 0, 1 -> π/2, -1 -> -π/2)
	/// </summary>
	public static double ASinD(double A) => double.Asin(A);
	/// <summary>
	/// Арккосинус числа [-1, 1] (0 -> π/2, 1 -> 0, -1 -> π)
	/// </summary>
	public static float ACosF(float A) => float.Acos(A);
	/// <summary>
	/// Арккосинус числа [-1, 1] (0 -> π/2, 1 -> 0, -1 -> π)
	/// </summary>
	public static double ACosD(double A) => double.Acos(A);
	/// <summary>
	/// Арктангенс числа ((1, 1) -> π/4, (0, 1) -> 0, (0, -1) -> π, (1, -1) -> 3π/4)
	/// </summary>
	public static float ATanF(float A) => float.Atan(A);
	/// <summary>
	/// Арктангенс числа ((1, 1) -> π/4, (0, 1) -> 0, (0, -1) -> π, (1, -1) -> 3π/4)
	/// </summary>
	public static double ATanD(double A) => double.Atan(A);
	/// <summary>
	/// Арктангенс по двум координатам
	/// </summary>
	public static float ATan2F(float A, float B) => float.Atan2(A, B);
	/// <summary>
	/// Арктангенс по двум координатам
	/// </summary>
	public static double ATan2D(double A, double B) => double.Atan2(A, B);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Возвращает экспоненту числа eˣ (0 -> 1, 1 -> 2.718, 2 -> 7.389)
	/// </summary>
	public static float ExpF(float A) => float.Exp(A);
	/// <summary>
	/// Возвращает экспоненту числа eˣ (0 -> 1, 1 -> 2.718, 2 -> 7.389)
	/// </summary>
	public static double ExpD(double A) => double.Exp(A);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Натуральный логарифм (1 -> 0, e -> 1, 7.389 -> 2)
	/// </summary>
	public static float LogF(float A) => float.Log(A);
	/// <summary>
	/// Натуральный логарифм (1 -> 0, e -> 1, 7.389 -> 2)
	/// </summary>
	public static double LogD(double A) => double.Log(A);
	/// <summary>
	/// Логарифм, где B основание, Bʳᵉᵗᵘʳⁿ = A ((8, 2) -> 3, (100, 10) -> 2, (27, 3) -> 3)
	/// </summary>
	public static float LogF(float A, float B) => float.Log(A, B);
	/// <summary>
	/// Логарифм, где B основание, Bʳᵉᵗᵘʳⁿ = A ((8, 2) -> 3, (100, 10) -> 2, (27, 3) -> 3)
	/// </summary>
	public static double LogD(double A, double B) => double.Log(A, B);
	/// <summary>
	/// Логарифм с основанием 10 (10 -> 1, 100 -> 2, 1000 -> 3)
	/// </summary>
	public static float Log10F(float A) => float.Log10(A);
	/// <summary>
	/// Логарифм с основанием 10 (10 -> 1, 100 -> 2, 1000 -> 3)
	/// </summary>
	public static double Log10D(double A) => double.Log10(A);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Возводит в степень Aᴮ (A^B)
	/// </summary>
	public static float PowF(float A, float B) => float.Pow(A, B);
	/// <summary>
	/// Возводит в степень Aᴮ (A^B)
	/// </summary>
	public static double PowD(double A, double B) => double.Pow(A, B);
	/// <summary>
	/// Квадратный корень
	/// </summary>
	public static float SqrtF(float A) => float.Sqrt(A);
	/// <summary>
	/// Квадратный корень
	/// </summary>
	public static double SqrtD(double A) => double.Sqrt(A);
	/// <summary>
	/// Кубический корень
	/// </summary>
	public static float CbrtF(float A) => float.Cbrt(A);
	/// <summary>
	/// Кубический корень
	/// </summary>
	public static double CbrtD(double A) => double.Cbrt(A);
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static float SqrF(float A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static double SqrD(double A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static int SqrI(int A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static uint SqrUI(uint A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static long SqrL(long A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static ulong SqrUL(ulong A) => A * A;
	/// <summary>
	/// Возводит число в квадрат
	/// </summary>
	public static decimal SqrDE(decimal A) => A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static float CubeF(float A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static double CubeD(double A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static int CubeI(int A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static uint CubeUI(uint A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static long CubeL(long A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static ulong CubeUL(ulong A) => A * A * A;
	/// <summary>
	/// Возводит число в куб
	/// </summary>
	public static decimal CubeDE(decimal A) => A * A * A;
	
	// ----------------------------------------------------------------------
	/* Конец генератора */
}