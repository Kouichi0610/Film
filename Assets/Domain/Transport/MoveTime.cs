using System;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public struct MoveTime
    {
        public float Start;
        public float Duration;

        public float End => Start + Duration;

        public MoveTime(float start, float duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("negative duration not supported. {0}", duration));
            }
            Start = start;
            Duration = duration;
        }

        public bool Exists(CurrentTime now)
        {
            if (now.Seconds < Start) return false;
            if (now.Seconds > End) return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("Start:{0} Duration:{1}", Start, Duration);
        }
    }
}
