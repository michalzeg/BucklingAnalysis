using CalculationActivities;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities.CalculixFiles;
using CalculixResultParserWorker.CalculixParser;
using Shared;
using Shared.Storage;

namespace CalculixResultParserWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly ICalculixFileManager _calculixFileManager;
    private readonly IEnumerable<ICalculixFileParser> _calculixFileParsers;
    private readonly IStorage _storage;

    public ActivityHandler(ICalculixFileManager calculixFileManager, IEnumerable<ICalculixFileParser> calculixFileParsers, IStorage storage)
    {
        _calculixFileManager = calculixFileManager;
        _calculixFileParsers = calculixFileParsers;
        _storage = storage;
    }

    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {

        var analysisType = context.ActivityName switch
        {
            nameof(ParseLinearAnalysisResultsActivity) => AnalysisType.Linear,
            nameof(ParseBucklingAnalysisResultsActivity) => AnalysisType.Buckling,
            nameof(ParseNonLinearAnalysisResultsActivity) => AnalysisType.Nonlinear,
            _ => throw new NotImplementedException(context.ActivityName)
        };

        var resultType = context.ActivityName switch
        {
            nameof(ParseLinearAnalysisResultsActivity) => ActivityArgumentType.LinearAnalysisResults,
            nameof(ParseBucklingAnalysisResultsActivity) => ActivityArgumentType.BucklingAnalysisResults,
            nameof(ParseNonLinearAnalysisResultsActivity) => ActivityArgumentType.NonLinearAnalysisResults,
            _ => throw new NotImplementedException(context.ActivityName)
        };

        var parser = analysisType switch
        {
            AnalysisType.Buckling => _calculixFileParsers.OfType<BucklingAnalysisCalculixFileParser>().Single() as ICalculixFileParser,
            AnalysisType.Linear or AnalysisType.Nonlinear => _calculixFileParsers.OfType<StaticAnalysisCalculixFileParser>().Single(),
            _ => throw new ArgumentOutOfRangeException(nameof(analysisType))
        };


        var file = _calculixFileManager.GetResultFileName(context.TrackingNumber, analysisType);
        var results = await parser.Parse(file);
        await _storage.SetAsync(resultType, results, context.TrackingNumber);

        return argument;
    }
}
