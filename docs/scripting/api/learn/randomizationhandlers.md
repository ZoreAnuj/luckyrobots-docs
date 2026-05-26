# RandomizationHandlers

`static class` · namespace `Hazel.MdpComponents`

Built-in domain randomization handlers. These wrap the existing SimulationContract DR fields as IRandomizationHandler implementations, making them discoverable via the capability manifest. The actual randomization is applied during agent reset via MujocoSceneComponent or RobotEnv native calls. Note: The existing SimulationContract/CommandManager path already handles these randomizations during AgentBatch.ResetAgent(). These handlers exist primarily for manifest discovery and contract validation — they document what the engine supports. The Apply() methods provide a registry-driven alternative for contract-negotiated sessions.

```csharp
public static class RandomizationHandlers
```

## Methods

### RegisterAll() {#m-registerall}

`static`

```csharp
public static void RegisterAll()
```


---
<small>Source: `Hazel/Learn/MdpComponents/RandomizationHandlers.cs`</small>
