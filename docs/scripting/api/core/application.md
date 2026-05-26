# Application

`static class` · namespace `Hazel`

```csharp
public static class Application
```

## Properties

### AspectRatio {#m-aspectratio}

`static`

```csharp
public static float AspectRatio { get; }
```

### DataDirectoryPath {#m-datadirectorypath}

`static`

```csharp
public static string? DataDirectoryPath { get; }
```

### Height {#m-height}

`static`

```csharp
public static uint Height { get; }
```

### Time {#m-time}

`static`

```csharp
public static float Time { get; }
```

### Width {#m-width}

`static`

```csharp
public static uint Width { get; }
```

## Methods

### GetSetting(string, string) {#m-getsetting}

`static`

```csharp
public static string? GetSetting(string name, string defaultValue = "")
```

### GetSettingFloat(string, float) {#m-getsettingfloat}

`static`

```csharp
public static float GetSettingFloat(string name, float defaultValue = 0.0f)
```

### GetSettingInt(string, int) {#m-getsettingint}

`static`

```csharp
public static int GetSettingInt(string name, int defaultValue = 0)
```

### Quit() {#m-quit}

`static`

```csharp
public static void Quit()
```

### RequestEnterPlayMode() {#m-requestenterplaymode}

`static`

```csharp
public static void RequestEnterPlayMode()
```

### RequestExitPlayMode() {#m-requestexitplaymode}

`static`

```csharp
public static void RequestExitPlayMode()
```

### SetSettingFloat(string, float) {#m-setsettingfloat}

`static`

```csharp
public static void SetSettingFloat(string name, float value)
```


---
<small>Source: `Hazel/Core/Application.cs`</small>
