# HzMath

`static class` · namespace `Hazel`

```csharp
public static class HzMath
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public const float EPSILON = 1e-6f</code> |  |
| <code>public const float MIN_NORM = 1e-9f</code> |  |
| <code>public const float PI = 3.14159265358979323846f</code> |  |
| <code>public const float TWO_PI = 2.0f * PI</code> |  |

## Methods

### AxisAngleFromQuat(Quaternion, float) {#m-axisanglefromquat}

`static`

```csharp
public static Vector3 AxisAngleFromQuat(Quaternion q, float eps = EPSILON)
```

### Clamp<T>(T, T, T) {#m-clamp}

`static`

```csharp
public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
```

### CombineFrameTransforms(Vector3, Quaternion, Vector3, Quaternion, Vector3, Quaternion) {#m-combineframetransforms}

`static`

```csharp
public static void CombineFrameTransforms(in Vector3 t01, in Quaternion q01, in Vector3 t12, in Quaternion q12, out Vector3 outPos, out Quaternion outQuat)
```

### ComputeAngularVelocity(Quaternion, Quaternion, float) {#m-computeangularvelocity}

`static`

```csharp
public static Vector3 ComputeAngularVelocity(in Quaternion currentQuat, in Quaternion previousQuat, float deltaTime)
```

### ComputeLinearVelocity(Vector3, Vector3, float) {#m-computelinearvelocity}

`static`

```csharp
public static Vector3 ComputeLinearVelocity(in Vector3 currentPos, in Vector3 previousPos, float deltaTime)
```

### MatrixFromQuat6D(Quaternion, float[], int) {#m-matrixfromquat6d}

`static`

```csharp
public static void MatrixFromQuat6D(in Quaternion q, float[] outArr, int startIndex = 0)
```

### NormalizeAngle(float) {#m-normalizeangle}

`static`

```csharp
public static float NormalizeAngle(float angle)
```

### QuatApply(Quaternion, Vector3) {#m-quatapply}

`static`

```csharp
public static Vector3 QuatApply(in Quaternion q, in Vector3 v)
```

### QuatApplyInverse(Quaternion, Vector3) {#m-quatapplyinverse}

`static`

```csharp
public static Vector3 QuatApplyInverse(in Quaternion q, in Vector3 v)
```

### QuatConjugate(Quaternion) {#m-quatconjugate}

`static`

```csharp
public static Quaternion QuatConjugate(in Quaternion q)
```

### QuatInverse(Quaternion) {#m-quatinverse}

`static`

```csharp
public static Quaternion QuatInverse(in Quaternion q)
```

### QuatMultiply(Quaternion, Quaternion) {#m-quatmultiply}

`static`

```csharp
public static Quaternion QuatMultiply(in Quaternion q1, in Quaternion q2)
```

### SampleGaussian(System.Random, float) {#m-samplegaussian}

`static`

```csharp
public static float SampleGaussian(System.Random random, float stdDev)
```

### SubtractFrameTransforms(Vector3, Quaternion, Vector3, Quaternion, Vector3, Quaternion) {#m-subtractframetransforms}

`static`

```csharp
public static void SubtractFrameTransforms(in Vector3 t01, in Quaternion q01, in Vector3 t02, in Quaternion q02, out Vector3 outPos, out Quaternion outQuat)
```


---
<small>Source: `Hazel/Learn/Utilities/HzMath.cs`</small>
