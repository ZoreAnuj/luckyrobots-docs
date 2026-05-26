# AssetHandle

`struct` · namespace `Hazel`

Implements `IEquatable<AssetHandle>`

```csharp
public struct AssetHandle : IEquatable<AssetHandle>
```

## Constructors

### AssetHandle(ulong) {#m-assethandle}

```csharp
public AssetHandle(ulong handle)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static readonly AssetHandle Invalid</code> |  |

## Methods

### Equals(AssetHandle) {#m-equals}

```csharp
public bool Equals(AssetHandle other)
```

### Equals(object?) {#m-equals-2}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### IsValid() {#m-isvalid}

```csharp
public bool IsValid()
```

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

## Operators

### implicit operator bool {#m-implicit-operator-bool}

`static`

```csharp
public static implicit operator bool(AssetHandle assetHandle)
```

### implicit operator ulong {#m-implicit-operator-ulong}

`static`

```csharp
public static implicit operator ulong(AssetHandle handle)
```

### operator !=(AssetHandle?, AssetHandle?) {#m-operator-2}

`static`

```csharp
public static bool operator !=(AssetHandle? handle0, AssetHandle? handle1)
```

### operator ==(AssetHandle?, AssetHandle?) {#m-operator}

`static`

```csharp
public static bool operator ==(AssetHandle? handle0, AssetHandle? handle1)
```


---
<small>Source: `Hazel/Core/AssetHandle.cs`</small>
