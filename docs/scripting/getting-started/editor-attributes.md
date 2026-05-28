# Editor Attributes

Editor attributes decorate a script's fields and methods to control how they appear in
the editor Inspector. They are plain C# attributes from the `Hazel` namespace, applied
directly above a field:

```csharp
[Tooltip("How fast the entity moves.")]
[Slider(0.0f, 10.0f)]
[Units("m/s")]
public float Speed = 5.0f;
```

A field with no attributes still appears in the Inspector with a default widget.
Attributes refine that default by adding a tooltip, a slider, a clamped range, a custom
label, a collapsible group, conditional visibility, or a different widget entirely.

The full list lives in the [Editor Attributes reference](../api/attributes/index.md).
This page introduces each category, shows what the attributes render as, and the common
patterns.

!!! note "The mockups below are approximate"
    The Inspector previews on this page are HTML approximations of the editor's look.
    They convey shape and behaviour rather than every pixel.

## Layout and labelling

### `[Group("Name")]`

Wraps contiguous fields with the same group name in a collapsible section.

```csharp
[Group("Movement")] public float Speed     = 5.0f;
[Group("Movement")] public float Accel     = 2.0f;
[Group("Movement")] public float Friction  = 0.5f;
```

<div class="inspector-mock">
<div class="group">Movement</div>
<div class="field"><span class="label">Speed</span><div class="control"><div class="number">5.00</div></div></div>
<div class="field"><span class="label">Accel</span><div class="control"><div class="number">2.00</div></div></div>
<div class="field"><span class="label">Friction</span><div class="control"><div class="number">0.50</div></div></div>
</div>

### `[Header("Text")]`

Renders a small subsection label above the field.

```csharp
[Header("Linear")]
public float Speed = 5.0f;
```

<div class="inspector-mock">
<div class="header-text">Linear</div>
<div class="field"><span class="label">Speed</span><div class="control"><div class="number">5.00</div></div></div>
</div>

### `[Divider]` and `[Space]`

`[Divider]` inserts a horizontal rule before the field. `[Space(8f)]` inserts vertical
padding.

```csharp
public float Above = 1.0f;
[Divider]
public float Below = 2.0f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Above</span><div class="control"><div class="number">1.00</div></div></div>
<div class="divider"></div>
<div class="field"><span class="label">Below</span><div class="control"><div class="number">2.00</div></div></div>
</div>

### `[DisplayName("Custom")]` and `[Units("m/s")]`

`[DisplayName]` overrides the auto-generated label. `[Units]` appends a unit suffix.

```csharp
[DisplayName("Forward speed")]
[Units("m/s")]
public float ForwardSpeed = 0.5f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Forward speed<span class="units">(m/s)</span></span><div class="control"><div class="number">0.50</div></div></div>
</div>

### `[Tooltip("...")]`

Hover-help text that appears on the field or button after a short delay.

```csharp
[Tooltip("Forward speed in metres per second.")]
public float ForwardSpeed = 0.5f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Forward Speed</span><div class="control"><div class="number">0.50</div></div></div>
<div class="inspector-mock-tooltip">Forward speed in metres per second.</div>
</div>

## Numeric ranges and validation

### `[Slider(min, max)]`

Renders the field as a horizontal slider. Ctrl+Click to type a value, double-click to
reset.

```csharp
[Slider(0.0f, 1.0f)]
public float Friction = 0.5f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Friction</span><div class="control"><div class="slider"><div class="slider-fill" style="width:50%"></div><div class="slider-value">0.50</div></div></div></div>
</div>

### `[ClampValue(min, max)]`, `[Min(v)]`, `[Max(v)]`

`[ClampValue]` is a two-bound clamp on the drag widget. `[Min]` and `[Max]` are
single-bound clamps and compose with each other.

```csharp
[ClampValue(0.0f, 100.0f)]
[Units("kg")]
public float Mass = 10.0f;
```

<div class="inspector-mock">
<div class="field"><span class="label">Mass<span class="units">(kg)</span></span><div class="control"><div class="number">10.00</div></div></div>
</div>

## Widget replacement

### `[ToggleButton("On", "Off")]`

Replaces the default checkbox with a full-width labelled button on `bool` fields.

```csharp
[ToggleButton("Active", "Paused")]
public bool Running = true;
```

<div class="inspector-mock-pair">
<div>
<div class="inspector-mock-state">value: true</div>
<div class="inspector-mock"><div class="field"><span class="label">Running</span><div class="control"><div class="toggle on">Active</div></div></div></div>
</div>
<div>
<div class="inspector-mock-state">value: false</div>
<div class="inspector-mock"><div class="field"><span class="label">Running</span><div class="control"><div class="toggle off">Paused</div></div></div></div>
</div>
</div>

### `[Dropdown("A", "B", "C")]`

Renders an `int` field as a combo box. The stored value is the selected index.

```csharp
[Dropdown("Idle", "Walk", "Run")]
public int State = 1;
```

<div class="inspector-mock">
<div class="field"><span class="label">State</span><div class="control"><div class="dropdown"><span>Walk</span></div></div></div>
</div>

