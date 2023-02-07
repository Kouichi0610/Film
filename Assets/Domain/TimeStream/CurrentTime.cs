
namespace Film.Domain.TimeStream
{
    /// <summary>
    /// ゲーム開始から進んだ時間
    /// </summary>
    public struct CurrentTime
    {
        public float Seconds;

        internal CurrentTime(float time)
        {
            Seconds = time;
        }

        public static CurrentTime FromFloat(float time)
        {
           return new CurrentTime(time);
        }

        internal CurrentTime Add(DeltaTime delta)
        {
            return new CurrentTime(Seconds + delta.Seconds);
        }
        internal CurrentTime Sub(DeltaTime delta)
        {
            var next = Seconds - delta.Seconds;
            if (next < 0) next = 0;
            return new CurrentTime(next);
        }

        public override int GetHashCode()
        {
            return Seconds.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is CurrentTime)) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}", Seconds);
        }
    }
}
