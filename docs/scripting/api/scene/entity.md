# Entity

`class` · namespace `Hazel`

Implements `IEquatable<Entity>`

```csharp
public class Entity : IEquatable<Entity>
```

## Constructors

### Entity() {#m-entity}

```csharp
protected Entity()
```

## Fields

| Field | Description |
|-------|-------------|
| <code>public readonly ulong ID</code> |  |
| <code>public static readonly Entity Invalid</code> |  |

## Properties

### Children {#m-children}

```csharp
public Entity[] Children { get; }
```

### Parent {#m-parent}

```csharp
public Entity? Parent { get; set; }
```

### Rotation {#m-rotation}

```csharp
public Vector3 Rotation { get; set; }
```

### RotationQuat {#m-rotationquat}

```csharp
public Quaternion RotationQuat { get; set; }
```

### Scale {#m-scale}

```csharp
public Vector3 Scale { get; set; }
```

### Tag {#m-tag}

```csharp
public string Tag { get; }
```

### Transform {#m-transform}

```csharp
public TransformComponent Transform { get; }
```

### Translation {#m-translation}

```csharp
public Vector3 Translation { get; set; }
```

## Methods

### As<T>() {#m-as}

```csharp
public T? As<T>() where T : Entity
```

Returns the script instance as type T if this entity is of the given type, otherwise null

**Type parameters**

| Name | Description |
|------|-------------|
| `T` |  |

### CreateComponent<T>() {#m-createcomponent}

```csharp
public T? CreateComponent<T>() where T : Component, new()
```

### Destroy() {#m-destroy}

```csharp
public void Destroy()
```

### Destroy(Entity) {#m-destroy-2}

```csharp
public void Destroy(Entity other)
```

### DestroyAllChildren() {#m-destroyallchildren}

```csharp
public void DestroyAllChildren()
```

### Equals(Entity?) {#m-equals-2}

```csharp
public bool Equals(Entity? other)
```

### Equals(object?) {#m-equals}

```csharp
public override bool Equals(object? obj)
```

### FindChildEntityByTag(string) {#m-findchildentitybytag}

```csharp
public Entity? FindChildEntityByTag(string tag)
```

### FindEntityByID(ulong) {#m-findentitybyid}

```csharp
public Entity? FindEntityByID(ulong entityID)
```

### FindEntityByTag(string) {#m-findentitybytag}

```csharp
public Entity? FindEntityByTag(string tag)
```

### GetComponent<T>() {#m-getcomponent}

```csharp
public T? GetComponent<T>() where T : Component, new()
```

### GetHashCode() {#m-gethashcode}

```csharp
public override int GetHashCode()
```

### HasComponent(Type) {#m-hascomponent-2}

```csharp
public bool HasComponent(Type type)
```

### HasComponent<T>() {#m-hascomponent}

```csharp
public bool HasComponent<T>() where T : Component
```

### Instantiate(Prefab) {#m-instantiate}

```csharp
public Entity? Instantiate(Prefab prefab)
```

### Instantiate(Prefab, Transform) {#m-instantiate-5}

```csharp
public Entity? Instantiate(Prefab prefab, Transform transform)
```

### Instantiate(Prefab, Vector3) {#m-instantiate-2}

```csharp
public Entity? Instantiate(Prefab prefab, Vector3 translation)
```

### Instantiate(Prefab, Vector3, Vector3) {#m-instantiate-3}

```csharp
public Entity? Instantiate(Prefab prefab, Vector3 translation, Vector3 rotation)
```

### Instantiate(Prefab, Vector3, Vector3, Vector3) {#m-instantiate-4}

```csharp
public Entity? Instantiate(Prefab prefab, Vector3 translation, Vector3 rotation, Vector3 scale)
```

### InstantiateChild(Prefab) {#m-instantiatechild}

```csharp
public Entity? InstantiateChild(Prefab prefab)
```

### InstantiateChild(Prefab, Transform) {#m-instantiatechild-5}

