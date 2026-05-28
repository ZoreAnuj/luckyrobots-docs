# Deterministic recording with sync points

By default, episodes start at slightly different engine steps from run to run, a side
effect of letting a control loop and a recorder tick at different rates. For general
training data this is desirable: real-world deployments have the same kind of jitter,
and learning across that variance produces a more robust policy.

Some workflows need the opposite. Regression tests, determinism proofs, and
locked-cadence training all need every episode to start on the same engine step. The
**sync point system** does that: a scene adds a **Sync** time runner at a chosen
drumbeat frequency, and scripts gate `BeginEpisode`, resets, and end-of-episode on its
ticks.

!!! abstract "In short"
    Sync points are an opt-in mechanism for locking episode boundaries to a configurable
    drumbeat. Reach for them when you need determinism between episodes or between batches
    of episodes. Skip them when you want the recorder's natural timing variance in your
    training data.

## When to reach for sync points

Sync points are useful when:

- You want to **prove determinism** by showing that two runs of the same scene produce
  bit-identical episode data.
- You are running a **regression test suite** where every episode must start from a
  known engine state.
- Your training target requires episodes to start from a fixed phase relationship
  between runners.

Sync points are usually wrong when:

- You are collecting **general training data**. Real-world deployments have timing
  jitter between sensors and controllers; letting that variance into the dataset trains
  a more robust policy.
- You want the recorder to capture variance in how the control loop behaves across the
  natural phase offsets of its sensors. Sync points hide that variance by collapsing it
  to a single fixed phase.

## How a sync point works

A Sync runner is a normal time runner whose only job is to host the `SyncPointSystem`
subsystem. On every tick of that runner, the system fires a sync event. Scripts watch
for those events through the `Hazel.TimeSync` API and use them as gates on episode
lifecycle:

```mermaid
graph LR
    S1["SYNC"]:::sync -.->|episode N| S2["SYNC"]:::sync -.->|episode N+1| S3["SYNC"]:::sync -.->|episode N+2| S4["SYNC"]:::sync
    classDef sync fill:#f5503d,stroke:#f5503d,color:#ffffff;
```

Each episode is bounded by sync beats. Because the engine state on every beat is the
same fixed phase of the simulation, two runs of the scene reach the same engine state
when they reach the same beat. Datasets become comparable across runs.

## Picking a drumbeat

The Sync runner's frequency expresses a particular pattern of alignment between the
other runners. The **greatest common divisor** of the rates you want to lock to is the
natural choice: every Sync tick lands on a step where all those runners coincide. For a
scene running control at 50 Hz and recording at 30 Hz, that is 10 Hz.

Sub-multiples of the GCD give looser patterns that still land on natural alignments.
5 Hz catches every other alignment, 1 Hz catches every tenth, and so on. The right
cadence is whichever pattern matches how often you want episodes to lock.

Avoid frequencies that are not divisors of the rates you care about. A 7 Hz Sync runner
against 50 Hz and 30 Hz lands on a different phase of each runner at every tick, which
defeats the point of using sync to enforce determinism.

## Scene setup

In the editor's **Time Runners** page, add a runner called **Sync** (or whatever name
you like) at your chosen drumbeat frequency. On the **Subsystems** page, point
`UpdateSyncPointSystemSettings` to that runner.

The scene's serialised form ends up like this:

```yaml
TimeRunners:
  - { ID: 0, Name: Robot,   Frequency: 50 }
  - { ID: 1, Name: Capture, Frequency: 30 }
  - { ID: 2, Name: Sync,    Frequency: 10 }  # the drumbeat: a divisor of both runner rates (here, the GCD of 50 and 30)

UpdateSyncPointSystemSettings:
  Enabled: true
  RunnerID: 2
  RunnerName: Sync
  Frequency: 10
  Priority: -200
  Phase: 0
```

`Priority: -200` places the sync subsystem at the head of the Acquisition phase on its
runner, so script callbacks reading `TimeSync` later in the same step see a fresh tick.

## The TimeSync API

Three static members on `Hazel.TimeSync`:

| Member | What it returns |
|--------|-----------------|
| `IsAtSyncPoint`     | `true` only on the engine step where the sync fires. |
| `CurrentSyncIndex`  | Monotonic count of sync ticks since session start. For "save the index, wait until it changes" patterns. |
| `SyncHz`            | The sync emission rate. Use it to convert a duration in seconds into an exact integer number of sync ticks. |

## Pattern: gate BeginEpisode on a sync tick

The most common use. A script that wants every episode to start on the same drumbeat
waits for the next sync tick after it started watching, then calls `BeginEpisode()`:

```csharp
using Hazel;
using Hazel.Data;

public class SyncedEpisodeStart : Entity
{
    private bool  m_Begun;
    private ulong m_StartSyncIndex;

    protected override void OnCreate()
    {
        m_Begun          = false;
        m_StartSyncIndex = TimeSync.CurrentSyncIndex;
    }

    protected override void OnUpdate(float ts)
    {
        if (m_Begun)
            return;

        // Begin on the first sync STRICTLY after OnCreate, so episode frame 0
        // lands the same fixed gap after every reset.
        if (!TimeSync.IsAtSyncPoint)
            return;
        if (TimeSync.CurrentSyncIndex <= m_StartSyncIndex)
            return;

        if (!Observer.IsRecording)
            Observer.StartRecording();

        Observer.BeginEpisode();
        m_Begun = true;
    }
}
```

