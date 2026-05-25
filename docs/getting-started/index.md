# Getting Started

## The scripting model

LuckyEngine scripts are C# classes in the `Hazel` namespace that derive from
[`Entity`](../api/scene/entity.md). You attach a compiled script to an entity in the
editor (via a **Script** component), and the engine calls your overridden lifecycle
methods at the right moments.

From inside a script you have access to:

- **This entity** — its [`Transform`](../api/components/transformcomponent.md),
  `Translation`/`Rotation`/`Scale`, `Tag`, `Parent`, and `Children`.
- **Components** — [`GetComponent<T>()`](../api/scene/entity.md#m-getcomponent) and
  friends (see [Working with components](working-with-components.md)).
- **Engine services** — [`Input`](../api/core/input.md),
  [`Physics`](../api/physics/index.md), [`Audio`](../api/audio/index.md),
  the [math types](../api/math/index.md), and more.

## Your first script

```csharp
using Hazel;

public class Mover : Entity
{
    // Public fields are editable per-entity in the Inspector.
    public float Speed = 5.0f;

    protected override void OnCreate()
    {
        Log.Info($"Mover created on entity '{Tag}'");
    }

    protected override void OnUpdate(float ts)
    {
        var move = Vector3.Zero;
        if (Input.IsKeyDown(KeyCode.W)) move += Vector3.Forward;
        if (Input.IsKeyDown(KeyCode.S)) move += Vector3.Back;
        if (Input.IsKeyDown(KeyCode.A)) move += Vector3.Left;
        if (Input.IsKeyDown(KeyCode.D)) move += Vector3.Right;

        // ts is the time since the last frame, in seconds.
        Translation += move * Speed * ts;
    }
}
```

What's happening:

| Piece | Reference |
|-------|-----------|
| `: Entity` | [`Entity`](../api/scene/entity.md) — the base class for all scripts |
| `OnCreate` / `OnUpdate` | [Entity lifecycle](entity-lifecycle.md) |
| `Input.IsKeyDown(KeyCode.W)` | [`Input`](../api/core/input.md) |
| `Vector3.Forward`, `Translation` | [`Vector3`](../api/math/vector3.md) |
| `Log.Info(...)` | [`Log`](../api/core/log.md) |

## Where to go next

- **[Entity lifecycle](entity-lifecycle.md)** — every callback and when it fires.
- **[Working with components](working-with-components.md)** — read and modify
  transforms, physics bodies, meshes, cameras, and more.
- **[API reference](../api/index.md)** — the full surface, grouped by area.
