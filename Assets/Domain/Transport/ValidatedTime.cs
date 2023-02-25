using System;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public struct ValidatedTime
    {
        LocalTime localTime;
        float duration;

        public static ValidatedTime FromWorldTimeAndDuration(WorldTime now, float duration)
        {
            return new ValidatedTime(now, duration);
        }

        ValidatedTime(WorldTime now, float duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("negative duration not supported. {0}", duration));
            }
            localTime = LocalTime.FromWorldTime(now);
            this.duration = duration;
        }

        internal float Delta(float amount)
        {
            return amount / duration;
        }

        internal float LocalSeconds(WorldTime now)
        {
            return localTime.LocalSeconds(now);
        }

        public bool Exists(WorldTime now)
        {
            if (localTime.Created(now) == false) return false;
            if (localTime.LocalSeconds(now) > duration) return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0} Duration:{1}(seconds)", localTime, duration);
        }
    }
}
