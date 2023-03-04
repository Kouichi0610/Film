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
            var worldTime = WorldTime.FromFloat(now);
            return ValidatedTime.FromWorldTimeAndDuration(worldTime, duration);
        }

        [Test]
        public void Linear_FromからToまで直線的に移動すること()
        {
            var moveTime = CreateValidated(5, 2);
            var from = new Vector2(1, 5);
            var to = new Vector2(3, 1);
            var transporter = Vector2Transporters.Linear(from, to, moveTime);

            Assert.That(transporter.Move(WorldTime.FromFloat(5)), Is.EqualTo(new Vector2(1, 5)));
            Assert.That(transporter.Move(WorldTime.FromFloat(6)), Is.EqualTo(new Vector2(2, 3)));
            Assert.That(transporter.Move(WorldTime.FromFloat(7)), Is.EqualTo(new Vector2(3, 1)));
        }

        [Test]
        public void BeforeLiving_開始時刻より前に存在し続けること()
        {
            var transporter = Vector2Transporters.BeforeLiving(new Vector2(3, 4), WorldTime.FromFloat(30));

            Assert.That(transporter.Exists(WorldTime.FromFloat(29.9f)), Is.True);
            Assert.That(transporter.Exists(WorldTime.FromFloat(30.0f)), Is.False);
            Assert.That(transporter.Exists(WorldTime.FromFloat(0.0f)), Is.True);
        }
        [Test]
        public void BeforeLiving_どの時刻においても開始地点をさし続けること()
        {
            var position = new Vector2(3, 4);
            var transporter = Vector2Transporters.BeforeLiving(position, WorldTime.FromFloat(30));

            Assert.That(transporter.Move(WorldTime.FromFloat(0)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(29.9f)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(30.0f)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(float.MaxValue)), Is.EqualTo(position));
        }
        [Test]
        public void AfterLiving_終了時刻より後に存在し続けること()
        {
            var transporter = Vector2Transporters.AfterLiving(new Vector2(3, 4), WorldTime.FromFloat(100));

            Assert.That(transporter.Exists(WorldTime.FromFloat(100.0f)), Is.False);
            Assert.That(transporter.Exists(WorldTime.FromFloat(100.1f)), Is.True);
            Assert.That(transporter.Exists(WorldTime.FromFloat(float.MaxValue)), Is.True);
        }
        [Test]
        public void AfterLiving_どの時刻においても終了地点をさし続けること()
        {
            var position = new Vector2(3, 4);
            var transporter = Vector2Transporters.AfterLiving(position, WorldTime.FromFloat(100));

            Assert.That(transporter.Move(WorldTime.FromFloat(0)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(100.0f)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(100.1f)), Is.EqualTo(position));
            Assert.That(transporter.Move(WorldTime.FromFloat(float.MaxValue)), Is.EqualTo(position));

        }
    }
}
