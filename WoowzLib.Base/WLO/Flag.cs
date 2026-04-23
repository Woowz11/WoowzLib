using System.Runtime.CompilerServices;

namespace WLO;

/// <summary>
/// Для работы с флагами
/// </summary>
public static class Flag{
    /// <summary>
    /// Проверяет, есть ли флаг внутри флагов
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<E>(E Flags, E Flag) where E : struct, Enum{
        ulong Flags__ = Convert.ToUInt64(Flags);
        ulong Flag__  = Convert.ToUInt64(Flag );
        return (Flags__ & Flag__) == Flag__;
    }
}