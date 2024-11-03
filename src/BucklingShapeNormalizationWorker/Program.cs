
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities;
using Infrastructure.MassTransit;
using Infrastructure.Utils;
using BucklingShapeNormalizationWorker;
using Infrastructure.Storage;
using Infrastructure.Redis;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddMassTransitActivityConfig<NormalizeBucklingShapeActivity, ActivityArgument>(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
