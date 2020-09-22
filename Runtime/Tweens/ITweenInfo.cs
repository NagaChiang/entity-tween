namespace Timespawn.EntityTween.Tweens
{
    internal interface ITweenInfo<T>
    {
        void SetTweenInfo(T start, T end);
        T GetTweenStart();
        T GetTweenEnd();
    }
}