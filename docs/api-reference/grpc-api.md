---
icon: material/api
---

# gRPC API

Lucky Engine runs an in-process gRPC server. It is the primary programmatic interface to
the engine: an external process steps the simulation, reads observations, sends actions
and policy commands, and resets episodes over gRPC. The interface is language agnostic, so
a client can be written in Python, C++, Go, Rust, or anything with a gRPC stack. Python
also has a convenience wrapper, covered at the end of this page.

!!! abstract "In short"
    Start the **gRPC Server** panel in the editor, register a robot or agent in the scene
    (a C# Learn script that does `new RobotManager(...)`), enable the **External Action
    Gate** in Scene Settings, and press Play. The server listens on `127.0.0.1:50051`
    (loopback, no TLS). Generate client stubs from the shipped `.proto` files, or explore
    the API live with reflection. Set the simulation mode off realtime, call
    `GetAgentSchema` and `ResetAgent`, then `Step` on repeat. `Step` **requires a
    registered agent** and is rejected otherwise. The loop is `Step` alone, paced by step
    count or the engine's sim clock, **never by a wall clock**.

## Starting the server

In LuckyEditor, open the **gRPC Server** panel (View → gRPC Server) and click **Start
Server**. The server listens on `127.0.0.1:50051` by default; the address and port are
editable in the panel.

The connection is loopback only and insecure (no TLS), intended for a local or trusted
lab network. Do not expose the port publicly.

The server runs at the application level, independent of any one scene. A scene must be
playing for the step loop to exchange data, so press **Play** before driving an agent.

## Connecting a client

There are two ways to build a client: generate stubs from the protos, or use reflection.

### Generate stubs from the protos

The API schema ships with the app under `Resources/Scripts/Grpc/Proto/`: `hazel_rpc.proto`,
`mujoco_scene.proto`, and `task_contract.proto`, all in the `hazel.rpc` package. Generate
stubs for any language with `protoc`.

```bash
pip install grpcio grpcio-tools
python -m grpc_tools.protoc -I Proto --python_out=. --grpc_python_out=. Proto/*.proto
```

```python
import grpc, hazel_rpc_pb2 as pb, hazel_rpc_pb2_grpc as rpc

channel = grpc.insecure_channel("127.0.0.1:50051")
scene = rpc.SceneServiceStub(channel)
print(scene.GetSceneInfo(pb.GetSceneInfoRequest()))
```

Other languages use the matching `protoc` plugin (`--cpp_out`, `--go_out`, and so on) with
the same `-I Proto Proto/*.proto` arguments.

### Explore with reflection

The server enables gRPC reflection, so tools can discover the API without the proto files:

```bash
grpcurl -plaintext 127.0.0.1:50051 list
grpcurl -plaintext 127.0.0.1:50051 describe hazel.rpc.AgentService
```

`grpcui` and Postman work the same way.

## Services

All services live in the `hazel.rpc` package:

| Service | Purpose |
|---------|---------|
| `AgentService`       | The RL step loop (`Step`, `ResetAgent`, `GetAgentSchema`), policy and motion-graph control, and task-contract negotiation |
| `MujocoSceneService` | Full MuJoCo model and data: model info, full state, `SetControl`, `SetQpos`, `ResetScene` |
| `SceneService`       | Scene info, entities and transforms, play mode, and simulation mode |
| `MujocoService`      | Agent-scoped joint state and model info, with a streaming variant |
| `CameraService`, `ViewportService` | Stream rendered camera and viewport frames |
| `TelemetryService`   | Stream `qpos` and the last applied control |
| `DebugService`       | Draw debug primitives in the viewport |

Server reflection (`grpc.reflection.v1alpha.ServerReflection`) is registered as well.

## The action gate

The **External Action Gate** keeps physics in lock-step with the client: each physics
tick blocks until the client sends a `Step`, so one `Step` advances exactly one tick.

`Step` **requires a registered external agent.** A C# Learn script registers an
external-mode agent at scene start (a script that does `new RobotManager(...)`; see
[The simulation loop](#the-simulation-loop)). Without one, `Step` is rejected with
"Step requires a registered external agent". There is no agent-free `Step` path. With an
agent registered and the gate enabled, `Step` imposes the request's `actions` and returns
an observation. That is the full reinforcement and imitation learning loop, with `ResetAgent`
and `GetAgentSchema` operating on that agent. A **tick-only** `Step` (empty `actions`) still
requires the registered agent. It advances one tick using the in-engine policy instead of
imposing an external action.

Enabling the gate in the scene is the prerequisite for strict lock-step. It is a per-scene
setting, **off by default in every scene** including the shipped examples (see
[Enabling it](#enabling-it)).

!!! warning "Enable the gate before driving an agent"
    With the gate off, physics runs freely at the simulation rate and never waits for an
    action. `Step` still applies actions and returns an observation, but the engine may
    advance several ticks between calls, or apply an action a tick late. One `Step` no
    longer maps to one physics tick, so episodes are not reproducible. Turn the gate on so
    each tick blocks until a fresh action arrives.

### Enabling it

The gate is enabled per scene, in one of two places:

- **Scene Settings**: tick **Enable External Action Gate (gRPC Sync)** under the External
  Action Gate section.
- **gRPC Server panel**: while the gate is off the panel shows a warning with a **Fix:
  Enable Action Gate** button that sets the same flag.

The toggle only changes the in-memory scene. Save the scene to persist it. There is **no
RPC to enable the gate**. A client cannot turn it on. It is purely a scene setting, so the
scene must already have it enabled before a client connects.

### How it behaves

| Aspect | Behavior |
|--------|----------|
| **A registered agent is required** | `Step` is rejected unless a scene has registered an external RL agent (a C# Learn script that does `new RobotManager(...)`, with `IsExternalBatchReady()` true). Until it is ready, three calls fail. `Step` returns "Step requires a registered external agent". `ResetAgent` returns `success=false` ("External agents not ready…"). `GetAgentSchema` returns `observation_size=0` / `action_size=0`. There is no agent-free `Step` path. |
| **Activation** | The gate **arms at the end of the post-reset settle period**. This happens on the engine main thread, at the 20th control tick after each reset, provided a client has connected. `GetAgentSchema`, `ResetAgent`, and `Step` each mark the client connected. Connecting does **not** by itself arm the gate, so an enabled gate never freezes an editor with no client attached. |
| **Settle window** | After every reset (and on initial Play) the gate is **deactivated** and physics runs **free (ungated)** for the settle period. The window is **Reset Settle Steps** control ticks, set per scene in the External Action Gate settings (default 20, about 0.4 s at a 50 Hz control rate). During it the robot holds default joint positions while it settles. The gate re-arms on the last settle tick. During this window a `Step` does **not** map 1:1 to a tick; expect the first settle ticks after a reset to be free-running. |
| **Lock-step** | Once active, every physics tick blocks until the client signals. Each `Step` releases **exactly one tick**. If the request carries `actions` (or `action_groups`) it imposes that action for the tick. A **tick-only** `Step` (empty `actions` and no `action_groups`) still releases one tick but imposes no external action: the in-engine policy or a prior `SetControl` owns `mjData.ctrl` that tick. |
| **Watchdog** | If no action arrives within the timeout (10 seconds by default, floored at a small minimum), the gate auto-deactivates and the scene stops. The timeout and a separate warning threshold are editable in the gRPC Server panel while the scene plays. |
| **Release** | `ResetAgent` **deactivates** the gate and starts a fresh settle period; it re-arms ~20 ticks later. A client that has finished can call `ReportProgress(finished=true)` to release the gate cleanly instead of letting the watchdog time out. The gate also deactivates on scene stop and gRPC server shutdown. |

The gate governs *whether* the simulation waits. It does not change *how fast* time
advances. For deterministic or maximum-rate stepping, pair it with `SetSimulationMode`
(see [Scene and simulation mode](#scene-and-simulation-mode)).

!!! tip "Tick-only stepping vs the full RL loop"
    `Step` always requires a registered external agent. A **tick-only** `Step` (empty
    `actions`) advances one gated tick using the in-engine policy, while a full `Step` also
    imposes the request's `actions` and returns an observation. Either way the agent must be
    registered. Without one, `GetAgentSchema` returns a zero-sized schema, `ResetAgent`
    returns `success=false` "External agents not ready", and `Step` is rejected with "Step
    requires a registered external agent". The engine logs a readiness diagnostic naming the
    cause. Stage out-of-band control with `MujocoSceneService.SetControl` / `SetQpos` or
    `AgentService.SetPolicyCommandFloat` between ticks as needed.

## The simulation loop

Actions in, one physics tick, observation out: the whole RL loop is `AgentService.Step`,
and the action gate is what makes each `Step` advance exactly one tick. Setup is three
calls. `GetAgentSchema` reports the observation and action sizes, `SetSimulationMode` takes
the sim off realtime (see [Scene and simulation mode](#scene-and-simulation-mode)), and
`ResetAgent` starts a fresh episode. All three, and `Step` itself, require a scene that
has registered an external RL agent. Until it is ready, `GetAgentSchema` returns a zero-sized
schema, `ResetAgent` is rejected, and `Step` is rejected with "Step requires a registered
external agent". These calls also mark the client connected, but the gate is not armed by the
RPC. It arms on the engine main thread, at the end of the post-reset settle period, once a
client has connected. That is about 20 ticks after `ResetAgent`. The loop body itself is `Step` alone.

```python
agent = rpc.AgentServiceStub(channel)
scene = rpc.SceneServiceStub(channel)

# Requires a registered external agent in the scene, plus the External Action
# Gate enabled in Scene Settings for strict lock-step (see above).
scene.SetSimulationMode(pb.SetSimulationModeRequest(
    mode=pb.SIMULATION_MODE_DETERMINISTIC_REALTIME))   # reproducible; use _HIGH_PERF to train faster
schema = agent.GetAgentSchema(pb.GetAgentSchemaRequest()).schema   # observation_size, action_size
agent.ResetAgent(pb.ResetAgentRequest())   # deactivates the gate, runs a ~20-tick settle, then re-arms

for _ in range(1000):
    # Each Step releases exactly one gated tick (this one also imposes the action;
    # an empty-actions Step would release a tick without imposing one, a "tick-only" step).
    resp = agent.Step(pb.StepRequest(actions=[0.0] * schema.action_size))
    observation = resp.observation.observations      # repeated float
    if resp.terminated or resp.truncated:
        agent.ResetAgent(pb.ResetAgentRequest())

agent.ReportProgress(pb.ProgressReport(finished=True))   # releases the gate cleanly
```

`StepRequest` carries the `actions` vector (sized to `action_size`, or left empty for a
tick-only step) plus three optional fields: `timeout_s`, `camera_requests`, and
`action_groups` for multi-policy control. An empty `agent_name` addresses the default agent. `StepResponse` returns the observation as an `AgentFrame` (`observations`,
`actions`, `frame_number`, `timestamp_ms`), the physics step duration, any requested
camera frames, and, once a task is negotiated, `reward_signals`, `terminated`, `truncated`,
`termination_flags`, and `info`. Multiple agents are addressed by name (`agent_0`,
`agent_1`, and so on); an empty name is the default agent.

### Let the engine keep time

One `Step` advances the simulation by exactly one control-runner tick, a fixed `dt` the
engine owns. The client calls `Step`; the engine decides how much sim time each tick is
worth.

**The wall-clock duration of a `Step` is irrelevant.** The TimeManager is strictly
deterministic, not real-time. A `Step` still advances *exactly one tick* of sim no matter
how long it takes to return, whether a microsecond or an hour. The IK solve, the number of
RPCs per tick, and host jitter make no difference. The engine simulates, observes, and
records entirely in **sim time**. A slow client only makes the run take longer on the wall
clock. The recorded data is byte-for-byte identical. The gate never lets the simulation
advance ahead of the client's `Step`s.

So a recorded artifact (jitter, stutter, uneven motion) comes from *what* is commanded in
sim time, not *how fast* it is sent. Per-tick command cadence, the recorder's framerate
relative to the control rate, and the per-`Step` `dt` are all sim-time quantities, read from
`state.time`.

!!! danger "Never pace the loop with a wall clock"
    Do not drive the control loop with `time.sleep`, `time.monotonic`, or any real-world
    clock, and do not compute or hardcode a control rate (`1 / hz`, `steps = seconds * hz`).
    Under the action gate the simulation only advances on a `Step`, so a wall-clock wait
    just stalls the client process while the sim sits frozen. In `DETERMINISTIC_HIGH_PERF`,
    sim time and real time are unrelated entirely. Pacing off the wall clock makes the number
    of `Step`s per phase depend on host speed and jitter, which silently breaks
    reproducibility. It was the single most common mistake in early integrations.

    Express every duration in the engine's own time instead:

    - **Count steps.** One `Step` is one tick. Advance a phase by stepping a fixed number of
      times. The tick rate is never needed to drive the loop.
    - **Read the sim clock.** `MujocoSceneService.GetFullState().state.time` is the engine's
      `mjData` simulation clock. To run a phase for *N* seconds of sim time, `Step` until that
      clock has advanced by *N*. The per-tick `dt` is then whatever the engine reports. A rate
      is never assumed, hardcoded, or measured.

    ```python
    # Advance ~2 s of SIM time using the engine clock only (no wall clock, no rate).
    def sim_time():
        return scene_mj.GetFullState(mj.GetFullStateRequest()).state.time

    end = sim_time() + 2.0
    while sim_time() < end:
        agent.Step(pb.StepRequest(actions=action))   # one tick; its dt is the engine's
    ```

!!! note "Not Python-specific"
    The loop above calls the generated stubs directly. It is the raw gRPC API, not the
    `luckyrobots` helper, and the helper (covered at the end of this page) is optional. The
    call sequence and the `StepRequest` / `StepResponse` fields are identical in every
    language. Generate stubs for Go, C++, or Rust as shown in
    [Connecting a client](#connecting-a-client), then call `GetAgentSchema`,
    `SetSimulationMode`, `ResetAgent`, and `Step` the same way. A single step can be
    exercised from the shell to confirm wiring, for example:

    ```bash
    grpcurl -plaintext -d '{"actions": [0, 0, 0]}' \
        127.0.0.1:50051 hazel.rpc.AgentService/Step
    ```

## Recording vs. observation frames { #recording-vs-observation-frames }

Two different things produce camera images, and conflating them is a common mistake.

- **`Step` camera frames are observations.** A `StepRequest.camera_requests` entry renders a
  fresh frame from a named scene camera **at that exact tick** and returns it in
  `StepResponse.camera_frames`, in lock-step with physics. It is the image a policy *sees* on
  that step, suitable as a network input. It is **not** a recording mechanism; do not stitch
  these per-step frames into a video.
- **The Observer is the recorder.** The engine-side [Observer](scripting/guides/recording.md)
  writes a structured dataset (state, actions, and camera video) to disk. It runs on its
  **own recorder time runner at its own rate** (set in the scene's Recorder settings). That
  rate is fully decoupled from the control/`Step` rate. Record at, say, 30 Hz while stepping the control
  loop at any rate. The two are independent, so any run can be recorded at any time without
  changing how it is stepped. Drive it from a C# script with `Observer.StartRecording()`,
  `Observer.EndCurrentEpisode(success)`, and `Observer.StopRecording()`.

## Reading and writing MuJoCo state

`MujocoSceneService` exposes the full MuJoCo model and data. `GetModelInfo` returns the
model dimensions and the joint and actuator descriptors. `GetFullState` returns `qpos`,
`qvel`, and `ctrl` (with optional filtering), and `StreamFullState` streams them.
`SetControl` writes actuator targets, `SetQpos` writes positions, and `ResetScene` snaps
the model back to its initial keyframe.

```python
import mujoco_scene_pb2 as mj, mujoco_scene_pb2_grpc as mjrpc

scene_mj = mjrpc.MujocoSceneServiceStub(channel)
info = scene_mj.GetModelInfo(mj.GetModelInfoRequest())     # nq, nv, nu, joints, actuators
scene_mj.SetControl(mj.SetControlRequest(bulk=[0.0] * info.nu))
```

`SetControl` is engine-wide and safety-gated: it rejects actuators owned by an active
policy slot or RL agent, so direct writes do not fight a running policy.

## Scene and simulation mode

`SceneService` controls play mode and the simulation rate. `EnterPlayMode` and
`ExitPlayMode` start and stop play from a client. In standalone builds these are no-ops,
since such builds are always running. `SetSimulationMode` selects how time advances:

| `SimulationMode` | Value | Behavior |
|------------------|-------|----------|
| `SIMULATION_MODE_NONDETERMINISTIC`        | 0 | Tracks the wall clock, dropping or repeating frames to keep pace. **Non-deterministic. Not for training.** |
| `SIMULATION_MODE_DETERMINISTIC_REALTIME`  | 1 | Fixed-step and reproducible, capped at one times real time. The default; for watchable runs. |
| `SIMULATION_MODE_DETERMINISTIC_HIGH_PERF` | 2 | Fixed-step and reproducible, as fast as the hardware allows. Best for training. |

!!! warning "Set the simulation mode for training"
    A scene starts in `SIMULATION_MODE_DETERMINISTIC_REALTIME`, which is fixed-step and
    reproducible but capped at one times real time. For fast training, call
    `SetSimulationMode` with `SIMULATION_MODE_DETERMINISTIC_HIGH_PERF` before stepping to run
    as fast as the hardware allows. Never select `SIMULATION_MODE_NONDETERMINISTIC`: it ties
    stepping to the wall clock and produces irreproducible episodes even with the action gate
    enabled.

Entering play triggers a MuJoCo recompile, so a brief readiness gap is normal. Poll
`GetAgentSchema` or `GetModelInfo` until it succeeds before driving the agent.

## Driving policies

The policy and motion-graph surface of `RobotControllerComponent` is exposed on
`AgentService`: `SetPolicyActive`, `SetPolicyCommandFloat` and `SetPolicyCommandBool`,
`SetPolicyDrivenJoints`, `SetPolicyPriority`, and the motion-graph calls
`SetMotionGraphActive`, `SetMotionGraphInput`, and `FireMotionGraphTrigger`.
`ListRobotControllers` and `ListPolicyDescriptors` discover what a scene exposes. This is
the same model documented in
[Controlling robots](scripting/guides/controlling-robots.md), reached over gRPC.

## Defining a task

For reinforcement learning, the engine computes observations, rewards, and terminations
from a negotiated task contract. Three calls drive it:

- `GetCapabilityManifest` lists the observation, reward, termination, and randomization
  components the engine knows about for a given robot and scene.
- `ValidateTaskContract` checks a contract without applying it.
- `NegotiateTask` validates and activates a contract. Once active, every `Step` returns the
  `reward_signals`, `terminated`, `truncated`, and `termination_flags` for that task.

Custom reward, observation, and termination components are defined in C# with the
`[MdpReward]`, `[MdpObservation]`, and `[MdpTermination]` attributes and discovered
automatically. The component reference is the
[`learn` API](scripting/api/learn/index.md).

## Python helper

For Python, the `luckyrobots` package wraps the generated stubs in a higher-level client,
so stubs do not need to be generated by hand. It is a convenience layer over the same gRPC
API.

```bash
pip install luckyrobots                # core client
pip install "luckyrobots[rl]"          # adds Gymnasium for LuckyEnv / PolicyEnv
```

```python
from luckyrobots import Session

with Session() as sess:
    sess.connect(timeout_s=30.0, robot="unitreego2")
    obs = sess.reset()
    for _ in range(1000):
        obs = sess.step(actions=[0.0] * len(obs.actions))
```

`Session.connect()` needs the robot name. `LuckyEnv` wraps a robot and scene in the
Gymnasium API (`reset`, `step`, `observation_space`, `action_space`), and `RobotController`
mirrors the policy calls from [Controlling robots](scripting/guides/controlling-robots.md).
The package ships pre-generated stubs, so everything it does is available over the raw API
above from any language.

## Where to go next

- [Controlling robots: policies and IK](scripting/guides/controlling-robots.md) is the
  policy and motion-graph model the policy calls mirror.
- [Recording](scripting/guides/recording.md) covers engine-side dataset recording to
  Parquet.
- [Get Started](get-started.md) installs the editor that hosts the server.
