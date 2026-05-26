# RobotAgent

`abstract class` · namespace `Hazel`

```csharp
public abstract class RobotAgent
```

## Constructors

### RobotAgent(float, string, string) {#m-robotagent}

```csharp
public RobotAgent(float dt, string motionDataset = "", string policyPath = "")
```

## Fields

| Field | Description |
|-------|-------------|
| <code>protected float[] m_ActionBuffer</code> |  |
| <code>protected int m_ActionOffset</code> |  |
| <code>protected int[] m_ActuatorIds</code> |  |
| <code>protected AnimationComponent? m_AnimationComponent</code> |  |
| <code>protected RefPoseCalibrator? m_Calibrator</code> |  |
| <code>protected float[] m_CtrlBuffer</code> |  |
| <code>protected int m_CtrlOffset</code> |  |
| <code>public uint m_DatasetAnchorId</code> |  |
| <code>protected uint m_FreeJointId</code> |  |
| <code>public bool m_IsCalibrating</code> |  |
| <code>protected uint[] m_JointQposAdrs</code> |  |
| <code>protected uint[] m_JointQvelAdrs</code> |  |
| <code>public MujocoSceneComponent m_MujocoScene</code> |  |
| <code>public uint m_PelvisBodyId</code> |  |
| <code>public uint m_PelvisDatasetId</code> |  |
| <code>public RefPoseExtractor? m_RefPoseExtractor</code> |  |
| <code>protected uint m_RobotAnchorId</code> |  |
| <code>protected RobotManager m_RobotManager</code> |  |
| <code>protected float[] m_StateBuffer</code> |  |
| <code>protected int m_StateOffset</code> |  |

## Properties

### ActionSize {#m-actionsize}

```csharp
public int ActionSize { get; protected set; }
```

### CalibrationTargets {#m-calibrationtargets}

```csharp
protected virtual List<CalibrationMapping> CalibrationTargets { get; }
```

### CommandManager {#m-commandmanager}

```csharp
public CommandManager CommandManager { get; }
```

### JointSpec {#m-jointspec}

```csharp
protected abstract List<JointConfig> JointSpec { get; }
```

### m_dt {#m-m-dt}

```csharp
public float m_dt { get; }
```

### m_ID {#m-m-id}

```csharp
public int m_ID { get; protected set; }
```

### m_MotionDataset {#m-m-motiondataset}

```csharp
public string m_MotionDataset { get; }
```

### m_NeedsCalibration {#m-m-needscalibration}

```csharp
protected abstract bool m_NeedsCalibration { get; }
```

### m_NumJoints {#m-m-numjoints}

```csharp
protected int m_NumJoints { get; }
```

### m_PolicyPath {#m-m-policypath}

```csharp
public string m_PolicyPath { get; }
```

### StateSize {#m-statesize}

```csharp
public int StateSize { get; protected set; }
```

### StateSpec {#m-statespec}

```csharp
protected abstract List<StateConfig> StateSpec { get; }
```

## Methods

### Action() {#m-action}

```csharp
public void Action()
```

### ApplyAdditionalJointOffsets(float[]) {#m-applyadditionaljointoffsets}

```csharp
public void ApplyAdditionalJointOffsets(float[] extraOffsets)
```

### BindBatchBuffers(float[], int, float[], int, float[], int) {#m-bindbatchbuffers}

```csharp
public void BindBatchBuffers(float[] stateBuffer, int stateOffset, float[] actionBuffer, int actionOffset, float[] ctrlBuffer, int ctrlOffset)
```

### CacheJointHandles(List<JointConfig>) {#m-cachejointhandles}

```csharp
protected void CacheJointHandles(List<JointConfig> specs)
```

### CaptureInitialPose() {#m-captureinitialpose}

```csharp
public void CaptureInitialPose()
```

### ClearBuffers() {#m-clearbuffers}

```csharp
public void ClearBuffers()
```

### GetActuatorIndices() {#m-getactuatorindices}

```csharp
public int[] GetActuatorIndices()
```

### GetAxisMapping() {#m-getaxismapping}

```csharp
public virtual Dictionary<string, (string axis, bool invert)>? GetAxisMapping()
```

### GetDefaultJointPos() {#m-getdefaultjointpos}

```csharp
public List<float> GetDefaultJointPos()
```

### GetJointNames() {#m-getjointnames}

```csharp
public List<string> GetJointNames()
```

### GetJointQposAdrs() {#m-getjointqposadrs}

```csharp
public uint[] GetJointQposAdrs()
```

### GetJointQvelAdrs() {#m-getjointqveladrs}

```csharp
public uint[] GetJointQvelAdrs()
```

### GetMotionAnchorPoseMj(Vector3, Quaternion) {#m-getmotionanchorposemj}

```csharp
public void GetMotionAnchorPoseMj(out Vector3 posMj, out Quaternion quatMj)
```

Get motion anchor pose in MuJoCo space (for observation). When RefPoseExtractor is null, returns Zero/Identity.

### GetMotionJointPos() {#m-getmotionjointpos}

```csharp
public float[] GetMotionJointPos()
```

### GetStateSpec() {#m-getstatespec}

```csharp
public List<StateConfig> GetStateSpec()
```

### OnAction() {#m-onaction}

```csharp
public abstract void OnAction()
```

### OnReset() {#m-onreset}

```csharp
public abstract void OnReset()
```

### OnSetup() {#m-onsetup}

```csharp
public abstract void OnSetup()
```

### OnState() {#m-onstate}

```csharp
public abstract void OnState()
```

### SetCalibrationOffsets(float[]) {#m-setcalibrationoffsets}

```csharp
public void SetCalibrationOffsets(float[] offsets)
```

### SetMujocoScene(MujocoSceneComponent) {#m-setmujocoscene}

```csharp
public void SetMujocoScene(MujocoSceneComponent mujocoScene)
```

### SetRobotManager(RobotManager) {#m-setrobotmanager}

```csharp
public void SetRobotManager(RobotManager robotManager)
```

### Setup(RobotManager, MujocoSceneComponent, int, bool) {#m-setup}

```csharp
public void Setup(RobotManager robotManager, MujocoSceneComponent mujocoScene, int ID, bool isExternalBatch = false)
```

### UpdateCalibration() {#m-updatecalibration}

```csharp
public void UpdateCalibration()
```


---
<small>Source: `Hazel/Learn/RobotAgent.cs`</small>
