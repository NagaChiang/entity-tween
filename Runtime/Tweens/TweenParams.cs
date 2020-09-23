using Timespawn.EntityTween.Math;

namespace Timespawn.EntityTween
{
    public struct TweenParams
    {
        public EaseType EaseType;
        public byte EaseExponent;
        public float Duration;
        public bool IsPingPong;
        public byte LoopCount;

        public TweenParams(EaseType easeType, int easeExponent, float duration, bool isPingPong, int loopCount)
        {
            EaseType = easeType;
            EaseExponent = (byte) easeExponent;
            Duration = duration;
            IsPingPong = isPingPong;
            LoopCount = (byte) loopCount;
        }
    }
}