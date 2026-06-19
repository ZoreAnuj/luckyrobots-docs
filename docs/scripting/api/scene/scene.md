# Scene

`class` · namespace `Hazel`

The active simulation scene: its entities, components, and settings. Find entities, read or set the SimulationMode, and drive playback from scripts.

Inherits [`Asset<Scene>`](../core/asset-t.md)

```csharp
public sealed class Scene : Asset<Scene>
```

## Properties

### ActionGateSettleSteps {#m-actiongatesettlesteps}

`static`

```csharp
public static int ActionGateSettleSteps { get; }
```

Settle ticks after each reset before the External Action Gate re-arms. Configurable per scene in the External Action Gate settings; default 20.

### ControlRunnerHz {#m-controlrunnerhz}

`static`

```csharp
public static double ControlRunnerHz { get; }
```

Frequency (Hz) of the time runner the External Action Gate / AgentService.Step advances. One gated Step is exactly 1/this seconds of sim time. 0 if unavailable.

### SimulationMode {#m-simulationmode}

`static`

```csharp
public static SimulationMode SimulationMode { get; set; }
```

Gets or sets the simulation timing mode. Use [`SimulationMode.DeterministicHighPerf`](simulationmode.md#m-deterministichighperf) for RL training to run faster than real-time.

### TimeScale {#m-timescale}

`static`

```csharp
public static float TimeScale { set; }
```

## Methods

### CreateEntity(string) {#m-createentity}

`static`

```csharp
public static Entity CreateEntity(string tag = "Unnamed")
```

### DestroyAllChildren(Entity) {#m-destroyallchildren}

`static`

```csharp
public static void DestroyAllChildren(Entity entity)
```

### DestroyEntity(Entity) {#m-destroyentity}

`static`

```csharp
public static void DestroyEntity(Entity entity)
```

### FindEntityByID(ulong) {#m-findentitybyid}

`static`

```csharp
public static Entity? FindEntityByID(ulong entityID)
```

### FindEntityByTag(string) {#m-findentitybytag}

`static`

```csharp
public static Entity? FindEntityByTag(string tag)
```

### GetEntities() {#m-getentities}

`static`

```csharp
public static Entity[] GetEntities()
```

### InstantiatePrefab(Prefab) {#m-instantiateprefab}

`static`

```csharp
public static Entity? InstantiatePrefab(Prefab prefab)
```

### InstantiatePrefab(Prefab, Transform) {#m-instantiateprefab-5}

`static`

```csharp
public static Entity? InstantiatePrefab(Prefab prefab, Transform transform)
```

### InstantiatePrefab(Prefab, Vector3) {#m-instantiateprefab-2}

`static`

```csharp
public static Entity? InstantiatePrefab(Prefab prefab, Vector3 translation)
```

### InstantiatePrefab(Prefab, Vector3, Vector3) {#m-instantiateprefab-3}

`static`

```csharp
public static Entity? InstantiatePrefab(Prefab prefab, Vector3 translation, Vector3 rotation)
```

### InstantiatePrefab(Prefab, Vector3, Vector3, Vector3) {#m-instantiateprefab-4}

`static`

```csharp
public static Entity? InstantiatePrefab(Prefab prefab, Vector3 translation, Vector3 rotation, Vector3 scale)
```

### InstantiatePrefabWithParent(Prefab, Entity) {#m-instantiateprefabwithparent}

`static`

```csharp
public static Entity? InstantiatePrefabWithParent(Prefab prefab, Entity parent)
```

### InstantiatePrefabWithParent(Prefab, Transform, Entity) {#m-instantiateprefabwithparent-5}

`static`

```csharp
public static Entity? InstantiatePrefabWithParent(Prefab prefab, Transform transform, Entity parent)
```

### InstantiatePrefabWithParent(Prefab, Vector3, Entity) {#m-instantiateprefabwithparent-2}

`static`

```csharp
public static Entity? InstantiatePrefabWithParent(Prefab prefab, Vector3 translation, Entity parent)
```

### InstantiatePrefabWithParent(Prefab, Vector3, Vector3, Entity) {#m-instantiateprefabwithparent-3}

`static`

```csharp
public static Entity? InstantiatePrefabWithParent(Prefab prefab, Vector3 translation, Vector3 rotation, Entity parent)
```

### InstantiatePrefabWithParent(Prefab, Vector3, Vector3, Vector3, Entity) {#m-instantiateprefabwithparent-4}

`static`

```csharp
public static Entity? InstantiatePrefabWithParent(Prefab prefab, Vector3 translation, Vector3 rotation, Vector3 scale, Entity parent)
```


---
<small>Source: `Hazel/Scene/Scene.cs`</small>
