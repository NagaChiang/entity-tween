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
        public float StartDelay;

        public TweenParams(
            in float duration, 
            in EaseType easeType = EaseType.Linear, 
            in int easeExponent = 0,
            in bool isPingPong = false, 
            in int loopCount = 1, 
            in float startDelay = 0.0f)
        {
            Duration = duration;
            EaseType = easeType;
            EaseExponent = (byte) easeExponent;
            IsPingPong = isPingPong;
            LoopCount = (byte) loopCount;
            StartDelay = startDelay;
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

            if (StartDelay > 0.0f)
            {
                msg += $", delayed {StartDelay} secs";
            }

            return msg;
        }
    }
}