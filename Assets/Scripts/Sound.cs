using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEType
{
    Reflect,
    Goal
}

public enum BGMType
{
    None,
}

public class Sound : MonoBehaviour
{
    [System.Serializable]
    public struct BGMData
    {
        [SerializeField]
        BGMType eventType;
        public BGMType EventType { get => eventType; }

        [SerializeField]
        AudioClip bgmClip;
        public AudioClip BGMClip { get => bgmClip; }
    }

    [System.Serializable]
    public struct SEData
    {
        [SerializeField]
        SEType seType;
        public SEType SEType { get => seType; }
        [SerializeField]
        AudioClip seClip;
        public AudioClip SEClip { get => seClip; }
    }

    [SerializeField, Range(0.0f, 1.0f)]
    float bgmVolume = 0.2f;
    [SerializeField, Range(0.0f, 1.0f)]
    float seVolume = 0.3f;
    [SerializeField]
    float bgmFadeSpeed = 2.0f;
    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource seSource;
    [SerializeField]
    List<BGMData> bgmList;
    [SerializeField]
    List<SEData> seList;

    Dictionary<BGMType, AudioClip> bgmDic = new Dictionary<BGMType, AudioClip>();
    Dictionary<SEType, AudioClip> seDic = new Dictionary<SEType, AudioClip>();

    bool isStopFadeOutBGM = false;

    public float BGMVolume
    {
        get { return bgmVolume; }
        set
        {
            if (1.0f < value)
            {
                bgmVolume = 1.0f;
            }
            else if (value < 0.0f)
            {
                bgmVolume = 0.0f;
            }
            else
            {
                bgmVolume = value;
            }
            bgmSource.volume = bgmVolume;
        }
    }

    public float SEVolume
    {
        get { return seVolume; }
        set
        {
            if (1.0f < value)
            {
                seVolume = 1.0f;
            }
            else if (value < 0.0f)
            {
                seVolume = 0.0f;
            }
            else
            {
                seVolume = value;
            }
            seSource.volume = seVolume;
        }
    }

    public void PlayBGM(BGMType type)
    {
        bgmSource.clip = bgmDic[type];
        bgmSource.Play();
    }

    public void ChangeBGM(
        BGMType type,
        EasingType easing_type,
        float speed = 0.0f)
    {
        StopFadeOutBGM(
            easing_type,
            speed,
            () =>
            {
                PlayFadeInBGM(
                    type,
                    easing_type,
                    speed
                    );
            }
            );
    }

    public void PlayFadeInBGM(
            BGMType type,
            EasingType easing_type,
        float speed = 0.0f)
    {
        if (isStopFadeOutBGM) return;
        if (speed <= 0.0f)
            speed = bgmFadeSpeed;

        bgmSource.volume = 0.0f;
        PlayBGM(type);

        StartCoroutine(
            FadeVolume(
                easing_type,
                speed,
                bgmVolume,
                bgmSource,
                () => { return isStopFadeOutBGM; },
                null
                ));
    }

    public void StopFadeOutBGM(
            EasingType easing_type,
        float speed = 0.0f,
        Action finish_act = null
        )
    {
        if (speed <= 0.0f)
            speed = bgmFadeSpeed;
        isStopFadeOutBGM = true;
        StartCoroutine(
            FadeVolume(
                easing_type,
                speed,
                0.0f,
                bgmSource,
                () => { return false; },
                () =>
                {
                    bgmSource.Stop();
                    isStopFadeOutBGM = false;
                    finish_act?.Invoke();
                }
            ));
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(SEType type, float volume_scale = 1.0f)
    {
        seSource.PlayOneShot(seDic[type], volume_scale);
    }

    public void StopSE()
    {
        seSource.Stop();
    }

    private IEnumerator FadeVolume(
        EasingType easing_type,
        float speed,
        float target_volume,
        AudioSource source,
        Func<bool> break_act,
        Action finish_act
        )
    {
        float time = 0.0f;
        float cacheVolume = source.volume;    //開始時キャッシュされる音量

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //フェードイージング計算
            source.volume = MathGeneral.Easing(easing_type, cacheVolume, target_volume, time);

            //ターゲットの値に到達した場合&&外部からのアクションがあった場合終了
            if (source.volume == target_volume)
            {
                source.volume = target_volume;
                return true;
            }
            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finish_act?.Invoke();
                break;
            }else if (break_act())
            {
                break;
            }
        }
    }

    public void Init()
    {
        //リストから辞書を作成
        foreach(var data in bgmList)
        {
            bgmDic.Add(data.EventType, data.BGMClip);
        }
        foreach(var data in seList)
        {
            seDic.Add(data.SEType, data.SEClip);
        }

        bgmSource.volume = bgmVolume;
        seSource.volume = seVolume;
    }
}
