# NegotiationOutcome

`class` · namespace `Hazel`

Result of a negotiation attempt.

```csharp
public sealed class NegotiationOutcome
```

## Properties

### Message {#m-message}

```csharp
public string Message { get; init; }
```

### Session {#m-session}

```csharp
public NegotiatedSession Session { get; init; }
```

### Success {#m-success}

```csharp
public bool Success { get; init; }
```

### Validation {#m-validation}

```csharp
public ValidationResult Validation { get; init; }
```


---
<small>Source: `Hazel/Learn/ContractNegotiator.cs`</small>
