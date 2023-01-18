using Unity.Mathematics;

public enum EasingType
{
    IN_SINE,
    OUT_SINE,
    INOUT_SINE,
    IN_QUAD,
    OUT_QUAD,
    INOUT_QUAD,
    IN_CUBIC,
    OUT_CUBIC,
    INOUT_CUBIC,
    IN_QUART,
    OUT_QUART,
    INOUT_QUART,
    IN_QUINT,
    OUT_QUINT,
    INOUT_QUINT,
    IN_EXPO,
    OUT_EXPO,
    INOUT_EXPO,
    IN_CIRC,
    OUT_CIRC,
    INOUT_CIRC,
    IN_BACK,
    OUT_BACK,
    INOUT_BACK,
    IN_ELASTIC,
    OUT_ELASTIC,
    INOUT_ELASTIC,
    IN_BOUNCE,
    OUT_BOUNCE,
    INOUT_BOUNCE,
    LINEAR,
    NOTIME
};

public static class MathGeneral
{
    /// <summary>
    /// 一次ベジエ曲線
    /// </summary>
    /// <param name="p0">始点</param>
    /// <param name="p1">終点</param>
    /// <param name="t">0.0~1.0時間</param>
    /// <returns></returns>
    public static float SignleInterPolation(float p0, float p1, float t)
    {
        return ((1.0f - t) * p0) + (t * p1);
    }

