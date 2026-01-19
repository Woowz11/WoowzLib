using System.Runtime.InteropServices;
using WLO;

namespace WL;

[WLModule(-50, 2)]
public class Render{
    static Render(){
        try{
            WL.WoowzLib.OnStop += () => {
                try{
                    if(DLL == IntPtr.Zero){ return; }

                    if(VK != IntPtr.Zero){
                        Native.vkDestroyInstance(VK, IntPtr.Zero);
                        VK = IntPtr.Zero;
                    }
                    
                    System.Native.Unload(DLL); DLL = IntPtr.Zero;
                }catch(Exception e){
                    throw new Exception("Произошла ошибка при остановке рендера!", e);
                }
            };
            
            DLL = System.Native.Load(WL.Explorer.Resources.Load("WoowzLib.vulkan-1.dll", typeof(WL.Render).Assembly).Path);

            Native.vkCreateInstance  = System.Native.DelegateFunction<Native.D_vkCreateInstance >("vkCreateInstance" , DLL);
            Native.vkDestroyInstance = System.Native.DelegateFunction<Native.D_vkDestroyInstance>("vkDestroyInstance", DLL);

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

                Native.VkInstanceCreateInfo CreateInfo = new Native.VkInstanceCreateInfo{
                    sType = Native.VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
                    pNext = IntPtr.Zero,
                    
                    pApplicationInfo        = ProjectInfo,
                    enabledLayerCount       = 0,
                    ppEnabledLayerNames     = IntPtr.Zero,
                    enabledExtensionCount   = 0,
                    ppEnabledExtensionNames = IntPtr.Zero
                };

                int Result = Native.vkCreateInstance(ref CreateInfo, IntPtr.Zero, out VK);
                
                System.Native.Free(Name       );
                System.Native.Free(Engine     );
                System.Native.Free(ProjectInfo);
                
                if(Result != 0){ throw new Exception("Произошла ошибка при создании Vulkan через vkCreateInstance! Код: " + Result); }

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
    private static IntPtr VK;
    
    public class Native{
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int D_vkCreateInstance(ref VkInstanceCreateInfo createInfo, IntPtr allocator, out IntPtr instance);
        public static D_vkCreateInstance vkCreateInstance = null!;
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void D_vkDestroyInstance(IntPtr instance, IntPtr allocator);
        public static D_vkDestroyInstance vkDestroyInstance = null!;
        
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
        
        public const uint VK_STRUCTURE_TYPE_APPLICATION_INFO     = 0;
        public const uint VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO = 1;
    }
}