using System.Runtime.InteropServices;
using System.Text;
using WLO.Rect;
using WLO.Vector;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr CreateThread(IntPtr lpThreadAttributes, UIntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

            [DllImport(DLL_Kernel)]
            public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

            [DllImport(DLL_Kernel, SetLastError = true)]
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

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string? lpName);

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool SetEvent(IntPtr hEvent);

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool ResetEvent(IntPtr hEvent);
            
            [DllImport(DLL_Kernel)]
            public static extern uint GetTempPath(uint nBufferLength, global::System.Text.StringBuilder lpBuffer);

            [DllImport(DLL_Kernel)]
            public static extern uint GetModuleFileName(IntPtr hModule, global::System.Text.StringBuilder lpFilename, uint nSize);
            
            [DllImport(DLL_Kernel)]
            public static extern IntPtr GetProcessHeap();

            [DllImport(DLL_Kernel)]
            public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

            [DllImport(DLL_Kernel, SetLastError = true)]
            public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

            [DllImport(DLL_Kernel)]
            public static extern IntPtr HeapReAlloc(IntPtr hHeap, uint dwFlags, IntPtr lpMem, UIntPtr dwBytes);
        
            [DllImport(DLL_Kernel, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string? lpModuleName);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr CreateWindowExW(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool SetWindowTextW(IntPtr hWnd, string lpString);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool UpdateWindow(IntPtr hWnd);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool TranslateMessage(ref MSG lpMsg);

            [DllImport(DLL_User)]
            public static extern IntPtr DispatchMessage(ref MSG lpMsg);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern ushort RegisterClassEx(ref WNDCLASSEX lpWndClass);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode)]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
            
            [DllImport(DLL_User)]
            public static extern void PostQuitMessage(int nExitCode);
            
            [DllImport(DLL_User)]
            public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool FillRect(IntPtr hDC, ref RECT lprc, IntPtr hbr);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr CreateSolidBrush(int color);

            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool DeleteObject(IntPtr hObject);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool DestroyWindow(IntPtr hWnd);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool AdjustWindowRectEx(ref RECT lpRect, uint dwStyle, bool bMenu, uint dwExStyle);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
            
            [DllImport(DLL_User, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool GetClassInfoEx(IntPtr hInstance, string lpClassName, ref WNDCLASSEX lpWndClass);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool GetClassInfo(IntPtr hInstance, string lpClassName, out WNDCLASS lpWndClass);
            
            [DllImport(DLL_User, CharSet = CharSet.Unicode)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
            
            [DllImport(DLL_User)]
            public static extern bool IsWindow(IntPtr hWnd);
            
            [DllImport(DLL_User)]
            public static extern IntPtr SetCursor(IntPtr hCursor);

            [DllImport(DLL_User, CharSet = CharSet.Auto)]
            public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorName);

            [DllImport(DLL_User)]
            public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
            
            [DllImport(DLL_User, SetLastError = true)]
            public static extern bool GetLayeredWindowAttributes(IntPtr hWnd, out uint pcrKey, out byte pbAlpha, out uint pdwFlags);
            
            [DllImport(DLL_User)]
            public static extern IntPtr GetDC(IntPtr hWnd);

            [DllImport(DLL_User)]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport(DLL_User)]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern int SetPixel(IntPtr hdc, int x, int y, uint color);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool Polygon(IntPtr hdc, POINT[] points, int count);

            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool Polyline(IntPtr hdc, POINT[] points, int count);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool MoveToEx(IntPtr hdc, int x, int y, out POINT lpPoint);
            
            [DllImport(DLL_GDI, SetLastError = true)]
            public static extern bool LineTo(IntPtr hdc, int x, int y);
            
            [DllImport(DLL_GDI, CharSet = CharSet.Unicode)]
            public static extern int TextOut(IntPtr hdc, int x, int y, string lpString, int c);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr CreateSolidBrush(uint crColor);
            
            [DllImport(DLL_GDI, CharSet = CharSet.Unicode)]
            public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);
            
            [DllImport(DLL_GDI)]
            public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
            
            [DllImport(DLL_GDI)]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);
            
            [DllImport(DLL_GDI)]
            public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, uint rop);
            
            [DllImport(DLL_GDI)]
            public static extern bool DeleteDC(IntPtr hdc);
            
            [DllImport(DLL_Kernel)]
            public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

            [DllImport(DLL_Kernel)]
            public static extern bool QueryPerformanceFrequency(out long lpFrequency);
            
            // ----------------------------------------------------------------------
            
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr WndProcDelegate(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
            
            // ----------------------------------------------------------------------

            public static IntPtr DefaultWndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam) => Windows.DefWindowProc(hwnd, msg, wParam, lParam);
            
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
            
            public struct MSG{
                public IntPtr hwnd;
                public uint   message;
                public IntPtr wParam;
                public IntPtr lParam;
                public uint   time;
                public POINT  pt;
            }
            
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT{
                public POINT(int X, int Y){ this.X = X; this.Y = Y; }
                public POINT(Vector2I V) : this(V.X, V.Y){}
                
                public int X;
                public int Y;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WNDCLASSEX{
                public WNDCLASSEX(){
                    cbSize = WL.System.Memory.StructSize<Native.Raw.Windows.WNDCLASSEX>();
                }
                
                public WNDCLASSEX(string Name) : this(){
                    lpszClassName = Name;
                }
                
                public uint cbSize;
                public uint style;
                public IntPtr lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpszMenuName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpszClassName;
                public IntPtr hIconSm;
            }
            
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WNDCLASS{
                public uint   style;
                public IntPtr lpfnWndProc;
                public int    cbClsExtra;
                public int    cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                public IntPtr lpszMenuName;
                public IntPtr lpszClassName;
            }
            
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT{
                public RECT(int left, int top, int right, int bottom){ this.left = left; this.top = top; this.right = right; this.bottom = bottom; }
                public RECT(Rect2I Rect){ left = Rect.Left; top = Rect.Bottom; right = Rect.Right; bottom = Rect.Top; }
                
                public int left;
                public int top;
                public int right;
                public int bottom;

                public int width  => right - left;
                public int height => bottom - top;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PAINTSTRUCT{
                public IntPtr hdc;
                public bool   fErase;
                public RECT   rcPaint;
                public bool   fRestore;
                public bool   fIncUpdate;
    
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                public byte[] rgbReserved;
            }
            
            [StructLayout(LayoutKind.Sequential)]
            public struct WINDOWPOS{
                public IntPtr hwnd;
                public IntPtr hwndInsertAfter;
                public int    x;
                public int    y;
                public int    cx;
                public int    cy;
                public uint   flags;
            }
            
            // ----------------------------------------------------------------------

            public static readonly IntPtr CURSOR_Arrow = LoadCursor(IntPtr.Zero, IDC_ARROW);
            
            public const uint MEM_COMMIT                 = 0x1000;
            public const uint MEM_RESERVE                = 0x2000;
            public const uint MEM_RELEASE                = 0x8000;
            public const uint PAGE_READWRITE             = 0x04;
            public const uint PAGE_EXECUTE_READWRITE     = 0x40;
            public const uint WAIT_OBJECT_0              = 0x00000000;
            public const uint INFINITE                   = 0xFFFFFFFF;
            public const int  WS_OVERLAPPED              = 0x00000000;
            public const int  WS_CAPTION                 = 0x00C00000;
            public const int  WS_SYSMENU                 = 0x00080000;
            public const int  WS_THICKFRAME              = 0x00040000;
            public const int  WS_MINIMIZEBOX             = 0x00020000;
            public const int  WS_MAXIMIZEBOX             = 0x00010000;
            public const int  WS_OVERLAPPEDWINDOW        = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
            public const int  WS_VISIBLE                 = 0x10000000;
            public const uint WS_CHILD                   = 0x40000000;
            public const uint PM_NOREMOVE                = 0x0000;
            public const uint PM_REMOVE                  = 0x0001;
            public const uint PM_NOYIELD                 = 0x0002;
            public const uint WM_DESTROY                 = 0x0002;
            public const uint WM_SETCURSOR               = 0x0020;
            public const uint WM_SETTEXT                 = 0x000C;
            public const uint WM_CLOSE                   = 0x0010;
            public const uint WM_PAINT                   = 0x000F;
            public const uint SWP_NOSIZE                 = 0x0001;
            public const uint SWP_NOMOVE                 = 0x0002;
            public const uint SWP_NOZORDER               = 0x0004;
            public const uint SWP_NOREDRAW               = 0x0008;
            public const uint SWP_NOACTIVATE             = 0x0010;
            public const uint SWP_FRAMECHANGED           = 0x0020;
            public const uint SWP_SHOWWINDOW             = 0x0040;
            public const uint SWP_HIDEWINDOW             = 0x0080;
            public const uint SWP_NOCOPYBITS             = 0x0100;
            public const uint SWP_NOOWNERZORDER          = 0x0200;
            public const uint SWP_NOSENDCHANGING         = 0x0400;
            public const uint SWP_DRAWFRAME              = SWP_FRAMECHANGED;
            public const uint SWP_NOREPOSITION           = SWP_NOOWNERZORDER;
            public const uint WM_MOVE                    = 0x0003;
            public const uint WM_SIZE                    = 0x0005;
            public const uint WM_SHOWWINDOW              = 0x0018;
            public const uint WM_WINDOWPOSCHANGED        = 0x0047;
            public const uint WM_WINDOWPOSCHANGING       = 0x0046;
            public const uint WM_ERASEBKGND              = 0x0014;
            public const uint WM_COMMAND                 = 0x0111;
            public const int  SW_HIDE                    = 0;
            public const int  SW_SHOW                    = 5;
            public const int  HTCLIENT                   = 1;
            public const int  GWL_STYLE                  = -16;
            public const int  GWL_EXSTYLE                = -20;
            public const int  ERROR_CLASS_DOES_NOT_EXIST = 1407;
            public const uint BS_PUSHBUTTON              = 0x00000000;
            public const uint BS_DEFPUSHBUTTON           = 0x00000001;
            public const uint BS_CHECKBOX                = 0x00000002;
            public const uint BS_AUTOCHECKBOX            = 0x00000003;
            public const uint BS_RADIOBUTTON             = 0x00000004;
            public const uint BS_3STATE                  = 0x00000005;
            public const uint BS_AUTO3STATE              = 0x00000006;
            public const uint BS_GROUPBOX                = 0x00000007;
            public const uint BS_USERBUTTON              = 0x00000008;
            public const uint BS_AUTORADIOBUTTON         = 0x00000009;
            public const uint BS_PUSHBOX                 = 0x0000000A;
            public const uint BS_OWNERDRAW               = 0x0000000B;
            public const uint BS_SPLITBUTTON             = 0x0000000C;
            public const uint BS_DEFSPLITBUTTON          = 0x0000000D;
            public const uint BS_COMMANDLINK             = 0x0000000E;
            public const uint BS_DEFCOMMANDLINK          = 0x0000000F;
            public const int  IDC_ARROW                  = 32512;
            public const int  WS_EX_LAYERED              = 0x00080000;
            public const uint LWA_COLORKEY               = 0x00000001;
            public const uint LWA_ALPHA                  = 0x00000002;
            public const int  HORZRES                    = 8;
            public const int  VERTRES                    = 10;
            public const uint SRCCOPY                    = 0x00CC0020;
            public const uint SRCPAINT                   = 0x00EE0086;
            public const uint SRCAND                     = 0x008800C6;
            public const uint SRCINVERT                  = 0x00660046;
            public const uint SRCERASE                   = 0x00440328;
            public const uint NOTSRCCOPY                 = 0x00330008;
            public const uint NOTSRCERASE                = 0x001100A6;
            public const uint MERGECOPY                  = 0x00C000CA;
            public const uint MERGEPAINT                 = 0x00BB0226;
            public const uint PATCOPY                    = 0x00F00021;
            public const uint PATPAINT                   = 0x00FB0A09;
            public const uint PATINVERT                  = 0x005A0049;
            public const uint DSTINVERT                  = 0x00550009;
            public const uint BLACKNESS                  = 0x00000042;
            public const uint WHITENESS                  = 0x00FF0062;
        }
    }
}