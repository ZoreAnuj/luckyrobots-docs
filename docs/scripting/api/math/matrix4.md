# Matrix4

`struct` · namespace `Hazel`

```csharp
public struct Matrix4
```

## Constructors

### Matrix4(float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) {#m-matrix4-2}

```csharp
public Matrix4(float d00, float d10, float d20, float d30, float d01, float d11, float d21, float d31, float d02, float d12, float d22, float d32, float d03, float d13, float d23, float d33)
```

### Matrix4(float) {#m-matrix4}

```csharp
public Matrix4(float value)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public float D00</code> |  |
| <code>public float D01</code> |  |
| <code>public float D02</code> |  |
| <code>public float D03</code> |  |
| <code>public float D10</code> |  |
| <code>public float D11</code> |  |
| <code>public float D12</code> |  |
| <code>public float D13</code> |  |
| <code>public float D20</code> |  |
| <code>public float D21</code> |  |
| <code>public float D22</code> |  |
| <code>public float D23</code> |  |
| <code>public float D30</code> |  |
| <code>public float D31</code> |  |
| <code>public float D32</code> |  |
| <code>public float D33</code> |  |

## Properties

### Inverse {#m-inverse}

```csharp
public Matrix4 Inverse { get; }
```

### Translation {#m-translation}

```csharp
public Vector3 Translation { get; set; }
```

## Methods

### DebugPrint() {#m-debugprint}

```csharp
public void DebugPrint()
```

### LookAt(Vector3, Vector3, Vector3) {#m-lookat}

`static`

```csharp
public static Matrix4 LookAt(Vector3 eye, Vector3 center, Vector3 up)
```

### MultiplyPoint(Vector3) {#m-multiplypoint}

```csharp
public Vector3 MultiplyPoint(Vector3 point)
```

### MultiplyVector(Vector3) {#m-multiplyvector}

```csharp
public Vector3 MultiplyVector(Vector3 point)
```

### Scale(Vector3) {#m-scale}

`static`

```csharp
public static Matrix4 Scale(Vector3 scale)
```

### Scale(float) {#m-scale-2}

`static`

```csharp
public static Matrix4 Scale(float scale)
```

### Translate(Vector3) {#m-translate}

`static`

```csharp
public static Matrix4 Translate(Vector3 translation)
```


---
<small>Source: `Hazel/Math/Matrix4.cs`</small>
