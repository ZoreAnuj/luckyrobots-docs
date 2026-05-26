# ContractNegotiator

`class` · namespace `Hazel`

Validates a TaskContract, resolves optional terms, configures the engine's observation/reward/termination pipeline, and returns a NegotiatedTaskSession that subsequent Step/Reset calls reference. The negotiator is the single entry point for setting up contract-driven training sessions. It replaces the implicit configuration where Step just returns whatever observations the engine happens to assemble. After negotiation, the engine knows exactly: - Which observations to assemble and in what order (ObservationSlot layout) - Which reward signals to compute each step - Which termination conditions to evaluate each step

```csharp
public sealed class ContractNegotiator
```

## Properties

### ActiveSession {#m-activesession}

```csharp
public NegotiatedSession ActiveSession { get; }
```

The currently active negotiated session, or null if none. Read by AgentBatch/ExternalAgentRegistry to determine whether to compute enriched step data (rewards, terminations).

## Methods

### ClearSession() {#m-clearsession}

```csharp
public void ClearSession()
```

Clear the active session. Called on scene stop.

### GetManifest(string) {#m-getmanifest}

```csharp
public ManifestSnapshot GetManifest(string robotName)
```

Build a capability manifest for the given robot. Delegates to MdpComponentRegistry.BuildManifest.

### Negotiate(Rpc.TaskContract) {#m-negotiate}

```csharp
public NegotiationOutcome Negotiate(Rpc.TaskContract contract)
```

Negotiate a task contract: validate, resolve optionals, configure engine. Returns a NegotiationOutcome with the session (on success) or validation errors.


---
<small>Source: `Hazel/Learn/ContractNegotiator.cs`</small>
