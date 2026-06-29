# Get Started

Lucky Engine is a desktop editor. Installing it, launching it, and opening the bundled
Welcome scene takes a few minutes. This page covers installation, the first launch,
signing in, running a scene, and creating a project.

!!! abstract "In short"
    Download Lucky Engine from luckyrobots.com and launch the editor. It opens the Robot
    Sandbox project on the Welcome scene. Press Play to run it, and use Create Project to
    start a new one. Signing in is optional and unlocks recording and the Content Vault.

## Install

Download Lucky Engine for Windows or Linux (64-bit) from
[luckyrobots.com](https://luckyrobots.com). The current release is **2026.1**.

Launch the editor: on Windows run `LuckyEditor.exe`; on Linux run the `LuckyEditor`
binary. A splash screen shows while the script engine and systems initialize, then the
editor window opens.

### Requirements

| Requirement | Detail |
|-------------|--------|
| Operating system | Windows 10 (version 1809, build 17763) or later, or a 64-bit Linux distribution |
| Graphics | A GPU and driver supporting **Vulkan 1.3** or later. The editor does not start without it. |

## First launch

By default the editor opens the most recent project, or the bundled **Robot Sandbox**
example on the first run. Robot Sandbox opens on the Welcome scene, a Franka Panda arm
running an automated pick-and-place loop (see [Example Projects](example-projects.md)).

The first screen is the Welcome screen:

<div class="grid cards" markdown>

-   :material-play: **Continue**

    Enter the current scene, the Welcome scene, and start working in the viewport.

-   :material-folder-open-outline: **Recent and Example Projects**

    Reopen a recent project or install an example from the Content Vault.

-   :material-plus-box-outline: **Create Project**

    Start a new project around a robot and an environment.

</div>

## Signing in

Signing in is optional. The editor, scenes, and play mode all work signed out. Sign in
with a Lucky Robots account to record datasets and to use the Content Vault and Hub.

To sign in, click **Sign In** in the title bar, or **Login** on the Welcome screen. The
editor opens a browser to authorize the account, then returns signed in. An authorization
code can also be pasted manually if the browser handoff does not complete.

## Running a scene

Click **Continue** to enter the Welcome scene. The central toolbar holds the run
controls:

| Control | Effect |
|---------|--------|
| **Play**  | Run the scene: scripts and physics together. Shortcut: ++alt+p++. |
| **Pause** | Hold the simulation. Press again to resume. |
| **Stop**  | Return to the editable scene. |

The Panda begins its pick-and-place loop as soon as Play starts. The full (Advanced)
editor layout adds a **Simulate Physics** control, which runs the physics without the
scripting runtime. How simulation time advances, in real time or deterministically, is
set by the [Time Manager](time-manager.md).

## Creating a project

On the Welcome screen, choose **Create Project**. The form collects:

- a robot, from the Content Vault (optional),
- an environment (optional),
- a project name,
- a location (default: `Documents/Lucky Engine/Projects`).

The editor scaffolds the project with an `Assets` folder, a starter scene, and a C#
scripts project, then installs the chosen robot and environment. The same flow is
available from **File → Create Project**.

A project's layout and how assets are tracked are covered in [Assets](assets.md). To
start writing behavior, see the [scripting guide](scripting/getting-started/index.md).

## Where to go next

- [Example Projects](example-projects.md) covers the Welcome scene and other ready-to-run
  scenes.
- [Robots](robots.md) covers installing robots from the Content Vault and bringing in an
  MJCF model.
- [Scripting](scripting/index.md) is the C# path for driving behavior in a scene.
- [gRPC API](grpc-api.md) drives simulations from any language over gRPC.
