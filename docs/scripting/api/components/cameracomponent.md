# CameraComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class CameraComponent : Component
```

## Properties

### OrthographicFarClip {#m-orthographicfarclip}

```csharp
public float OrthographicFarClip { get; set; }
```

### OrthographicNearClip {#m-orthographicnearclip}

```csharp
public float OrthographicNearClip { get; set; }
```

### OrthographicSize {#m-orthographicsize}

```csharp
public float OrthographicSize { get; set; }
```

### PerspectiveFarClip {#m-perspectivefarclip}

```csharp
public float PerspectiveFarClip { get; set; }
```

### PerspectiveNearClip {#m-perspectivenearclip}

```csharp
public float PerspectiveNearClip { get; set; }
```

### Primary {#m-primary}

```csharp
public bool Primary { get; set; }
```

### ProjectionType {#m-projectiontype}

```csharp
public CameraComponentType ProjectionType { get; set; }
```

### VerticalFOV {#m-verticalfov}

```csharp
public float VerticalFOV { get; set; }
```

## Methods

### GetRayDirection(Vector2) {#m-getraydirection}

```csharp
public Vector3 GetRayDirection(Vector2 screenPos)
```

### SetOrthographic(float, float, float) {#m-setorthographic}

```csharp
public void SetOrthographic(float size, float nearClip, float farClip)
```

### SetPerspective(float, float, float) {#m-setperspective}

```csharp
public void SetPerspective(float verticalFov, float nearClip, float farClip)
```

### ToScreenSpace(Vector3) {#m-toscreenspace}

```csharp
public Vector2 ToScreenSpace(Vector3 worldTranslation)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
