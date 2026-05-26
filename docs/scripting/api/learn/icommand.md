# ICommand

`interface` · namespace `Hazel`

```csharp
public interface ICommand
```

## Properties

### Name {#m-name}

```csharp
string Name { get; }
```

### Size {#m-size}

```csharp
int Size { get; }
```

### Values {#m-values}

```csharp
float[] Values { get; }
```

## Methods

### ApplyConfig(Rpc.SimulationContract?) {#m-applyconfig}

```csharp
void ApplyConfig(Rpc.SimulationContract? contract)
```

### Sample(System.Random) {#m-sample}

```csharp
void Sample(System.Random rng)
```

### Step(float, System.Random) {#m-step}

```csharp
void Step(float dt, System.Random rng)
```


---
<small>Source: `Hazel/Learn/CommandManager.cs`</small>
