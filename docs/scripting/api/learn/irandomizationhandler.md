# IRandomizationHandler

`interface` · namespace `Hazel`

Domain randomization handler. Applies randomization to MuJoCo model parameters during reset.

```csharp
public interface IRandomizationHandler
```

## Properties

### Descriptor {#m-descriptor}

```csharp
RandomizationDescriptor Descriptor { get; }
```

Capability descriptor for the manifest.

### Name {#m-name}

```csharp
string Name { get; }
```

Unique identifier used in contracts and manifests.

## Methods

### Apply(MujocoSceneComponent, float, float, System.Random) {#m-apply}

```csharp
void Apply(MujocoSceneComponent mujocoScene, float rangeMin, float rangeMax, System.Random rng)
```

Apply randomization within the given range using the provided RNG. Called during agent reset.


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
