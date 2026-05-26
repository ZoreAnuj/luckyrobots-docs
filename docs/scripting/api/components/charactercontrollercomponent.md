# CharacterControllerComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class CharacterControllerComponent : Component
```

## Properties

### AngularVelocity {#m-angularvelocity}

```csharp
public Vector3 AngularVelocity { get; set; }
```

### CollisionFlags {#m-collisionflags}

```csharp
public ECollisionFlags CollisionFlags { get; }
```

### IsGravityEnabled {#m-isgravityenabled}

```csharp
public bool IsGravityEnabled { get; set; }
```

### IsGrounded {#m-isgrounded}

```csharp
public bool IsGrounded { get; }
```

### LinearVelocity {#m-linearvelocity}

```csharp
public Vector3 LinearVelocity { get; set; }
```

### SlopeLimit {#m-slopelimit}

```csharp
public float SlopeLimit { get; set; }
```

### StepOffset {#m-stepoffset}

```csharp
public float StepOffset { get; set; }
```

## Methods

### Jump(float) {#m-jump}

```csharp
public void Jump(float jumpPower)
```

### Move(Vector3) {#m-move}

```csharp
public void Move(Vector3 displacement)
```

### Rotate(Quaternion) {#m-rotate}

```csharp
public void Rotate(Quaternion rotation)
```

### SetRotation(Quaternion) {#m-setrotation}

```csharp
public void SetRotation(Quaternion rotation)
```

### SetTranslation(Vector3) {#m-settranslation}

```csharp
public void SetTranslation(Vector3 translation)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
