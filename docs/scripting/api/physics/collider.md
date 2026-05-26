# Collider

`class` · namespace `Hazel`

```csharp
public class Collider
```

## Constructors

### Collider() {#m-collider}

```csharp
public Collider()
```

### Collider(ulong, Shape, Vector3) {#m-collider-2}

```csharp
public Collider(ulong entityID, Shape collisionShape, Vector3 offset)
```

## Properties

### CollisionShape {#m-collisionshape}

```csharp
public Shape? CollisionShape { get; protected set; }
```

### Entity {#m-entity}

```csharp
public Entity? Entity { get; }
```

### Offset {#m-offset}

```csharp
public Vector3 Offset { get; protected set; }
```

### RigidBody {#m-rigidbody}

```csharp
public RigidBodyComponent? RigidBody { get; }
```

## Methods

### ToString() {#m-tostring}

```csharp
public override string ToString()
```


---
<small>Source: `Hazel/Physics/Collider.cs`</small>
