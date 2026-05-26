# Vector4

`struct` · namespace `Hazel`

```csharp
public struct Vector4
```

## Constructors

### Vector4(Vector3, float) {#m-vector4-3}

```csharp
public Vector4(Vector3 xyz, float w)
```

### Vector4(float) {#m-vector4}

```csharp
public Vector4(float scalar)
```

### Vector4(float, float, float, float) {#m-vector4-2}

```csharp
public Vector4(float x, float y, float z, float w)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static Vector4 Infiniity</code> |  |
| <code>public static Vector4 Infinity</code> |  |
| <code>public static Vector4 One</code> |  |
| <code>public float W</code> |  |
| <code>public float X</code> |  |
| <code>public float Y</code> |  |
| <code>public float Z</code> |  |
| <code>public static Vector4 Zero</code> |  |

## Properties

### XYZ {#m-xyz}

```csharp
public Vector3 XYZ { get; set; }
```

## Methods

### Apply(Func<float, float>) {#m-apply}

```csharp
public void Apply(Func<float, float> func)
```

Allows you to pass in a delegate that takes in a double to process the vector per axis, retaining W. i.e. (Mathf.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'float' method to act as a scalar to process X, Y and Z, retaining W |

### Clamp(Vector4, Vector4) {#m-clamp}

```csharp
public void Clamp(Vector4 min, Vector4 max)
```

### Cos(Vector4) {#m-cos}

`static`

```csharp
public static Vector4 Cos(Vector4 vector)
```

### Equals(Vector4) {#m-equals-2}

```csharp
public bool Equals(Vector4 right)
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

### Lerp(Vector4, Vector4, float) {#m-lerp}

`static`

```csharp
public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
```

### New(Func<float, float>) {#m-new}

```csharp
public Vector4 New(Func<float, float> func)
```

Allows you to pass in a delegate that takes in and returns a new Vector processed per axis, retaining W. i.e. (float.Cos) or a lambda (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'double' method to act as a scalar to process X, Y and Z, retaining W |

### Normalize() {#m-normalize}

```csharp
public void Normalize()
```

### Normalized() {#m-normalized}

```csharp
public Vector4 Normalized()
```

### Sin(Vector4) {#m-sin}

`static`

```csharp
public static Vector4 Sin(Vector4 vector)
```

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

## Operators

### operator !=(Vector4, Vector4) {#m-operator-9}

`static`

```csharp
public static bool operator !=(Vector4 left, Vector4 right)
```

### operator *(Vector4, Vector4) {#m-operator-3}

`static`

```csharp
public static Vector4 operator *(Vector4 left, Vector4 right)
```

### operator *(Vector4, float) {#m-operator-4}

`static`

```csharp
public static Vector4 operator *(Vector4 left, float scalar)
```

### operator *(float, Vector4) {#m-operator-5}

`static`

```csharp
public static Vector4 operator *(float scalar, Vector4 right)
```

### operator +(Vector4, Vector4) {#m-operator}

`static`

```csharp
public static Vector4 operator +(Vector4 left, Vector4 right)
```

### operator -(Vector4, Vector4) {#m-operator-2}

`static`

```csharp
public static Vector4 operator -(Vector4 left, Vector4 right)
```

### operator /(Vector4, Vector4) {#m-operator-6}

`static`

```csharp
public static Vector4 operator /(Vector4 left, Vector4 right)
```

### operator /(Vector4, float) {#m-operator-7}

`static`

```csharp
public static Vector4 operator /(Vector4 left, float scalar)
```

### operator ==(Vector4, Vector4) {#m-operator-8}

`static`

```csharp
public static bool operator ==(Vector4 left, Vector4 right)
```


---
<small>Source: `Hazel/Math/Vector4.cs`</small>
