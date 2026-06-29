# Viewport Navigation

The editor viewport has three navigation modes, each entered by a different modifier: a
**fly camera** for free movement around the scene, an **orbit camera** for inspecting a
single focal point, and a **default** mode for selecting entities and switching gizmo
tools.

<div class="grid cards" markdown>

-   :material-airplane: **Fly camera**

    Hold the right mouse button, then steer with WASD and look with the mouse. The
    everyday way to fly around a scene.

-   :material-orbit: **Orbit camera**

    Hold ++alt++ and drag with a mouse button to orbit, pan, or zoom around the camera's
    focal point. Useful for inspecting a single subject.

-   :material-cursor-default: **Selection and gizmos**

    Cursor over the viewport with no modifier: click to select, scroll to zoom, single
    keys switch between transform tools.

</div>

## Fly camera

Hold the **right mouse button** to enter fly mode. The cursor is captured for the
duration. Release the button to regain the cursor and exit fly mode.

| Input | Effect |
|-------|--------|
| Mouse drag                           | Look (yaw and pitch) |
| ++w++ &nbsp; / &nbsp; ++s++          | Move forward / backward |
| ++a++ &nbsp; / &nbsp; ++d++          | Strafe left / right |
| ++q++ &nbsp; / &nbsp; ++e++          | Move down / up (world up) |
| Mouse scroll                         | Adjust fly speed |
| Hold ++shift++                       | Speed boost while moving |

Pitch is clamped just short of straight up and straight down, so the camera will not
flip over. Fly speed scales with the current setting, so scrolling once at slow speeds
makes a small adjustment and scrolling once at high speeds makes a large one.

## Orbit camera

Hold ++alt++ and drag with a mouse button. The camera orbits around its current focal
point (set by the last focus or by zooming).

| Input | Effect |
|-------|--------|
| ++alt++ + Left-mouse drag    | Orbit around the focal point |
| ++alt++ + Middle-mouse drag  | Pan the focal point sideways |
| ++alt++ + Right-mouse drag   | Zoom in or out along the focal axis |

## Selection and gizmos

When the cursor is over the viewport and the right mouse button is **not** held,
single-key presses switch between transform tools. Left-click to select an entity;
selected entities receive the active gizmo.

| Key   | Effect |
|-------|--------|
| ++q++ | Clear the gizmo (selection only) |
| ++w++ | Translate gizmo |
| ++e++ | Rotate gizmo |
| ++r++ | Scale gizmo |
| ++t++ | Universal gizmo (translate, rotate, and scale together) |
| ++f++ | Focus the camera on the current selection |
| ++p++ | Toggle MuJoCo pivot manipulation |

Mouse scroll without any modifier zooms toward the focal point. The wheel changes the
camera's distance to the focal point, leaving its angle alone.

!!! note
    The same letter keys mean different things depending on the right mouse button.
    With right-click held: ++w++ ++a++ ++s++ ++d++ ++q++ ++e++ move the camera. Without
    it: ++w++ ++e++ ++r++ ++t++ switch gizmo tools and ++q++ clears the gizmo.

## At a glance

| Mode | How to enter | Headline action |
|------|--------------|-----------------|
| Fly                  | Hold right mouse     | WASD to move, mouse to look |
| Orbit                | Hold ++alt++         | Alt + mouse-button to orbit, pan, or zoom |
| Selection and gizmos | No modifier          | Click to select, ++w++ ++e++ ++r++ ++t++ to switch tools, ++f++ to focus |

!!! tip "Frame the selection"
    ++f++ snaps the camera to a sensible distance from whatever is currently selected,
    using the entity's bounding box. It is the fastest way to recover when the camera
    has drifted off the scene.
