using Film.Domain.TimeStream;

namespace Film.Domain.Entity
{
    public interface DamageReceiver
    {
        void Damage(int damagePoint, WorldTime now);
    }
}
