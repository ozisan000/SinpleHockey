using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerInput playerInput;        //�v���C���[�̓��͂������ǂ�N���X

    const string jumpActName = "Jump";
    const string slidingActName = "Sliding";
    const string startActName = "Start";
    const string pauseActName = "Pause";

    InputAction jumpAct;            //�W�����v�̓���
    InputAction slidingAct;         //�X���C�f�B���O�̓���
    InputAction startAct;           //�X�^�[�g���ɉ����{�^��
    InputAction pauseAct;           //

    public bool JumpInput { get => jumpAct.IsPressed(); }       //�{�^�����������Ƃ�
    public bool SlidingInput { get => slidingAct.IsPressed(); } //�{�^����������Ă���Ƃ�
    public bool StartInput { get => startAct.triggered; }       //�X�^�[�g���̓���

    public bool PauseInput { get=>pauseAct.triggered; }

    public void MyAwake()
    {
        //�v���C���[�C���v�b�g���擾
        playerInput = GetComponent<PlayerInput>();

        //�A�N�V�����}�b�v����C���v�b�g�A�N�V�����i�o�C���h���ꂽ���́j���擾
        jumpAct = playerInput.currentActionMap[jumpActName];
        slidingAct = playerInput.currentActionMap[slidingActName];
        startAct = playerInput.currentActionMap[startActName];
        pauseAct = playerInput.currentActionMap[pauseActName];
    }
}
