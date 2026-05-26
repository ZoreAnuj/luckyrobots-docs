# MdpTerminationAttribute

`class` · namespace `Hazel`

Marks a static method as an MDP termination component. Method signature: bool Evaluate(MdpContext ctx, Dictionary&lt;string, string&gt; parameters)

Inherits `Attribute`

```csharp
public sealed class MdpTerminationAttribute : Attribute
```

## Constructors

### MdpTerminationAttribute(string) {#m-mdpterminationattribute}

```csharp
public MdpTerminationAttribute(string name)
```

## Properties

### Category {#m-category}

```csharp
public string Category { get; set; }
```

### Description {#m-description}

```csharp
public string Description { get; set; }
```

### IsTimeout {#m-istimeout}

```csharp
public bool IsTimeout { get; set; }
```

### Name {#m-name}

```csharp
public string Name { get; }
```

### RobotTypes {#m-robottypes}

```csharp
public string[] RobotTypes { get; set; }
```


---
<small>Source: `Hazel/Learn/MdpAttributes.cs`</small>
