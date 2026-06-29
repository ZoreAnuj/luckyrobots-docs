# Assets

Assets are the files a project is built from: scenes, meshes, materials, textures, robot
models, learned policies, and scripts. Lucky Engine keeps every asset inside the
project's `Assets` folder and tracks it in a registry. Scenes and components reference an
asset by a stable handle, so a reference keeps working after the asset is renamed or
moved within the editor.

!!! abstract "In short"
    The **Content Browser** is the home for project assets. **Import** copies a source
    file into the project, the **New** menu creates engine-native assets, and ++ctrl+s++
    saves the current scene and the project. Ready-made robot packs arrive through the
    Content Vault, covered in [Robots](robots.md).

## The Content Browser

The **Content Browser** (View → Content Browser, open by default) is the panel for
browsing, importing, creating, and organizing project assets. It has two parts: a folder
tree on the left and a thumbnail grid on the right. The grid shows live previews for
textures, materials, meshes, environment maps, and prefabs, and a type icon for
everything else.

A few related panels sit alongside it:

<div class="grid cards" markdown>

-   :material-folder-multiple-outline: **Content Browser**

    The project file browser. Import, create, rename, move, and delete the assets that
    live under the project's `Assets` folder.

-   :material-cloud-download-outline: **Content Vault**

    Installs ready-made robot and environment packs from the Lucky Robots library. See
    [Robots](robots.md) for the pack workflow.

-   :material-palette-outline: **Material Editor**

    Opens when a material is double-clicked. Edits the PBR channels and their textures.
    Motion graphs, animation graphs, and sound graphs each open their own node editor.

</div>

## How assets are stored

A project is a folder on disk. The editor writes a project file and an `Assets`
directory next to it:

```text
MyProject/
  MyProject.hproj          project file (YAML)
  Assets/
    AssetRegistry.hzr      asset registry (handle, path, type)
    Scenes/
    Meshes/
    Materials/
    ...
  Cache/                   generated data, safe to delete
```

Every asset has a 64-bit **handle**. The registry, `Assets/AssetRegistry.hzr`, maps each
handle to a file path and an asset type. Scenes, prefabs, and components store the handle
rather than the path, which is why an asset can move between folders without breaking the
things that point at it.

!!! note "Organize inside the Content Browser"
    Renaming and moving assets from the Content Browser keeps the registry in sync.
    Moving the same files from outside the editor can leave the registry pointing at the
    old location, so prefer the in-editor actions.

## Asset types

Lucky Engine recognizes an asset by its file extension. Two groups exist: source files
imported from outside, and engine-native files created and saved in the editor.

Imported from source files:

| Content | Source files | Becomes |
|---------|--------------|---------|
| 3D models          | `.fbx` `.gltf` `.glb` `.obj` `.dae` `.vrm` `.stl` | Mesh source |
| Images, textures   | `.png` `.jpg` `.jpeg`                             | Texture |
| Environment maps    | `.hdr`                                            | Environment map |
| Robot and world models | `.xml`                                        | MuJoCo scene (MJCF) |
| Policy models      | `.onnx`                                            | ONNX model, or a policy via Import ONNX Policy |
| Motion clips       | `.json`                                            | Motion |
| Datasets           | `.parquet` `.parq` `.pq`                           | Parquet data |
| Audio              | `.wav` `.ogg`                                      | Audio |
| Fonts              | `.ttf` `.otf` `.ttc`                               | Font |

Created and saved in the editor (native formats):

| Asset | File | Made with |
|-------|------|-----------|
| Scene               | `.hscene`     | New → Scene |
| Material            | `.hmaterial`  | New → Material, edited in the Material Editor |
| Prefab              | `.hprefab`    | Drag a scene entity into the Content Browser |
| Static mesh         | `.hsmesh`     | Convert to Static Mesh, from a mesh source |
| Skinned mesh        | `.hmesh`      | Convert to Dynamic Mesh, from a mesh source |
| Motion graph        | `.hmograph`   | New → Motion Graph |
| Animation graph     | `.hanimgraph` | New → Animation Graph |
| Script              | `.cs`         | New → Script |
| Sound config        | `.hsoundc`    | New → Audio → Sound Config |
| Spatialization config | `.hspatconfig` | New → Audio → Spatialization Config |
| SoundGraph sound    | `.sound_graph`| New → Audio → SoundGraph Sound |

!!! warning "Every `.json` imports as a Motion asset"
    The `.json` extension always maps to a Motion asset. Policy configuration files such
    as `model_config.json` and `policy_descriptor.*.json` are read by path where they are
    needed, not used as standalone assets, so their classification in the browser can be
    ignored.

## Importing assets

Importing brings an outside file into the project. Right-click an empty area of the
Content Browser grid and pick one of:

| Menu item | What it does |
|-----------|--------------|
| **Import**                       | Opens a file picker and copies the chosen file into the current folder. |
| **Import ONNX Policy (.honnx)**  | Bundles an `.onnx` model with its `model_config.json` into a `.honnx` policy. |
| **Bulk Import**                  | Copies an entire folder tree into the project at once. |

The copied file is registered as an asset on the next refresh, using the type from its
extension. A file whose extension is not recognized is copied but not tracked.

