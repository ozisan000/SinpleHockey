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
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// �C���v�b�g�}�l�[�W���[����L�[���͏���������Ă���i�ǂ̃{�^���������ꂽ�����ׂ�j�v���O����
    /// </summary>
    /// <param name="value"></param>
    public void InputKey(float value)
    {
        inputKeyValue = value;
    }


    /// <summary>
    /// �v���C���[���~�߂�v���O����
    /// </summary>
    public void StopMove()
    {
        rb.velocity = Vector3.zero;
        inputKeyValue = 0.0f;
    }


    /// <summary>
    /// �v���C���[�̒��g�̏����X�V����v���O����
    /// </summary>
    private void Update()
    {
        rb.velocity = Vector3.forward * inputKeyValue * speed;
    }
}
