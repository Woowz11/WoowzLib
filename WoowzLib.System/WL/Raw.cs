using System.Runtime.InteropServices;

namespace WL;

public static partial class Native{
    public static partial class Raw{
        public static class Windows{
            private const string DLL_Kernel  = "kernel32.dll";
            private const string DLL_User    = "user32.dll";
            private const string DLL_NTDLL   = "ntdll.dll";
            private const string DLL_GDI     = "gdi32.dll";
            private const string DLL_DWMAPI  = "dwmapi.dll";
            private const string DLL_WS2     = "ws2_32.dll";
            private const string DLL_WinInet = "wininet.dll";
            private const string DLL_WinHTTP = "winhttp.dll";
            private const string DLL_ADVAPI  = "advapi32.dll";
            private const string DLL_Crypt   = "crypt32.dll";
            private const string DLL_OLE     = "ole32.dll";
            private const string DLL_OLEAUT  = "oleaut32.dll";
            private const string DLL_Comdlg  = "comdlg32.dll";
            private const string DLL_Shell   = "shell32.dll";
            private const string DLL_Winmm   = "winmm.dll";
            private const string DLL_MF      = "mf.dll";

            // ----------------------------------------------------------------------
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr GetCurrentProcess();
            
            [DllImport(DLL_Kernel)]
            public static extern uint GetCurrentProcessId();

            [DllImport(DLL_Kernel)]
            public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

            [DllImport(DLL_Kernel)]
            public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

            [DllImport(DLL_Kernel)]
            public static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

            [DllImport(DLL_Kernel)]
            public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr CreateThread(IntPtr lpThreadAttributes, UIntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

            [DllImport(DLL_Kernel)]
            public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

            [DllImport(DLL_Kernel)]
            public static extern bool CloseHandle(IntPtr hObject);
            
            [DllImport(DLL_Kernel)]
            public static extern void GetSystemTime(out SYSTEMTIME lpSystemTime);

            [DllImport(DLL_Kernel)]
            public static extern void GetLocalTime(out SYSTEMTIME lpSystemTime);
            
            [DllImport(DLL_Kernel)]
            public static extern uint GetLastError();

            [DllImport(DLL_Kernel)]
            public static extern void SetLastError(uint dwErrCode);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr LoadLibrary(string lpLibFileName);

            [DllImport(DLL_Kernel)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string? lpName);

            [DllImport(DLL_Kernel)]
            public static extern bool SetEvent(IntPtr hEvent);

            [DllImport(DLL_Kernel)]
            public static extern bool ResetEvent(IntPtr hEvent);
            
            [DllImport(DLL_Kernel)]
            public static extern uint GetTempPath(uint nBufferLength, global::System.Text.StringBuilder lpBuffer);

            [DllImport(DLL_Kernel)]
            public static extern uint GetModuleFileName(IntPtr hModule, global::System.Text.StringBuilder lpFilename, uint nSize);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr GetProcessHeap();

            [DllImport(DLL_Kernel)]
            public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

            [DllImport(DLL_Kernel)]
            public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr HeapReAlloc(IntPtr hHeap, uint dwFlags, IntPtr lpMem, UIntPtr dwBytes);
        
            // ----------------------------------------------------------------------
            
            public struct SYSTEMTIME{
                public ushort Year;
                public ushort Month;
                public ushort DayOfWeek;
                public ushort Day;
                public ushort Hour;
                public ushort Minute;
                public ushort Second;
                public ushort Milliseconds;
            }
            
            // ----------------------------------------------------------------------
            
            public const uint MEM_COMMIT             = 0x1000;
            public const uint MEM_RESERVE            = 0x2000;
            public const uint MEM_RELEASE            = 0x8000;
            public const uint PAGE_READWRITE         = 0x04;
            public const uint PAGE_EXECUTE_READWRITE = 0x40;
            public const uint WAIT_OBJECT_0          = 0x00000000;
            public const uint INFINITE               = 0xFFFFFFFF;
        }
    }
}