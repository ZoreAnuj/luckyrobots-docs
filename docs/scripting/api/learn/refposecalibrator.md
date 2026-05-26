# RefPoseCalibrator

`class` · namespace `Hazel`

```csharp
public class RefPoseCalibrator
```

## Constructors

### RefPoseCalibrator(MujocoSceneComponent, RobotControllerComponent) {#m-refposecalibrator}

```csharp
public RefPoseCalibrator(MujocoSceneComponent scene, RobotControllerComponent controller)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public float LiftHeight_Hz</code> |  |
| <code>public Vector3 PelvisPositionOffset_Hz</code> |  |
| <code>public Vector3 PelvisRotationOffsetEuler_Hz</code> |  |

## Methods

### AddTarget(string, string) {#m-addtarget}

```csharp
public void AddTarget(string graphInputName, string refBoneTag)
```

### CalculateOffsets(RobotAgent) {#m-calculateoffsets}

```csharp
public float[] CalculateOffsets(RobotAgent agent)
```

### Reset(float) {#m-reset}

```csharp
public void Reset(float duration)
```

### UnweldPelvis() {#m-unweldpelvis}

```csharp
public void UnweldPelvis()
```

### Update(float) {#m-update}

```csharp
public bool Update(float dt)
```


---
<small>Source: `Hazel/Learn/Utilities/RefPoseCalibrator.cs`</small>
