
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
