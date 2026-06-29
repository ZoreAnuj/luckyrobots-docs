# Example Projects

Lucky Engine ships with ready-made example projects in the Content Vault. Each one is a
complete scene built around a specific robot and a specific technique, set up to run as
soon as it opens. They are the quickest way to see policies, IK, gripping, and data
recording working together in a real scene.

!!! abstract "In short"
    Open the Content Vault (View → Content Vault) and filter to the **Examples**
    category to install a project. The **Welcome** scene opens on first run as the start
    scene of the default Robot Sandbox project. **Piper Pattern Stacking** and **Walking
    Pick and Place** install as their own projects.

<div class="grid cards" markdown>

-   :material-hand-wave: **Welcome (Robot Sandbox)**

    A Franka Panda arm runs an automated pick-and-place loop with motion-graph IK. The
    first scene the editor opens.

-   :material-cube-outline: **Piper Pattern Stacking**

    An AgileX Piper arm stacks blocks into a target pattern.

-   :material-walk: **Walking Pick and Place**

    A Unitree G1 humanoid walks to cups, picks them up, and moves them, combining a
    trained walking policy with dynamic IK.

</div>

## Welcome (Robot Sandbox)

The Welcome scene is the start scene of **Robot Sandbox**, the default project, so it is
what the editor opens on first run. It features a Franka Emika Panda arm with a
`RobotControllerComponent` and a MuJoCo physics body.

A script drives an automated pick-and-place loop that runs on its own when the scene
plays. The arm watches a zone for props, approaches each one, closes its gripper, carries
it to a drop target, releases it, and returns home. The motion runs entirely through
motion-graph IK: the script sets a world-space target position, orientation, and
duration, then triggers the graph. Each completed cycle is recorded as an episode through
the Observer.

It is a compact tour of the `RobotControllerComponent`, motion-graph IK, gripper
actuators, and the recording loop. For the script-level view of these, see
[Working with components](scripting/getting-started/working-with-components.md) and
[Recording with the Observer](scripting/guides/recording.md).

## Piper Pattern Stacking

Piper Pattern Stacking is built around the AgileX Piper, a 6-DoF arm with a parallel
gripper. The arm stacks blocks into a target pattern. The project ships with the Piper
robot assets, materials, and a ready-to-run scene.

It is one of a family of Piper examples in the Content Vault, alongside pick-and-place,
block stacking, and unscrew-cap. They share the same robot and differ in the task, which
makes them useful for comparing how a single arm is scripted across different goals.

<iframe class="doc-video" src="https://www.youtube-nocookie.com/embed/rcNxVd_snUg?cc_load_policy=1&rel=0&vq=hd1080" title="Piper Pattern Stacking Tutorial // Lucky Engine" loading="lazy" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

## Walking Pick and Place

Walking Pick and Place is a Unitree G1 humanoid fitted with five-fingered hands. It walks
to cups, picks them up, and moves them by combining two control surfaces at once: a
trained walking policy and dynamic IK.

It is the example to study for policy-and-IK co-control on a humanoid. The general
pattern, where a policy and an IK chain drive different joints at the same time and a
joint-ownership mask keeps them from overwriting each other, is described in
[Controlling robots: policies and IK](scripting/guides/controlling-robots.md). The
five-fingered hands make it the richest grasping example in the set.

<iframe class="doc-video" src="https://www.youtube-nocookie.com/embed/6Yh42V46-IU?cc_load_policy=1&rel=0&vq=hd1080" title="Walking Pick and Place Tutorial // Lucky Engine" loading="lazy" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

## Opening an example

All examples live in the **Content Vault** (View → Content Vault). Set the category
filter to **Examples** to see the list, then install one. Installing sets up the
project's scenes, robot assets, materials, and scripts, ready to open and run. The
Content Vault workflow and the shape of a robot pack are covered in [Robots](robots.md).

The Examples category holds more than the three above, including additional Piper tasks,
an SO-100 pick-and-place, Unitree Go2 and G1 setups, and an oscillator. The Content Vault
panel is the authoritative current list.

## Where to go next

- [Robots](robots.md) covers the Content Vault, robot packs, and registering policies and
  motion graphs on a robot.
- [Controlling robots: policies and IK](scripting/guides/controlling-robots.md) is the
  script-level guide behind Walking Pick and Place's policy-and-IK co-control.
- [Working with components](scripting/getting-started/working-with-components.md) shows how
  scripts drive MuJoCo bodies, policies, and IK targets at runtime.
- [Recording with the Observer](scripting/guides/recording.md) covers the episode loop the
  Welcome scene uses.
