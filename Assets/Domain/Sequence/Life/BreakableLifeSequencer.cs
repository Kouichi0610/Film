using System.Collections.Generic;
using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    internal class BreakableLifeSequencer : LifeSequencer
    {
        readonly int fullHitPoint;
        Logger damageLog;

        internal BreakableLifeSequencer(int fullHitPoint)
        {
            this.fullHitPoint = fullHitPoint;
            damageLog = new Logger();
        }
        void LifeSequencer.Damage(int damagePoint, WorldTime now)
        {
            damageLog.Damage(damagePoint, now);
        }

        bool LifeSequencer.Living(WorldTime now)
        {
            return fullHitPoint > damageLog.TotalDamage(now);
        }

        void LifeSequencer.Rewind(WorldTime redo)
        {
            damageLog = damageLog.Rewind(redo);
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
