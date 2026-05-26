# ParamDescriptor

`struct` · namespace `Hazel`

Interfaces for engine-side MDP (Markov Decision Process) components. These interfaces define the contract for observation, reward, termination, randomization, and auxiliary data providers that can be registered in the [`MdpComponentRegistry`](mdpcomponentregistry.md) and discovered by external training frameworks via the GetCapabilityManifest RPC. Design follows TimeManager's pattern: lightweight structs for data, interfaces for behavior, no allocations on the hot path.

```csharp
public struct ParamDescriptor
```

## Constructors

### ParamDescriptor(string, string, string, float, float, bool) {#m-paramdescriptor}

```csharp
public ParamDescriptor(string type, string defaultValue, string description, float rangeMin = 0f, float rangeMax = 0f, bool hasRange = false)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public string DefaultValue</code> |  |
| <code>public string Description</code> |  |
| <code>public bool HasRange</code> |  |
| <code>public float RangeMax</code> |  |
| <code>public float RangeMin</code> |  |
| <code>public string Type</code> |  |


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
