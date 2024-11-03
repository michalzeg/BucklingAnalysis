using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using Infrastructure.MassTransit;
using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using static Infrastructure.Extensions.GlobalExtensions;

namespace CalculationActivities.CalculationsRoutingSlip;

public class CalculationsRoutingSlipBuilder : ICalculationsRoutingSlipBuilder
{
    private readonly ILogger<CalculationsRoutingSlipBuilder> _logger;
    private readonly IEndpointNameFormatter _formatter;
    private readonly RabbitConfig _config;

    public CalculationsRoutingSlipBuilder(ILogger<CalculationsRoutingSlipBuilder> logger, IEndpointNameFormatter formatter, RabbitConfig config)
    {
        _logger = logger;
        _formatter = formatter;
        _config = config;
    }

    public RoutingSlip GetRoutingSlip(Guid trackingNumber)
    {
        _logger.LogInformation("Starting building routing slip: {trackingNumber}", trackingNumber);

        var builder = new RoutingSlipBuilder(trackingNumber);

        AddActivity<TriangulateActivity>(builder);
        AddActivity<GenerateStructureActivity>(builder);
        AddActivity<GenerateFacadeActivity>(builder);
        AddActivity<GenerateLinearAnalysisCalculixInputActivity>(builder);
        AddActivity<RunLinearAnalysisActivity>(builder);
        AddActivity<ParseLinearAnalysisResultsActivity>(builder);
        AddActivity<FilterLinearAnalysisResultsActivity>(builder);
        AddActivity<GenerateBucklingAnalysisCalculixInputActivity>(builder);
        AddActivity<RunBucklingAnalysisActivity>(builder);
        AddActivity<ParseBucklingAnalysisResultsActivity>(builder);
        AddActivity<FilterBucklingAnalysisResultsActivity>(builder);
        AddActivity<NormalizeBucklingShapeActivity>(builder);
        AddActivity<GenerateImperfectionsActivity>(builder);
        AddActivity<GenerateNonLinearAnalysisCalculixInputActivity>(builder);
        AddActivity<RunNonLinearAnalysisActivity>(builder);
        AddActivity<ParseNonLinearAnalysisResultsActivity>(builder);
        AddActivity<FilterNonLinearAnalysisResultsActivity>(builder);

        _logger.LogInformation("Finished building routing slip: {trackingNumber}", trackingNumber);

        return builder.Build();
    }

    private void AddActivity<T>(RoutingSlipBuilder builder, ActivityArgument? argument = null)
        where T : class, IExecuteActivity<ActivityArgument>
    {
        var name = _formatter.ExecuteActivity<T, ActivityArgument>();
        var qName = _config.GetQueueUri(name);
        builder.AddActivity(GetName<T>(), qName, argument ?? new());
    }
}