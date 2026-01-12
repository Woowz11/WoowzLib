using System.Runtime.InteropServices;

namespace WLO.GLFW;

/// <summary>
/// GLFW окно
/// </summary>
public class Window : IDisposable{
    public Window(int Width = 800, int Height = 600, string Title = "WL Window"){
        try{
            IntPtr Title__ = Marshal.StringToHGlobalAnsi(Title);
            
            Hanlde = WL.GLFW.Native.glfwCreateWindow(Width, Height, Title__, IntPtr.Zero, IntPtr.Zero);
            
            Marshal.FreeHGlobal(Title__);

            if(Hanlde == IntPtr.Zero){ throw new Exception("Не получилось создать окно внутри glfwCreateWindow!"); }

            this.Width  = Width;
            this.Height = Height;
            this.Title  = Title;
            
            ID = Hanlde.ToInt64();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Ссылка на окно (<c>GLFWwindow*</c>)
    /// </summary>
    public IntPtr Hanlde{ get; private set; }

    /// <summary>
    /// Уникальный ID окна (основан на Handle)
    /// </summary>
    public long ID{ get; }

    /// <summary>
    /// Уничтожено окно?
    /// </summary>
    public bool Destroyed => Hanlde == IntPtr.Zero;

    /// <summary>
    /// Проверяет, уничтожено окно или нет? (Выдаёт ошибку)
    /// </summary>
    public Window CheckDestroyed(){ if(Destroyed){ throw new Exception("Окно [" + this + "] уничтожено!"); } return this; }

    /// <summary>
    /// Ширина окна
    /// </summary>
    public int Width{
        get => __Width;
        set{
            if(__Width == value){ return; }
            __Width = value;
            
            CheckDestroyed();
            WL.GLFW.Native.glfwSetWindowSize(Hanlde, __Width, __Height);
        }
    }
    private int __Width;
    
    /// <summary>
    /// Высота окна
    /// </summary>
    public int Height{
        get => __Height;
        set{
            if(__Height == value){ return; }
            __Height = value;
            
            CheckDestroyed();
            WL.GLFW.Native.glfwSetWindowSize(Hanlde, __Width, __Height);
        }
    }
    private int __Height;

    /// <summary>
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            if(__Title == value){ return; }
            __Title = value;

            CheckDestroyed();

            IntPtr Title__ = Marshal.StringToHGlobalAnsi(__Title);
            WL.GLFW.Native.glfwSetWindowTitle(Hanlde, Title__);
            Marshal.FreeHGlobal(Title__);
        }
    }
    private string __Title;

    #region Overwrite

        public void Dispose(){
            
        }

    #endregion
}