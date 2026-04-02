using WLO;

namespace WL;

public static partial class System{
    public static class Thread{
        /// <summary>
        /// Ограничивает поток через условие, не останавливая цикл (вызывает функцию если совпало всё)
        /// </summary>
        /// <param name="LastTime">Последнее время (должен быть уникальным!)</param>
        /// <param name="TargetDeltaTime">Целевой DeltaTime</param>
        /// <param name="TD">Информация об DeltaTime (должен быть уникальным!)</param>
        /// <returns>Ограничения прошли успешно</returns>
        public static bool Limit(ref double LastTime, double TargetDeltaTime, ref TickData TD){
            try{
                if(LastTime == 0){ LastTime = WL.System.Time.HighLifeS; }
                double Now = WL.System.Time.HighLifeS;

                TD.Start = LastTime;
                TD.End   = Now;

                if(TD.DeltaTime >= TargetDeltaTime){
                    LastTime = Now;
                    TD.TickCount++;
                    return true;
                }
                
                return false;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при ограничивании потока через DeltaTime!\nЦель: " + TargetDeltaTime, e);
            }
        }

        /// <summary>
        /// Ограничивает поток через условие, не останавливая цикл (вызывает функцию если совпало всё)
        /// </summary>
        /// <param name="LastTime">Последнее время (должен быть уникальным!)</param>
        /// <param name="TargetFPS">Целевой FPS</param>
        /// <param name="TD">Информация об DeltaTime (должен быть уникальным!)</param>
        /// <returns>Ограничения прошли успешно</returns>
        public static bool LimitFPS(ref double LastTime, double TargetFPS, ref TickData TD) => Limit(ref LastTime, TickData.FPSToDeltaTime(TargetFPS), ref TD);
    }
}