# Changelog

## [Unreleased]

### Added

- `Tween` functions overloads taking `TweenParams`

### Fixed

- Systems used `World.DefaultGameObjectInjectionWorld`

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
