using MassTransit;

namespace SagaCoordinator.Saga;
public class CalculationsCoordinatorState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public int CurrentState { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public int ActivityIndex { get; set; }
    public List<string> CompletedActivities { get; set; } = [];
}