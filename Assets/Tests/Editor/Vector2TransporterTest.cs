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
        [Test]
        public void Linear_FromTo間の移動()
        {
            var moveTime = new MoveTime(5, 2);
            var from = new Vector2(1, 5);
            var to = new Vector2(3, 1);
            var transporter = Vector2Transporters.Linear(from, to, moveTime);

            Assert.That(transporter.Move(CurrentTime.FromFloat(5)), Is.EqualTo(new Vector2(1, 5)));
            Assert.That(transporter.Move(CurrentTime.FromFloat(6)), Is.EqualTo(new Vector2(2, 3)));
            Assert.That(transporter.Move(CurrentTime.FromFloat(7)), Is.EqualTo(new Vector2(3, 1)));
        }
    }
}
