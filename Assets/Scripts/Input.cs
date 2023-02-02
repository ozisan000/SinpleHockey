using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerInput playerInput;        //�v���C���[�̓��͂������ǂ�N���X

    const string player1MoveName = "Player1Move";
    const string player2MoveName = "Player2Move";
    const string startGameName = "StartGame";
    const string resetGameName = "Reset";

    InputAction player1Act;            //�W�����v�̓���
    InputAction player2Act;            //�X���C�f�B���O�̓���
    InputAction startGameAct;          //
    InputAction resetGameAct;

    public float Player1Input { get => player1Act.ReadValue<Vector2>().y; }       //�{�^�����������Ƃ�
    public float Player2Input { get => player2Act.ReadValue<Vector2>().y; } //�{�^����������Ă���Ƃ�

    public bool StartGameTrigger { get => startGameAct.triggered; }

    public bool ResetGameTrigger { get => resetGameAct.triggered; }

    public void Init()
    {
        //�v���C���[�C���v�b�g���擾
        playerInput = GetComponent<PlayerInput>();

        //�A�N�V�����}�b�v����C���v�b�g�A�N�V�����i�o�C���h���ꂽ���́j���擾
        player1Act = playerInput.currentActionMap[player1MoveName];
        player2Act = playerInput.currentActionMap[player2MoveName];
        startGameAct = playerInput.currentActionMap[startGameName];
        resetGameAct = playerInput.currentActionMap[resetGameName];
    }

}
