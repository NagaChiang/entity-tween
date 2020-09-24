using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    // Update Order:
    // - TweenGenerationSystemGroup
    //   - TweenGenerationSystem(s)
    // - TweenEaseSystem
    // - TweenApplySystemGroup
    //   - TweenTranslationSystem
    //   - etc.
    // - TweenResumeSystem
    // - TweenDestroySystemGroup
    //   - TweenDestroySystem(s)

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    internal class TweenSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateBefore(typeof(TweenEaseSystem))]
    internal class TweenGenerationSystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenEaseSystem))]
    internal class TweenApplySystemGroup : ComponentSystemGroup {}

    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal class TweenDestroySystemGroup : ComponentSystemGroup {}
}