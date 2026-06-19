# ExternalAgentRegistry

`static class` · namespace `Hazel`

Thread-safe bridge between the engine-driven Learn pipeline and the gRPC services. An external process (for example, a Python RL/IL loop) uses it to drive an external-mode RobotAgent batch. It holds the lock-step action gate, the latest observation/action snapshots, the negotiated task session, and the per-step reward/termination data.

```csharp
public static class ExternalAgentRegistry
```

## Methods

### ClearGrpcClientFlag() {#m-cleargrpcclientflag}

`static`

```csharp
public static void ClearGrpcClientFlag()
```

### ClearNegotiatedSession() {#m-clearnegotiatedsession}

`static`

```csharp
public static void ClearNegotiatedSession()
```

Clear the active negotiated session. Called on scene stop.

### CommitPendingGroups() {#m-commitpendinggroups}

`static`

```csharp
public static bool CommitPendingGroups()
```

Merge all pending action groups into the action buffer and signal the gate. Uses DefaultJointPosBuffer as the base for any indices not covered by groups. Called by Step() when action_groups are provided but no flat actions vector.

### EngineDriveExternalAgentActions() {#m-enginedriveexternalagentactions}

`static`

```csharp
public static void EngineDriveExternalAgentActions()
```

### EngineDriveExternalAgentUpdate() {#m-enginedriveexternalagentupdate}

`static`

```csharp
public static void EngineDriveExternalAgentUpdate()
```

### GetAgentName(int) {#m-getagentname}

`static`

```csharp
public static string GetAgentName(int agentIndex)
```

### GetCurrentFrameNumber() {#m-getcurrentframenumber}

`static`

```csharp
public static long GetCurrentFrameNumber()
```

### GetNegotiator() {#m-getnegotiator}

`static`

```csharp
public static ContractNegotiator GetNegotiator()
```

Get or create the contract negotiator singleton.

### GetOwnedActuatorIndices() {#m-getownedactuatorindices}

`static`

```csharp
public static System.Collections.Generic.HashSet<int> GetOwnedActuatorIndices()
```

### GetReadinessDiagnostic() {#m-getreadinessdiagnostic}

`static`

```csharp
public static string GetReadinessDiagnostic()
```

Returns a human-readable diagnostic string describing why the external agent batch is not ready, or empty string if ready. Thread-safe. Intended for polling from the editor UI and gRPC responses.

### HasGrpcClient() {#m-hasgrpcclient}

`static`

```csharp
public static bool HasGrpcClient()
```

### HasPendingGroups() {#m-haspendinggroups}

`static`

```csharp
public static bool HasPendingGroups()
```

Check if any action groups have been preloaded but not yet committed.

### InvalidateBatch() {#m-invalidatebatch}

`static`

```csharp
public static void InvalidateBatch()
```

### IsExternalBatchReady() {#m-isexternalbatchready}

`static`

```csharp
public static bool IsExternalBatchReady()
```

### MarkGrpcClientConnected() {#m-markgrpcclientconnected}

`static`

```csharp
public static void MarkGrpcClientConnected()
```

### NotifyExternalBatchSetupComplete(RobotManager) {#m-notifyexternalbatchsetupcomplete}

`static`

```csharp
public static void NotifyExternalBatchSetupComplete(RobotManager manager)
```

### PreloadActionGroup(string, float[], int[]) {#m-preloadactiongroup}

`static`

```csharp
public static bool PreloadActionGroup(string groupName, float[] actions, int[] indices)
```

Preload actions for a named group. Does NOT trigger a physics step. Multiple groups accumulate until CommitPendingGroups() or a flat TrySetExternalActions() call merges and signals the gate. Thread-safe. Called from gRPC handler threads.

### Register(RobotManager, RobotEnv, Entity) {#m-register}

`static`

```csharp
public static void Register(RobotManager manager, RobotEnv env, Entity mujocoEntity)
```

### SetResetCallback(Action?) {#m-setresetcallback}

`static`

```csharp
public static void SetResetCallback(Action? callback)
```

### TryCopyActionSlice(int, float[], long) {#m-trycopyactionslice}

`static`

```csharp
public static bool TryCopyActionSlice(int agentIndex, float[] destination, out long version)
```

### TryCopyEnrichedStepData(Dictionary<string, float>?, Dictionary<string, bool>?, bool, bool) {#m-trycopyenrichedstepdata}

`static`

```csharp
public static bool TryCopyEnrichedStepData(out Dictionary<string, float>? rewardSignals, out Dictionary<string, bool>? terminationFlags, out bool terminated, out bool truncated)
```

Copy the latest enriched step data for the gRPC response. Returns false if no enriched data is available.

### TryCopyStateSlice(int, float[], long) {#m-trycopystateslice}

`static`

```csharp
public static bool TryCopyStateSlice(int agentIndex, float[] destination, out long version)
```

### TryGetAgentByName(string, RobotAgent?, int) {#m-trygetagentbyname}

`static`

```csharp
public static bool TryGetAgentByName(string agentName, out RobotAgent? agent, out int agentIndex)
```

### TryGetAgentSizes(int, int, int) {#m-trygetagentsizes}

`static`

```csharp
public static bool TryGetAgentSizes(int agentIndex, out int stateSize, out int actionSize)
```

### TryGetExternalActionLayout(int, int, int) {#m-trygetexternalactionlayout}

`static`

```csharp
public static bool TryGetExternalActionLayout(out int agentCount, out int actionSize, out int totalActionSize)
```

### TryGetMujocoEntity(Entity) {#m-trygetmujocoentity}

`static`

```csharp
public static bool TryGetMujocoEntity(out Entity entity)
```

### TryResetAgentByName(string, Hazel.Rpc.SimulationContract?, string) {#m-tryresetagentbyname}

`static`

```csharp
public static bool TryResetAgentByName(string agentName, Hazel.Rpc.SimulationContract? simulationContract, out string message)
```

### TrySetExternalActions(IReadOnlyList<float>) {#m-trysetexternalactions}

`static`

```csharp
public static bool TrySetExternalActions(IReadOnlyList<float> controls)
```

### Unregister(RobotManager) {#m-unregister}

`static`

```csharp
public static void Unregister(RobotManager manager)
```

### WaitForObservation(long, CancellationToken, float) {#m-waitforobservation}

`static`

```csharp
public static Task<long> WaitForObservation(long currentFrame, CancellationToken cancellationToken, float timeoutS = 0.1f)
```


---
<small>Source: `Hazel/Learn/Utilities/ExternalAgentRegistry.cs`</small>
