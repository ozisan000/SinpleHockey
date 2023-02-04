using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;     //ゲーム開始時のボールの速度

    Rigidbody rb;
    Sound sound;
    Vector3 ballDir;      //ボールの向き

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
        rb = GetComponent<Rigidbody>();

        collisionHandler = GetComponent<CollisionHandler>();
        this.sound = sound;
        collisionHandler.collisionEnterEvent += ReflectBall;
    }

    /// <summary>
    /// ボールの動きのプログラム
    /// </summary>
    public void Move()
    {
        const int notDir = 0;
        const int minDir = -1;
        const int maxDir = 2;

        Vector3 startDir = Vector3.zero;
        while (startDir.x == notDir || startDir.z == notDir)
        {
            startDir.x = UnityEngine.Random.Range(minDir, maxDir);
            startDir.z = UnityEngine.Random.Range(minDir, maxDir);
        }

        rb.AddForce(startDir * startSpeed, ForceMode.Impulse);
    }


    /// <summary>
    /// ボールを止めるプログラム
    /// </summary>
    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        ballDir = Vector3.zero;
    }


    /// <summary>
    /// ボールの中身の情報を更新するプログラム
    /// </summary>
    private void Update()
    {
        ballDir = rb.velocity;
    }



    /// <summary>
    /// ボールが反射したときの計算のプログラム
    /// </summary>
    /// <param name="collision"></param>
    void ReflectBall(Collision collision)
    {
        Vector3 result = Vector3.Reflect(ballDir, collision.contacts[0].normal);

        if (collision.transform.tag == playerTag)
        {
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            var playerMoveSpeed = playerRigidBody.velocity.normalized.z;

            if (PlayerUpMoveCheck(playerMoveSpeed, result.z) ||
                PlayerDownMoveCheck(playerMoveSpeed, result.z))
            {
                result.z = -result.z;
            }
        }

        rb.velocity = result;
        sound.PlaySE(SEType.Reflect);
        ballDir = rb.velocity;
    }

    //==========================
    //上級者向け
    //==========================

    bool PlayerUpMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed > 0 && ball_dir < 0;
    }

    bool PlayerDownMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed < 0 && 0 < ball_dir;
    }
}
