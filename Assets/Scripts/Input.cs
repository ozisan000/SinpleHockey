using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerInput playerInput;        //プレイヤーの入力をつかさどるクラス

    const string player1MoveName = "Player1Move";
    const string player2MoveName = "Player2Move";

    InputAction player1Act;            //ジャンプの入力
    InputAction player2Act;         //スライディングの入力

    public float Player1Input { get => player1Act.ReadValue<Vector2>().y; }       //ボタンを押したとき
    public float Player2Input { get => player2Act.ReadValue<Vector2>().y; } //ボタンが押されているとき

    public void Init()
    {
        //プレイヤーインプットを取得
        playerInput = GetComponent<PlayerInput>();

        //アクションマップからインプットアクション（バインドされた入力）を取得
        player1Act = playerInput.currentActionMap[player1MoveName];
        player2Act = playerInput.currentActionMap[player2MoveName];
    }

    public void Awake()
    {
        Init();
    }
}
