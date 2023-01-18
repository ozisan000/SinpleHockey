using System;
using System.Collections;
using UnityEngine;

public static class UILib
{
    /// <summary>
    /// サイズを変更
    /// </summary>
    /// <param name="scaleTransform"></param>
    /// <param name="popupflag"></param>
    /// <param name="target_scale"></param>
    /// <param name="speed"></param>
    /// <param name="finishAct"></param>
    /// <returns></returns>
    public static IEnumerator WaitScalerProgress(
        RectTransform scaleTransform,
        bool popupflag,
        Vector3 target_scale,
        float speed,
        Action finishAct
    )
    {
        Func<bool> checkValueFunc;
        if (popupflag)
        {
            checkValueFunc = () =>
            {
                scaleTransform.localScale = new Vector3(
                    scaleTransform.localScale.x + speed * Time.deltaTime,
                    scaleTransform.localScale.y + speed * Time.deltaTime,
                    scaleTransform.localScale.z + speed * Time.deltaTime
                    );
                if (
                scaleTransform.localScale.x >= target_scale.x &&
                scaleTransform.localScale.x >= target_scale.y &&
                scaleTransform.localScale.x >= target_scale.z
                )
                {
                    scaleTransform.localScale = target_scale;
                    return true;
                }
                return false;
            };
        }
        else
        {
            checkValueFunc = () =>
            {
                scaleTransform.localScale = new Vector3(
                scaleTransform.localScale.x - speed * Time.deltaTime,
                scaleTransform.localScale.y - speed * Time.deltaTime,
                scaleTransform.localScale.z - speed * Time.deltaTime
                );
                if (
                scaleTransform.localScale.x <= target_scale.x &&
                scaleTransform.localScale.x <= target_scale.y &&
                scaleTransform.localScale.x <= target_scale.z)
                {
                    scaleTransform.localScale = target_scale;
                    return true;
                }
                return false;
            };
        }

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }

    public static IEnumerator WaitScalerProgress(
    RectTransform scaleTransform,
    EasingType type,
    Vector3 target_scale,
    float speed,
    Action finishAct
)
    {
        float time = 0.0f;
        Vector3 startCacheScale = scaleTransform.localScale;    //開始時キャッシュされるアルファ

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;

            scaleTransform.localScale = new Vector3(
                MathGeneral.Easing(type, startCacheScale.x, target_scale.x, time),
                MathGeneral.Easing(type, startCacheScale.y, target_scale.y, time),
                MathGeneral.Easing(type, startCacheScale.z, target_scale.z, time)
                );

            if (
                scaleTransform.localScale.x == target_scale.x &&
                scaleTransform.localScale.y == target_scale.y &&
                scaleTransform.localScale.z == target_scale.z)
            {
                scaleTransform.localScale = target_scale;
                return true;
            }

            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// フェードインフェードアウト
    /// </summary>
    /// <param name="group"></param>
    /// <param name="inflag"></param>
    /// <param name="targetvalue"></param>
    /// <param name="speed"></param>
    /// <param name="finishAct"></param>
    /// <returns></returns>
    public static IEnumerator WaitGroupFadeProgress(
        CanvasGroup group,
        bool inflag,
        float targetvalue,
        float speed,
        Action finishAct
    )
    {
        Func<bool> checkValueFunc;
        if (inflag)
        {
            checkValueFunc = () =>
            {
                group.alpha += speed * Time.deltaTime;
                if (group.alpha >= targetvalue)
                {
                    group.alpha = targetvalue;
                    return true;
                }
                return false;
            };
        }
        else
        {
            checkValueFunc = () =>
            {
                group.alpha -= speed * Time.deltaTime;
                if (group.alpha <= targetvalue)
                {
                    group.alpha = targetvalue;
                    return true;
                }
                return false;
            };
        }

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// フェードインフェードアウト(イージング仕様)
    /// </summary>
    /// <param name="group"></param>
    /// <param name="easing_type"></param>
    /// <param name="target_alpha"></param>
    /// <param name="speed"></param>
    /// <param name="finishAct"></param>
    /// <returns></returns>
    public static IEnumerator WaitGroupFadeProgress(
    CanvasGroup group,
    EasingType easing_type,
    float target_alpha,
    float speed,
    Action finishAct
)
    {
        float time = 0.0f;
        float startCacheAlpha = group.alpha;    //開始時キャッシュされるアルファ

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //フェードイージング計算
            group.alpha = MathGeneral.Easing(easing_type, startCacheAlpha, target_alpha, time);

            //ターゲットの値に到達した場合終了
            if (group.alpha == target_alpha)
            {
                group.alpha = target_alpha;
                return true;
            }
            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// フェードインフェードアウト(イージング仕様)
    /// </summary>
    /// <param name="group"></param>
    /// <param name="easing_type"></param>
    /// <param name="target_alpha"></param>
    /// <param name="speed"></param>
    /// <param name="finishact"></param>
    /// <returns></returns>
    public static IEnumerator WaitGroupFadeProgress(
    CanvasGroup group,
    EasingType easing_type,
    float target_alpha,
    float speed,
    Func<bool> skipact,
    Action finishact
        )
    {
        float time = 0.0f;
        float startCacheAlpha = group.alpha;    //開始時キャッシュされるアルファ

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //フェードイージング計算
            group.alpha = MathGeneral.Easing(easing_type, startCacheAlpha, target_alpha, time);

            //ターゲットの値に到達した場合&&外部からのアクションがあった場合終了
            if (group.alpha == target_alpha || skipact())
            {
                group.alpha = target_alpha;
                return true;
            }
            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishact?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// UIの座標を変更(イージング仕様)
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="easing_type"></param>
    /// <param name="target_vector"></param>
    /// <param name="speed"></param>
    /// <param name="finishAct"></param>
    /// <returns></returns>
    public static IEnumerator WaitMoveUIProgress(
        RectTransform trans,
        EasingType easing_type,
        Vector2 target_vector,
        float speed,
        Action finishAct
    )
    {
        float time = 0.0f;
        Vector2 startCacheVector = trans.localPosition;   //開始時キャッシュされる座標
        Vector2 easingVector = Vector2.zero;             //イージング計算用Vector2

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //Xの計算
            easingVector.x = MathGeneral.Easing(easing_type, startCacheVector.x, target_vector.x, time);
            //Yの計算
            easingVector.y = MathGeneral.Easing(easing_type, startCacheVector.y, target_vector.y, time);
            //ポジションに代入
            trans.localPosition = easingVector;

            //ターゲット座標に到達した場合終了
            if (trans.localPosition.x == target_vector.x &&
            trans.localPosition.y == target_vector.y
            )
            {
                trans.localPosition = target_vector;
                return true;
            }
            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }

    /// <summary>
    /// UIの座標を変更(イージング仕様)
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="easing_type"></param>
    /// <param name="target_vector"></param>
    /// <param name="speed"></param>
    /// <param name="finishAct"></param>
    /// <returns></returns>
    public static IEnumerator WaitMoveUIProgress(
        RectTransform trans,
        EasingType easing_type,
        Vector2 target_vector,
        float speed,
        Func<bool> skipact,
        Action finishAct
    )
    {
        float time = 0.0f;
        Vector2 startCacheVector = trans.localPosition;   //開始時キャッシュされる座標
        Vector2 easingVector = Vector2.zero;             //イージング計算用Vector2

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //Xの計算
            easingVector.x = MathGeneral.Easing(easing_type, startCacheVector.x, target_vector.x, time);
            //Yの計算
            easingVector.y = MathGeneral.Easing(easing_type, startCacheVector.y, target_vector.y, time);
            //ポジションに代入
            trans.localPosition = easingVector;

            //ターゲット座標に到達した場合&&スキップされたか
            if ((trans.localPosition.x == target_vector.x &&
            trans.localPosition.y == target_vector.y) ||
            skipact()
            )
            {
                trans.localPosition = target_vector;
                return true;
            }
            return false;
        };

        while (true)
        {
            yield return null;
            if (checkValueFunc())
            {
                finishAct?.Invoke();
                break;
            }
        }
    }
}