The latched `m_StartSyncIndex` matters. Without it, if a sync tick happened to fire on
the same engine step that ran `OnCreate`, `BeginEpisode` would be called immediately
instead of on the next clean drumbeat. The strictly-greater-than guard keeps it on the
next beat.

## Pattern: deterministic sync-tick waits

For repeating cycles (run, reset, run again, reset...), `ts`-accumulating waits drift
relative to the sync clock. The bit-exact pattern converts each wait into an integer
number of sync ticks and counts on the sync itself:

```csharp
using System;
using Hazel;

public class SyncCycle : Entity
{
    [Tooltip("How long to hold quiet before resetting.")]
    public float QuietSeconds = 1.0f;

    [Tooltip("How long to settle after a reset before the next run.")]
    public float SettleSeconds = 2.0f;

    private enum Stage { WaitForStart, Quiet, Settle, Done }

    private Stage m_Stage;
    private ulong m_StageStartSyncIndex;
    private ulong m_QuietSyncTicks;
    private ulong m_SettleSyncTicks;

    protected override void OnCreate()
    {
        double syncHz     = Math.Max(1.0, TimeSync.SyncHz);
        m_QuietSyncTicks  = (ulong)Math.Max(1.0, Math.Ceiling(QuietSeconds  * syncHz));
        m_SettleSyncTicks = (ulong)Math.Max(1.0, Math.Ceiling(SettleSeconds * syncHz));
        m_Stage           = Stage.WaitForStart;
    }

    protected override void OnUpdate(float ts)
    {
        if (!TimeSync.IsAtSyncPoint)
            return;

        if (m_Stage == Stage.WaitForStart)
        {
            m_StageStartSyncIndex = TimeSync.CurrentSyncIndex;
            m_Stage = Stage.Quiet;
            return;
        }

        if (m_Stage == Stage.Quiet)
        {
            if (TimeSync.CurrentSyncIndex - m_StageStartSyncIndex < m_QuietSyncTicks)
                return;

            PerformReset();
            m_StageStartSyncIndex = TimeSync.CurrentSyncIndex;
            m_Stage = Stage.Settle;
            return;
        }

        if (m_Stage == Stage.Settle)
        {
            if (TimeSync.CurrentSyncIndex - m_StageStartSyncIndex < m_SettleSyncTicks)
                return;

            BeginNextCycle();
            m_Stage = Stage.Done;
        }
    }

    private void PerformReset()    { /* ... */ }
    private void BeginNextCycle()  { /* ... */ }
}
```

Each stage transition fires on the exact sync tick that is `N` drumbeats after the
previous stage's start. The gap between reset and the next cycle's first recorded frame
is the same every iteration. No `ts` accumulation, no rounding drift.

This is the pattern Cup Cleaner uses to chain quiet wait → MuJoCo reset → settle wait →
re-enqueue the cycle, so every cycle iteration's frame 0 lands on the same beat.

## Pattern: trigger N runner steps after a sync

The sync drumbeat is the coarse rhythm. For finer alignment, count subsequent
control-runner updates after each beat and trigger work on a specific one. "The second
50 Hz update after the sync" lands a fixed 40 ms after the beat, every beat, without
adding a faster Sync runner.

```csharp
using Hazel;

public class PostSyncTrigger : Entity
{
    [Tooltip("How many control steps to wait after each sync beat before firing.")]
    public int StepsAfterSync = 2;

    private ulong m_LastSyncIndex;
    private int   m_StepsSinceSync;
    private bool  m_FiredThisBeat;

    protected override void OnCreate()
    {
        m_LastSyncIndex  = 0;
        m_StepsSinceSync = 0;
        m_FiredThisBeat  = true;   // wait for the first real sync before arming
    }

    protected override void OnUpdate(float ts)
    {
        // A new sync beat resets the per-beat counter and re-arms the trigger.
        if (TimeSync.IsAtSyncPoint && TimeSync.CurrentSyncIndex != m_LastSyncIndex)
        {
            m_LastSyncIndex  = TimeSync.CurrentSyncIndex;
            m_StepsSinceSync = 0;
            m_FiredThisBeat  = false;
            return;
        }

        if (m_FiredThisBeat)
            return;

        m_StepsSinceSync += 1;

        if (m_StepsSinceSync == StepsAfterSync)
        {
            DoTheThing();
            m_FiredThisBeat = true;
        }
    }

    private void DoTheThing() { /* ... */ }
}
```

The trigger re-arms on every new sync beat and fires exactly once per beat, on the
configured step. The exact same engine step fires every iteration, so the determinism
guarantee of sync gating carries through to the in-between work.

## When to skip sync points entirely

If you are collecting general training data, leave the sync point system disabled. The
recorder will run at its configured rate, the controller will run at its own, and the
phase between them will vary slightly across episodes. That variance is what trains a
robust policy — preserve it. See [Recording with the Observer](recording.md) for the
default path.
