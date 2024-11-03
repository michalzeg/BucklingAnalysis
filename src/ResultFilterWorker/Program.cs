
using CalculationActivities;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using Infrastructure.MassTransit;
using Infrastructure.Storage;
using Infrastructure.Utils;
using ResultFilterWorker;
using ResultFilterWorker.Filters;
using ResultFilterWorker.Storage;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddScoped<StaticAnalysisResultFilter>();
builder.Services.AddScoped<BucklingAnalysisResultFilter>();
builder.Services.AddScoped<IResultFilterProvider, ResultFilterProvider>();
builder.Services.AddScoped<IStorageProvider, StorageProvider>();

builder.Services.AddMassTransitMultipleActivityConfig(builder.Configuration, cfg =>
{
    cfg.AddExecuteActivity<FilterLinearAnalysisResultsActivity, ActivityArgument>();
    cfg.AddExecuteActivity<FilterBucklingAnalysisResultsActivity, ActivityArgument>();
    cfg.AddExecuteActivity<FilterNonLinearAnalysisResultsActivity, ActivityArgument>();
});
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
