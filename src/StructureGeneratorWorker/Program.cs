using CalculationActivities;
using StructureGeneratorWorker;
using Infrastructure.MassTransit;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using Infrastructure.Utils;
using Infrastructure.Storage;
using StructureGeneratorWorker.Templates;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IActivityHandler, ActivityHandler>();
builder.Services.AddScoped<IElementTemplate, HBeamElementTemplate>();
builder.Services.AddScoped<IStructureTemplate, HBeamStructureTemplate>();
builder.Services.AddScoped<IStructureGenerator, StructureGenerator>();
builder.Services.AddMassTransitActivityConfig<GenerateStructureActivity, ActivityArgument>(builder.Configuration);
builder.Services.AddStorage(builder.Configuration);

var host = builder.Build();
await Wait.Execute();
await host.RunAsync();
