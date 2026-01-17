using System.Runtime.CompilerServices;

namespace WLO.WinForm;

public class Window : IDisposable{
    /// <summary>
    /// Создаёт WinForm окно
    /// </summary>
    /// <param name="Width">Ширина окна</param>
    /// <param name="Height">Высота окна</param>
    /// <param name="Title">Название окна</param>
    public Window(uint Width = 800, uint Height = 600, string Title = "WL Window"){
        try{
            Form = new Form();
            ID   = RuntimeHelpers.GetHashCode(Form);
            
            this.Title = Title;
            Size  = new Vector2U(Width, Height);
            
            __X = -1;
            __Y = -1;
            Position = Vector2I.Zero;
            Focused  = Form.Focused;

            Form.Closing += (_, e__) => {
                try{
                    OnClose?.Invoke(this);
                    
                    if(!DisableOnClose){ if(!ShouldDestroy){ WaitDestroy(); } }

                    e__.Cancel = true;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при закрытии окна [" + this + "], через крестик!", e);
                }
            };
            
            Form.Resize += (_, _) => {
                __Width  = (uint)Form.Width;
                __Height = (uint)Form.Height;

                try{
                    OnResize?.Invoke(this, __Width, __Height);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение размера окна [" + this + "]!\nШирина: " + Width + "\nВысота: " + Height, e);
                }
            };

            Form.Move += (_, _) => {
                __X = Form.Location.X;
                __Y = Form.Location.Y;

                try{
                    OnMove?.Invoke(this, X, Y);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение позиции окна [" + this + "]!\nX: " + X + "\nY: " + Y, e);
                }
            };

            Form.Activated += (_, _) => {
                Focused = true;
                
                try{
                    OnFocus?.Invoke(this, Focused);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение фокуса окна [" + this + "] на true!\nФокус: " + Focused, e);
                }
            };
            
            Form.Deactivate += (_, _) => {
                Focused = false;
                
                try{
                    OnFocus?.Invoke(this, Focused);   
                }catch(Exception e){
                    Logger.Error("Произошла ошибка при вызове ивентов на изменение фокуса окна [" + this + "] на false!\nФокус: " + Focused, e);
                }
            };
            
            Thread Thread__ = new Thread(() => {
                try{
                    Application.Run(Form);
                }catch(Exception e){
                    Logger.Error("Произошла ошибка в потоке WinForm окна [" + this + "]!", e);
                }
            });
            Thread__.SetApartmentState(ApartmentState.STA);
            Thread__.IsBackground = true;
            Thread__.Start();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании WinForm окна [" + this + "]!", e);
        }
    }

    /// <summary>
    /// Уникальный ID окна (основан на Handle)
    /// </summary>
    public long ID{ get; protected set; }
    
    /// <summary>
    /// Оригинальное окно
    /// </summary>
    public Form? Form{ get; private set; }

