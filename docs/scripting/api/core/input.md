# Input

`static class` · namespace `Hazel`

```csharp
public static class Input
```

## Methods

### GetConnectedControllerIDs() {#m-getconnectedcontrollerids}

`static`

```csharp
public static int[] GetConnectedControllerIDs()
```

### GetControllerAxis(int, int) {#m-getcontrolleraxis}

`static`

```csharp
public static float GetControllerAxis(int id, int axis)
```

### GetControllerDeadzone(int, int) {#m-getcontrollerdeadzone}

`static`

```csharp
public static float GetControllerDeadzone(int id, int axis)
```

Getter for the specified controller's axis' deadzone, default value is 0.0f

### GetControllerHat(int, int) {#m-getcontrollerhat}

`static`

```csharp
public static byte GetControllerHat(int id, int hat)
```

### GetControllerName(int) {#m-getcontrollername}

`static`

```csharp
public static string GetControllerName(int id)
```

### GetCursorMode() {#m-getcursormode}

`static`

```csharp
public static CursorMode GetCursorMode()
```

### GetMousePosition() {#m-getmouseposition}

`static`

```csharp
public static Vector2 GetMousePosition()
```

### IsControllerButtonDown(int, GamepadButton) {#m-iscontrollerbuttondown}

`static`

```csharp
public static bool IsControllerButtonDown(int id, GamepadButton button)
```

Returns true every frame that the button is down. Equivalent to doing `Input.IsMouseButtonPressed(key) || Input.IsMouseButtonHeld(key)`

### IsControllerButtonDown(int, int) {#m-iscontrollerbuttondown-2}

`static`

```csharp
public static bool IsControllerButtonDown(int id, int button)
```

### IsControllerButtonHeld(int, GamepadButton) {#m-iscontrollerbuttonheld}

`static`

```csharp
public static bool IsControllerButtonHeld(int id, GamepadButton button)
```

Returns true every frame after the button was initially pressed (returns false when [`Input.IsMouseButtonPressed`](#m-ismousebuttonpressed) returns true)

### IsControllerButtonHeld(int, int) {#m-iscontrollerbuttonheld-2}

`static`

```csharp
public static bool IsControllerButtonHeld(int id, int button)
```

### IsControllerButtonPressed(int, GamepadButton) {#m-iscontrollerbuttonpressed}

`static`

```csharp
public static bool IsControllerButtonPressed(int id, GamepadButton button)
```

Returns true during the frame that the button was released

### IsControllerButtonPressed(int, int) {#m-iscontrollerbuttonpressed-2}

`static`

```csharp
public static bool IsControllerButtonPressed(int id, int button)
```

### IsControllerButtonReleased(int, GamepadButton) {#m-iscontrollerbuttonreleased}

`static`

```csharp
public static bool IsControllerButtonReleased(int id, GamepadButton button)
```

Returns true during the frame that the button was released

### IsControllerButtonReleased(int, int) {#m-iscontrollerbuttonreleased-2}

`static`

```csharp
public static bool IsControllerButtonReleased(int id, int button)
```

### IsControllerPresent(int) {#m-iscontrollerpresent}

`static`

```csharp
public static bool IsControllerPresent(int id)
```

### IsKeyDown(KeyCode) {#m-iskeydown}

`static`

```csharp
public static bool IsKeyDown(KeyCode keycode)
```

Returns true every frame that the key is down. Equivalent to doing `Input.IsKeyPressed(key) || Input.IsKeyHeld(key)`

### IsKeyHeld(KeyCode) {#m-iskeyheld}

`static`

```csharp
public static bool IsKeyHeld(KeyCode keycode)
```

Returns true every frame after the key was initially pressed (returns false when [`Input.IsKeyPressed`](#m-iskeypressed) returns true)

### IsKeyPressed(KeyCode) {#m-iskeypressed}

`static`

```csharp
public static bool IsKeyPressed(KeyCode keycode)
```

Returns true the first frame that the key represented by the given KeyCode is pressed down

### IsKeyReleased(KeyCode) {#m-iskeyreleased}

`static`

```csharp
public static bool IsKeyReleased(KeyCode keycode)
```

Returns true during the frame that the key was released

### IsKeyToggledOn(KeyCode) {#m-iskeytoggledon}

`static`

```csharp
public static bool IsKeyToggledOn(KeyCode keycode)
```

Returns true if one of the toggleable keys (CapsLock, NumLock, ScrollLock) is toggled on. If keycode is not one of these keys, this will return false.

**Parameters**

| Name | Description |
|------|-------------|
| `keycode` |  |

### IsMouseButtonDown(MouseButton) {#m-ismousebuttondown}

`static`

```csharp
public static bool IsMouseButtonDown(MouseButton button)
```

Returns true every frame that the button is down. Equivalent to doing `Input.IsMouseButtonPressed(key) || Input.IsMouseButtonHeld(key)`

### IsMouseButtonHeld(MouseButton) {#m-ismousebuttonheld}

`static`

```csharp
public static bool IsMouseButtonHeld(MouseButton button)
```

Returns true every frame after the button was initially pressed (returns false when [`Input.IsMouseButtonPressed`](#m-ismousebuttonpressed) returns true)

### IsMouseButtonPressed(MouseButton) {#m-ismousebuttonpressed}

`static`

```csharp
public static bool IsMouseButtonPressed(MouseButton button)
```

Returns true the first frame that the button represented by the given MouseButton is pressed down

### IsMouseButtonReleased(MouseButton) {#m-ismousebuttonreleased}

`static`

```csharp
public static bool IsMouseButtonReleased(MouseButton button)
```

Returns true during the frame that the button was released

### SetControllerDeadzone(int, int, float) {#m-setcontrollerdeadzone}

`static`

```csharp
public static void SetControllerDeadzone(int id, int axis, float deadzone)
```

Setter for the specified controller's axis' deadzone, default value is 0.0f

### SetCursorMode(CursorMode) {#m-setcursormode}

`static`

```csharp
public static void SetCursorMode(CursorMode mode)
```


---
<small>Source: `Hazel/Core/Input.cs`</small>
