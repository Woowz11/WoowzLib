namespace WLO;

/// <summary>
/// Является основой для графических API (OpenGL, Vulkan, DirectX, и т.д)
/// </summary>
public abstract class RenderAPI{
    protected RenderAPI(RenderAPI? Parent = null){
        try{
            if(Parent != null){
                this.Parent = Parent;
                Parent.Child.Add(this);
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании RenderAPI [" + this + "]!\nРодитель: " + Parent, e);
        }
    }

    /// <summary>
    /// Привязанный родительский RenderAPI (К примеру для OpenGL, влияет на общие ресурсы)
    /// </summary>
    public readonly RenderAPI? Parent;

    /// <summary>
    /// Все привязанные к этому RenderAPI
    /// </summary>
    public readonly List<RenderAPI> Child = [];

    protected abstract void __StartContext(Drawable Target);
    protected abstract void __StopContext();

    /// <summary>
    /// Вызвать функции RenderAPI у указанной цели
    /// </summary>
    /// <param name="Target">Цель</param>
    /// <param name="Action">Действия</param>
    public void Context(Drawable Target, Action Action){
        try{
            CurrentDrawable = Target;
            CurrentRenderAPI = this;
        
            __StartContext(Target);
            try{
                Action();
            }catch(Exception e){
                throw new Exception("Произошла ошибка при вызове действий у контекста [" + this + "]!\nЦель: " + Target);
            }finally{
                __StopContext();
            }

            CurrentDrawable = null;
            CurrentRenderAPI = null;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при вызове контекста [" + this + "]!\nЦель: " + Target);
        }
    }

    /// <summary>
    /// Текущий RenderAPI в контексте
    /// </summary>
    public static RenderAPI? CurrentRenderAPI{ get; private set; }
    /// <summary>
    /// Текущая цель в контексте
    /// </summary>
    public static Drawable?  CurrentDrawable { get; private set; }
    
    public bool Shared(RenderAPI Other){
        return this == Other || Parent == Other || Other.Parent == this;
    }
    
    public const int __OpenGLMajor = 4;
    public const int __OpenGLMinor = 6;
}