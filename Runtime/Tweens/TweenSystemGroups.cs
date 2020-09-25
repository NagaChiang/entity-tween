using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    // Update Order:
    // - TweenGenerateSystemGroup
    //   - TweenGenerateSystem(s)
    // - TweenEaseSystem
    // - TweenApplySystemGroup
    //   - TweenTranslationSystem
    //   - TweenRotationSystem
    //   - TweenScaleSystem
    //   - etc.
    // - TweenStateSyetem
    // - TweenResumeSystem
    // - TweenStopSystem
    // - TweenDestroySystemGroup
    //   - TweenDestroySystem(s)

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    internal class TweenSimulationSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateBefore(typeof(TweenEaseSystem))]
    internal class TweenGenerateSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenEaseSystem))]
    internal class TweenApplySystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal class TweenDestroySystemGroup : ComponentSystemGroup {}
}