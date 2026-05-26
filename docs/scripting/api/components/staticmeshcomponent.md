# StaticMeshComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class StaticMeshComponent : Component
```

## Properties

### Mesh {#m-mesh}

```csharp
public StaticMesh Mesh { get; set; }
```

### Visible {#m-visible}

```csharp
public bool Visible { get; set; }
```

## Methods

### GetMaterial(int) {#m-getmaterial}

```csharp
public Material? GetMaterial(int index)
```

### HasMaterial(int) {#m-hasmaterial}

```csharp
public bool HasMaterial(int index)
```

### SetMaterial(int, Material) {#m-setmaterial}

```csharp
public void SetMaterial(int index, Material material)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
