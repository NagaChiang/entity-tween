using Timespawn.EntityTween.Math;

namespace Timespawn.EntityTween
{
    internal struct TweenParams
    {
        public EaseType EaseType;
        public byte EaseExponent;
        public float Duration;
        public bool IsPingPong;
        public byte LoopCount;

        public TweenParams(EaseType easeType, byte easeExponent, float duration, bool isPingPong, byte loopCount)
        {
            EaseType = easeType;
            EaseExponent = easeExponent;
            Duration = duration;
            IsPingPong = isPingPong;
            LoopCount = loopCount;
        }
    }
}