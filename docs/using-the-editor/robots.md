---
icon: material/face-agent
---

# Robots

Robots in Lucky Engine are delivered as packs. Each pack is built around an MJCF
(`.xml`) model file (MuJoCo's standard format), with the meshes, textures, and optional
learned policies that go with it. The editor's **Content Vault** is the primary way to
bring robots into a project.

!!! abstract "In short"
    Open the Content Vault panel, install a robot pack, and drop it into a scene. The
    editor wires up the physics, the motion graph, and any trained policies
    automatically, ready to drive from script.

## In the Content Vault

The **Content Vault** panel (View → Content Vault) shows every pack available in the
Lucky Robots library. Click the download icon on a pack to install it into the active
project. Any C# scripts the pack ships with are compiled automatically.

Public packs currently in the Vault:

<div class="grid cards" markdown>

-   :material-robot-industrial: **Panda**

    The Franka Emika Panda 7-DoF arm. Single-arm and two-arm variants. Featured in the
    Welcome scene that opens on first run.

-   :material-robot-industrial: **Piper**

    The AgileX Piper 6-DoF arm with parallel gripper. The base for the Piper sample
    projects (pick-and-place, block stacking, pattern stacking, unscrew cap).

-   :material-robot-industrial: **OpenArm (Pedestal)**

    The Enactic OpenArm v2 bi-manual manipulator on a static pedestal, with per-motor
    Damiao physics and articulated multi-segment fingers.

-   :material-dog: **Unitree Go2**

    Quadruped reference robot, with both standard and MJX (MuJoCo XLA) variants.

-   :material-robot: **Unitree G1**

    Humanoid biped, available with three-finger and five-finger hand variants. The
    five-finger variant ships trained walker and rotator policies; the base and
    three-finger variants ship the walker policy.

</div>

The Content Vault panel is the authoritative current list. New packs land there as they
ship.

## What's in a pack

When a pack installs, the editor unpacks the following into the project:

| Asset | What it is |
|-------|------------|
| MJCF (`.xml`)                       | The robot model. Usually a root model and a scene wrapper. |
| `assets/`                           | Meshes and textures referenced by the MJCF. |
| Policies (`.onnx` + descriptor)     | Learned policies the pack ships with, if any. |
| Motion graph (`.hmograph`)          | IK and motion planning, if the pack includes one. |

## Adding a robot to a scene

With a pack installed, drag the robot from the Content Vault panel into the scene
hierarchy or the viewport. The new entity carries the components needed to simulate it:

| Component | Purpose |
|-----------|---------|
| `MujocoSceneComponent`     | The MuJoCo physics instance for this robot. |
| `RobotControllerComponent` | Hosts the robot's policies and motion graph. |
| Per-link child entities    | Each rigid body in the MJCF gets a child entity. Their transforms are synced from MuJoCo every physics step, and they carry any visual mesh. |

Every robot picks up a motion graph at placement time, exposed for C# control. Packs
that ship trained policies, like the Unitree G1 (walker and rotator), get those wired
into `RobotControllerComponent.Policies[]` automatically too, with their command enums
generated for script use.

For the G1, the `RobotControllerComponent` looks like this in the Inspector right after
placement:

<div class="inspector-mock">
<div class="group">RobotControllerComponent</div>
<div class="field"><span class="label">Motion Graph</span><div class="control"><div class="dropdown"><span>G1_Locomotion.hmograph</span></div></div></div>
<div class="header-text">Policies</div>
<div class="field"><span class="label">walker</span><div class="control"><div class="toggle off">Inactive</div></div></div>
<div class="field"><span class="label">rotator</span><div class="control"><div class="toggle off">Inactive</div></div></div>
</div>

Policies start inactive; activate them from script with `SetPolicyActive(slotId, true)`.
For the script-level view of these components, see
[Working with components → Mujoco bodies](scripting/getting-started/working-with-components.md#mujoco-bodies-robotics).
For an end-to-end example driving the G1 walker, see the
[Getting Started walker script](scripting/getting-started/index.md).

## Adding policies and motion graphs

A robot needs a controller to do anything. `RobotControllerComponent` is the engine's
home for that controller and holds two things side by side:

- A list of **policy slots**, each binding a trained policy (an `.onnx` model with its
  descriptor) to the robot, with its own slot ID, priority, and driven-joint mask.
- A single **motion graph** reference (a `.hmograph` asset) that drives IK and motion
  planning for any joints the policies have not claimed.

A robot can host many policies and a motion graph at the same time. Joint ownership is
divided up so they do not overwrite each other.

!!! note "Most packs come pre-wired"
    Robot packs that ship a motion graph or policies get them attached automatically at
    placement, as shown above. The workflows below are for adding more policies on top,
    swapping the motion graph, or wiring up a robot that did not come with either.

### Adding a policy

Right-click the robot entity (in the viewport or the scene hierarchy) and pick
**Add Policy**. The submenu lists every policy already in the project's
`Assets/Policies/` folder. Pick one to register it on the robot.

To add a policy from outside the project, click **Import Policy...** in the same menu.
The file picker asks for the `.onnx` (or `.honnx`) and its `model_config.json`, the JSON
that lists joint names, default poses, PD gains, and action scales for the policy. The
editor then:

1. Generates a `policy_descriptor.<name>.json` next to the policy, listing the policy's
   command IDs (1-based, for example `SetVx = 1`, `SetVy = 2`, `SetYawRate = 3`), the
   observation layout, and the joint metadata.
2. Updates the project's `PolicyRegistry.yaml`.
3. Adds a new entry to the robot's `RobotControllerComponent.Policies[]`, inactive by
   default.
4. Generates a starter C# script with the policy's command enum, if this is the entity's
   first policy and it has no script attached yet.

To activate the policy at runtime, flip its slot from a script:

```csharp
GetComponent<RobotControllerComponent>().SetPolicyActive(slotId, true);
```

See [Getting started](scripting/getting-started/index.md) for the WASD walker example
that drives the G1 walker policy end to end.

### The policy slot model

Each policy slot on `RobotControllerComponent` carries:

| Field | Purpose |
|-------|---------|
| `Id`              | Script-facing slot ID (1, 2, 3, ...). Used in `SetPolicyActive`, `SetFloat`, `GetFloat`. |
| `Name`            | Display name shown in the Inspector. |
| `DescriptorPath`  | Asset path to the policy's `policy_descriptor.*.json`. |
| `Active`          | Whether the policy evaluates this step. Toggled from script. |
| `Priority`        | Higher priority wins joint-ownership ties when multiple active policies want the same joint. |
| `DrivenJoints`    | Whitelist of actuated joints this policy may write. Empty means all of the robot's actuated joints. |

Multiple slots on the same robot let one entity host, for example, a walker and a
rotator at the same time. The active set is whichever slots have `Active = true` this
step, with `DrivenJoints` (and priority) deciding who writes each joint.

### Attaching a motion graph

The motion graph is a single asset on the component, not a slot. Most robot packs ship
one already attached. To swap it for a different graph, or attach one to a robot that
did not come with any, select the robot in the scene hierarchy and drag a `.hmograph`
asset onto the `RobotControllerComponent`'s **Motion Graph** field in the Inspector.
The graph runs every step alongside the active policies, driving IK and motion planning
for any joints the policies have not claimed.

The script-side view of policy commands and motion-graph IK targets is in
[Working with components → Mujoco bodies](scripting/getting-started/working-with-components.md#mujoco-bodies-robotics).

## Where to go next

- [Getting started](scripting/getting-started/index.md) has the WASD walker script for
  the UnitreeG1.
- [Working with components](scripting/getting-started/working-with-components.md) covers
  the MuJoCo component family and how to drive policies and IK from script.
- [Recording with the Observer](scripting/guides/recording.md) for collecting datasets
  from robot scenes.
