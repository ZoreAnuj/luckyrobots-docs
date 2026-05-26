# RefPoseExtractor

`class` · namespace `Hazel`

```csharp
public class RefPoseExtractor
```

## Constructors

### RefPoseExtractor(float, Dictionary<string, (string axis, bool invert)>, uint) {#m-refposeextractor}

```csharp
public RefPoseExtractor(float dt, Dictionary<string, (string axis, bool invert)> axisConfig, uint datasetAnchorId = uint.MaxValue)
```

## Methods

### GetAnchorBoneName() {#m-getanchorbonename}

```csharp
public string? GetAnchorBoneName()
```

### GetMotionAngVelW_Hz(string) {#m-getmotionangvelw-hz}

```csharp
public Vector3 GetMotionAngVelW_Hz(string boneName)
```

### GetMotionJointPos() {#m-getmotionjointpos}

```csharp
public float[] GetMotionJointPos()
```

### GetMotionJointVel() {#m-getmotionjointvel}

```csharp
public float[] GetMotionJointVel()
```

### GetMotionLinVelW_Hz(string) {#m-getmotionlinvelw-hz}

```csharp
public Vector3 GetMotionLinVelW_Hz(string boneName)
```

### GetMotionPosW_Hz(string) {#m-getmotionposw-hz}

```csharp
public Vector3 GetMotionPosW_Hz(string boneName)
```

### GetMotionQuatW_Hz(string) {#m-getmotionquatw-hz}

```csharp
public Quaternion GetMotionQuatW_Hz(string boneName)
```

### UpdatePoseData() {#m-updateposedata}

```csharp
public void UpdatePoseData()
```


---
<small>Source: `Hazel/Learn/Utilities/RefPoseExtractor.cs`</small>
