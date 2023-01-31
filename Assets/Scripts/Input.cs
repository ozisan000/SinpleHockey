using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerInput playerInput;        //プレイヤーの入力をつかさどるクラス

    const string player1MoveName = "Player1Move";
    const string player2MoveName = "Player2Move";
    const string startGameName = "StartGame";

    InputAction player1Act;            //ジャンプの入力
    InputAction player2Act;            //スライディングの入力
    InputAction startGameAct;           //

    public float Player1Input { get => player1Act.ReadValue<Vector2>().y; }       //ボタンを押したとき
    public float Player2Input { get => player2Act.ReadValue<Vector2>().y; } //ボタンが押されているとき

    public bool StartGameInput { get => startGameAct.triggered; }

    public void Init()
    {
        //プレイヤーインプットを取得
        playerInput = GetComponent<PlayerInput>();

        //アクションマップからインプットアクション（バインドされた入力）を取得
        player1Act = playerInput.currentActionMap[player1MoveName];
        player2Act = playerInput.currentActionMap[player2MoveName];
        startGameAct = playerInput.currentActionMap[startGameName];
    }

    public void Awake()
    {
        Init();
    }
}
