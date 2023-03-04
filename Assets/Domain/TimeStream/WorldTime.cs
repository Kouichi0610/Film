
namespace Film.Domain.TimeStream
{
    /// <summary>
    /// ゲームを開始してからの経過時間
    /// </summary>
    public struct WorldTime
    {
        public float Seconds;

        internal WorldTime(float time)
        {
            Seconds = time;
        }

        public static WorldTime FromFloat(float time)
        {
           return new WorldTime(time);
        }

        public WorldTime AddSeconds(float seconds)
        {
            return new WorldTime(Seconds + seconds);
        }

        internal WorldTime Add(DeltaTime delta)
        {
            return new WorldTime(Seconds + delta.Seconds);
        }
        internal WorldTime Sub(DeltaTime delta)
        {
            var next = Seconds - delta.Seconds;
            if (next < 0) next = 0;
            return new WorldTime(next);
        }

        public bool Before(WorldTime now)
        {
            return Seconds < now.Seconds;
        }

        public bool After(WorldTime now)
        {
            return now.Seconds < Seconds;
        }

        public bool Exists(WorldTime start, WorldTime end)
        {
            return Seconds >= start.Seconds
                && Seconds <= end.Seconds;
        }

        public override int GetHashCode()
        {
            return Seconds.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is WorldTime)) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}", Seconds);
        }
    }
}
