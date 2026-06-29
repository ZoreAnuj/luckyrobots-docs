---
icon: material/star-outline
---

# What's New

This page lists the highlights of each Lucky Engine release. Lucky Engine **2026.1** is
the first public release.

!!! abstract "In short"
    Lucky Engine 2026.1 is the first public release: a deterministic robotics simulation
    and data-generation tool with internal C# scripting, trained-policy support, and a
    gRPC API to drive the simulation from external code for reinforcement-learning
    training and live policy inference.

## 2026.1

The first public release.

**Simulation**

- Deterministic simulation driven by the [Time Manager](time-manager.md): multiple
  fixed-rate time runners interleave on a single nanosecond-accurate timeline, and run in
  real time or faster than real time.
- MuJoCo physics for robots, with Jolt and Box2D solvers also available.

**Robots and control**

- Multiple ways to drive a robot: direct joint states, torques, node-based motion graphs,
  inverse kinematics, and trained policies, with several policies active on one robot at
  once. See [Controlling robots](scripting/guides/controlling-robots.md).
- A [Content Vault](robots.md) of ready-made robots (Franka Panda, AgileX Piper, Enactic
  OpenArm, Unitree Go2, Unitree G1) and
  [example projects](example-projects.md) (Welcome, Piper Pattern Stacking, Walking Pick and Place).
  Any MuJoCo XML (MJCF) model can be imported too.

**Content and data**

- A [Content Browser and asset system](assets.md) for scenes, meshes, materials, MJCF
  models, policies, and motion graphs.
- Data and video recording through the Observer: Parquet datasets and camera video,
  covered in [Recording](scripting/guides/recording.md).

**Programming**

- A C# [scripting API](scripting/index.md) for scene behavior, robot control, and
  reinforcement-learning task definition.
- A [gRPC API](grpc-api.md) for driving simulations from any language, with a Python helper
  and Gymnasium environments. It supports reinforcement-learning training and policy inference. A
  registered robot or agent and the External Action Gate give strict lock-step stepping, where
  one `Step` advances exactly one sim tick. The post-reset settle window is configurable per
  scene.

**Platforms**

- Windows and Linux, 64-bit. Requires a GPU supporting Vulkan 1.3.

New to Lucky Engine? Start with [Get Started](get-started.md).
