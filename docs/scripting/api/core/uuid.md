# UUID

`struct` · namespace `Hazel`

Implements `IEquatable<UUID>`

```csharp
public struct UUID : IEquatable<UUID>
```

## Constructors

### UUID() {#m-uuid}

```csharp
public UUID()
```

### UUID(ulong) {#m-uuid-2}

```csharp
public UUID(ulong id)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static readonly UUID Invalid</code> |  |

## Properties

### ID {#m-id}

```csharp
public ulong ID { get; }
```

## Methods

### Equals(UUID) {#m-equals}

```csharp
public bool Equals(UUID other)
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
public static implicit operator bool(UUID uuid)
```

### operator !=(UUID, ulong) {#m-operator-4}

`static`

```csharp
public static bool operator !=(UUID a, ulong b)
```

### operator !=(UUID?, UUID?) {#m-operator-2}

`static`

```csharp
public static bool operator !=(UUID? a, UUID? b)
```

### operator !=(ulong, UUID) {#m-operator-6}

`static`

```csharp
public static bool operator !=(ulong a, UUID b)
```

### operator ==(UUID, ulong) {#m-operator-3}

`static`

```csharp
public static bool operator ==(UUID a, ulong b)
```

### operator ==(UUID?, UUID?) {#m-operator}

`static`

```csharp
public static bool operator ==(UUID? a, UUID? b)
```

### operator ==(ulong, UUID) {#m-operator-5}

`static`

```csharp
public static bool operator ==(ulong a, UUID b)
```


---
<small>Source: `Hazel/Core/UUID.cs`</small>
