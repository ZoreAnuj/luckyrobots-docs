# HzObservations

`static class` · namespace `Hazel`

```csharp
public static class HzObservations
```

## Methods

### BaseAngVelB(Quaternion, Vector3) {#m-baseangvelb}

`static`

```csharp
public static Vector3 BaseAngVelB(in Quaternion baseQuatW, in Vector3 baseAngVelW)
```

### BaseLinVelB(Quaternion, Vector3) {#m-baselinvelb}

`static`

```csharp
public static Vector3 BaseLinVelB(in Quaternion baseQuatW, in Vector3 baseLinVelW)
```

### MotionAnchorAngVelB(Vector3, Quaternion) {#m-motionanchorangvelb}

`static`

```csharp
public static Vector3 MotionAnchorAngVelB(in Vector3 targetAngVelW, in Quaternion robotAnchorQuat)
```

### MotionAnchorLinVelB(Vector3, Quaternion) {#m-motionanchorlinvelb}

`static`

```csharp
public static Vector3 MotionAnchorLinVelB(in Vector3 targetLinVelW, in Quaternion robotAnchorQuat)
```

### MotionAnchorOriB(Quaternion, Quaternion, float[]) {#m-motionanchororib}

`static`

```csharp
public static void MotionAnchorOriB(in Quaternion robotAnchorQuatMj, in Quaternion motionAnchorQuatMj, float[] out6)
```

### MotionAnchorPosB(Vector3, Quaternion, Vector3, Quaternion) {#m-motionanchorposb}

`static`

```csharp
public static Vector3 MotionAnchorPosB(in Vector3 robotAnchorPos, in Quaternion robotAnchorQuat, in Vector3 motionAnchorPos, in Quaternion motionAnchorQuat)
```

### RobotBodyOriB(Quaternion, IReadOnlyList<Quaternion>, float[]) {#m-robotbodyorib}

`static`

```csharp
public static void RobotBodyOriB(in Quaternion robotAnchorQuat, IReadOnlyList<Quaternion> bodyOrientations, float[] out6N)
```

### RobotBodyPosB(Vector3, Quaternion, IReadOnlyList<Vector3>, IReadOnlyList<Quaternion>, float[]) {#m-robotbodyposb}

`static`

```csharp
public static void RobotBodyPosB(in Vector3 robotAnchorPos, in Quaternion robotAnchorQuat, IReadOnlyList<Vector3> bodyPositions, IReadOnlyList<Quaternion> bodyOrientations, float[] out3N)
```


---
<small>Source: `Hazel/Learn/Utilities/HzObservation.cs`</small>
