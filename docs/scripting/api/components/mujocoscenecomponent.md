# MujocoSceneComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class MujocoSceneComponent : Component
```

## Properties

### IsControlledManually {#m-iscontrolledmanually}

```csharp
public bool IsControlledManually { get; set; }
```

### ModelNbody {#m-modelnbody}

```csharp
public uint ModelNbody { get; }
```

### ModelNgeom {#m-modelngeom}

```csharp
public uint ModelNgeom { get; }
```

### ModelNjnt {#m-modelnjnt}

```csharp
public uint ModelNjnt { get; }
```

### ModelNq {#m-modelnq}

```csharp
public uint ModelNq { get; }
```

### ModelNsite {#m-modelnsite}

```csharp
public uint ModelNsite { get; }
```

### ModelNu {#m-modelnu}

```csharp
public uint ModelNu { get; }
```

### ModelNv {#m-modelnv}

```csharp
public uint ModelNv { get; }
```

### MujocoTime {#m-mujocotime}

```csharp
public double MujocoTime { get; }
```

## Methods

### GetActuator(string) {#m-getactuator}

```csharp
public MujocoActuator? GetActuator(string name)
```

### GetActuatorCtrlRange(uint, float, float) {#m-getactuatorctrlrange}

```csharp
public bool GetActuatorCtrlRange(uint actuatorIndex, out float lo, out float hi)
```

### GetActuatorGainPrms(uint, float, float) {#m-getactuatorgainprms}

```csharp
public bool GetActuatorGainPrms(uint actuatorIndex, out float gainPrm0, out float biasPrm0)
```

### GetActuatorID(string) {#m-getactuatorid}

```csharp
public int GetActuatorID(string name)
```

### GetActuatorIndex(string) {#m-getactuatorindex}

```csharp
public uint GetActuatorIndex(string actuatorName)
```

### GetActuatorNameByIndex(uint) {#m-getactuatornamebyindex}

```csharp
public string GetActuatorNameByIndex(uint actuatorIndex)
```

### GetActuatorTrnJointID(uint) {#m-getactuatortrnjointid}

```csharp
public int GetActuatorTrnJointID(uint actuatorIndex)
```

### GetAngularVelocity(uint, bool) {#m-getangularvelocity}

```csharp
public Vector3 GetAngularVelocity(uint bodyId, bool hazelSpace = true)
```

### GetAxisHzToMj() {#m-getaxishztomj}

```csharp
public Quaternion GetAxisHzToMj()
```

### GetBodyFreeJointIndex(uint) {#m-getbodyfreejointindex}

```csharp
public uint GetBodyFreeJointIndex(uint bodyIndex)
```

### GetBodyID(string) {#m-getbodyid}

```csharp
public uint GetBodyID(string name)
```

### GetBodyMass(uint) {#m-getbodymass}

```csharp
public float GetBodyMass(uint bodyIndex)
```

### GetBodyNameByIndex(uint) {#m-getbodynamebyindex}

```csharp
public string GetBodyNameByIndex(uint bodyIndex)
```

### GetBodyParentID(uint) {#m-getbodyparentid}

```csharp
public int GetBodyParentID(uint bodyIndex)
```

### GetBodyPoseMj(uint, Vector3, Quaternion, Vector3, Vector3) {#m-getbodyposemj}

```csharp
public bool GetBodyPoseMj(uint bodyIndex, out Vector3 position, out Quaternion orientation, out Vector3 linearVel, out Vector3 angularVel)
```

### GetBodyRootID(uint) {#m-getbodyrootid}

```csharp
public int GetBodyRootID(uint bodyIndex)
```

### GetDefaultFreeJointPosition(uint) {#m-getdefaultfreejointposition}

```csharp
public Vector3 GetDefaultFreeJointPosition(uint freeJointId)
```

### GetDefaultFreeJointQuaternion(uint) {#m-getdefaultfreejointquaternion}

```csharp
public Quaternion GetDefaultFreeJointQuaternion(uint freeJointId)
```

### GetFreeJointID() {#m-getfreejointid}

```csharp
public uint GetFreeJointID()
```

### GetFullCtrl(float[]) {#m-getfullctrl}

```csharp
public int GetFullCtrl(float[] outBuffer)
```

### GetFullQpos(float[]) {#m-getfullqpos}

```csharp
public int GetFullQpos(float[] outBuffer)
```

### GetFullQvel(float[]) {#m-getfullqvel}

```csharp
public int GetFullQvel(float[] outBuffer)
```

### GetGeomContactForce(uint) {#m-getgeomcontactforce}

```csharp
public float GetGeomContactForce(uint geomId)
```

### GetGeomDescriptor(uint, int, int, int, int, int, Vector3) {#m-getgeomdescriptor}

```csharp
public bool GetGeomDescriptor(uint geomIndex, out int parentBodyId, out int geomType, out int group, out int contype, out int conaffinity, out Vector3 size)
```

### GetGeomID(string) {#m-getgeomid}

```csharp
public uint GetGeomID(string name)
```

### GetGeomNameByIndex(uint) {#m-getgeomnamebyindex}

```csharp
public string GetGeomNameByIndex(uint geomIndex)
```

### GetGeomPoseMj(uint, Vector3, Quaternion) {#m-getgeomposemj}

```csharp
public bool GetGeomPoseMj(uint geomIndex, out Vector3 position, out Quaternion orientation)
```

### GetJointID(string) {#m-getjointid}

```csharp
public uint GetJointID(string jointName)
```

### GetJointNameByIndex(uint) {#m-getjointnamebyindex}

```csharp
public string GetJointNameByIndex(uint jointIndex)
```

### GetJointPosition(uint) {#m-getjointposition}

```csharp
public float GetJointPosition(uint qposAdr)
```

### GetJointPositions(uint[], float[]) {#m-getjointpositions}

```csharp
public void GetJointPositions(uint[] qposAdrs, float[] outPositions)
```

### GetJointQposAdr(uint) {#m-getjointqposadr}

```csharp
public uint GetJointQposAdr(uint jointId)
```

### GetJointQvelAdr(uint) {#m-getjointqveladr}

```csharp
public uint GetJointQvelAdr(uint jointId)
```

### GetJointRange(uint, float, float) {#m-getjointrange}

```csharp
public bool GetJointRange(uint jointIndex, out float lo, out float hi)
```

### GetJointType(uint) {#m-getjointtype}

```csharp
public int GetJointType(uint jointIndex)
```

### GetJointVelocities(uint[], float[]) {#m-getjointvelocities}

```csharp
public void GetJointVelocities(uint[] qvelAdrs, float[] outVelocities)
```

### GetJointVelocity(uint) {#m-getjointvelocity}

```csharp
public float GetJointVelocity(uint qvelAdr)
```

### GetOrientation(uint, bool) {#m-getorientation}

```csharp
public Quaternion GetOrientation(uint bodyId, bool hazelSpace = true)
```

### GetPosition(uint, bool) {#m-getposition}

```csharp
public Vector3 GetPosition(uint bodyID, bool hazelSpace = true)
```

### GetSceneTranslation() {#m-getscenetranslation}

```csharp
public Vector3 GetSceneTranslation()
```

### GetSiteDescriptor(uint, int, int, Vector3) {#m-getsitedescriptor}

```csharp
public bool GetSiteDescriptor(uint siteIndex, out int parentBodyId, out int siteType, out Vector3 size)
```

### GetSiteID(string) {#m-getsiteid}

```csharp
public uint GetSiteID(string name)
```

### GetSiteLinearVelocityMj(uint) {#m-getsitelinearvelocitymj}

```csharp
public Vector3 GetSiteLinearVelocityMj(uint siteId)
```

### GetSiteNameByIndex(uint) {#m-getsitenamebyindex}

```csharp
public string GetSiteNameByIndex(uint siteIndex)
```

### GetSitePoseMj(uint, Vector3, Quaternion, Vector3) {#m-getsiteposemj}

```csharp
public bool GetSitePoseMj(uint siteIndex, out Vector3 position, out Quaternion orientation, out Vector3 linearVel)
```

### GetSitePositionMj(uint) {#m-getsitepositionmj}

```csharp
public Vector3 GetSitePositionMj(uint siteId)
```

### GetVelocity(uint, bool) {#m-getvelocity}

```csharp
public Vector3 GetVelocity(uint bodyId, bool hazelSpace = true)
```

### HasNonFootGroundContact(uint[]) {#m-hasnonfootgroundcontact}

```csharp
public bool HasNonFootGroundContact(uint[] footGeomIds)
```

### IsActuatorClaimedByPolicy(uint) {#m-isactuatorclaimedbypolicy}

```csharp
public bool IsActuatorClaimedByPolicy(uint actuatorIndex)
```

### IsGeomInContact(uint) {#m-isgeomincontact}

```csharp
public bool IsGeomInContact(uint geomId)
```

### ReseedPolicyPDTargets() {#m-reseedpolicypdtargets}

```csharp
public void ReseedPolicyPDTargets()
```

### ResetToInitialState(bool) {#m-resettoinitialstate}

```csharp
public void ResetToInitialState(bool preserveTime = false)
```

### SetActuatorGainPrms(uint, float, float) {#m-setactuatorgainprms}

```csharp
public bool SetActuatorGainPrms(uint actuatorIndex, float gainPrm0, float biasPrm0)
```

### SetControlledManually(bool) {#m-setcontrolledmanually}

```csharp
public void SetControlledManually(bool value)
```

### SetCtrlByIndex(uint, float) {#m-setctrlbyindex}

```csharp
public bool SetCtrlByIndex(uint actuatorIndex, float value)
```

### SetFullCtrl(float[]) {#m-setfullctrl}

```csharp
public int SetFullCtrl(float[] values)
```

### SetFullQpos(float[]) {#m-setfullqpos}

```csharp
public int SetFullQpos(float[] values)
```

### SetQfrcApplied(uint, float) {#m-setqfrcapplied}

```csharp
public bool SetQfrcApplied(uint dofIndex, float value)
```

### SetQposByIndex(uint, float) {#m-setqposbyindex}

```csharp
public bool SetQposByIndex(uint qposIndex, float value)
```

### UnweldBody(uint) {#m-unweldbody}

```csharp
public void UnweldBody(uint bodyID)
```

### WeldBody(uint, Vector3, Quaternion, bool) {#m-weldbody}

```csharp
public void WeldBody(uint bodyID, Vector3 positionMj, Quaternion orientationMj, bool persistent = true)
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
