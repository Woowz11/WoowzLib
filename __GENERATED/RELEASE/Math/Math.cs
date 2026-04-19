using System.Runtime.CompilerServices;
using WLO.Attribute;

namespace WL;

public static class Math{
	/* Сгенерировано с помощью WoowzLibGenerator 0.0.0.343, внутри класса "Math.cs" */
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Число PI (π)
	/// </summary>
	public const decimal PiDE = 3.1415926535897932384626433833m;
	/// <summary>
	/// Число PI (π)
	/// </summary>
	public const double PiD = (double)PiDE;
	/// <summary>
	/// Число PI (π)
	/// </summary>
	public const float PiF = (float)PiD;
	
	/// <summary>
	/// Половина числа PI (π/2)
	/// </summary>
	public const decimal HalfPiDE = PiDE * 0.5m;
	/// <summary>
	/// Половина числа PI (π/2)
	/// </summary>
	public const double HalfPiD = (double)HalfPiDE;
	/// <summary>
	/// Половина числа PI (π/2)
	/// </summary>
	public const float HalfPiF = (float)HalfPiD;
	
	/// <summary>
	/// Два числа PI (2π)
	/// </summary>
	public const decimal TwoPiDE = PiDE * 2m;
	/// <summary>
	/// Два числа PI (2π)
	/// </summary>
	public const double TwoPiD = (double)TwoPiDE;
	/// <summary>
	/// Два числа PI (2π)
	/// </summary>
	public const float TwoPiF = (float)TwoPiD;
	
	/// <summary>
	/// Число e (Экспонента)
	/// </summary>
	public const decimal EDE = 2.7182818284590452353602874714m;
	/// <summary>
	/// Число e (Экспонента)
	/// </summary>
	public const double ED = (double)EDE;
	/// <summary>
	/// Число e (Экспонента)
	/// </summary>
	public const float EF = (float)ED;
	
	/// <summary>
	/// Половина числа e (Экспонента)
	/// </summary>
	public const decimal HalfEDE = EDE * 0.5m;
	/// <summary>
	/// Половина числа e (Экспонента)
	/// </summary>
	public const double HalfED = (double)HalfEDE;
	/// <summary>
	/// Половина числа e (Экспонента)
	/// </summary>
	public const float HalfEF = (float)HalfED;
	
	/// <summary>
	/// Корень из 2 (√2)
	/// </summary>
	public const decimal Sqrt2DE = 1.4142135623730950488016887242m;
	/// <summary>
	/// Корень из 2 (√2)
	/// </summary>
	public const double Sqrt2D = (double)Sqrt2DE;
	/// <summary>
	/// Корень из 2 (√2)
	/// </summary>
	public const float Sqrt2F = (float)Sqrt2D;
	
	/// <summary>
	/// Натуральный логарифм 2
	/// </summary>
	public const decimal Ln2DE = 0.6931471805599453094172321215m;
	/// <summary>
	/// Натуральный логарифм 2
	/// </summary>
	public const double Ln2D = (double)Ln2DE;
	/// <summary>
	/// Натуральный логарифм 2
	/// </summary>
	public const float Ln2F = (float)Ln2D;
	
	/// <summary>
	/// Натуральный логарифм 10
	/// </summary>
	public const decimal Ln10DE = 2.3025850929940456840179914547m;
	/// <summary>
	/// Натуральный логарифм 10
	/// </summary>
	public const double Ln10D = (double)Ln10DE;
	/// <summary>
	/// Натуральный логарифм 10
	/// </summary>
	public const float Ln10F = (float)Ln10D;
	
