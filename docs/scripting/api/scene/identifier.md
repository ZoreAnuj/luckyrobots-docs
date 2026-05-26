# Identifier

`struct` · namespace `Hazel`

```csharp
public readonly struct Identifier
```

## Constructors

### Identifier(string) {#m-identifier}

```csharp
public Identifier(string name)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public readonly uint ID</code> |  |
| <code>public static readonly Identifier Invalid</code> |  |

## Methods

### FromId(uint) {#m-fromid}

`static`

```csharp
public static Identifier FromId(uint id)
```

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

## Operators

### implicit operator uint {#m-implicit-operator-uint}

`static`

```csharp
public static implicit operator uint(Identifier id)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