### `[Bitmask("Read", "Write", "Exec")]`

Renders an `int` field as a multi-select combo. Each label maps to a bit.

```csharp
[Bitmask("Read", "Write", "Exec")]
public int Permissions = 0b011;  // Read | Write
```

<div class="inspector-mock">
<div class="field"><span class="label">Permissions</span><div class="control"><div class="dropdown"><span>Read, Write</span></div></div></div>
</div>

### `[Multiline(rows: 4)]`

Renders a `string` field as a multi-line text area.

```csharp
[Multiline(rows: 4)]
public string Notes = "";
```

<div class="inspector-mock">
<div class="field"><span class="label">Notes</span><div class="control"><div class="multiline">Multi-line text area…</div></div></div>
</div>

### `[ColorPreview]`

Renders a `Vector3` or `Vector4` field as a colour picker with a swatch and RGB / HSV /
hex inputs.

```csharp
[ColorPreview]
public Vector4 Tint = new Vector4(0.96f, 0.31f, 0.24f, 1.0f);
```

<div class="inspector-mock">
<div class="field"><span class="label">Tint</span><div class="control"><div class="color"><span class="color-swatch" style="background:#f5503d"></span><span class="color-rgb">RGBA  0.96, 0.31, 0.24, 1.00</span></div></div></div>
</div>

## Conditional display

The referenced field must be a `bool`. Both attributes accept `Invert = true` to flip
the gate.

### `[EditCondition("OtherField")]`

The field is read-only when `OtherField` is `false`.

```csharp
public bool UseCustomColor = false;

[EditCondition("UseCustomColor")]
[ColorPreview]
public Vector4 CustomColor = new Vector4(1, 1, 1, 1);
```

<div class="inspector-mock-pair">
<div>
<div class="inspector-mock-state">UseCustomColor = false</div>
<div class="inspector-mock">
<div class="field"><span class="label">Use Custom Color</span><div class="control"><div class="toggle off">Off</div></div></div>
<div class="field disabled"><span class="label">Custom Color</span><div class="control"><div class="color"><span class="color-swatch" style="background:#fff"></span><span class="color-rgb">RGBA  1.00, 1.00, 1.00, 1.00</span></div></div></div>
</div>
</div>
<div>
<div class="inspector-mock-state">UseCustomColor = true</div>
<div class="inspector-mock">
<div class="field"><span class="label">Use Custom Color</span><div class="control"><div class="toggle on">On</div></div></div>
<div class="field"><span class="label">Custom Color</span><div class="control"><div class="color"><span class="color-swatch" style="background:#fff"></span><span class="color-rgb">RGBA  1.00, 1.00, 1.00, 1.00</span></div></div></div>
</div>
</div>
</div>

### `[HideCondition("OtherField")]`

The field is hidden completely when `OtherField` is `true`.

## Visibility and access

Hidden fields are still serialised.

### `[ReadOnly]`

Greys out the field. The value cannot be edited but still appears.

```csharp
[ReadOnly]
public float ComputedSpeed = 0.0f;
```

<div class="inspector-mock">
<div class="field disabled"><span class="label">Computed Speed</span><div class="control"><div class="number">0.00</div></div></div>
</div>

### `[HideFromEditor]` and `[ShowInEditor("Label")]`

`[HideFromEditor]` hides a public field. `[ShowInEditor]` surfaces a non-public field.

```csharp
[HideFromEditor]
public float InternalState = 0.0f;

[ShowInEditor("Cached robot")]
private RobotControllerComponent? m_Robot;
```

## Action buttons

### `[Button("Label")]`

Renders a method as a clickable button. Defaults to runtime-only (disabled outside play
mode); set `RuntimeOnly = false` to allow editor-time clicks.

```csharp
[Button("Reset position")]
public void ResetPosition()
{
    Translation  = Vector3.Zero;
    RotationQuat = Quaternion.Identity;
}
```

<div class="inspector-mock">
<div class="field"><span class="label">&nbsp;</span><div class="control"><span class="button">Reset position</span></div></div>
</div>

## Composing attributes

Attributes stack. A single field can carry layout, validation, widget, and conditional
attributes at once:

```csharp
[Group("Locomotion")]
[Tooltip("Walker forward speed.")]
[Slider(0.0f, 1.5f)]
[Units("m/s")]
public float ForwardSpeed = 0.5f;
```

<div class="inspector-mock">
<div class="group">Locomotion</div>
<div class="field"><span class="label">Forward Speed<span class="units">(m/s)</span></span><div class="control"><div class="slider"><div class="slider-fill" style="width:33%"></div><div class="slider-value">0.50</div></div></div></div>
</div>

The Inspector renders fields in declaration order, so the order in the source file
determines the order on screen.

## The full set

Every attribute is documented individually in the
[Editor Attributes reference](../api/attributes/index.md), including
`[AllowedAssetTypes]`, `[NoClear]`, `[DisplayPriority]`, the `[ColorPreview]` options
(`HDR`, `Alpha`), and the `[ToggleButton]` colour states.
