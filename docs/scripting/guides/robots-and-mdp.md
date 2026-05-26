# Robots & MDP Tasks

Lucky Engine exposes an engine-side **MDP** (Markov Decision Process) component model for
authoring robot agents and reinforcement-learning tasks. Training frameworks discover
these components and compose them into a task contract.

This is an overview; every type is listed in the
[Robots & Learning (MDP) reference](../api/learn/index.md).

## The building blocks

An MDP task is assembled from small, single-purpose components, each implementing one
of these interfaces:

| Interface | Role |
|-----------|------|
| [`IMdpObservation`](../api/learn/imdpobservation.md) | Computes a slice of the observation vector from scene/agent state. |
| [`IMdpReward`](../api/learn/imdpreward.md) | Computes a scalar reward for the current step. |
| [`IMdpTermination`](../api/learn/imdptermination.md) | Decides whether the episode should end (termination vs. timeout). |
| [`IRandomizationHandler`](../api/learn/irandomizationhandler.md) | Applies domain randomization on reset. |

Each receives an [`MdpContext`](../api/learn/mdpcontext.md) — an immutable snapshot of the
MuJoCo scene, the [`RobotAgent`](../api/learn/robotagent.md), timing, and the current/previous
action buffers — and reports a `ComponentDescriptor` so training frameworks know its
parameters and output shape.

```csharp
using System.Collections.Generic;
using Hazel;

// A reward term that penalizes large actions (encourages smooth control).
public sealed class ActionMagnitudePenalty : IMdpReward
{
    public string Name => "action_magnitude_penalty";

    public float Compute(MdpContext ctx, Dictionary<string, string> parameters)
    {
        float sum = 0.0f;
        foreach (var a in ctx.CurrentActions)
            sum += a * a;
        return -sum;
    }

    public ComponentDescriptor Descriptor =>
        new ComponentDescriptor(Name, "Penalizes large actions.", "generic");
}
```

Components are registered with the
[`MdpComponentRegistry`](../api/learn/mdpcomponentregistry.md) and discovered
automatically.

## Robots

[`RobotAgent`](../api/learn/robotagent.md) and
[`RobotController`](../api/learn/robotcontroller.md) represent a controllable robot and its
control interface. See the [reference](../api/learn/index.md) for agent management,
command managers, and contract negotiation.

!!! note
    The MDP types span the `Hazel` and `Hazel.MdpComponents` namespaces and are grouped
    together here under **Robots & Learning**. Hot-path methods (`Compute`, `Evaluate`)
    are called every simulation step and must not allocate.
