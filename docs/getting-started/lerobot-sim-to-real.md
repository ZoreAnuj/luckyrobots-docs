---
icon: material/robot
---

# LeRobot: Sim-to-Real Tutorial

<div style="background:var(--md-default-bg-color--light);border:1px solid var(--md-default-fg-color--lightest);border-radius:14px;padding:36px 32px 28px;margin:24px 0 32px;">
  <p style="font-size:0.72rem;font-weight:700;letter-spacing:0.1em;text-transform:uppercase;color:var(--md-default-fg-color--light);margin:0 0 14px;text-align:center;">Record in sim &nbsp;·&nbsp; Deploy on real</p>
  <p style="font-size:clamp(1.1rem,2vw,1.35rem);font-weight:700;line-height:1.3;text-align:center;margin:0 0 28px;letter-spacing:-0.01em;color:var(--md-default-fg-color);">SO-100 Sim2Real <span style="text-decoration:underline;text-decoration-color:var(--md-default-fg-color--light);text-decoration-thickness:2px;text-underline-offset:4px;">without</span> the Teleop</p>
  <div style="display:grid;grid-template-columns:1fr 1fr;gap:16px;margin-bottom:20px;">
    <figure style="margin:0;border-radius:10px;overflow:hidden;border:1px solid var(--md-default-fg-color--lightest);">
      <video autoplay muted loop playsinline preload="metadata" style="display:block;width:100%;background:#000;" src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/hero_teleop.mp4"></video>
      <figcaption style="padding:10px 14px 12px;font-size:0.85rem;text-align:center;color:var(--md-default-fg-color);"><strong>Manual leader-arm teleop.</strong><br><span style="color:var(--md-default-fg-color--light);">One human, one demo at a time.</span></figcaption>
    </figure>
    <figure style="margin:0;border-radius:10px;overflow:hidden;border:1px solid var(--md-default-fg-color--lightest);">
      <video autoplay muted loop playsinline preload="metadata" style="display:block;width:100%;background:#000;" src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/hero_sim.mp4"></video>
      <figcaption style="padding:10px 14px 12px;font-size:0.85rem;text-align:center;color:var(--md-default-fg-color);"><strong>Scripted in LuckyEngine.</strong><br><span style="color:var(--md-default-fg-color--light);">Hands-free, repeatable, same dataset format.</span></figcaption>
    </figure>
  </div>
  <p style="text-align:center;font-size:0.95rem;margin:0 0 16px;color:var(--md-default-fg-color);"><span style="color:var(--md-default-fg-color--light);text-decoration:line-through;">by hand, one at a time</span> &nbsp;<strong style="color:var(--lr-green);">→</strong>&nbsp; <strong>by script, as many as you need</strong></p>
  <p style="text-align:center;margin:0;"><span style="display:inline-block;background:var(--lr-green);color:#1a1a1a;font-weight:700;font-size:1rem;border-radius:999px;padding:10px 24px;">72% success on the real SO-100 — trained only on sim recordings.</span></p>
</div>

**What you'll build:**

- A LeRobot 3.0 dataset recorded entirely inside LuckyEngine
- An ACT policy trained on that dataset (~2–6 hours on a consumer GPU)
- A 30 Hz inference loop running on the physical SO-100

---

## Prerequisites

- LuckyEngine scene already built with a robot agent in it
- Python 3.10–3.12
- Any CUDA GPU for training; LE renders on the host GPU
- `pip install lerobot luckyrobots torch grpcio numpy` (LeRobot ≥ 3.0)
- For §4 (Genesis sim-to-sim): `pip install genesis-world`

---

## 1 · Record demos in LE

LE demos are produced by **scripted C# scene scripts** — no human teleop. The script drives the robot through waypoints; the engine writes a LeRobot 3.0 dataset to disk in parallel.

<video class="doc-video" controls preload="metadata"
  src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/recording_walkthrough.mp4">
</video>

<video class="doc-video" controls preload="metadata"
  src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/so100_sim.mp4">
</video>

### Start a recording

See `Assets/ContentVault/Examples/SO100 Pick And Place/SO100PickAndPlace.cs` for a complete example. The minimum:

```csharp
Observer.RegisterTask(0, "Pick up the lego block and place it on the target");
Observer.StartRecording();

foreach (var episode in episodes) {
    DriveSO100ToWaypoints();
    bool ok = CheckTaskSuccess();
    Observer.EndCurrentEpisode(ok);
    ResetSceneForNextEpisode();
}

Observer.StopRecording();
```

### What ends up on disk

