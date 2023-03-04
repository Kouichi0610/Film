using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Airframe
{
    public class AirframeTest
    {
        [UnityTest]
        public IEnumerator TODO_Domain作成後()
        {
            Assert.Fail();
            yield break;
        }
#if false
        AirframeLoader loader;
        [OneTimeSetUp]
        public void InitializeScene()
        {
            SceneManager.LoadScene("AirframeTestScene");
            loader = null;
        }

        [UnityTest, Order(0)]
        public IEnumerator AirframeLoaderの取得()
        {
            loader = GetFirstComponent<AirframeLoader>();
            Assert.That(loader, Is.Not.Null);
            yield return null;
        }
        [UnityTest, Order(1)]
        public IEnumerator Ａirframeオブジェクトに位置を指定して生成()
        {
            var airframe = loader.Create(new Vector2(15, 10));
            Assert.That(airframe, Is.Not.Null);

            yield return null;
        }

        T GetFirstComponent<T>() where T : class
        {
            var scene = SceneManager.GetActiveScene();
            foreach (var r in scene.GetRootGameObjects())
            {
                var res = r.GetComponent<T>();
                if (res != null) return res;
            }
            throw new System.NullReferenceException(string.Format("{0} not found.", typeof(T).Name));
        }
#endif
    }
}
