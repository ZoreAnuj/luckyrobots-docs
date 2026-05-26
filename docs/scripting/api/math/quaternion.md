# Quaternion

`struct` · namespace `Hazel`

Implements `IEquatable<Quaternion>`

```csharp
public struct Quaternion : IEquatable<Quaternion>
```

## Constructors

### Quaternion(Vector3) {#m-quaternion-3}

```csharp
public Quaternion(Vector3 euler)
```

### Quaternion(Vector3, float) {#m-quaternion-2}

```csharp
public Quaternion(Vector3 xyz, float w)
```

### Quaternion(float, float, float, float) {#m-quaternion}

```csharp
public Quaternion(float x, float y, float z, float w)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static Quaternion Identity</code> |  |
| <code>public float W</code> |  |
| <code>public float X</code> |  |
| <code>public float Y</code> |  |
| <code>public float Z</code> |  |

## Properties

### Conjugate {#m-conjugate}

```csharp
public Quaternion Conjugate { get; }
```

### EulerAngles {#m-eulerangles}

```csharp
public Vector3 EulerAngles { get; }
```

### Pitch {#m-pitch}

```csharp
public float Pitch { get; }
```

### Roll {#m-roll}

```csharp
public float Roll { get; }
```

### XYZ {#m-xyz}

```csharp
public Vector3 XYZ { get; set; }
```

### Yaw {#m-yaw}

```csharp
public float Yaw { get; }
```

## Methods

### AngleAxis(float, Vector3) {#m-angleaxis}

`static`

```csharp
public static Quaternion AngleAxis(float aAngle, Vector3 aAxis)
```

### Equals(Quaternion) {#m-equals-2}

```csharp
public bool Equals(Quaternion right)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### FromToRotation(Vector3, Vector3) {#m-fromtorotation}

`static`

```csharp
public static Quaternion FromToRotation(Vector3 aFrom, Vector3 aTo)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### Length() {#m-length}

```csharp
public float Length()
```

### LengthSquared() {#m-lengthsquared}

```csharp
public float LengthSquared()
```

### LookRotation(Vector3, Vector3) {#m-lookrotation}

`static`

```csharp
public static Quaternion LookRotation(Vector3 forward, Vector3 up)
```

### Normalize() {#m-normalize}

```csharp
public void Normalize()
```

### Normalized() {#m-normalized}

```csharp
public Quaternion Normalized()
```

### QuaternionLookRotation(Vector3, Vector3) {#m-quaternionlookrotation}

`static`

```csharp
public static Quaternion QuaternionLookRotation(Vector3 forward, Vector3 up)
```

### Slerp(Quaternion, Quaternion, float) {#m-slerp}

`static`

```csharp
public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
```

### SlerpUnclamped(Quaternion, Quaternion, float) {#m-slerpunclamped}

`static`

```csharp
public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
```

## Operators

### operator !=(Quaternion, Quaternion) {#m-operator-4}

`static`

```csharp
public static bool operator !=(Quaternion left, Quaternion right)
```

### operator *(Quaternion, Quaternion) {#m-operator-2}

`static`

```csharp
public static Quaternion operator *(Quaternion a, Quaternion b)
```

### operator *(Quaternion, Vector3) {#m-operator}

`static`

```csharp
public static Vector3 operator *(Quaternion q, Vector3 v)
```

### operator ==(Quaternion, Quaternion) {#m-operator-3}

`static`

```csharp
public static bool operator ==(Quaternion left, Quaternion right)
```


---
<small>Source: `Hazel/Math/Quaternion.cs`</small>
