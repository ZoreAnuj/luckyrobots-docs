# MeshColliderComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class MeshColliderComponent : Component
```

## Properties

### ColliderMeshHandle {#m-collidermeshhandle}

```csharp
public AssetHandle ColliderMeshHandle { get; }
```

### IsStaticMesh {#m-isstaticmesh}

```csharp
public bool IsStaticMesh { get; }
```

### Material {#m-material}

```csharp
public PhysicsMaterial Material { get; set; }
```

## Methods

### GetColliderMesh() {#m-getcollidermesh}

```csharp
public Mesh? GetColliderMesh()
```

### GetStaticColliderMesh() {#m-getstaticcollidermesh}

```csharp
public StaticMesh? GetStaticColliderMesh()
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
