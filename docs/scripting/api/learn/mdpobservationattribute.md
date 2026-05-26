# MdpObservationAttribute

`class` · namespace `Hazel`

Attributes for marking user C# script methods as MDP components. Users place C# scripts in RobotSandbox/Assets/Scripts/Source/ and decorate static methods with these attributes. The ScriptEngine discovers them at runtime via [`MdpScriptDiscovery`](mdpscriptdiscovery.md) and registers them in the [`MdpComponentRegistry`](mdpcomponentregistry.md). Example: 

```csharp
public class CustomRewards
{
    [MdpReward("upright_bonus", Description = "Reward for staying upright",
               Category = "locomotion")]
    public static float UprightBonus(MdpContext ctx, Dictionary<string, string> parameters)
    {
        // ... compute reward from ctx.MujocoScene, ctx.Agent, etc.
        return value;
    }
}
```

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
