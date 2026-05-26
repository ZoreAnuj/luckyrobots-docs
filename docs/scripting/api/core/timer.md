# Timer

`static class` · namespace `Hazel`

```csharp
public static class Timer
```

## Methods

### Clear(Handle) {#m-clear}

`static`

```csharp
public static void Clear(Handle inHandle)
```

### ClearAll() {#m-clearall}

`static`

```csharp
public static void ClearAll()
```

### Internal_Tick(float) {#m-internal-tick}

`static`

```csharp
public static bool Internal_Tick(float ts)
```

### IsActive(Handle) {#m-isactive}

`static`

```csharp
public static bool IsActive(Handle inHandle)
```

### Set(Action, float, bool, float) {#m-set}

`static`

```csharp
public static Handle Set(Action callback, float rateSeconds, bool looping = false, float firstDelaySeconds = -1.0f)
```


---
<small>Source: `Hazel/Core/Timer.cs`</small>
