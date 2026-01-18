namespace WLO;

/// <summary>
/// Является основой для графических API (OpenGL, Vulkan, DirectX, и т.д)
/// </summary>
public abstract class RenderAPI{
    protected RenderAPI(RenderAPI? Parent = null){
        if(Parent != null){
            this.Parent = Parent;
            Parent.Child.Add(this);
        }
    }

    protected RenderAPI Parent;

    protected readonly List<RenderAPI> Child = [];

    protected abstract void MakeCurrent(Drawable Target);
    protected abstract void DoneCurrent();

    public void Context(Drawable Target, Action Action){
        CurrentDrawable = Target;
        CurrentRenderAPI = this;
        
        MakeCurrent(Target);
        try{
            Action();
        }finally{
            DoneCurrent();
        }

        CurrentDrawable = null;
        CurrentRenderAPI = null;
    }

    public static RenderAPI? CurrentRenderAPI;
    public static Drawable?  CurrentDrawable;
    
    public abstract void __Stop();
    
    public bool Shared(RenderAPI Other){
        return this == Other || Parent == Other || Other.Parent == this;
    }
    
    public const int __OpenGLMajor = 4;
    public const int __OpenGLMinor = 6;
}