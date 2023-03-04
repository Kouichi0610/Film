using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using Film.Domain.Transport;
using Film.Domain.Sequence;

namespace Film.Tests
{
    public class LifeSequencerTest
    {
        #region 仮実装
        public sealed class LifeSequencerBuilder
        {
            WorldTime start;
            float duration = 0;
            int hitPoint = 0;

            public static LifeSequencerBuilder Start(WorldTime start)
            {
                return new LifeSequencerBuilder(start);
            }
            public LifeSequencerBuilder(WorldTime start)
            {
                this.start = start;
            }
            public LifeSequencerBuilder Duration(float duration)
            {
                this.duration = duration;
                return this;
            }
            public LifeSequencerBuilder HitPoint(int hp)
            {
                hitPoint = hp;
                return this;
            }
            public LifeSequencer Build()
            {
                if (hitPoint <= 0)
                {
                    throw new ArgumentException(string.Format("illegal HitPoint:{0}", hitPoint));
                }
                if (duration <= 0)
                {
                    throw new ArgumentException(string.Format("illegal Duration:{0}", duration));
                }
                return new LifeSequencer(start, duration, hitPoint);
            }
        }

        public sealed class LifeSequencer
        {
            readonly ValidatedTime validatedTime;
            readonly int hitPoint;

            internal LifeSequencer(WorldTime start, float duration, int hitPoint)
            {
                validatedTime = ValidatedTime.FromWorldTimeAndDuration(start, duration);
                this.hitPoint = hitPoint;
            }

            public bool Exists(WorldTime now)
            {
                return validatedTime.Exists(now);
            }

        }
        #endregion

        [Test, Order(1)]
        public void LifeSequencer_開始時刻と生存時間_HitPointを指定()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .Duration(15.0f)
                .HitPoint(100);
            Assert.That(builder.Build, Is.Not.Null);
        }
        [Test, Order(1)]
        public void LifeSequencer_生存期間を指定しないと例外を投げること()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .HitPoint(100);
            Assert.Throws<ArgumentException>(() =>
            {
                builder.Build();
            });
        }
        [Test, Order(2)]
        public void LifeSequencer_生存期間エラー()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .HitPoint(100)
                .Duration(-1);
            Assert.Throws<ArgumentException>(() =>
            {
                builder.Build();
            });
        }
        [Test, Order(3)]
        public void LifeSequencer_ヒットポイントを指定しないと例外を投げること()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .Duration(10);
            Assert.Throws<ArgumentException>(() =>
            {
                builder.Build();
            });
        }
        [Test, Order(4)]
        public void LifeSequencer_ヒットポイントエラー()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .Duration(10)
                .HitPoint(-1);
            Assert.Throws<ArgumentException>(() =>
            {
                builder.Build();
            });
        }
        [Test, Order(5)]
        public void LifeSequencer_生存期間を判定()
        {
            var builder = LifeSequencerBuilder.Start(WorldTime.FromFloat(15.0f))
                .Duration(5.0f)
                .HitPoint(10);
            var lifeSequencer = builder.Build();

            Assert.That(lifeSequencer.Exists(WorldTime.FromFloat(14.9f)), Is.False);
            Assert.That(lifeSequencer.Exists(WorldTime.FromFloat(15.0f)), Is.True);
            Assert.That(lifeSequencer.Exists(WorldTime.FromFloat(17.5f)), Is.True);
            Assert.That(lifeSequencer.Exists(WorldTime.FromFloat(20.0f)), Is.True);
            Assert.That(lifeSequencer.Exists(WorldTime.FromFloat(20.1f)), Is.False);
        }

        [Test]
        public void TODO_HitPointと生存期間を分ける()
        {
            Assert.Fail("TODO");

        }

    }
}
