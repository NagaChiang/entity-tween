namespace Timespawn.EntityTween.Tweens
{
    internal interface ITweenParams
    {
        void SetTweenParams(in TweenParams tweenParams);
        TweenParams GetTweenParams();
    }
}