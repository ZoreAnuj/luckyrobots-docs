# Material

`class` · namespace `Hazel`

Inherits [`Asset<Material>`](../core/asset-t.md)

```csharp
public sealed class Material : Asset<Material>
```

## Properties

### AlbedoColor {#m-albedocolor}

```csharp
public Vector3 AlbedoColor { get; set; }
```

### Emission {#m-emission}

```csharp
public float Emission { get; set; }
```

### Metalness {#m-metalness}

```csharp
public float Metalness { get; set; }
```

### Roughness {#m-roughness}

```csharp
public float Roughness { get; set; }
```

## Methods

### Set(string, Texture2D) {#m-set-2}

```csharp
public void Set(string uniform, Texture2D texture)
```

### Set(string, Vector3) {#m-set-3}

```csharp
public void Set(string uniform, Vector3 value)
```

### Set(string, Vector4) {#m-set-4}

```csharp
public void Set(string uniform, Vector4 value)
```

### Set(string, float) {#m-set}

```csharp
public void Set(string uniform, float value)
```

### SetTexture(string, Texture2D) {#m-settexture}

```csharp
public void SetTexture(string uniform, Texture2D texture)
```


---
<small>Source: `Hazel/Renderer/Material.cs`</small>
