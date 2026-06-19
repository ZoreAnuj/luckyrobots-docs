# MdpObservationAttribute

`class` · namespace `Hazel`

Marks a static method as an MDP observation component. Method signature: int Compute(MdpContext ctx, Dictionary&lt;string, string&gt; parameters, float[] buffer, int offset)

Inherits `Attribute`

```csharp
public sealed class MdpObservationAttribute : Attribute
```

## Constructors

### MdpObservationAttribute(string) {#m-mdpobservationattribute}

```csharp
public MdpObservationAttribute(string name)
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

### OutputSize {#m-outputsize}

```csharp
public int OutputSize { get; set; }
```

### RobotTypes {#m-robottypes}

```csharp
public string[] RobotTypes { get; set; }
```


---
<small>Source: `Hazel/Learn/MdpAttributes.cs`</small>
