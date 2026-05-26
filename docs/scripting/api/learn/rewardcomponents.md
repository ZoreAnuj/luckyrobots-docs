# RewardComponents

`static class` · namespace `Hazel.MdpComponents`

Built-in reward signal components computed from MuJoCo state. Each component returns a scalar float per step. The training framework receives these as named signals in the enriched StepResponse and combines them with user-defined weights into the final reward. Conventions: - Tracking rewards use exp(-scale * error^2) — bounded [0, 1], 1 = perfect - Penalty rewards return negative values (magnitude of undesired quantity) - All computations are in MuJoCo coordinate space (Z-up) - Hot path: no allocations, no dictionary lookups per step

```csharp
public static class RewardComponents
```

## Methods

### RegisterAll() {#m-registerall}

`static`

```csharp
public static void RegisterAll()
```


---
<small>Source: `Hazel/Learn/MdpComponents/RewardComponents.cs`</small>
