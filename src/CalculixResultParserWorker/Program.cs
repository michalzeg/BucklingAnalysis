using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities;
using Infrastructure.MassTransit;
using Infrastructure.Utils;
using CalculixResultParserWorker;
using CalculationActivities.CalculixFiles;
using Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CalculixResultParserWorker.CalculixParser;
using CalculixResultParserWorker.ResultLineParser;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<ICalculixFileManager, CalculixFileManager>();
builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddScoped<IResultLineParser, RegexLineParser>();
builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<ICalculixFileParser, BucklingAnalysisCalculixFileParser>());
builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<ICalculixFileParser, StaticAnalysisCalculixFileParser>());
builder.Services.AddMassTransitMultipleActivityConfig(builder.Configuration, cfg =>
{
    cfg.AddExecuteActivity<ParseLinearAnalysisResultsActivity, ActivityArgument>();
    cfg.AddExecuteActivity<ParseBucklingAnalysisResultsActivity, ActivityArgument>();
    cfg.AddExecuteActivity<ParseNonLinearAnalysisResultsActivity, ActivityArgument>();
});
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
