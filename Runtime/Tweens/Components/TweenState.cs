using Timespawn.EntityTween.Math;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    public struct TweenState : IBufferElementData, ITweenId
    {
        public const byte LOOP_COUNT_INFINITE = 0;
        private const byte LOOP_COUNT_PENDING_DESTROY = byte.MaxValue;

        public int Id;
        public EaseType EaseType;
        public byte EaseExponent;
        public float Duration;
        public float Time;
        public float EasePercentage;
        public bool IsPingPong;
        public byte LoopCount;
        public bool IsReverting;

        internal TweenState(EaseType easeType, byte easeExponent, float duration, bool isPingPong, byte loopCount, double elapsedTime, int chunkIndex, int tweenInfoTypeIndex) : this()
        {
            EaseType = easeType;
            EaseExponent = easeExponent;
            Duration = duration;
            IsPingPong = isPingPong;
            LoopCount = loopCount;

            Id = GenerateId(elapsedTime, chunkIndex, tweenInfoTypeIndex);
        }

        internal TweenState(TweenParams tweenParams, double elapsedTime, int chunkIndex, int tweenInfoTypeIndex)
            : this(tweenParams.EaseType, tweenParams.EaseExponent, tweenParams.Duration, tweenParams.IsPingPong, tweenParams.LoopCount, elapsedTime, chunkIndex, tweenInfoTypeIndex)
        {
        }

        public float GetNormalizedTime()
        {
            float oneWayDuration = Duration;
            if (IsPingPong)
            {
                oneWayDuration /= 2.0f;
            }

            return math.clamp(Time / oneWayDuration, 0.0f, 1.0f);
        }

        public void SetTweenId(int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        internal bool IsPendingDestroy()
        {
            return LoopCount == LOOP_COUNT_PENDING_DESTROY;
        }

        internal void SetPendingDestroy()
        {
            LoopCount = LOOP_COUNT_PENDING_DESTROY;
        }

        private int GenerateId(double elapsedTime, int entityInQueryIndex, int tweenInfoTypeIndex)
        {
            unchecked
            {
                int hashCode = (int) EaseType;
                hashCode = (hashCode * 397) ^ EaseExponent.GetHashCode();
                hashCode = (hashCode * 397) ^ Duration.GetHashCode();
                hashCode = (hashCode * 397) ^ IsPingPong.GetHashCode();
                hashCode = (hashCode * 397) ^ LoopCount.GetHashCode();
                hashCode = (hashCode * 397) ^ elapsedTime.GetHashCode();
                hashCode = (hashCode * 397) ^ entityInQueryIndex;
                hashCode = (hashCode * 397) ^ tweenInfoTypeIndex;

                return hashCode;
            }
        }
    }
}