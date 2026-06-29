# Editor Interface

The Lucky Engine editor is built around a central 3D viewport surrounded by dockable
panels, with a title bar across the top for the menus, the project and scene name, the
account controls, and the window layout. The editor ships in two layouts: a simplified
default and a full advanced layout.

!!! abstract "In short"
    The editor opens in **Simple** mode: the viewport and editors in the center, framed by
    docks on the left, bottom, and right, each with its own fixed panels, plus floating
    toolbars. **Advanced** mode unlocks the full set of freely dockable panels. Switch
    between them in Settings, on the Editor page, and open any panel from the **View**
    menu.

## Simple and Advanced layouts

The editor starts in **Simple** mode by default. It launches on a Welcome screen
(Continue, Blank Scene, Create Project, Example Projects, and recent projects), then opens
a fixed layout: the viewport and any open editors in the center, framed by three docks.

| Dock | Panels (as tabs) |
|------|------------------|
| **Left**   | The **Sidebar**, an icon rail that switches between the **File Tree**, **Scene Hierarchy**, **gRPC Server**, and **Observer**. |
| **Bottom** | **Content Browser**, **Log**, **Content Vault**, and **Asset Manager**. |
| **Right**  | **Agent**, **Content Creator**, **Scene Renderer**, **Statistics**, **Shortcuts**, and **MuJoCo Force Tool**. |
| **Center** | The viewport, plus the editors and panels that open as tabs over it: **Material Editor**, the **Motion Graph** and **Animation Graph** editors, **Settings**, **LuckyHub**, **Policies**, **Data Viewer**, and open scripts. |

Floating overlay toolbars sit over the viewport for play and recording, the transform
gizmos, and selection actions.

**Advanced** mode replaces the fixed docks and overlays with the full set of freely
dockable panels (listed in the tables below) and the in-viewport toolbars. Every panel can
be docked, floated, tabbed, and resized, and is opened from the **View** menu (Settings
and Audio Events from Edit, Shortcuts and Licenses from Help).

Switch layouts in **Settings → Editor → Simple UX**, or from the viewport settings toolbar
in Advanced mode with **Switch to Simple Mode**.

!!! note "Where the Inspector lives"
    There is no separate Inspector or Properties panel. Entity properties and Add
    Component are shown inside the **Scene Hierarchy** panel, below the entity tree.

## The title bar

The title bar runs across the top in both layouts. From left to right it holds:

- the Lucky Robots logo and the **menu bar** (File, Edit, Build, View, Tools, Help),
- the current **project** name and a **scene** dropdown that lists the project's scenes,
- a **Build C# Assembly** button (++ctrl+b++),
- the engine name and version,
- the dock toggles and a **Settings** gear,
- the account control: **Sign In**, or an avatar menu (Profile, LuckyHub, Logout) when
  signed in,
- the window minimize, maximize, and close buttons.

## Core panels

| Panel | Open from | What it does |
|-------|-----------|--------------|
| **Viewport** | always open | The 3D scene view. Camera modes and gizmo tools are in [Viewport Navigation](viewport-navigation.md). |
| **Scene Hierarchy** | View menu / Sidebar | The entity tree, plus the properties and Add Component inspector for the selected entity. |
| **Content Browser** | open by default | Browse and open project assets: scenes, scripts, materials, meshes, and more. See [Assets](assets.md). |
| **Log** | open by default | Console and log output. |
| **Settings** | Edit menu / gear | Editor, project, scene, and recorder settings, including the Simple UX toggle. |

## Robots and simulation panels

| Panel | Open from | What it does |
|-------|-----------|--------------|
| **Content Vault** | View menu | Install robot and environment packs, and create projects from them. See [Robots](robots.md). |
| **Policies** | View menu | Manage and run the robot's control policies. |
| **Observer** | View menu / Sidebar | Control recording and data-capture sessions. See [Recording](scripting/guides/recording.md). |
| **gRPC Server** | View menu / Sidebar | Start, stop, and monitor the gRPC control server. See [gRPC API](grpc-api.md). |
| **MuJoCo Force Tool** | View menu | Apply forces and torques to MuJoCo bodies. |
| **MuJoCo Stats**, **Physics Stats** | View menu | Solver and physics statistics. |

## Asset and authoring panels

| Panel | Open from | What it does |
|-------|-----------|--------------|
| **Material Editor** | View menu | Edit PBR materials. |
| **File Tree** | View menu / Sidebar | Browse the project's files and open scripts, scenes, and data. |
| **Data Viewer** | opens on a dataset | View recorded dataset files. |
| **Content Creator** | View menu | Package robots and environments into Content Vault packs. |
| **Audio Events** | Edit menu | Edit audio event definitions. |
| **Scene Renderer** | View menu | Scene renderer and render-pipeline settings. |

## Account, diagnostics, and help

| Panel | Open from | What it does |
|-------|-----------|--------------|
| **LuckyHub** | View menu / avatar | Account and cloud integration. |
| **Agent** | View menu | The in-editor AI assistant. |
| **ECS Debug**, **Asset Manager**, **Physics Captures**, **Statistics** | View menu | Diagnostic and profiling views, shown in Advanced mode. |
| **Shortcuts**, **Licenses** | Help menu | Keyboard reference and third-party licenses. |

## Menus

The menu bar sits in the title bar:

| Menu | Holds |
|------|-------|
| **File** | New, open, and save scene; create, open, and save project; import and export project; build. |
| **Edit** | Undo and redo, Settings, Audio Events, and cache tools. |
| **Build** | Open or regenerate the C# solution, build and reload the C# assembly. |
| **View** | Toggles for every panel, and the viewport list. |
| **Tools** | FFmpeg settings, the sound bank, and ImGui developer tools. |
| **Help** | Shortcuts, Licenses, Paths, and About. |

## Play controls

The central toolbar holds the run controls in both layouts:

| Control | Effect |
|---------|--------|
| **Play** | Run the scene: scripts and physics together. Shortcut: ++alt+p++. |
| **Pause** | Hold the simulation. Press again to resume. |
| **Stop** | Return to the editable scene. |
| **Simulate Physics** | Advanced mode. Run the physics without the scripting runtime. |

The gizmo tools and camera modes are covered in
[Viewport Navigation](viewport-navigation.md).

## Arranging panels

The editor uses a docking workspace. In Advanced mode, panels dock, float, tab, and
resize freely; drag a panel's tab to move it. Open or close any panel from the **View**
menu (Settings and Audio Events are under Edit, Shortcuts and Licenses under Help).

## Where to go next

- [Viewport Navigation](viewport-navigation.md) covers the camera modes and gizmo tools.
- [Assets](assets.md) covers the Content Browser and the asset system.
- [Get Started](get-started.md) installs the editor and opens the first scene.
