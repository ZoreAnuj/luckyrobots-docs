# ValidationMessage

`class` · namespace `Hazel`

Individual validation message with actionable suggestion.

```csharp
public sealed class ValidationMessage
```

## Constructors

### ValidationMessage(string, string, string, string, string) {#m-validationmessage}

```csharp
public ValidationMessage(string severity, string component, string termName, string message, string suggestion)
```

## Properties

### Component {#m-component}

```csharp
public string Component { get; }
```

### Message {#m-message}

```csharp
public string Message { get; }
```

### Severity {#m-severity}

```csharp
public string Severity { get; }
```

### Suggestion {#m-suggestion}

```csharp
public string Suggestion { get; }
```

### TermName {#m-termname}

```csharp
public string TermName { get; }
```


---
<small>Source: `Hazel/Learn/ContractValidator.cs`</small>
