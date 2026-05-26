# MdpComponentRegistry

`static class` · namespace `Hazel`

Central registration table for engine-side MDP components. All built-in observation functions, reward signals, termination conditions, randomization handlers, and auxiliary data providers register here at initialization. User-defined C# script components are discovered and registered at runtime via [`MdpScriptDiscovery`](mdpscriptdiscovery.md). Thread safety: Registration happens during initialization (single-threaded). Lookups happen from gRPC threads during negotiation and from the main thread during step computation. The registry is immutable after initialization, so no locking is needed for reads. Design follows TimeManager's pattern: - Static singleton (like PhysicsSystem) - Pre-allocated dictionaries, no per-frame allocation - Clear separation between registration (init) and lookup (runtime)

```csharp
public static class MdpComponentRegistry
```

## Properties

### AuxiliaryData {#m-auxiliarydata}

`static`

```csharp
public static IReadOnlyDictionary<string, IAuxiliaryDataProvider> AuxiliaryData { get; }
```

Get all registered auxiliary data descriptors.

### Observations {#m-observations}

`static`

```csharp
public static IReadOnlyDictionary<string, IMdpObservation> Observations { get; }
```

Get all registered observation descriptors.

### Randomizations {#m-randomizations}

`static`

```csharp
public static IReadOnlyDictionary<string, IRandomizationHandler> Randomizations { get; }
```

Get all registered randomization descriptors.

### Rewards {#m-rewards}

`static`

```csharp
public static IReadOnlyDictionary<string, IMdpReward> Rewards { get; }
```

Get all registered reward descriptors.

### Terminations {#m-terminations}

`static`

```csharp
public static IReadOnlyDictionary<string, IMdpTermination> Terminations { get; }
```

Get all registered termination descriptors.

## Methods

### BuildManifest(string) {#m-buildmanifest}

`static`

```csharp
public static ManifestSnapshot BuildManifest(string robotName = "")
```

Build a capability manifest describing all registered MDP components. Optionally filtered by robot name (empty = include all). Called by GetCapabilityManifest RPC.

### Clear() {#m-clear}

`static`

```csharp
public static void Clear()
```

Clear all registered components. Used for testing and scene teardown.

### GetAuxiliary(string) {#m-getauxiliary}

`static`

```csharp
public static IAuxiliaryDataProvider GetAuxiliary(string name)
```

Get an auxiliary data provider by name. Returns null if not found.

### GetObservation(string) {#m-getobservation}

`static`

```csharp
public static IMdpObservation GetObservation(string name)
```

Get an observation component by name. Returns null if not found.

### GetRandomization(string) {#m-getrandomization}

`static`

```csharp
public static IRandomizationHandler GetRandomization(string name)
```

Get a randomization handler by name. Returns null if not found.

### GetReward(string) {#m-getreward}

`static`

```csharp
public static IMdpReward GetReward(string name)
```

Get a reward component by name. Returns null if not found.

### GetTermination(string) {#m-gettermination}

`static`

```csharp
public static IMdpTermination GetTermination(string name)
```

Get a termination component by name. Returns null if not found.

### HasAuxiliary(string) {#m-hasauxiliary}

`static`

```csharp
public static bool HasAuxiliary(string name)
```

Check if an auxiliary data provider is registered.

### HasObservation(string) {#m-hasobservation}

`static`

```csharp
public static bool HasObservation(string name)
```

Check if an observation component is registered.

### HasRandomization(string) {#m-hasrandomization}

`static`

```csharp
public static bool HasRandomization(string name)
```

Check if a randomization handler is registered.

### HasReward(string) {#m-hasreward}

`static`

```csharp
public static bool HasReward(string name)
```

Check if a reward component is registered.

### HasTermination(string) {#m-hastermination}

`static`

```csharp
public static bool HasTermination(string name)
```

Check if a termination component is registered.

### Initialize() {#m-initialize}

`static`

```csharp
public static void Initialize()
```

Initialize the registry with all built-in MDP components. Called once during engine startup, before any gRPC connections. Idempotent — subsequent calls are no-ops.

### Register(IAuxiliaryDataProvider) {#m-register-5}

`static`

```csharp
public static void Register(IAuxiliaryDataProvider component)
```

Register an auxiliary data provider. Overwrites if name already exists.

### Register(IMdpObservation) {#m-register}

`static`

```csharp
public static void Register(IMdpObservation component)
```

Register an observation component. Overwrites if name already exists.

### Register(IMdpReward) {#m-register-2}

`static`

```csharp
public static void Register(IMdpReward component)
```

Register a reward component. Overwrites if name already exists.

### Register(IMdpTermination) {#m-register-3}

`static`

```csharp
public static void Register(IMdpTermination component)
```

Register a termination component. Overwrites if name already exists.

### Register(IRandomizationHandler) {#m-register-4}

`static`

```csharp
public static void Register(IRandomizationHandler component)
```

Register a randomization handler. Overwrites if name already exists.


---
<small>Source: `Hazel/Learn/MdpComponentRegistry.cs`</small>
