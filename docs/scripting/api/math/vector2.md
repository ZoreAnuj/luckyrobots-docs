# Vector2

`struct` · namespace `Hazel`

```csharp
public struct Vector2
```

## Constructors

### Vector2(Vector3) {#m-vector2-3}

```csharp
public Vector2(Vector3 vector)
```

### Vector2(float) {#m-vector2}

```csharp
public Vector2(float scalar)
```

### Vector2(float, float) {#m-vector2-2}

```csharp
public Vector2(float x, float y)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static Vector2 Down</code> |  |
| <code>public static Vector2 Left</code> |  |
| <code>public static Vector2 One</code> |  |
| <code>public static Vector2 Right</code> |  |
| <code>public static Vector2 Up</code> |  |
| <code>public float X</code> |  |
| <code>public float Y</code> |  |
| <code>public static Vector2 Zero</code> |  |

## Methods

### Apply(Func<float, float>) {#m-apply}

```csharp
public void Apply(Func<float, float> func)
```

Allows you to pass in a delegate that takes in a double to process the vector per axis. i.e. (float.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'float' method to act as a scalar to process X and Y |

### Clamp(Vector2, Vector2) {#m-clamp}

```csharp
public void Clamp(Vector2 min, Vector2 max)
```

### Distance(Vector2) {#m-distance}

```csharp
public float Distance(Vector2 other)
```

### Distance(Vector2, Vector2) {#m-distance-2}

`static`

```csharp
public static float Distance(Vector2 p1, Vector2 p2)
```

### Dot(Vector2, Vector2) {#m-dot}

`static`

```csharp
public static float Dot(Vector2 lhs, Vector2 rhs)
```

### EpsilonEquals(Vector2, Vector2, float) {#m-epsilonequals}

`static`

```csharp
public static bool EpsilonEquals(Vector2 p1, Vector2 p2, float epsilon = Mathf.Epsilon)
```

### EqualEpsilon(Vector2, float) {#m-equalepsilon}

```csharp
public bool EqualEpsilon(Vector2 other, float epsilon = Mathf.Epsilon)
```

### Equals(Vector2) {#m-equals-2}

```csharp
public bool Equals(Vector2 right)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### Length() {#m-length}

```csharp
public float Length()
```

### Lerp(Vector2, Vector2, float) {#m-lerp}

`static`

```csharp
public static Vector2 Lerp(Vector2 p1, Vector2 p2, float t)
```

### New(Func<float, float>) {#m-new}

```csharp
public Vector2 New(Func<float, float> func)
```

Allows you to pass in a delegate that takes in and returns a new Vector processed per axis. i.e. (float.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'float' method to act as a scalar to process X and Y |

### Normalize() {#m-normalize}

```csharp
public void Normalize()
```

### Normalized() {#m-normalized}

```csharp
public Vector2 Normalized()
```

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

## Operators

### operator !=(Vector2, Vector2) {#m-operator-13}

`static`

```csharp
public static bool operator !=(Vector2 left, Vector2 right)
```

### operator *(Vector2, Vector2) {#m-operator-3}

`static`

```csharp
public static Vector2 operator *(Vector2 left, Vector2 right)
```

### operator *(Vector2, float) {#m-operator}

`static`

```csharp
public static Vector2 operator *(Vector2 left, float scalar)
```

### operator *(float, Vector2) {#m-operator-2}

`static`

```csharp
public static Vector2 operator *(float scalar, Vector2 right)
```

### operator +(Vector2, Vector2) {#m-operator-7}

`static`

```csharp
public static Vector2 operator +(Vector2 left, Vector2 right)
```

### operator +(Vector2, float) {#m-operator-8}

`static`

```csharp
public static Vector2 operator +(Vector2 left, float right)
```

### operator -(Vector2, Vector2) {#m-operator-9}

`static`

```csharp
public static Vector2 operator -(Vector2 left, Vector2 right)
```

### operator -(Vector2, float) {#m-operator-10}

`static`

```csharp
public static Vector2 operator -(Vector2 left, float right)
```

### operator -(Vector2) {#m-operator-11}

`static`

```csharp
public static Vector2 operator -(Vector2 vector)
```

### operator /(Vector2, Vector2) {#m-operator-4}

`static`

```csharp
public static Vector2 operator /(Vector2 left, Vector2 right)
```

### operator /(Vector2, float) {#m-operator-5}

`static`

```csharp
public static Vector2 operator /(Vector2 left, float scalar)
```

### operator /(float, Vector2) {#m-operator-6}

`static`

```csharp
public static Vector2 operator /(float scalar, Vector2 right)
```

### operator ==(Vector2, Vector2) {#m-operator-12}

`static`

```csharp
public static bool operator ==(Vector2 left, Vector2 right)
```


---
<small>Source: `Hazel/Math/Vector2.cs`</small>