	/// <summary>
	/// Золотое сечение (φ)
	/// </summary>
	public const decimal PhiDE = 1.6180339887498948482045868344m;
	/// <summary>
	/// Золотое сечение (φ)
	/// </summary>
	public const double PhiD = (double)PhiDE;
	/// <summary>
	/// Золотое сечение (φ)
	/// </summary>
	public const float PhiF = (float)PhiD;
	
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	[WoowzLibHint(Information.New)]
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
	
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static float ClampF(float A, float Min, float Max) => float.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static double ClampD(double A, double Min, double Max) => double.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static int ClampI(int A, int Min, int Max) => int.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static uint ClampUI(uint A, uint Min, uint Max) => uint.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static short ClampS(short A, short Min, short Max) => short.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static ushort ClampUS(ushort A, ushort Min, ushort Max) => ushort.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static long ClampL(long A, long Min, long Max) => long.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static ulong ClampUL(ulong A, ulong Min, ulong Max) => ulong.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static byte ClampB(byte A, byte Min, byte Max) => byte.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static sbyte ClampSB(sbyte A, sbyte Min, sbyte Max) => sbyte.Clamp(A, Min, Max);
	/// <summary>
	/// Ограничивает число между Min и Max
	/// </summary>
	public static decimal ClampDE(decimal A, decimal Min, decimal Max) => decimal.Clamp(A, Min, Max);
	
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
	[WoowzLibHint(Information.New)]
	public static float CotF(float A) => 1 / WL.Math.TanF(A);
	/// <summary>
	/// Котангенс (0 -> ∞, π/2 -> 0, π -> ∞)
	/// </summary>
	[WoowzLibHint(Information.New)]
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
	/// Арктангенс числа (0 -> 0, 1 -> π/4, -1 -> -π/4)
	/// </summary>
	public static float ATanF(float A) => float.Atan(A);
	/// <summary>
	/// Арктангенс числа (0 -> 0, 1 -> π/4, -1 -> -π/4)
	/// </summary>
	public static double ATanD(double A) => double.Atan(A);
	/// <summary>
	/// Арктангенс по двум координатам ((1, 1) -> π/4, (0, 1) -> 0, (0, -1) -> π, (1, -1) -> 3π/4)
	/// </summary>
	public static float ATan2F(float A, float B) => float.Atan2(A, B);
	/// <summary>
	/// Арктангенс по двум координатам ((1, 1) -> π/4, (0, 1) -> 0, (0, -1) -> π, (1, -1) -> 3π/4)
	/// </summary>
	public static double ATan2D(double A, double B) => double.Atan2(A, B);
	/// <summary>
	/// Синус и косинус числа
	/// </summary>
	public static (float Sin, float Cos) SinCosF(float A) => float.SinCos(A);
	/// <summary>
	/// Синус и косинус числа
	/// </summary>
	public static (double Sin, double Cos) SinCosD(double A) => double.SinCos(A);
	/// <summary>
	/// Гиперболический синус числа (0 -> 0, π/2 -> 2.301, π -> 11.548)
	/// </summary>
	public static float HSinF(float A) => float.Sinh(A);
	/// <summary>
	/// Гиперболический синус числа (0 -> 0, π/2 -> 2.301, π -> 11.548)
	/// </summary>
	public static double HSinD(double A) => double.Sinh(A);
	/// <summary>
	/// Гиперболический косинус числа (0 -> 1, π/2 -> 2.509, π -> 11.592)
	/// </summary>
	public static float HCosF(float A) => float.Cosh(A);
	/// <summary>
	/// Гиперболический косинус числа (0 -> 1, π/2 -> 2.509, π -> 11.592)
	/// </summary>
	public static double HCosD(double A) => double.Cosh(A);
	/// <summary>
	/// Гиперболический тангенс числа (0 -> 0, π/2 -> 0.916, π -> 0.997)
	/// </summary>
	public static float HTanF(float A) => float.Tanh(A);
	/// <summary>
	/// Гиперболический тангенс числа (0 -> 0, π/2 -> 0.916, π -> 0.997)
	/// </summary>
	public static double HTanD(double A) => double.Tanh(A);
	/// <summary>
	/// Гиперболический арксинус числа [-1, 1] (0 -> 0, 1 -> 0.881, -1 -> -0.881)
	/// </summary>
	public static float HASinF(float A) => float.Asinh(A);
	/// <summary>
	/// Гиперболический арксинус числа [-1, 1] (0 -> 0, 1 -> 0.881, -1 -> -0.881)
	/// </summary>
	public static double HASinD(double A) => double.Asinh(A);
	/// <summary>
	/// Гиперболический арккосинус числа [-1, 1] (0 -> 0, 1 -> 1.317, 2 -> 1.762)
	/// </summary>
	public static float HACosF(float A) => float.Acosh(A);
	/// <summary>
	/// Гиперболический арккосинус числа [-1, 1] (0 -> 0, 1 -> 1.317, 2 -> 1.762)
	/// </summary>
	public static double HACosD(double A) => double.Acosh(A);
	/// <summary>
	/// Гиперболический арктангенс числа (0 -> 0, 0.5 -> 0.549, -0.75 -> -0.972)
	/// </summary>
	public static float HATanF(float A) => float.Atanh(A);
	/// <summary>
	/// Гиперболический арктангенс числа (0 -> 0, 0.5 -> 0.549, -0.75 -> -0.972)
	/// </summary>
	public static double HATanD(double A) => double.Atanh(A);
	/// <summary>
	/// Положительный синус числа, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float DSinF(float A) => (WL.Math.SinF(A) + 1) * 0.5f;
	/// <summary>
	/// Положительный синус числа, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double DSinD(double A) => (WL.Math.SinD(A) + 1) * 0.5;
	/// <summary>
	/// Положительный косинус числа, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float DCosF(float A) => (WL.Math.CosF(A) + 1) * 0.5f;
	/// <summary>
	/// Положительный косинус числа, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double DCosD(double A) => (WL.Math.CosD(A) + 1) * 0.5;
	/// <summary>
	/// Синус числа, с линейной скоростью
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float LSinF(float A) => WL.Math.AbsF((WL.Math.WrapF((HalfPiF - A) * 0.5f, PiF) / HalfPiF) - 1);
	/// <summary>
	/// Синус числа, с линейной скоростью
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double LSinD(double A) => WL.Math.AbsD((WL.Math.WrapD((HalfPiD - A) * 0.5, PiD) / HalfPiD) - 1);
	/// <summary>
	/// Косинус числа, с линейной скоростью
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float LCosF(float A) => WL.Math.AbsF((WL.Math.WrapF(A * 0.5f, PiF) / HalfPiF) - 1);
	/// <summary>
	/// Косинус числа, с линейной скоростью
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double LCosD(double A) => WL.Math.AbsD((WL.Math.WrapD(A * 0.5, PiD) / HalfPiD) - 1);
	/// <summary>
	/// Положительный синус числа, с линейной скоростью, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float LDSinF(float A) => (WL.Math.LSinF(A) + 1) * 0.5f;
	/// <summary>
	/// Положительный синус числа, с линейной скоростью, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double LDSinD(double A) => (WL.Math.LSinD(A) + 1) * 0.5;
	/// <summary>
	/// Положительный косинус числа, с линейной скоростью, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static float LDCosF(float A) => (WL.Math.LCosF(A) + 1) * 0.5f;
	/// <summary>
	/// Положительный косинус числа, с линейной скоростью, в диапазоне [0, 1]
	/// </summary>
	[WoowzLibHint(Information.New)]
	public static double LDCosD(double A) => (WL.Math.LCosD(A) + 1) * 0.5;
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Возвращает экспоненту числа eˣ (0 -> 1, 1 -> 2.718, 2 -> 7.389)
	/// </summary>
	public static float ExpF(float A) => float.Exp(A);
	/// <summary>
	/// Возвращает экспоненту числа eˣ (0 -> 1, 1 -> 2.718, 2 -> 7.389)
	/// </summary>
	public static double ExpD(double A) => double.Exp(A);
	
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
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static float SqrF(float A) => WL.Math.Pow2F(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static double SqrD(double A) => WL.Math.Pow2D(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static int SqrI(int A) => WL.Math.Pow2I(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static uint SqrUI(uint A) => WL.Math.Pow2UI(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static long SqrL(long A) => WL.Math.Pow2L(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static ulong SqrUL(ulong A) => WL.Math.Pow2UL(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static decimal SqrDE(decimal A) => WL.Math.Pow2DE(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static float CubeF(float A) => WL.Math.Pow3F(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static double CubeD(double A) => WL.Math.Pow3D(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static int CubeI(int A) => WL.Math.Pow3I(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static uint CubeUI(uint A) => WL.Math.Pow3UI(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static long CubeL(long A) => WL.Math.Pow3L(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static ulong CubeUL(ulong A) => WL.Math.Pow3UL(A);
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static decimal CubeDE(decimal A) => WL.Math.Pow3DE(A);
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static float Pow2F(float A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static double Pow2D(double A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static int Pow2I(int A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static uint Pow2UI(uint A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static long Pow2L(long A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static ulong Pow2UL(ulong A) => A * A;
	/// <summary>
	/// Возводит число в квадрат A² (A^2)
	/// </summary>
	public static decimal Pow2DE(decimal A) => A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static float Pow3F(float A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static double Pow3D(double A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static int Pow3I(int A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static uint Pow3UI(uint A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static long Pow3L(long A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static ulong Pow3UL(ulong A) => A * A * A;
	/// <summary>
	/// Возводит число в куб A³ (A^3)
	/// </summary>
	public static decimal Pow3DE(decimal A) => A * A * A;
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static float Pow4F(float A){
		float A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static double Pow4D(double A){
		double A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int Pow4I(int A){
		int A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static uint Pow4UI(uint A){
		uint A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static long Pow4L(long A){
		long A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static ulong Pow4UL(ulong A){
		ulong A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 4 A⁴ (A^4)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static decimal Pow4DE(decimal A){
		decimal A2 = A * A;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static float Pow8F(float A){
		float A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static double Pow8D(double A){
		double A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int Pow8I(int A){
		int A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static uint Pow8UI(uint A){
		uint A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static long Pow8L(long A){
		long A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static ulong Pow8UL(ulong A){
		ulong A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 8 A⁸ (A^8)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static decimal Pow8DE(decimal A){
		decimal A2 = A * A;
		A2 = A2 * A2;
		return A2 * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static float Pow10F(float A){
		float A2 = A * A;
		float A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static double Pow10D(double A){
		double A2 = A * A;
		double A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int Pow10I(int A){
		int A2 = A * A;
		int A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static uint Pow10UI(uint A){
		uint A2 = A * A;
		uint A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static long Pow10L(long A){
		long A2 = A * A;
		long A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static ulong Pow10UL(ulong A){
		ulong A2 = A * A;
		ulong A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	/// <summary>
	/// Возводит число в степень 10 A¹⁰ (A^10)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static decimal Pow10DE(decimal A){
		decimal A2 = A * A;
		decimal A4 = A2 * A2;
		return (A4 * A4) * A2;
	}
	
	/// <summary>
	/// Корень B из числа A √ᴮA (A^(1/B))
	/// </summary>
	public static float RootF(float A, float B) => WL.Math.PowF(A, 1 / B);
	/// <summary>
	/// Корень B из числа A √ᴮA (A^(1/B))
	/// </summary>
	public static double RootD(double A, double B) => WL.Math.PowD(A, 1 / B);
	/// <summary>
	/// Квадратный корень √A
	/// </summary>
	public static float SqrtF(float A) => float.Sqrt(A);
	/// <summary>
	/// Квадратный корень √A
	/// </summary>
	public static double SqrtD(double A) => double.Sqrt(A);
	/// <summary>
	/// Кубический корень ∛A
	/// </summary>
	public static float CbrtF(float A) => float.Cbrt(A);
	/// <summary>
	/// Кубический корень ∛A
	/// </summary>
	public static double CbrtD(double A) => double.Cbrt(A);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Округляет число к ближайшему чётному числу (0.25 -> 0, 0.5 -> 0, 0.75 -> 1)
	/// </summary>
	public static float RoundF(float A) => float.Round(A);
	/// <summary>
	/// Округляет число к ближайшему чётному числу (0.25 -> 0, 0.5 -> 0, 0.75 -> 1)
	/// </summary>
	public static double RoundD(double A) => double.Round(A);
	/// <summary>
	/// Округляет число к ближайшему чётному числу (0.25 -> 0, 0.5 -> 0, 0.75 -> 1)
	/// </summary>
	public static decimal RoundDE(decimal A) => decimal.Round(A);
	/// <summary>
	/// Округляет число в меньшую сторону (0.25 -> 0, 0.5 -> 0, 0.75 -> 0)
	/// </summary>
	public static float FloorF(float A) => float.Floor(A);
	/// <summary>
	/// Округляет число в меньшую сторону (0.25 -> 0, 0.5 -> 0, 0.75 -> 0)
	/// </summary>
	public static double FloorD(double A) => double.Floor(A);
	/// <summary>
	/// Округляет число в меньшую сторону (0.25 -> 0, 0.5 -> 0, 0.75 -> 0)
	/// </summary>
	public static decimal FloorDE(decimal A) => decimal.Floor(A);
	/// <summary>
	/// Округляет число в большую сторону (0.25 -> 1, 0.5 -> 1, 0.75 -> 1)
	/// </summary>
	public static float CeilF(float A) => float.Ceiling(A);
	/// <summary>
	/// Округляет число в большую сторону (0.25 -> 1, 0.5 -> 1, 0.75 -> 1)
	/// </summary>
	public static double CeilD(double A) => double.Ceiling(A);
	/// <summary>
	/// Округляет число в большую сторону (0.25 -> 1, 0.5 -> 1, 0.75 -> 1)
	/// </summary>
	public static decimal CeilDE(decimal A) => decimal.Ceiling(A);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// Остаток от деления ((7, 3) -> 1, (-7, 3) -> -1, (7.5, 2) -> 1.5)
	/// </summary>
	public static float ModF(float A, float B) => A % B;
	/// <summary>
	/// Остаток от деления ((7, 3) -> 1, (-7, 3) -> -1, (7.5, 2) -> 1.5)
	/// </summary>
	public static double ModD(double A, double B) => A % B;
	
	/// <summary>
	/// Остаток от деления, но в диапазоне [0, ∞] ((7, 3) -> 1, (-7, 3) -> 2, (7.5, 2) -> 1.5)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static float WrapF(float A, float B){
		float R = A % B;
		if(R < 0){
			R += WL.Math.AbsF(B);
		}
		return R;
	}
	/// <summary>
	/// Остаток от деления, но в диапазоне [0, ∞] ((7, 3) -> 1, (-7, 3) -> 2, (7.5, 2) -> 1.5)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static double WrapD(double A, double B){
		double R = A % B;
		if(R < 0){
			R += WL.Math.AbsD(B);
		}
		return R;
	}
	
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
	
	/// <summary>
	/// Убирает дробную часть (3.5612 -> 3)
	/// </summary>
	public static float TruncF(float A) => float.Truncate(A);
	/// <summary>
	/// Убирает дробную часть (3.5612 -> 3)
	/// </summary>
	public static double TruncD(double A) => double.Truncate(A);
	/// <summary>
	/// Убирает дробную часть (3.5612 -> 3)
	/// </summary>
	public static decimal TruncDE(decimal A) => decimal.Truncate(A);
	/// <summary>
	/// Берёт дробную часть (3.5612 -> 0.5612, -2.61 -> -0.61)
	/// </summary>
	public static float FracF(float A) => A - WL.Math.TruncF(A);
	/// <summary>
	/// Берёт дробную часть (3.5612 -> 0.5612, -2.61 -> -0.61)
	/// </summary>
	public static double FracD(double A) => A - WL.Math.TruncD(A);
	/// <summary>
	/// Берёт дробную часть (3.5612 -> 0.5612, -2.61 -> -0.61)
	/// </summary>
	public static decimal FracDE(decimal A) => A - WL.Math.TruncDE(A);
	
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignF(float A) => float.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignD(double A) => double.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignI(int A) => int.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignS(short A) => short.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignL(long A) => long.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignSB(sbyte A) => sbyte.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, -612 -> -1, 0 -> 0)
	/// </summary>
	public static int SignDE(decimal A) => decimal.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, 0 -> 0)
	/// </summary>
	public static int SignUI(uint A) => uint.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, 0 -> 0)
	/// </summary>
	public static int SignUS(ushort A) => ushort.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, 0 -> 0)
	/// </summary>
	public static int SignUL(ulong A) => ulong.Sign(A);
	/// <summary>
	/// Знак числа (12 -> 1, 0 -> 0)
	/// </summary>
	public static int SignB(byte A) => byte.Sign(A);
	
	/// <summary>
	/// Эквивалентно A * B + C, но быстрее и точнее
	/// </summary>
	public static float FmaF(float A, float B, float C) => float.FusedMultiplyAdd(A, B, C);
	/// <summary>
	/// Эквивалентно A * B + C, но быстрее и точнее
	/// </summary>
	public static double FmaD(double A, double B, double C) => double.FusedMultiplyAdd(A, B, C);
	
	// ----------------------------------------------------------------------
	
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignF(float A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignD(double A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignI(int A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignS(short A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignL(long A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignSB(sbyte A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignDE(decimal A) => (A > 0 ? 1 : 0) - (A < 0 ? 1 : 0);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignUI(uint A) => A == 0 ? 0 : 1;
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignUS(ushort A) => A == 0 ? 0 : 1;
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignUL(ulong A) => A == 0 ? 0 : 1;
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastSignB(byte A) => A == 0 ? 0 : 1;
	
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static float FastClampF(float A, float Min, float Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static double FastClampD(double A, double Min, double Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static int FastClampI(int A, int Min, int Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static uint FastClampUI(uint A, uint Min, uint Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static short FastClampS(short A, short Min, short Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static ushort FastClampUS(ushort A, ushort Min, ushort Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static long FastClampL(long A, long Min, long Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static ulong FastClampUL(ulong A, ulong Min, ulong Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static byte FastClampB(byte A, byte Min, byte Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static sbyte FastClampSB(sbyte A, sbyte Min, sbyte Max) => A < Min ? Min : (A > Max ? Max : A);
	/// <summary>
	/// ~
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[WoowzLibHint(Information.New)]
	public static decimal FastClampDE(decimal A, decimal Min, decimal Max) => A < Min ? Min : (A > Max ? Max : A);
	
	// ----------------------------------------------------------------------
	/* Конец генератора */
}