# Working with Components

Components hold the data and behaviour attached to an entity: its transform, mesh,
physics body, camera, lights, audio, robot policy, and more. Scripts read and modify
them through a small generic API on [`Entity`](../api/scene/entity.md).

## The component API

| Method | Purpose |
|--------|---------|
| [`GetComponent<T>()`](../api/scene/entity.md#m-getcomponent) | Returns the component of type `T`, or `null` if the entity does not have one. |
| [`HasComponent<T>()`](../api/scene/entity.md#m-hascomponent) | `true` if the entity has a component of type `T`. |
| [`CreateComponent<T>()`](../api/scene/entity.md#m-createcomponent) | Adds and returns a new component of type `T`. |
| [`RemoveComponent<T>()`](../api/scene/entity.md#m-removecomponent) | Removes the component of type `T`. |

`GetComponent<T>()` returns a nullable reference, so check before using it:

```csharp
protected override void OnCreate()
{
    m_Mesh = GetComponent<MeshComponent>();
    if (m_Mesh is null)
        return;

    // ... configure m_Mesh
}
```

Cache components in `OnCreate` rather than calling `GetComponent` every frame.

## Transforms

Every entity has a [`TransformComponent`](../api/components/transformcomponent.md).
`Entity` exposes shortcuts for the local-space transform, so the component itself rarely
needs to be fetched for everyday work:

```csharp
// Shortcuts on Entity. All are LOCAL space, relative to the parent.
Translation  = new Vector3(0.0f, 1.0f, 0.0f);   // metres
Rotation     = new Vector3(0.0f, 3.14f, 0.0f);  // euler angles, radians
Scale        = Vector3.One;
RotationQuat = Quaternion.Identity;             // quaternion form of Rotation
```

For world-space values, or for the raw transform matrix, fetch the component:

```csharp
TransformComponent t = GetComponent<TransformComponent>();
Vector3    worldPos = t.WorldTranslation;
Quaternion worldRot = t.WorldRotationQuat;
Matrix4    matrix   = t.WorldTransformMatrix;
Transform  world    = t.World;   // packed local-to-world transform
```

| What | Local (shortcut on `Entity`) | World (on `TransformComponent`) |
|------|------------------------------|---------------------------------|
| Position | `Translation` | `WorldTranslation` |
| Euler rotation | `Rotation` (radians) | `WorldRotation` |
| Quaternion rotation | `RotationQuat` | `WorldRotationQuat` |
| Scale | `Scale` | `WorldScale` |
| Matrix | `TransformComponent.Matrix` | `WorldTransformMatrix` |

## Physics

Lucky Engine ships three physics solvers. The right one for an entity is chosen by which
body and collider components the entity carries. All three can run in the same scene at
the same time, on disjoint entity sets.

<div class="grid cards" markdown>

-   :material-robot: **Mujoco**

    Robotics simulation: articulated chains, joints, actuators, contact-rich
    manipulation. The solver behind UnitreeG1, UnitreeGo2, the Piper arm, SO100, and the
    rest of the robotics samples.

-   :material-cube-outline: **Jolt**

    Game-style 3D rigid body physics: rigid props, character controllers, simple
    constraints. Fast and stable for non-articulated bodies.

-   :material-vector-square: **Box2D**

    2D physics for 2D scenes.

</div>

### Mujoco bodies (robotics)

A robot is anchored by a
[`MujocoSceneComponent`](../api/components/mujocoscenecomponent.md) and shapes its
geometry through `Mujoco*ColliderComponent` parts. Its trained policies, motion graphs,
and joint actuators are all reached through
[`RobotControllerComponent`](../api/components/robotcontrollercomponent.md).

#### Driving a policy

A trained policy is activated by slot ID; its commands are written by ID each step. For
the G1 walker, slot 1 with commands 1, 2, 3 = forward velocity, strafe velocity, yaw
rate:

```csharp
private const uint k_WalkerSlot = 1u;
private const uint k_SetVx      = 1u;
private const uint k_SetYawRate = 3u;

private RobotControllerComponent? m_Robot;

protected override void OnCreate()
{
    m_Robot = GetComponent<RobotControllerComponent>();
    m_Robot?.SetPolicyActive(k_WalkerSlot, true);
}

protected override void OnUpdate(float ts)
{
    if (m_Robot is null)
        return;

    m_Robot.SetFloat(k_WalkerSlot, k_SetVx,      0.5f);  // m/s
    m_Robot.SetFloat(k_WalkerSlot, k_SetYawRate, 0.0f);  // rad/s
}
```

#### Driving IK targets through a motion graph

If the robot carries a motion-graph asset, IK targets are exposed as named inputs on the
same `RobotControllerComponent`. The script writes a target pose and fires a trigger to
start the move; the graph interpolates the arm over the requested duration.

```csharp
public class ArmReacher : Entity
{
    // Input names must match those declared in the robot's motion-graph asset.
    private static readonly Identifier k_TargetPosition    = new Identifier("Target_Position");
    private static readonly Identifier k_TargetOrientation = new Identifier("Target_Orientation");
    private static readonly Identifier k_Duration          = new Identifier("Duration");
    private static readonly Identifier k_Start             = new Identifier("Start");

    private RobotControllerComponent? m_Robot;

    protected override void OnCreate()
    {
        m_Robot = GetComponent<RobotControllerComponent>();
    }

    [Button("Reach to test pose")]
    public void ReachToTestPose()
    {
        if (m_Robot is null)
            return;

        Vector3 worldPosition  = new Vector3(0.3f, 0.5f, 0.1f);  // metres
        Vector3 orientationRad = Vector3.Zero;                    // identity rotation

        m_Robot.SetInputVector3(k_TargetPosition,    worldPosition);
        m_Robot.SetInputVector3(k_TargetOrientation, orientationRad * Mathf.Rad2Deg);  // graph reads degrees
        m_Robot.SetInputFloat  (k_Duration,          2.0f);                            // seconds
        m_Robot.SetInputTrigger(k_Start);                                              // fires one move
    }
}
```

Position is in metres in world space, orientation in degrees as Euler XYZ. Each
`SetInputTrigger(k_Start)` cancels any in-flight move and starts a fresh interpolation
to the most recently written target.

!!! info "Policies and IK share the same component"
    `RobotControllerComponent` runs trained policies and motion-graph IK side by side.
    Joint ownership is partitioned with `SetDrivenJoints(slotId, jointNames[])`. Each
    joint can be written by one active policy, by the motion graph's IK, or by the
    script directly, but never by more than one in the same step.

The Mujoco component family includes `MujocoBodyComponent`, `MujocoGeomComponent`,
`MujocoBoxColliderComponent`, `MujocoSphereColliderComponent`,
`MujocoCapsuleColliderComponent`, `MujocoCylinderColliderComponent`,
`MujocoMeshColliderComponent`, `MujocoJointComponent`, and `MujocoEqualityComponent`.

!!! tip "Real examples"
    The ContentVault projects under `UnitreeG1`, `UnitreeGo2`, `Piper`,
    `PiperPatternStacking`, `CupCleaner`, and `SO100PickAndPlace` are all Mujoco-driven
    robotics setups. They are good references for how a real scene composes Mujoco
    bodies, colliders, joints, policy slots, and motion-graph IK targets.

### Jolt rigid bodies (game-style 3D)

[`RigidBodyComponent`](../api/components/rigidbodycomponent.md) plus a collider
(`BoxColliderComponent`, `SphereColliderComponent`,
`CapsuleColliderComponent`, or `MeshColliderComponent`) makes an entity dynamic under
Jolt. Forces and impulses are applied through the body, on the physics step:

```csharp
private RigidBodyComponent? m_Body;

protected override void OnCreate()
{
    m_Body = GetComponent<RigidBodyComponent>();
}

protected override void OnPhysicsUpdate(float ts)
{
    if (m_Body is null)
        return;

    m_Body.AddForce(Vector3.Up * 25.0f);
    Log.Info($"speed = {m_Body.LinearVelocity.Length()}");
}
```

For character movement and stair stepping, use
[`CharacterControllerComponent`](../api/components/charactercontrollercomponent.md)
rather than a raw rigid body.

### Box2D rigid bodies (2D)

[`RigidBody2DComponent`](../api/components/rigidbody2dcomponent.md) with
[`BoxCollider2DComponent`](../api/components/boxcollider2dcomponent.md) or
`CircleCollider2DComponent` drives 2D physics.

## Common components by area

| Area | Components |
|------|------------|
| Transform | `TransformComponent` |
| Rendering | `MeshComponent`, `StaticMeshComponent`, `CameraComponent`, `DirectionalLightComponent`, `PointLightComponent`, `SpotLightComponent`, `SkyLightComponent` |
| Mujoco (robotics) | `MujocoSceneComponent`, `MujocoBodyComponent`, `Mujoco*ColliderComponent`, `MujocoJointComponent`, `MujocoEqualityComponent`, `RobotControllerComponent` |
| Jolt (3D physics) | `RigidBodyComponent`, `BoxColliderComponent`, `SphereColliderComponent`, `CapsuleColliderComponent`, `MeshColliderComponent`, `CharacterControllerComponent` |
| Box2D (2D physics) | `RigidBody2DComponent`, `BoxCollider2DComponent` |
| Audio | `AudioComponent`, `AudioListenerComponent` |
| Animation | `AnimationComponent` |

Browse them all in the [Components reference](../api/components/index.md).
