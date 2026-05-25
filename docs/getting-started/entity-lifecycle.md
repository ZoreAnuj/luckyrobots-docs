# Entity Lifecycle

Override the `protected virtual` methods on [`Entity`](../api/scene/entity.md) to hook
into the simulation. You only override the ones you need.

## Per-frame callbacks

These fire once per rendered frame, in this order:

| Callback | When |
|----------|------|
| [`OnPreUpdate(float ts)`](../api/scene/entity.md#m-onpreupdate)   | Before the main update pass. |
| [`OnUpdate(float ts)`](../api/scene/entity.md#m-onupdate)         | The main per-frame update — most game logic goes here. |
| [`OnPostUpdate(float ts)`](../api/scene/entity.md#m-onpostupdate) | After the main update pass. |
| [`OnLateUpdate(float ts)`](../api/scene/entity.md#m-onlateupdate) | After all entities have updated — good for camera follow and cleanup. |

`ts` is the elapsed time since the previous frame, in **seconds**. Multiply
rates by `ts` to stay frame-rate independent (`Translation += velocity * ts`).

## Physics callback

[`OnPhysicsUpdate(float ts)`](../api/scene/entity.md#m-onphysicsupdate) runs on the
fixed physics step rather than per frame. Apply forces and read physics state here so
behavior is consistent regardless of frame rate. See
[Working with components → Physics](working-with-components.md#physics-bodies).

## Creation and destruction

| Callback | When |
|----------|------|
| [`OnCreate()`](../api/scene/entity.md#m-oncreate)   | Once, when the entity's script starts. Cache components and initialize state here. |
| [`OnDestroy()`](../api/scene/entity.md#m-ondestroy) | Once, when the entity is destroyed. Release anything you set up. |

## Advanced: free-running callbacks

[`OnFreePreUpdate`](../api/scene/entity.md#m-onfreepreupdate) and
[`OnFreePostUpdate`](../api/scene/entity.md#m-onfreepostupdate) are decoupled from the
standard stepped update. Most scripts don't need them — reach for them only when you
have logic that must run outside the normal update ordering. See the
[`Entity` reference](../api/scene/entity.md) for the exact signatures.

## Events

Beyond the lifecycle methods, `Entity` exposes events you can subscribe to in
`OnCreate`, such as collision and trigger callbacks:

```csharp
protected override void OnCreate()
{
    CollisionBeginEvent += other => Log.Info($"Hit {other.Tag}");
}
```

See the **Events** section of the [`Entity` reference](../api/scene/entity.md) for the
full list (`CollisionBeginEvent`, `TriggerBeginEvent`, `DestroyedEvent`, and more).
