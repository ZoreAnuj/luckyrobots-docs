# RobotManager

`class` · namespace `Hazel`

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

### Dispose() {#m-dispose}

```csharp
public void Dispose()
```

### GetActions() {#m-getactions}

```csharp
public void GetActions()
```

### GetAgent(int) {#m-getagent}

```csharp
public RobotAgent? GetAgent(int id)
```

### Reset() {#m-reset}

```csharp
public void Reset()
```

### Setup(MujocoSceneComponent) {#m-setup}

```csharp
public void Setup(MujocoSceneComponent mujocoScene)
```

### UpdateCommands() {#m-updatecommands}

```csharp
public void UpdateCommands()
```


---
<small>Source: `Hazel/Learn/RobotManager.cs`</small>
