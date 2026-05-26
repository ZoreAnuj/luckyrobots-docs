# MdpContext

`class` · namespace `Hazel`

Immutable context passed to MDP component Compute/Evaluate methods. Provides access to the MuJoCo scene, agent state, and per-step timing without exposing mutable engine internals. Analogous to TimeManager's StepContext: immutable during callback execution, no dynamic allocation, all data available by reference.

```csharp
public sealed class MdpContext
```

## Constructors

### MdpContext(MujocoSceneComponent, RobotAgent, CommandManager, float, int, float, float[], float[]) {#m-mdpcontext}

```csharp
public MdpContext(MujocoSceneComponent mujocoScene, RobotAgent agent, CommandManager commands, float dt, int stepCount, float maxEpisodeLengthS, float[] previousActions, float[] currentActions)
```

## Properties

### Agent {#m-agent}

```csharp
public RobotAgent Agent { get; }
```

### Commands {#m-commands}

```csharp
public CommandManager Commands { get; }
```

### CurrentActions {#m-currentactions}

```csharp
public float[] CurrentActions { get; }
```

### Dt {#m-dt}

```csharp
public float Dt { get; }
```

### MaxEpisodeLengthS {#m-maxepisodelengths}

```csharp
public float MaxEpisodeLengthS { get; }
```

### MujocoScene {#m-mujocoscene}

```csharp
public MujocoSceneComponent MujocoScene { get; }
```

### PreviousActions {#m-previousactions}

```csharp
public float[] PreviousActions { get; }
```

### StepCount {#m-stepcount}

```csharp
public int StepCount { get; }
```


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
