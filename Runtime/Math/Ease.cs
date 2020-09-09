using Unity.Mathematics;

namespace Timespawn.EntityTween.Math
{
    public enum EaseType : byte
    {
        Linear,
        SmoothStart,
        SmoothStop,
        SmoothStep,
    }

    public static class Ease
    {
        public delegate float EaseFunction(float t);

        public static float SmoothStart(float t, int exponent)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= t;
            }

            return product;
        }

        public static float SmoothStop(float t, int exponent)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= (1 - t);
            }

            return 1 - product;
        }

        public static float SmoothStep(float t, int exponent)
        {
            return math.lerp(SmoothStart(t, exponent), SmoothStop(t, exponent), t);
        }

        public static float CrossFade(EaseFunction easeA, EaseFunction easeB, float t)
        {
            return (easeA(t) * (1 - t)) + (easeB(t) * t);
        }

        public static float CalculatePercentage(float t, EaseType type, int exponent)
        {
            switch (type)
            {
                case EaseType.Linear:
                    return t;
                case EaseType.SmoothStart:
                    return SmoothStart(t, exponent);
                case EaseType.SmoothStop:
                    return SmoothStop(t, exponent);
                case EaseType.SmoothStep:
                    return SmoothStep(t, exponent);
                default:
                    return t;
            }
        }
    }
}