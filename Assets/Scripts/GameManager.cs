using TMPro;
using UnityEngine;

enum GameEvent
{
    Start,
    Main,
    Result
}

public class GameManager : MonoBehaviour
{

    [SerializeField, Range(1, 100),Header("GameSetting")]
    int pointMax = 1;
    [SerializeField]
    GameEvent eventID = GameEvent.Start;

    [SerializeField]
    Sound sound;
    [SerializeField]
    Input input;

    [SerializeField]
    Ball ball;
    [SerializeField]
    Vector3 startBallPos;

    [SerializeField]
    string winText = " Win!";

    bool allowInputFlag = false;

    const string goal1Name = "Goal1";
    const string goal2Name = "Goal2";

    [SerializeField]
    TextMeshPro startText;
    [SerializeField]
    TextMeshPro resultText;
    [SerializeField]
    TextMeshPro resultText2;

    [SerializeField, Header("Player1Setting")]
    Player player1;
    [SerializeField]
    Vector3 startPlayer1Pos;
    [SerializeField]
    TextMeshPro player1Text;
    [SerializeField]
    ParticleSystem goal1Effect;
    [SerializeField]
    Color player1Color;
    int player1Point = 0;
    const string player1Name = "Red";

    [SerializeField, Header("Player2Setting")]
    Player player2;
    [SerializeField]
    Vector3 startPlayer2Pos;
    [SerializeField]
    TextMeshPro player2Text;
    [SerializeField]
    ParticleSystem goal2Effect;
    [SerializeField]
    Color player2Color;
    int player2Point = 0;
    const string player2Name = "Blue";

    // Start is called before the first frame update
    void Start()
    {
        input.Init();
        sound.Init();
        player1.Init();
        player2.Init();
        ball.Init(sound);
        ball.CollisionHandler.triggerEnterEvent += PutItInTheGoal;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputUpdate();
        UpdateUI();
        switch (eventID)
        {
            case GameEvent.Start:
                GameStart();
                break;

            case GameEvent.Main:
                allowInputFlag = true;
                if (input.ResetGameTrigger)
                {
                    ResetSetGame();
                }
                break;

            case GameEvent.Result:
                Result();
                break;
        }
    }

    void UpdateUI()
    {
        player1Text.text = player1Name + ":" + player1Point;
        player2Text.text = player2Name + ":" + player2Point;
    }

    void GameStart()
    {
        if (input.StartGameTrigger)
        {
            eventID++;
            ball.Move();
            startText.alpha = 0.0f;
        }
        else
        {
            startText.alpha = 1.0f;
        }
        Debug.Log("GameStart");
    }

    void CheckPoint()
    {
        if( pointMax <= player2Point || pointMax <= player1Point)
        {
            eventID++;
            goal1Effect.Play();
            goal2Effect.Play();
        }
        else
        {
            eventID--;
        }
    }

    void PlayerInputUpdate()
    {
        if (allowInputFlag)
        {
            player1.InputKey(input.Player1Input);
            player2.InputKey(input.Player2Input);
        }
    }

    void Result()
    {
        Debug.Log("Result");
        if (input.StartGameTrigger)
        {
            ResetGame();
            resultText2.alpha = resultText.alpha = 0.0f;
        }
        else
        {
            resultText2.alpha = resultText.alpha = 1.0f;
            if (pointMax <= player1Point)
            {
                resultText.text = player1Name + winText;
                resultText.color = player1Color;
            }
            else
            {
                resultText.text = player2Name + winText;
                resultText.color = player2Color;
            }
        }
    }

    void ResetGame()
    {
        eventID = GameEvent.Start;
        player1Point = 0;
        player2Point = 0;
        UpdateUI();
    }

    void PutItInTheGoal(Collider collider)
    {
        if (collider.tag == goal1Name)
        {
            player2Point++;
            goal1Effect.Play();
            sound.PlaySE(SEType.Goal);
            ResetSetGame();
        }
        else if (collider.tag == goal2Name)
        {
            player1Point++;
            goal2Effect.Play();
            sound.PlaySE(SEType.Goal);
            ResetSetGame();
        }
    }

    void ResetSetGame()
    {
        ball.StopMove();
        //ボールの座標を変更
        ball.gameObject.transform.position = startBallPos;

        allowInputFlag = false;

        //プレイヤーの座標を変更
        player1.StopMove();
        player2.StopMove();

        player1.transform.position = startPlayer1Pos;
        player2.transform.position = startPlayer2Pos;


        CheckPoint();
        Debug.Log("1:" + player1Point + " " + "2:" + player2Point);
    }
}
