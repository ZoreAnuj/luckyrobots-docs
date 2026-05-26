# IAuxiliaryDataProvider

`interface` · namespace `Hazel`

Auxiliary data provider. Collects engine-side data not part of the standard observation vector, for use in Python-side reward computation or analysis.

```csharp
public interface IAuxiliaryDataProvider
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

### Collect(MdpContext, Dictionary<string, string>) {#m-collect}

```csharp
byte[] Collect(MdpContext ctx, Dictionary<string, string> parameters)
```

Collect auxiliary data and return as a byte array. Called per-step only when requested by the contract.


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
