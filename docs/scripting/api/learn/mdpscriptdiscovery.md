# MdpScriptDiscovery

`static class` · namespace `Hazel`

Discovers user-defined MDP components from loaded C# assemblies. Scans for methods decorated with [MdpObservation], [MdpReward], or [MdpTermination] attributes and wraps them as IMdpObservation/IMdpReward/ IMdpTermination implementations registered in MdpComponentRegistry. Called after ScriptEngine loads the project assembly (same timing as ScriptComponent discovery). This enables users to define custom reward functions, observations, and termination conditions in C# scripts without modifying engine code.

```csharp
public static class MdpScriptDiscovery
```

## Methods

### DiscoverAndRegister(Assembly) {#m-discoverandregister}

`static`

```csharp
public static void DiscoverAndRegister(Assembly assembly)
```

Scan an assembly for MDP-attributed methods and register them.


---
<small>Source: `Hazel/Learn/MdpScriptDiscovery.cs`</small>
