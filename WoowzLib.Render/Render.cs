using System.Runtime.InteropServices;
using WL.WLO;
using WLO;

namespace WL;

[WLModule(-50, 6)]
public class Render{
    static Render(){
        try{
            WL.WoowzLib.OnStop += () => {
                try{
                    if(DLL == IntPtr.Zero){ return; }

                    if(CommandBuffer != IntPtr.Zero){
                        if(Debug.LogMain){ Logger.Info("Уничтожен CommandBuffer!"); }
                        
                        Native.vkFreeCommandBuffers(Device, CommandPool, 1, [CommandBuffer]);
                        CommandBuffer = IntPtr.Zero;
                    }

                    if(CommandPool != IntPtr.Zero){
                        if(Debug.LogMain){ Logger.Info("Уничтожен CommandPool!"); }
                        
                        Native.vkDestroyCommandPool(Device, CommandPool, IntPtr.Zero);
                        CommandPool = IntPtr.Zero;
                    }
                    
                    if(Device != IntPtr.Zero){
                        if(Debug.LogMain){ Logger.Info("Уничтожен Device!"); }
                        
                        Native.vkDestroyDevice(Device, IntPtr.Zero);
                        Device = IntPtr.Zero;
                    }
                    
                    if(VK != IntPtr.Zero){
                        if(Debug.LogMain){ Logger.Info("Уничтожен Vulkan!"); }
                        
                        Native.vkDestroyInstance(VK, IntPtr.Zero);
                        VK = IntPtr.Zero;
                    }
                    
                    if(Debug.LogMain){ Logger.Info("Разгружена Vulkan DLL!"); }
                    
                    System.Native.Unload(DLL); DLL = IntPtr.Zero;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при остановке рендера!", e);
                }
            };
            
            DLL = System.Native.Load(WL.Explorer.Resources.Load("WoowzLib.vulkan-1.dll", typeof(WL.Render).Assembly).Path);

            if(Debug.LogMain){ Logger.Info("Загружен Vulkan DLL!"); }

            
            
            Native.vkQueueSubmit                             = System.Native.DelegateFunction<Native.D_vkQueueSubmit                            >("vkQueueSubmit"                            ,DLL);
            Native.vkCreateDevice                            = System.Native.DelegateFunction<Native.D_vkCreateDevice                           >("vkCreateDevice"                           ,DLL);
            Native.vkQueueWaitIdle                           = System.Native.DelegateFunction<Native.D_vkQueueWaitIdle                          >("vkQueueWaitIdle"                          ,DLL);
            Native.vkDestroyDevice                           = System.Native.DelegateFunction<Native.D_vkDestroyDevice                          >("vkDestroyDevice"                          ,DLL);
            Native.vkGetDeviceQueue                          = System.Native.DelegateFunction<Native.D_vkGetDeviceQueue                         >("vkGetDeviceQueue"                         ,DLL);
            Native.vkCreateInstance                          = System.Native.DelegateFunction<Native.D_vkCreateInstance                         >("vkCreateInstance"                         ,DLL);
            Native.vkQueuePresentKHR                         = System.Native.DelegateFunction<Native.D_vkQueuePresentKHR                        >("vkQueuePresentKHR"                        ,DLL);
            Native.vkCreateImageView                         = System.Native.DelegateFunction<Native.D_vkCreateImageView                        >("vkCreateImageView"                        ,DLL);
            Native.vkDestroyInstance                         = System.Native.DelegateFunction<Native.D_vkDestroyInstance                        >("vkDestroyInstance"                        ,DLL);
            Native.vkEndCommandBuffer                        = System.Native.DelegateFunction<Native.D_vkEndCommandBuffer                       >("vkEndCommandBuffer"                       ,DLL);
            Native.vkCmdEndRenderPass                        = System.Native.DelegateFunction<Native.D_vkCmdEndRenderPass                       >("vkCmdEndRenderPass"                       ,DLL);
            Native.vkDestroyImageView                        = System.Native.DelegateFunction<Native.D_vkDestroyImageView                       >("vkDestroyImageView"                       ,DLL);
            Native.vkCreateRenderPass                        = System.Native.DelegateFunction<Native.D_vkCreateRenderPass                       >("vkCreateRenderPass"                       ,DLL);
            Native.vkCreateCommandPool                       = System.Native.DelegateFunction<Native.D_vkCreateCommandPool                      >("vkCreateCommandPool"                      ,DLL);
            Native.vkDestroySurfaceKHR                       = System.Native.DelegateFunction<Native.D_vkDestroySurfaceKHR                      >("vkDestroySurfaceKHR"                      ,DLL);
            Native.vkCreateFramebuffer                       = System.Native.DelegateFunction<Native.D_vkCreateFramebuffer                      >("vkCreateFramebuffer"                      ,DLL);
            Native.vkDestroyCommandPool                      = System.Native.DelegateFunction<Native.D_vkDestroyCommandPool                     >("vkDestroyCommandPool"                     ,DLL);
            Native.vkFreeCommandBuffers                      = System.Native.DelegateFunction<Native.D_vkFreeCommandBuffers                     >("vkFreeCommandBuffers"                     ,DLL);
            Native.vkBeginCommandBuffer                      = System.Native.DelegateFunction<Native.D_vkBeginCommandBuffer                     >("vkBeginCommandBuffer"                     ,DLL);
            Native.vkCmdBeginRenderPass                      = System.Native.DelegateFunction<Native.D_vkCmdBeginRenderPass                     >("vkCmdBeginRenderPass"                     ,DLL);
            Native.vkResetCommandBuffer                      = System.Native.DelegateFunction<Native.D_vkResetCommandBuffer                     >("vkResetCommandBuffer"                     ,DLL);
            Native.vkDestroyFramebuffer                      = System.Native.DelegateFunction<Native.D_vkDestroyFramebuffer                     >("vkDestroyFramebuffer"                     ,DLL);
            Native.vkCreateSwapchainKHR                      = System.Native.DelegateFunction<Native.D_vkCreateSwapchainKHR                     >("vkCreateSwapchainKHR"                     ,DLL);
            Native.vkDestroySwapchainKHR                     = System.Native.DelegateFunction<Native.D_vkDestroySwapchainKHR                    >("vkDestroySwapchainKHR"                    ,DLL);
            Native.vkAcquireNextImageKHR                     = System.Native.DelegateFunction<Native.D_vkAcquireNextImageKHR                    >("vkAcquireNextImageKHR"                    ,DLL);
            Native.vkCreateWin32SurfaceKHR                   = System.Native.DelegateFunction<Native.D_vkCreateWin32SurfaceKHR                  >("vkCreateWin32SurfaceKHR"                  ,DLL);
            Native.vkGetSwapchainImagesKHR                   = System.Native.DelegateFunction<Native.D_vkGetSwapchainImagesKHR                  >("vkGetSwapchainImagesKHR"                  ,DLL);
            Native.vkAllocateCommandBuffers                  = System.Native.DelegateFunction<Native.D_vkAllocateCommandBuffers                 >("vkAllocateCommandBuffers"                 ,DLL);            
            Native.vkEnumeratePhysicalDevices                = System.Native.DelegateFunction<Native.D_vkEnumeratePhysicalDevices               >("vkEnumeratePhysicalDevices"               ,DLL);
            Native.vkGetPhysicalDeviceProperties             = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceProperties            >("vkGetPhysicalDeviceProperties"            ,DLL);
            Native.vkGetPhysicalDeviceSurfaceFormatsKHR      = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceSurfaceFormatsKHR     >("vkGetPhysicalDeviceSurfaceFormatsKHR"     ,DLL);
            Native.vkGetPhysicalDeviceQueueFamilyProperties  = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceQueueFamilyProperties >("vkGetPhysicalDeviceQueueFamilyProperties" ,DLL);
            Native.vkGetPhysicalDeviceSurfaceCapabilitiesKHR = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceSurfaceCapabilitiesKHR>("vkGetPhysicalDeviceSurfaceCapabilitiesKHR",DLL);
            Native.vkGetPhysicalDeviceSurfacePresentModesKHR = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceSurfacePresentModesKHR>("vkGetPhysicalDeviceSurfacePresentModesKHR",DLL);
            
            if(Debug.LogMain){ Logger.Info("Загружены функции Vulkan!"); }

            int Result;
            
            #region Создание Vulkan

                IntPtr Name   = WL.System.Native.MemoryString(WL.WoowzLib.ProjectInfo.Name);
                IntPtr Engine = WL.System.Native.MemoryString(WL.WoowzLib.ProjectInfo.Engine);

                IntPtr ProjectInfo = WL.System.Native.Memory<Native.VkApplicationInfo>(new Native.VkApplicationInfo{
                    sType = Native.VK_STRUCTURE_TYPE_APPLICATION_INFO,
                    pNext = IntPtr.Zero,
                        
                    pApplicationName   = Name,
                    applicationVersion = WL.WoowzLib.ProjectInfo.Version,
                    pEngineName        = Engine,
                    engineVersion      = WL.WoowzLib.ProjectInfo.EngineVersion,
                    apiVersion         = Native.VK_MAKE_VERSION(1, 4, 329) // Какую версию Vulkan использовать?
                });

                string[] ExtensionsVK = [
                    "VK_KHR_surface",
                    "VK_KHR_win32_surface"
                ];
                
                IntPtr[] ExtensionsVK__ = new IntPtr[ExtensionsVK.Length];
                for(int i = 0; i < ExtensionsVK.Length; i++){
                    ExtensionsVK__[i] = WL.System.Native.MemoryString(ExtensionsVK[i]);
                }
                
                try{
                    Native.VkInstanceCreateInfo CreateInfo = new Native.VkInstanceCreateInfo{
                        sType = Native.VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
                        pNext = IntPtr.Zero,
                        flags = 0,
                        
                        enabledExtensionCount   = (uint)ExtensionsVK__.Length,
                        ppEnabledExtensionNames = Marshal.UnsafeAddrOfPinnedArrayElement(ExtensionsVK__, 0),
                        
                        enabledLayerCount       = 0,
                        ppEnabledLayerNames     = IntPtr.Zero,
                        
                        pApplicationInfo        = ProjectInfo
                    };

                    Result = Native.vkCreateInstance(ref CreateInfo, IntPtr.Zero, out IntPtr VK__);
                    if(Result != 0){ throw new Exception("Произошла ошибка при создании Vulkan через vkCreateInstance! Код: " + Result); }
                    
                    VK = VK__;
                    
                    if(Debug.LogMain){ Logger.Info("Создан Vulkan! VK: " + VK); }
                }finally{
                    System.Native.Free(Name       );
                    System.Native.Free(Engine     );
                    System.Native.Free(ProjectInfo);

                    foreach(IntPtr Extension in ExtensionsVK__){
                        WL.System.Native.Free(Extension);
                    }
                }

            #endregion

            #region Выбор GPU

                try{
                    uint TotalGPU__ = 0;
                    Result = Native.vkEnumeratePhysicalDevices(VK, ref TotalGPU__, IntPtr.Zero);
                    TotalGPU = TotalGPU__;

                    if(Result   != 0){ throw new Exception("Произошла ошибка при получении кол-во GPU! Код: " + Result); }
                    if(TotalGPU == 0){ throw new Exception("Не найдена ни одна GPU!"); }

                    IntPtr Array__ = WL.System.Native.MemoryEmpty(IntPtr.Size * (int)TotalGPU);

                    try{
                        Result = Native.vkEnumeratePhysicalDevices(VK, ref TotalGPU__, Array__);
                        if(Result != 0){ throw new Exception("Произошла ошибка при получении списка GPU! Код: " + Result); }

                        IntPtr[] GPU__ = new IntPtr[TotalGPU];
                        for(int i = 0; i < TotalGPU; i++){
                            GPU__[i] = Marshal.ReadIntPtr(Array__, i * IntPtr.Size);
                        }

                        GPU = GPU__[0];
                        
                        if(Debug.LogMain){ Logger.Info("Выбрана GPU! GPU: " + GPU); }
                    }finally{
                        WL.System.Native.Free(Array__);
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при выборе GPU!", e);
                }

            #endregion

            #region Информация о очередях GPU

                int GraphicQueueFamilyIndex = -1;
            
                try{
                    uint TotalQueueFamily = 0;
                    Native.vkGetPhysicalDeviceQueueFamilyProperties(GPU, ref TotalQueueFamily, IntPtr.Zero);

                    IntPtr QueueFamily = WL.System.Native.MemoryEmpty<Native.VkQueueFamilyProperties>((int)TotalQueueFamily);
                    try{
                        Native.vkGetPhysicalDeviceQueueFamilyProperties(GPU, ref TotalQueueFamily, QueueFamily);
                        
                        for(int i = 0; i < TotalQueueFamily; i++){
                            Native.VkQueueFamilyProperties QueueFamily__ = Marshal.PtrToStructure<Native.VkQueueFamilyProperties>(QueueFamily + i * Marshal.SizeOf<Native.VkQueueFamilyProperties>());

                            if((QueueFamily__.queueFlags & Native.VkQueueFlags.GRAPHICS) != 0){
                                GraphicQueueFamilyIndex = i;
                                break;
                            }
                        }

                        if(GraphicQueueFamilyIndex == -1){ throw new Exception("Не найден графический Queue Family!"); }
                    }finally{
                        WL.System.Native.Free(QueueFamily); 
                    }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при получении информации о очередях GPU [" + GPU + "]!", e);
                }

            #endregion

            #region Создание логического устройства

                Native.VkDeviceQueueCreateInfo QueueCreateInfo = new Native.VkDeviceQueueCreateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_DEVICE_QUEUE_CREATE_INFO,
                    queueFamilyIndex = (uint)GraphicQueueFamilyIndex,
                    queueCount = 1,
                    pQueuePriorities = WL.System.Native.MemoryEmpty(sizeof(float)),
                    pNext = IntPtr.Zero
                };

                Marshal.Copy(new float[]{ 1 }, 0, QueueCreateInfo.pQueuePriorities, 1);
                
                string[] ExtensionsDevice = [
                    "VK_KHR_swapchain"
                ];
                
                IntPtr[] ExtensionsDevice__ = new IntPtr[ExtensionsDevice.Length];
                for(int i = 0; i < ExtensionsDevice.Length; i++){
                    ExtensionsDevice__[i] = WL.System.Native.MemoryString(ExtensionsDevice[i]);
                }
                
                Native.VkDeviceCreateInfo DeviceCreateInfo = new Native.VkDeviceCreateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_DEVICE_CREATE_INFO,
                    queueCreateInfoCount = 1,
                    pQueueCreateInfos = WL.System.Native.Memory<Native.VkDeviceQueueCreateInfo>(QueueCreateInfo),
                    
                    enabledExtensionCount = (uint)ExtensionsDevice__.Length,
                    ppEnabledExtensionNames = Marshal.UnsafeAddrOfPinnedArrayElement(ExtensionsDevice__, 0)
                };
            
                try{
                    Result = Native.vkCreateDevice(GPU, ref DeviceCreateInfo, IntPtr.Zero, out IntPtr Device__);
                    if(Result != 0){ throw new Exception("Произошла ошибка при создании логического устройства через vkCreateDevice! Код: " + Result); }

                    Device = Device__;
                    
                    if(Debug.LogMain){ Logger.Info("Создан Device! Device: " + Device); }
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при создании логического устройства!", e);
                }
                finally{
                    WL.System.Native.Free(QueueCreateInfo .pQueuePriorities );
                    WL.System.Native.Free(DeviceCreateInfo.pQueueCreateInfos);
                }

            #endregion

            #region Получение графической очереди

                Native.vkGetDeviceQueue(Device, (uint)GraphicQueueFamilyIndex, 0, out IntPtr GraphicQueue__);
                GraphicQueue = GraphicQueue__;
                
                if(Debug.LogMain){ Logger.Info("Графическая очередь: " + GraphicQueue); }

            #endregion

            #region Создание Command Pool

                Native.VkCommandPoolCreateInfo CreatePoolInfo = new Native.VkCommandPoolCreateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_COMMAND_POOL_CREATE_INFO,
                    queueFamilyIndex = (uint)GraphicQueueFamilyIndex,
                    flags = 0
                };

