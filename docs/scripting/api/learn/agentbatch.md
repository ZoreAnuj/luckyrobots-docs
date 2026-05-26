# AgentBatch

`class` · namespace `Hazel`

```csharp
public class AgentBatch
```

## Constructors

### AgentBatch(string, string) {#m-agentbatch}

```csharp
public AgentBatch(string policyName, string policyPath)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public float[] ActionBuffer</code> |  |
| <code>public int[] ActuatorBuffer</code> |  |
| <code>public float[] CtrlBuffer</code> |  |
| <code>public float[] DefaultJointPosBuffer</code> |  |
| <code>public List&lt;RobotAgent&gt; m_Agents</code> |  |
| <code>public float[] StateBuffer</code> |  |

## Properties

### Entity {#m-entity}

```csharp
public Entity Entity { get; }
```

## Methods

### CollectState(RobotManager) {#m-collectstate}

```csharp
public void CollectState(RobotManager robotManager)
```

### GetActions(RobotManager) {#m-getactions}

```csharp
public void GetActions(RobotManager robotManager)
```

### Reset(RobotManager, IReadOnlyList<int>?) {#m-reset}

```csharp
public void Reset(RobotManager robotManager, IReadOnlyList<int>? agentIndices = null)
```

### Setup(RobotManager, MujocoSceneComponent, float) {#m-setup}

```csharp
public void Setup(RobotManager robotManager, MujocoSceneComponent mujocoScene, float dt)
```

### UpdateCommands(RobotManager) {#m-updatecommands}

```csharp
public void UpdateCommands(RobotManager robotManager)
```


---
<small>Source: `Hazel/Learn/AgentBatch.cs`</small>
