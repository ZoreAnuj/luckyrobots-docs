# ManifestSnapshot

`class` · namespace `Hazel`

Snapshot of all MDP component descriptors for a given robot. Produced by [`MdpComponentRegistry.BuildManifest`](mdpcomponentregistry.md#m-buildmanifest) and serialized to protobuf for the GetCapabilityManifest RPC response.

```csharp
public sealed class ManifestSnapshot
```

## Properties

### AuxiliaryData {#m-auxiliarydata}

```csharp
public List<ComponentDescriptor> AuxiliaryData { get; }
```

### Observations {#m-observations}

```csharp
public List<ComponentDescriptor> Observations { get; }
```

### Randomizations {#m-randomizations}

```csharp
public List<RandomizationDescriptor> Randomizations { get; }
```

### Rewards {#m-rewards}

```csharp
public List<ComponentDescriptor> Rewards { get; }
```

### Terminations {#m-terminations}

```csharp
public List<ComponentDescriptor> Terminations { get; }
```


---
<small>Source: `Hazel/Learn/MdpComponentRegistry.cs`</small>
