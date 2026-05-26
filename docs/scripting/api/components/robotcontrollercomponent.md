# RobotControllerComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class RobotControllerComponent : Component
```

## Methods

### ClearPolicyAction(uint) {#m-clearpolicyaction}

```csharp
public void ClearPolicyAction(uint slotId)
```

### ClearPolicyGains(uint) {#m-clearpolicygains}

```csharp
public void ClearPolicyGains(uint slotId)
```

### GetBool(uint, uint) {#m-getbool}

```csharp
public bool GetBool(uint slotId, uint commandId)
```

### GetFloat(uint, uint) {#m-getfloat}

```csharp
public float GetFloat(uint slotId, uint commandId)
```

### GetInputBool(Identifier) {#m-getinputbool}

```csharp
public bool GetInputBool(Identifier inputID)
```

### GetInputFloat(Identifier) {#m-getinputfloat}

```csharp
public float GetInputFloat(Identifier inputID)
```

### GetInputInt(Identifier) {#m-getinputint}

```csharp
public int GetInputInt(Identifier inputID)
```

### GetInputVector3(Identifier) {#m-getinputvector3}

```csharp
public Vector3 GetInputVector3(Identifier inputID)
```

### GetPolicyBasePose(uint, float, float, float, float, float, float) {#m-getpolicybasepose}

```csharp
public bool GetPolicyBasePose(uint slotId, out float x, out float y, out float yaw, out float xHz, out float zHz, out float yawHz)
```

### GetPolicyLastAction(uint, float[]) {#m-getpolicylastaction}

```csharp
public int GetPolicyLastAction(uint slotId, float[] outBuffer)
```

### GetPolicyLastActionSize(uint) {#m-getpolicylastactionsize}

```csharp
public int GetPolicyLastActionSize(uint slotId)
```

### GetPolicySlotsSummaryJson() {#m-getpolicyslotssummaryjson}

```csharp
public string GetPolicySlotsSummaryJson()
```

### IsMotionGraphActive() {#m-ismotiongraphactive}

```csharp
public bool IsMotionGraphActive()
```

### IsPolicyActive(uint) {#m-ispolicyactive}

```csharp
public bool IsPolicyActive(uint slotId)
```

### SetBool(uint, uint, bool) {#m-setbool}

```csharp
public void SetBool(uint slotId, uint commandId, bool value)
```

### SetDrivenJoints(uint, string[]) {#m-setdrivenjoints}

```csharp
public void SetDrivenJoints(uint slotId, string[] jointNames)
```

### SetFloat(uint, uint, float) {#m-setfloat}

```csharp
public void SetFloat(uint slotId, uint commandId, float value)
```

### SetInputBool(Identifier, bool) {#m-setinputbool}

```csharp
public void SetInputBool(Identifier inputID, bool value)
```

### SetInputFloat(Identifier, float) {#m-setinputfloat}

```csharp
public void SetInputFloat(Identifier inputID, float value)
```

### SetInputInt(Identifier, int) {#m-setinputint}

```csharp
public void SetInputInt(Identifier inputID, int value)
```

### SetInputTrigger(Identifier) {#m-setinputtrigger}

```csharp
public void SetInputTrigger(Identifier inputID)
```

### SetInputVector3(Identifier, Vector3) {#m-setinputvector3}

```csharp
public void SetInputVector3(Identifier inputID, Vector3 value)
```

### SetMotionGraphActive(bool) {#m-setmotiongraphactive}

```csharp
public void SetMotionGraphActive(bool active)
```

### SetPolicyAction(uint, float[], bool) {#m-setpolicyaction}

```csharp
public int SetPolicyAction(uint slotId, float[] action, bool hold = true)
```

### SetPolicyActive(uint, bool) {#m-setpolicyactive}

```csharp
public void SetPolicyActive(uint slotId, bool active)
```

### SetPolicyClampObservation(uint, bool) {#m-setpolicyclampobservation}

```csharp
public void SetPolicyClampObservation(uint slotId, bool clamp)
```

### SetPolicyDescriptor(uint, string) {#m-setpolicydescriptor}

```csharp
public void SetPolicyDescriptor(uint slotId, string descriptorPath)
```

### SetPolicyGains(uint, string[], float[]?, float[]?, float[]?, float[]?, float[]?) {#m-setpolicygains}

```csharp
public void SetPolicyGains(uint slotId, string[] jointNames, float[]? kp = null, float[]? kd = null, float[]? effortLimit = null, float[]? actionScale = null, float[]? defaultPos = null)
```

### SetPolicyPriority(uint, int) {#m-setpolicypriority}

```csharp
public void SetPolicyPriority(uint slotId, int priority)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
