using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Tests
{
    public class FloatTransporterTest
    {
        // 初期値、終端値、開始時刻、終了時刻
        [Test]
        public void 初期化()
        {
            var transporter = FloatTransporters.Linear(4, 5, new MoveTime(1, 5));
            Assert.That(transporter, Is.Not.Null);
        }
        [Test]
        public void 移動時間に0以下を指定できない_例外を投げる()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var moveTime = new MoveTime(1, 0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var moveTime = new MoveTime(1, -1);
            });
        }
        [Test]
        public void MoveTime_存在できない時間の判定()
        {
            var moveTime = new MoveTime(3, 8);
            Assert.That(moveTime.Exists(CurrentTime.FromFloat(2.9f)), Is.False);
            Assert.That(moveTime.Exists(CurrentTime.FromFloat(3)), Is.True);
            Assert.That(moveTime.Exists(CurrentTime.FromFloat(11)), Is.True);
            Assert.That(moveTime.Exists(CurrentTime.FromFloat(11.1f)), Is.False);
        }

        [Test]
        public void Linear_FromTo間の移動_プラス方向()
        {
            var moveTime = new MoveTime(3, 5);
            var transporter = FloatTransporters.Linear(1, 5, moveTime);

            Assert.That(transporter.Move(CurrentTime.FromFloat(3+0)), Is.EqualTo(1));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3+2.5f)), Is.EqualTo(3));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3+5)), Is.EqualTo(5));

            Assert.That(transporter.Move(CurrentTime.FromFloat(3-1)), Is.EqualTo(1));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3+5+1)), Is.EqualTo(5));
        }
        [Test]
        public void Linear_FromTo間の移動_マイナス方向()
        {
            var moveTime = new MoveTime(3, 5);
            var transporter = FloatTransporters.Linear(5, 1, moveTime);

            Assert.That(transporter.Move(CurrentTime.FromFloat(3 + 0)), Is.EqualTo(5));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3 + 2.5f)), Is.EqualTo(3));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3 + 5)), Is.EqualTo(1));

            Assert.That(transporter.Move(CurrentTime.FromFloat(3 - 1)), Is.EqualTo(5));
            Assert.That(transporter.Move(CurrentTime.FromFloat(3 + 5 + 1)), Is.EqualTo(1));
        }

    }
}
