using WLO;

namespace WL{
    /// <summary>
    /// Работа с байтами
    /// </summary>
    [WLModule(-7500, 2)]
    public static class Byte{
        /// <summary>
        /// Вычисляет размер объекта в байтах
        /// </summary>
        public static int Size(object? Object){
            try{
                switch(Object){
                    case null:
                        return (int)ByteSize.Null;
                    case ByteObject BO:
                        return BO.BSize();
                    case Array A:
                        return SizeArray(A);
                }

                try{
                    return (int)SizePrimitive(Object);
                }catch{ /**/ }

                throw new Exception("Неизвестно как вычислять размер у объекта типа [" + Object.GetType() + "]!");
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении размера в байтах у объекта [" + Object + "]!", e);
            }
        }

        /// <summary>
        /// Вычисляет размер массива
        /// </summary>
        public static int SizeArray(Array Array){
            try{
                Type ElementType = Array.GetType().GetElementType()!;
                int  ElementSize = Size(ElementType);
                return ElementSize * Array.Length;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при вычислении размера массива в байтах [" + Array + "]!", e);
            }
        }
        
        /// <summary>
        /// Вычисляет размер у примитивов
        /// </summary>
        /// <param name="Primitive"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ByteSize SizePrimitive(object Primitive){
            if(Primitive is Type T){ return SizePrimitiveType(T); }

            return Primitive switch{
                int    => ByteSize.Int   ,
                uint   => ByteSize.UInt  ,
                float  => ByteSize.Float ,
                double => ByteSize.Double,
                byte   => ByteSize.Byte  ,
                sbyte  => ByteSize.SByte ,
                short  => ByteSize.Short ,
                ushort => ByteSize.UShort,
                long   => ByteSize.Long  ,
                ulong  => ByteSize.ULong ,
                char   => ByteSize.Char  ,
                bool   => ByteSize.Bool  ,
                _ => throw new Exception("Неизвестный примитив [" + Primitive.GetType() + "], неизвестно как вычислять размер!")
            };
        }

        /// <summary>
        /// Вычисляет размер у типа примитивов
        /// </summary>
        /// <param name="Type">Тип примитивов [<c>typeof(double)</c>, <c>typeof(ushort)</c>]</param>
        public static ByteSize SizePrimitiveType(Type Type){
            if(Type == typeof(int   )) return ByteSize.Int   ;
            if(Type == typeof(uint  )) return ByteSize.UInt  ;
            if(Type == typeof(float )) return ByteSize.Float ;
            if(Type == typeof(double)) return ByteSize.Double;
            if(Type == typeof(byte  )) return ByteSize.Byte  ;
            if(Type == typeof(sbyte )) return ByteSize.SByte ;
            if(Type == typeof(short )) return ByteSize.Short ;
            if(Type == typeof(ushort)) return ByteSize.UShort;
            if(Type == typeof(long  )) return ByteSize.Long  ;
            if(Type == typeof(ulong )) return ByteSize.ULong ;
            if(Type == typeof(char  )) return ByteSize.Char  ;
            if(Type == typeof(bool  )) return ByteSize.Bool  ;
            throw new Exception("Неизвестный примитивный тип [" + Type.Name + "], неизвестно как вычислять размер!");
        }
    }
}