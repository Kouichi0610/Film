using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移動プログラムにそって動く
/// <summary>
/// 時間進行
/// </summary>
public class TimeStream
{
    //現在時刻(開始からの)
    float now;


    public TimeStream()
    {
        now = Time.realtimeSinceStartup;
    }

    // forward
    // speed


    // 先に進んでいるか巻き戻しているか


    public override string ToString()
    {
        return string.Format("{0}", now);
    }
}
