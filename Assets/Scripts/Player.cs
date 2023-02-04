using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;                //プレイヤーのスピード
    float inputKeyValue = 0.0f;
    Rigidbody rb;


    /// <summary>
    /// プレイヤーを初期化するプログラム（動かす準備を行う）
    /// </summary>
    public void Init()
    {
        //ゲームオブジェクトから物理計算プログラムを取得
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// インプットマネージャーからキー入力情報をもらってくる（どのボタンが押されたか調べる）プログラム
    /// </summary>
    /// <param name="value"></param>
    public void InputKey(float value)
    {
        //外部からの入力をプログラム内に一時保存
        inputKeyValue = value;
    }


    /// <summary>
    /// プレイヤーを止めるプログラム
    /// </summary>
    public void StopMove()
    {
        //プレイヤーの速度を0に
        rb.velocity = Vector3.zero;
        //一時保存されている入力データを0に
        inputKeyValue = 0.0f;
    }


    /// <summary>
    /// プレイヤーの中身の情報を更新するプログラム
    /// </summary>
    private void Update()
    {
        //物理演算の速度に
        //プレイヤーの上下の動きを固定 * キーの上下入力 * スピード を反映
        rb.velocity = Vector3.forward * inputKeyValue * speed;
    }
}
