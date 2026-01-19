using System.Runtime.InteropServices;
using WLO;

namespace WL;

[WLModule(-50, 3)]
public class Render{
    static Render(){
        try{
            WL.WoowzLib.OnStop += () => {
                try{
                    if(DLL == IntPtr.Zero){ return; }

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

            Native.vkCreateDevice                           = System.Native.DelegateFunction<Native.D_vkCreateDevice                          >("vkCreateDevice"                          ,DLL);
            Native.vkDestroyDevice                          = System.Native.DelegateFunction<Native.D_vkDestroyDevice                         >("vkDestroyDevice"                         ,DLL);
            Native.vkGetDeviceQueue                         = System.Native.DelegateFunction<Native.D_vkGetDeviceQueue                        >("vkGetDeviceQueue"                        ,DLL);
            Native.vkCreateInstance                         = System.Native.DelegateFunction<Native.D_vkCreateInstance                        >("vkCreateInstance"                        ,DLL);
            Native.vkDestroyInstance                        = System.Native.DelegateFunction<Native.D_vkDestroyInstance                       >("vkDestroyInstance"                       ,DLL);
            Native.vkEnumeratePhysicalDevices               = System.Native.DelegateFunction<Native.D_vkEnumeratePhysicalDevices              >("vkEnumeratePhysicalDevices"              ,DLL);
            Native.vkGetPhysicalDeviceProperties            = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceProperties           >("vkGetPhysicalDeviceProperties"           ,DLL);
            Native.vkGetPhysicalDeviceQueueFamilyProperties = System.Native.DelegateFunction<Native.D_vkGetPhysicalDeviceQueueFamilyProperties>("vkGetPhysicalDeviceQueueFamilyProperties",DLL);

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
                    apiVersion         = Native.VK_MAKE_VERSION(1, 3, 204) // Какую версию Vulkan использовать?
                });
            
                try{
                    Native.VkInstanceCreateInfo CreateInfo = new Native.VkInstanceCreateInfo{
                        sType = Native.VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
                        pNext = IntPtr.Zero,
                    
                        pApplicationInfo        = ProjectInfo,
                        enabledLayerCount       = 0,
                        ppEnabledLayerNames     = IntPtr.Zero,
                        enabledExtensionCount   = 0,
                        ppEnabledExtensionNames = IntPtr.Zero
                    };

                    Result = Native.vkCreateInstance(ref CreateInfo, IntPtr.Zero, out IntPtr VK__);
                    if(Result != 0){ throw new Exception("Произошла ошибка при создании Vulkan через vkCreateInstance! Код: " + Result); }
                    
                    VK = VK__;
                    
                    if(Debug.LogMain){ Logger.Info("Создан Vulkan! VK: " + VK); }
                }finally{
                    System.Native.Free(Name       );
                    System.Native.Free(Engine     );
                    System.Native.Free(ProjectInfo);
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

                Native.VkDeviceCreateInfo DeviceCreateInfo = new Native.VkDeviceCreateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_DEVICE_CREATE_INFO,
                    queueCreateInfoCount = 1,
                    pQueueCreateInfos = WL.System.Native.Memory<Native.VkDeviceQueueCreateInfo>(QueueCreateInfo)
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
            public VkQueueFlags queueFlags;       // Какие операции поддерживаются
            public uint         queueCount;               // Количество очередей в этом семействе
            public uint         timestampValidBits;       // Количество бит для таймстемпов
            public VkExtent3D   minImageTransferGranularity; // Минимальная размерность при копировании изображений
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct VkExtent3D
        {
            public uint width;
            public uint height;
            public uint depth;
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
        
        public const uint VK_STRUCTURE_TYPE_APPLICATION_INFO         = 0;
        public const uint VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO     = 1;
        public const uint VK_STRUCTURE_TYPE_DEVICE_QUEUE_CREATE_INFO = 2;
        public const uint VK_STRUCTURE_TYPE_DEVICE_CREATE_INFO       = 3;
    }
}