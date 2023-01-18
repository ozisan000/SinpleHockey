using System;
using System.Collections;
using UnityEngine;

public static class UILib
{
    /// <summary>
    /// �T�C�Y��ύX
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
        Vector3 startCacheScale = scaleTransform.localScale;    //�J�n���L���b�V�������A���t�@

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
    /// �t�F�[�h�C���t�F�[�h�A�E�g
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
    /// �t�F�[�h�C���t�F�[�h�A�E�g(�C�[�W���O�d�l)
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
        float startCacheAlpha = group.alpha;    //�J�n���L���b�V�������A���t�@

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //�t�F�[�h�C�[�W���O�v�Z
            group.alpha = MathGeneral.Easing(easing_type, startCacheAlpha, target_alpha, time);

            //�^�[�Q�b�g�̒l�ɓ��B�����ꍇ�I��
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
    /// �t�F�[�h�C���t�F�[�h�A�E�g(�C�[�W���O�d�l)
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
        float startCacheAlpha = group.alpha;    //�J�n���L���b�V�������A���t�@

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //�t�F�[�h�C�[�W���O�v�Z
            group.alpha = MathGeneral.Easing(easing_type, startCacheAlpha, target_alpha, time);

            //�^�[�Q�b�g�̒l�ɓ��B�����ꍇ&&�O������̃A�N�V�������������ꍇ�I��
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
    /// UI�̍��W��ύX(�C�[�W���O�d�l)
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
        Vector2 startCacheVector = trans.localPosition;   //�J�n���L���b�V���������W
        Vector2 easingVector = Vector2.zero;             //�C�[�W���O�v�Z�pVector2

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //X�̌v�Z
            easingVector.x = MathGeneral.Easing(easing_type, startCacheVector.x, target_vector.x, time);
            //Y�̌v�Z
            easingVector.y = MathGeneral.Easing(easing_type, startCacheVector.y, target_vector.y, time);
            //�|�W�V�����ɑ��
            trans.localPosition = easingVector;

            //�^�[�Q�b�g���W�ɓ��B�����ꍇ�I��
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
    /// UI�̍��W��ύX(�C�[�W���O�d�l)
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
        Vector2 startCacheVector = trans.localPosition;   //�J�n���L���b�V���������W
        Vector2 easingVector = Vector2.zero;             //�C�[�W���O�v�Z�pVector2

        Func<bool> checkValueFunc = () =>
        {
            time += speed * Time.deltaTime;
            //X�̌v�Z
            easingVector.x = MathGeneral.Easing(easing_type, startCacheVector.x, target_vector.x, time);
            //Y�̌v�Z
            easingVector.y = MathGeneral.Easing(easing_type, startCacheVector.y, target_vector.y, time);
            //�|�W�V�����ɑ��
            trans.localPosition = easingVector;

            //�^�[�Q�b�g���W�ɓ��B�����ꍇ&&�X�L�b�v���ꂽ��
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
