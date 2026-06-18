# Robots & Learning (MDP)

APIs for authoring robot agents and reinforcement-learning tasks: the MDP component model (observations, rewards, terminations, randomization), robot controllers, and agent management.

| Type | Kind | Summary |
|------|------|---------|
| [ActionGroupSlotInfo](actiongroupslotinfo.md) | `class` | Describes a resolved action group for multi-policy control. |
| [AgentBatch](agentbatch.md) | `class` |  |
| [AgentResetData](agentresetdata.md) | `struct` |  |
| [BodyData](bodydata.md) | `class` |  |
| [CalibrationMapping](calibrationmapping.md) | `struct` |  |
| [CommandManager](commandmanager.md) | `class` | Holds a RobotAgent's commands (e.g. |
| [ComponentDescriptor](componentdescriptor.md) | `struct` | Describes one available MDP component for the capability manifest. |
| [ContractNegotiator](contractnegotiator.md) | `class` | Validates a TaskContract, resolves optional terms, configures the engine's observation/reward/termination pipeline, and returns a NegotiatedTaskSession that subsequent Step/Reset calls reference. |
| [ContractValidator](contractvalidator.md) | `class` | Validates a TaskContract against the MdpComponentRegistry. |
| [ExternalAgentRegistry](externalagentregistry.md) | `static class` | Thread-safe bridge between the engine-driven Learn pipeline and the gRPC services. |
| [HzMath](hzmath.md) | `static class` |  |
| [HzObservations](hzobservations.md) | `static class` |  |
| [IAuxiliaryDataProvider](iauxiliarydataprovider.md) | `interface` | Auxiliary data provider. |
| [ICommand](icommand.md) | `interface` |  |
| [IMdpObservation](imdpobservation.md) | `interface` | Engine-side observation function. |
| [IMdpReward](imdpreward.md) | `interface` | Engine-side reward signal. |
| [IMdpTermination](imdptermination.md) | `interface` | Engine-side termination condition. |
| [IRandomizationHandler](irandomizationhandler.md) | `interface` | Domain randomization handler. |
| [JointConfig](jointconfig.md) | `struct` |  |
| [ManifestSnapshot](manifestsnapshot.md) | `class` | Snapshot of all MDP component descriptors for a given robot. |
| [MdpComponentRegistry](mdpcomponentregistry.md) | `static class` | Central registration table for engine-side MDP components. |
| [MdpContext](mdpcontext.md) | `class` | Immutable context passed to MDP component Compute/Evaluate methods. |
| [MdpObservationAttribute](mdpobservationattribute.md) | `class` | Marks a static method as an MDP observation component. |
| [MdpRewardAttribute](mdprewardattribute.md) | `class` | Marks a static method as an MDP reward component. |
| [MdpScriptDiscovery](mdpscriptdiscovery.md) | `static class` | Discovers user-defined MDP components from loaded C# assemblies. |
| [MdpTerminationAttribute](mdpterminationattribute.md) | `class` | Marks a static method as an MDP termination component. |
| [NegotiatedSession](negotiatedsession.md) | `class` | Runtime state for a negotiated task session. |
| [NegotiationOutcome](negotiationoutcome.md) | `class` | Result of a negotiation attempt. |
| [ObservationComponents](observationcomponents.md) | `static class` | Built-in observation components that extract robot state from MuJoCo. |
| [ObservationSlotInfo](observationslotinfo.md) | `class` | Describes one slot in the negotiated observation layout. |
| [ParamDescriptor](paramdescriptor.md) | `struct` | Describes an accepted parameter for an MDP component. |
| [RandomizationDescriptor](randomizationdescriptor.md) | `struct` | Extended descriptor for randomization components with range defaults. |
| [RandomizationHandlers](randomizationhandlers.md) | `static class` | Built-in domain randomization handlers. |
| [RefPoseCalibrator](refposecalibrator.md) | `class` |  |
| [RefPoseExtractor](refposeextractor.md) | `class` |  |
| [RewardComponents](rewardcomponents.md) | `static class` | Built-in reward signal components computed from MuJoCo state. |
| [RobotAgent](robotagent.md) | `abstract class` | Base class for a robot in the Learn pipeline. |
| [RobotController](robotcontroller.md) | `struct` |  |
| [RobotEnv](robotenv.md) | `class` | Internal per-policy batching layer owned by RobotManager. |
| [RobotManager](robotmanager.md) | `class` | Owns the per-tick Learn pipeline for a set of RobotAgents. |
| [StateConfig](stateconfig.md) | `struct` |  |
| [TerminationComponents](terminationcomponents.md) | `static class` | Built-in termination conditions evaluated from MuJoCo state. |
| [ValidationMessage](validationmessage.md) | `class` | Individual validation message with actionable suggestion. |
| [ValidationResult](validationresult.md) | `class` | Result of contract validation. |
| [VelocityCommand](velocitycommand.md) | `class` |  |
