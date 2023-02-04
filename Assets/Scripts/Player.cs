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
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// インプットマネージャーからキー入力情報をもらってくる（どのボタンが押されたか調べる）プログラム
    /// </summary>
    /// <param name="value"></param>
    public void InputKey(float value)
    {
        inputKeyValue = value;
    }


    /// <summary>
    /// プレイヤーを止めるプログラム
    /// </summary>
    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        inputKeyValue = 0.0f;
    }


    /// <summary>
    /// プレイヤーの中身の情報を更新するプログラム
    /// </summary>
    private void Update()
    {
        rb.velocity = Vector3.forward * inputKeyValue * speed;
    }
}
