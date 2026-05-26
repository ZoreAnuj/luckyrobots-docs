# IMdpTermination

`interface` · namespace `Hazel`

Engine-side termination condition. Evaluates whether the episode should end based on MuJoCo/agent state.

```csharp
public interface IMdpTermination
```

## Properties

### Descriptor {#m-descriptor}

```csharp
ComponentDescriptor Descriptor { get; }
```

Capability descriptor for the manifest.

### IsTimeout {#m-istimeout}

```csharp
bool IsTimeout { get; }
```

Whether this is a timeout (truncation) vs. a hard termination. Truncation: episode ended due to time limit (not a failure). Termination: episode ended due to constraint violation (failure).

### Name {#m-name}

```csharp
string Name { get; }
```

Unique identifier used in contracts and manifests.

## Methods

### Evaluate(MdpContext, Dictionary<string, string>) {#m-evaluate}

```csharp
bool Evaluate(MdpContext ctx, Dictionary<string, string> parameters)
```

Evaluate whether the termination condition is met. Hot path — must not allocate.


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
