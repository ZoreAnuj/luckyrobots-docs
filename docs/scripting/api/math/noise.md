# Noise

`class` · namespace `Hazel`

```csharp
public class Noise
```

## Constructors

### Noise(int) {#m-noise}

```csharp
public Noise(int seed = 8)
```

## Properties

### Frequency {#m-frequency}

```csharp
public float Frequency { get; set; }
```

### Gain {#m-gain}

```csharp
public float Gain { get; set; }
```

### Lacunarity {#m-lacunarity}

```csharp
public float Lacunarity { get; set; }
```

### Octaves {#m-octaves}

```csharp
public int Octaves { get; set; }
```

### StaticSeed {#m-staticseed}

`static`

```csharp
public static int StaticSeed { set; }
```

## Methods

### Get(float, float) {#m-get}

```csharp
public float Get(float x, float y)
```

### Perlin(float, float) {#m-perlin}

`static`

```csharp
public static float Perlin(float x, float y)
```


---
<small>Source: `Hazel/Math/Noise.cs`</small>
