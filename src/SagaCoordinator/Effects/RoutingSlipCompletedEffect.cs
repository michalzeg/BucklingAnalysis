using MassTransit;
using MassTransit.Courier.Contracts;
using SagaCoordinator.Saga;

namespace SagaCoordinator.Effects;

public class RoutingSlipCompletedEffect : IEffect<RoutingSlipCompleted>
{
    private readonly ILogger<RoutingSlipCompletedEffect> _logger;

    public RoutingSlipCompletedEffect(ILogger<RoutingSlipCompletedEffect> logger)
    {
        _logger = logger;
    }
    public void Then(BehaviorContext<CalculationsCoordinatorState, RoutingSlipCompleted> context)
    {
        _logger.LogInformation("SAGA: Process completed. Message: {message}. Duration: {duration}", context.Message.Duration, context.Message.Duration);
    }

    public Task ThenAsync(BehaviorContext<CalculationsCoordinatorState, RoutingSlipCompleted> context)
    {
        return Task.CompletedTask;
    }
}
