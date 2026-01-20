using System.Runtime.InteropServices;
using WLO;

namespace WL.WLO;

public class RenderContext{
    public RenderContext(RenderSurface RS){
        try{
            RenderSurface = RS;
            
            IntPtr Handle = RS.RenderHandle();

            if(Handle == IntPtr.Zero){ throw new Exception("Не найден Handle у RenderSurface!"); }

            WL.Render.Native.VkWin32SurfaceCreateInfoKHR SurfaceInfo = new WL.Render.Native.VkWin32SurfaceCreateInfoKHR{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_WIN32_SURFACE_CREATE_INFO_KHR,
                hinstance = WL.System.Native.Windows.GetModuleHandle(null),
                hwnd = Handle
            };
            
            int Result = WL.Render.Native.vkCreateWin32SurfaceKHR(WL.Render.VK, ref SurfaceInfo, IntPtr.Zero, out IntPtr Surface__);
            if(Result != 0){ throw new Exception("Произошла ошибка при вызове vkCreateWin32SurfaceKHR! Код: " + Result); }

            Surface = Surface__;
            
            __CreateSwapchain();
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании RenderContext [" + this + "]!", e);
        }
    }

    public void __CreateSwapchain(){
        try{
            if(this.Swapchain != IntPtr.Zero){
                if(Framebuffers != null){
                    foreach(IntPtr Framebuffer in Framebuffers){
                        WL.Render.Native.vkDestroyFramebuffer(WL.Render.Device, Framebuffer, IntPtr.Zero);
                    }
                    Framebuffers = null;
                }

                if(ImageViews != null){
                    foreach(IntPtr ImageView in ImageViews){
                        WL.Render.Native.vkDestroyImageView(WL.Render.Device, ImageView, IntPtr.Zero);
                    }
                    ImageViews = null;
                }
                
                WL.Render.Native.vkDestroySwapchainKHR(WL.Render.Device, this.Swapchain, IntPtr.Zero);
                this.Swapchain = IntPtr.Zero;
            }
            
            int Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceCapabilitiesKHR(WL.Render.GPU, Surface, out WL.Render.Native.VkSurfaceCapabilitiesKHR Caps);
            if(Result != 0){ throw new Exception("Не получилось узнать возможности поверхности через vkGetPhysicalDeviceSurfaceCapabilitiesKHR! Код: " + Result); }
            
            Extent = (Caps.currentExtent.width, Caps.currentExtent.height);
            
            uint FormatTotal = 0;
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceFormatsKHR(WL.Render.GPU, Surface, ref FormatTotal, IntPtr.Zero);
            if(Result != 0){ throw new Exception("Не получилось узнать кол-во форматов поверхности! Код: " + Result); }

            IntPtr Formats = WL.System.Native.MemoryEmpty<WL.Render.Native.VkSurfaceFormatKHR>((int)FormatTotal);
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfaceFormatsKHR(WL.Render.GPU, Surface, ref FormatTotal, Formats);
            if(Result != 0){ throw new Exception("Не получилось получить список форматов поверхности! Код: " + Result); }

            WL.Render.Native.VkSurfaceFormatKHR Format = Marshal.PtrToStructure<WL.Render.Native.VkSurfaceFormatKHR>(Formats);
            WL.System.Native.Free(Formats);
            
             ImageFormat = Format.format;

            uint PresentModeTotal = 0;
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfacePresentModesKHR(WL.Render.GPU, Surface, ref PresentModeTotal, IntPtr.Zero);
            if(Result != 0){ throw new Exception("Не получилось узнать кол-во \"Как показывается кадр\"! Код: " + Result); }

            IntPtr PresentModes = WL.System.Native.MemoryEmpty<int>((int)PresentModeTotal);
            Result = WL.Render.Native.vkGetPhysicalDeviceSurfacePresentModesKHR(WL.Render.GPU, Surface, ref PresentModeTotal, PresentModes);
            if(Result != 0){ throw new Exception("Не получилось получить список \"Как показывается кадр\"! Код: " + Result); }

            uint PresentMode = WL.Render.Native.VK_PRESENT_MODE_FIFO_KHR;

            for(int i = 0; i < PresentModeTotal; i++){
                uint Mode = (uint)Marshal.ReadInt32(PresentModes, i * sizeof(int));

                if(Mode == WL.Render.Native.VK_PRESENT_MODE_MAILBOX_KHR){
                    PresentMode = Mode; 
                }

                if(Mode == WL.Render.Native.VK_PRESENT_MODE_IMMEDIATE_KHR && PresentMode != WL.Render.Native.VK_PRESENT_MODE_MAILBOX_KHR){
                    PresentMode = Mode;
                }
            }
            
            uint ImageTotal = Caps.minImageCount + 1;
            if(Caps.maxImageCount > 0 && ImageTotal > Caps.maxImageCount){ ImageTotal = Caps.maxImageCount; }

            WL.Render.Native.VkSwapchainCreateInfoKHR SwapInfo = new WL.Render.Native.VkSwapchainCreateInfoKHR{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_SWAPCHAIN_CREATE_INFO_KHR,
                surface = Surface,
                
                minImageCount   = ImageTotal,
                imageFormat     = ImageFormat,
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
            
            WL.Render.Native.VkAttachmentDescription ColorAttachment = new Render.Native.VkAttachmentDescription{
                format         = ImageFormat,
                samples        = WL.Render.Native.VK_SAMPLE_COUNT_1_BIT,
                loadOp         = WL.Render.Native.VK_ATTACHMENT_LOAD_OP_CLEAR,
                storeOp        = WL.Render.Native.VK_ATTACHMENT_STORE_OP_STORE,
                stencilLoadOp  = WL.Render.Native.VK_ATTACHMENT_LOAD_OP_DONT_CARE,
                stencilStoreOp = WL.Render.Native.VK_ATTACHMENT_STORE_OP_DONT_CARE,
                initialLayout  = WL.Render.Native.VK_IMAGE_LAYOUT_UNDEFINED,
                finalLayout    = WL.Render.Native.VK_IMAGE_LAYOUT_PRESENT_SRC_KHR
            };

            WL.Render.Native.VkAttachmentReference ColorAttachmentReference = new Render.Native.VkAttachmentReference{
                attachment = 0,
                layout = WL.Render.Native.VK_IMAGE_LAYOUT_COLOR_ATTACHMENT_OPTIMAL
            };

            WL.Render.Native.VkSubpassDescription Subpass = new Render.Native.VkSubpassDescription{
                pipelineBindPoint = WL.Render.Native.VK_PIPELINE_BIND_POINT_GRAPHICS,
                colorAttachmentCount = 1,
                pColorAttachments = Marshal.UnsafeAddrOfPinnedArrayElement([ColorAttachmentReference], 0)
            };

            WL.Render.Native.VkRenderPassCreateInfo RenderPassInfo = new Render.Native.VkRenderPassCreateInfo{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_RENDER_PASS_CREATE_INFO,
                attachmentCount = 1,
                pAttachments = Marshal.UnsafeAddrOfPinnedArrayElement([ColorAttachment], 0),
                subpassCount = 1,
                pSubpasses = Marshal.UnsafeAddrOfPinnedArrayElement([Subpass], 0)
            };
            
            Result = WL.Render.Native.vkCreateRenderPass(WL.Render.Device, ref RenderPassInfo, IntPtr.Zero, out IntPtr RenderPass__);
            if(Result != 0){ throw new Exception("Произошла ошибка при создании RenderPass! Код: " + Result); }

            RenderPass = RenderPass__;
            
            ImageViews = new IntPtr[SwapchainImages.Length];
            
            Framebuffers = new IntPtr[SwapchainImages.Length];
            for(int i = 0; i < SwapchainImages.Length; i++){
                WL.Render.Native.VkImageViewCreateInfo ViewInfo = new Render.Native.VkImageViewCreateInfo{
                    sType = WL.Render.Native.VK_STRUCTURE_TYPE_IMAGE_VIEW_CREATE_INFO,
                    image = SwapchainImages[i],
                    viewType = WL.Render.Native.VK_IMAGE_VIEW_TYPE_2D,
                    format = ImageFormat,
                    components = new WL.Render.Native.VkComponentMapping(),
                    subresourceRange = new WL.Render.Native.VkImageSubresourceRange{
                        aspectMask = WL.Render.Native.VK_IMAGE_ASPECT_COLOR_BIT,
                        baseMipLevel = 0,
                        levelCount = 1,
                        baseArrayLayer = 0,
                        layerCount = 1
                    }
                };

                Result = WL.Render.Native.vkCreateImageView(WL.Render.Device, ref ViewInfo, IntPtr.Zero, out IntPtr ImageView__);
                if(Result != 0){ throw new Exception("Не получилось создать ImageView! Код: " + Result); }

                ImageViews[i] = ImageView__;

                WL.Render.Native.VkFramebufferCreateInfo FramebufferInfo = new Render.Native.VkFramebufferCreateInfo{
                    sType = WL.Render.Native.VK_STRUCTURE_TYPE_FRAMEBUFFER_CREATE_INFO,
                    renderPass = RenderPass__,
                    attachmentCount = 1,
                    pAttachments = Marshal.UnsafeAddrOfPinnedArrayElement([ImageView__], 0),
                    width = Extent.Item1,
                    height = Extent.Item2,
                    layers = 1
                };

                Result = WL.Render.Native.vkCreateFramebuffer(WL.Render.Device, ref FramebufferInfo, IntPtr.Zero, out IntPtr Framebuffer);
                if(Result != 0){ throw new Exception("Не получилось создать Framebuffer! Код: " + Result); }

                Framebuffers[i] = Framebuffer;
            }
        }catch(Exception e){
            throw new Exception("Произошла ошибка при создании/пересоздании Swapchain!", e);
        }
    }
    
    public RenderSurface RenderSurface  { get; private set; }
    public IntPtr        Surface        { get; private set; }
    public IntPtr        Swapchain      { get; private set; }
    public IntPtr[]      SwapchainImages{ get; private set; }
    public uint          ImageFormat    { get; private set; }
    public (uint, uint)  Extent         { get; private set; }
    public IntPtr        RenderPass     { get; private set; }
    public IntPtr[]?     ImageViews     { get; private set; }
    public IntPtr[]?     Framebuffers   { get; private set; }

    /// <summary>
    /// Контекст ещё живой?
    /// </summary>
    public bool Alive => RenderSurface.RenderHandle() != IntPtr.Zero;

    /// <summary>
    /// Рендерит
    /// <param name="BackgroundColor">Цвет заднего фона</param>
    /// <param name="Action">Действие на рендер</param>
    /// <returns>Удачно произошёл рендер?</returns>
    /// </summary>
    public bool Render(ColorF BackgroundColor, Action Action){
        int Result = 0;
        
        try{
            if(!Alive){ throw new Exception("Контекста не существует!"); }

            if(__Resized){
                __CreateSwapchain();
                __Resized = false;
            }
            
            Result = WL.Render.Native.vkAcquireNextImageKHR(WL.Render.Device, Swapchain, ulong.MaxValue, IntPtr.Zero, IntPtr.Zero, out uint ImageIndex);
            if(Result != 0){ throw new Exception("Не удалось получить индекс изображения Swapchain! Код: " + Result); }

            WL.Render.Native.VkClearValue BackgroundColor__ = new WL.Render.Native.VkClearValue{
                color = new WL.Render.Native.VkClearColorValue{
                    float32_0 = BackgroundColor.R,
                    float32_1 = BackgroundColor.G,
                    float32_2 = BackgroundColor.B,
                    float32_3 = BackgroundColor.A
                }
            };

            IntPtr __BackgroundColor__ = WL.System.Native.Memory<Render.Native.VkClearValue>(BackgroundColor__);
            
            Exception? e__ = null;
            WL.Render.UseCommandBuffer(() => {
                WL.Render.Native.VkRenderPassBeginInfo RP_Begin = new Render.Native.VkRenderPassBeginInfo{
                    sType = WL.Render.Native.VK_STRUCTURE_TYPE_RENDER_PASS_BEGIN_INFO,
                    renderPass = RenderPass,
                    framebuffer = Framebuffers![ImageIndex],
                    renderArea = new WL.Render.Native.VkRect2D{
                        offset = new WL.Render.Native.VkOffset2D { x = 0, y = 0 },
                        extent = new WL.Render.Native.VkExtent2D { width = Extent.Item1, height = Extent.Item2 }
                    },
                    clearValueCount = 1,
                    pClearValues = __BackgroundColor__
                };
            
                WL.Render.Native.vkCmdBeginRenderPass(WL.Render.CommandBuffer, ref RP_Begin, WL.Render.Native.VK_SUBPASS_CONTENTS_INLINE);
                
                try{ Action.Invoke(); }catch(Exception e){ e__ = e; }

                WL.Render.Native.vkCmdEndRenderPass(WL.Render.CommandBuffer);
            });
            
            WL.System.Native.Free(__BackgroundColor__);
            
            if(e__ != null){ throw e__; }
            
            Result = Show(ImageIndex);

            WL.Render.Native.vkQueueWaitIdle(WL.Render.GraphicQueue);
        }catch(Exception e){
            if(__ThatResizeError(Result)){
                __Resized = true;
                return false;
            }
            
            throw new Exception("Произошла ошибка в рисовании/рендере!", e);
        }

        return true;
    }
    private bool __Resized = false;

    /// <summary>
    /// Это ошибка VK_ERROR_OUT_OF_DATE_KHR или VK_SUBOPTIMAL_KHR?
    /// </summary>
    public bool __ThatResizeError(int Result){ return Result is WL.Render.Native.VK_ERROR_OUT_OF_DATE_KHR or WL.Render.Native.VK_SUBOPTIMAL_KHR; }
    
    /// <summary>
    /// Показывает нарисованное в контекст
    /// </summary>
    /// <param name="ImageIndex">Где нарисовано?</param>
    private int Show(uint ImageIndex){
        int Result = 0;
        
        try{
            WL.Render.Native.VkPresentInfoKHR PresentInfo = new Render.Native.VkPresentInfoKHR{
                sType = WL.Render.Native.VK_STRUCTURE_TYPE_PRESENT_INFO_KHR,
                swapchainCount = 1,
                pSwapchains = Marshal.UnsafeAddrOfPinnedArrayElement([Swapchain], 0),
                pImageIndices = Marshal.UnsafeAddrOfPinnedArrayElement([ImageIndex], 0),
                pResults = IntPtr.Zero
            };
            
            Result = WL.Render.Native.vkQueuePresentKHR(WL.Render.GraphicQueue, ref PresentInfo);
            if(Result != 0){ throw new Exception("Не получилось показать изображение! Код: " + Result); }
        }catch(Exception e){
            if(__ThatResizeError(Result)){ return Result; }

            throw new Exception("Произошла ошибка при показе нарисованного в Surface через [" + this + "]!\nImageIndex: " + ImageIndex, e);
        }

        return Result;
    }
    
    public void __Destroy(){
        try{
            if(Swapchain != IntPtr.Zero){
                if(Framebuffers != null){
                    foreach(IntPtr Framebuffer in Framebuffers){
                        WL.Render.Native.vkDestroyFramebuffer(WL.Render.Device, Framebuffer, IntPtr.Zero);
                    }
                    Framebuffers = null;
                }

                if(ImageViews != null){
                    foreach(IntPtr ImageView in ImageViews){
                        WL.Render.Native.vkDestroyImageView(WL.Render.Device, ImageView, IntPtr.Zero);
                    }
                    ImageViews = null;
                }
                
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