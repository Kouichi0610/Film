using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Tests
{
    public class Vector2TransporterTest
    {
        ValidatedTime CreateValidated(float now, float duration)
        {
            var wt = WorldTime.FromFloat(now);
            return ValidatedTime.FromWorldTimeAndDuration(wt, duration);
        }

        [Test]
        public void Linear_FromTo間の移動()
        {
            var moveTime = CreateValidated(5, 2);
            var from = new Vector2(1, 5);
            var to = new Vector2(3, 1);
            var transporter = Vector2Transporters.Linear(from, to, moveTime);

            Assert.That(transporter.Move(WorldTime.FromFloat(5)), Is.EqualTo(new Vector2(1, 5)));
            Assert.That(transporter.Move(WorldTime.FromFloat(6)), Is.EqualTo(new Vector2(2, 3)));
            Assert.That(transporter.Move(WorldTime.FromFloat(7)), Is.EqualTo(new Vector2(3, 1)));
        }
    }
}
