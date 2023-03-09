
namespace Film.Domain.TimeStream
{
    /// <summary>
    /// オブジェクトのライフサイクルが開始してからの経過時間
    /// </summary>
    public struct LocalTime
    {
        WorldTime started;

        LocalTime(WorldTime now)
        {
            started = now;
        }

        public static LocalTime FromWorldTime(WorldTime now)
        {
            return new LocalTime(now);
        }

        public float LocalSeconds(WorldTime now)
        {
            return now.ElaspedTime(started);
        }

        public bool Created(WorldTime now)
        {
            return LocalSeconds(now) >= 0;
        }

        public override string ToString()
        {
            return string.Format("LocalTime {0}(seconds)", started);
        }
    }
}
