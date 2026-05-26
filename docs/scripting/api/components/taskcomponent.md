# TaskComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class TaskComponent : Component
```

## Properties

### IsActive {#m-isactive}

```csharp
public bool IsActive { get; set; }
```

### PendingTaskCount {#m-pendingtaskcount}

```csharp
public int PendingTaskCount { get; set; }
```

## Methods

### AddSubroutine(int, string) {#m-addsubroutine}

```csharp
public void AddSubroutine(int taskIndex, string typeFullName)
```

### AddTask(string) {#m-addtask}

```csharp
public void AddTask(string name)
```

### ClearTasks() {#m-cleartasks}

```csharp
public void ClearTasks()
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
