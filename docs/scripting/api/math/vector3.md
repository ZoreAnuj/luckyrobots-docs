# Vector3

`struct` · namespace `Hazel`

Implements `IEquatable<Vector3>`

```csharp
public struct Vector3 : IEquatable<Vector3>
```

## Constructors

### Vector3(Vector2) {#m-vector3-5}

```csharp
public Vector3(Vector2 vector)
```

### Vector3(Vector2, float) {#m-vector3-4}

```csharp
public Vector3(Vector2 xy, float z)
```

### Vector3(Vector4) {#m-vector3-6}

```csharp
public Vector3(Vector4 vector)
```

### Vector3(float) {#m-vector3}

```csharp
public Vector3(float scalar)
```

### Vector3(float, Vector2) {#m-vector3-3}

```csharp
public Vector3(float x, Vector2 yz)
```

### Vector3(float, float, float) {#m-vector3-2}

```csharp
public Vector3(float x, float y, float z)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static Vector3 Back</code> |  |
| <code>public static Vector3 Down</code> |  |
| <code>public static Vector3 Forward</code> |  |
| <code>public static Vector3 Inifinity</code> |  |
| <code>public const float kEpsilonNormalSqrt = 1e-15F</code> |  |
| <code>public static Vector3 Left</code> |  |
| <code>public static Vector3 One</code> |  |
| <code>public static Vector3 Right</code> |  |
| <code>public static Vector3 Up</code> |  |
| <code>public float X</code> |  |
| <code>public float Y</code> |  |
| <code>public float Z</code> |  |
| <code>public static Vector3 Zero</code> |  |

## Properties

### sqrMagnitude {#m-sqrmagnitude}

```csharp
public float sqrMagnitude { get; }
```

### XY {#m-xy}

```csharp
public Vector2 XY { get; set; }
```

### XZ {#m-xz}

```csharp
public Vector2 XZ { get; set; }
```

### YZ {#m-yz}

```csharp
public Vector2 YZ { get; set; }
```

## Methods

### Abs(Vector3) {#m-abs}

`static`

```csharp
public static Vector3 Abs(Vector3 vector)
```

### Angle(Vector3, Vector3) {#m-angle}

`static`

```csharp
public static float Angle(Vector3 from, Vector3 to)
```

### Apply(Func<float, float>) {#m-apply}

```csharp
public void Apply(Func<float, float> func)
```

Allows you to pass in a delegate that takes in a double to process the vector per axis. i.e. (Mathf.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'float' method to act as a scalar to process X, Y and Z |

### Clamp(Vector3, Vector3) {#m-clamp}

```csharp
public void Clamp(Vector3 min, Vector3 max)
```

### ClampLength(Vector3, float) {#m-clamplength}

`static`

```csharp
public static Vector3 ClampLength(Vector3 vector, float maxLength)
```

### Cos(Vector3) {#m-cos}

`static`

```csharp
public static Vector3 Cos(Vector3 vector)
```

### Cross(Vector3, Vector3) {#m-cross}

`static`

```csharp
public static Vector3 Cross(Vector3 x, Vector3 y)
```

### DirectionFromEuler(Vector3) {#m-directionfromeuler}

`static`

```csharp
public static Vector3 DirectionFromEuler(Vector3 rotation)
```

### Distance(Vector3) {#m-distance}

```csharp
public float Distance(Vector3 other)
```

### Distance(Vector3, Vector3) {#m-distance-2}

`static`

```csharp
public static float Distance(Vector3 p1, Vector3 p2)
```

### Dot(Vector3, Vector3) {#m-dot}

`static`

```csharp
public static float Dot(Vector3 lhs, Vector3 rhs)
```

### EpsilonEquals(Vector3, Vector3, float) {#m-epsilonequals}

`static`

```csharp
public static bool EpsilonEquals(Vector3 left, Vector3 right, float epsilon = Mathf.Epsilon)
```

### EqualEpsilon(Vector3, float) {#m-equalepsilon}

```csharp
public bool EqualEpsilon(Vector3 other, float epsilon = Mathf.Epsilon)
```

### Equals(Vector3) {#m-equals-2}

```csharp
public bool Equals(Vector3 right)
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

### Lerp(Vector3, Vector3, float) {#m-lerp}

`static`

```csharp
public static Vector3 Lerp(Vector3 p1, Vector3 p2, float t)
```

### New(Func<float, float>) {#m-new}

```csharp
public Vector3 New(Func<float, float> func)
```

Allows you to pass in a delegate that takes in and returns a new Vector processed per axis. i.e. (float.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'float' method to act as a scalar to process X, Y and Z |

### Normalize() {#m-normalize}

```csharp
public void Normalize()
```

### Normalized() {#m-normalized}

```csharp
public Vector3 Normalized()
```

### SignedAngle(Vector3, Vector3, Vector3) {#m-signedangle}

`static`

```csharp
public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
```

### Sin(Vector3) {#m-sin}

`static`

```csharp
public static Vector3 Sin(Vector3 vector)
```

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

### UnwrapEuler(Vector3, Vector3) {#m-unwrapeuler}

`static`

```csharp
public static Vector3 UnwrapEuler(Vector3 previous, Vector3 current)
```

## Operators

### operator !=(Vector3, Vector3) {#m-operator-13}

`static`

```csharp
public static bool operator !=(Vector3 left, Vector3 right)
```

### operator *(Vector3, Vector3) {#m-operator-3}

`static`

```csharp
public static Vector3 operator *(Vector3 left, Vector3 right)
```

### operator *(Vector3, float) {#m-operator}

`static`

```csharp
public static Vector3 operator *(Vector3 left, float scalar)
```

### operator *(float, Vector3) {#m-operator-2}

`static`

```csharp
public static Vector3 operator *(float scalar, Vector3 right)
```

### operator +(Vector3, Vector3) {#m-operator-7}

`static`

```csharp
public static Vector3 operator +(Vector3 left, Vector3 right)
```

### operator +(Vector3, float) {#m-operator-8}

`static`

```csharp
public static Vector3 operator +(Vector3 left, float right)
```

### operator -(Vector3, Vector3) {#m-operator-9}

`static`

```csharp
public static Vector3 operator -(Vector3 left, Vector3 right)
```

### operator -(Vector3, float) {#m-operator-10}

`static`

```csharp
public static Vector3 operator -(Vector3 left, float right)
```

### operator -(Vector3) {#m-operator-11}

`static`

```csharp
public static Vector3 operator -(Vector3 vector)
```

### operator /(Vector3, Vector3) {#m-operator-4}

`static`

```csharp
public static Vector3 operator /(Vector3 left, Vector3 right)
```

### operator /(Vector3, float) {#m-operator-5}

`static`

```csharp
public static Vector3 operator /(Vector3 left, float scalar)
```

### operator /(float, Vector3) {#m-operator-6}

`static`

```csharp
public static Vector3 operator /(float scalar, Vector3 right)
```

### operator ==(Vector3, Vector3) {#m-operator-12}

`static`

```csharp
public static bool operator ==(Vector3 left, Vector3 right)
```


---
<small>Source: `Hazel/Math/Vector3.cs`</small>
