using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject goal;

    [SerializeField]
    AudioClip bgm;
    [SerializeField]
    AudioClip goalSE;
    [SerializeField]
    AudioClip startSE;
    [SerializeField]
    AudioClip gameOverSE;

    [SerializeField]
    GameObject player;

    [SerializeField]
    TextMeshProUGUI eventTextData;
    [SerializeField]
    TextMeshProUGUI timeTextData;
    [SerializeField]
    string startText;
    [SerializeField]
    string goalText;
    [SerializeField]
    string gameOverText;
    [SerializeField]
    float startTextTime = 1.0f;
    [SerializeField,Range(5.0f,999.0f)]
    float limit = 30.0f;

    int flag = 0;

    private float time = 0.0f;
    private float timeBuffer = 0.0f;

    private bool onTimeSEFlag = true;

    private bool inputFlag = false;

    private bool endFlag = false;

    public float GameTimer { get => time; }
    public bool InputFlag { get => inputFlag; }

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //スタートの座標に移動
        player.transform.position = startPos.transform.position;
        audioSource = GetComponent<AudioSource>();
        time = limit;
    }

    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
        switch (flag)
        {
            case 0:
                GameStart();
                break;
            case 1:
                CheckEvent();
                break;
            case 2:
                GameClear();
                break;
            case -1:
                GameOver();
                break;
        }
    }

    void TimerUpdate()
    {
        if (!endFlag)
        {
            time -= Time.deltaTime;
        }

        if (timeBuffer != time)
        {
            timeBuffer = time;
            timeTextData.text = ((int)time).ToString();
        }
    }

    void GameStart()
    {
        if (time > limit - startTextTime)
        {
            eventTextData.text = startText;

            if (onTimeSEFlag)
            {
                onTimeSEFlag = false;
                audioSource.PlayOneShot(startSE);
                audioSource.loop = true;
                audioSource.PlayOneShot(bgm);
            }
        }
        else
        {
            flag++;
            eventTextData.text = "";
            inputFlag = true;
            onTimeSEFlag = true;
        }
    }

    void CheckEvent()
    {
        if (HPOverEvent() || TimerOverEvent())
            flag = -1;
        if (GoalEvent())
            flag++;
    }

    bool HPOverEvent()
    {
        //if (status.HP <= 0)
        //    return true;
        return false;
    }

    bool TimerOverEvent()
    {
        if (time <= 0)
        {
            time = 0;
            return true;
        }
        return false;
    }

    bool GoalEvent()
    {
        //if (status.GoalFlag)
        //    return true;
        return false;
    }

    void GameClear()
    {
        eventTextData.text = goalText;
        inputFlag = false;
        endFlag = true;
        EndGameSE(goalSE);
    }

    void GameOver()
    {
        eventTextData.text = gameOverText;
        inputFlag = false;
        endFlag = true;
        EndGameSE(gameOverSE);
    }

    void EndGameSE(AudioClip se)
    {
        if (onTimeSEFlag)
        {
            audioSource.Stop();
            audioSource.loop = false;
            onTimeSEFlag = false;
            audioSource.PlayOneShot(se);
        }
    }
}
