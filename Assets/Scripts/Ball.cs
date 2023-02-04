using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;             //ゲーム開始時のボールの速度

    Rigidbody rb;                 //物理演算のプログラム
    Sound sound;                  //サウンドのプログラム
    Vector3 ballDir;              //ボールの向き

    CollisionHandler collisionHandler;
    public CollisionHandler CollisionHandler
    {
        get
        {
            return collisionHandler;
        }
    }
    const string playerTag = "Player";


    /// <summary>
    /// ボールを初期化するプログラム（動かす準備を行う）
    /// </summary>
    /// <param name="sound"></param>
    public void Init(Sound sound)
    {
        collisionHandler = GetComponent<CollisionHandler>();

        //当たり判定のプログラムにボールが
        //オブジェクトに当たったら反射するプログラムを設定
        collisionHandler.collisionEnterEvent += ReflectBall;
        //ゲームオブジェクトから物理計算プログラムを取得
        rb = GetComponent<Rigidbody>();
        //サウンドのプログラムを保存
        this.sound = sound;
    }

    /// <summary>
    /// ボールの動きのプログラム
    /// </summary>
    public void Move()
    {
        const int notDir = 0;   //方向なし
        const int minDir = -1;  //小さい値
        const int maxDir = 2;   //大きい値

        //Moveだけ使えるstartDir変数を初期化
        Vector3 startDir = Vector3.zero;

        //xが方向なしの時 または zが方向なしの時
        //もう一度ランダムに方向を決定する
        while (startDir.x == notDir || startDir.z == notDir)
        {
            //ランダムに方向を決定する
            startDir.x = UnityEngine.Random.Range(minDir, maxDir);
            startDir.z = UnityEngine.Random.Range(minDir, maxDir);
        }

        //ランダムな方向にスピードを加えて飛ばす
        rb.AddForce(startDir * startSpeed, ForceMode.Impulse);
    }


    /// <summary>
    /// ボールを止めるプログラム
    /// </summary>
    public void StopMove()
    {
        //ボールの速度を0に
        rb.velocity = Vector3.zero;
        //現在のボールの向きをリセット
        ballDir = Vector3.zero;
    }


    /// <summary>
    /// ボールの中身の情報を更新するプログラム
    /// </summary>
    private void Update()
    {
        //現在のボールの向きを常に取得
        ballDir = rb.velocity;
    }



    /// <summary>
    /// ボールが反射したときの計算のプログラム
    /// </summary>
    /// <param name="collision"></param>
    void ReflectBall(Collision collision)
    {
        //現在の向きを反射させる
        Vector3 result = Vector3.Reflect(ballDir, collision.contacts[0].normal);

        /*==========================
        //上級者向け
        //==========================
        //当たったオブジェクトがプレイヤーかどうか
        if (collision.transform.tag == playerTag)
        {
        //プレイヤーの物理演算プログラムを取得
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            //プレイヤーがどっちに移動しているか取得
            var playerMoveSpeed = playerRigidBody.velocity.normalized.z;
            //ボールを反転させるかチェック
            if (PlayerUpMoveCheck(playerMoveSpeed, result.z) ||
                PlayerDownMoveCheck(playerMoveSpeed, result.z))
            {
                //飛んできた方向に反転
                result.z = -result.z;
            }
        }
        //==========================*/

        //反射する際の音を再生
        sound.PlaySE(SEType.Reflect);
        //速度を反映
        rb.velocity = result;
        ballDir = rb.velocity;
    }

    /*//==========================
    //上級者向け
    //==========================
    /// <summary>
    /// プレイヤーが上に移動しているかつボールが下に移動している場合
    /// </summary>
    /// <param name="player_speed"></param>
    /// <param name="ball_dir"></param>
    /// <returns></returns>
    bool PlayerUpMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed > 0 && ball_dir < 0;
    }

    /// <summary>
    /// プレイヤーがに下移動しているかつボールが上に移動している場合
    /// </summary>
    /// <param name="player_speed"></param>
    /// <param name="ball_dir"></param>
    /// <returns></returns>
    bool PlayerDownMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed < 0 && 0 < ball_dir;
    }*/
}