```
session_<ts>/
├── data/chunk-NNN/file-NNN.parquet
├── videos/observation.images.<cam>/
└── meta/info.json + stats.json + tasks.parquet + episodes/
```

| Field | Description |
|---|---|
| `action` | `float32` per actuator (`m->nu`) |
| `observation.state` | All actuated joint qpos — trim to what your policy needs |
| `observation.images.<cam>` | `uint8` RGB as h264 mp4 chunks |


---

## 2 · Train (ACT)

```bash
lerobot-train \
  --dataset.root=path/to/your_session \
  --dataset.repo_id=local/your_session \
  --policy.type=act \
  --policy.chunk_size=100 \
  --batch_size=8 \
  --steps=100000 \
  --output_dir=outputs/act_my_task \
  --wandb.enable=true
```

| Parameter | Notes |
|---|---|
| `policy.chunk_size` | Actions per forward pass. 100 is a good default. |
| `policy.kl_weight` | CVAE KL term. Default 10; lower if the latent collapses. |
| `steps` | ~100k for ~200 demos on a tabletop task. |

Wall-clock: **2–6 hours** on an RTX 3090/4070/4090. Keep at least the last few checkpoints — best performance is rarely the final step.

---

## 3 · Evaluate in LE (in-domain)

Close the loop in the same simulator you trained in using the [`luckyrobots`](https://github.com/luckyrobots/luckyrobots) SDK. `step()` returns a synchronous `ObservationResponse` — state and camera frames together, no async stream to race against.

<video class="doc-video" controls preload="metadata"
  src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/le_finalist_trials.mp4">
</video>

??? "Full in-domain eval loop (Python)"

    ```python
    from luckyrobots import Session

    with Session(host="127.0.0.1", port=50051) as session:
        session.start(scene="my_scene", robot="my_robot", task="my_task")
        session.configure_cameras([
            {"name": "CameraFront",  "width": 96, "height": 96},
            {"name": "LaptopCamera", "width": 96, "height": 96},
        ])

        policy, pre, post = load_policy(checkpoint_dir)
        state_dim = 6

        for trial in range(N_TRIALS):
            obs_resp = session.reset()
            for _ in range(SETTLE_STEPS):
                obs_resp = session.step(HOME_ACTION)
            for step in range(MAX_STEPS):
                state  = obs_resp.observation[:state_dim]
                frames = {cf.name: cf.image for cf in obs_resp.camera_frames}
                action = predict(policy, pre, post, build_obs(state, frames))
                obs_resp = session.step(action.tolist())
                if success(obs_resp):
                    break
    ```

!!! note "Common gotchas"
    - `task` is required in `session.start(...)`.
    - State is at `obs_resp.observation` (a flat `list[float]`). Slice the first `state_dim` entries.
    - Camera frames are in the same response after `configure_cameras(...)` — no separate stream.

---

## 4 · Evaluate in Genesis (sim-to-sim)

Run the same checkpoint in [Genesis](https://github.com/Genesis-Embodied-AI/Genesis) — a different physics solver and renderer — as a cheap generalization probe before touching real hardware.

1. **Convert axes.** LE is Y-up, Genesis is Z-up: `hz_to_gs(p) = (p[0], -p[2], p[1])`
2. **Reuse the MJCF.** Pass the same `so_arm100.xml` and match the home-pose keyframe.
3. **Place cameras by intrinsics.** Same position, lookat, FOV, and 96×96 size.
4. **Rebuild task objects.** Close enough — you're testing generalization, not pixel parity.

<video class="doc-video" controls preload="metadata"
  src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/genesis_sim2sim.mp4">
</video>

---

## 5 · Evaluate on the real robot

<video class="doc-video" controls preload="metadata"
  src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/sim2real_real_robot.mp4">
</video>

| Stage | Latency |
|---|---|
| Camera `async_read` + joint encoder | ~5–8 ms |
| Preprocess (resize 96×96, state units) | ~2–3 ms |
| `policy.predict` (EMA + AMP) | 9–17 ms |
| Action mapper + safety scan | <1 ms |
| `robot.send_position` @ 30 Hz | ~2–4 ms |
| **Total budget** | **33 ms** |

1. **Match cameras & rate.** Same resolution, mounting, and Hz as your LE scene. If you mirrored a camera in LE, apply `cv2.flip(..., 1)` — without it the policy sees a mirror-image arm and fails silently.
2. **Match action units.** Verify radians↔degrees conversion and dataset min/max against calibrated joint range.
3. **Calibrate joint zeros.** Re-run so "0 rad in dataset" maps to physical home consistently.
4. **Add a safety scan.** Abort (don't clip silently) if the policy drifts off-distribution.

!!! warning "33 ms is tight, but achievable"
    Use `async_read` camera backends so I/O overlaps the previous step's policy call. If you can't hold 30 Hz, drop to 20 Hz *uniformly* — jitter hurts more than a lower steady rate.

??? "Full real-robot inference loop (Python)"

    ```python
    from lerobot.robots.so_follower.so_follower import SOFollower
    from lerobot.robots.so_follower.config_so_follower import SOFollowerRobotConfig
    from lerobot.cameras.realsense.camera_realsense import RealSenseCamera
    from lerobot.cameras.realsense.configuration_realsense import RealSenseCameraConfig

    follower = SOFollower(SOFollowerRobotConfig(port="COM5", id="my_follower", use_degrees=True))
    follower.connect(calibrate=True)

    cam_a = RealSenseCamera(RealSenseCameraConfig(serial_number_or_name=SERIAL_A, fps=30, width=640, height=480))
    cam_b = RealSenseCamera(RealSenseCameraConfig(serial_number_or_name=SERIAL_B, fps=30, width=640, height=480))
    cam_a.connect(); cam_b.connect()

    policy, pre, post = load_checkpoint(CKPT_DIR)
    mapper = ActionMapper(stats_path=STATS, calib_path=CALIB)

    dt = 1.0 / 30.0
    while step < max_steps:
        t0 = time.perf_counter()
        obs = follower.get_observation()
        state_real  = np.array([obs[f"{j}.pos"] for j in JOINT_NAMES], dtype=np.float32)
        state_train = mapper.state_to_training(state_real)
        img_a = cv2.flip(cam_a.async_read(), 1)
        img_b = cam_b.async_read()
        frames = {
            "LaptopCamera": cv2.resize(img_a, (96, 96), interpolation=cv2.INTER_LINEAR),
            "CameraFront":  cv2.resize(img_b, (96, 96), interpolation=cv2.INTER_LINEAR),
        }
        action_rad, _, outside = mapper.action_to_real(policy.predict(state_train, frames))
        if outside: abort("action outside calibrated range")
        follower.send_action({f"{n}.pos": float(v) for n, v in zip(JOINT_NAMES, action_rad)})
        precise_sleep(dt - (time.perf_counter() - t0))
    ```

---

## 6 · Case study — what we shipped

??? "Results breakdown"

    200 LE-recorded SO-100 episodes, 30 Hz, 96×96, two cameras. Vanilla IMLE policy with ResNet18 + SpatialSoftmax encoder, 6-DOF joint state, 1D U-Net generator (~66 M params).

    **Vanilla IMLE — LE finalist eval (25 trials × 3 checkpoints)**

    | Checkpoint | Success | Lift | Grasp |
    |---|---|---|---|
    | `020160` | 52% | 72% | 68% |
    | `022400` | 60% | 92% | 92% |
    | `044800` | **84%** | 96% | 96% |

    **The transfer cliff:** checkpoint `044800` dropped from 84% in LE to **67% in Genesis** zero-shot — 17 pp gap from renderer differences (colour cast, micro-shading, edge sharpness).

    **DICE v3** closed the gap: a frozen encoder pretrained on ~21k paired LE↔Real frames, producing L2-normalized (B, 128, 6, 6) grids with dense InfoNCE + VICReg + DANN losses.

    **DICE-IMLE — Genesis sim2sim (25 trials)**

    | Checkpoint | Success | Lift | Grasp |
    |---|---|---|---|
    | `006280` | **76%** | 80% | 84% |
    | `009430` | 64% | 68% | 72% |

    **DICE-IMLE — Real SO-100 (25 trials each)**

    | Checkpoint step | Success |
    |---|---|
    | 18 000 | 60% |
    | 19 500 | **72%** |

    <video class="doc-video" controls preload="metadata"
      src="https://zoreanuj.github.io/lerobot-luckyengine-tutorial/videos/sim2real_real_robot.mp4">
    </video>

---

## Where to go next

- **[gRPC API](../../api-reference/grpc-api.md)** — full SDK reference for `Session`, `step()`, and `ObservationResponse`
- **[LeRobot docs](https://huggingface.co/docs/lerobot)** — policy training reference
- **[Genesis](https://github.com/Genesis-Embodied-AI/Genesis)** — sim-to-sim transfer
- **[ACT paper](https://arxiv.org/abs/2304.13705)** — Zhao et al. 2023
