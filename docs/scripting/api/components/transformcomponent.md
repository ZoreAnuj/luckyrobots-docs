# TransformComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class TransformComponent : Component
```

## Properties

### Inverse {#m-inverse}

```csharp
public Transform Inverse { get; }
```

### Local {#m-local}

```csharp
public Transform Local { get; set; }
```

Transform relative to parent entity

### LocalTransform {#m-localtransform}

```csharp
public Transform LocalTransform { get; }
```

### Matrix {#m-matrix}

```csharp
public Matrix4 Matrix { get; set; }
```

### Rotation {#m-rotation}

```csharp
public Vector3 Rotation { get; set; }
```

### RotationQuat {#m-rotationquat}

```csharp
public Quaternion RotationQuat { get; set; }
```

### Scale {#m-scale}

```csharp
public Vector3 Scale { get; set; }
```

### TransformMatrix {#m-transformmatrix}

```csharp
public Matrix4 TransformMatrix { get; }
```

### Translation {#m-translation}

```csharp
public Vector3 Translation { get; set; }
```

### World {#m-world}

```csharp
public Transform World { get; set; }
```

Transform in world coordinate space

### WorldRotation {#m-worldrotation}

```csharp
public Vector3 WorldRotation { get; set; }
```

### WorldRotationQuat {#m-worldrotationquat}

```csharp
public Quaternion WorldRotationQuat { get; set; }
```

### WorldScale {#m-worldscale}

```csharp
public Vector3 WorldScale { get; set; }
```

### WorldTransform {#m-worldtransform}

```csharp
public Transform WorldTransform { get; }
```

### WorldTransformMatrix {#m-worldtransformmatrix}

```csharp
public Matrix4 WorldTransformMatrix { get; set; }
```

### WorldTranslation {#m-worldtranslation}

```csharp
public Vector3 WorldTranslation { get; set; }
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
