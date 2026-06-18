# CommandManager

`class` · namespace `Hazel`

Holds a RobotAgent's commands (e.g. a target velocity) and resamples them on their own schedule each tick.

```csharp
public class CommandManager
```

## Properties

### Commands {#m-commands}

```csharp
public IReadOnlyList<ICommand> Commands { get; }
```

## Methods

### Add(ICommand) {#m-add}

```csharp
public void Add(ICommand command)
```

### ApplyConfig(Rpc.SimulationContract?, System.Random) {#m-applyconfig}

```csharp
public void ApplyConfig(Rpc.SimulationContract? contract, System.Random rng)
```

### GetCommand(string) {#m-getcommand}

```csharp
public ICommand? GetCommand(string name)
```

### Step(float, System.Random) {#m-step}

```csharp
public void Step(float dt, System.Random rng)
```


---
<small>Source: `Hazel/Learn/CommandManager.cs`</small>
