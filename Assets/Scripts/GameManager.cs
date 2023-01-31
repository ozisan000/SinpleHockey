using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Sound sound;
    [SerializeField]
    CameraManager cameraManager;
    [SerializeField]
    Input input;

    [SerializeField]
    Ball ball;
    [SerializeField]
    Vector3 startBallPos;

    const string goal1Name = "Goal1";
    const string goal2Name = "Goal2";

    [SerializeField]
    Player player1;
    [SerializeField]
    Vector3 startPlayer1Pos;
    int player1Point = 0;

    [SerializeField]
    Player player2;
    [SerializeField]
    Vector3 startPlayer2Pos;
    int player2Point = 0;

    [SerializeField, Range(1, 100)]
    int pointMax = 1;
    [SerializeField]
    int eventID = 0;

    void Init()
    {
        player1.Init(sound);
        player2.Init(sound);
        ball.Init(PutItInTheGoal);
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Init();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (eventID)
        {
            case 0:
                GameStart();
                break;
            case 1:
                PlayerInputUpdate();
                break;
            case 2:
                Result();
                break;
        }
    }

    void GameStart()
    {
        if (input.StartGameInput)
        {
            //サウンド再生
            eventID++;
            ball.Move();
        }
        Debug.Log("GameStart");
    }

    void CheckPoint()
    {
        if( pointMax <= player2Point || pointMax <= player1Point)
        {
            eventID++;
        }
        else
        {
            eventID--;
        }
    }

    void PlayerInputUpdate()
    {
        player1.InputKey(input.Player1Input);
        player2.InputKey(input.Player2Input);
    }

    void Result()
    {
        //UI表示
        Debug.Log("Result");
    }

    void PutItInTheGoal(Collider collider)
    {
        if (collider.tag == goal1Name)
        {
            player2Point++;
            ResetSetGame();
        }
        else if (collider.tag == goal2Name)
        {
            player1Point++;
            ResetSetGame();
        }
    }

    void ResetSetGame()
    {
        ball.StopMove();
        //ボールの座標を変更
        ball.gameObject.transform.position = startBallPos;

        //プレイヤーの座標を変更
        player1.transform.position = startPlayer1Pos;
        player2.transform.position = startPlayer2Pos;
        CheckPoint();
        Debug.Log("1:" + player1Point + " " + "2:" + player2Point);
    }
}
