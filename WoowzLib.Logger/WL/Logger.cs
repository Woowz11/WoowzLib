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
            StatusInfo[(byte)MessageStatus.Default ] = new StatusInfo{ Symbol = 'I'};
            StatusInfo[(byte)MessageStatus.Warning ] = new StatusInfo{ Symbol = 'W', Color = WLO.StatusInfo.ANSI_Yellow};
            StatusInfo[(byte)MessageStatus.Error   ] = new StatusInfo{ Symbol = 'E', Color = WLO.StatusInfo.ANSI_Red};
            StatusInfo[(byte)MessageStatus.Fatal   ] = new StatusInfo{ Symbol = 'F', Color = WLO.StatusInfo.ANSI_Magenta};
            StatusInfo[(byte)MessageStatus.Debug   ] = new StatusInfo{ Symbol = 'D', Color = WLO.StatusInfo.ANSI_Green};
            StatusInfo[(byte)MessageStatus.External] = new StatusInfo{ Symbol = '?'};
            
            WL.Core.Output = (Status, ExtraInfo, Message) => {
                StringBuilder SB = new StringBuilder();

                StatusInfo StatusInfo = Logger.StatusInfo[Status];

                string[] Lines = Message.Split('\n');
                
                string  Prefix        =                    WL.Logger.Prefix(Status, StatusInfo);
                string? PrefixNewLine = Lines.Length > 0 ? WL.Logger.Prefix(Status, StatusInfo, true) : null;

                for(int i = 0; i < Lines.Length; i++){
                    string Line = Lines[i].TrimEnd('\r');

                    SB.Append(i == 0 ? Prefix : PrefixNewLine);

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
    public static string Prefix(byte Status, StatusInfo StatusInfo, bool NewLine = false){
        return (NewLine ? '~' : (StatusInfo.Symbol == ' ' ? Status : StatusInfo.Symbol)) + ": " + StatusInfo.Color;
    }

    public static string Suffix(StatusInfo StatusInfo){
        return WLO.StatusInfo.ANSI_End;
    }
    
    // ----------------------------------------------------------------------

    /// <summary>
    /// Класс, для информации об статусе
    /// </summary>
    public class StatusInfoCollection{
        private readonly StatusInfo[] __StatusInfos = new StatusInfo[255];

        /// <summary>
        /// Очищает массив статусов
        /// </summary>
        public void Clear(){ Array.Fill(__StatusInfos, WLO.StatusInfo.Default); }
        
        public StatusInfo this[byte Status]{
            get => __StatusInfos[Status];
            set => __StatusInfos[Status] = value;
        }
    }

    /// <summary>
    /// Информации об статусе
    /// </summary>
    public static readonly StatusInfoCollection StatusInfo = new StatusInfoCollection();
}