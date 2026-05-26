# ComponentDescriptor

`struct` · namespace `Hazel`

Describes one available MDP component for the capability manifest. Returned by each component's Descriptor property.

```csharp
public struct ComponentDescriptor
```

## Constructors

### ComponentDescriptor(string, string, string) {#m-componentdescriptor}

```csharp
public ComponentDescriptor(string name, string description, string category)
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public string Category</code> |  |
| <code>public string Description</code> |  |
| <code>public string Name</code> |  |
| <code>public int[] OutputShape</code> |  |
| <code>public Dictionary&lt;string, ParamDescriptor&gt; ParamsSchema</code> |  |
| <code>public string[] Requires</code> |  |
| <code>public string[] RobotTypes</code> |  |


---
<small>Source: `Hazel/Learn/MdpComponent.cs`</small>
