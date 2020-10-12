# Entity Tween

Entity compatible tween library for Unity ECS/DOTS.

## Demonstration

![](https://i.imgur.com/3GM0RCE.gif)

[Link to the larger gif](https://i.imgur.com/3oZnviK.gif)

### Configuration

- 70000 tweening entities
    - `Translation`
    - `Rotation`
    - `NonUniformScale`
- Burst
    - Leak Detection: Off
    - Safety Checks: Off
    - Synchronous Compilation: On
    - Jobs Debugger: Off
- GPU instancing

### Hardware

- Intel i7-8700 @ 3.2GHz
- NVIDIA GeForce GTX 1660 Ti

## Features

- Supporting components
    - `Translation`
    - `Rotation`
    - `NonUniformScale`
- Pause, resume and stop tweens on an entity
- Multiple types of active tweens on the same entity at the same time
- Ping pong
- Loop
- Ease library (inspired by [Squirrel Eiserloh's talk on GDC 2015](https://www.youtube.com/watch?v=mr5xkf6zSzk))
    - Smooth start
    - Smooth stop
    - Smooth step
    - Crossfade

## Dependency

- Unity 2020.1
  - Entities 0.14.0-preview.18
  - Burst 1.3.6
  - Mathematics 1.2.1

## Installation

Entity Tween is a Unity package. You can [install it from the git URL](https://docs.unity3d.com/2020.1/Documentation/Manual/upm-ui-giturl.html) in Unity package manager.

Or, you can edit `Packages/manifest.json` manually, adding git URL as a dependency:

```json
"dependencies": {
    "com.timespawn.entitytween": "https://github.com/NagaChiang/entity-tween.git"
}
```

To specify particular branch or tag, you can add it after the URL:

```json
"dependencies": {
    "com.timespawn.entitytween": "https://github.com/NagaChiang/entity-tween.git#develop"
}
```

```json
"dependencies": {
    "com.timespawn.entitytween": "https://github.com/NagaChiang/entity-tween.git#v0.1.0"
}
```

To update existing Entity Tween package to the latest, remove the dependency on `com.timespawn.entitytween` in `Packages/packages-lock.json` then refresh:

```json
"dependencies": {
    "com.timespawn.entitytween": {
      "version": "https://github.com/NagaChiang/entity-tween.git",
      "depth": 0,
      "source": "git",
      "dependencies": {},
      "hash": "d3341cf672c26d1f0fe6e09d3bf7978c8cf22df7"
    }
}
```

For more information, please visit [Unity documentation](https://docs.unity3d.com/2020.1/Documentation/Manual/upm-git.html).

## Examples

The main entry point of the library is the `EntityTween` class. All functionality have overloads to support `EntityManager`, `EntityCommandBuffer` and `EntityCommandBuffer.ParallelWriter`.

### Move the entity

```cs
float3 start = new float3(0.0f, 0.0f, 0.0f);
float3 end = new float3(1.0f, 1.0f, 1.0f);
float duration = 5.0f;
bool isPingPong = false;
int loopCount = 1;

EntityTween.Move(entityManager, entity, start, end, duration, new EaseDesc(EaseType.SmoothStep, 2), isPingPong, loopCount);
EntityTween.Move(commandBuffer, entity, start, end, duration, new EaseDesc(EaseType.SmoothStep, 2), isPingPong, loopCount);
EntityTween.Move(parallelWriter, entity, start, end, duration, new EaseDesc(EaseType.SmoothStep, 2), isPingPong, loopCount);
```

### Stop the entity

```cs
EntityTween.Stop(entityManager, entity);
```

### Check if the entity is tweening

Any entity with component `Tween` is tweening; that is, to know if the entity is tweening, check if the entity has any `Tween` component in any way.

```cs
if (EntityManager.HasComponent<Tween>(entity))
{
    Debug.Log("It's tweening!");
}
```

## Workflow

### Command

When starting a tween by calling `EntityTween` functions (e.g. `EntityTween.Move()`), it creates a tween command component of its kind (e.g. `TweenTranslationCommand`) containing the tweening data on the target entity.

If starting multiple tweens with the same type consequently, the command component will be overridden by the last one, which means only the last tween will be successfully triggered.

### Generation

`TweenGenerateSystem` is an abstract generic system, which will take the commands of its kind, then append a `Tween` with an unique tween ID to the `DynamicBuffer` of the target entity. Also, it creates another component with the same tween ID, storing the start and end information of the tween.

Making `Tween` an `IBufferElementData` allows multiple active tweens on the same entity at the same time, while the other part of the tween data, e.g. `TweenTranslation`, is just an `IComponentData`, which ensures that there must be only one tween of the certain type.

For example, `TweenTranslationGenerateSystem`, which inherits `TweenGenerateSystem`, will take `TweenTranslationCommand` then create `Tween` and `TweenTranslation` on the target entity.

### Easing

`TweenEaseSystem` iterates all `Tween` components and update the elapsed time of the tween, then calculate the progress by `Ease.CalculatePercentage()` for later use.

### Applying

For every type of tweens, there is a system responsible for applying the value to the target component.

For example, `TweenTranslationSystem` takes `Tween` and `TweenTranslation` and interpolate the value depending on the eased percentage, then apply it to `Translation`.

### Checking State

`TweenStateSystem` iterates all `Tween` components checking if they're about to be looped, ping-ponged or destroyed. When `Tween.LoopCount == 0` after being updated means it should be looped infinitely, while `Tween.LoopCount == 255` means it's pending destroyed by `TweenDestroySystem` later.

### Destroying

`TweenDestroySystem` is also an abstract generic system for each type of tween to implement, which destroys `Tween` marked by `TweenStateSystem` earlier and its corresponding tween data component.

For example, `TweenTranslationDestroySystem` will be responsible for destroying `Tween` and `TweenTranslation`.

## Donation

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/C0C12EHR2)