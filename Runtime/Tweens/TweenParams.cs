using Timespawn.EntityTween.Math;

namespace Timespawn.EntityTween
{
    public struct TweenParams
    {
        public float Duration;
        public EaseType EaseType;
        public byte EaseExponent;
        public bool IsPingPong;
        public byte LoopCount;

        public TweenParams(float duration, EaseType easeType = EaseType.Linear, int easeExponent = 0, bool isPingPong = false, int loopCount = 1)
        {
            Duration = duration;
            EaseType = easeType;
            EaseExponent = (byte) easeExponent;
            IsPingPong = isPingPong;
            LoopCount = (byte) loopCount;
        }

        public override string ToString()
        {
            string msg = $"{Duration} secs";

            if (EaseType != EaseType.Linear || EaseExponent != 0)
            {
                msg += $", {EaseType} ({EaseExponent})";
            }

            if (IsPingPong)
            {
                msg += ", pingpong";
            }

            if (LoopCount != 1)
            {
                msg += LoopCount == 0 ? ", infinite" : $", {LoopCount} times";
            }

            return msg;
        }
    }
}