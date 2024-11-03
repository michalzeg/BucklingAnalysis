
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities;
using Infrastructure.MassTransit;
using Infrastructure.Utils;
using ImperfectionGeneratorWorker;
using Infrastructure.Storage;
using Infrastructure.Redis;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddMassTransitActivityConfig<GenerateImperfectionsActivity, ActivityArgument>(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
