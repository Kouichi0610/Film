
namespace Film.Domain.TimeStream
{
    /// <summary>
    /// 1フレームあたりにかかった時間
    /// </summary>
    public struct DeltaTime
    {
        public float Seconds;

        public static DeltaTime FromRealTime()
        {
            return new DeltaTime(UnityEngine.Time.deltaTime);
        }

        public static DeltaTime FromFloat(float time)
        {
            return new DeltaTime(time);
        }
        DeltaTime(float time) => Seconds = time;

        public DeltaTime Factor(double factor)
        {
            return new DeltaTime((float)(Seconds * factor));
        }
    }
}
