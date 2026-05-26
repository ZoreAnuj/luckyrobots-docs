# Http

`static class` · namespace `Hazel`

HTTP client for making web requests to JSON APIs and other services.

```csharp
public static class Http
```

## Methods

### Get(string) {#m-get}

`static`

```csharp
public static string Get(string url)
```

Performs an HTTP GET request and returns the response body as a string.

**Parameters**

| Name | Description |
|------|-------------|
| `url` | The URL to request |

**Returns** — Response body as string, or empty string on error

### Get(string, string) {#m-get-2}

`static`

```csharp
public static string Get(string url, string headers)
```

Performs an HTTP GET request with custom headers.

**Parameters**

| Name | Description |
|------|-------------|
| `url` | The URL to request |
| `headers` | Newline-separated headers (e.g. "Authorization: Bearer token\nX-Custom: value") |

**Returns** — Response body as string, or empty string on error

### Post(string, string) {#m-post}

`static`

```csharp
public static string Post(string url, string jsonBody)
```

Performs an HTTP POST request with a JSON body. Content-Type: application/json is automatically set.

**Parameters**

| Name | Description |
|------|-------------|
| `url` | The URL to request |
| `jsonBody` | The JSON body to send |

**Returns** — Response body as string, or empty string on error

### Post(string, string, string) {#m-post-2}

`static`

```csharp
public static string Post(string url, string jsonBody, string headers)
```

Performs an HTTP POST request with a JSON body and custom headers. Content-Type: application/json is automatically set.

**Parameters**

| Name | Description |
|------|-------------|
| `url` | The URL to request |
| `jsonBody` | The JSON body to send |
| `headers` | Newline-separated headers (e.g. "Authorization: Bearer token\nX-Custom: value") |

**Returns** — Response body as string, or empty string on error


---
<small>Source: `Hazel/Core/Http.cs`</small>
