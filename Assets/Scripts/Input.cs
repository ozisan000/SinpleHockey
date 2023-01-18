using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerInput playerInput;        //プレイヤーの入力をつかさどるクラス

    const string jumpActName = "Jump";
    const string slidingActName = "Sliding";
    const string startActName = "Start";
    const string pauseActName = "Pause";

    InputAction jumpAct;            //ジャンプの入力
    InputAction slidingAct;         //スライディングの入力
    InputAction startAct;           //スタート時に押すボタン
    InputAction pauseAct;           //

    public bool JumpInput { get => jumpAct.IsPressed(); }       //ボタンを押したとき
    public bool SlidingInput { get => slidingAct.IsPressed(); } //ボタンが押されているとき
    public bool StartInput { get => startAct.triggered; }       //スタート時の入力

    public bool PauseInput { get=>pauseAct.triggered; }

    public void MyAwake()
    {
        //プレイヤーインプットを取得
        playerInput = GetComponent<PlayerInput>();

        //アクションマップからインプットアクション（バインドされた入力）を取得
        jumpAct = playerInput.currentActionMap[jumpActName];
        slidingAct = playerInput.currentActionMap[slidingActName];
        startAct = playerInput.currentActionMap[startActName];
        pauseAct = playerInput.currentActionMap[pauseActName];
    }
}
