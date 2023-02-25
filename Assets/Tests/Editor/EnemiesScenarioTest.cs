using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Film.Tests
{
    // currenttime -> localtime
    /// <summary>
    /// 実体をレンタル
    /// 出現
    /// 移動
    /// 射撃準備
    /// 射撃
    /// 退出
    /// 実体をリリース
    /// 
    /// 各行動に終了待ち
    /// </summary>
    public class EnemiesScenarioTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }
        [SetUp]
        public void SetUp()
        {
        }
        [TearDown]
        public void TearDown()
        {
        }
        [Test]
        public void ScenarioTest()
        {
            Assert.That(false, Is.True);
        }
        [Test]
        public void ScenarioTest2()
        {
            Assert.That(false, Is.True);
        }

        public interface EnemyEntity
        {
        }



    }
}
