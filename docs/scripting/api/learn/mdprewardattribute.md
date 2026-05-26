# MdpRewardAttribute

`class` · namespace `Hazel`

Marks a static method as an MDP reward component. Method signature: float Compute(MdpContext ctx, Dictionary&lt;string, string&gt; parameters)

Inherits `Attribute`

```csharp
public sealed class MdpRewardAttribute : Attribute
```

## Constructors

### MdpRewardAttribute(string) {#m-mdprewardattribute}

```csharp
public MdpRewardAttribute(string name)
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