```csharp
public Entity? InstantiateChild(Prefab prefab, Transform transform)
```

### InstantiateChild(Prefab, Vector3) {#m-instantiatechild-2}

```csharp
public Entity? InstantiateChild(Prefab prefab, Vector3 translation)
```

### InstantiateChild(Prefab, Vector3, Vector3) {#m-instantiatechild-3}

```csharp
public Entity? InstantiateChild(Prefab prefab, Vector3 translation, Vector3 rotation)
```

### InstantiateChild(Prefab, Vector3, Vector3, Vector3) {#m-instantiatechild-4}

```csharp
public Entity? InstantiateChild(Prefab prefab, Vector3 translation, Vector3 rotation, Vector3 scale)
```

### Is<T>() {#m-is}

```csharp
public bool Is<T>() where T : Entity
```

Checks if this entity is a script entity of type T

**Type parameters**

| Name | Description |
|------|-------------|
| `T` |  |

**Returns** — True if this entity is a script entity of type T

### OnCreate() {#m-oncreate}

```csharp
protected virtual void OnCreate()
```

### OnDestroy() {#m-ondestroy}

```csharp
protected virtual void OnDestroy()
```

### OnFreePostUpdate(float) {#m-onfreepostupdate}

```csharp
protected virtual void OnFreePostUpdate(float ts)
```

### OnFreePreUpdate(float) {#m-onfreepreupdate}

```csharp
protected virtual void OnFreePreUpdate(float ts)
```

### OnLateUpdate(float) {#m-onlateupdate}

```csharp
protected virtual void OnLateUpdate(float ts)
```

### OnPhysicsUpdate(float) {#m-onphysicsupdate}

```csharp
protected virtual void OnPhysicsUpdate(float ts)
```

### OnPostUpdate(float) {#m-onpostupdate}

```csharp
protected virtual void OnPostUpdate(float ts)
```

### OnPreUpdate(float) {#m-onpreupdate}

```csharp
protected virtual void OnPreUpdate(float ts)
```

### OnUpdate(float) {#m-onupdate}

```csharp
protected virtual void OnUpdate(float ts)
```

### RemoveComponent<T>() {#m-removecomponent}

```csharp
public bool RemoveComponent<T>() where T : Component
```

## Operators

### implicit operator bool {#m-implicit-operator-bool}

`static`

```csharp
public static implicit operator bool(Entity entity)
```

### operator !=(Entity?, Entity?) {#m-operator-2}

`static`

```csharp
public static bool operator !=(Entity? entityA, Entity? entityB)
```

### operator ==(Entity?, Entity?) {#m-operator}

`static`

```csharp
public static bool operator ==(Entity? entityA, Entity? entityB)
```

## Events

### AnimationEvent {#m-animationevent}

```csharp
protected event Action<Identifier>? AnimationEvent
```

### Collision2DBeginEvent {#m-collision2dbeginevent}

```csharp
public event Action<Entity>? Collision2DBeginEvent
```

### Collision2DEndEvent {#m-collision2dendevent}

```csharp
public event Action<Entity>? Collision2DEndEvent
```

### CollisionBeginEvent {#m-collisionbeginevent}

```csharp
public event Action<Entity>? CollisionBeginEvent
```

### CollisionEndEvent {#m-collisionendevent}

```csharp
public event Action<Entity>? CollisionEndEvent
```

### DestroyedEvent {#m-destroyedevent}

```csharp
public event Action<Entity>? DestroyedEvent
```

### JointBreakEvent {#m-jointbreakevent}

```csharp
public event Action<Vector3, Vector3>? JointBreakEvent
```

### TriggerBeginEvent {#m-triggerbeginevent}

```csharp
public event Action<Entity>? TriggerBeginEvent
```

### TriggerEndEvent {#m-triggerendevent}

```csharp
public event Action<Entity>? TriggerEndEvent
```


---
<small>Source: `Hazel/Scene/Entity.cs`</small>
