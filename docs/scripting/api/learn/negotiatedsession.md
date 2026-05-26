# NegotiatedSession

`class` · namespace `Hazel`

Runtime state for a negotiated task session. Contains everything needed to compute enriched StepResponse data.

```csharp
public sealed class NegotiatedSession
```

## Properties

### ActionGroupSlots {#m-actiongroupslots}

```csharp
public List<ActionGroupSlotInfo> ActionGroupSlots { get; init; }
```

Resolved action group layout for multi-policy control.

### Contract {#m-contract}

```csharp
public Rpc.TaskContract Contract { get; init; }
```

### ObservationParams {#m-observationparams}

```csharp
public Dictionary<string, Dictionary<string, string>> ObservationParams { get; init; }
```

Pre-resolved parameter dictionaries for observation computation. Keyed by term name.

### ObservationSlots {#m-observationslots}

```csharp
public List<ObservationSlotInfo> ObservationSlots { get; init; }
```

### RewardParams {#m-rewardparams}

```csharp
public Dictionary<string, Dictionary<string, string>> RewardParams { get; init; }
```

Pre-resolved parameter dictionaries for reward computation. Keyed by term name.

### RewardTermNames {#m-rewardtermnames}

```csharp
public List<string> RewardTermNames { get; init; }
```

### SessionId {#m-sessionid}

```csharp
public string SessionId { get; init; }
```

### TerminationParams {#m-terminationparams}

```csharp
public Dictionary<string, Dictionary<string, string>> TerminationParams { get; init; }
```

Pre-resolved parameter dictionaries for termination evaluation. Keyed by term name.

### TerminationTermNames {#m-terminationtermnames}

```csharp
public List<string> TerminationTermNames { get; init; }
```

### TotalObservationSize {#m-totalobservationsize}

```csharp
public int TotalObservationSize { get; set; }
```

Total resolved observation vector size. 0 if observations are not contract-driven.


---
<small>Source: `Hazel/Learn/ContractNegotiator.cs`</small>
