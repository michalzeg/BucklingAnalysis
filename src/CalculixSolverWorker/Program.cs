using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities;
using Infrastructure.MassTransit;
using CalculixSolverWorker;
using Infrastructure.Utils;
using CalculationActivities.CalculixFiles;
using CalculixSolverWorker.Services;
using Infrastructure.Storage;
using Infrastructure.Redis;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<ISolver, Solver>();
builder.Services.AddScoped<ICalculixFileManager, CalculixFileManager>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddMassTransitMultipleActivityConfig(builder.Configuration, cfg =>
{
    cfg.AddExecuteActivity<RunLinearAnalysisActivity, ActivityArgument>();
    cfg.AddExecuteActivity<RunBucklingAnalysisActivity, ActivityArgument>();
    cfg.AddExecuteActivity<RunNonLinearAnalysisActivity, ActivityArgument>();
});
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
