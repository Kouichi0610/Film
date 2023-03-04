using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Domain.Sequence
{
    public sealed class MoveSequencer
    {
        readonly IReadOnlyList<Vector2Transporter> transporters;

        internal MoveSequencer(IReadOnlyList<Vector2Transporter> transporters)
        {
            this.transporters = transporters;
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
    }
}
