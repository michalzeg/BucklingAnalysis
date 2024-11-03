
using CalculationActivities;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using Infrastructure.MassTransit;
using Infrastructure.Redis;
using Infrastructure.Storage;
using Infrastructure.Utils;
using TriangulatorWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddMassTransitActivityConfig<TriangulateActivity, ActivityArgument>(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
