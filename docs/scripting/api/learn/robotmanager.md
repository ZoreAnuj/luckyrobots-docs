# RobotManager

`class` · namespace `Hazel`

Owns the per-tick Learn pipeline for a set of RobotAgents. The author surface is setup only. Subclass RobotAgent, construct new RobotManager(numAgents, dt, mujocoEntity, agentFactory), then call Setup(mujocoScene). Call Dispose() on teardown. The engine then drives UpdateCommands/CollectState/GetActions each tick. Scenes do not call those.

Implements `IDisposable`

```csharp
public class RobotManager : IDisposable
```

## Constructors

### RobotManager(int, float, Entity, Func<int, RobotAgent>) {#m-robotmanager}

```csharp
public RobotManager(int numAgents, float dt, Entity mujocoEntity, Func<int, RobotAgent> agentFactory)
```

## Properties

### NativeHandle {#m-nativehandle}

```csharp
public IntPtr NativeHandle { get; }
```

## Methods

### CollectState() {#m-collectstate}

```csharp
public void CollectState()
```

Engine-driven. Called each tick at the OnUpdate point to gather observations and publish them for the gRPC Step. A scene does not call it.

### Dispose() {#m-dispose}

```csharp
public void Dispose()
```

### GetActions() {#m-getactions}

```csharp
public void GetActions()
```

Engine-driven. Called on the PreMujocoStep event (post motion-graph, pre-physics) to write the latest external action to mjData.ctrl. A scene does not call it.

### GetAgent(int) {#m-getagent}

```csharp
public RobotAgent? GetAgent(int id)
```

### Reset() {#m-reset}

```csharp
public void Reset()
```

Re-initializes every agent (clears buffers, resamples commands, applies the reset pose). Engine/RPC-driven (for example, after a reset). Not part of the script setup path.

### Setup(MujocoSceneComponent) {#m-setup}

```csharp
public void Setup(MujocoSceneComponent mujocoScene)
```

### UpdateCommands() {#m-updatecommands}

```csharp
public void UpdateCommands()
```

Engine-driven. Called each tick at the OnUpdate point to step the agent's command generators. A scene does not call it; it only constructs the RobotManager and calls Setup().


---
<small>Source: `Hazel/Learn/RobotManager.cs`</small>
