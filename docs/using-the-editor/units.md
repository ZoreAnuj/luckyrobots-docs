---
icon: material/ruler
---

# Units

Lucky Engine uses **SI units** throughout. Distances are in metres, time in seconds,
mass in kilograms, forces in Newtons. Angles are **radians** in the script API and
**degrees** in the Inspector; the engine converts between them at the boundary.

!!! abstract "At a glance"
    | Quantity | Unit |
    |----------|------|
    | Distance, position, scale | metres (m) |
    | Time, durations           | seconds (s) |
    | Rotation in script        | radians (rad) |
    | Rotation in Inspector     | degrees (°) |
    | Mass                      | kilograms (kg) |
    | Force                     | Newtons (N) |
    | Linear velocity           | m/s |
    | Angular velocity          | rad/s |
    | Camera FOV                | degrees (°) |
    | Light intensity           | unitless multiplier |
    | Audio volume, pitch       | unitless multiplier |

## Distance and position

Translations, world positions, mesh sizes, and collider dimensions are all in **metres**.
`Entity.Translation`, `Entity.Scale`, the Inspector's Transform widget, MuJoCo body
positions, and Jolt rigid bodies all share the same scale.

```csharp
Translation = new Vector3(0.0f, 1.0f, 0.0f);   // one metre up
```

There are no centimetres or millimetres in the user-facing API.

## Time

Every duration the engine exposes is in **seconds**:

- `OnUpdate(float ts)` and the other per-step callbacks. `ts` is the runner's fixed step
  in seconds (for example `0.02` for a 50 Hz runner).
- `Timer.Set(callback, rateSeconds, looping, firstDelaySeconds)`.
- Animation lengths, audio durations.

No public API takes or returns milliseconds.

## Angles and rotation

Rotation is the one place where two units coexist:

- **Script API**: radians. `Entity.Rotation`, `TransformComponent.Rotation`, and
  `Entity.RotationQuat` (quaternion derived from radians).
- **Inspector**: degrees. The Transform widget reads, displays, and writes degrees; the
  editor converts to radians when it stores the value.
- **Motion-graph IK targets**: degrees. The `Target_Orientation` input on a motion graph
  expects degrees, so script code applies `Mathf.Rad2Deg` at the boundary.
- **Robot policy angular commands**: rad/s. `SetYawRate` on the walker takes radians
  per second.

Two helpers handle the conversions:

```csharp
float radians = degrees * Mathf.Deg2Rad;
float degrees = radians * Mathf.Rad2Deg;
```

`Mathf.WrapToPi(radians)` normalises an angle to the `[-π, π]` range.

!!! note "Why both?"
    Radians are the natural unit for math and the script API. Degrees are easier to read
    in the Inspector and in motion-graph editor fields. The conversion is small,
    consistent, and always at the boundary.

## Physics

| Quantity         | Unit  | Where it appears |
|------------------|-------|------------------|
| Mass             | kg    | `RigidBodyComponent.Mass` |
| Force            | N     | `RigidBodyComponent.AddForce` |
| Impulse          | N·s   | `RigidBodyComponent.AddImpulse` |
| Linear velocity  | m/s   | `RigidBodyComponent.LinearVelocity`, walker `SetVx`, `SetVy` |
| Angular velocity | rad/s | `RigidBodyComponent.AngularVelocity`, walker `SetYawRate` |

All physics solvers (Jolt, MuJoCo, Box2D) use these same units at the C# API boundary.

## Camera, lighting, audio

- **Camera vertical FOV**: degrees. The Inspector and `CameraComponent.SetVerticalFOV`
  both take degrees; an internal helper exposes the radian form when needed.
- **Light intensity** on `DirectionalLightComponent`, `PointLightComponent`, and
  `SpotLightComponent`: a unitless multiplier applied to the light's radiance. Not lux,
  not lumens.
- **Spot light cone angle**: degrees.
- **Audio volume and pitch** on `AudioComponent` (`VolumeMultiplier`, `PitchMultiplier`):
  unitless multipliers around `1.0`.

## Coordinate system

The editor world is **Y-up, right-handed**. Gravity acts in `-Y`. Transforms, camera,
and rendering all share that convention.

MuJoCo bodies (robots) run in MuJoCo's own coordinate space. The engine bridges between
the world and MuJoCo so the simulation stays consistent for both sides.

## Declaring units on a field

When a public field has a meaningful unit, decorate it with
[`[Units(...)]`](scripting/getting-started/editor-attributes.md#layout-and-labelling).
The Inspector appends the suffix to the label, so the unit is visible without forcing
script authors to spell it out in the field name:

```csharp
[Units("m/s")] public float Speed     = 0.5f;
[Units("rad")] public float TurnAngle = 0.0f;
[Units("s")]   public float Cooldown  = 1.0f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Speed<span class="units">(m/s)</span></span><div class="control"><div class="number">0.50</div></div></div>
<div class="field"><span class="label">Turn Angle<span class="units">(rad)</span></span><div class="control"><div class="number">0.00</div></div></div>
<div class="field"><span class="label">Cooldown<span class="units">(s)</span></span><div class="control"><div class="number">1.00</div></div></div>
</div>

The attribute is informational. It appends the suffix and nothing else; conversions stay
the script's responsibility.
