# RobotEnv

`class` · namespace `Hazel`

Internal per-policy batching layer owned by RobotManager. Scripts construct a RobotManager, not a RobotEnv.

```csharp
public class RobotEnv
```

## Constructors

### RobotEnv(RobotManager, float, Entity) {#m-robotenv}

```csharp
public RobotEnv(RobotManager manager, float dt, Entity mujocoEntity)
```

## Methods

### CollectState() {#m-collectstate}

```csharp
public void CollectState()
```

Engine-driven via RobotManager. Not called from scripts.

### GetActions() {#m-getactions}

```csharp
public void GetActions()
```

Engine-driven via RobotManager. Not called from scripts.

### RegisterAgent(RobotAgent) {#m-registeragent}

```csharp
public void RegisterAgent(RobotAgent agent)
```

### Reset() {#m-reset}

```csharp
public void Reset()
```

### Setup(RobotManager, MujocoSceneComponent) {#m-setup}

```csharp
public void Setup(RobotManager robotManager, MujocoSceneComponent mujocoScene)
```

### UpdateCommands() {#m-updatecommands}

```csharp
public void UpdateCommands()
```

Engine-driven via RobotManager. Not called from scripts.


---
<small>Source: `Hazel/Learn/RobotEnv.cs`</small>
