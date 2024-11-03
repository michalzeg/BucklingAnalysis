using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities;
using CalculixInputGeneratorWorker;
using Infrastructure.MassTransit;
using Infrastructure.Utils;
using CalculationActivities.CalculixFiles;
using Infrastructure.Storage;
using Infrastructure.Redis;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<ICalculixFileManager, CalculixFileManager>();
builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddMassTransitMultipleActivityConfig(builder.Configuration,cfg =>
{
    cfg.AddExecuteActivity<GenerateLinearAnalysisCalculixInputActivity, ActivityArgument>();
    cfg.AddExecuteActivity<GenerateBucklingAnalysisCalculixInputActivity, ActivityArgument>();
    cfg.AddExecuteActivity<GenerateNonLinearAnalysisCalculixInputActivity, ActivityArgument>();
});
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
