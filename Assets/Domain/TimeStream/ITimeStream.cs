using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Film.Domain.TimeStream
{
    /// <summary>
    /// 時間の流れインターフェイス
    /// 
    /// TODO:誤差大丈夫かどうか
    /// </summary>

    [System.Obsolete("use TimeStreamer.")]
    public interface ITimeStream
    {
        /// <summary>
        /// 現在時刻を取得
        /// </summary>
        //CurrentTime Current { get; }

        /// <summary>
        /// 現在時刻を更新
        /// </summary>
        /// <param name="deltaTime">Time.deltaTimeを想定</param>
        void Update(float deltaTime);
    }
}
