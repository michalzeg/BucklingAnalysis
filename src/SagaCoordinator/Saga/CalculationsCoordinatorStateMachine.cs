using MassTransit;
using MassTransit.Courier.Contracts;
using SagaCoordinator.Effects;
using Shared.Events;

namespace SagaCoordinator.Saga;

public class CalculationsCoordinatorStateMachine : MassTransitStateMachine<CalculationsCoordinatorState>
{
    private readonly ILogger<CalculationsCoordinatorStateMachine> _logger;
    private readonly IEffectsProvider _effectsProvider;

    public CalculationsCoordinatorStateMachine(ILogger<CalculationsCoordinatorStateMachine> logger, IEffectsProvider effectsProvider)
    {
        _logger = logger;
        _effectsProvider = effectsProvider;
        InstanceState(x => x.CurrentState);

        Event(() => CalculationProcessRequested, x => x.CorrelateById(context => context.Message.TrackingNumber));
        Event(() => ActivitySlipCompleted, x => x.CorrelateById(context => context.Message.TrackingNumber));
        Event(() => SlipCompleted, x => x.CorrelateById(context => context.Message.TrackingNumber));
        Event(() => CalculixSolverProgress, x => x.CorrelateById(context => context.Message.TrackingNumber));

        Initially(
            When(CalculationProcessRequested)
            .Effect(_effectsProvider)
            .TransitionTo(Executing)
        );

        During(Executing,
            When(ActivitySlipCompleted)
                .Effect(_effectsProvider),
            When(CalculixSolverProgress)
                .Effect(_effectsProvider),
            When(SlipCompleted)
                .Effect(_effectsProvider)
                .Finalize()
        );

        During(Final,
            Ignore(ActivitySlipCompleted)
            );

        SetCompletedWhenFinalized();
    }

    public State? Executing { get; private set; }

    public Event<CalculationProcessRequested>? CalculationProcessRequested { get; private set; }
    public Event<RoutingSlipActivityCompleted>? ActivitySlipCompleted { get; private set; }
    public Event<RoutingSlipCompleted>? SlipCompleted { get; private set; }
    public Event<CalculixSolverProgress>? CalculixSolverProgress { get; private set; }

}

public class ProcessSagaDefinition : SagaDefinition<CalculationsCoordinatorState>
{
    public ProcessSagaDefinition()
    {
        ConcurrentMessageLimit = 1;
    }

    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<CalculationsCoordinatorState> sagaConfigurator, IRegistrationContext context)
    {
        base.ConfigureSaga(endpointConfigurator, sagaConfigurator, context);

        endpointConfigurator.UseInMemoryOutbox(context);
    }
}