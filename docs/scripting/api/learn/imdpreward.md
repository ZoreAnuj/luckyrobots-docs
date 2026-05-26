# IMdpReward

`interface` · namespace `Hazel`

Engine-side reward signal. Computes a scalar reward from MuJoCo/agent state each step. Examples: velocity tracking error, joint acceleration penalty, feet air time.

```csharp
public interface IMdpReward
```

## Properties

### Descriptor {#m-descriptor}

```csharp
ComponentDescriptor Descriptor { get; }
```

Capability descriptor for the manifest.

### Name {#m-name}

```csharp
string Name { get; }
```

Unique identifier used in contracts and manifests.

## Methods

### Compute(MdpContext, Dictionary<string, string>) {#m-compute}

```csharp
float Compute(MdpContext ctx, Dictionary<string, string> parameters)
```

Compute the scalar reward value for the current step. Hot path — must not allocate.


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
