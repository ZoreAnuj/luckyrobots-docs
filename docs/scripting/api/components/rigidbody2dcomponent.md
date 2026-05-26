# RigidBody2DComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class RigidBody2DComponent : Component
```

## Properties

### BodyType {#m-bodytype}

```csharp
public RigidBody2DBodyType BodyType { get; set; }
```

### GravityScale {#m-gravityscale}

```csharp
public float GravityScale { get; set; }
```

### LinearVelocity {#m-linearvelocity}

```csharp
public Vector2 LinearVelocity { get; set; }
```

### Mass {#m-mass}

```csharp
public float Mass { get; set; }
```

### Rotation {#m-rotation}

```csharp
public float Rotation { get; set; }
```

### Translation {#m-translation}

```csharp
public Vector2 Translation { get; set; }
```

## Methods

### AddForce(Vector2, Vector2, bool) {#m-addforce}

```csharp
public void AddForce(Vector2 force, Vector2 offset, bool wake)
```

### AddTorque(float, bool) {#m-addtorque}

```csharp
public void AddTorque(float torque, bool wake)
```

### ApplyAngularImpulse(float, bool) {#m-applyangularimpulse}

```csharp
public void ApplyAngularImpulse(float impulse, bool wake)
```

### ApplyLinearImpulse(Vector2, Vector2, bool) {#m-applylinearimpulse}

```csharp
public void ApplyLinearImpulse(Vector2 impulse, Vector2 offset, bool wake)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
