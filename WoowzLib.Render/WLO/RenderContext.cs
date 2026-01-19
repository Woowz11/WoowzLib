using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class RenderContext{
    public RenderContext(RenderSurface RS){
        try{
            RenderSurface = RS;
            
            IntPtr HWND = RS.RenderHandle();

            WL.Render.Native.VkWin32SurfaceCreateInfoKHR SurfaceInfo = new WL.Render.Native.VkWin32SurfaceCreateInfoKHR{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_WIN32_SURFACE_CREATE_INFO_KHR,
                hinstance = WL.System.Native.Windows.GetModuleHandle(null),
                hwnd = HWND
            };

            int Result = WL.Render.Native.vkCreateWin32SurfaceKHR(WL.Render.VK, ref SurfaceInfo, IntPtr.Zero, out IntPtr Surface);
            if(Result != 0){ throw new Exception("Произошла ошибка при вызове vkCreateWin32SurfaceKHR! Код: " + Result); }
            
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceCapabilitiesKHR(WL.Render.GPU, Surface, out WL.Render.Native.VkSurfaceCapabilitiesKHR Caps);
            if(Result != 0){ throw new Exception("Не получилось узнать возможности поверхности через vkGetPhysicalDeviceSurfaceCapabilitiesKHR! Код: " + Result); }

            uint FormatTotal = 0;
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceFormatsKHR(WL.Render.GPU, Surface, ref FormatTotal, IntPtr.Zero);
            if(Result != 0){ throw new Exception("Не получилось узнать кол-во форматов поверхности! Код: " + Result); }

            IntPtr Formats = WL.System.Native.MemoryEmpty<WL.Render.Native.VkSurfaceFormatKHR>((int)FormatTotal);
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceFormatsKHR(WL.Render.GPU, Surface, ref FormatTotal, Formats);
            if(Result != 0){ throw new Exception("Не получилось получить список форматов поверхности! Код: " + Result); }

            WL.Render.Native.VkSurfaceFormatKHR Format = Marshal.PtrToStructure<WL.Render.Native.VkSurfaceFormatKHR>(Formats);
            WL.System.Native.Free(Formats);

            uint PresentModeTotal = 0;
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfacePresentModesKHR(WL.Render.GPU, Surface, ref PresentModeTotal, IntPtr.Zero);
            if(Result != 0){ throw new Exception("Не получилось узнать кол-во \"Как показывается кадр\"! Код: " + Result); }

            IntPtr PresentModes = WL.System.Native.MemoryEmpty<int>((int)PresentModeTotal);
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfacePresentModesKHR(WL.Render.GPU, Surface, ref PresentModeTotal, PresentModes);
            if(Result != 0){ throw new Exception("Не получилось получить список \"Как показывается кадр\"! Код: " + Result); }

            uint PresentMode = WL.Render.Native.VK_PRESENT_MODE_FIFO_KHR;

            uint ImageTotal = Caps.minImageCount + 1;
            if(Caps.maxImageCount > 0 && ImageTotal > Caps.maxImageCount){ ImageTotal = Caps.maxImageCount; }

            WL.Render.Native.VkSwapchainCreateInfoKHR SwapInfo = new WL.Render.Native.VkSwapchainCreateInfoKHR{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_SWAPCHAIN_CREATE_INFO_KHR,
                surface = Surface,
                
                minImageCount   = ImageTotal,
                imageFormat     = Format.format,
                imageColorSpace = Format.colorSpace,
                
                imageExtent      = Caps.currentExtent,
                imageArrayLayers = 1,
                imageUsage       = WL.Render.Native.VK_IMAGE_USAGE_COLOR_ATTACHMENT_BIT,
                
                imageSharingMode = WL.Render.Native.VK_SHARING_MODE_EXCLUSIVE,
                preTransform     = Caps.currentTransform,
                compositeAlpha   = WL.Render.Native.VK_COMPOSITE_ALPHA_OPAQUE_BIT_KHR,
                presentMode      = PresentMode,
                clipped          = 1,
                
                oldSwapchain = IntPtr.Zero
            };
            
            Result = WL.Render.Native.vkCreateSwapchainKHR(WL.Render.Device, ref SwapInfo, IntPtr.Zero, out IntPtr Swapchain);
            if(Result != 0){ throw new Exception("Произошла ошибка при создании Swapchain через vkCreateSwapchainKHR! Код: " + Result); }

            this.Swapchain = Swapchain;
            
            WL.System.Native.Free(PresentModes);

            uint SwapchainImageTotal = 0;
            Result = WL.Render.Native.vkGetSwapchainImagesKHR(WL.Render.Device, Swapchain, ref SwapchainImageTotal, IntPtr.Zero);
            if(Result != 0){ throw new Exception("Не получилось узнать кол-во изображений Swapchain! Код: " + Result); }

            IntPtr SwapchainImages__ = WL.System.Native.MemoryEmpty<IntPtr>((int)SwapchainImageTotal);
            Result = WL.Render.Native.vkGetSwapchainImagesKHR(WL.Render.Device, Swapchain, ref SwapchainImageTotal, SwapchainImages__);
            if(Result != 0){ throw new Exception("Не получилось получить список изображений Swapchain! Код: " + Result); }

            SwapchainImages = new IntPtr[SwapchainImageTotal];
            for(int i = 0; i < SwapchainImageTotal; i++){
                SwapchainImages[i] = Marshal.ReadIntPtr(SwapchainImages__, i * IntPtr.Size);
            }
            WL.System.Native.Free(SwapchainImages__);

            ImageFormat = Format.format;
            Extent = (Caps.currentExtent.width, Caps.currentExtent.height);
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании RenderContext [" + this + "]!", e);
        }
    }
    
    public RenderSurface RenderSurface  { get; private set; }
    public IntPtr        Surface        { get; private set; }
    public IntPtr        Swapchain      { get; private set; }
    public IntPtr[]      SwapchainImages{ get; private set; }
    public uint          ImageFormat    { get; private set; }
    public (uint, uint)  Extent         { get; private set; }

    public bool Alive => Surface != IntPtr.Zero && RenderSurface.RenderHandle() != IntPtr.Zero;

    public void __Destroy(){
        try{
            if(Swapchain != IntPtr.Zero){
                WL.Render.Native.vkDestroySwapchainKHR(WL.Render.Device, Swapchain, IntPtr.Zero);
                Swapchain = IntPtr.Zero;
            }
            
            WL.Render.Native.vkDestroySurfaceKHR(WL.Render.VK, Surface, IntPtr.Zero);
                
            Surface = IntPtr.Zero;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при уничтожении [" + this + "]!", e);
        }
    }
}