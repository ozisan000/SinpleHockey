using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;                //�v���C���[�̃X�s�[�h
    float inputKeyValue = 0.0f;
    Rigidbody rb;


    /// <summary>
    /// �v���C���[������������v���O�����i�������������s���j
    /// </summary>
    public void Init()
    {
        //�Q�[���I�u�W�F�N�g���畨���v�Z�v���O�������擾
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// �C���v�b�g�}�l�[�W���[����L�[���͏���������Ă���i�ǂ̃{�^���������ꂽ�����ׂ�j�v���O����
    /// </summary>
    /// <param name="value"></param>
    public void InputKey(float value)
    {
        //�O������̓��͂��v���O�������Ɉꎞ�ۑ�
        inputKeyValue = value;
    }


    /// <summary>
    /// �v���C���[���~�߂�v���O����
    /// </summary>
    public void StopMove()
    {
        //�v���C���[�̑��x��0��
        rb.velocity = Vector3.zero;
        //�ꎞ�ۑ�����Ă�����̓f�[�^��0��
        inputKeyValue = 0.0f;
    }


    /// <summary>
    /// �v���C���[�̒��g�̏����X�V����v���O����
    /// </summary>
    private void Update()
    {
        //�������Z�̑��x��
        //�v���C���[�̏㉺�̓������Œ� * �L�[�̏㉺���� * �X�s�[�h �𔽉f
        rb.velocity = Vector3.forward * inputKeyValue * speed;
    }
}
