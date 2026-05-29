# Input & Controls

All input is polled through the static [`Input`](../api/core/input.md) class, typically
from [`OnUpdate`](../api/scene/entity.md#m-onupdate). The scripts that reach for it most
often are data-collection controllers (start and stop recording, mark episode outcomes,
switch tasks) and robot driver scripts (WASD-style locomotion control).

## Keyboard

Keys are identified by the [`KeyCode`](../api/core/keycode.md) enum. Four query styles
cover the common needs:

| Method | Returns true… |
|--------|---------------|
| [`Input.IsKeyPressed(key)`](../api/core/input.md#m-iskeypressed) | …on the first frame the key goes down. |
| [`Input.IsKeyHeld(key)`](../api/core/input.md#m-iskeyheld) | …every frame after that initial press. |
| [`Input.IsKeyDown(key)`](../api/core/input.md#m-iskeydown) | …every frame the key is down (pressed **or** held). |
| [`Input.IsKeyReleased(key)`](../api/core/input.md#m-iskeyreleased) | …on the frame the key is released. |

The right choice is almost always **`IsKeyPressed`** for one-shot actions and
**`IsKeyDown`** for continuous ones:

```csharp
using Hazel;
using Hazel.Data;

protected override void OnUpdate(float ts)
{
    // One-shot recording controls: each press triggers exactly once.
    if (Input.IsKeyPressed(KeyCode.Enter))
        Observer.StartRecording();
    if (Input.IsKeyPressed(KeyCode.X))
        Observer.EndCurrentEpisode(success: false);
    if (Input.IsKeyPressed(KeyCode.Y))
        Observer.EndCurrentEpisode(success: true);
    if (Input.IsKeyPressed(KeyCode.Escape))
        Observer.StopRecording();

    // Continuous robot drive: every frame while held.
    if (Input.IsKeyDown(KeyCode.W))
        DriveForward(ts);
    if (Input.IsKeyDown(KeyCode.A))
        TurnLeft(ts);
}
```

Reach for `IsKeyPressed` when one press should map to one action: starting a session,
closing an episode, switching to a task index, resetting the scene. Reach for
`IsKeyDown` or `IsKeyHeld` when the action should happen every frame the key is held:
driving a robot, scrubbing a value, holding a finger gripper closed.

!!! tip "`IsKeyHeld` vs `IsKeyDown`"
    `IsKeyDown` is the union of pressed-and-held. `IsKeyHeld` excludes the very first
    frame of the press. Both are fine for continuous actions; `IsKeyDown` is the more
    common choice and matches every other engine's "is the key down right now".

## Common bindings

There are no engine-defined keybindings for script-level work. These are conventions
worth following so users see consistent controls across scenes:

| Key | Common use |
|-----|------------|
| ++enter++              | Start a recording session |
| ++x++                  | End the current episode as failed |
| ++y++                  | End the current episode as successful |
| ++escape++             | Stop recording |
| ++r++                  | Reset the scene to its starting state |
| ++space++              | Pause / step / toggle a debug flag |
| ++1++ – ++9++          | Switch task index (with `Observer.SetCurrentTaskIndex`) |
| ++w++ ++a++ ++s++ ++d++ | Drive a robot |

See [Recording with the Observer](recording.md) for the full episode-lifecycle API and
[Getting started](../getting-started/index.md) for the WASD walker example.

## Mouse

`Input` exposes the same four query styles for mouse buttons —
`IsMouseButtonPressed`, `IsMouseButtonHeld`, `IsMouseButtonDown`,
`IsMouseButtonReleased` — plus
[`GetMousePosition()`](../api/core/input.md#m-getmouseposition) for the cursor.
Mouse buttons are identified by the [`MouseButton`](../api/core/mousebutton.md) enum.

```csharp
if (Input.IsMouseButtonPressed(MouseButton.Left))
    PickAt(Input.GetMousePosition());
```

See the [`Input` reference](../api/core/input.md) for the complete list including the
mouse scroll wheel.

## Controllers

Connected gamepads are queried by id:

- [`Input.GetConnectedControllerIDs()`](../api/core/input.md#m-getconnectedcontrollerids)
  returns the IDs currently plugged in.
- `Input.IsControllerButtonDown(id, button)` and
  `Input.GetControllerAxis(id, axis)` read button and stick state.

Buttons and axes use the
[`GamepadButton`](../api/core/gamepadbutton.md) enum and matching axis identifiers.

A gamepad driver script for a robot then looks the same shape as the keyboard one,
swapping `Input.IsKeyDown` for `Input.GetControllerAxis(id, axis)` on the stick that
should drive forward velocity.
