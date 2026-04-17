using System.Collections;
using System.Text;
using WLO;

namespace WL;

public static class Logger{
    /// <summary>
    /// Устанавливает новый Logger
    /// </summary>
    public static void Initialize(LoggerSettings? Settings = null){
        try{
            WL.Core.BaseLoggerInitialize();
            
            if(Settings.HasValue){ WL.Logger.Settings = Settings.Value; }
            
            StatusInfo.Clear();
            StatusInfo[(byte)MessageStatus.Default ] = new StatusInfo{ Symbol = 'I', Color = ANSI.Code.White  , Color_Second = ANSI.Code.GrayB   };
            StatusInfo[(byte)MessageStatus.Warning ] = new StatusInfo{ Symbol = 'W', Color = ANSI.Code.YellowB, Color_Second = ANSI.Code.Yellow  };
            StatusInfo[(byte)MessageStatus.Error   ] = new StatusInfo{ Symbol = 'E', Color = ANSI.Code.Red    , Color_Second = ANSI.Code.RedB    };
            StatusInfo[(byte)MessageStatus.Fatal   ] = new StatusInfo{ Symbol = 'F', Color = ANSI.Code.Magenta, Color_Second = ANSI.Code.MagentaB};
            StatusInfo[(byte)MessageStatus.Debug   ] = new StatusInfo{ Symbol = 'D', Color = ANSI.Code.Green  , Color_Second = ANSI.Code.GreenB  };
            StatusInfo[(byte)MessageStatus.External] = new StatusInfo{ Symbol = '?', Color = ANSI.Code.CyanB  , Color_Second = ANSI.Code.Cyan    };

            bool Eval = false;
            byte OldStatus = 0;
            WL.Core.Output = (Status, ExtraInfo, Message) => {
                StringBuilder SB = new StringBuilder();

                StatusInfo StatusInfo = Logger.StatusInfo[Status];

                string[] Lines = Message.Split('\n');

                if(OldStatus != Status){
                    OldStatus = Status;
                    Eval = false;
                }
                
                string  Prefix        =                    WL.Logger.Prefix(Status, StatusInfo, false, Eval);
                string? PrefixNewLine = Lines.Length > 0 ? WL.Logger.Prefix(Status, StatusInfo, true , Eval) : null;

                for(int i = 0; i < Lines.Length; i++){
                    string Line = Lines[i].TrimEnd('\r');

                    SB.Append(i == 0 ? Prefix : PrefixNewLine);

                    if(i == 0){ Eval = !Eval; }
                    
                    SB.Append(Line);

                    SB.Append(Suffix(StatusInfo));
                    
                    if(i < Lines.Length - 1){ SB.Append('\n'); }
                }

                return SB.ToString();
            };
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке нового Logger!\nНастройки: " + WL.__Base.Other.ToString(Settings), e);
        }
    }

    /// <summary>
    /// Настройки Logger
    /// </summary>
    public static LoggerSettings Settings = new LoggerSettings();

    /// <summary>
    /// Генерирует префикс сообщения
    /// </summary>
    public static string Prefix(byte Status, StatusInfo StatusInfo, bool NewLine = false, bool Eval = false){
        string Color = ANSI.ToANSI(Eval ? StatusInfo.Color_Second : StatusInfo.Color);
        return Color + (NewLine ? '~' : (StatusInfo.Symbol == ' ' ? Status : StatusInfo.Symbol)) + ANSI.ToANSI(ANSI.Code.Reset) + ": " + Color;
    }

    /// <summary>
    /// Генерирует суффикс сообщения
    /// </summary>
    public static string Suffix(StatusInfo StatusInfo){
        return ANSI.ToANSI(ANSI.Code.Reset);
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Класс, для информации об статусе
    /// </summary>
    public class StatusInfoCollection : IEnumerable<StatusInfo>{
        private readonly StatusInfo[] __StatusInfos = new StatusInfo[255];

        /// <summary>
        /// Очищает массив статусов
        /// </summary>
        public void Clear(){ Array.Fill(__StatusInfos, WLO.StatusInfo.Default); }
        
        public StatusInfo this[byte Status]{
            get => __StatusInfos[Status];
            set => __StatusInfos[Status] = value;
        }
        
        public IEnumerator<StatusInfo> GetEnumerator() => ((IEnumerable<StatusInfo>)__StatusInfos).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// Информации об статусе
    /// </summary>
    public static readonly StatusInfoCollection StatusInfo = new StatusInfoCollection();
}