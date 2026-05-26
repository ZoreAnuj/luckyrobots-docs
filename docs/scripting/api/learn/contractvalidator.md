# ContractValidator

`class` · namespace `Hazel`

Validates a TaskContract against the MdpComponentRegistry. Three-phase validation: 1. Existence: Does each requested term exist in the registry? 2. Parameters: Do supplied params match the component's schema? 3. Dependencies: Are all component dependencies satisfied? Produces actionable diagnostics with suggestions for fixing errors.

```csharp
public sealed class ContractValidator
```

## Methods

### Validate(Rpc.TaskContract) {#m-validate}

```csharp
public ValidationResult Validate(Rpc.TaskContract contract)
```

Validate a task contract against the current registry state. Returns a result with errors, warnings, and resolved optionals.


---
<small>Source: `Hazel/Learn/ContractValidator.cs`</small>
