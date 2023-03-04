using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using System;

namespace Film.Tests
{
    /// <summary>
    /// TimerStreamer
    /// 時間の流れに関する処理
    ///
    /// ・1フレームにかかった時間を与えて進む
    /// これらの操作が可能
    /// ・通常進行
    /// ・倍速
    /// ・スロー
    /// ・巻き戻し
    /// 
    /// ・スキップ(時間を飛ばす)
    /// 　(通常モードからのみ可能？)
    /// </summary>
    public class TimeStreamerTest
    {
        [Test]
        public void 初期生成_DeltaTimeを与えるとそのまま進む()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var delta = DeltaTime.FromFloat(1.0f);
            streamer.Stream(delta);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
        }

        [Test]
        public void 高速モードに変更_時間を速く進行する_変更前の現在時刻を引き継ぐこと()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var delta = DeltaTime.FromFloat(1.0f);
            streamer.Stream(delta);
            streamer = streamer.ToFastMode(2.0);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
            streamer.Stream(delta);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(3.0f)));
        }

        [Test]
        public void 高速モードに変更_通常速度以下なら例外を投げること()
        {
            var streamer = TimeStreamerFactory.CreateStart();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToFastMode(0.9);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToFastMode(1.0);
            });
        }

        [Test]
        public void 低速モードに変更_時間をゆっくり進行する_変更前の現在時刻を引き継ぐこと()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var delta = DeltaTime.FromFloat(1.0f);
            streamer.Stream(delta);
            streamer = streamer.ToSlowMode(0.5);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
            streamer.Stream(delta);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.5f)));
        }

        [Test]
        public void 低速モードに変更_通常速度以上なら例外を投げること_0でも例外投げる()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToSlowMode(1.1);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToSlowMode(1.0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToSlowMode(0.0);
            });
        }

        [Test]
        public void 任意のモードから通常モードに変更_変更前の現在時刻を引き継ぐこと()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var delta = DeltaTime.FromFloat(1.0f);
            streamer = streamer.ToFastMode(2.0);
            streamer.Stream(delta);
            streamer = streamer.ToSequential();
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(2.0f)));
            streamer.Stream(delta);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(3.0f)));
        }

        [Test]
        public void 停止モードに変更_時間は固定されること()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var delta = DeltaTime.FromFloat(1.0f);
            streamer.Stream(delta);
            streamer = streamer.ToStopMode();
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
            streamer.Stream(delta);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
        }

        [Test]
        public void 巻き戻しモードに変更_時間を巻き戻す()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            var tenSeconds = DeltaTime.FromFloat(10.0f);
            streamer.Stream(tenSeconds);
            streamer = streamer.ToReverseMove();
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(10.0f)));
            var oneSeconds = DeltaTime.FromFloat(1.0f);
            streamer.Stream(oneSeconds);
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(9.0f)));
        }

        [Test]
        public void 巻き戻しモードに変更_0秒以前には巻き戻らないこと()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            streamer.Stream(DeltaTime.FromFloat(1.0f));
            streamer = streamer.ToReverseMove();
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(1.0f)));
            streamer.Stream(DeltaTime.FromFloat(2.0f));
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(0.0f)));
        }

        [Test]
        public void 巻き戻しモード_速度変更()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            streamer.Stream(DeltaTime.FromFloat(10.0f));
            streamer = streamer.ToReverseMove(0.5);
            streamer.Stream(DeltaTime.FromFloat(1.0f));

            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(9.5f)));
        }

        [Test]
        public void 巻き戻しモード変更_速度0以下にならない()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToReverseMove(0.0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                streamer = streamer.ToReverseMove(-1.0);
            });
        }

        [Test]
        public void スキップ_任意の秒を飛ばす_通常モードに移行()
        {
            var streamer = TimeStreamerFactory.CreateStart();
            streamer = streamer.Skip(DeltaTime.FromFloat(10.0f));
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(10.0f)));
            streamer.Stream(DeltaTime.FromFloat(2));
            Assert.That(streamer.Now, Is.EqualTo(WorldTime.FromFloat(12.0f)));
        }

        // 時を飛ばす(スキップ)
    }
}
