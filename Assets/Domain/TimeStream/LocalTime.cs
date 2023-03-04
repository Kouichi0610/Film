
namespace Film.Domain.TimeStream
{
    /// <summary>
    /// オブジェクトのライフサイクルが開始してからの経過時間
    /// </summary>
    public struct LocalTime
    {
        float StartedSeconds;

        LocalTime(WorldTime now)
        {
            StartedSeconds = now.Seconds;
        }

        public static LocalTime FromWorldTime(WorldTime now)
        {
            return new LocalTime(now);
        }

        public float LocalSeconds(WorldTime now)
        {
            return now.Seconds - StartedSeconds;
        }

        public bool Created(WorldTime now)
        {
            return LocalSeconds(now) >= 0;
        }

        public override string ToString()
        {
            return string.Format("LocalTime since:{0}(seconds)", StartedSeconds);
        }
    }
}
