using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float startSpeed;             //�Q�[���J�n���̃{�[���̑��x

    Rigidbody rb;                 //�������Z�̃v���O����
    Sound sound;                  //�T�E���h�̃v���O����
    Vector3 ballDir;              //�{�[���̌���

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
        collisionHandler = GetComponent<CollisionHandler>();

        //�����蔻��̃v���O�����Ƀ{�[����
        //�I�u�W�F�N�g�ɓ��������甽�˂���v���O������ݒ�
        collisionHandler.collisionEnterEvent += ReflectBall;
        //�Q�[���I�u�W�F�N�g���畨���v�Z�v���O�������擾
        rb = GetComponent<Rigidbody>();
        //�T�E���h�̃v���O������ۑ�
        this.sound = sound;
    }

    /// <summary>
    /// �{�[���̓����̃v���O����
    /// </summary>
    public void Move()
    {
        const int notDir = 0;   //�����Ȃ�
        const int minDir = -1;  //�������l
        const int maxDir = 2;   //�傫���l

        //Move�����g����startDir�ϐ���������
        Vector3 startDir = Vector3.zero;

        //x�������Ȃ��̎� �܂��� z�������Ȃ��̎�
        //������x�����_���ɕ��������肷��
        while (startDir.x == notDir || startDir.z == notDir)
        {
            //�����_���ɕ��������肷��
            startDir.x = UnityEngine.Random.Range(minDir, maxDir);
            startDir.z = UnityEngine.Random.Range(minDir, maxDir);
        }

        //�����_���ȕ����ɃX�s�[�h�������Ĕ�΂�
        rb.AddForce(startDir * startSpeed, ForceMode.Impulse);
    }


    /// <summary>
    /// �{�[�����~�߂�v���O����
    /// </summary>
    public void StopMove()
    {
        //�{�[���̑��x��0��
        rb.velocity = Vector3.zero;
        //���݂̃{�[���̌��������Z�b�g
        ballDir = Vector3.zero;
    }


    /// <summary>
    /// �{�[���̒��g�̏����X�V����v���O����
    /// </summary>
    private void Update()
    {
        //���݂̃{�[���̌�������Ɏ擾
        ballDir = rb.velocity;
    }



    /// <summary>
    /// �{�[�������˂����Ƃ��̌v�Z�̃v���O����
    /// </summary>
    /// <param name="collision"></param>
    void ReflectBall(Collision collision)
    {
        //���݂̌����𔽎˂�����
        Vector3 result = Vector3.Reflect(ballDir, collision.contacts[0].normal);

        /*==========================
        //�㋉�Ҍ���
        //==========================
        //���������I�u�W�F�N�g���v���C���[���ǂ���
        if (collision.transform.tag == playerTag)
        {
        //�v���C���[�̕������Z�v���O�������擾
            var playerRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            //�v���C���[���ǂ����Ɉړ����Ă��邩�擾
            var playerMoveSpeed = playerRigidBody.velocity.normalized.z;
            //�{�[���𔽓]�����邩�`�F�b�N
            if (PlayerUpMoveCheck(playerMoveSpeed, result.z) ||
                PlayerDownMoveCheck(playerMoveSpeed, result.z))
            {
                //���ł��������ɔ��]
                result.z = -result.z;
            }
        }
        //==========================*/

        //���˂���ۂ̉����Đ�
        sound.PlaySE(SEType.Reflect);
        //���x�𔽉f
        rb.velocity = result;
        ballDir = rb.velocity;
    }

    /*//==========================
    //�㋉�Ҍ���
    //==========================
    /// <summary>
    /// �v���C���[����Ɉړ����Ă��邩�{�[�������Ɉړ����Ă���ꍇ
    /// </summary>
    /// <param name="player_speed"></param>
    /// <param name="ball_dir"></param>
    /// <returns></returns>
    bool PlayerUpMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed > 0 && ball_dir < 0;
    }

    /// <summary>
    /// �v���C���[���ɉ��ړ����Ă��邩�{�[������Ɉړ����Ă���ꍇ
    /// </summary>
    /// <param name="player_speed"></param>
    /// <param name="ball_dir"></param>
    /// <returns></returns>
    bool PlayerDownMoveCheck(float player_speed, float ball_dir)
    {
        return player_speed < 0 && 0 < ball_dir;
    }*/
}
