# Lucky Engine

Lucky Engine is a robotics simulation engine — physics-driven scenes, real-time
rendering, an editor, C# scripting, and a gRPC server with a Python SDK for driving
simulations programmatically.

These docs are organized by engine system. Each section below is self-contained; use
the version selector in the header to switch between engine releases (e.g. `2026.1` ↔
`2026.2`).

<div class="grid cards" markdown>

-   :material-new-box: **[What's New](whats-new.md)**

    Release highlights for Lucky Engine 2026.1, the first public release.

-   :material-rocket-launch-outline: **[Get Started](get-started.md)**

    Install Lucky Engine, sign in, run the Welcome scene, and create a first project.

-   :material-language-csharp: **[Scripting](scripting/index.md)**

    Write C# to drive behavior in your scenes — move entities, respond to input and
    collisions, query physics, control robots, and define reinforcement-learning tasks.
    Includes a getting-started path, task-focused guides, and the full
    [API reference](scripting/api/index.md) generated from engine source.

-   :material-api: **[gRPC API](grpc-api.md)**

    The engine's gRPC server: drive simulations from any language. Step the agent loop,
    read and write MuJoCo state, control policies, and negotiate RL tasks. Includes a
    Python helper.

-   :material-clock-outline: **[The Time Manager](time-manager.md)**

    The fixed-timestep orchestrator. Up to eight runners on one shared timeline, five
    execution phases per simulation step, interleaved shared steps across runners, and
    the frame budget that ties it all to the UI clock.

-   :material-cursor-default: **[Viewport Navigation](viewport-navigation.md)**

    The three camera modes (fly, orbit, default), the keyboard shortcuts for switching
    transform gizmos, and the conflicts between movement and tool keys.

-   :material-ruler: **[Units](units.md)**

    SI units throughout: metres, seconds, kilograms, Newtons. Radians in the script API,
    degrees in the Inspector. Where each quantity shows up and how to label fields.

-   :material-folder-multiple-outline: **[Assets](assets.md)**

    The Content Browser, the project's Assets folder and registry, importing source
    files, creating engine-native assets, saving, and dragging assets into a scene.

-   :material-robot: **[Robots](robots.md)**

    What ships with the engine, what's in the Content Vault, the shape of a robot pack,
    and how to bring in your own MJCF or a MuJoCo Menagerie model.

-   :material-play-box-multiple-outline: **[Example Projects](example-projects.md)**

    Ready-to-run scenes from the Content Vault: the Welcome pick-and-place, the Cup
    Cleaner humanoid, and Piper pattern stacking.

</div>

## More sections coming

Scripting is the first system documented here. Other engine systems — rendering, the
editor, and the simulation/physics runtime — will land as their own sections alongside
it.

!!! tip "Where the content lives"
    The scripting **API reference** is generated directly from the engine's C# source
    (`Hazel-ScriptCore`), so signatures never drift from the code. Guides and section
    overviews are hand-written under each system's folder in `docs/`.
