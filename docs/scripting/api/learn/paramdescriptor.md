# ParamDescriptor

`struct` · namespace `Hazel`

Describes an accepted parameter for an MDP component. Used in capability manifests so training frameworks know what parameters a component accepts and their valid ranges.

```csharp
public struct ParamDescriptor
```

## Constructors

### ParamDescriptor(string, string, string, float, float, bool) {#m-paramdescriptor}

```csharp
public ParamDescriptor(string type, string defaultValue, string description, float rangeMin = 0f, float rangeMax = 0f, bool hasRange = false)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public string DefaultValue</code> |  |
| <code>public string Description</code> |  |
| <code>public bool HasRange</code> |  |
| <code>public float RangeMax</code> |  |
| <code>public float RangeMin</code> |  |
| <code>public string Type</code> |  |


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
