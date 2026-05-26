# TerminationComponents

`static class` · namespace `Hazel.MdpComponents`

Built-in termination conditions evaluated from MuJoCo state. Each component returns a boolean indicating whether the episode should end. The IsTimeout property distinguishes between hard terminations (failures) and truncations (time limits) — this distinction matters for value function bootstrapping in RL algorithms.

```csharp
public static class TerminationComponents
```

## Methods

### RegisterAll() {#m-registerall}

`static`

```csharp
public static void RegisterAll()
```


---
<small>Source: `Hazel/Learn/MdpComponents/TerminationComponents.cs`</small>
