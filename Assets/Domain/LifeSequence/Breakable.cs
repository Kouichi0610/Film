using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Entity;

namespace Film.Domain.LifeSequence
{
    internal sealed class Breakable : LifeSequencer, DamageReceiver
    {
        readonly int fullHitPoint;
        Logger damageLog;

        internal Breakable(int fullHitPoint)
        {
            this.fullHitPoint = fullHitPoint;
            damageLog = new Logger();
        }

        DamageReceiver LifeSequencer.DamageReceiver => this;

        bool LifeSequencer.Living(WorldTime now)
        {
            return fullHitPoint > damageLog.TotalDamage(now);
        }

        void LifeSequencer.Rewind(WorldTime redoTime)
        {
            damageLog = damageLog.Rewind(redoTime);
        }

        void DamageReceiver.Damage(int damagePoint, WorldTime now)
        {
            damageLog.Damage(damagePoint, now);
        }
        class Logger
        {
            List<Log> damageLog = new List<Log>();

            public int TotalDamage(WorldTime now)
            {
                var res = 0;
                foreach (var log in damageLog)
                {
                    if (log.Past(now) == false) continue;
                    res += log.Damage;
                }
                return res;
            }
            public void Damage(int damage, WorldTime now)
            {
                damageLog.Add(new Log(damage, now));
            }

            public Logger Rewind(WorldTime now)
            {
                var next = new List<Log>();
                foreach (var log in damageLog)
                {
                    if (log.Past(now))
                    {
                        next.Add(log);
                    }
                }

                return new Logger
                {
                    damageLog = next
                };
            }
        }

        struct Log
        {
            public WorldTime Time;
            public int Damage;
            public Log(int damage, WorldTime time)
            {
                Time = time;
                Damage = damage;
            }
            public bool Past(WorldTime now)
            {
                return Time.Past(now);
            }
            public override string ToString()
            {
                return string.Format("Damage:{0} at {1}", Damage, Time);
            }
        }
    }
}
