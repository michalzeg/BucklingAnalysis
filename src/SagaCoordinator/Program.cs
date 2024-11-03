using CalculationActivities.CalculationsRoutingSlip;
using Infrastructure.MassTransit;
using Infrastructure.Redis;
using Infrastructure.Storage;
using Infrastructure.Utils;
using MassTransit;
using MassTransit.Courier.Contracts;
using SagaCoordinator.Effects;
using SagaCoordinator.Saga;
using Shared.Events;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<ICalculationsRoutingSlipBuilder, CalculationsRoutingSlipBuilder>();
builder.Services.AddSingleton<IEndpointNameFormatter>(_ => new DefaultEndpointNameFormatter(includeNamespace: false));
builder.Services.AddMassTransitSagaConfig<CalculationsCoordinatorStateMachine>(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);

builder.Services.AddTransient<RoutingSlipActivityCompletedEffect>();
builder.Services.AddTransient<RoutingSlipCompletedEffect>();
builder.Services.AddTransient<CalculixSolverProgressEffect>();
builder.Services.AddTransient<CalculationProcessRequestedEffect>();
builder.Services.AddTransient<EffectsProvider>();
builder.Services.AddTransient<IEffectsProvider, EffectsProvider>(s =>
{
    var result = s.GetRequiredService<EffectsProvider>();
    result.Add<RoutingSlipActivityCompleted, RoutingSlipActivityCompletedEffect>();
    result.Add<RoutingSlipCompleted, RoutingSlipCompletedEffect>();
    result.Add<CalculixSolverProgress, CalculixSolverProgressEffect>();
    result.Add<CalculationProcessRequested, CalculationProcessRequestedEffect>();

    return result;
});

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
