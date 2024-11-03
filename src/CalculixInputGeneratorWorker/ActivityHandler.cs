using CalculationActivities;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculationActivities.CalculixFiles;
using CalculixInputGeneratorWorker.CalculixInpuGenerator;
using CalculixInputGeneratorWorker.Extensions;
using Newtonsoft.Json;
using Shared;
using Shared.Contracts.Structure;
using Shared.Storage;

namespace CalculixInputGeneratorWorker;

public class ActivityHandler : IActivityHandler
{
    private readonly ICalculixFileManager _calculixFileManager;
    private readonly IStorage _storage;

    public ActivityHandler(ICalculixFileManager calculixFileManager, IStorage storage)
    {
        _calculixFileManager = calculixFileManager;
        _storage = storage;
    }

    public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
    {


        if (context.ActivityName == nameof(GenerateLinearAnalysisCalculixInputActivity))
        {
            var filePath = _calculixFileManager.GetInputFileName(context.TrackingNumber, AnalysisType.Linear);
            var baseStructure = await _storage.GetAsync<StructureDetails>(ActivityArgumentType.BaseStructure, context.TrackingNumber);
            await LinearAnalysisCalculixInputGenerator.Instance.Run(baseStructure, filePath);
        }
        else if (context.ActivityName == nameof(GenerateBucklingAnalysisCalculixInputActivity))
        {
            var filePath = _calculixFileManager.GetInputFileName(context.TrackingNumber, AnalysisType.Buckling);
            var baseStructure = await _storage.GetAsync<StructureDetails>(ActivityArgumentType.BaseStructure, context.TrackingNumber);
            await BucklingAnalysisCalculixInputGenerator.Instance.Run(baseStructure, filePath);
        }
        else if (context.ActivityName == nameof(GenerateNonLinearAnalysisCalculixInputActivity))
        {
            var filePath = _calculixFileManager.GetInputFileName(context.TrackingNumber, AnalysisType.Nonlinear);
            var imperfectedStructure = await _storage.GetAsync<StructureDetails>(ActivityArgumentType.ImperfectedStructure, context.TrackingNumber);
            await NonLinearAnalysisCalculixInputGenerator.Instance.Run(imperfectedStructure, filePath);
        }


        return argument;
    }
}
