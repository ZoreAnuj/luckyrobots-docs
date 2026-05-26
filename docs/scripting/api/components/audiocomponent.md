# AudioComponent

`class` · namespace `Hazel` · **Component**

Inherits [`Component`](../scene/component.md)

```csharp
public class AudioComponent : Component
```

## Properties

### PitchMultiplier {#m-pitchmultiplier}

```csharp
public float PitchMultiplier { get; set; }
```

### VolumeMultiplier {#m-volumemultiplier}

```csharp
public float VolumeMultiplier { get; set; }
```

## Methods

### IsPlaying() {#m-isplaying}

```csharp
public bool IsPlaying()
```

### Pause() {#m-pause}

```csharp
public bool Pause()
```

### Play(float) {#m-play}

```csharp
public bool Play(float startTime = 0.0f)
```

### Resume() {#m-resume}

```csharp
public bool Resume()
```

### SetEvent(AudioCommandID) {#m-setevent}

```csharp
public void SetEvent(AudioCommandID eventID)
```

### SetEvent(string) {#m-setevent-2}

```csharp
public void SetEvent(string eventName)
```

### Stop() {#m-stop}

```csharp
public bool Stop()
```


---
<small>Source: `Hazel/Scene/Components.cs`</small>
