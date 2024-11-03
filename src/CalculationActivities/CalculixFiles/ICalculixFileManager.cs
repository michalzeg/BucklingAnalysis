using Shared;

namespace CalculationActivities.CalculixFiles;

public interface ICalculixFileManager
{
    string GetInputFileName(Guid trackingNumber, AnalysisType analysisType);
    string GetModelName(Guid trackingNumber, AnalysisType analysisType);
    string GetResultFileName(Guid trackingNumber, AnalysisType analysisType);
}