                Result = Native.vkCreateCommandPool(Device, ref CreatePoolInfo, IntPtr.Zero, out IntPtr CommandPool__);
                if(Result != 0){ throw new Exception("Произошла ошибка при создании Command Pool! Код: " + Result); }

                CommandPool = CommandPool__;
                
                if(Debug.LogMain){ Logger.Info("Создан CommandPool! CommandPool: " + CommandPool); }

            #endregion

            #region Создание Command Buffers

                Native.VkCommandBufferAllocateInfo AllocateInfo = new Native.VkCommandBufferAllocateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_COMMAND_BUFFER_ALLOCATE_INFO,
                    commandPool = CommandPool,
                    level = Native.VK_COMMAND_BUFFER_LEVEL_PRIMARY,
                    commandBufferCount = 1
                };

                Result = Native.vkAllocateCommandBuffers(Device, ref AllocateInfo, out IntPtr CommandBuffer__);
                if(Result != 0){ throw new Exception("Произошла ошибка при создании Command Buffer! Код: " + Result); }

                CommandBuffer = CommandBuffer__;
                
                if(Debug.LogMain){ Logger.Info("Создан CommandBuffer! CommandBuffer: " + CommandBuffer); }

            #endregion
        }catch(Exception e){
            throw new Exception("Произошла ошибка при инициализации рендера!", e);
        }
    }
    
    /// <summary>
    /// Ссылка на vulkan-1.dll
    /// </summary>
    private static IntPtr DLL;

    /// <summary>
    /// Сам Vulkan
    /// </summary>
    public static IntPtr VK{ get; private set; }

    /// <summary>
    /// Сколько видеокарт в компьютере?
    /// </summary>
    public static uint TotalGPU{ get; private set; }

    /// <summary>
    /// Используемая видеокарта
    /// </summary>
    public static IntPtr GPU{ get; private set; }

    /// <summary>
    /// Логическое устройство (взаимодействует с выбранной видеокартой)
    /// </summary>
    public static IntPtr Device{ get; private set; }

    /// <summary>
    /// Графическая очередь
    /// </summary>
    public static IntPtr GraphicQueue{ get; private set; }

    /// <summary>
    /// Сборник буферов команд рендера
    /// </summary>
    public static IntPtr CommandPool{ get; private set; }

    /// <summary>
    /// Буфер команд рендера
    /// </summary>
    public static IntPtr CommandBuffer{ get; private set; }

    /// <summary>
    /// Соединяет рендер с RenderSurface
    /// </summary>
    public static RenderContext Connect(RenderSurface RS){
        try{
            RenderContext RC = new RenderContext(
                RS
            );
            
            RS.RenderDestroy += () => {
                if(Debug.LogMain){ Logger.Info("Отсоединено [" + RS + "] от рендера!"); }

                RC.__Destroy();
            };

            if(Debug.LogMain){ Logger.Info("Присоединено [" + RS + "] к рендеру!"); }

            return RC;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при соединении рендера с [" + RS + "]!", e);
        }
    }
    
    public class Debug{
        public static bool LogMain;
    }
    
    public class Native{
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateInstance(ref VkInstanceCreateInfo createInfo, IntPtr allocator, out IntPtr instance);
        public static D_vkCreateInstance vkCreateInstance = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyInstance(IntPtr instance, IntPtr allocator);
        public static D_vkDestroyInstance vkDestroyInstance = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkEnumeratePhysicalDevices(IntPtr instance, ref uint deviceCount, IntPtr pPhysicalDevices);
        public static D_vkEnumeratePhysicalDevices vkEnumeratePhysicalDevices = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkGetPhysicalDeviceProperties(IntPtr physicalDevice, out VkPhysicalDeviceProperties properties);
        public static D_vkGetPhysicalDeviceProperties vkGetPhysicalDeviceProperties = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateDevice(IntPtr physicalDevice, ref VkDeviceCreateInfo createInfo, IntPtr allocator, out IntPtr device);
        public static D_vkCreateDevice vkCreateDevice = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyDevice(IntPtr device, IntPtr allocator);
        public static D_vkDestroyDevice vkDestroyDevice = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkGetDeviceQueue(IntPtr device, uint queueFamilyIndex, uint queueIndex, out IntPtr queue);
        public static D_vkGetDeviceQueue vkGetDeviceQueue = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkGetPhysicalDeviceQueueFamilyProperties(
            IntPtr physicalDevice,
            ref uint pQueueFamilyPropertyCount,
            IntPtr pQueueFamilyProperties
        );
        public static D_vkGetPhysicalDeviceQueueFamilyProperties vkGetPhysicalDeviceQueueFamilyProperties = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateCommandPool(
            IntPtr device,
            ref VkCommandPoolCreateInfo pCreateInfo,
            IntPtr pAllocator,
            out IntPtr pCommandPool
        );
        public static D_vkCreateCommandPool vkCreateCommandPool = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyCommandPool(
            IntPtr device,
            IntPtr commandPool,
            IntPtr pAllocator
        );
        public static D_vkDestroyCommandPool vkDestroyCommandPool = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkAllocateCommandBuffers(
            IntPtr device,
            ref VkCommandBufferAllocateInfo pAllocateInfo,
            out IntPtr pCommandBuffers
        );
        public static D_vkAllocateCommandBuffers vkAllocateCommandBuffers = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkFreeCommandBuffers(
            IntPtr device,
            IntPtr commandPool,
            uint commandBufferCount,
            IntPtr[] pCommandBuffers
        );
        public static D_vkFreeCommandBuffers vkFreeCommandBuffers = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkBeginCommandBuffer(
            IntPtr commandBuffer,
            ref VkCommandBufferBeginInfo pBeginInfo
        );
        public static D_vkBeginCommandBuffer vkBeginCommandBuffer = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkEndCommandBuffer(
            IntPtr commandBuffer
        );
        public static D_vkEndCommandBuffer vkEndCommandBuffer = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkResetCommandBuffer(
            IntPtr commandBuffer,
            uint flags
        );
        public static D_vkResetCommandBuffer vkResetCommandBuffer = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateWin32SurfaceKHR(
            IntPtr instance,
            ref VkWin32SurfaceCreateInfoKHR pCreateInfo,
            IntPtr pAllocator,
            out IntPtr pSurface
        );
        public static D_vkCreateWin32SurfaceKHR vkCreateWin32SurfaceKHR = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroySurfaceKHR(
            IntPtr instance,
            IntPtr surface,
            IntPtr pAllocator
        );
        public static D_vkDestroySurfaceKHR vkDestroySurfaceKHR = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroySwapchainKHR(
            IntPtr device,
            IntPtr swapchain,
            IntPtr pAllocator
        );
        public static D_vkDestroySwapchainKHR vkDestroySwapchainKHR = null!;

        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkGetPhysicalDeviceSurfaceCapabilitiesKHR(
            IntPtr physicalDevice,
            IntPtr surface,
            out VkSurfaceCapabilitiesKHR capabilities
        );
        public static D_vkGetPhysicalDeviceSurfaceCapabilitiesKHR vkGetPhysicalDeviceSurfaceCapabilitiesKHR = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyFramebuffer(IntPtr device, IntPtr framebuffer, IntPtr pAllocator);
        public static D_vkDestroyFramebuffer vkDestroyFramebuffer = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyImageView(IntPtr device, IntPtr imageView, IntPtr pAllocator);
        public static D_vkDestroyImageView vkDestroyImageView = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkGetPhysicalDeviceSurfaceFormatsKHR(
            IntPtr physicalDevice,
            IntPtr surface,
            ref uint surfaceFormatCount,
            IntPtr surfaceFormats
        );
        public static D_vkGetPhysicalDeviceSurfaceFormatsKHR vkGetPhysicalDeviceSurfaceFormatsKHR = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkGetPhysicalDeviceSurfacePresentModesKHR(
            IntPtr physicalDevice,
            IntPtr surface,
            ref uint presentModeCount,
            IntPtr presentModes
        );
        public static D_vkGetPhysicalDeviceSurfacePresentModesKHR vkGetPhysicalDeviceSurfacePresentModesKHR = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateSwapchainKHR(
            IntPtr device,
            ref VkSwapchainCreateInfoKHR createInfo,
            IntPtr allocator,
            out IntPtr swapchain
        );
        public static D_vkCreateSwapchainKHR vkCreateSwapchainKHR = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkGetSwapchainImagesKHR(
            IntPtr device,
            IntPtr swapchain,
            ref uint swapchainImageCount,
            IntPtr swapchainImages
        );
        public static D_vkGetSwapchainImagesKHR vkGetSwapchainImagesKHR = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateImageView(
            IntPtr device,
            ref VkImageViewCreateInfo createInfo,
            IntPtr allocator,
            out IntPtr imageView
        );
        public static D_vkCreateImageView vkCreateImageView = null!;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateRenderPass(
            IntPtr device,
            ref VkRenderPassCreateInfo createInfo,
            IntPtr allocator,
            out IntPtr renderPass
        );
        public static D_vkCreateRenderPass vkCreateRenderPass = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateFramebuffer(
            IntPtr device,
            ref VkFramebufferCreateInfo createInfo,
            IntPtr allocator,
            out IntPtr framebuffer
        );
        public static D_vkCreateFramebuffer vkCreateFramebuffer = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkQueueSubmit(IntPtr queue, uint submitCount, ref VkSubmitInfo pSubmits, IntPtr fence);
        public static D_vkQueueSubmit vkQueueSubmit = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkQueueWaitIdle(IntPtr queue);
        public static D_vkQueueWaitIdle vkQueueWaitIdle = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkCmdBeginRenderPass(IntPtr commandBuffer, ref VkRenderPassBeginInfo pRenderPassBegin, uint contents);
        public static D_vkCmdBeginRenderPass vkCmdBeginRenderPass = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkCmdEndRenderPass(IntPtr commandBuffer);
        public static D_vkCmdEndRenderPass vkCmdEndRenderPass = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkQueuePresentKHR(IntPtr queue, ref VkPresentInfoKHR pPresentInfo);
        public static D_vkQueuePresentKHR vkQueuePresentKHR = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkAcquireNextImageKHR(
            IntPtr device,
            IntPtr swapchain,
            ulong timeout,
            IntPtr semaphore,
            IntPtr fence,
            out uint pImageIndex
        );
        public static D_vkAcquireNextImageKHR vkAcquireNextImageKHR = null!;
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkSubmitInfo
        {
            public uint   sType;
            public IntPtr pNext;

            public uint   waitSemaphoreCount;
            public IntPtr pWaitSemaphores;
            public IntPtr pWaitDstStageMask;

            public uint   commandBufferCount;
            public IntPtr pCommandBuffers;

            public uint   signalSemaphoreCount;
            public IntPtr pSignalSemaphores;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkPresentInfoKHR
        {
            public uint   sType;
            public IntPtr pNext;

            public uint   waitSemaphoreCount;
            public IntPtr pWaitSemaphores; 
        
            public uint   swapchainCount;
            public IntPtr pSwapchains;     
            public IntPtr pImageIndices;   
            public IntPtr pResults;        
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkAttachmentDescription
        {
            public uint flags;
            public uint format; 
            public uint samples;
            public uint loadOp; 
            public uint storeOp;
            public uint stencilLoadOp; 
            public uint stencilStoreOp;
            public uint initialLayout;
            public uint finalLayout;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkAttachmentReference
        {
            public uint attachment; 
            public uint layout;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkSubpassDescription
        {
            public uint   flags;
            public uint   pipelineBindPoint;
            public uint   inputAttachmentCount;
            public IntPtr pInputAttachments;
            public uint   colorAttachmentCount;
            public IntPtr pColorAttachments;
            public IntPtr pResolveAttachments;
            public IntPtr pDepthStencilAttachment;
            public uint   preserveAttachmentCount;
            public IntPtr pPreserveAttachments;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkRenderPassCreateInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public uint   attachmentCount;
            public IntPtr pAttachments;
            public uint   subpassCount;
            public IntPtr pSubpasses;
            public uint   dependencyCount;
            public IntPtr pDependencies;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkImageViewCreateInfo
        {
            public uint                    sType;
            public IntPtr                  pNext;
            public uint                    flags;
            public IntPtr                  image;
            public uint                    viewType;
            public uint                    format;
            public VkComponentMapping      components;
            public VkImageSubresourceRange subresourceRange;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkFramebufferCreateInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public IntPtr renderPass;
            public uint   attachmentCount;
            public IntPtr pAttachments;
            public uint   width;
            public uint   height;
            public uint   layers;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkComponentMapping
        {
            public uint r, g, b, a;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkImageSubresourceRange
        {
            public uint aspectMask;
            public uint baseMipLevel;
            public uint levelCount;
            public uint baseArrayLayer;
            public uint layerCount;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkSurfaceFormatKHR{
            public uint format;
            public uint colorSpace;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkSurfaceCapabilitiesKHR{
            public uint       minImageCount;
            public uint       maxImageCount;
            public VkExtent2D currentExtent;
            public VkExtent2D minImageExtent;
            public VkExtent2D maxImageExtent;
            public uint       maxImageArrayLayers;
            public uint       supportedTransforms;
            public uint       currentTransform;
            public uint       supportedCompositeAlpha;
            public uint       supportedUsageFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkExtent2D{
            public uint width;
            public uint height;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkSwapchainCreateInfoKHR{
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;

            public IntPtr surface;

            public uint       minImageCount;
            public uint       imageFormat;
            public uint       imageColorSpace;
            public VkExtent2D imageExtent;

            public uint imageArrayLayers;
            public uint imageUsage;

            public uint   imageSharingMode;
            public uint   queueFamilyIndexCount;
            public IntPtr pQueueFamilyIndices;

            public uint preTransform;
            public uint compositeAlpha;
            public uint presentMode;
            public uint clipped;

            public IntPtr oldSwapchain;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkInstanceCreateInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public IntPtr pApplicationInfo;
            public uint   enabledLayerCount;
            public IntPtr ppEnabledLayerNames;
            public uint   enabledExtensionCount;
            public IntPtr ppEnabledExtensionNames;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkApplicationInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public IntPtr pApplicationName;
            public uint   applicationVersion;
            public IntPtr pEngineName;
            public uint   engineVersion;
            public uint   apiVersion;
        }
        
        public static uint VK_MAKE_VERSION(uint major, uint minor, uint patch){
            return (major << 22) | (minor << 12) | patch;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkPhysicalDeviceProperties
        {
            public uint apiVersion;
            public uint driverVersion;
            public uint vendorID;
            public uint deviceID;
            public uint deviceType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] deviceName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public uint[] pipelineCacheUUID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkDeviceQueueCreateInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public uint   queueFamilyIndex;
            public uint   queueCount;
            public IntPtr pQueuePriorities;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkDeviceCreateInfo
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public uint   queueCreateInfoCount;
            public IntPtr pQueueCreateInfos;
            public uint   enabledLayerCount;
            public IntPtr ppEnabledLayerNames;
            public uint   enabledExtensionCount;
            public IntPtr ppEnabledExtensionNames;
            public IntPtr pEnabledFeatures;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkQueueFamilyProperties
        {
            public VkQueueFlags queueFlags;
            public uint         queueCount;
            public uint         timestampValidBits;
            public VkExtent3D   minImageTransferGranularity;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkExtent3D
        {
            public uint width;
            public uint height;
            public uint depth;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkCommandPoolCreateInfo {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public uint   queueFamilyIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkCommandBufferAllocateInfo {
            public uint   sType;
            public IntPtr pNext;
            public IntPtr commandPool;
            public uint   level;
            public uint   commandBufferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkCommandBufferBeginInfo {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public IntPtr pInheritanceInfo;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkWin32SurfaceCreateInfoKHR
        {
            public uint   sType;
            public IntPtr pNext;
            public uint   flags;
            public IntPtr hinstance;
            public IntPtr hwnd;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkClearColorValue
        {
            public float float32_0;
            public float float32_1;
            public float float32_2;
            public float float32_3;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct VkClearValue
        {
            [FieldOffset(0)]
            public VkClearColorValue color;

            [FieldOffset(0)]
            public VkClearDepthStencilValue depthStencil;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkClearDepthStencilValue
        {
            public float depth;
            public uint  stencil;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct VkRenderPassBeginInfo
        {
            public uint     sType;
            public IntPtr   pNext;
            public IntPtr   renderPass;
            public IntPtr   framebuffer;
            public VkRect2D renderArea;
            public uint     clearValueCount;
            public IntPtr   pClearValues;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkRect2D
        {
            public VkOffset2D offset;
            public VkExtent2D extent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkOffset2D
        {
            public int x;
            public int y;
        }
        
        [global::System.Flags]
        public enum VkQueueFlags : uint
        {
            GRAPHICS       = 0x00000001,
            COMPUTE        = 0x00000002,
            TRANSFER       = 0x00000004,
            SPARSE_BINDING = 0x00000008,
            PROTECTED      = 0x00000010
        }
        
        public const uint VK_STRUCTURE_TYPE_APPLICATION_INFO               = 0;
        public const uint VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO           = 1;
        public const uint VK_STRUCTURE_TYPE_DEVICE_QUEUE_CREATE_INFO       = 2;
        public const uint VK_STRUCTURE_TYPE_DEVICE_CREATE_INFO             = 3;
        public const uint VK_STRUCTURE_TYPE_COMMAND_POOL_CREATE_INFO       = 4;
        public const uint VK_STRUCTURE_TYPE_COMMAND_BUFFER_ALLOCATE_INFO   = 5;
        public const uint VK_STRUCTURE_TYPE_COMMAND_BUFFER_BEGIN_INFO      = 6;
        public const uint VK_COMMAND_BUFFER_LEVEL_PRIMARY                  = 0;
        public const uint VK_COMMAND_BUFFER_LEVEL_SECONDARY                = 1;
        public const uint VK_COMMAND_POOL_CREATE_TRANSIENT_BIT             = 0x00000001;
        public const uint VK_COMMAND_POOL_CREATE_RESET_COMMAND_BUFFER_BIT  = 0x00000002;
        public const uint VK_COMMAND_BUFFER_USAGE_ONE_TIME_SUBMIT_BIT      = 0x00000001;
        public const uint VK_COMMAND_BUFFER_USAGE_RENDER_PASS_CONTINUE_BIT = 0x00000002;
        public const uint VK_COMMAND_BUFFER_USAGE_SIMULTANEOUS_USE_BIT     = 0x00000004;
        public const uint VK_STRUCTURE_TYPE_WIN32_SURFACE_CREATE_INFO_KHR  = 1000009000;
        public const uint VK_PRESENT_MODE_FIFO_KHR                         = 2;
        public const uint VK_STRUCTURE_TYPE_SWAPCHAIN_CREATE_INFO_KHR      = 1000001000;
        public const uint VK_IMAGE_USAGE_COLOR_ATTACHMENT_BIT              = 0x00000010;
        public const uint VK_SHARING_MODE_EXCLUSIVE                        = 0;
        public const uint VK_COMPOSITE_ALPHA_OPAQUE_BIT_KHR                = 0x00000001;
        public const uint VK_STRUCTURE_TYPE_RENDER_PASS_CREATE_INFO        = 38;
        public const uint VK_STRUCTURE_TYPE_FRAMEBUFFER_CREATE_INFO        = 42;
        public const uint VK_IMAGE_LAYOUT_COLOR_ATTACHMENT_OPTIMAL         = 2;
        public const uint VK_IMAGE_LAYOUT_PRESENT_SRC_KHR                  = 1000001002;
        public const uint VK_PIPELINE_BIND_POINT_GRAPHICS                  = 0;
        public const uint VK_ATTACHMENT_LOAD_OP_CLEAR                      = 1;
        public const uint VK_ATTACHMENT_STORE_OP_STORE                     = 0;
        public const uint VK_SAMPLE_COUNT_1_BIT                            = 0x00000001;
        public const uint VK_STRUCTURE_TYPE_SUBMIT_INFO                    = 4;
        public const uint VK_ATTACHMENT_LOAD_OP_LOAD                       = 0;
        public const uint VK_ATTACHMENT_LOAD_OP_DONT_CARE                  = 2;
        public const uint VK_ATTACHMENT_STORE_OP_DONT_CARE                 = 1;
        public const uint VK_IMAGE_LAYOUT_UNDEFINED                        = 0;
        public const uint VK_IMAGE_LAYOUT_GENERAL                          = 1;
        public const uint VK_SUBPASS_CONTENTS_INLINE                       = 0;
        public const uint VK_IMAGE_ASPECT_COLOR_BIT                        = 0x00000001;
        public const uint VK_STRUCTURE_TYPE_IMAGE_VIEW_CREATE_INFO         = 15;
        public const uint VK_IMAGE_VIEW_TYPE_1D                            = 0;
        public const uint VK_IMAGE_VIEW_TYPE_2D                            = 1;
        public const uint VK_IMAGE_VIEW_TYPE_3D                            = 2;
        public const uint VK_IMAGE_VIEW_TYPE_CUBE                          = 3;
        public const uint VK_IMAGE_VIEW_TYPE_1D_ARRAY                      = 4;
        public const uint VK_IMAGE_VIEW_TYPE_2D_ARRAY                      = 5;
        public const uint VK_IMAGE_VIEW_TYPE_CUBE_ARRAY                    = 6;
        public const uint VK_STRUCTURE_TYPE_RENDER_PASS_BEGIN_INFO         = 38;
        public const uint VK_STRUCTURE_TYPE_PRESENT_INFO_KHR               = 1000001000;
        public const uint VK_STRUCTURE_TYPE_ACQUIRE_NEXT_IMAGE_INFO_KHR    = 1000001001;
        public const int  VK_SUCCESS                                       = 0;
        public const int  VK_NOT_READY                                     = 1;
        public const int  VK_TIMEOUT                                       = 2;
        public const int  VK_EVENT_SET                                     = 3;
        public const int  VK_EVENT_RESET                                   = 4;
        public const int  VK_INCOMPLETE                                    = 5;
        public const int  VK_ERROR_OUT_OF_HOST_MEMORY                      = -1;
        public const int  VK_ERROR_OUT_OF_DEVICE_MEMORY                    = -2;
        public const int  VK_ERROR_INITIALIZATION_FAILED                   = -3;
        public const int  VK_ERROR_DEVICE_LOST                             = -4;
        public const int  VK_ERROR_MEMORY_MAP_FAILED                       = -5;
        public const int  VK_ERROR_LAYER_NOT_PRESENT                       = -6;
        public const int  VK_ERROR_EXTENSION_NOT_PRESENT                   = -7;
        public const int  VK_ERROR_FEATURE_NOT_PRESENT                     = -8;
        public const int  VK_ERROR_INCOMPATIBLE_DRIVER                     = -9;
        public const int  VK_ERROR_TOO_MANY_OBJECTS                        = -10;
        public const int  VK_ERROR_FORMAT_NOT_SUPPORTED                    = -11;
        public const int  VK_ERROR_FRAGMENTED_POOL                         = -12;
        public const int  VK_ERROR_OUT_OF_DATE_KHR                         = -1000001004;
        public const int  VK_SUBOPTIMAL_KHR                                = 1000001003;
    }
}