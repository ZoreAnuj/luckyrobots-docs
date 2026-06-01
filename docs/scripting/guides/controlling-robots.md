# Controlling robots: policies and IK

`RobotControllerComponent` is where robot control lives. It hosts two control surfaces in
one component: a list of **policy slots** that bind trained policies to the robot, and a
single **motion graph** that drives IK targets. Most real scenes combine both: a policy
drives the lower body, IK drives an arm, and a joint-ownership mask keeps them out of
each other's way.

The worked example throughout this page is a G1 humanoid carrying multiple policies
and an arm IK chain: a walker policy for gait, a rotator policy for clean turn-in-place,
and motion-graph IK for arm reach and grasp.

## Activating and driving a policy

Two calls drive every policy: `SetPolicyActive` and `SetFloat`. The slot ID comes from
the `PolicyIds` enum the editor generates; the command ID comes from a matching command
enum (for example `WalkerCommands.SetVx`, `SetVy`, `SetYawRate`).

```csharp
using Hazel;

public class WalkerDriver : Entity
{
    private RobotControllerComponent? m_Rcc;
    private RobotController           m_Robot;   // typed wrapper, implicit from m_Rcc

    protected override void OnCreate()
    {
        m_Rcc = GetComponent<RobotControllerComponent>();
        if (m_Rcc is null)
            return;
        m_Robot = m_Rcc;

        m_Robot.SetPolicyActive(PolicyIds.Walker, true);
    }

    protected override void OnUpdate(float ts)
    {
        if (m_Rcc is null)
            return;

        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetVx,      0.5f);  // m/s
        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetVy,      0.0f);  // m/s
        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetYawRate, 0.0f);  // rad/s
    }
}
```

`RobotController` is a thin wrapper struct that adds enum-typed overloads of the
policy commands and converts implicitly from `RobotControllerComponent`, so caching
it alongside `m_Rcc` keeps every policy call site readable. The component still
exposes the raw `uint` form `m_Rcc.SetFloat(slotId, commandId, value)` for code
that hasn't picked up the generated enums yet, and it remains the entry point for
motion-graph inputs and policy setup further down this page.

## Switching between policies

A robot can carry several trained policies and swap between them. The walker handles
gait; the rotator is trained to turn cleanly in place from a standstill, which the
walker is not. A turn-to-face routine has to bring the body to a halt with the walker,
hand control to the rotator, wait until the heading is aligned, damp residual rotation,
then hand control back. It cannot collapse to a single-frame swap, because
`SetPolicyActive(slotId, false)` only flips a flag and reaps the slot's policy
runtime: it does not clear the body's velocity in the underlying physics state, and
the engine carries no observation history that an incoming policy could replay. If
the rotator activates while the body still has forward velocity, its first
observation falls outside the standstill distribution it was trained on and it
stumbles. The walker has to physically decelerate the body across several ticks
before the swap happens.

The handoff is therefore a small state machine spanning multiple ticks of the Robot
runner (50 Hz by default, so each tick is 20 ms):

```csharp
public class TurnToFace : Entity
{
    private enum Phase { Walking, DecelBody, Rotating, DampRotation }

    private const int k_DecelTicks = 6;   // walker brings body to a standstill
    private const int k_DampTicks  = 4;   // rotator damps residual rotation

    private RobotControllerComponent? m_Rcc;
    private RobotController           m_Robot;

    private Phase m_Phase          = Phase.Walking;
    private int   m_TicksInPhase   = 0;
    private float m_DesiredYawRate = 0.0f;

    protected override void OnCreate()
    {
        m_Rcc = GetComponent<RobotControllerComponent>();
        if (m_Rcc is null)
            return;
        m_Robot = m_Rcc;
        m_Robot.SetPolicyActive(PolicyIds.Walker, true);
    }

    // Called by a navigation script when a heading change is needed.
    public void RequestTurn(float yawRate)
    {
        m_DesiredYawRate = yawRate;
        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetVx,      0.0f);
        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetVy,      0.0f);
        m_Robot.SetFloat(PolicyIds.Walker, WalkerCommands.SetYawRate, 0.0f);
        m_Phase        = Phase.DecelBody;
        m_TicksInPhase = 0;
    }

    protected override void OnUpdate(float ts)
    {
        if (m_Rcc is null)
            return;
        m_TicksInPhase++;

        switch (m_Phase)
        {
            case Phase.DecelBody:
                // Walker is still active and commanding zero velocity; the body
                // bleeds off forward speed across these ticks.
                if (m_TicksInPhase >= k_DecelTicks)
                {
                    m_Robot.SetPolicyActive(PolicyIds.Walker,  false);
                    m_Robot.SetPolicyActive(PolicyIds.Rotator, true);
                    m_Robot.SetFloat(PolicyIds.Rotator, RotatorCommands.SetYawRate, m_DesiredYawRate);
                    m_Phase        = Phase.Rotating;
                    m_TicksInPhase = 0;
                }
                break;

            case Phase.Rotating:
                if (HeadingAligned())
                {
                    m_Robot.SetFloat(PolicyIds.Rotator, RotatorCommands.SetYawRate, 0.0f);
                    m_Phase        = Phase.DampRotation;
                    m_TicksInPhase = 0;
                }
                break;

            case Phase.DampRotation:
                if (m_TicksInPhase >= k_DampTicks)
                {
                    m_Robot.SetPolicyActive(PolicyIds.Rotator, false);
                    m_Robot.SetPolicyActive(PolicyIds.Walker,  true);
                    m_Phase = Phase.Walking;
                }
                break;
        }
    }

    private bool HeadingAligned() => /* compare current heading to target */ true;
}
```

