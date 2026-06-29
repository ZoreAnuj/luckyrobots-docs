---
icon: material/code-tags
---

# Lucky Engine Scripting

Write C# to drive behavior in Lucky Engine scenes: move entities, respond to
input and collisions, query physics, control robots, and define reinforcement-learning
tasks.

<div class="grid cards" markdown>

-   :material-rocket-launch: **[Getting started](getting-started/index.md)**

    The scripting model, a first `Entity` script, and the update lifecycle.

-   :material-book-open-variant: **[Guides](guides/index.md)**

    Task-focused walkthroughs: input, components, physics, and the robot/MDP APIs.

-   :material-api: **[API reference](api/index.md)**

    Every public type in the `Hazel` scripting namespace, generated from source.

</div>

## A first taste

Every script is a class that derives from [`Entity`](api/scene/entity.md). Override the
lifecycle methods that matter and call the engine APIs from inside them:

```csharp
using Hazel;

public class Spinner : Entity
{
    // Public fields appear in the Inspector. See "Editor Attributes" in the reference.
    public float DegreesPerSecond = 90.0f;

    protected override void OnUpdate(float ts)
    {
        // ts is the fixed simulation step, in seconds (0.02 at 50 Hz). Rotation is in radians.
        Rotation += new Vector3(0.0f, DegreesPerSecond * Mathf.Deg2Rad * ts, 0.0f);
    }
}
```

Attach the compiled script to an entity via a **Script** component in the editor, and
`OnUpdate` runs every simulation step (50 Hz by default).

## How these docs are built

The **API reference** is generated directly from the engine's C# source
(`Hazel-ScriptCore`), so signatures never drift from the code. The **guides** are
hand-written. Use the version selector in the header to switch between engine releases
(e.g. `2026.1` ↔ `2026.2`).

!!! tip "Improving the reference"
    Reference text comes from `///` XML doc comments in the engine source. To improve a
    description, edit the doc comment on the type or member in `Hazel-ScriptCore`. The
    next docs build picks it up automatically.
