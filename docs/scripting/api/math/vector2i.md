# Vector2i

`struct` · namespace `Hazel`

```csharp
public struct Vector2i
```

## Constructors

### Vector2i(int) {#m-vector2i}

```csharp
public Vector2i(int scalar)
```

### Vector2i(int, int) {#m-vector2i-2}

```csharp
public Vector2i(int x, int y)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public static Vector2i Down</code> |  |
| <code>public static Vector2i Left</code> |  |
| <code>public static Vector2i One</code> |  |
| <code>public static Vector2i Right</code> |  |
| <code>public static Vector2i Up</code> |  |
| <code>public int X</code> |  |
| <code>public int Y</code> |  |
| <code>public static Vector2i Zero</code> |  |

## Methods

### Apply(Func<int, int>) {#m-apply}

```csharp
public void Apply(Func<int, int> func)
```

Allows you to pass in a delegate that takes in a double to process the vector per axis. i.e. (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'int' method to act as a scalar to process X and Y |

### Distance(Vector2i) {#m-distance}

```csharp
public float Distance(Vector2i other)
```

### Distance(Vector2i, Vector2i) {#m-distance-2}

`static`

```csharp
public static float Distance(Vector2i p1, Vector2i p2)
```

### Equals(Vector2i) {#m-equals-2}

```csharp
public bool Equals(Vector2i right)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### Lerp(Vector2i, Vector2i, int) {#m-lerp}

`static`

```csharp
public static Vector2i Lerp(Vector2i p1, Vector2i p2, int t)
```

### New(Func<int, int>) {#m-new}

```csharp
public Vector2i New(Func<int, int> func)
```

Allows you to pass in a delegate that takes in and returns a new Vector processed per axis. i.e. (v =&gt; v * 3)

**Parameters**

| Name | Description |
|------|-------------|
| `func` | Delegate 'int' method to act as a scalar to process X and Y |

### ToString() {#m-tostring}

```csharp
public override string ToString()
```

## Operators

### operator !=(Vector2i, Vector2i) {#m-operator-13}

`static`

```csharp
public static bool operator !=(Vector2i left, Vector2i right)
```

### operator *(Vector2i, Vector2i) {#m-operator-3}

`static`

```csharp
public static Vector2i operator *(Vector2i left, Vector2i right)
```

### operator *(Vector2i, int) {#m-operator}

`static`

```csharp
public static Vector2i operator *(Vector2i left, int scalar)
```

### operator *(int, Vector2i) {#m-operator-2}

`static`

```csharp
public static Vector2i operator *(int scalar, Vector2i right)
```

### operator +(Vector2i, Vector2i) {#m-operator-7}

`static`

```csharp
public static Vector2i operator +(Vector2i left, Vector2i right)
```

### operator +(Vector2i, int) {#m-operator-8}

`static`

```csharp
public static Vector2i operator +(Vector2i left, int right)
```

### operator -(Vector2i, Vector2i) {#m-operator-9}

`static`

```csharp
public static Vector2i operator -(Vector2i left, Vector2i right)
```

### operator -(Vector2i, int) {#m-operator-10}

`static`

```csharp
public static Vector2i operator -(Vector2i left, int right)
```

### operator -(Vector2i) {#m-operator-11}

`static`

```csharp
public static Vector2i operator -(Vector2i vector)
```

### operator /(Vector2i, Vector2i) {#m-operator-4}

`static`

```csharp
public static Vector2i operator /(Vector2i left, Vector2i right)
```

### operator /(Vector2i, int) {#m-operator-5}

`static`

```csharp
public static Vector2i operator /(Vector2i left, int scalar)
```

### operator /(int, Vector2i) {#m-operator-6}

`static`

```csharp
public static Vector2i operator /(int scalar, Vector2i right)
```

### operator ==(Vector2i, Vector2i) {#m-operator-12}

`static`

```csharp
public static bool operator ==(Vector2i left, Vector2i right)
```


---
<small>Source: `Hazel/Math/Vector2i.cs`</small>
