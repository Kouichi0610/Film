
namespace Film.Domain.Transport
{
    public struct FromTo
    {
        public float From;
        public float To;
        float min;
        float max;

        public float Distance => To - From;

        public FromTo(float from, float to)
        {
            From = from;
            To = to;

            if (from < to)
            {
                min = from;
                max = to;
            }
            else
            {
                min = to;
                max = from;
            }
        }

        public float Clamp(float d)
        {
            if (d < min) return min;
            if (d > max) return max;
            return d;
        }

        public override string ToString()
        {
            return string.Format("From:{0} To:{1}", From, To);
        }
    }
}
