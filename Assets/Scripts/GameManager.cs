using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum JOIN_PLAYER
{
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Sound soundManager;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    CameraManager cameraManager;
    [SerializeField]
    Input inputManager;

    [SerializeField]
    GameObject ball;
    [SerializeField]
    Transform startBallPos;

    [SerializeField]
    Player player1;
    [SerializeField]
    Transform startPlayer1Pos;
    [SerializeField]
    CollisionHandler goal1;
    int player1Point = 0;

    [SerializeField]
    Player player2;
    [SerializeField]
    Transform startPlayer2Pos;
    [SerializeField]
    CollisionHandler goal2;
    int player2Point = 0;

    [SerializeField, Range(1, 100)]
    int pointMax = 1;
    [SerializeField,Range(5.0f,999.0f)]
    float timeLimit = 30.0f;

    int flag = 0;

    float time = 0.0f;
    float timeBuffer = 0.0f;

    float startUITime = 0.0f;

    private bool onTimeSEFlag = true;

    private bool resultFlag = false;

    void Init()
    {
        time = timeLimit;
        player1.Init(inputManager, soundManager, JOIN_PLAYER.Player1);
        player2.Init(inputManager, soundManager, JOIN_PLAYER.Player2);
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager.Init();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (0 <= flag)
            TimerUpdate();
        uiManager.UIUpdate(flag);
        switch (flag)
        {
            case -1:

                break;
            case 0:
                GameStart();
                break;
            case 1:
                CheckGoToResult();
                break;
            case 2:
                Result();
                break;
        }
    }

    void TimerUpdate()
    {
        if (!resultFlag)
        {
            time -= Time.deltaTime;
        }

        if (timeBuffer != time)
        {
            timeBuffer = time;
        }
    }

    void GameStart()
    {
        if (time > timeLimit - startUITime)
        {
            //eventTextData.text = startText;

            if (onTimeSEFlag)
            {
                onTimeSEFlag = false;
                //サウンド再生
            }
        }
        else
        {
            flag++;
            onTimeSEFlag = true;
        }
    }

    void CheckGoToResult()
    {
        if (CheckTimerOver()|| CheckPoint())
            flag++;
    }

    bool CheckTimerOver()
    {
        if (time <= 0)
        {
            time = 0;
            return true;
        }
        return false;
    }

    bool CheckPoint(JOIN_PLAYER player)
    {
        if (player == JOIN_PLAYER.Player1)
        {
            return pointMax <= player1Point;
        }
        else
        {
            return pointMax <= player2Point;
        }
    }

    bool CheckPoint()
    {
        return pointMax <= player2Point || pointMax <= player1Point;
    }

    void Result()
    {
        //UI表示
    }

    void GameClear()
    {
        //eventTextData.text = goalText;
        resultFlag = true;
        //EndGameSE(goalSE);
    }

    void GameOver()
    {
        //eventTextData.text = gameOverText;
        resultFlag = true;
        //EndGameSE(gameOverSE);
    }
}
