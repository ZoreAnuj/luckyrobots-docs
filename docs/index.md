# Lucky Engine

Lucky Engine is a robotics simulation engine — physics-driven scenes, real-time
rendering, an editor, C# scripting, and a gRPC server with a Python SDK for driving
simulations programmatically.

These docs are organized by engine system. Each section below is self-contained; use
the version selector in the header to switch between engine releases (e.g. `2026.1` ↔
`2026.2`).

<div class="grid cards" markdown>

-   :material-language-csharp: **[Scripting](scripting/index.md)**

    Write C# to drive behavior in your scenes — move entities, respond to input and
    collisions, query physics, control robots, and define reinforcement-learning tasks.
    Includes a getting-started path, task-focused guides, and the full
    [API reference](scripting/api/index.md) generated from engine source.

-   :material-clock-outline: **[The Time Manager](time-manager.md)**

    The fixed-timestep orchestrator. Up to eight runners on one shared timeline, five
    execution phases per simulation step, interleaved shared steps across runners, and
    the frame budget that ties it all to the UI clock.

-   :material-cursor-default: **[Viewport Navigation](viewport-navigation.md)**

    The three camera modes (fly, orbit, default), the keyboard shortcuts for switching
    transform gizmos, and the conflicts between movement and tool keys.

</div>

## More sections coming

Scripting is the first system documented here. Other engine systems — rendering, the
editor, the simulation/physics runtime, and the Python SDK — will land as their own
sections alongside it.

!!! tip "Where the content lives"
    The scripting **API reference** is generated directly from the engine's C# source
    (`Hazel-ScriptCore`), so signatures never drift from the code. Guides and section
    overviews are hand-written under each system's folder in `docs/`.
