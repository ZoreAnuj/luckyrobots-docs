# Physics

`static class` · namespace `Hazel`

```csharp
public static class Physics
```

## Properties

### Gravity {#m-gravity}

`static`

```csharp
public static Vector3 Gravity { get; set; }
```

## Methods

### AddRadialImpulse(Vector3, float, float, EFalloffMode, bool) {#m-addradialimpulse}

`static`

```csharp
public static void AddRadialImpulse(Vector3 origin, float radius, float strength, EFalloffMode falloff = EFalloffMode.Constant, bool velocityChange = false)
```

Adds a radial impulse to the scene. Any entity within the radius of the origin will be pushed/pulled according to the strength. You'd use this method for something like an explosion.

**Parameters**

| Name | Description |
|------|-------------|
| `origin` | The origin of the impulse in world space |
| `radius` | The radius of the area affected by the impulse (1 unit = 1 meter radius) |
| `strength` | The strength of the impulse |
| `falloff` | The falloff method used when calculating force over distance |
| `velocityChange` | Setting this value to **true** will make this impulse ignore an actors mass |

### CastRay(RaycastData, SceneQueryHit) {#m-castray}

`static`

```csharp
public static bool CastRay(RaycastData raycastData, out SceneQueryHit hit)
```

### CastShape(ShapeQueryData, SceneQueryHit) {#m-castshape}

`static`

```csharp
public static bool CastShape(ShapeQueryData shapeCastData, out SceneQueryHit outHit)
```

### OverlapShape(ShapeQueryData, SceneQueryHit[]) {#m-overlapshape}

`static`

```csharp
public static int OverlapShape(ShapeQueryData shapeQueryData, out SceneQueryHit[] outHits)
```

Performs an overlap scene query, returning an array of any colliders that the provided shape overlaps. Always allocates.

**Parameters**

| Name | Description |
|------|-------------|
| `shapeQueryData` |  |
| `outHits` |  |


---
<small>Source: `Hazel/Physics/Physics.cs`</small>
