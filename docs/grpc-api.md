# gRPC API

Lucky Engine runs an in-process gRPC server. It is the primary programmatic interface to
the engine: an external process steps the simulation, reads observations, sends actions
and policy commands, and resets episodes over gRPC. The interface is language agnostic, so
a client can be written in Python, C++, Go, Rust, or anything with a gRPC stack. Python
also has a convenience wrapper, covered at the end of this page.

!!! abstract "In short"
    Start the **gRPC Server** panel in the editor and press Play. The server listens on
    `127.0.0.1:50051` (loopback, no TLS). Generate client stubs from the shipped `.proto`
    files, or explore the API live with reflection. The reinforcement-learning loop is one
    call: `AgentService.Step`.

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

## The simulation loop

Actions in, one physics tick, observation out: the whole RL loop is `AgentService.Step`.
Two calls set it up. `GetAgentSchema` reports the observation and action sizes, and
`ResetAgent` starts a fresh episode.

```python
agent = rpc.AgentServiceStub(channel)

schema = agent.GetAgentSchema(pb.GetAgentSchemaRequest()).schema   # observation_size, action_size
agent.ResetAgent(pb.ResetAgentRequest())

for _ in range(1000):
    resp = agent.Step(pb.StepRequest(actions=[0.0] * schema.action_size))
    observation = resp.observation.observations      # repeated float
    if resp.terminated or resp.truncated:
        agent.ResetAgent(pb.ResetAgentRequest())
```

`StepRequest` carries the `actions` vector (sized to `action_size`), an optional
`timeout_s`, optional `camera_requests`, and optional `action_groups` for multi-policy
control. `StepResponse` returns the observation as an `AgentFrame` (`observations`,
`actions`, `frame_number`, `timestamp_ms`), the physics step duration, any requested
camera frames, and, once a task is negotiated, `reward_signals`, `terminated`, `truncated`,
`termination_flags`, and `info`. An empty `agent_name` addresses the default agent;
multiple agents are addressed by name (`agent_0`, `agent_1`, and so on).

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
| `SIMULATION_MODE_REALTIME`      | 0 | Tracks the real-time clock. The default. |
| `SIMULATION_MODE_DETERMINISTIC` | 1 | Deterministic, capped at one times real time. |
| `SIMULATION_MODE_FAST`          | 2 | As fast as the hardware allows. Best for training. |

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
