# SimulationMode

`enum` · namespace `Hazel`

Controls how physics simulation timing relates to real-time.

```csharp
public enum SimulationMode
```

## Values

| Name | Value | Description |
|------|-------|-------------|
| <span id="m-realtime">`Realtime`</span> | `0` | Maintains real-time clock, may drop frames if overloaded. |
| <span id="m-deterministic">`Deterministic`</span> | `1` | Deterministic, capped at 1x real-time speed. |
| <span id="m-fast">`Fast`</span> | `2` | Runs physics as fast as possible with no real-time limit. |


---
<small>Source: `Hazel/Scene/Scene.cs`</small>
