# AnimationSequenceGeneric<T>

`abstract class` · namespace `Hazel`

```csharp
public abstract class AnimationSequenceGeneric<T>
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public List&lt;Keyframe&lt;T&gt;&gt; Keyframes</code> |  |

## Methods

### AddKeyframe(T, float, KeyframeType) {#m-addkeyframe}

```csharp
public void AddKeyframe(T data, float time, KeyframeType type = KeyframeType.Linear)
```

### GetValue(float) {#m-getvalue}

```csharp
public T GetValue(float time)
```

### Interpolate(KeyframeType, T, T, float) {#m-interpolate}

```csharp
protected abstract T Interpolate(KeyframeType type, T a, T b, float time)
```


---
<small>Source: `Hazel/Animation/AnimationSequenceGeneric.cs`</small>
