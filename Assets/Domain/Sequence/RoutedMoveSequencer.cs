using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;
using System.Linq;

namespace Film.Domain.Sequence
{
    public sealed class RoutedMoveSequencer
    {
        readonly IReadOnlyList<Vector2Transporter> transporters;
        readonly Vector2Transporter before;
        readonly Vector2Transporter after;

        internal RoutedMoveSequencer(IReadOnlyList<Vector2Transporter> transporters)
        {
            this.transporters = transporters;
            before = transporters.First();
            after = transporters.Last();
        }
        public Vector2 Move(WorldTime now)
        {
            foreach (var transporter in transporters)
            {
                if (transporter.Exists(now) == false) continue;
                return transporter.Move(now);
            }
            throw new ArgumentOutOfRangeException(string.Format("Can not Time {0}", now));
        }

        public bool Exists(WorldTime now)
        {
            if (before.Exists(now)) return false;
            if (after.Exists(now)) return false;
            return true;
        }
    }
}
