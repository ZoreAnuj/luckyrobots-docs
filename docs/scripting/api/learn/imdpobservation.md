# IMdpObservation

`interface` · namespace `Hazel`

Engine-side observation function. Computes a slice of the observation vector from MuJoCo/agent state. Each implementation corresponds to one named observation term (e.g., "base_lin_vel", "joint_pos", "projected_gravity").

```csharp
public interface IMdpObservation
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

### Compute(MdpContext, Dictionary<string, string>, float[], int) {#m-compute}

```csharp
int Compute(MdpContext ctx, Dictionary<string, string> parameters, float[] buffer, int offset)
```

Compute observation values into the provided buffer. Returns the number of floats written. Must not exceed OutputSize. Hot path — must not allocate.

### GetOutputSize(RobotAgent, Dictionary<string, string>) {#m-getoutputsize}

```csharp
int GetOutputSize(RobotAgent agent, Dictionary<string, string> parameters)
```

Number of floats this observation produces. May depend on the agent (e.g., joint count). Called once during negotiation, not per-step.


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