Importing a 3D model opens the **Create Assets From Mesh Source** wizard. It offers to
build a static mesh, a dynamic (skinned) mesh, animations, and an animation graph from
the imported model, with options for generating colliders.

For learned policies, **Import ONNX Policy** writes a `.honnx` manifest that points at the
copied `.onnx` model and its `model_config.json`. The robot-side workflow for registering
that policy on a `RobotControllerComponent` is in
[Robots → Adding a policy](robots.md#adding-a-policy).

## Creating assets

Right-click an empty area of the grid and open the **New** submenu:

| New → | Creates |
|-------|---------|
| **Folder**          | A new folder for organizing assets. |
| **Scene**           | An empty scene. |
| **Material**        | A PBR material, opened in the Material Editor. |
| **Motion Graph**    | A robot motion graph, opened in the node editor. |
| **Animation Graph** | A skeletal animation graph. |
| **Script**          | A C# script from the starter template, after choosing a namespace and class name. |
| **Audio**           | A sound config, spatialization config, or SoundGraph sound. |

A mesh source has two more options in its right-click menu: **Convert to Static Mesh** and
**Convert to Dynamic Mesh**, which write a `.hsmesh` or `.hmesh` asset that scenes can
reference directly.

## Saving

++ctrl+s++ saves the current scene and the project together, and reports `Project Saved`.
++ctrl+shift+s++ saves the current scene under a new name.

Native assets are files in the `Assets` folder. Their editors, such as the Material Editor
and the motion graph editor, write the asset when changes are applied. Imported source
files are never rewritten by the engine: the file on disk stays authoritative, and the
engine only reads from it.

The **File** menu holds the scene and project actions: New Scene, Open Scene, Save Scene
As, Set Scene as Startup, and Revert to Saved, along with Import Project and Export
Project.

## Organizing assets

Right-click an asset for its actions:

| Action | Notes |
|--------|-------|
| **Rename**            | Inline rename. Keeps every handle reference intact. |
| **Duplicate**         | ++ctrl+d++. |
| **Copy** / **Paste**  | ++ctrl+c++ / ++ctrl+v++. |
| **Delete**            | ++del++, with a confirmation prompt. |
| **Reload**            | Re-reads the asset from disk. |
| **Open in Text Editor** / **Open Externally** | Opens the file for editing outside the grid. |
| **Show in Explorer**  | Reveals the file in the OS file manager. |
| **Export** (scenes)   | Writes the scene and its asset dependencies to a folder with their own registry. |

Move an asset by dragging it onto a folder in the grid, the folder tree, or a breadcrumb
entry. The top bar adds back and forward navigation (++alt+left++ and ++alt+right++), a
filter that limits the grid to one asset type, and a search box (++ctrl+f++) that matches
file names. ++ctrl+shift+n++ creates a new folder. A settings popup adjusts the thumbnail
size and toggles the asset-type labels.

## Using assets in a scene

Most assets are placed by dragging them out of the Content Browser:

| Drag this | Onto | Result |
|-----------|------|--------|
| Mesh or static mesh | The viewport       | A new entity at the drop point |
| Prefab              | The viewport       | A prefab instance |
| MJCF model          | The viewport       | A robot or world with MuJoCo physics |
| Material            | A surface in the viewport | Assigns the material to that submesh |
| Texture             | A channel in the Material Editor | Assigns the texture to that channel |
| Scene               | The viewport       | Opens the scene |
| Mesh source         | The viewport       | Opens the Create Assets From Mesh Source wizard |
| A scene entity      | The Content Browser | Saves the entity as a prefab |

Double-clicking an asset opens it. Materials open the Material Editor; motion graphs,
animation graphs, and sound graphs each open their own node editor. Scenes open in the
viewport.

The lifecycle of an imported asset is short and linear:

```mermaid
graph LR
    A([Source file]):::s -->|Import| B([Copied under Assets/]):::s
    B --> C([Registered with a handle]):::s
    C -->|loaded on first use| D([Placed in a scene]):::s
    D -->|referenced by handle| E([Scenes and components]):::s
    classDef s fill:#2d3a4f,stroke:#4a5e7e,color:#ffffff;
```

## Robotics assets

The robot-specific asset types fit into the same system:

- **MJCF models** (`.xml`) are MuJoCo scenes. A robot is an MJCF model with its meshes
  and textures. Dragging one into the viewport builds the robot with its physics.
- **Policies** (`.honnx`) bundle an ONNX model with its configuration. They are imported
  through Import ONNX Policy and registered on a robot from the robot's context menu.
- **Motion graphs** (`.hmograph`) drive IK and motion planning. They are authored in the
  motion graph node editor.

Most robots arrive prebuilt as Content Vault packs, so these files are already wired up on
placement. The pack contents and the robot setup are covered in [Robots](robots.md).
Policy configuration files (`model_config.json`, `policy_descriptor.*.json`) and the
project's `PolicyRegistry.yaml` are plain configuration read by path, not catalog assets.

## Where to go next

- [Robots](robots.md) covers robot packs, the Content Vault, and registering policies and
  motion graphs on a robot.
- [Working with components](scripting/getting-started/working-with-components.md) shows how
  scripts reference meshes, materials, and MuJoCo bodies at runtime.
- [Recording with the Observer](scripting/guides/recording.md) produces the Parquet
  datasets that show up as data assets.
