# RobotController

`struct` · namespace `Hazel`

```csharp
public readonly struct RobotController
```

## Constructors

### RobotController(RobotControllerComponent) {#m-robotcontroller}

```csharp
public RobotController(RobotControllerComponent component)
```

## Methods

### GetBool(uint, uint) {#m-getbool}

```csharp
public bool GetBool(uint slotId, uint commandId)
```

### GetBool<TSlot, TCmd>(TSlot, TCmd) {#m-getbool-2}

```csharp
public bool GetBool<TSlot, TCmd>(TSlot slotId, TCmd commandId) where TSlot : unmanaged, Enum where TCmd : unmanaged, Enum
```

### GetFloat(uint, uint) {#m-getfloat}

```csharp
public float GetFloat(uint slotId, uint commandId)
```

### GetFloat<TSlot, TCmd>(TSlot, TCmd) {#m-getfloat-2}

```csharp
public float GetFloat<TSlot, TCmd>(TSlot slotId, TCmd commandId) where TSlot : unmanaged, Enum where TCmd : unmanaged, Enum
```

### IsMotionGraphActive() {#m-ismotiongraphactive}

```csharp
public bool IsMotionGraphActive()
```

### IsPolicyActive(uint) {#m-ispolicyactive}

```csharp
public bool IsPolicyActive(uint slotId)
```

### IsPolicyActive<TSlot>(TSlot) {#m-ispolicyactive-2}

```csharp
public bool IsPolicyActive<TSlot>(TSlot slotId) where TSlot : unmanaged, Enum
```

### SetBool(uint, uint, bool) {#m-setbool}

```csharp
public void SetBool(uint slotId, uint commandId, bool value)
```

### SetBool<TSlot, TCmd>(TSlot, TCmd, bool) {#m-setbool-2}

```csharp
public void SetBool<TSlot, TCmd>(TSlot slotId, TCmd commandId, bool value) where TSlot : unmanaged, Enum where TCmd : unmanaged, Enum
```

### SetDrivenJoints(uint, string[]) {#m-setdrivenjoints}

```csharp
public void SetDrivenJoints(uint slotId, string[] jointNames)
```

### SetDrivenJoints<TSlot>(TSlot, string[]) {#m-setdrivenjoints-2}

```csharp
public void SetDrivenJoints<TSlot>(TSlot slotId, string[] jointNames) where TSlot : unmanaged, Enum
```

### SetFloat(uint, uint, float) {#m-setfloat}

```csharp
public void SetFloat(uint slotId, uint commandId, float value)
```

### SetFloat<TSlot, TCmd>(TSlot, TCmd, float) {#m-setfloat-2}

```csharp
public void SetFloat<TSlot, TCmd>(TSlot slotId, TCmd commandId, float value) where TSlot : unmanaged, Enum where TCmd : unmanaged, Enum
```

### SetMotionGraphActive(bool) {#m-setmotiongraphactive}

```csharp
public void SetMotionGraphActive(bool active)
```

### SetPolicyActive(uint, bool) {#m-setpolicyactive}

```csharp
public void SetPolicyActive(uint slotId, bool active)
```

### SetPolicyActive<TSlot>(TSlot, bool) {#m-setpolicyactive-2}

```csharp
public void SetPolicyActive<TSlot>(TSlot slotId, bool active) where TSlot : unmanaged, Enum
```

## Operators

### implicit operator RobotController {#m-implicit-operator-robotcontroller}

`static`

```csharp
public static implicit operator RobotController(RobotControllerComponent c)
```


---
<small>Source: `Hazel/Robots/RobotController.cs`</small>
