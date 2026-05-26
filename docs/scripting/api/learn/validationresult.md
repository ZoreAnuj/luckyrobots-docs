# ValidationResult

`class` · namespace `Hazel`

Result of contract validation. Aggregates errors, warnings, and optional resolution.

```csharp
public sealed class ValidationResult
```

## Properties

### Errors {#m-errors}

```csharp
public List<ValidationMessage> Errors { get; }
```

### IsValid {#m-isvalid}

```csharp
public bool IsValid { get; set; }
```

### ResolvedOptionals {#m-resolvedoptionals}

```csharp
public List<string> ResolvedOptionals { get; }
```

### UnresolvedOptionals {#m-unresolvedoptionals}

```csharp
public List<string> UnresolvedOptionals { get; }
```

### Warnings {#m-warnings}

```csharp
public List<ValidationMessage> Warnings { get; }
```

## Methods

### AddError(string, string, string, string) {#m-adderror}

```csharp
public void AddError(string component, string termName, string message, string suggestion)
```

### AddWarning(string, string, string, string) {#m-addwarning}

```csharp
public void AddWarning(string component, string termName, string message, string suggestion)
```


---
<small>Source: `Hazel/Learn/ContractValidator.cs`</small>
