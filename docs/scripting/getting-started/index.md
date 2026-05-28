# Getting Started

## The scripting model

Lucky Engine scripts are C# classes in the `Hazel` namespace that derive from
[`Entity`](../api/scene/entity.md). A compiled script attaches to an entity in the editor
via a **Script** component, and the engine calls the overridden lifecycle methods at the
right moments.

A script has access to:

- **This entity** — its [`Transform`](../api/components/transformcomponent.md),
  `Translation`/`Rotation`/`Scale`, `Tag`, `Parent`, and `Children`.
- **Components** — [`GetComponent<T>()`](../api/scene/entity.md#m-getcomponent) and
  friends (see [Working with components](working-with-components.md)).
- **Engine services** — [`Input`](../api/core/input.md),
  [`Physics`](../api/physics/index.md), [`Audio`](../api/audio/index.md),
  the [math types](../api/math/index.md), and more.

## Your first script

Open the **UnitreeG1** sample scene from ContentVault, attach this script to the G1
entity (the one carrying `RobotControllerComponent`), and press Play. The robot's
trained walker policy responds to WASD: W and S move forward and back, A and D turn left
and right.

```csharp
using Hazel;

public class WalkerInput : Entity
{
    [Tooltip("Forward speed while holding W or S.")]
    public float ForwardSpeed = 0.5f;

    [Tooltip("Yaw rate while holding A or D.")]
    public float TurnRate = 1.0f;

    // Slot 1 is the walker policy on the UnitreeG1 prefab.
    private const uint k_WalkerSlot = 1u;
    private const uint k_SetVx      = 1u;
    private const uint k_SetVy      = 2u;
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

        float forwardVelocity = 0.0f;
        float yawRate         = 0.0f;

        if (Input.IsKeyDown(KeyCode.W))
            forwardVelocity = ForwardSpeed;
        if (Input.IsKeyDown(KeyCode.S))
            forwardVelocity = -ForwardSpeed;
        if (Input.IsKeyDown(KeyCode.A))
            yawRate = TurnRate;
        if (Input.IsKeyDown(KeyCode.D))
            yawRate = -TurnRate;

        m_Robot.SetFloat(k_WalkerSlot, k_SetVx,      forwardVelocity);  // m/s
        m_Robot.SetFloat(k_WalkerSlot, k_SetVy,      0.0f);              // m/s
        m_Robot.SetFloat(k_WalkerSlot, k_SetYawRate, yawRate);           // rad/s
    }
}
```

!!! note "Conventions used here"
    Member fields are prefixed `m_`, constants `k_`, and components are cached in
    `OnCreate` rather than fetched every frame. This is the same pattern the engine's
    own templates use (`HumanoidWalkDriver`, `walker_Policy`).

What's happening:

| Piece | Reference |
|-------|-----------|
| `: Entity` | [`Entity`](../api/scene/entity.md), the base class for all scripts |
| `OnCreate` / `OnUpdate` | [Entity lifecycle](entity-lifecycle.md) |
| `RobotControllerComponent` | [`RobotControllerComponent`](../api/components/robotcontrollercomponent.md), the wrapper around a robot's loaded policies |
| `SetPolicyActive` / `SetFloat` | The policy command surface. Slot 1 is the walker; command IDs 1, 2, 3 are forward velocity, strafe velocity, and yaw rate. |
| `Input.IsKeyDown(KeyCode.W)` | [`Input`](../api/core/input.md) |

!!! tip "Public fields are editable per entity"
    `ForwardSpeed` and `TurnRate` appear as fields on the script in the Inspector. Edit
    them per entity to retune without recompiling. See
    [Editor attributes](editor-attributes.md) for sliders, ranges, tooltips, and other
    Inspector polish.

## Where to go next

- **[Entity lifecycle](entity-lifecycle.md)** — every callback and when it fires.
- **[Time runners](time-runners.md)** — the fixed-step clocks that drive callbacks, and how multiple runners share the timeline.
- **[Working with components](working-with-components.md)** — transforms, the three physics solvers, and the rest of the component surface.
- **[Editor attributes](editor-attributes.md)** — decorate script fields to shape the Inspector.
- **[API reference](../api/index.md)** — the full surface, grouped by area.
