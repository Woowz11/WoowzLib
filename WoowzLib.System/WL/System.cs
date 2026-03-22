using WLO;

namespace WLO{
    /// <summary>
    /// Операционная система
    /// </summary>
    public enum OS{ Android, FreeBSD, IOS, Linux, MacCatalyst, MacOS, TvOS, WatchOS, Windows, Unknown }
}

namespace WL{
    public static partial class System{
        static System(){
            // Детект операционной системы
            OS DetectOS(){
                if(OperatingSystem.IsWindows    ()){ return OS.Windows    ; }
                if(OperatingSystem.IsLinux      ()){ return OS.Linux      ; }
                if(OperatingSystem.IsMacOS      ()){ return OS.MacOS      ; }
                if(OperatingSystem.IsMacCatalyst()){ return OS.MacCatalyst; }
                if(OperatingSystem.IsFreeBSD    ()){ return OS.FreeBSD    ; }
                if(OperatingSystem.IsAndroid    ()){ return OS.Android    ; }
                if(OperatingSystem.IsIOS        ()){ return OS.IOS        ; }
                if(OperatingSystem.IsTvOS       ()){ return OS.TvOS       ; }
                if(OperatingSystem.IsWatchOS    ()){ return OS.WatchOS    ; }
                
                return OS.Unknown;
            }
            
            CurrentOS = DetectOS();

            if(CurrentOS != OS.Windows){ throw new Exception("WoowzLib.System работает только на Windows операционной системе! А сейчас: " + CurrentOS); }
        }

        /// <summary>
        /// Текущая операционная система
        /// </summary>
        public static readonly OS CurrentOS;
    }
}