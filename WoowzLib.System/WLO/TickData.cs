using System.Runtime.CompilerServices;

namespace WLO;

/// <summary>
/// Содержит в себе информацию об тиках
/// </summary>
public struct TickData{
    public TickData(long Start, long End, uint TickCount){ this.Start = Start; this.End = End; this.TickCount = TickCount; }
    
    /// <summary>
    /// Начальное время
    /// </summary>
    public double Start;
    
    /// <summary>
    /// Конечное время
    /// </summary>
    public double End;

    /// <summary>
    /// Кол-во прошедших кадров
    /// </summary>
    public uint TickCount;

    /// <summary>
    /// Прошедшее время
    /// </summary>
    public double Delta => End - Start;

    /// <summary>
    /// Время между кадрами в секундах
    /// </summary>
    public double DeltaTime => Delta;

    /// <summary>
    /// Текущий FPS
    /// </summary>
    public double FPS => DeltaTimeToFPS(DeltaTime);

    /// <summary>
    /// Превращает DeltaTime в FPS
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DeltaTimeToFPS(double DeltaTime) => DeltaTime > 0 ? 1 / DeltaTime : 0;
    
    /// <summary>
    /// Превращает FPS в DeltaTime
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double FPSToDeltaTime(double FPS) => FPS > 0 ? 1 / FPS : 0;

    public override string ToString() => "TickData(" + WL.String.LimitF(FPS, 1) + " (" + WL.String.LimitF(DeltaTime, 5) + "), " + WL.String.LimitF(Start, 2) + " - " + WL.String.LimitF(End, 2) + " = " + WL.String.LimitF(Delta, 2) + ", " + TickCount + " Tick)";
}