    /// <summary>
    /// イージング
    /// </summary>
    /// <param name="type">イージングタイプ</param>
    /// <param name="p0">始点</param>
    /// <param name="p1">終点</param>
    /// <param name="t">0.0~1.0時間</param>
    /// <returns></returns>
    public static float Easing(EasingType type, float p0, float p1, float t)
    {
        float time = 0.0f;

        if (t < 0.0f)
        {
            t = 0.0f;
        }

        else if (1.0f < t)
        {
            t = 1.0f;
        }

        const float n1 = 7.5625f;
        const float d1 = 2.75f;
        const float c5 = (2.0f * math.PI) / 4.5f;
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        const float c3 = c1 + 1.0f;
        const float c4 = (2.0f * math.PI) / 3.0f;
        float bounceTime = 1.0f - t;

        float bounce = 0.0f;

        if (0.0f < t && t < 1.0f)
        {
            switch (type)
            {
                case EasingType.IN_SINE:
                    time = 1.0f - math.cos((t * math.PI) / 2.0f);
                    break;

                case EasingType.OUT_SINE:
                    time = math.cos((t * math.PI) / 2.0f);
                    break;

                case EasingType.INOUT_SINE:
                    time = -(math.cos(math.PI * t) - 1.0f) / 2.0f;
                    break;

                case EasingType.IN_QUAD:
                    time = t * t;
                    break;

                case EasingType.OUT_QUAD:
                    time = 1.0f - (1.0f - t) * (1.0f - t);
                    break;

                case EasingType.INOUT_QUAD:
                    if (t < 0.5f)
                    {
                        time = 2.0f * t * t;
                    }
                    else
                    {
                        time = 1.0f - math.pow(-2.0f * t + 2.0f, 2.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_CUBIC:
                    time = t * t * t;
                    break;

                case EasingType.OUT_CUBIC:
                    time = 1.0f - math.pow(1.0f - t, 3.0f);
                    break;

                case EasingType.INOUT_CUBIC:
                    time = 1.0f - math.pow(1.0f - t, 3.0f);
                    if (t < 0.5f)
                    {
                        time = 4.0f * t * t * t;
                    }
                    else
                    {
                        time = 1.0f - math.pow(-2.0f * t + 2.0f, 3.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_QUART:
                    time = t * t * t * t;
                    break;

                case EasingType.OUT_QUART:
                    time = 1.0f - math.pow(1.0f - t, 4.0f);
                    break;

                case EasingType.INOUT_QUART:
                    if (t < 0.5f)
                    {
                        time = 8.0f * t * t * t * t;
                    }
                    else
                    {
                        time = 1.0f - math.pow(-2.0f * t + 2.0f, 4.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_QUINT:
                    time = t * t * t * t * t;
                    break;

                case EasingType.OUT_QUINT:
                    time = 1.0f - math.pow(1.0f - t, 5.0f);
                    break;

                case EasingType.INOUT_QUINT:
                    if (t < 0.5f)
                    {
                        time = 16.0f * t * t * t * t * t;
                    }
                    else
                    {
                        time = 1.0f - math.pow(-2.0f * t + 2.0f, 5.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_EXPO:
                    if (t == 0.0f)
                    {
                        time = 0.0f;
                    }
                    else
                    {
                        time = math.pow(2.0f, 10.0f * t - 10.0f);
                    }
                    break;

                case EasingType.OUT_EXPO:
                    if (t == 1.0f)
                    {
                        time = 1.0f;
                    }
                    else
                    {
                        time = 1.0f - math.pow(2.0f, -10.0f * t);
                    }
                    break;

                case EasingType.INOUT_EXPO:
                    if (t == 1.0f)
                    {
                        time = 1.0f;
                    }
                    else if (t == 0.0f)
                    {
                        time = 0.0f;
                    }
                    else if (t < 0.5f)
                    {
                        time = math.pow(2.0f, 20.0f * t - 10.0f) / 2.0f;
                    }
                    else
                    {
                        time = (2.0f - math.pow(2.0f, -20.0f * t + 10.0f)) / 2.0f;
                    }
                    break;

                case EasingType.IN_CIRC:
                    time = 1.0f - math.sqrt(1.0f - math.pow(t, 2.0f));
                    break;

                case EasingType.OUT_CIRC:
                    time = math.sqrt(1.0f - math.pow(t - 1.0f, 2.0f));
                    break;

                case EasingType.INOUT_CIRC:
                    if (t < 0.5f)
                    {
                        time = (1.0f - math.sqrt(1.0f - math.pow(2.0f * t, 2.0f))) / 2.0f;
                    }
                    else
                    {
                        time = (math.sqrt(1.0f - math.pow(-2.0f * t + 2.0f, 2.0f)) + 1.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_BACK:
                    time = c3 * t * t * t - c1 * t * t;
                    break;

                case EasingType.OUT_BACK:
                    time = 1.0f + c3 * math.pow(t - 1.0f, 3.0f) + c1 * math.pow(t - 1.0f, 2.0f);
                    break;

                case EasingType.INOUT_BACK:
                    if (t < 0.5f)
                    {
                        time = (math.pow(2.0f * t, 2.0f) * ((c2 + 1.0f) * 2.0f * t - c2)) / 2.0f;
                    }
                    else
                    {
                        time = (math.pow(2.0f * t - 2.0f, 2.0f) * ((c2 + 1.0f) * (t * 2.0f - 2.0f) + c2) + 2.0f) / 2.0f;
                    }
                    break;

                case EasingType.IN_ELASTIC:
                    if (t == 0.0f)
                    {
                        time = 0.0f;
                        break;
                    }
                    else if (t == 1.0f)
                    {
                        time = 1.0f;
                    }
                    else
                    {
                        time = -math.pow(2.0f, 10.0f * t - 10.0f) * math.sin((t * 10.0f - 10.75f) * c4);
                    }
                    break;

                case EasingType.OUT_ELASTIC:
                    if (t == 0.0f)
                    {
                        time = 0.0f;
                        break;
                    }
                    else if (t == 1.0f)
                    {
                        time = 1.0f;
                    }
                    else
                    {
                        time = math.pow(2.0f, -10.0f * t) * math.sin((t * 10.0f - 0.75f) * c4) + 1.0f;
                    }
                    break;

                case EasingType.INOUT_ELASTIC:

                    if (t == 0.0f)
                    {
                        time = 0.0f;
                        break;
                    }
                    else if (t == 1.0f)
                    {
                        time = 1.0f;
                    }
                    else if (t < 0.5)
                    {
                        time = -(math.pow(2.0f, 20.0f * t - 10.0f) * math.sin((20.0f * t - 11.125f) * c5)) / 2.0f;
                    }
                    else
                    {
                        time = (math.pow(2.0f, -20.0f * t + 10.0f) * math.sin((20.0f * t - 11.125f) * c5)) / 2.0f + 1.0f;
                    }
                    break;

                case EasingType.IN_BOUNCE:

                    if (bounceTime < 1.0f / d1)
                    {
                        time = n1 * bounceTime * bounceTime;
                    }
                    else if (bounceTime < 2.0f / d1)
                    {
                        bounce = bounceTime - (1.5f / d1);
                        time = n1 * bounce * bounceTime + 0.75f;
                    }
                    else if (bounceTime < 2.5f / d1)
                    {
                        bounce = bounceTime - (2.25f / d1);
                        time = n1 * bounce * bounceTime + 0.9375f;
                    }
                    else
                    {
                        bounce = bounceTime - (2.625f / d1);
                        time = n1 * bounce * bounceTime + 0.984375f;
                    }

                    time = 1.0f - time;
                    break;

                case EasingType.OUT_BOUNCE:

                    if (t < 1.0f / d1)
                    {
                        time = n1 * t * t;
                    }
                    else if (t < 2.0f / d1)
                    {
                        bounce = t - (1.5f / d1);
                        time = n1 * bounce * t + 0.75f;
                    }
                    else if (t < 2.5f / d1)
                    {
                        bounce = t - (2.25f / d1);
                        time = n1 * bounce * t + 0.9375f;
                    }
                    else
                    {
                        bounce = t - (2.625f / d1);
                        time = n1 * bounce * t + 0.984375f;
                    }
                    break;
                case EasingType.INOUT_BOUNCE:

                    if (t < 0.5f)
                    {
                        bounceTime = 1.0f - 2.0f * t;
                    }
                    else
                    {
                        bounceTime = 2.0f * t - 1.0f;
                    }

                    if (t < 1.0f / d1)
                    {
                        time = n1 * t * t;
                    }
                    else if (t < 2.0f / d1)
                    {
                        bounce = t - (1.5f / d1);
                        time = n1 * bounce * t + 0.75f;
                    }
                    else if (t < 2.5f / d1)
                    {
                        bounce = t - (2.25f / d1);
                        time = n1 * bounce * t + 0.9375f;
                    }
                    else
                    {
                        bounce = t - (2.625f / d1);
                        time = n1 * bounce * t + 0.984375f;
                    }

                    if (t < 0.5f)
                    {
                        time = (1.0f - time) / 2.0f;
                    }
                    else
                    {
                        time = (1.0f + time) / 2.0f;
                    }

                    break;

                case EasingType.LINEAR:
                    time = t;
                    break;
                case EasingType.NOTIME:
                    time = t;
                    break;
            }
        }
        else
        {
            time = t;
        }

        return SignleInterPolation(p0, p1, time);
    }
}
