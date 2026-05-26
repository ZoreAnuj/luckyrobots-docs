# Asset<T>

`class` · namespace `Hazel`

Implements `IEquatable<T>`

```csharp
public partial class Asset<T> : IEquatable<T>
```

## Properties

### Handle {#m-handle}

```csharp
public AssetHandle Handle { get; init; }
```

## Methods

### Equals(T?) {#m-equals-2}

```csharp
public bool Equals(T? other)
```

### Equals(object?) {#m-equals}

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

### IsValid(Asset<T>?) {#m-isvalid-2}

`static`

```csharp
public static bool IsValid(Asset<T>? asset)
```

## Operators

### implicit operator bool {#m-implicit-operator-bool}

`static`

```csharp
public static implicit operator bool(Asset<T>? asset)
```


---
<small>Source: `Hazel/Core/Asset.cs`</small>
