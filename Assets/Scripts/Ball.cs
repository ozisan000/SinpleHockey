using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;     //�Q�[���J�n���̃{�[���̑��x

    Rigidbody rb;
    Sound sound;
    Vector3 ballDir;      //�{�[���̌���

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
    /// �{�[��������������v���O�����i�������������s���j
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
    /// �{�[���̓����̃v���O����
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
    /// �{�[�����~�߂�v���O����
    /// </summary>
    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        ballDir = Vector3.zero;
    }


    /// <summary>
    /// �{�[���̒��g�̏����X�V����v���O����
    /// </summary>
    private void Update()
    {
        ballDir = rb.velocity;
    }



    /// <summary>
    /// �{�[�������˂����Ƃ��̌v�Z�̃v���O����
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
    //�㋉�Ҍ���
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
