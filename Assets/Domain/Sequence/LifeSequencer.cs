using System.Collections.Generic;
using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    public sealed class LifeSequencer
    {
        readonly int fullHitPoint;
        List<DamageLog> log = new List<DamageLog>();

        public int HitPoint(WorldTime now)
        {
            var res = fullHitPoint;
            foreach (var l in log)
            {
                if (l.Past(now) == false) continue;
                res -= l.Damage;
            }
            return Mathf.Clamp(res, 0, res);
        }
        public bool Defeated(WorldTime now)
        {
            return HitPoint(now) == 0;
        }

        public static LifeSequencer FromHitPoint(int startHitPoint)
        {
            return new LifeSequencer(startHitPoint);
        }

        LifeSequencer(int fullHitPoint)
        {
            this.fullHitPoint = fullHitPoint;
        }
        LifeSequencer(int fullHitPoint, List<DamageLog> log)
        {
            this.fullHitPoint = fullHitPoint;
            this.log = log;
        }

        public void Damage(int damagePoint, WorldTime now)
        {
            log.Add(new DamageLog(now, damagePoint));
        }

        public LifeSequencer Rewind(WorldTime redoTime)
        {
            var nextLog = new List<DamageLog>();
            foreach (var l in log)
            {
                if (l.Past(redoTime))
                {
                    nextLog.Add(l);
                }
            }
            return new LifeSequencer(fullHitPoint, nextLog);
        }

        struct DamageLog
        {
            public readonly WorldTime Time;
            public readonly int Damage;
            public DamageLog(WorldTime time, int damage)
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
                return string.Format("DamageLog At:{0} Damage:{1}", Time, Damage);
            }
        }
    }
}
