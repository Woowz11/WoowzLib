using WL;

namespace WLO.GL;

public enum DataCount : int{
    /// <summary>
    /// <c>float</c>, <c>int</c>
    /// </summary>
    One = 1,
    /// <summary>
    /// <c>vec2</c>, <c>ivec2</c>
    /// </summary>
    Two = 2,
    /// <summary>
    /// <c>vec3</c>, <c>ivec3</c>
    /// </summary>
    Three = 3,
    /// <summary>
    /// <c>vec4</c>, <c>ivec4</c>
    /// </summary>
    Four = 4
}

/// <summary>
/// (VERTEXARRAY)
/// </summary>
public class VertexConfig : GLResource{
    public VertexConfig(Render.GL Context) : base(Context){
        try{
            uint[] ID__ = new uint[1];
            WL.GL.Native.glGenVertexArrays(1, ID__);
            ID = ID__[0];
            if(ID == 0){ throw new Exception("Не получилось создать шейдер в glGenVertexArrays!"); }
            
            __Finish(WL.GL.Native.GL_BUFFER, "VertexConfig");

            if(WL.GL.Debug.LogCreate){ Logger.Info("Создан VertexConfig [" + this + "]!"); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании VertexConfig [" + this + "]!", e);
        }
    }
    
    protected override void __Destroy(){
        uint ID__ = ID;
        WL.GL.Native.glDeleteVertexArrays(1, ref ID__);
    }
    
    /// <summary>
    /// Использовать этот VertexConfig
    /// </summary>
    public VertexConfig Use(){
        Context.CurrentVertexConfig = this;
        return this;
    }

    /// <summary>
    /// Присоединённые буферы
    /// </summary>
    private readonly Dictionary<uint, Buffer> ConnectedBuffers = new Dictionary<uint, Buffer>();

    /// <summary>
    /// Добавляет буфер в VertexConfig
    /// </summary>
    /// <param name="Buffer">Буфер</param>
    /// <param name="Location">Номер в шейдере (<c>layout(location = 0) in ...</c>)</param>
    /// <param name="Count">Кол-во значений (Если равен 2 -> <c>vec2</c>, если равен 3 -> <c>vec3</c> и т.д)</param>
    /// <param name="Stride">Длина одного набора данных (Длина повторок (<b>всех!</b>), пример: XYZ,RGBA,XYZ,RGBA... -> будет: XYZ (3) + RGBA (4) = 7 (XYZ, RGBA))</param>
    /// <param name="Offset">Смещение в элементах. Пример: XYZ,RGBA,XYZ,RGBA... -> для BA будет: XYZ (3) + RG (2) = 5 </param>
    /// <param name="Normalized">Преобразовывать целочисленные значения? Пример: 0...255 -> 0.0...1.0 (Работает только для дробных буферов!)</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public VertexConfig Connect(Buffer Buffer, uint Location, DataCount Count, int Stride, int Offset, bool Normalized){
        try{
            if(ConnectedBuffers.TryGetValue(Location, out Buffer? Existing) && Existing == Buffer){ return this; }

            Use();
            Buffer.__Use();

            if(Buffer.GLValue == GLValue.Float){
                WL.GL.Native.glVertexAttribPointer(
                    Location,
                    (int)Count,
                    (uint)Buffer.GLValue,
                    Normalized,
                    Stride * Buffer.ElementBSize(),
                    new IntPtr(Offset * Buffer.ElementBSize())
                );
            }else{
                if(Normalized){ Logger.Warn("Нормализация не работает для целочисленного GL буфера!"); }

                WL.GL.Native.glVertexAttribIPointer(
                    Location,
                    (int)Count,
                    (uint)Buffer.GLValue,
                    Stride * Buffer.ElementBSize(),
                    new IntPtr(Offset * Buffer.ElementBSize())
                );
            }
            
            ConnectedBuffers[Location] = Buffer;

            if(WL.GL.Debug.LogVertexConfig){ Logger.Info("Присоединён буфер [" + Buffer + "] к VertexConfig [" + this + "]! Параметры: " + Location + ", " + Count + ", " + Stride + ", " + Offset + ", " + Normalized); }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при присоединении буфера [" + Buffer + "] к VertexConfig [" + this + "]!\nЛокация: " + Location + "\nКол-во значений: " + Count + "\nДлина набора: " + Stride + "\nСмещение: " + Offset + "\nНормализация: " + Normalized, e);
        }

        return this;
    }
}