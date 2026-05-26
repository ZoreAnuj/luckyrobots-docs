# AudioCommandID

`struct` · namespace `Hazel`

```csharp
public struct AudioCommandID
```

## Constructors

### AudioCommandID(string) {#m-audiocommandid}

```csharp
public AudioCommandID(string commandName)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public readonly uint ID</code> |  |

## Methods

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? c)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

## Operators

### implicit operator uint {#m-implicit-operator-uint}

`static`

```csharp
public static implicit operator uint(AudioCommandID commandID)
```

### operator !=(AudioCommandID, AudioCommandID) {#m-operator-2}

`static`

```csharp
public static bool operator !=(AudioCommandID c1, AudioCommandID c2)
```

### operator ==(AudioCommandID, AudioCommandID) {#m-operator}

`static`

```csharp
public static bool operator ==(AudioCommandID c1, AudioCommandID c2)
```


---
<small>Source: `Hazel/Audio/Audio.cs`</small>
