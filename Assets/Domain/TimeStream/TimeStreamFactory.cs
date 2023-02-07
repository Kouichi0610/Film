using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Film.Domain.TimeStream
{
#if false
    public sealed class TimeStreamFactory
    {
        public ITimeStream Create()
        {
            return new Sequential(new CurrentTime(0), Scale.One);
        }
    }

    public static class TimeStreamExtension
    {
        public static ITimeStream ToSequential(this ITimeStream s, double scale = 1.0f)
        {
            return new Sequential(s.Current, Scale.FromDouble(scale));
        }
        public static ITimeStream ToReversal(this ITimeStream s, double scale = 1.0)
        {
            return new Reversal(s.Current, Scale.FromDouble(scale));
        }
        public static ITimeStream ToPause(this ITimeStream s)
        {
            return new Pausing(s.Current);
        }
    }
#endif
}
