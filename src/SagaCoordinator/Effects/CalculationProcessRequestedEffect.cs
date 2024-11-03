using CalculationActivities.CalculationsRoutingSlip;
using Infrastructure.Json;
using MassTransit;
using SagaCoordinator.Saga;
using Shared;
using Shared.Events;
using Shared.Storage;

namespace SagaCoordinator.Effects;

public class CalculationProcessRequestedEffect : IEffect<CalculationProcessRequested>
{
    private readonly ILogger<CalculationProcessRequestedEffect> _logger;
    private readonly IStorage _storage;
    private readonly ICalculationsRoutingSlipBuilder _routingSlipBuilder;

    public CalculationProcessRequestedEffect(ILogger<CalculationProcessRequestedEffect> logger, IStorage storage, ICalculationsRoutingSlipBuilder routingSlipBuilder)
    {
        _logger = logger;
        _storage = storage;
        _routingSlipBuilder = routingSlipBuilder;
    }
    public void Then(BehaviorContext<CalculationsCoordinatorState, CalculationProcessRequested> context)
    {

    }

    public async Task ThenAsync(BehaviorContext<CalculationsCoordinatorState, CalculationProcessRequested> context)
    {
        _logger.LogInformation("SAGA: Process requested. Message: {message}", context.Message.ToJson());
        context.Saga.StartTime = DateTimeOffset.Now;
        await _storage.SetAsync(Constants.ConnectionId, context.Message.ConnectionId, context.Message.TrackingNumber);
        await _storage.SetAsync(context.Message.ConnectionId, context.Message.TrackingNumber);
        await _storage.SetAsync(ActivityArgumentType.Geometry, context.Message.Geometry, context.Message.TrackingNumber);

        var routingSlip = _routingSlipBuilder.GetRoutingSlip(context.Message.TrackingNumber);
        await context.Execute(routingSlip);
    }
}
