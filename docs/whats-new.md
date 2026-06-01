# What's New

This page lists the highlights of each Lucky Engine release. Lucky Engine **2026.1** is
the first public release.

!!! abstract "In short"
    Lucky Engine 2026.1 is the first public release: a MuJoCo-based robotics simulation
    editor with C# scripting, trained-policy and IK robot control, data recording, and a
    gRPC API for driving simulations from code.

## 2026.1

The first public release.

**Simulation**

- Deterministic fixed-timestep simulation driven by the [Time Manager](time-manager.md),
  with real-time, deterministic, and fast modes.
- MuJoCo physics for robots, with Jolt and Box2D solvers also available.

**Robots and control**

- `RobotControllerComponent` hosts trained policies and a motion graph for IK on one robot,
  with per-joint ownership between them. See
  [Controlling robots](scripting/guides/controlling-robots.md).
- A [Content Vault](robots.md) of ready-made robots (Franka Panda, AgileX Piper, Enactic
  OpenArm, Unitree Go2, Unitree G1) and
  [example projects](example-projects.md) (Welcome, Cup Cleaner, Piper Pattern Stacking).

**Content and data**

- A [Content Browser and asset system](assets.md) for scenes, meshes, materials, MJCF
  models, policies, and motion graphs.
- Data recording to Parquet datasets through the Observer, covered in
  [Recording](scripting/guides/recording.md).

**Programming**

- A C# [scripting API](scripting/index.md) for scene behavior, robot control, and
  reinforcement-learning task definition.
- A [gRPC API](grpc-api.md) for driving simulations from any language, with a Python helper
  and Gymnasium environments.

**Platforms**

- Windows and Linux, 64-bit. Requires a GPU supporting Vulkan 1.3.

New to Lucky Engine? Start with [Get Started](get-started.md).
