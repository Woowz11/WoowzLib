using System.Numerics;
using WLO;
using WLO.Rect;

namespace WL;

public static partial class System{
    public static class Draw{
        /// <summary>
        /// Получает DeviceContext окна
        /// </summary>
        public static DeviceContext DeviceContext(Window Window) => Window.DeviceContext;

        /// <summary>
        /// Получает DeviceContext окна на прямую
        /// </summary>
        public static IntPtr DeviceContextPointer(Window Window){
            try{
                Window.CheckAlive();
                
                IntPtr Result = WL.Native.Raw.Windows.GetDC(Window.Handle);
                if(Result == IntPtr.Zero){ throw new Exception("GetDC вернул ноль!"); }

                return Result;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при получении Device Context у окна [" + Window + "]!", e);
            }
        }
        
        /// <summary>
        /// Освобождает DeviceContext
        /// </summary>
        public static void ReleaseDeviceContext(IntPtr WindowHandle, IntPtr DeviceContextHandle){
            try{
                if(DeviceContextHandle == 0){ throw new Exception("Уже очищенный!"); }

                WL.Native.Raw.Windows.ReleaseDC(WindowHandle, DeviceContextHandle);
            }catch(Exception e){
                throw new Exception("Произошла ошибка при очистке Device Context!\nDevice Context: " + DeviceContextHandle + "\nОкно: " + WindowHandle, e);
            }
        }

        /// <summary>
        /// Освобождает DeviceContext
        /// </summary>
        public static void ReleaseDeviceContext(Window? Window, DeviceContext DeviceContext){
            try{
                if(Window == null){ throw new Exception("Не указано окно!"); }
                
                ReleaseDeviceContext(Window.Handle, DeviceContext.Handle);
                
                DeviceContext.Handle = 0;
                DeviceContext.Window = null;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при очистке Device Context [" + DeviceContext + "]!\nОкно: " + WL.__Base.Other.ToString(Window), e);
            }
        }
        
        // ----------------------------------------------------------------------

        public static void ApplyRectangle(IntPtr DC, RectI Rect){
            // wip
        }
    }
}