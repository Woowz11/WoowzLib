using System.Text;
using WLO;

namespace WL;

public static class Logger{
    /// <summary>
    /// Устанавливает новый Logger
    /// </summary>
    public static void Initialize(){
        try{
            StatusInfo.Clear();
            StatusInfo[(byte)MessageStatus.Default] = new StatusInfo{ Symbol = 'I'};
            StatusInfo[(byte)MessageStatus.Warning] = new StatusInfo{ Symbol = 'W'};
            StatusInfo[(byte)MessageStatus.Error  ] = new StatusInfo{ Symbol = 'E'};
            StatusInfo[(byte)MessageStatus.Fatal  ] = new StatusInfo{ Symbol = 'F'};
            StatusInfo[(byte)MessageStatus.Debug  ] = new StatusInfo{ Symbol = 'D'};
            
            WL.Core.Output = (Status, ExtraInfo, Message) => {
                StringBuilder SB = new StringBuilder();

                StatusInfo StatusInfo = Logger.StatusInfo[Status];

                SB.Append(WL.Logger.Prefix(Status, StatusInfo));
                
                SB.Append(Message);
                
                Console.WriteLine(SB.ToString());
            };
        }catch(Exception e){
            throw new Exception("Произошла ошибка при установке нового Logger!", e);
        }
    }

    /// <summary>
    /// Генерирует префикс сообщения
    /// </summary>
    public static string Prefix(byte Status, StatusInfo StatusInfo){
        return (StatusInfo.Symbol == ' ' ? Status : StatusInfo.Symbol) + ": TESTPRO ";
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