Two things to notice. First, the zero-velocity commands in `RequestTurn` are issued
while the walker is **still active**, so the walker spends `k_DecelTicks` ticks
decelerating the body before being deactivated. Issuing the same `SetFloat(...0)`
calls in the same frame as `SetPolicyActive(walker, false)` would be wasted: the
walker would never run another inference with those commands, and the slot's
command store is discarded with the runtime when the slot deactivates. Second, the
same shape applies in reverse: the rotator zeros its yaw-rate command, runs for
`k_DampTicks` more ticks to damp any leftover rotation, and only then hands the
body back to the walker. Tick counts are robot-specific and worth tuning against
observed deceleration on the target platform.

## Joint ownership

When two control surfaces want different joints, declare which joints each is allowed
to write. `SetDrivenJoints` takes a string array per policy slot; empty means "all
actuated joints," a non-empty list is an explicit whitelist.

```csharp
private static readonly string[] s_LegsAndWaistJoints =
{
    "left_hip_pitch",  "left_hip_roll",  "left_hip_yaw",  "left_knee",
    "right_hip_pitch", "right_hip_roll", "right_hip_yaw", "right_knee",
    "waist_yaw"
};

// Restrict the walker to legs and waist while the IK chain drives the active arm.
m_Robot.SetDrivenJoints(PolicyIds.Walker, s_LegsAndWaistJoints);
```

A walking-and-reaching scene switches the mask each phase: full-body walker when no
arm is extended, legs-and-waist when one arm is on IK, legs-and-one-arm when one arm
carries something and the other is reaching for something new. The mask is what keeps
the policy from fighting the IK over shared joints.

## Driving IK targets

The motion graph runs every step on a robot that has one attached. Scripts talk to it
by writing **named inputs** declared in the `.hmograph` asset, addressed through cached
`Identifier` keys.

For a humanoid with two-arm IK, the conventional input names are `Left_Target`,
`Left_Target_Orientation`, `Right_Target`, `Right_Target_Orientation`:

```csharp
public class ArmIK : Entity
{
    private static readonly Identifier s_LeftTarget      = new Identifier("Left_Target");
    private static readonly Identifier s_LeftOrientation = new Identifier("Left_Target_Orientation");

    private RobotControllerComponent? m_Rcc;

    protected override void OnCreate()
    {
        m_Rcc = GetComponent<RobotControllerComponent>();
    }

    public void SetLeftArmTarget(Vector3 worldPosition, Vector3 eulerRadians)
    {
        if (m_Rcc is null)
            return;

        m_Rcc.SetInputVector3(s_LeftTarget,      worldPosition);
        m_Rcc.SetInputVector3(s_LeftOrientation, eulerRadians * Mathf.Rad2Deg);
    }
}
```

Positions are in metres, world space. Orientations are in degrees (motion-graph inputs
expect degrees, so the script applies `Mathf.Rad2Deg` at the boundary). Cache the
`Identifier` instances in `static readonly` fields; they are not free to construct
every frame.

Input names are robot-specific. A single-arm robot's graph might expose
`Target_Position` and `Target_Orientation`; a bimanual graph exposes `Left_*` and
`Right_*`; some graphs also expose elbow-pole inputs (`Left_Pole_Target`,
`Left_Pole_Weight`) to lock the elbow into the desired IK branch. Open the motion
graph in the editor to see exactly which inputs are wired.

## The combined pattern: walk and reach

A walk-and-pickup cycle on a humanoid is the canonical walker + IK combination,
structured as a sequence of phases:

```mermaid
flowchart LR
    A[Walk to cup<br/>walker, full body] --> B[Settle<br/>walker, zero commands]
    B --> C[Policy → IK<br/>mask walker to legs+waist,<br/>park IK target near cup]
    C --> D[Reach<br/>IK slides target onto cup]
    D --> E[Close<br/>gripper close, hold]
    E --> F[IK → Policy<br/>blend back to walker]
```

The state the controller carries through this is small: the active arm (left or right),
the target's world position, the current IK target, and a phase flag. The walker keeps
the body upright through every phase; the IK only takes over the active arm; the joint
mask keeps them sharing the body without fighting.

## Where to go next

- The editor-side view in
  [Robots → Adding policies and motion graphs](../../robots.md#adding-policies-and-motion-graphs)
  covers the Add Policy context menu and what the `RobotControllerComponent` Inspector
  shows.
- [Working with components → Mujoco bodies](../getting-started/working-with-components.md#mujoco-bodies-robotics)
  has the script-level surface for the Mujoco component family.
- [Getting started](../getting-started/index.md) has the minimal WASD walker driver as
  a stripped-down version of the policy patterns above.
