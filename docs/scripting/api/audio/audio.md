# Audio

`static class` · namespace `Hazel`

```csharp
public static class Audio
```

## Methods

### CreateAudioEntity(AudioCommandID, Transform, float, float) {#m-createaudioentity}

`static`

```csharp
public static AudioEntity CreateAudioEntity(AudioCommandID triggerEventID, Transform location, float volume = 1.0f, float pitch = 1.0f)
```

### CreateAudioEntity(string, Transform, float, float) {#m-createaudioentity-2}

`static`

```csharp
public static AudioEntity CreateAudioEntity(string triggerEventName, Transform location, float volume = 1.0f, float pitch = 1.0f)
```

### PauseEventID(uint) {#m-pauseeventid}

`static`

```csharp
public static bool PauseEventID(uint eventID)
```

Pause playing audio sources associated to active event. Returns true - if event has playing audio soruces to pause.

### PostEvent(AudioCommandID, AudioComponent) {#m-postevent-3}

`static`

```csharp
public static uint PostEvent(AudioCommandID id, ref AudioComponent audioComponent)
```

Post event on AudioComponent. Returns active Event ID

### PostEvent(AudioCommandID, ulong) {#m-postevent}

`static`

```csharp
public static uint PostEvent(AudioCommandID id, ulong objectID)
```

Post event on an object. Returns active Event ID

### PostEvent(string, ulong) {#m-postevent-2}

`static`

```csharp
public static uint PostEvent(string eventName, ulong objectID)
```

Post event by name. Prefer overload that takes CommandID for speed and initialize CommandIDs once. This creates new CommandID object from eventName, which has to hash the string.

### PostEventAtLocation(AudioCommandID, Vector3, Vector3) {#m-posteventatlocation}

`static`

```csharp
public static uint PostEventAtLocation(AudioCommandID id, Vector3 location, Vector3 rotation)
```

Post event on at location creating temporary `Audio Object`. Returns active Event ID

### PreloadEventSources(AudioCommandID) {#m-preloadeventsources}

`static`

```csharp
public static void PreloadEventSources(AudioCommandID eventID)
```

### ResumeEventID(uint) {#m-resumeeventid}

`static`

```csharp
public static bool ResumeEventID(uint eventID)
```

Resume playing audio sources associated to active event. Returns true - if event has playing audio soruces to resume.

### SetHighPassFilterValue(AudioComponent, float) {#m-sethighpassfiltervalue}

`static`

```csharp
public static void SetHighPassFilterValue(ref AudioComponent audioComponent, float value)
```

### SetHighPassFilterValue(uint, float) {#m-sethighpassfiltervalue-2}

`static`

```csharp
public static void SetHighPassFilterValue(uint eventID, float value)
```

### SetHighPassFilterValueObj(ulong, float) {#m-sethighpassfiltervalueobj}

`static`

```csharp
public static void SetHighPassFilterValueObj(ulong objectID, float value)
```

### SetLowPassFilterValue(AudioComponent, float) {#m-setlowpassfiltervalue}

`static`

```csharp
public static void SetLowPassFilterValue(ref AudioComponent audioComponent, float value)
```

### SetLowPassFilterValue(uint, float) {#m-setlowpassfiltervalue-2}

`static`

```csharp
public static void SetLowPassFilterValue(uint eventID, float value)
```

### SetLowPassFilterValueObj(ulong, float) {#m-setlowpassfiltervalueobj}

`static`

```csharp
public static void SetLowPassFilterValueObj(ulong objectID, float value)
```

### SetParameter(ParameterID, uint, bool) {#m-setparameter-6}

`static`

```csharp
public static void SetParameter(ParameterID id, uint eventID, bool value)
```

### SetParameter(ParameterID, uint, float) {#m-setparameter-4}

`static`

```csharp
public static void SetParameter(ParameterID id, uint eventID, float value)
```

### SetParameter(ParameterID, uint, int) {#m-setparameter-5}

`static`

```csharp
public static void SetParameter(ParameterID id, uint eventID, int value)
```

### SetParameter(ParameterID, ulong, bool) {#m-setparameter-3}

`static`

```csharp
public static void SetParameter(ParameterID id, ulong objectID, bool value)
```

### SetParameter(ParameterID, ulong, float) {#m-setparameter}

`static`

```csharp
public static void SetParameter(ParameterID id, ulong objectID, float value)
```

### SetParameter(ParameterID, ulong, int) {#m-setparameter-2}

`static`

```csharp
public static void SetParameter(ParameterID id, ulong objectID, int value)
```

### SetParameterForAC(ParameterID, AudioComponent, bool) {#m-setparameterforac-3}

`static`

```csharp
public static void SetParameterForAC(ParameterID id, ref AudioComponent audioComponent, bool value)
```

### SetParameterForAC(ParameterID, AudioComponent, float) {#m-setparameterforac}

`static`

```csharp
public static void SetParameterForAC(ParameterID id, ref AudioComponent audioComponent, float value)
```

### SetParameterForAC(ParameterID, AudioComponent, int) {#m-setparameterforac-2}

`static`

```csharp
public static void SetParameterForAC(ParameterID id, ref AudioComponent audioComponent, int value)
```

### StopEventID(uint) {#m-stopeventid}

`static`

```csharp
public static bool StopEventID(uint eventID)
```

Stop playing audio sources associated to active event. Returns true - if event has playing audio soruces to stop.

### UnloadEventSources(AudioCommandID) {#m-unloadeventsources}

`static`

```csharp
public static void UnloadEventSources(AudioCommandID eventID)
```


---
<small>Source: `Hazel/Audio/Audio.cs`</small>
