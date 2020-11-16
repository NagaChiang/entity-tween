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

    public struct EaseDesc
    {
        public static readonly EaseDesc Linear = new EaseDesc(EaseType.Linear, 0);
        public static readonly EaseDesc SmoothStart = new EaseDesc(EaseType.SmoothStart, 2);
        public static readonly EaseDesc SmoothStop = new EaseDesc(EaseType.SmoothStop, 2);
        public static readonly EaseDesc SmoothStep = new EaseDesc(EaseType.SmoothStep, 2);

        public EaseType Type;
        public int Exponent;

        public EaseDesc(in EaseType type, in int exponent)
        {
            Type = type;
            Exponent = exponent;
        }
    }

    public static class Ease
    {
        public delegate float EaseFunction(in float t);

        public static float SmoothStart(in float t, in int exponent)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= t;
            }

            return product;
        }

        public static float SmoothStop(in float t, in int exponent)
        {
            float product = 1;
            for (int n = 0; n < exponent; n++)
            {
                product *= (1 - t);
            }

            return 1 - product;
        }

        public static float SmoothStep(in float t, in int exponent)
        {
            return math.lerp(SmoothStart(t, exponent), SmoothStop(t, exponent), t);
        }

        public static float Crossfade(in EaseFunction easeA, in EaseFunction easeB, in float t)
        {
            return (easeA(t) * (1 - t)) + (easeB(t) * t);
        }

        public static float CalculatePercentage(in float t, in EaseType type, in int exponent)
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