    /// <summary>
    /// Вызывает функции окна в нужном для него потоке
    /// </summary>
    private void __Invoke(Action Action){
        try{
            CheckDestroyed();
            
            if(Form!.InvokeRequired){
                Form.Invoke(Action);
            }else{
                Action.Invoke();
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при попытке вызова действий WinForm окну [" + this + "]!", e);
        }
    }
    
    /// <summary>
    /// Уничтожено окно?
    /// </summary>
    public bool Destroyed => ID == -1;
    
    /// <summary>
    /// Проверяет, уничтожено окно или нет? (Выдаёт ошибку)
    /// </summary>
    public Window CheckDestroyed(){ return Destroyed ? throw new Exception("Окно [" + this + "] уничтожено!") : this; }
    
    #region Events
    
        /// <summary>
        /// Вызывается перед уничтожением окна
        /// </summary>
        public event WindowEvent? OnDestroy;
            
        /// <summary>
        /// Вызывается перед закрытием окна (на крестик)
        /// </summary>
        public event WindowEvent? OnClose;
        /// <summary>
        /// Отключает дефолтный ивент OnClose
        /// </summary>
        public bool DisableOnClose;

        /// <summary>
        /// Вызывается при изменении размера окна
        /// </summary>
        public event WindowEvent_Size? OnResize;
            
        /// <summary>
        /// Вызывается при изменении позиции окна
        /// </summary>
        public event WindowEvent_Position? OnMove;
            
        /// <summary>
        /// Вызывается при изменении фокуса окна
        /// </summary>
        public event WindowEvent_Focus? OnFocus;

        #region Delegates

            public delegate void WindowEvent(Window Window);
            public delegate void WindowEvent_Size(Window Window, uint Width, uint Height);
            public delegate void WindowEvent_Position(Window Window, int X, int Y);
            public delegate void WindowEvent_Focus(Window Window, bool Focus);
                
        #endregion
    
    #endregion
    
    /// <summary>
    /// Окно в фокусе?
    /// </summary>
    public bool Focused{ get; private set; }
    
    /// <summary>
    /// Ширина окна
    /// </summary>
    public uint Width{
        get => __Width;
        set{
            try{
                if(__Width == value){ return; }
                __Width = value;

                __Invoke(() => {
                    Form!.Width = (int)__Width;
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке ширины окну [" + this + "]!\nШирина: " + value, e);
            }
        }
    }
    private uint __Width;
    
    /// <summary>
    /// Высота окна
    /// </summary>
    public uint Height{
        get => __Height;
        set{
            try{
                if(__Height == value){ return; }
                __Height = value;

                __Invoke(() => {
                    Form!.Height = (int)__Height;
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке высоты окну [" + this + "]!\nВысота: " + value, e);
            }
        }
    }

    private uint __Height;
    
    /// <summary>
    /// Размер окна
    /// </summary>
    public Vector2U Size{
        get => new Vector2U(__Width, __Height);
        set{
            try{
                if(__Width == value.X && __Height == value.Y){ return; }
                __Width  = value.X;
                __Height = value.Y;

                __Invoke(() => {
                    Form!.Width = (int)__Width;
                    Form!.Height = (int)__Height;
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке размера окну [" + this + "]!\nРазмер: " + value, e);
            }
        }
    }
    
    /// <summary>
    /// Позиция окна по X
    /// </summary>
    public int X{
        get => __X;
        set{
            try{
                if(__X == value){ return; }
                __X = value;

                __Invoke(() => {
                    Form!.Location = new Point(__X, __Y);
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции по X у окна [" + this + "]!\nX: " + value, e);
            }
        }
    }
    private int __X;
    
    /// <summary>
    /// Позиция окна по Y
    /// </summary>
    public int Y{
        get => __Y;
        set{
            try{
                if(__Y == value){ return; }
                __Y = value;

                __Invoke(() => {
                    Form!.Location = new Point(__X, __Y);
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции по Y у окна [" + this + "]!\nY: " + value, e);
            }
        }
    }
    private int __Y;

    /// <summary>
    /// Позиция окна
    /// </summary>
    public Vector2I Position{
        get => new Vector2I(__X, __Y);
        set{
            try{
                if(__X == value.X && __Y == value.Y){ return; }
                __X = value.X;
                __Y = value.Y;

                __Invoke(() => {
                    Form!.Location = new Point(__X, __Y);
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции у окна [" + this + "]!\nПозиция: " + value, e);
            }
        }
    }

    /// <summary>
    /// Позиция и размер окна
    /// </summary>
    public RectI Rect{
        get => new RectI(__X, __Y, (int)__Width, (int)__Height);
        set{
            try{
                Position = new Vector2I(      value.X    ,       value.Y     );
                Size     = new Vector2U((uint)value.Width, (uint)value.Height);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке позиции и размера у окна [" + this + "]!\nRect: " + value, e);
            }
        }
    }
    
    /// <summary>
    /// Название окна
    /// </summary>
    public string Title{
        get => __Title;
        set{
            try{
                if(__Title == value){ return; }
                __Title = value;

                __Invoke(() => {
                    Form!.Text = __Title;
                });
            }catch(Exception e){
                throw new Exception("Произошла ошибка при установке названия окну [" + this + "]!\nНазвание: \"" + value + "\"", e);
            }
        }
    }
    private string __Title;
    
    /// <summary>
    /// Окно должно уничтожиться? (При получении уничтожает окно если должно)
    /// </summary>
    public bool ShouldDestroy{ get; private set; }
    
    /// <summary>
    /// Добавляет окно в очередь на уничтожение
    /// </summary>
    public void WaitDestroy(){
        ShouldDestroy = true;
    }
    
    /// <summary>
    /// Уничтожает окно
    /// </summary>
    public void Destroy(){
        try{
            CheckDestroyed();
            
            try{
                OnDestroy?.Invoke(this);   
            }catch(Exception e){
                Logger.Error("Произошла ошибка при вызове ивентов уничтожения окна [" + this + "]!", e);
            }
            
            Form!.Close();
            Form = null;
            
            ID = -1;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении окна [" + this + "]!", e);
        }
    }
    
    #region Overwride
    
        public void Dispose(){
            try{
                if(Destroyed){ return; }
                Destroy();
            }catch{ /**/ }
        }
            
        public override string ToString(){
            return "WinForm.Window(\"" + Title + "\", " + X + ":" + Y + ", " + Width + "x" + Height + ", " + (Destroyed ? "Уничтожено" : ID) + ")";
        }

        public override bool Equals(object? obj){
            if(obj is not Window other){ return false; }
            return ID == other.ID;
        }

        public override int GetHashCode(){
            return ID.GetHashCode();
        }

        public static bool operator ==(Window? A, Window? B){
            if(ReferenceEquals(A, B)){ return true; }
            if(A is null || B is null){ return false; }
            return A.ID == B.ID;
        }

        public static bool operator !=(Window? A, Window? B){
            return !(A == B);
        }

    #endregion
}