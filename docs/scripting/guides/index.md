# Guides

Task-focused walkthroughs. For the exhaustive type-by-type listing, see the
[API reference](../api/index.md).

<div class="grid cards" markdown>

-   :material-keyboard: **[Input & Controls](input.md)**

    Keyboard, mouse, and controller polling with [`Input`](../api/core/input.md). Common
    bindings for recording control and robot driving.

-   :material-robot: **[Controlling robots](controlling-robots.md)**

    Driving trained policies, switching between them, partitioning joints, and setting
    motion-graph IK targets. Worked through with a G1 humanoid carrying walker, rotator,
    and arm IK.

-   :material-record-rec: **[Recording with the Observer](recording.md)**

    Start and stop recording, manage episodes, and tag sub-tasks with the Observer API.

-   :material-tune: **[Deterministic recording with sync points](recording-advanced.md)**

    Lock episode boundaries to a configurable drumbeat for regression tests and
    determinism proofs.

</div>

## Reference areas

| Area | What's inside |
|------|---------------|
| [Scene & Entities](../api/scene/index.md) | The entity model, scene queries, prefabs. |
| [Components](../api/components/index.md) | Everything you attach to an entity. |
| [Math](../api/math/index.md) | Vectors, quaternions, matrices, interpolation, randomness. |
| [Physics](../api/physics/index.md) | 3D/2D queries, colliders, materials, forces. |
| [Rendering](../api/rendering/index.md) | Meshes, materials, textures, debug drawing. |
| [Audio](../api/audio/index.md) | Sound playback and control. |
| [Animation](../api/animation/index.md) | Animation sequences. |
| [Core & Input](../api/core/index.md) | Input, logging, timing, application, assets. |
| [Editor Attributes](../api/attributes/index.md) | Control how script fields appear in the Inspector. |
| [Robots & Learning (MDP)](../api/learn/index.md) | Robot agents and the MDP component model. |
