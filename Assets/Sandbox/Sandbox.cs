using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 時間操作試作
/// 
/// やりたいこと
/// ・通常
/// ・早送り
/// ・スロー
/// ・スキップ
/// ・巻き戻し
/// ・時間停止
/// 
/// やるべきこと
/// ・時間管理(進行、早送り、巻き戻し...)
/// ・移動プログラム(前進、右折、左折)あらかじめ決められたプログラムを元に進行する
/// ・時間管理の移動プログラムへ干渉
/// 
/// ・移動プログラム->現在時刻を渡すとその時点の位置を返す(forward, reverse関係なし)
/// 
/// Sequence -> Reverse
/// 逆方向への移動命令を作成
/// 
/// 移動命令のログを取る
/// 補間
/// 
/// </summary>
public class Sandbox : MonoBehaviour
{
    [SerializeField]
    MovingObject[] movingObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnForward()
    {
        Debug.Log("Forward");
    }
    public void OnFast()
    {
        Debug.Log("Fast");
    }
    public void OnReverse()
    {
        Debug.Log("Reverse");
    }
    public void OnSkip()
    {
        Debug.Log("Skip");
    }
    public void OnPause()
    {
        Debug.Log("Pause");
    }
}
