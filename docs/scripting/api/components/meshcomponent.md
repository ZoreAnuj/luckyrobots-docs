# MeshComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md) · Implements `IEquatable<MeshComponent>`

```csharp
public class MeshComponent : Component, IEquatable<MeshComponent>
```

## Properties

### IsRigged {#m-isrigged}

```csharp
public bool IsRigged { get; }
```

### Mesh {#m-mesh}

```csharp
public Mesh? Mesh { get; set; }
```

### Visible {#m-visible}

```csharp
public bool Visible { get; set; }
```

## Methods

### Equals(MeshComponent?) {#m-equals-2}

```csharp
public bool Equals(MeshComponent? right)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### GetMaterial(int) {#m-getmaterial}

```csharp
public Material? GetMaterial(int index)
```

### HasMaterial(int) {#m-hasmaterial}

```csharp
public bool HasMaterial(int index)
```

## Operators

### operator !=(MeshComponent, MeshComponent) {#m-operator-2}

`static`

```csharp
public static bool operator !=(MeshComponent left, MeshComponent right)
```

### operator ==(MeshComponent, MeshComponent) {#m-operator}

`static`

```csharp
public static bool operator ==(MeshComponent left, MeshComponent right)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
