# RigidBodyComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class RigidBodyComponent : Component
```

## Properties

### AngularDrag {#m-angulardrag}

```csharp
public float AngularDrag { get; set; }
```

### AngularVelocity {#m-angularvelocity}

```csharp
public Vector3 AngularVelocity { get; set; }
```

### BodyType {#m-bodytype}

```csharp
public EBodyType BodyType { get; set; }
```

### IsGravityEnabled {#m-isgravityenabled}

```csharp
public bool IsGravityEnabled { get; set; }
```

### IsSleeping {#m-issleeping}

```csharp
public bool IsSleeping { get; set; }
```

### IsTrigger {#m-istrigger}

```csharp
public bool IsTrigger { get; set; }
```

### Layer {#m-layer}

```csharp
public uint Layer { get; set; }
```

### LayerName {#m-layername}

```csharp
public string LayerName { get; set; }
```

### LinearDrag {#m-lineardrag}

```csharp
public float LinearDrag { get; set; }
```

### LinearVelocity {#m-linearvelocity}

```csharp
public Vector3 LinearVelocity { get; set; }
```

### Mass {#m-mass}

```csharp
public float Mass { get; set; }
```

### MaxAngularVelocity {#m-maxangularvelocity}

```csharp
public float MaxAngularVelocity { get; set; }
```

### MaxLinearVelocity {#m-maxlinearvelocity}

```csharp
public float MaxLinearVelocity { get; set; }
```

## Methods

### AddForce(Vector3, EForceMode) {#m-addforce}

```csharp
public void AddForce(Vector3 force, EForceMode forceMode = EForceMode.Force)
```

### AddForceAtLocation(Vector3, Vector3, EForceMode) {#m-addforceatlocation}

```csharp
public void AddForceAtLocation(Vector3 force, Vector3 location, EForceMode forceMode = EForceMode.Force)
```

### AddTorque(Vector3, EForceMode) {#m-addtorque}

```csharp
public void AddTorque(Vector3 torque, EForceMode forceMode = EForceMode.Force)
```

### GetLockedAxes() {#m-getlockedaxes}

```csharp
public uint GetLockedAxes()
```

### IsAxisLocked(EActorAxis) {#m-isaxislocked}

```csharp
public bool IsAxisLocked(EActorAxis axis)
```

### MoveKinematic(Vector3, Vector3, float) {#m-movekinematic}

```csharp
public void MoveKinematic(Vector3 targetPosition, Vector3 targetRotation, float deltaSeconds)
```

### Rotate(Vector3) {#m-rotate}

```csharp
public void Rotate(Vector3 rotation)
```

### SetAxisLock(EActorAxis, bool, bool) {#m-setaxislock}

```csharp
public void SetAxisLock(EActorAxis axis, bool value, bool forceWake = false)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
