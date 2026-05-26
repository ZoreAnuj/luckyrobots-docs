# ObservationComponents

`static class` · namespace `Hazel.MdpComponents`

Built-in observation components that extract robot state from MuJoCo. Each component writes its values into a contiguous slice of the observation buffer at the provided offset. The hot path (Compute) must not allocate. Observation names match the existing StateSpec conventions used by RobotAgent subclasses and LuckyLab's ObservationSchema, ensuring backward compatibility with the flat observation vector layout.

```csharp
public static class ObservationComponents
```

## Methods

### RegisterAll() {#m-registerall}

`static`

```csharp
public static void RegisterAll()
```


---
<small>Source: `Hazel/Learn/MdpComponents/ObservationComponents.cs`</small>
