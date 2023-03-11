using Film.Domain.TimeStream;

namespace Film.Domain.LifeSequence
{
    public static class LifeSequencers
    {
        public static LifeSequencer Breakable(int hitpoint)
        {
            return new Breakable(hitpoint);
        }
        public static LifeSequencer Invincible()
        {
            return new Invincible();
        }
    }
}
