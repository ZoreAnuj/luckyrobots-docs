# AudioEntity

`class` · namespace `Hazel`

`Sound2D` is just a basic helper class to easily spawn and control simple 2D sound objects.

Inherits [`Entity`](../scene/entity.md)

```csharp
public class AudioEntity : Entity
```

## Constructors

### AudioEntity() {#m-audioentity}

```csharp
internal protected AudioEntity()
```

### AudioEntity(ulong) {#m-audioentity-2}

```csharp
internal protected AudioEntity(ulong entityID)
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

### Stop() {#m-stop}

```csharp
public bool Stop()
```


---
<small>Source: `Hazel/Audio/Audio.cs`</small>
