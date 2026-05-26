# VelocityCommand

`class` · namespace `Hazel`

Implements [`ICommand`](icommand.md)

```csharp
public class VelocityCommand : ICommand
```

## Properties

### Name {#m-name}

```csharp
public string Name { get; }
```

### Size {#m-size}

```csharp
public int Size { get; }
```

### Values {#m-values}

```csharp
public float[] Values { get; }
```

## Methods

### ApplyConfig(Rpc.SimulationContract?) {#m-applyconfig}

```csharp
public void ApplyConfig(Rpc.SimulationContract? contract)
```

### Sample(System.Random) {#m-sample}

```csharp
public void Sample(System.Random rng)
```

### Step(float, System.Random) {#m-step}

```csharp
public void Step(float dt, System.Random rng)
```


---
<small>Source: `Hazel/Learn/CommandManager.cs`</small>
