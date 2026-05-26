# Transform

`struct` · namespace `Hazel`

```csharp
public struct Transform
```

## Constructors

### Transform(Vector3, Vector3, Vector3) {#m-transform}

```csharp
public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public Vector3 Position</code> |  |
| <code>public Vector3 Rotation</code> |  |
| <code>public Vector3 Scale</code> |  |

## Properties

### Forward {#m-forward}

```csharp
public Vector3 Forward { get; }
```

### Inverse {#m-inverse}

```csharp
public Transform Inverse { get; }
```

### Right {#m-right}

```csharp
public Vector3 Right { get; }
```

### Up {#m-up}

```csharp
public Vector3 Up { get; }
```

## Methods

### Equals(Transform) {#m-equals-2}

```csharp
public bool Equals(Transform right)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

## Operators

### operator !=(Transform, Transform) {#m-operator-3}

`static`

```csharp
public static bool operator !=(Transform left, Transform right)
```

### operator *(Transform, Transform) {#m-operator}

`static`

```csharp
public static Transform operator *(Transform a, Transform b)
```

### operator ==(Transform, Transform) {#m-operator-2}

`static`

```csharp
public static bool operator ==(Transform left, Transform right)
```


---
<small>Source: `Hazel/Math/Transform.cs`</small>
