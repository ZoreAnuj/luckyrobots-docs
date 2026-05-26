# Input & Controls

All input is polled through the static [`Input`](../api/core/input.md) class, typically
from [`OnUpdate`](../api/scene/entity.md#m-onupdate).

## Keyboard

Keys are identified by the `KeyCode` enum. There are four query styles:

| Method | Returns true… |
|--------|---------------|
| [`Input.IsKeyPressed(key)`](../api/core/input.md#m-iskeypressed) | …on the first frame the key goes down. |
| [`Input.IsKeyHeld(key)`](../api/core/input.md#m-iskeyheld) | …every frame after that initial press. |
| [`Input.IsKeyDown(key)`](../api/core/input.md#m-iskeydown) | …every frame the key is down (pressed **or** held). |
| [`Input.IsKeyReleased(key)`](../api/core/input.md#m-iskeyreleased) | …on the frame the key is released. |

```csharp
protected override void OnUpdate(float ts)
{
    if (Input.IsKeyPressed(KeyCode.Space))
        Jump();                       // fires once per press

    if (Input.IsKeyDown(KeyCode.W))
        MoveForward(ts);              // fires while held
}
```

Use **`IsKeyPressed`** for one-shot actions (jump, fire, toggle) and **`IsKeyDown`**
for continuous actions (movement).

## Mouse

`Input` exposes the equivalent mouse-button queries
(`IsMouseButtonPressed`/`Held`/`Down`/`Released`) plus
[`GetMousePosition()`](../api/core/input.md#m-getmouseposition). See the
[`Input` reference](../api/core/input.md) for the complete list.

## Controllers

Connected gamepads are queried by id — see
[`GetConnectedControllerIDs()`](../api/core/input.md#m-getconnectedcontrollerids),
`GetControllerAxis`, and `IsControllerButtonDown` on the
[`Input` reference](../api/core/input.md).
