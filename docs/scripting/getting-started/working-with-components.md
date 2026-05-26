# Working with Components

Components hold the data and behavior attached to an entity — its transform, mesh,
physics body, camera, lights, audio, and more. Scripts read and modify them through a
small generic API on [`Entity`](../api/scene/entity.md).

## The component API

| Method | Purpose |
|--------|---------|
| [`GetComponent<T>()`](../api/scene/entity.md#m-getcomponent) | Returns the component of type `T`, or `null` if the entity doesn't have one. |
| [`HasComponent<T>()`](../api/scene/entity.md#m-hascomponent) | `true` if the entity has a component of type `T`. |
| [`CreateComponent<T>()`](../api/scene/entity.md#m-createcomponent) | Adds and returns a new component of type `T`. |
| [`RemoveComponent<T>()`](../api/scene/entity.md#m-removecomponent) | Removes the component of type `T`. |

`GetComponent<T>()` returns a nullable reference, so check it before use:

```csharp
protected override void OnCreate()
{
    if (HasComponent<MeshComponent>())
    {
        var mesh = GetComponent<MeshComponent>();
        // ... configure the mesh
    }
}
```

## Transforms

Every entity has a [`TransformComponent`](../api/components/transformcomponent.md).
`Entity` exposes shortcuts so you rarely fetch it directly:

```csharp
// These three are shortcuts to the TransformComponent:
Translation = new Vector3(0.0f, 1.0f, 0.0f);   // local-space position
Rotation    = new Vector3(0.0f, 3.14f, 0.0f);  // euler angles, radians
Scale       = Vector3.One;

// For world-space or the raw transform, go through the component:
var world = GetComponent<TransformComponent>().World;
```

## Physics bodies

Give an entity a [`RigidBodyComponent`](../api/components/rigidbodycomponent.md) to make
it dynamic. Apply forces from [`OnPhysicsUpdate`](../api/scene/entity.md#m-onphysicsupdate)
so they're applied on the fixed physics step:

```csharp
protected override void OnPhysicsUpdate(float ts)
{
    var body = GetComponent<RigidBodyComponent>();
    if (body == null)
        return;

    body.AddForce(Vector3.Up * 25.0f);     // defaults to EForceMode.Force
    Log.Info($"speed = {body.LinearVelocity.Length()}");
}
```

See the [Physics](../api/physics/index.md) section for ray casts and queries, and
[`RigidBodyComponent`](../api/components/rigidbodycomponent.md) for the full set of
forces, velocities, and constraints.

## Common components

| Component | Use |
|-----------|-----|
| [`TransformComponent`](../api/components/transformcomponent.md) | Position, rotation, scale. |
| [`MeshComponent`](../api/components/meshcomponent.md) / [`StaticMeshComponent`](../api/components/staticmeshcomponent.md) | Rendered geometry. |
| [`RigidBodyComponent`](../api/components/rigidbodycomponent.md) | Dynamic 3D physics body. |
| [`CameraComponent`](../api/components/cameracomponent.md) | A camera the scene can render from. |
| [`AudioComponent`](../api/components/audiocomponent.md) | Positional sound playback. |

Browse them all in the [Components reference](../api/components/index.md).
