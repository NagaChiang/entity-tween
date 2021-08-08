# Changelog

## [0.6.0] - 2021.08.08

### Changed

- Dependency upgrade
  - Entities 0.17.0-preview.42 (from 0.14.0-preview.18)
  - Burst 1.4.9 (from 1.3.6)

### Fixed

- Reflection data of generic jobs has not automatically initialized ([#1](https://github.com/NagaChiang/entity-tween/issues/1))

## [0.5.1] - 2021.08.02

### Fixed

- Ended tweening entity is set back to start value ([#2](https://github.com/NagaChiang/entity-tween/issues/2))
- Multiple TweenStates are not destroyed properly at the same time

## [0.5.0] - 2021.01.21

### Changed

- `TweenDestroySystem.DestroyJob` now uses `BufferTypeHandle` instead of `BufferFromEntity`
- Improve performance by marking tweens to be destroyed with `TweenDestroyCommand` instead of `TweenState.LoopCount == 255`

### Fixed

- An exception in Tiny builds when `new TTweenInfo()` in `TweenGenerateSystem` invokes `Activator.CreateInstance()`

## [0.4.1] - 2020.11.17

### Changed

- Add `in` modifiers

### Fixed

- Fix an ambiguous reference for `SpriteRenderer` when Tiny 0.31.0 and 2D Entities both installed

## [0.4.0] - 2020.11.11

### Added

- Tween parameter: Start delay
- Tween support for `SpriteRenderer.Color` in Tiny (`Tween.Tint()`)
- Support for Tiny 0.29.0 and above
- Unit tests
    - `Ease_Delayed`
    - Generation, application and destruction of `Tween.Tint()`
- `EaseDesc` shortcuts (with exponent = 2)
    - `EaseDesc.Linear`
    - `EaseDesc.SmoothStart`
    - `EaseDesc.SmoothStop`
    - `EaseDesc.SmoothStep`

### Changed

- Set WriteGroup for `Translation`, `Rotation` and `Scale`
- Replace `TweenState.LOOP_COUNT_INFINITE` with `Tween.Infinite`

### Fixed

- Parallel writing with `[NativeDisableContainerSafetyRestriction]` in `TweenDestroySystem`

## [0.3.0] - 2020.11.03

### Added

- `Tween` functions overloads taking `TweenParams`
- `TweenParams` default values in constructor
- `TweenParams` overrides `ToString()`
- Unit tests
    - Ease
    - Pause
    - Resume
    - Stop
    - Ping-pong
    - Loop
    - Generate, apply and destroy
        - `Translation`
        - `Rotation`
        - `NonUniformScale`

### Changed

- `TweenParams` parameters order in constructor
- `TweenStopSystem` schedules structural changes to `EndSimulationEntityCommandBufferSystem` (was `BeginSimulationEntityCommandBufferSystem`)
- Systems use `World` (was `World.DefaultGameObjectInjectionWorld`)

## [0.2.0] - 2020.10.23

### Added

- GitHub Actions to build the default scene for Win64
- `Editor.BuildUtils` for building the default scene with GitHub Actions
- Sample: Stress Test

### Changed

- Rename buffer element `Tween` to `TweenState`
- Rename static class `EntityTween` to `Tween`
- Replace assertions with if-else checks to be compatible to Burst

### Fixed

- Typo "Crossfade"

## [0.1.0] - 2020.10.08

### Added

- `Ease` utilities for interpolation
- `Translation`, `Rotation` and `Scale` tween support
- Pause, resume and stop tweens on the entity
