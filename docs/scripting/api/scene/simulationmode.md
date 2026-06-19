# SimulationMode

`enum` · namespace `Hazel`

Controls how physics simulation timing relates to real-time.

```csharp
public enum SimulationMode
```

## Values

| Name | Value | Description |
|------|-------|-------------|
| <span id="m-nondeterministic">`Nondeterministic`</span> | `0` | NOT reproducible; tracks the wall clock, dropping or repeating frames; do not use for training. |
| <span id="m-deterministicrealtime">`DeterministicRealtime`</span> | `1` | Fixed-step and reproducible, capped at 1x real-time; the default, for watchable runs. |
| <span id="m-deterministichighperf">`DeterministicHighPerf`</span> | `2` | Fixed-step and reproducible, as fast as the hardware allows; best for RL training. |


---
<small>Source: `Hazel/Scene/Scene.cs`</small>
