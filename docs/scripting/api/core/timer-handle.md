# Timer.Handle

`struct` · namespace `Hazel`

Implements `IEquatable<Handle>`

```csharp
public readonly struct Handle : IEquatable<Handle>
```

## Properties

### IsValid {#m-isvalid}

```csharp
public bool IsValid { get; }
```

## Methods

### Equals(Handle) {#m-equals}

```csharp
public bool Equals(Handle other)
```

### Equals(object?) {#m-equals-2}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

## Operators

### operator !=(Handle, Handle) {#m-operator-2}

`static`

```csharp
public static bool operator !=(Handle a, Handle b)
```

### operator ==(Handle, Handle) {#m-operator}

`static`

```csharp
public static bool operator ==(Handle a, Handle b)
```


---
<small>Source: `Hazel/Core/Timer.cs`</small>
