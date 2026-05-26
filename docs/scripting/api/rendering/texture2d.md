# Texture2D

`class` · namespace `Hazel`

Inherits [`Asset<Texture2D>`](../core/asset-t.md)

```csharp
public class Texture2D : Asset<Texture2D>
```

## Constructors

### Texture2D(ulong) {#m-texture2d}

```csharp
public Texture2D(ulong handle)
```

## Properties

### Height {#m-height}

```csharp
public uint Height { get; }
```

### Width {#m-width}

```csharp
public uint Width { get; }
```

## Methods

### Create(uint, uint, TextureWrapMode, TextureFilterMode, Vector4[]?) {#m-create}

`static`

```csharp
public static Texture2D? Create(uint width, uint height, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureFilterMode filterMode = TextureFilterMode.Linear, Vector4[]? data = null)
```

### SetData(Vector4[]) {#m-setdata}

```csharp
public void SetData(Vector4[] data)
```


---
<small>Source: `Hazel/Renderer/Texture2D.cs`</small>
